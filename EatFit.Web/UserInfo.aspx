<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="UserInfo" Title="User Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">


    <div class="header">
      <div class="bs-container">
        <div class="row">
        <div class="logo col-md-7"><a href="http://eatfit.ucdavis.edu/"><img src="http://v.caes.ucdavis.edu/EatFit/Diazo/img/logo.png" alt="Eatfit" /></a></div>
        <div class="ea-box col-md-5"><a href="http://eatfit.ucdavis.edu/ea"><h3>Eating Analysis</h3></a></div>
        </div>
      </div>
    </div>


    <div class="navbar navbar-inverse">
      <div class="bs-container">
        <!-- <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
        </div> -->
        <div class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
            <li id="portaltab-index_html" class="selected"><a href="http://eatfit.ucdavis.edu" title="">Home</a></li>
            <li id="portaltab-ea" class="plain"><a href="http://eatfit.ucdavis.edu/ea" title="">Eating Analysis</a></li>
            <li id="portaltab-goals" class="plain"><a href="http://eatfit.ucdavis.edu/goals" title="">My goals</a></li>
            <li id="portaltab-recipes" class="plain"><a href="http://eatfit.ucdavis.edu/recipes" title="">Recipes</a></li>
            <li id="portaltab-exercises" class="plain"><a href="http://eatfit.ucdavis.edu/exercises" title="">Exercises</a></li>
            <li id="portaltab-about" class="plain"><a href="http://eatfit.ucdavis.edu/about" title="">About</a></li>
          </ul></div><!--/.nav-collapse -->
      </div>
    </div>

     <div class="main bs-container">
      <div class="row">
        <div class="leftcol col-md-3">
        <nav>
          <ul>
            <li><a href="http://eatfit.ucdavis.edu/ea/analysis">eating analysis</a></li>
            <li><a href="http://eatfit.ucdavis.edu/ea/about">about the analysis</a></li>
            <li><a href="http://eatfit.ucdavis.edu/ea/help">need help?</a></li>
          </ul>
        </nav>
      </div><!-- /.leftcol -->
      
      <div class="content col-md-9">
        <div class="header">
          <img src="~/images/eatinganalysis.jpg" alt="" />
          <i class="icon-bolt"></i>
          <i class="icon-food"></i>
          <i class="icon-flag"></i>
          <i class="icon-info"></i>
          <i class="icon-star"></i>
          <h2>eating analysis</h2>
        </div><!-- /.header -->
        <div class="text">
         <div class="row">
          <div class="">


    <asp:Literal ID="litLastLoginDate" runat="server"></asp:Literal>
    <br />
    <asp:Literal ID="litCreationDate" runat="server"></asp:Literal><br />
    <asp:Literal ID="litLastPasswordChangedDate" runat="server"></asp:Literal><br />
    



    <table>
        <tr>
            <td>
                UserId:</td>
            <td>
                <asp:TextBox ID="tbUserId" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Current Password:
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="tbCurrentPassword"
                    ErrorMessage="Current Password is required." ValidationGroup="vgPassword">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                        ID="PasswordRequired2" runat="server" ControlToValidate="tbCurrentPassword" ErrorMessage="Current Password is required."
                        ValidationGroup="vgChangePassword">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:TextBox ID="tbCurrentPassword" runat="server" TextMode="Password" Width="148px"></asp:TextBox></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                New Password:
                <asp:RequiredFieldValidator ID="NewPasswordRequired2" runat="server" ControlToValidate="tbNewPassword"
                    ErrorMessage="New Password is required." ValidationGroup="vgChangePassword" Width="1px">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password" Width="148px" ValidationGroup="vgChangePassword"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" BackColor="LemonChiffon"
                    ControlToValidate="tbNewPassword" ErrorMessage="RegularExpressionValidator" ValidationExpression="(?=^.{7,30}$)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{&quot;:;'?/>.<,])(?!.*\s).*$"
                    Width="368px">Password must be at least 7 characters and include 1 special character (!_.-*@#$%, etc).</asp:RegularExpressionValidator></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Retype Password:
                <asp:RequiredFieldValidator ID="RetypeNewPasswordRequired2" runat="server" ControlToValidate="tbNewPassword"
                    ErrorMessage="Retype Password is required." ValidationGroup="vgChangePassword">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:TextBox ID="tbRetypePassword" runat="server" TextMode="Password" Width="148px" ValidationGroup="vgChangePassword"></asp:TextBox>
 
        <asp:CompareValidator
                    ID="PasswordCompareValidator" runat="server" BackColor="LemonChiffon" ControlToCompare="tbNewPassword"
                    ControlToValidate="tbRetypePassword" ErrorMessage="CompareValidator" ValidationGroup="vgChangePassword"
                    Width="316px">New and Retype Passwords must match.</asp:CompareValidator></td>
            <td>
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" ValidationGroup="vgChangePassword" Width="139px" /></td>
        </tr>
        <tr>
            <td>
                Password Question:
                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="dlistPasswordQuestion"
                    ErrorMessage="Password Question is required." ValidationGroup="vgPassword" InitialValue="-1">*</asp:RequiredFieldValidator></td>
            <td>
                &nbsp;<asp:DropDownList ID="dlistPasswordQuestion" runat="server" ValidationGroup="vgPassword">
                    <asp:ListItem Value="-1">Choose a security question</asp:ListItem>
                    <asp:ListItem>What is your favorite pet?</asp:ListItem>
                    <asp:ListItem>What is your favorite fruit?</asp:ListItem>
                    <asp:ListItem>What is your favorite sport?</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:Button ID="btnChangeQuestion" runat="server" Text="Change Question" Width="140px" OnClick="btnChangeQuestion_Click" ValidationGroup="vgPassword" /></td>
        </tr>
        <tr>
            <td>
                Password Answer:
                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="tbPasswordAnswer"
                    ErrorMessage="Password Answer is required." ValidationGroup="vgPassword">*</asp:RequiredFieldValidator></td>
            <td style="width: 160px; height: 18px">
                <asp:TextBox ID="tbPasswordAnswer" runat="server" ValidationGroup="vgPassword"></asp:TextBox></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Email Address:</td>
            <td style="width: 160px; height: 18px">
                <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnChangeEmail" runat="server" Text="Change Email" Width="139px" OnClick="btnChangeEmail_Click" /></td>
        </tr>
        <tr>
            <td>
                Age:
                <asp:RequiredFieldValidator ID="AgeRequired" runat="server" ControlToValidate="dlistAges"
                    ErrorMessage="Age is required." InitialValue="-1" ValidationGroup="vgAge">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:DropDownList ID="dlistAges" runat="server" AppendDataBoundItems="True" DataSourceID="AgeDataSource"
                    DataTextField="age" DataValueField="age" OnSelectedIndexChanged="dlistAges_SelectedIndexChanged"
                    ValidationGroup="vgAge">
                    <asp:ListItem Selected="True" Value="-1">0</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:ObjectDataSource ID="AgeDataSource" runat="server" OnSelecting="AgeDataSource_Selecting"
                    SelectMethod="loadAgeField" TypeName="DataAccess"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnUpdateAge" runat="server" OnClick="btnUpdateAge_Click" Text="Change Age" Width="142px" /></td>
        </tr>
        <tr>
            <td>
                School:&nbsp;
                <asp:RequiredFieldValidator ID="SchoolRequired" runat="server" ControlToValidate="dlistSchools"
                    ErrorMessage="School is required." InitialValue="-1" ValidationGroup="vgSchool">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:DropDownList ID="dlistSchools" runat="server" AppendDataBoundItems="True" DataTextField="NameCityState"
                    DataValueField="SchoolId" ValidationGroup="vgSchool" DataSourceID="SchoolsDataSource">
                    <asp:ListItem Value="-1">Choose a School</asp:ListItem>
                    <asp:ListItem Value="1">Other</asp:ListItem>
                    <asp:ListItem Value="0">N/A</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:ObjectDataSource ID="SchoolsDataSource" runat="server" SelectMethod="GetAllSchools"
                    TypeName="EatFit.Data.Schools"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnUpdateSchool" runat="server" OnClick="btnUpdateSchool_Click" Text="Change School"
                    ValidationGroup="vgSchool" Width="143px" /></td>
        </tr>
        <tr>
            <td>
                Grade Level:
                <asp:RequiredFieldValidator ID="GradeRequired" runat="server" ControlToValidate="dlistGrades"
                    ErrorMessage="Grade is required." InitialValue="-1" ValidationGroup="vgGradeLevel">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:DropDownList ID="dlistGrades" runat="server" AppendDataBoundItems="True" DataTextField="GradeLevel"
                    DataValueField="GradeLevelId" ValidationGroup="vgGradeLevel" DataSourceID="GradesDataSource">
                    <asp:ListItem Value="-1">Choose a Grade</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="GradesDataSource" runat="server" SelectMethod="GetAllGrades"
                    TypeName="EatFit.Data.Grades"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:Button ID="btnUpdateGradeLevel" runat="server" OnClick="btnUpdateGradeLevel_Click" Text="Change Grade Level"
                    ValidationGroup="vgGradeLevel" Width="143px" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <AjaxControlToolkit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lblMessage">
                    <Animations>
                        <OnLoad>
                            <Sequence>
                               <Color Duration="2"
                                    StartValue="#ffff99"
                                    EndValue="#FFFFFF"
                                    Property="style"
                                    PropertyKey="backgroundColor" />
                                <StyleAction Attribute="backgroundColor" value="" />
                                <FadeOut Duration="5" Fps="20" />
                            </Sequence>
                        </OnLoad>
                    </Animations>                         
                </AjaxControlToolkit:AnimationExtender>
                &nbsp;<asp:Label ID="lblMessage" runat="server" Font-Italic="True" ForeColor="DarkGreen" BackColor="LemonChiffon" EnableViewState="false"></asp:Label>
                <br />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="LemonChiffon"
                    DisplayMode="SingleParagraph" HeaderText="*required fields:" Height="34px" Width="602px" ValidationGroup="vgChangePassword" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" BackColor="LemonChiffon"
                    DisplayMode="SingleParagraph" HeaderText="*required fields:" Height="30px" ValidationGroup="vgPassword"
                    Width="603px" />
            </td>
        </tr>
        <tr><td colspan="2" align="center"><asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" PostBackUrl="~/EatingAnalysis.aspx" /></td><td></td></tr>
    </table>



        </div></div></div>
    <div class="row">
      <div class="col-md-10 col-md-offset-1 footer">
        <p>&copy; 2013 - Regents of the University of California  &mdash;
        If any of these links no longer work, please let us know at <a href="mailto:eatfit@ucdavis.edu">eatfit@ucdavis.edu</a> </p>
      </div><!--/.footer-->
    </div><!--/.row-->

</asp:Content>

