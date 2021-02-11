<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="PasswordReset, App_Web_ozkuslr0" title="Password Reset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">


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
              <li id="portaltab-ea" class="selected"><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/EatingAnalysis.aspx">Eating Analysis</asp:HyperLink></li>
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
          <div class="">


     <asp:ChangePassword ID="ChangePassword1" runat="server" DisplayUserName="True" CancelDestinationPageUrl="LoginPage.aspx" ContinueDestinationPageUrl="default.html">
    </asp:ChangePassword>



        </div></div></div>
    <div class="row">
      <div class="col-md-10 col-md-offset-1 footer">
        <p>&copy; 2013 - Regents of the University of California  &mdash;
        If any of these links no longer work, please let us know at <a href="mailto:eatfit@ucdavis.edu">eatfit@ucdavis.edu</a> </p>
      </div><!--/.footer-->
    </div><!--/.row-->
    
</asp:Content>

