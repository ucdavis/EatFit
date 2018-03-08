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
using CAESDO;
using System.Text;
using System.Web.Services;

public partial class Intro : System.Web.UI.Page
{
    private static string STR_MALE = "M";
    //private static string STR_FEMALE = "F";
    private static string STR_ARE_YOU_SURE_YOU_WANT_TO_LOGOUT = "All eating record information you entered will be lost.  Are you sure you want to logout?";
    private static string STR_YOU_MUST_ENTER_TWO_CHOICES = "You must enter two (2) choices!";
    private static string STR_HELP_PAGE_URL = "./help2.aspx";
    private static string STR_ACTIVE_VIEW_INDEX = "ActiveViewIndex";

    protected enum PageViewType
    {
        ProfileInformation,
        FoodEntry,
        FoodList,
        FoodConfirmation,
        HabitsQuestions,
        EatingAnalysis,
        MinorGoals,
        Contract
    }
    
    DataAccess da = new DataAccess();
    private EatFit.Data.User user;

    public static String SetNameSub(HttpContext context)
    {
        // fetch the name variable from session
        if (context.Session["personName"] != null)
        {
            return (String)context.Session["personName"];
        }

        return "";
    }

    protected void setActiveViewIndex(int invar)
    {
        Session[STR_ACTIVE_VIEW_INDEX] = invar;
    }

