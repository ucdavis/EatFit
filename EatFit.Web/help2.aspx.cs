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

public partial class help2 : System.Web.UI.Page
{
    private static string STR_ACTIVE_VIEW_INDEX = "ActiveViewIndex";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            try
            {
                //int wizardStep = (int.TryParse(Request.QueryString[0], out wizardStep) ? wizardStep : 0);
                int wizardStep = (Session[STR_ACTIVE_VIEW_INDEX] != null ? (int)Session[STR_ACTIVE_VIEW_INDEX] : 0);
                // Index steps are 0-5.

                switch (wizardStep)
                {
                    case 0:
                        wizardStep = 1;
                        break;
                    case 1:
                        wizardStep = 2;
                        break;
                    case 2:
                        wizardStep = 2;
                        break;
                    case 3:
                        wizardStep = 2;
                        break;
                    case 4:
                        wizardStep = 3;
                        break;    
                    case 5:
                        wizardStep = 4;
                        break;
                    case 6:
                        wizardStep = 4;
                        break;
                    case 7:
                        wizardStep = 5;
                        break;
                    default:
                        wizardStep = 0;
                        break;
                } // end switch
           
                Wizard1.ActiveStepIndex = wizardStep;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Wizard1.ActiveStepIndex = 0;
            }

        }
    }
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        string strscript = "<script language=\"javascript\" type=\"text/javascript\">window.top.close();</script>";
        if (!Page.IsStartupScriptRegistered("clientScript"))
        {
            Page.RegisterStartupScript("clientScript", strscript);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strscript = "<script language=\"javascript\" type=\"text/javascript\">window.top.close();</script>";
        //if (!Page.IsStartupScriptRegistered("clientScript"))
        {
            Page.RegisterStartupScript("clientScript", strscript);
        }
    }
}
