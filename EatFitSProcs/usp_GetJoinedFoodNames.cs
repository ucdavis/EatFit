using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_getJoinedFoodsNames(String like, int byID)
    {
        // Put your code here
        SqlConnection con = new SqlConnection("Context Connection=true");

        String query = "SELECT * FROM dbo.joined_foods_view";
        bool needAnd = false;
        
        // append filter, either choose by LIKE or by ID
        if (like != null && like != "")
        {
            query += " WHERE foodname LIKE '%" + like + "%'";
            needAnd = true;
        }

        if (byID > 0)
        {
            if (needAnd)
            {
                query += " AND ";
            }
            else
            {
                query += " WHERE ";
            }

            query += " foodid = " + byID;
        }

        query += " ORDER BY foodname";

        SqlCommand cmd = new SqlCommand(@query, con);

        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        con.Close();
    }
};
