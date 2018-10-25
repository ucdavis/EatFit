<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="LoginPage, App_Web_ozkuslr0" title="EatFit - Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    <asp:Login ID="Login1" runat="server" CreateUserText="Create new user" CreateUserUrl="~/CreateUser.aspx" OnLoggedIn="Login1_LoggedIn" DisplayRememberMe="False" PasswordRecoveryText="Forgot your password?" PasswordRecoveryUrl="~/PasswordReset.aspx" >
        <LayoutTemplate>

    <div class="header">
      <div class="bs-container">
        <div class="row">
        <div class="logo col-md-7"><a href="https://eatfit.net/"><img src="http://v.caes.ucdavis.edu/EatFit/Diazo/img/logo.png" alt="Eatfit" /></a></div>
       <%-- <div class="ea-box col-md-5"><a href="EatingAnalysis.aspx"><h3>Explore what you eat</h3></a></div>--%>
        <div class="ea-box col-md-5"><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/EatingAnalysis.aspx"><h3>Explore what you eat</h3></asp:HyperLink>
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
              <li id="portaltab-ea" class="selected"><asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/EatingAnalysis.aspx">Eating analysis</asp:HyperLink></li>
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
              <li><asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/EatingAnalysis.aspx">eating analysis</asp:HyperLink></li>
              <li><a href="https://eatfit.net/about/teachers">about the analysis</a></li>
              <li><a href="https://eatfit.net/help">need help?</a></li>
          </ul>
        </nav>
      </div><!-- /.leftcol -->
      
      <div class="content col-md-9">
        <div class="header">
          <img src="~/images/eatinganalysis.jpg" alt="Eating Analysis" />
          <i class="icon-bolt"></i>
          <i class="icon-food"></i>
          <i class="icon-flag"></i>
          <i class="icon-info"></i>
          <i class="icon-star"></i>
          <h2>explore what you eat</h2>
        </div><!-- /.header -->
        <div class="text">
         <div class="row">
          <div class="">
            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="height: 54px">
                        <table border="0" cellpadding="0">
                                                    <tr>
                                <td colspan="2">
                                    <p>Please plan to enter all of your foods at one time. Once you start this process, data cannot be viewed or altered again later.</p>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CreateUser.aspx">Create new user</asp:HyperLink> <br />
                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/ChangePassword.aspx">Change Password</asp:HyperLink><br />
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PasswordReset.aspx">Forgot your password?</asp:HyperLink>
        
                                </td>
                            </tr>
                            <asp:Panel ID="pnlLoginControl" runat="server" Visible="true">
                            <tr>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="color: red">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                </td>
                            </tr></asp:Panel>
                        </table>
                    </td>
                </tr>
                <asp:Panel ID="pnlLogoutControl" runat="server" Visible="false">
                <tr>
                <td colspan="2">
                You are currently logged in as <asp:Label id="lblUsername" runat="server" Font-Bold="true"/>.<br /><br />Please logout or select the <asp:HyperLink id="HyperLink3" NavigateUrl="~/EatingAnalysis.aspx" runat="server">Explore what you eat</asp:HyperLink> page to continue as the current user. </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <p>
                         <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
                         </p>
                    </td>
                </tr>
                </asp:Panel>
            </table>

        </div></div></div>
    <div class="row">
      <div class="col-md-10 col-md-offset-1 footer">
        <p>&copy; 2013 - Regents of the University of California  &mdash;
        If any of these links no longer work, please let us know at <a href="mailto:eatfit@ucdavis.edu">eatfit@ucdavis.edu</a> </p>
      </div><!--/.footer-->
    </div><!--/.row-->


        </LayoutTemplate>
    </asp:Login>
    
</asp:Content>

