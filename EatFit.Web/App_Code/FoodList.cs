using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;


/// <summary>
/// Summary description for FoodList
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class FoodList : System.Web.Services.WebService {

    public FoodList () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }

        if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        Random random = new Random();
        System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>(count);
        for (int i = 0; i < count; i++)
        {
            char c1 = (char)random.Next(65, 90);
            char c2 = (char)random.Next(97, 122);
            char c3 = (char)random.Next(97, 122);

            items.Add(prefixText + c1 + c2 + c3);
        }

        return items.ToArray();
    }

    [WebMethod]
    public string[] GetFoodList(string prefixText, int count)
    {
        System.Collections.Generic.List<string> foodList = new System.Collections.Generic.List<string>();
        DataSet ds = new DataSet();

        ObjectDataOps data = new ObjectDataOps();

        //Pull all matching food names out of the database
        ds = data.GetFoodList(prefixText);

        //Make sure we got some results
        if (ds.Tables.Count == 0)
            return null;

        //Add each result to an output string
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foodList.Add(dr["foodname"].ToString());
        }

        //return the list of matching foods
        return foodList.ToArray();
    }    
}

