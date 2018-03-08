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
using AjaxControlToolkit;

public partial class UserInfo : System.Web.UI.Page
{
    private static string STR_LAST_LOGIN_PREFIX = "Your last login was on: ";
    private static string STR_CREATION_DATE_PREFIX = "Your was created on: ";
    private static string STR_LAST_PASSWORD_CHANGED_PREFIX = "Your password was last changed on: ";
    private static string STR_EMAIL_UPDATED_MESSAGE = "Email Updated.";
    private static string STR_PASSWORD_QUESTION_CHANGED_MESSAGE = "Password Question Changed.";
    private static string STR_PASSWORD_QUESTION_FAILED_MESSAGE = "Password Question Failed.";
    private static string STR_PASSWORD_CHANGED_MESSAGE = "Password Changed.";
    private static string STR_PASSWORD_FAILED_MESSAGE = "Password Failed.";
    private static string STR_AGE_UPDATED_MESSAGE = "Age Updated.";
    private static string STR_SCHOOL_UPDATED_MESSAGE = "School Updated.";
    private static string STR_SCHOOL_FAILED_MESSAGE = "School Update Failed.";
    private static string STR_GRADE_LEVEL_UPDATED_MESSAGE = "Grade Level Updated.";
    private static string STR_GRADE_LEVEL_FAILED_MESSAGE = "Grade Level Failed.";
    private static System.Drawing.Color RED = System.Drawing.Color.Red;
    private static System.Drawing.Color GREEN = System.Drawing.Color.DarkGreen;

    private MembershipUser user = null;
    private string UserName = null;
    EatFit.Data.User userDetail = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Membership.GetUser();
        UserName = user.UserName;
        userDetail = EatFit.Data.Users.GetUser(UserName);
        litLastLoginDate.Text = STR_LAST_LOGIN_PREFIX + user.LastLoginDate;
        litCreationDate.Text = STR_CREATION_DATE_PREFIX + user.CreationDate;
        litLastPasswordChangedDate.Text = STR_LAST_PASSWORD_CHANGED_PREFIX + user.LastPasswordChangedDate;
       
        if (!IsPostBack)
        {
            //EatFit.Data.User user = EatFit.Data.Users.GetUser(Membership.GetUser().UserName);
            lblMessage.Visible = false;
            tbUserId.Text = user.UserName.ToString();
            dlistPasswordQuestion.SelectedValue = user.PasswordQuestion;
            tbEmail.Text = user.Email;
            dlistAges.SelectedValue = Convert.ToString(userDetail.Age);

            /*
            // Load the schools into a list.
            //System.Collections.Generic.List<School> schoolList = new System.Collections.Generic.List<School>(Schools.GetAllSchools());

            dlistSchools.DataSource = new System.Collections.Generic.List<School>(Schools.GetAllSchools()); 
            dlistSchools.DataBind();
            */
            dlistSchools.SelectedValue = Convert.ToString(userDetail.SchoolId);
           

            /*
            // Load the grade levels into a list.
            //System.Collections.Generic.List<Grade> gradeList = new System.Collections.Generic.List<Grade>(Grades.GetAllGrades());
            dlistGrades.DataSource = new System.Collections.Generic.List<Grade>(Grades.GetAllGrades());
            dlistGrades.DataBind();
             */
            dlistGrades.SelectedValue = Convert.ToString(userDetail.GradeLevelId);
            
        } 
    }

    protected void btnChangeQuestion_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        if (user.ChangePasswordQuestionAndAnswer(tbCurrentPassword.Text, dlistPasswordQuestion.SelectedValue, tbPasswordAnswer.Text))
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = GREEN;
            lblMessage.Text = STR_PASSWORD_QUESTION_CHANGED_MESSAGE;
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = RED;
            lblMessage.Text = STR_PASSWORD_QUESTION_FAILED_MESSAGE;
        }
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        if (user.ChangePassword(tbCurrentPassword.Text, tbNewPassword.Text))
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = GREEN;
            lblMessage.Text = STR_PASSWORD_CHANGED_MESSAGE;
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = RED;
            lblMessage.Text = STR_PASSWORD_FAILED_MESSAGE;
        }
    }
    protected void btnChangeEmail_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        user.Email = tbEmail.Text;
        Membership.UpdateUser(user);
        lblMessage.Visible = true;
        lblMessage.ForeColor = GREEN;
        lblMessage.Text = STR_EMAIL_UPDATED_MESSAGE;

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

    }
    protected void btnUpdateAge_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        Users.AddAgeToUser(UserName, Convert.ToInt16(dlistAges.SelectedValue));
        lblMessage.Visible = true;
        lblMessage.ForeColor = GREEN;
        lblMessage.Text = STR_AGE_UPDATED_MESSAGE;
    }

    protected void dlistAges_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void AgeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

    }
 
    protected void btnUpdateSchool_Click(object sender, EventArgs e)
    {
        Response.Write("Name: ["+UserName+"]; SchoolId: ["+Convert.ToInt64(dlistSchools.SelectedValue)+"]");
        lblMessage.Visible = false;
        //if (Schools.AddUserToSchool(UserName, Convert.ToInt64(dlistSchools.SelectedValue)))
        if (Users.AddSchoolToUser(UserName, Convert.ToInt64(dlistSchools.SelectedValue)))
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = GREEN;
            lblMessage.Text = STR_SCHOOL_UPDATED_MESSAGE;
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = RED;
            lblMessage.Text = STR_SCHOOL_FAILED_MESSAGE;
        }
    }

    protected void btnUpdateGradeLevel_Click(object sender, EventArgs e)
    {
        Response.Write("Name: [" + UserName + "]; GradeLevelId: [" + Convert.ToInt32(dlistGrades.SelectedValue)+"]");
        lblMessage.Visible = false;
        if (Users.AddGradeLevelToUser(UserName, Convert.ToInt32(dlistGrades.SelectedValue)))
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = GREEN;
            lblMessage.Text = STR_GRADE_LEVEL_UPDATED_MESSAGE;
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = RED;
            lblMessage.Text = STR_GRADE_LEVEL_FAILED_MESSAGE;
        }

    }
}
