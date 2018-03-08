using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Text;


using CAESDO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Security.Permissions;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    DataOps dops = new DataOps();

    public void populateMinorGoalList(RadioButtonList rbl)
    {

        // gets the minor goal based on previous answers

        // grab the listings from the DB
        dops.ResetDops();
        dops.Sproc = "foo";

        DataSet ds = dops.get_dataset();

        // set the buttonlist to the right strings
        rbl.Items.FindByValue("foo").Selected = true;
    }

    public DataSet populateFoodList()
    {
        dops.ResetDops();
        dops.Sproc = "usp_getJoinedFoodsNames";
        dops.SetParameter("@like", "", "input");
        dops.SetParameter("@byID", 0, "input");

        return dops.get_dataset();
    }

    public DataSet GetFilteredFoodList(string matchingText)
    {
        dops.ResetDops();
        dops.Sproc = "usp_getJoinedFoodsNames";

        dops.SetParameter("@like", matchingText, "input");
        dops.SetParameter("@byID", 0, "input");

        return dops.get_dataset();
    }

    // saves the final analysis results to db
    public void saveToDB(EatFit.Data.User user)
    {
        dops.ResetDops();
        dops.Sproc = "usp_InsertSessionInformation";

        dops.SetParameter("@SessionID", System.Guid.NewGuid().ToString(), CAESDO.DataOps.DopsDirection.Input);

        // convert the food list to an xml stream
        DataSet ds = (DataSet) HttpContext.Current.Session["foodVars"];
        string xml;
        UTF8Encoding encoding = new UTF8Encoding();

        using (MemoryStream memoryStream = new MemoryStream())
        {
            ds.WriteXml(memoryStream);
           
            xml = encoding.GetString(memoryStream.ToArray());
        }
        // save the daily food list  
        dops.SetParameter("@mealInfoXML", xml, CAESDO.DataOps.DopsDirection.Input);

        // save the nutrient list  
        System.Collections.Specialized.StringDictionary[] nutinfo;
        // Convert the nutrients (vScore) list into an XML stream:
        nutinfo = (System.Collections.Specialized.StringDictionary[])HttpContext.Current.Session["vScore"];
        float totFat =0;
        float totSugar = 0;
        float totFruits = 0;
        float totIron = 0;
        float totCalcium = 0;
        float totHabits = 0;
        float tempInt = 0;
        foreach (System.Collections.Specialized.StringDictionary nutrients in nutinfo) 
        {
            totFat += (float.TryParse(nutrients["fat"], out tempInt) ? tempInt : 0);
            totSugar += (float.TryParse(nutrients["sugar"], out tempInt) ? tempInt : 0);
            totFruits += (float.TryParse(nutrients["fruits"], out tempInt) ? tempInt : 0);
            totIron += (float.TryParse(nutrients["iron"], out tempInt) ? tempInt : 0);
            totCalcium += (float.TryParse(nutrients["calcium"], out tempInt) ? tempInt : 0);
            totHabits += (float.TryParse(nutrients["habits"], out tempInt) ? tempInt : 0);
        }
        dops.SetParameter("@fat", totFat, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@sugar", totSugar, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@fruits", totFruits, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@iron", totIron, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@calcium", totCalcium, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@habits", totHabits, CAESDO.DataOps.DopsDirection.Input);
        /*
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            //bf.FilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            bf.Serialize(ms, nutinfo);
            //UTF8Encoding encoding = new UTF8Encoding();
            xml = Convert.ToBase64String(ms.ToArray());
            //xml = encoding.GetString(ms.ToArray());
        }
         * */
        /*
        // Deserialization test.
        System.Collections.Specialized.StringDictionary[] nutrientInfo;
        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(xml)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            nutrientInfo = (System.Collections.Specialized.StringDictionary[])bf.Deserialize(ms);
        }
        */
       
        //dops.SetParameter("@nutinfoxml", xml, CAESDO.DataOps.DopsDirection.Input);

        // save the user's choices goal choices
        int[] goals = (int[])HttpContext.Current.Session["goals"];
        dops.SetParameter("@goal1", Convert.ToInt16(goals[0]), CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@goal2", Convert.ToInt16(goals[1]), CAESDO.DataOps.DopsDirection.Input);

        // save minor goals
        dops.SetParameter("@minor_goal", Convert.ToInt16(HttpContext.Current.Session["MinorGoal"]), CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@howto_goal", Convert.ToInt16((string)HttpContext.Current.Session["EatingArea"]), CAESDO.DataOps.DopsDirection.Input);

        // save user info
        dops.SetParameter("@name", (string)HttpContext.Current.Session["personName"], CAESDO.DataOps.DopsDirection.Input);
        //dops.SetParameter("@name", user.UserName, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@age", Convert.ToInt16(HttpContext.Current.Session["age"]), CAESDO.DataOps.DopsDirection.Input);
        //dops.SetParameter("@age", user.Age, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@gender", (string)HttpContext.Current.Session["gender"], CAESDO.DataOps.DopsDirection.Input);
        //dops.SetParameter("@gender", user.Gender, CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@UserId", user.UserId, CAESDO.DataOps.DopsDirection.Input);

        // save eating habits
        string[] habits = (string[])HttpContext.Current.Session["habits"];
        dops.SetParameter("@q1", Convert.ToInt16(habits[0]), CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@q2", Convert.ToInt16(habits[1]), CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@q3", Convert.ToInt16(habits[2]), CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@q4", Convert.ToInt16(habits[3]), CAESDO.DataOps.DopsDirection.Input);
        dops.SetParameter("@q5", Convert.ToInt16(habits[4]), CAESDO.DataOps.DopsDirection.Input);

        try
        {
            dops.Execute_Sql();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw ex;
        }           
    }

    public DataSet loadAgeField()
    {
        DataSet ds = new DataSet();
        DataColumn dc = new DataColumn();
        
        dc.ColumnName = "age";
        dc.DataType = System.Type.GetType("System.Int16");
       
        DataTable dt = new DataTable();
        dt.Columns.Add(dc);

        for(int i = 1; i < 100; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            
            dt.Rows.Add(dr);
        }
        
        ds.Tables.Add(dt);
        return ds;
    }

    public DataSet LoadGoals()
    {
        dops.ResetDops();
        dops.Sproc = "usp_getGoals";

        return dops.get_dataset();

    }

    public DataSet LoadMinorGoals()
    {
        dops.ResetDops();
        dops.Sproc = "usp_GetGoalStatement";
        dops.SetParameter("nutrient", Convert.ToInt32((String) HttpContext.Current.Session["EatingArea"]), "input");

        return dops.get_dataset();
        
    }

    public string GetMinorGoalAfterText(int minorgoal)
    {
        dops.ResetDops();
        dops.Sproc = "usp_MinorGoalAfterText";
        dops.SetParameter("minorgoal", minorgoal, "input");

        //HttpContext.Current.Response.Write(minorgoal);

        return dops.get_dataset().Tables[0].Rows[0]["after_text"].ToString();
    }

    public string GetMinorGoalWhy(int nutrient, int goal)
    {
        dops.ResetDops();
        dops.Sproc = "usp_GetFeedbackWhy";
        dops.SetParameter("nutrient", nutrient, "input");
        dops.SetParameter("goal", goal, "input");

        return dops.get_dataset().Tables[0].Rows[0]["feedback_reason"].ToString();
    }

    public DataSet LoadEatingArea()
    {
        // figure out what the two mins are
        string min1 = getKudoMin1Min2(1);
        string min2 = getKudoMin1Min2(2);

        dops.ResetDops();
        dops.Sproc = "usp_GetNutrient";

        dops.SetParameter("min1", min1, "input");
        dops.SetParameter("min2", min2, "input");

        return dops.get_dataset();
    }

    #region food selection

    /// <summary>
    /// Removes the row identified by rowIndex from the FoodVars session parameter
    /// </summary>
    /// <param name="rowIndex">RowIndex to be deleted from Session["foodVars"]</param>
    public void removeFoodFromSession(int rowIndex)
    {
        DataSet ds;

        if (HttpContext.Current.Session["foodVars"] == null)
            return;

        ds = (DataSet)HttpContext.Current.Session["foodVars"];

        if (ds.Tables.Count == 0)
            return;

        ds.Tables[0].Rows.RemoveAt(rowIndex);

        HttpContext.Current.Session["foodVars"] = ds;
    }

    public void changeFoodQuantityFromSession(int rowIndex, string servsize)
    {
        double servingSize;

        //Make sure that serving size can be represented by a number
        if (double.TryParse(servsize, out servingSize) == false)
            return;

        servsize = servingSize.ToString("n");

        DataSet ds;

        if (HttpContext.Current.Session["foodVars"] == null)
            return;

        ds = (DataSet)HttpContext.Current.Session["foodVars"];

        if (ds.Tables.Count == 0)
            return;

        ds.Tables[0].Rows[rowIndex]["Quantity"] = servsize;

        //Save back to session
        HttpContext.Current.Session["foodVars"] = ds;
    }

    public void saveFoodToSession(int value)
    {

        DataSet ds;

        // copy from previous session
        if (HttpContext.Current.Session["foodVars"] != null)
        {
            ds = (DataSet) HttpContext.Current.Session["foodVars"];
        }
        else
        {
            ds = new DataSet();

            // create new one
            DataColumn dc0 = new DataColumn();
            dc0.ColumnName = "Name";
            dc0.DataType = System.Type.GetType("System.String");

            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "Quantity";
            dc1.DataType = System.Type.GetType("System.Double");

            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "Units";
            dc2.DataType = System.Type.GetType("System.String");

            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "Picture";
            dc3.DataType = System.Type.GetType("System.String");
            
            DataColumn dc4 = new DataColumn();
            dc4.ColumnName = "foodid";
            dc4.DataType = System.Type.GetType("System.Int32");
            
            DataTable dt = new DataTable();
            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            
            ds.Tables.Add(dt);
        }
                
        // grab the values from the db
        dops.ResetDops();
        dops.Sproc = "usp_GetJoinedFoodsNames";
        dops.SetParameter("@like", "", "input");
        dops.SetParameter("@byID", value, "input");

        DataSet ds1 = dops.get_dataset();

        DataRow dr = ds.Tables[0].NewRow();
        dr["Name"] = ds1.Tables[0].Rows[0]["foodname"];
        dr["Quantity"] = (double) ds1.Tables[0].Rows[0]["servsize"];
        dr["Units"] = ds1.Tables[0].Rows[0]["name"];
        dr["Picture"] = ds1.Tables[0].Rows[0]["pic_name"];
        dr["foodid"] = ds1.Tables[0].Rows[0]["foodid"];

        ds.Tables[0].Rows.Add(dr);
                
        // save it back out        
        HttpContext.Current.Session["foodVars"] = ds;
    }

    public DataSet popFoodGrid()
    {
        if (HttpContext.Current.Session["foodVars"] != null)
            return (DataSet)HttpContext.Current.Session["foodVars"];
        else
            return null;
    }

    #endregion

    // saves the food calculations to session
    public void saveCalcToSession(int gender)
    {
        System.Collections.Generic.Dictionary<string, double> foo = new System.Collections.Generic.Dictionary<string, double>();
        foo["sugar"] = 0;
        foo["fruits"] = 0;
        foo["calcium"] = 0;
        foo["iron"] = 0;
        foo["carbo"] = 0;
        foo["protein"] = 0;
        foo["alcohol"] = 0;
        foo["gFat"] = 0;
        foo["fat"] = 0;
        foo["habits"] = 0;

        if (HttpContext.Current.Session["habits"] != null)
        {
            string[] bar = (string[])HttpContext.Current.Session["habits"];

            for (int i = 0; i < 5; i++)
            {
                foo["habits"] += Convert.ToInt32(bar[i]);
            }
        }

        if (HttpContext.Current.Session["foodVars"] != null)
        {
            DataSet fds = (DataSet)HttpContext.Current.Session["foodVars"];

            // add up all the food values
            foreach (DataRow dr in fds.Tables[0].Rows)
            {

                int foodid = (int)dr["foodid"];
                double qt = (double)dr["Quantity"];

                // get the food statistics from db
                dops.ResetDops();
                dops.Sproc = "usp_getFoodPropertiesByID";
                dops.SetParameter("foodid", foodid, "input");

                DataRow result = dops.get_dataset().Tables[0].Rows[0];
                foo["sugar"] += (double)result["sothersugar"] * qt;
                foo["fruits"] += ((double)result["sfruit"] * qt) + ((double)result["sveg"] * qt);
                foo["calcium"] += (double)result["mgca"] * qt;
                foo["iron"] += (double)result["mgiron"] * qt;
                foo["carbo"] += (double)result["gcarbo"] * qt;
                foo["protein"] += (double)result["gprotein"] * qt;
                foo["alcohol"] += (double)result["galcohol"] * qt;
                foo["gFat"] += (double)result["gfat"] * qt;

                foo["fat"] += PercentFat(foo["carbo"], foo["gFat"], foo["protein"], foo["alcohol"]);
            }

            // compare them against the goals table
            dops.ResetDops();
            dops.Sproc = "usp_GetOptimalGoalsByGender";
            dops.SetParameter("gender", gender, "input");

            DataTable myresult = dops.get_dataset().Tables[0];
            //System.Collections.Generic.Dictionary<int, int> vscore = new System.Collections.Generic.Dictionary<int,int>();
            System.Collections.Specialized.StringDictionary[] vscore = new System.Collections.Specialized.StringDictionary[6];

            // apparently this really helps
            for (int i = 0; i < 6; i++)
            {
                vscore[i] = new System.Collections.Specialized.StringDictionary();
            }

            string[] keys = { "fat", "sugar", "fruits", "iron", "calcium", "habits" };
            
            foreach (DataRow dr in myresult.Rows)
            {
                int i = 0;

                foreach (string k in keys)
                {
                    
                    if (foo[k] >= (double)dr[string.Format("{0}min", k)] && foo[k] <= (double)dr[String.Format("{0}max",k)])
                    {
                        vscore[i][k] = dr["score"].ToString();
                    }

                    i++;
                }

            
            }

            // now save vscore to session
            HttpContext.Current.Session["vScore"] = vscore;
            
        }
    }

    // converted mostly literally from vb function
    public double PercentFat(double gCarbo, double gFat, double gProtein, double gAlcohol)
    {
        double cC = gCarbo * 4;
        double cF = gFat * 9;
        double cP = gProtein * 4;
        double cA = gAlcohol * 7;

        double tot = cC + cF + cP + cA;

        if (tot > 0)
        {
            return (cF / tot) * 100;
        }

        return 0;
    }

    #region major goal stuff

    public DataSet getFeedback(string nut_id, int goal_id)
    {
        dops.ResetDops();
        dops.Sproc = "usp_getJoinedFeedback";

        dops.SetParameter("nutrient_id", nut_id, "input");
        dops.SetParameter("goal_id", goal_id, "input");

        return dops.get_dataset();
    }

    public string getKudoMin1Min2(int var)
    {
        string[] value ={ "", "", "" };
        int exclude = -1;


        // grab the vscore array
        if (HttpContext.Current.Session["vscore"] != null)
        {
            System.Collections.Specialized.StringDictionary[] foo = (System.Collections.Specialized.StringDictionary[])HttpContext.Current.Session["vscore"];

            int max = -1;

            //grab the greatest value
            for (int i = 0; i < 6; i++)
            {
                foreach (string k in foo[i].Keys)
                {
                    if(Convert.ToInt32(foo[i][k]) >= max)
                    {
                        max = Convert.ToInt32(foo[i][k]);
                        value[0] = k;
                    }
                }
            }

            int min = 16;
 

            // grab the lowest value
            for (int i = 0; i < 6; i++)
            {
                foreach (string k in foo[i].Keys)
                {
                    if (Convert.ToInt32(foo[i][k]) <= min)
                    {
                        min = Convert.ToInt32(foo[i][k]);
                        value[1] = k;
                        exclude = i;

                     
                    }
                }
            }

            min = 16;

            // grab the second lowest value
            for (int i = 0; i < 6; i++)
            {
                if (exclude != i)
                {
                    foreach (string k in foo[i].Keys)
                    {
                        if (Convert.ToInt32(foo[i][k]) <= min)
                        {
                            min = Convert.ToInt32(foo[i][k]);
                            value[2] = k;

                        }
                    }
                }
            }
        }

        return value[var];
    }

    public string getMajorGoalText(int statement, int goal_choice)
    {
        //return getKudoMin1Min2(statement);
        string foo = getKudoMin1Min2(statement);

        // get the actual string
        DataSet ds = getFeedback(foo, goal_choice);
        return ds.Tables[0].Rows[0]["feedback_text"].ToString();
        
    }

    #endregion

}
