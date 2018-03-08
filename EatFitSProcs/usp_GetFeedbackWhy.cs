using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_GetFeedbackWhy(int nutrient, int goal)
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        String query = string.Format("SELECT * FROM dbo.feedback WHERE nutrient_id = {0} AND goal_id = {1}", nutrient, goal);


        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
