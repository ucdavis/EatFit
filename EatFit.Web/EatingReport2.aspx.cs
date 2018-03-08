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

public partial class EatingReport2 : System.Web.UI.Page
{
    private static string REPORT_PATH_PREFIX = null;

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
            if (User.Identity.IsAuthenticated && User.IsInRole("auditor"))
            {
                // allow user to view page:
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportServer"]);

                authorized.Visible = true;
                not_authorized.Visible = false;

                if (String.IsNullOrEmpty(Request.QueryString["command"]) == false && Request.QueryString["command"].Equals("ViewReport"))
                {
                    if (String.IsNullOrEmpty(Request.QueryString["report"]) == false)
                    {
                        ddlReportToView.SelectedValue = Request.QueryString["report"];
                        ReportViewer1.ServerReport.ReportPath = REPORT_PATH_PREFIX + Request.QueryString["report"];
                    }
                }
                else
                {
                    ReportViewer1.ServerReport.ReportPath = REPORT_PATH_PREFIX + ddlReportToView.SelectedValue.ToString();
                }
            }
        }
    }

    protected void ddlReportToView_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReportViewer1.ServerReport.ReportPath = REPORT_PATH_PREFIX + ddlReportToView.SelectedValue.ToString();
    }
}
