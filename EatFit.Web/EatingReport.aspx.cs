using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class EatingReport : System.Web.UI.Page
{
    private static string REPORT_PATH_PREFIX = null;
    private static string REPORT_NAME = "Eating Sessions by User";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (String.IsNullOrEmpty(REPORT_PATH_PREFIX))
            REPORT_PATH_PREFIX = System.Configuration.ConfigurationManager.AppSettings["ReportPathPrefix"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportServer"]);

            if (String.IsNullOrEmpty(Request.QueryString["command"]) == false && Request.QueryString["command"].Equals("ViewReport"))
            {
                if (String.IsNullOrEmpty(Request.QueryString["report"]) == false)
                {
                    ReportViewer1.ServerReport.ReportPath = REPORT_PATH_PREFIX + Request.QueryString["report"]; 
                }
            }
            else
            {
                ReportViewer1.ServerReport.ReportPath = REPORT_PATH_PREFIX + REPORT_NAME;
            }
        }
    }
}
