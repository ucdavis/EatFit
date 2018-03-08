using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_getGoals()
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        String query = "SELECT * FROM dbo.goal_id WHERE goal_id < 7";
        
        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
