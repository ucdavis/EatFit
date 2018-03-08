using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_getFoodPropertiesByID(int foodid)
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        String query = "SELECT * FROM dbo.tblfoods WHERE foodid =" + foodid;

        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
