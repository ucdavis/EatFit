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

public partial class LoginPage : System.Web.UI.Page
{
    private static string STR_TEACHER_ROLE_NAME_LOWERED = "teacher";
    private static string STR_TEACHERS_WEB_PAGE = "~/teachers.htm";

    // 2007-10-31 by kjt: Added logic for auditor/report viewer account.
    private static string STR_AUDITOR_ROLE_NAME_LOWERED = "auditor";
    private static string STR_AUDITOR_WEB_PAGE = "~/EatingReport2.aspx";

    private static string STR_EATING_ANALYSIS_WEB_PAGE = "~/EatingAnalysis.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Panel pnlLogoutControl = (Panel)Login1.FindControl("pnlLogoutControl");
            pnlLogoutControl.Visible = false;
            Panel pnlLoginControl = (Panel)Login1.FindControl("pnlLoginControl");
            pnlLoginControl.Visible = true;
            Label lblUsername = (Label)pnlLogoutControl.FindControl("lblUsername");
            MembershipUser user = null;
            if (Login1.UserName != null && Login1.UserName.Length > 0)
            {
                MembershipUserCollection users = Membership.FindUsersByName(Login1.UserName);
                user = users[Login1.UserName];
            }

            if (user != null)
            {
                // show logout button
                pnlLogoutControl.Visible = true;
                pnlLoginControl.Visible = false;
                lblUsername.Text = user.UserName;

            }
            else if (User.Identity.IsAuthenticated)
            {
                // show logout button
                pnlLogoutControl.Visible = true;
                pnlLoginControl.Visible = false;
                lblUsername.Text = User.Identity.Name;
            }
        }
    }

    protected void  Login1_LoggedIn(object sender, EventArgs e)
    {
        // All of this extra code is necessary because although the user appears to be
        // logged in, User.Identity.IsAuthenticated and User.IsInRole do not appear to
        // work properly in regards to being able to determine who they are or what
        // their role is.

        MembershipUserCollection users = Membership.FindUsersByName(Login1.UserName);
        MembershipUser user = users[Login1.UserName];
        string username = string.Empty;
        
        if (user != null)
        {
            username = user.UserName;
        }
        else if (User.Identity.IsAuthenticated)
        {
            username = User.Identity.Name;
        }

        if (!username.Equals(string.Empty))
        {
            if (Roles.IsUserInRole(username, STR_TEACHER_ROLE_NAME_LOWERED))
            {
                Login1.DestinationPageUrl = STR_TEACHERS_WEB_PAGE;
                Response.Redirect(STR_TEACHERS_WEB_PAGE);
            }
            else if (Roles.IsUserInRole(username, STR_AUDITOR_ROLE_NAME_LOWERED))
            {
                Login1.DestinationPageUrl = STR_AUDITOR_WEB_PAGE;
                Response.Redirect(STR_AUDITOR_WEB_PAGE);
            }
            else
            {
                Login1.DestinationPageUrl = STR_EATING_ANALYSIS_WEB_PAGE;
                Response.Redirect(STR_EATING_ANALYSIS_WEB_PAGE);
            }
        }
        else
        {
            Login1.DestinationPageUrl = FormsAuthentication.DefaultUrl;
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        TextBox tbUsername = (TextBox)Login1.FindControl("UserName");
        tbUsername.Text = String.Empty;
        Login1.UserName = String.Empty;
        Session.RemoveAll();
        Session.Clear();
        Session.Abandon();
        FormsAuthentication.SignOut();
        HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl);
    }
}