    protected int getActiveViewIndex()
    {
        return (Session[STR_ACTIVE_VIEW_INDEX] != null?(int)Session[STR_ACTIVE_VIEW_INDEX]:0);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnHelp.Attributes.Add("onclick", "return window.open('" + STR_HELP_PAGE_URL + "');");
            btnLogout.Attributes.Add("onclick", "return confirm('" + STR_ARE_YOU_SURE_YOU_WANT_TO_LOGOUT + "');");

            if (User.Identity.IsAuthenticated)
            {
                NameBox.Text = Membership.GetUser().UserName;
                user = EatFit.Data.Users.GetUser(Membership.GetUser().UserName);
                Session["user"] = user;
                if (user != null)
                {
                    AgeDropDown.SelectedValue = Convert.ToString(user.Age);
                    string strGender = user.Gender;
                    if (strGender != null)
                    {
                        if (strGender.Equals(STR_MALE))
                        {
                            RadioButtonList1.SelectedIndex = 0;
                        }
                        else
                        {
                            RadioButtonList1.SelectedIndex = 1;
                        }
                    }
                }
            }
        }
    }

    public static String getEatingAreaText(HttpContext context)
    {
        if (context.Session["EatingAreaText"] != null)
        {
            return (String)context.Session["EatingAreaText"];
        }
        return null;
    }

    protected void resetAnalysis(object sender, EventArgs e)
    {
        // reset all session variables
        Session["foodVars"] = null;
        Session["personName"] = null;
        Session["habits"] = null;
        Session["user"] = null;

        // and re-empty the select foods grid
        SelectedFoodsGridView.DataBind();

        // I do not want this happening anymore, since I'm using 
        // an authenticated user.
        // reset all buttons
        /*
        NameBox.Text = "";
        AgeDropDown.DataBind();
        AgeDropDown.SelectedIndex = 10;
        */
        // fixme, clear out the editfoodgridview

        foreach(ListItem x in GoalsLists.Items)
        {
            x.Selected = false;
        }

        // Clear out the habits questions:
        dlistEatingHabitsQ1.SelectedIndex = 0;
        dlistEatingHabitsQ2.SelectedIndex = 0;
        dlistEatingHabitsQ3.SelectedIndex = 0;
        rbtnlistEatingHabitsQ4.SelectedIndex = -1;
        rbtnlistEatingHabitsQ5.SelectedIndex = -1;
        // Clear out the eating area button:
        rbtnlistEatingArea.SelectedIndex = -1;
        // Clear out the minor goals button:
        MinorGoalList.SelectedIndex = -1;
        
        // return us to the first page
        MultiView1.ActiveViewIndex = (int) PageViewType.ProfileInformation;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // check to make sure everything is filled out
        
        try
        {
            int choicesSelected = 0;

            // check that GoalsLists has at least 2 selected items

            foreach(ListItem x in GoalsLists.Items)
            {
                if(x.Selected)
                {
                    choicesSelected++;
                }
            }

            if (NameBox.Text != "" && choicesSelected == 2)
            {
                // save some state for these vars
                Session["personName"] = NameBox.Text;
                Session["age"] = AgeDropDown.SelectedValue;
                Session["gender"] = (RadioButtonList1.SelectedValue.Equals("0") ? "F" : "M");

                int cur = 0;
                int[] foo = new int[2];

                foreach (ListItem x in GoalsLists.Items)
                {
                    if (x.Selected)
                    {
                        foo[cur] =Convert.ToInt32(x.Value);
                        cur++;
                    }
                }

                Session["goals"] = foo;
            }
            else
            {
                throw new Exception();
            }

            lblName.ForeColor = System.Drawing.Color.Black;
            lblTwoChoices.ForeColor = System.Drawing.Color.Black;

            MultiView1.ActiveViewIndex = 1;
            setActiveViewIndex(MultiView1.ActiveViewIndex);
        }
        catch(Exception ee)
        {
            //lblName.ForeColor = System.Drawing.Color.Red;
            lblTwoChoices.ForeColor = System.Drawing.Color.Red;
            lblTwoChoices.Text = STR_YOU_MUST_ENTER_TWO_CHOICES;

            MultiView1.ActiveViewIndex = 0;
            setActiveViewIndex(MultiView1.ActiveViewIndex);
        }      

    }

    #region button clicks

    protected void FindFoodButton_Click(object sender, EventArgs e)
    {
        ListBox1.DataBind();
        //doSearchLoad();
    }

    protected void MinorGoalNextButton_Click(object sender, EventArgs e)
    {
        if (MinorGoalList.SelectedIndex == -1)
        {
            lblErrorMinorGoals.Text = "Please Select a Minor Goal";
            return;
        }

        try
        {

            // save minor goal to session
            Session["MinorGoal"] = MinorGoalList.SelectedIndex;

            // try saving session to database:
            if (user == null)
                user = (EatFit.Data.User)Session["user"];
            if (user == null)
                user = EatFit.Data.Users.GetUser(Membership.GetUser().UserName);
            //da.saveToDB(EatFit.Data.Users.GetUser(Membership.GetUser().UserName));
            da.saveToDB(user);
            
            /*
            StringBuilder sb = new StringBuilder();
            System.Collections.Specialized.NameObjectCollectionBase.KeysCollection sessionKeys = Session.Keys;
            foreach ( String key in sessionKeys)
            {
                sb.Append(key + " ");
            }
            Response.Write("SessionID: ["+Session.SessionID+"]; "+ sb.ToString());
             */
        }
        finally
        {
        }
            
        // label contract stuff next page
        lblContractMajorGoal.Text = getMajorGoalText(rbtnlistEatingArea.SelectedItem.Text, "my", true);
        
        //grab alterante minorgoal text from db
        lblContractMinorGoal.Text = da.GetMinorGoalAfterText(Convert.ToInt32(MinorGoalList.SelectedValue));

        // grab the why
        lblContractWhy.Text = da.GetMinorGoalWhy(System.Convert.ToInt32((string)Session["EatingArea"]), ((int[])Session["goals"])[0]);
        
        MultiView1.ActiveViewIndex = 7;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }
    /*
    protected string getMajorGoalText(string area, string person)
    {
        string buffer = String.Format("Increasing {0} intake of ", person);

        if (area == "habits")
            buffer = String.Format("Improving on {0} eating ", person);
        else if (area == "fat" || area == "sugar")
            buffer = String.Format("Decreasing {0} intake of ", person);

        return buffer + area;
    }
    */

    protected string getMajorGoalText(string area, string person, bool lowerCase)
    {
        string INCREASING_TITLE_CASE = "Increasing";
        string INCREASING_LOWER_CASE = "increasing";
        string IMPROVING_TITLE_CASE = "Improving";
        string IMPROVING_LOWER_CASE = "improving";
        string DECREASING_TITLE_CASE = "Decreasing";
        string DECREASING_LOWER_CASE = "decreasing";

        string buffer = String.Format((lowerCase ? INCREASING_LOWER_CASE : INCREASING_TITLE_CASE) + " {0} intake of ", person);

        if (area == "habits")
            buffer = String.Format((lowerCase ? IMPROVING_LOWER_CASE : IMPROVING_TITLE_CASE) + " on {0} eating ", person);
        else if (area == "fat" || area == "sugar")
            buffer = String.Format((lowerCase ? DECREASING_LOWER_CASE : DECREASING_TITLE_CASE) + " {0} intake of ", person);

        return buffer + area;
    }

    protected string getMajorGoalText(string area, string person)
    {
        return this.getMajorGoalText(area, person, false);
    }

    protected void EatingAreaNextButton_Click(object sender, EventArgs e)
    {
        //if there was no selection, alert the user and don't change the page
        if (rbtnlistEatingArea.SelectedIndex == -1)
        {
            lblErrorEatingAreas.Text = "Please Select an Eating Area";
            return;
        }

        // save the eating area to session
        Session["EatingArea"] = rbtnlistEatingArea.SelectedItem.Value;
        Session["EatingAreaText"] = rbtnlistEatingArea.SelectedItem.Text;

        // change the major label on the next page
        lblIsMajorGoal.Text = getMajorGoalText(rbtnlistEatingArea.SelectedItem.Text, "your");
        

        MinorGoalList.DataBind();

        MultiView1.ActiveViewIndex = 6;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }
    
    protected void HabitsNextButton_Click(object sender, EventArgs e)
    {
        // deal with habits questions
        string[] habits = new string[5];

        habits[0] = dlistEatingHabitsQ1.SelectedValue;
        habits[1] = dlistEatingHabitsQ2.SelectedValue;
        habits[2] = dlistEatingHabitsQ3.SelectedValue;

        habits[3] = rbtnlistEatingHabitsQ4.SelectedValue;
        habits[4] = rbtnlistEatingHabitsQ5.SelectedValue;

        // and store it into session
        Session["habits"] = habits;

        // calculate food and store to session
        da.saveCalcToSession(RadioButtonList1.SelectedIndex);

        // populate the labels on the next view
        lblNameSub.Text = NameBox.Text;
        lblKudo.Text = da.getMajorGoalText(0, 7);
        lblMin1.Text = da.getMajorGoalText(1, ((int[])Session["goals"])[0]);
        lblMin2.Text = da.getMajorGoalText(2, ((int[])Session["goals"])[1]);

        MultiView1.ActiveViewIndex = 5;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }
    
    protected void AddButton_Click(object sender, EventArgs e)
    {
        // save the choice to session
        da.saveFoodToSession(Convert.ToInt16(ListBox1.SelectedValue));

        // insert objects into gridview
        SelectedFoodsGridView.DataBind();

        // return to previous view
        MultiView1.ActiveViewIndex = 1;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        lblMinOneFoodRequired.Visible = false;
        FoodSearchBox.Text = "";
        ListBox1.SelectedIndex = -1;

        foreach (GridViewRow gvr in SelectedFoodsGridView.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                da.changeFoodQuantityFromSession(gvr.RowIndex, ((TextBox)gvr.FindControl("tbFoodQuantity")).Text);
            }
        }
        // load/update the review gridview
        ReviewGridView.DataBind();
        MultiView1.ActiveViewIndex = 2;
        setActiveViewIndex(MultiView1.ActiveViewIndex);

        //doSearchLoad();

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        // First check to see if at least one food items has been added:
        if (SelectedFoodsGridView.Rows.Count >= 1)
        {
            // Read through the rows in the gridview and update the value
            //data.changeFoodQuantityFromSession(e.RowIndex, e.NewValues["Quantity"].ToString());
            foreach (GridViewRow gvr in SelectedFoodsGridView.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    da.changeFoodQuantityFromSession(gvr.RowIndex, ((TextBox)gvr.FindControl("tbFoodQuantity")).Text);
                }
            }


            // load/update the review gridview
            ReviewGridView.DataBind();

            MultiView1.ActiveViewIndex = 3;
            setActiveViewIndex(MultiView1.ActiveViewIndex);
        }
        else
        {
            lblMinOneFoodRequired.Visible = true;
        }
    }

    
    protected void Button4_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    // generic multiview increment function
    protected void ButtonNext_Click(object sender, EventArgs e)
    {
        //MultiView1.ActiveViewIndex = (int) e;
    }

    #endregion

    protected void setupPageView(PageViewType type)
    {
        switch (type)
        {
            case PageViewType.Contract: break;
            case PageViewType.EatingAnalysis: break;
            case PageViewType.FoodConfirmation: break;
            case PageViewType.FoodEntry: break;
            case PageViewType.FoodList: break;
            case PageViewType.HabitsQuestions: break;
            case PageViewType.MinorGoals: break;
            case PageViewType.ProfileInformation: break;
        }
    }
    protected void btnPrintContract_Click(object sender, EventArgs e)
    {
    }
    /*
    protected void btnPrintContract_Click(object sender, EventArgs e)
    {
        ExportOps eops = new ExportOps();

        StringBuilder contract = new StringBuilder();
        
        contract.Append("<b>Eating Contract</b>"+Environment.NewLine + Environment.NewLine);
        contract.Append("     I, ");
        contract.Append((string)Session["personName"]);
        contract.Append(", promise to try my best to reach my major goal of ");
        contract.Append(lblContractMajorGoal.Text);
        contract.Append(" by ");
        contract.Append(lblContractMinorGoal.Text);
        contract.Append(Environment.NewLine);
        contract.Append("     This is because ");
        //contract.Append(da.GetMinorGoalWhy(System.Convert.ToInt32((string)Session["EatingArea"]), ((int[])Session["goals"])[0]));
        contract.Append(lblContractWhy.Text);
        contract.Append(Environment.NewLine + Environment.NewLine +  Environment.NewLine);
        contract.Append("__________________       __________________" + Environment.NewLine);
        contract.Append("  Your Signature         Friend's Signature      " + Environment.NewLine);
        contract.Append(Environment.NewLine + Environment.NewLine + Environment.NewLine);
        contract.Append("__________________" + Environment.NewLine);
        contract.Append("Parent's Signature    ");

        //eops.AddPageW(contract.ToString(), 1, 1, 500);
        eops.PDFAddPage();
        eops.PDFAddFormattedTextArea(contract.ToString(), 1);

        eops.PDFDocumentTitle = "EatFit Contract";
        eops.PDFDocumentAuthor = "College of Agricultural and Environmental Sciences Dean's Office";

        eops.ExportToPDF();
    }
    */
    /*
    protected void btnPrintContract_Click(object sender, EventArgs e)
    {
        ExportOps eops = new ExportOps();

        StringBuilder contract = new StringBuilder();
        contract.Append("<table border=0 cellpadding=2 cellspacing=0 align=center width=440><tr><td style=\"width: 520px\">");
        contract.Append("<table border=\"1\" width=100% cellpadding=2 cellspacing=0 align=center>");
        contract.Append("  <tr>");
        contract.Append("    <td width=\"440\" height=\"59\" colspan=\"2\">");
        contract.Append("    <img src=\"images/eating_contract2.jpg\" width=440 height=\"59\" id=\"imgEatingContractTitle\" >");
        contract.Append("    </td></tr>");
        contract.Append("            <tr>");
        contract.Append("    <td width=\"100%\" class=\"cinfo3\" colspan=\"2\" style=\"font-size: 12pt; height: 100px; font-family: Verdana;\"><br>");
        contract.Append("        &nbsp; &nbsp;&nbsp;");
        contract.Append("    I,&nbsp;<span class=bold>");
        contract.Append((string)Session["personName"]);
        contract.Append("</span>, promise to try my best to reach my");
        contract.Append("      major goal");
        contract.Append("      of <span class=bold>");
        contract.Append(lblContractMajorGoal.Text);
        contract.Append("</span> by <span class=bold>");
        contract.Append(lblContractMinorGoal.Text);
        contract.Append("</span><br />");
        contract.Append("        &nbsp; &nbsp;&nbsp;");
        contract.Append("            This is because <span class=bold>");
        contract.Append(lblContractWhy.Text);
        contract.Append("</span>");
        contract.Append("      <br />&nbsp;</td>");
        contract.Append("  </tr>");
        contract.Append("                     <tr>");
        contract.Append("    <td width=\"50%\" class=\"cinfo3\" align=\"center\" style=\"font-size: 12pt; height: 90px\"><span class=bold>__________________</span><br>");
        contract.Append("      Your Signature</td>");
        contract.Append("    <td width=\"50%\" class=\"cinfo3\" align=\"center\" style=\"font-size: 12pt; height: 90px\"><span class=bold style=\"font-size: 12pt\">__________________</span><br>");
        contract.Append("      Friend's Signature</td>");
        contract.Append("  </tr>");
        contract.Append("  <tr>");
        contract.Append("    <td width=\"50%\" class=\"cinfo3\" align=\"center\" style=\"font-size: 12pt\"><br><span class=bold>");
        contract.Append("      __________________</span><br>");
        contract.Append("      Parent's Signature</td>");
        contract.Append("    <td width=\"50%\" class=\"cinfo3\" align=\"middle\" style=\"font-size: 12pt\">&nbsp;</td>");
        contract.Append("  </tr>");
        contract.Append("</table>");
        contract.Append("</td></tr></table>");
        eops.PDFAddPage();
        eops.PDFAddFormattedTextArea(contract.ToString(), 1);

        eops.PDFDocumentTitle = "EatFit Contract";
        eops.PDFDocumentAuthor = "College of Agricultural and Environmental Sciences Dean's Office";

        eops.ExportToPDF();
    }
    */

    protected void SelectedFoodsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataAccess data = new DataAccess();

        //Delete the row from the session
        data.removeFoodFromSession(e.RowIndex);

        //Make sure a normal delete command isn't issued
        e.Cancel = true;

        //Rebind the data to the new session dataset
        SelectedFoodsGridView.DataBind();
    }

    protected void SelectedFoodsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataAccess data = new DataAccess();

        data.changeFoodQuantityFromSession(e.RowIndex, e.NewValues["Quantity"].ToString());

        //Make sure a normal update command isn't issued
        e.Cancel = true;
        SelectedFoodsGridView.EditIndex = -1;
        //Rebind the gridview
        SelectedFoodsGridView.DataBind();
    }
    
    protected void SelectedFoodsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Only bother checking data rows
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grab out the hyperlink for this row
            Control c = e.Row.FindControl("hlinkUnits");

            //make sure we found a control
            if (c == null)
                return;

            HyperLink hlink = (HyperLink)c;

            //images/servings/.jpg is 20 chars long, so we want a navigateURL longer than that
            if (hlink.NavigateUrl.Length <= 20)
            {
                hlink.Visible = false;
                ((Label)e.Row.FindControl("lblUnits")).Visible = true;
            }
        }
    }

    protected void btnCancelFoodChoice_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void btnPreviousConfirmList_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void btnPreviousHabits_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void btnPreviousEatingAreas_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void btnPreviousMinorGoals_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 5;
        setActiveViewIndex(MultiView1.ActiveViewIndex);
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        resetAnalysis(sender, e);
        FormsAuthentication.SignOut();
        Session.Clear();
        Response.Redirect(FormsAuthentication.LoginUrl);
    }
}