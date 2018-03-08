using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_getJoinedFeedback(string nutrient_id, int goal_id)
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        String query = string.Format("SELECT * FROM dbo.joinedFeedbackview WHERE goal_id = {0} and nutrient_name = '{1}'", goal_id, nutrient_id);
        

        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
