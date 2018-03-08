<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PasswordReset.aspx.cs" Inherits="PasswordReset" Title="Password Reset" %>
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

