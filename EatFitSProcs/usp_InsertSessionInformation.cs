using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Security.Permissions;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_InsertSessionInformation(string SessionID, short goal1, short goal2,
                                                    short minor_goal, short howto_goal, string name, short age, string gender, string UserId,
                                                    short q1, short q2, short q3, short q4, short q5,
                                                    float fat, float sugar, float fruits, float iron, float calcium, float habits,
                                                    string mealInfoXML)
    {
        // Notes:
        // 2007-09-25 by kjt: I had to pass in the Guids as strings and then created new ones set to the same ID
        // in order to get this to handle GUIDS correctly.

        DataSet mealInfo = new DataSet();
        UTF8Encoding encoding = new UTF8Encoding();
        
        using(MemoryStream ms = new MemoryStream(encoding.GetBytes(mealInfoXML)))
        {
            mealInfo.ReadXml(ms);
        }

        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        // Added open statement:
        con.Open();

        Guid guid = new Guid(SessionID);
        // This is most likely no longer needed because we're only saving the session information on completion.
        /*
        //First wipe out any existing information with this SessionID (since we don't want the same person
        //being counted twice
        
        string deleteSession = "DELETE FROM Session WHERE SessionID = " + SessionID;

        cmd.CommandText = deleteSession;
        cmd.ExecuteNonQuery();
        
         * */

        //Now insert the session information into the database
        string insertSession =  "INSERT session (session_id, goal1, goal2, minor_goal, howto_goal, name"
                                   + ", age, gender, person_id, q1, q2, q3, q4, q5, calcium, iron, habits, fruits"
                                   + ", sugar, fat, created)" 
                              + "VALUES (@SessionID,@goal1,@goal2,@minor_goal,@howto_goal,@name"
                              +        ",@age,@gender, @UserId,@q1,@q2,@q3,@q4,@q5,@calcium,@iron,@habits,@fruits"
                              +        ",@sugar,@fat,@created)";

        cmd.CommandText = insertSession;
        cmd.Parameters.AddWithValue("@SessionID", guid);
        cmd.Parameters.AddWithValue("@goal1", goal1);
        cmd.Parameters.AddWithValue("@goal2", goal2);
        cmd.Parameters.AddWithValue("@minor_goal", minor_goal);
        cmd.Parameters.AddWithValue("@howto_goal", howto_goal);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@age", age);
        cmd.Parameters.AddWithValue("@gender", gender);
        cmd.Parameters.AddWithValue("@UserId", new Guid(UserId)); 
        cmd.Parameters.AddWithValue("@q1", q1);
        cmd.Parameters.AddWithValue("@q2", q2);
        cmd.Parameters.AddWithValue("@q3", q3);
        cmd.Parameters.AddWithValue("@q4", q4);
        cmd.Parameters.AddWithValue("@q5", q5);

        cmd.Parameters.AddWithValue("@fat", fat);
        cmd.Parameters.AddWithValue("@sugar", sugar);
        cmd.Parameters.AddWithValue("@fruits", fruits);
        cmd.Parameters.AddWithValue("@iron", iron);
        cmd.Parameters.AddWithValue("@calcium", calcium);
        cmd.Parameters.AddWithValue("@habits", habits);

        cmd.Parameters.AddWithValue("@created", DateTime.Now);
        
        /*
        string insertSession = "exec dbo.session_CreateSession '" +guid + "'," + goal1 + "," + goal2 + "," + minor_goal + "," + howto_goal + ",'" + name + "'," + age + "," + q1 + age + "," + q2 + "," + q3 + "," + q4 + "," + q5;
        cmd.CommandText = insertSession;
         * */
        cmd.ExecuteNonQuery();
        
        cmd.Parameters.Clear();//remove all parameters
        
        //Now insert the foodIDs to from the mealInfo dataSet into the db
        if (mealInfo.Tables.Count != 0)
        {
            foreach (DataRow dr in mealInfo.Tables[0].Rows)
            {
                //for each row, insert the relevant information into the database
                string insertMeal = "INSERT Meal (session_goal_id, food_id, quantity) VALUES (@Session_goal_id, @food_id, @quantity)";
                cmd.CommandText = insertMeal;

                cmd.Parameters.AddWithValue("@Session_goal_id", guid);
                cmd.Parameters.AddWithValue("@food_id", dr["foodid"]);
                cmd.Parameters.AddWithValue("@quantity", dr["Quantity"]);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }

        // Added statement to close database connection.
        con.Close();
    }
};
