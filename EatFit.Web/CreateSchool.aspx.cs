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
using EatFit.Data;

public partial class CreateSchool : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Display schools when page is first loaded:
        if (!IsPostBack)
        {
            // Load the schools into a list and then sort it.
            //System.Collections.Generic.List<School> schoolList = new System.Collections.Generic.List<School>();

            lboxSchools.DataSource = Schools.GetAllSchools();
            lboxSchools.DataBind();
        }
    }

    protected void butCreateSchool_Click(object sender, EventArgs e)
    {
        string schoolName = tbTxtSchool.Text;
        string schoolCity = tbSchoolCity.Text;
        string schoolState = tbSchoolState.Text;
        if (!Schools.SchoolExists(schoolName, schoolCity, schoolState))
        {
            Schools.CreateSchool(schoolName, schoolCity, schoolState);
            // reload the schools into a list.
            lboxSchools.DataSource = Schools.GetAllSchools();
            lboxSchools.DataBind();
            errorMessage.Visible = false;
        }
        else
        {
            errorMessage.Visible = true;
        }
    }
    protected void lboxSchools_SelectedIndexChanged(object sender, EventArgs e)
    {
        lboxUsers.Items.Clear();
        if (lboxSchools.SelectedItem != null)
        {
            System.Collections.Generic.List<User> userList = new System.Collections.Generic.List<User>(Schools.GetUsersInSchool(Convert.ToInt64(lboxSchools.SelectedValue)));
            userList.Sort();
            lboxUsers.DataSource = userList;
            lboxUsers.DataBind();
        }
    }
    protected void butDeleteSchool_Click(object sender, EventArgs e)
    {
        // Will also need to add logic for removing the deleted school from
        // all the users who had it!
        long school = Convert.ToInt64(lboxSchools.SelectedValue);
        Schools.DeleteSchool(school);

        // reload the schools into a list.
        lboxSchools.DataSource = Schools.GetAllSchools();
        lboxSchools.DataBind();
    }
}
