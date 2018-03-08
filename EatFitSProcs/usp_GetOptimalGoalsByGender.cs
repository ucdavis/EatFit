using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_GetOptimalGoalsByGender(int gender)
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        string cgender = "'M'";
        if(gender == 0)
        {
                cgender = "'F'";
        }

        String query = "SELECT * FROM dbo.optimal_goals WHERE gender = " + cgender;

        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
