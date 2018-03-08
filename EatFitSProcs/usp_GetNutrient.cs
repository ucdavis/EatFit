using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_getNutrient(string min1, string min2)
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        // weird bug
        String query = 
            string.Format("SELECT * FROM dbo.nutrient_id WHERE (nutrient_name = '{0}') OR (nutrient_name = '{1}')", min1, min2);

        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
