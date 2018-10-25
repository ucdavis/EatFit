<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" Title="Create New User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    <AjaxControlToolkit:NoBot ID="NoBot1" runat="server" />
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/EatingAnalysis.aspx" OnCreatedUser="CreateUserWizard1_CreatedUser" OnCreateUserError="CreateUserWizard1_CreateUserError" >
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>

    <div class="header">
      <div class="bs-container">
        <div class="row">
            <div class="logo col-md-7"><a href="https://eatfit.net/"><img src="http://v.caes.ucdavis.edu/EatFit/Diazo/img/logo.png" alt="Eatfit" /></a></div>
            <div class="ea-box col-md-5"><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/EatingAnalysis.aspx"><h3>Explore what you eat</h3></asp:HyperLink></div>
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
              <li id="portaltab-index_html" class="plain"><a href="https://eatfit.net" title="">Home</a></li>
              <li id="portaltab-ea" class="selected"><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/EatingAnalysis.aspx">Eating analysis</asp:HyperLink></li>
              <li id="portaltab-goals" class="plain"><a href="https://eatfit.net/goals/setting-goals" title="">My goals</a></li>
              <li id="portaltab-recipes" class="plain"><a href="https://eatfit.net/recipes" title="">Recipes</a></li>
              <li id="portaltab-exercises" class="plain"><a href="https://eatfit.net/exercises/ex-menu" title="">Exercises</a></li>
              <li id="portaltab-about" class="plain"><a href="https://eatfit.net/about/teachers" title="">About</a></li>
          </ul></div><!--/.nav-collapse -->
      </div>
    </div>

     <div class="main bs-container">
      <div class="row">
        <div class="leftcol col-md-3">
        <nav>
          <ul>
              <li><asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/EatingAnalysis.aspx">eating analysis</asp:HyperLink></li>
              <li><a href="https://eatfit.net/about/teachers">about the analysis</a></li>
              <li><a href="https://eatfit.net/help">need help?</a></li>
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
          <div class="col-md-4">

                <div class="form">
                    <table border="0">
                        <tr>
                            <td align="left" colspan="2">Sign up for your new account</td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name</asp:Label></td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <asp:Label ID="lblUsernameError" runat="server" EnableViewState="False" ForeColor="Red" Visible="False" Width="194px">Username already exists.  Please select another.</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" ValidationGroup="vgPassword" CausesValidation="True" ToolTip="Must be at least 7 chars, include 1 special char (!_.-*@#$%, etc), and contain no spaces."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>&nbsp;
    
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="(?=^.{7,30}$)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{&quot;:;'?/>.<,])(?!.*\s).*$" ValidationGroup="vgPassword" Width="368px" ControlToValidate="Password">Password must be at least 7 characters, include 1 special character (!_.-*@#$%, etc), and contain no spaces.</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td  class="label">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password</asp:Label></td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" ValidationGroup="vgPassword"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirm Password must match."
                                    ValidationGroup="vgPassword" Width="368px"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail</asp:Label></td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="Question" runat="server" ValidationGroup="CreateUserWizard1">
                                    <asp:ListItem Selected="True" Value="-1">Choose a security question</asp:ListItem>
                                    <asp:ListItem>What is your favorite pet?</asp:ListItem>
                                    <asp:ListItem>What is your favorite fruit?</asp:ListItem>
                                    <asp:ListItem>What is your favorite sport?</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                    ErrorMessage="Security question is required." ToolTip="Security question is required."
                                    ValidationGroup="CreateUserWizard1" InitialValue="-1" Width="7px">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer</asp:Label></td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                    ErrorMessage="Security answer is required." ToolTip="Security answer is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color: red">
                                <asp:Literal ID="ErrorMessage1" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Age <asp:RequiredFieldValidator ID="AgeRequired" runat="server" ControlToValidate="AgeDropDownList"
                                    ErrorMessage="Age is required." InitialValue="-1" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator></td>
                            <td>
                                <asp:DropDownList ID="AgeDropDownList" runat="server"
                                    DataSourceID="AgeDataSource" DataTextField="age" DataValueField="age" AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1">0</asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:ObjectDataSource ID="AgeDataSource" runat="server" OnSelecting="AgeDataSource_Selecting"
                                    SelectMethod="loadAgeField" TypeName="DataAccess"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Gender <asp:RequiredFieldValidator ID="GenderRequired" runat="server" ErrorMessage="Gender is required."
                                    ValidationGroup="CreateUserWizard1" ControlToValidate="rblstGender">*</asp:RequiredFieldValidator></td>
                            <td>
                                <asp:RadioButtonList ID="rblstGender" runat="server" RepeatDirection="Horizontal" ValidationGroup="CreateUserWizard1">
                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td class="label" >
                                <asp:Literal ID="litRoleQuestion" runat="server" Text="Role: &nbsp;&nbsp;" EnableViewState="False"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="dlistRoles" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" >
                                <asp:Literal ID="litSchoolQuestion" runat="server" Text="School" EnableViewState="False"></asp:Literal> <asp:RequiredFieldValidator ID="SchoolRequired" runat="server" ControlToValidate="dlistSchools"
                                    ErrorMessage="School is required." InitialValue="-1" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="dlistSchools" runat="server" AppendDataBoundItems="True" DataTextField="NameCityState" DataValueField="SchoolId" ValidationGroup="CreateUserWizard1">
                                    <asp:ListItem Value="-1">Choose a School</asp:ListItem>
                                    <asp:ListItem Value="1">Other</asp:ListItem>
                                    <asp:ListItem Value="0">N/A</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Grade <asp:RequiredFieldValidator ID="GradeRequired" runat="server" ControlToValidate="dlistGrades"
                                    ErrorMessage="Grade is required." InitialValue="-1" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator></td>
                            <td>
                                <asp:DropDownList ID="dlistGrades" runat="server" AppendDataBoundItems="True" DataTextField="GradeLevel"
                                    DataValueField="GradeLevelId" ValidationGroup="CreateUserWizard1">
                                    <asp:ListItem Value="-1">Choose a Grade</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <CustomNavigationTemplate>
                    <table border="0" cellspacing="5" style="width: 100%; height: 100%;">
                        <tr align="right">
                            <td align="left" colspan="0">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Create User"
                                    ValidationGroup="CreateUserWizard1" />
                            </td>
                        </tr>
                    </table>
                </div>

        </div></div></div>
    <div class="row">
      <div class="col-md-10 col-md-offset-1 footer">
        <p>&copy; 2013 - Regents of the University of California  &mdash;
        If any of these links no longer work, please let us know at <a href="mailto:eatfit@ucdavis.edu">eatfit@ucdavis.edu</a> </p>
      </div><!--/.footer-->
    </div><!--/.row-->

    
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Previous" />
            <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="Finish" />
        </FinishNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Next" />
        </StepNavigationTemplate>
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next" />
        </StartNavigationTemplate>
    </asp:CreateUserWizard>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
        HeaderText="*required fields: " Height="12px" ValidationGroup="CreateUserWizard1" />

</asp:Content>

