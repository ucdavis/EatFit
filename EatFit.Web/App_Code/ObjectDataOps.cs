using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ObjectDataOps
/// </summary>
public partial class ObjectDataOps
{

    public DataSet populateMinorGoalList()
    {
        DataSet ds = new DataSet();

        return ds;
    }

    public DataSet popReviewGrid()
    {
        DataSet ds = new DataSet();

        return ds;
    }

    public DataSet GetFoodList(string matchingText)
    {
        CAESDO.DataOps dops = new CAESDO.DataOps();
        dops.ResetDops();
        dops.Sproc = "usp_getJoinedFoodsNames";

        dops.SetParameter("@like", matchingText, "input");
        dops.SetParameter("@byID", 0, "input");

        return dops.get_dataset();
    }
}
