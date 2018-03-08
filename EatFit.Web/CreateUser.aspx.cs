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

public partial class CreateUser : System.Web.UI.Page
{
    private static string STR_DLIST_ROLES = "dlistRoles";
    private static string STR_DLIST_SCHOOLS = "dlistSchools";
    private static string STR_DLIST_GRADES = "dlistGrades";
    private static string STR_DLIST_AGES = "AgeDropDownList";
    private static string STR_RBLIST_GENDER = "rblstGender";
    private static string STR_STUDENT = "Student";
    private static string STR_USERNAME_ERROR = "lblUsernameError";
    private static string STR_CONTENT_TEMPLATE = "CreateUserStepContainer";
    private static string STR_ERROR_MSG = "ErrorMessage1";

    public DropDownList dlistRoles
    {
        // Find the ROLES dropdown list from the page and return it.
        get     
        {
            return (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl(STR_DLIST_ROLES);
        }
    }

    public DropDownList dlistSchools
    {
        // Find the SCHOOLS dropdown list from the page and return it.
        get
        {
            return (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl(STR_DLIST_SCHOOLS);
        }
    }

    public DropDownList dlistGrades
    {
        // Find the GRADES dropdown list from the page and return it.
        get
        {
            return (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl(STR_DLIST_GRADES);
        }
    }

    public DropDownList dlistAges
    {
        // Find the AGES dropdown list from the page and return it.
        get
        {
            return (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl(STR_DLIST_AGES);
        }
    }

    public RadioButtonList rblistGender
    {
        // Find the GENDER radiobutton list from the page and return it.
        get
        {
            return (RadioButtonList)CreateUserWizardStep1.ContentTemplateContainer.FindControl(STR_RBLIST_GENDER);
        }
    }

    public Label lblUsernameError
    {
        // Find the lblUsernameError label from the page and return it.
        get
        {
            return (Label)CreateUserWizardStep1.FindControl(STR_CONTENT_TEMPLATE).FindControl(STR_USERNAME_ERROR);
        }
    }

    public Literal litErrorMessage
    {
        // Find the ErrorMessage literal from the page and return it.
        get
        {
            //return (Literal)CreateUserWizardStep1.ContentTemplateContainer.FindControl(STR_ERROR_MSG);
            return (Literal)CreateUserWizard1.FindControl(STR_CONTENT_TEMPLATE).FindControl(STR_ERROR_MSG);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Load the roles into a list and then sort it.
            System.Collections.Generic.List<string> roleList = new System.Collections.Generic.List<string>(Roles.GetAllRoles());
            roleList.Sort();

            dlistRoles.DataSource = roleList;
            dlistRoles.DataBind();

            // Attempt to find a STUDENT role and set it as selected
            ListItem student = dlistRoles.Items.FindByText(STR_STUDENT) as ListItem;

            if (student != null)
                student.Selected = true;

            // Load the schools into a list and then sort it.
            System.Collections.Generic.List<School> schoolList = new System.Collections.Generic.List<School>(Schools.GetAllSchools());
            //schoolList.Sort(); sorted at DB level.

            dlistSchools.DataSource = schoolList;
            dlistSchools.DataBind();

            // Load the grade levels into a list.
            System.Collections.Generic.List<Grade> gradeList = new System.Collections.Generic.List<Grade>(Grades.GetAllGrades());
            dlistGrades.DataSource = gradeList;
            dlistGrades.DataBind();
        }
    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        //User has already been successfully created
        // This is an addon, which adds the user to a given role.
        Roles.AddUserToRole(CreateUserWizard1.UserName, dlistRoles.SelectedValue);
        Schools.AddUserToSchool(CreateUserWizard1.UserName, Convert.ToInt64(dlistSchools.SelectedValue));
        Grades.AddUserToGrade(CreateUserWizard1.UserName, Convert.ToInt16(dlistGrades.SelectedValue));
        Users.AddAgeToUser(CreateUserWizard1.UserName, Convert.ToInt16(dlistAges.SelectedValue));
        Users.AddGenderToUser(CreateUserWizard1.UserName, rblistGender.SelectedValue);
        //Users.AddGradeToUser(CreateUserWizard1.UserName, Convert.ToInt(dlistGrades.SelectedValue));
    }
    protected void AgeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

    }

    protected void CreateUserWizard1_CreateUserError(object sender, CreateUserErrorEventArgs e)
    {
        litErrorMessage.Text = GetErrorMessage(e.CreateUserError);
        if (e.CreateUserError == MembershipCreateStatus.DuplicateUserName)
        {
            litErrorMessage.Visible = false;
            lblUsernameError.Visible = true;
        }
        else
        {
            litErrorMessage.Visible = true;
            lblUsernameError.Visible = false;
        }
    }

    public string GetErrorMessage(MembershipCreateStatus status) 
    {
        switch (status) {
            case MembershipCreateStatus.DuplicateUserName:
                //return "Username already exists. Please enter a different user name.";
                return CreateUserWizard1.DuplicateUserNameErrorMessage; 
              
            case MembershipCreateStatus.DuplicateEmail:
                //return "A username for that e-mail address already exists. Please enter a different e-mail address.";
                return CreateUserWizard1.DuplicateEmailErrorMessage;
                
            case MembershipCreateStatus.InvalidPassword:
                //return "The password provided is invalid. Please enter a valid password value.";
                return CreateUserWizard1.InvalidPasswordErrorMessage;
               
            case MembershipCreateStatus.InvalidEmail:
                //return "The e-mail address provided is invalid. Please check the value and try again.";
                return CreateUserWizard1.InvalidEmailErrorMessage;
               
            case MembershipCreateStatus.InvalidAnswer:
                //return "The password retrieval answer provided is invalid. Please check the value and try again.";
                return CreateUserWizard1.InvalidAnswerErrorMessage;
              
            case MembershipCreateStatus.InvalidQuestion:
                //return "The password retrieval question provided is invalid. Please check the value and try again.";
                return CreateUserWizard1.InvalidQuestionErrorMessage;
               
            case MembershipCreateStatus.InvalidUserName:
                return "The user name provided is invalid. Please check the value and try again.";
               
            case MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
              
            case MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
              
            default:
                //return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                return status.ToString();
               
        } // End switch
    } //End Function GetErrorMessage
}
