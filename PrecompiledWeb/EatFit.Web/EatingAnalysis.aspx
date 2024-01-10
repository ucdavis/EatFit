<%@ page language="C#" debug="true" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Intro, App_Web_ozkuslr0" title="EatFit - Explore what you eat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
<script language="javascript" type="text/javascript" src="modalbox_formatted.js"></script>
<script language="javascript" type="text/javascript" src="JScript.js"></script>

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
          <h2>Explore what you eat</h2>
        </div><!-- /.header -->
        <div class="text">
         <div class="row">
          <div class="">

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="View1" runat="server">



            <div class="headfont">Welcome!</div><br />
            Want to find out how your diet rates?<br />
            Just start by answering the questions below.<br />

            <br />
            <table summary="Information Table (Name, Age, Gender)" >
                <tr>
                    <td >
                        <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>&nbsp;</td>
                    <td> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="NameBox"
                            Display="Static" ErrorMessage="Please enter your name." InitialValue="" ></asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox ID="NameBox" runat="server" ReadOnly="True" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
    Age:&nbsp;</td>
                    <td>
                        
            <asp:DropDownList ID="AgeDropDown" runat="server" DataSourceID="AgeDataSource" DataTextField="age" DataValueField="age" AppendDataBoundItems="True" Enabled="False">
                <asp:ListItem Value="0">0</asp:ListItem>
            </asp:DropDownList><asp:ObjectDataSource ID="AgeDataSource" runat="server" SelectMethod="loadAgeField"
                TypeName="DataAccess"></asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
    Gender:&nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="RadioButtonList1"
                            Display="Static" ErrorMessage="Please choose Male or Female." InitialValue=""
                            ></asp:RequiredFieldValidator>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="1px" Width="222px" Enabled="False" >
        <asp:ListItem Value="1" >Male</asp:ListItem>
        <asp:ListItem Value="0">Female</asp:ListItem>
    </asp:RadioButtonList></td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnEditUser" runat="server" PostBackUrl="~/UserInfo.aspx" Text="Edit User" /><br />
    <br />
            Which of these are most important to you:<br />
            <asp:Label ID="lblTwoChoices" runat="server" Text="(check two choices)"></asp:Label><br />
            <div>
            &nbsp;
                <asp:CheckBoxList ID="GoalsLists" runat="server" DataSourceID="GoalsListDataSource" DataTextField="goal_name" DataValueField="goal_id">
            </asp:CheckBoxList>&nbsp;
                <asp:ObjectDataSource ID="GoalsListDataSource" runat="server"
                SelectMethod="LoadGoals" TypeName="DataAccess"></asp:ObjectDataSource>
                <br />
            </div>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Next" OnClick="Button1_Click" />

            </asp:View>
        <asp:View ID="View2" runat="server">
            




            Click on <strong>Add Food</strong> to begin.<br />
            <br />
            <asp:GridView ID="SelectedFoodsGridView" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No Foods Selected" OnRowDeleting="SelectedFoodsGridView_RowDeleting" OnRowUpdating="SelectedFoodsGridView_RowUpdating" OnRowDataBound="SelectedFoodsGridView_RowDataBound" >
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EEF7FC" ForeColor="#333333" />
                <EditRowStyle BackColor="#EEEEEE"/>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Black" CssClass="botbord" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField AccessibleHeaderText="Name" DataField="Name" HeaderText="Name" ReadOnly="True" />
                    <asp:TemplateField AccessibleHeaderText="Quantity" HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="tbFoodQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Units" HeaderText="Units">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlinkUnits" class="unitslink" runat="server" 
                                Text='<%# Eval("Units") %>' NavigateUrl='<%# String.Format("images/servings/{0}.jpg", Eval("Picture")) %>' Target="_blank" >
                            </asp:HyperLink>
                            <asp:Label ID="lblUnits" runat="server" Text='<%# Eval("Units") %>' Visible="false"></asp:Label>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <p>Click on the unit type for a picture to help you estimate your portion size.</p>

            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="popFoodGrid"
                TypeName="DataAccess" ></asp:ObjectDataSource>
            &nbsp;&nbsp;
            <asp:Label ID="lblMinOneFoodRequired" runat="server" ForeColor="Red" Text="<P>Please add at least one food item.</P>"
                Visible="False"></asp:Label><br />
            &nbsp;<asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Add Food" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Done Adding Foods" /></asp:View>
        <asp:View ID="View3" runat="server">
        <br /><div>
            <div>Available Food List</div>
            <br />
            find food name:
            <asp:TextBox ID="FoodSearchBox" runat="server" Width="300"></asp:TextBox>
            <AjaxControlToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="FoodSearchBox" ServiceMethod="GetFoodList" ServicePath="FoodList.asmx"></AjaxControlToolkit:AutoCompleteExtender>
            <asp:Button ID="FindFoodButton" runat="server" OnClick="FindFoodButton_Click" Text="Find" /><br />
            <br />
            food list:<br />
            <asp:ListBox ID="ListBox1" runat="server" Rows="10" DataSourceID="FoodListDataSource" DataTextField="foodname" DataValueField="foodid" CausesValidation="True" ValidationGroup="vgAdd">
            </asp:ListBox><asp:ObjectDataSource ID="FoodListDataSource" runat="server" SelectMethod="GetFilteredFoodList"
                TypeName="DataAccess">
                <SelectParameters>
                    <asp:ControlParameter ControlID="FoodSearchBox" DefaultValue="" Name="matchingText"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:RequiredFieldValidator ID="lblPleaseSelectAFoodItem" runat="server" ControlToValidate="ListBox1"
                ErrorMessage="Please select a food item to add." ValidationGroup="vgAdd"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Add" ValidationGroup="vgAdd" />
            <asp:Button ID="btnCancelFoodChoice" runat="server" Text="Cancel" CommandArgument="2" CommandName="Previous" OnClick="btnCancelFoodChoice_Click" /></div></asp:View>
        <asp:View ID="View4" runat="server">
            <div class="headfont">Confirmation List</div><br />
            <asp:GridView ID="ReviewGridView" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="ObjectDataSource2" >
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EEF7FC" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Black" CssClass="botbord" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField AccessibleHeaderText="Name" DataField="Name" HeaderText="Name" ReadOnly="True" />
                <asp:BoundField AccessibleHeaderText="Quantity" DataField="Quantity" HeaderText="Quantity" ReadOnly="True" />
                <asp:BoundField AccessibleHeaderText="Units" DataField="Units" HeaderText="Units" ReadOnly="True" />
            </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="popFoodGrid"
                TypeName="DataAccess"></asp:ObjectDataSource>
            <br />
            <asp:Button ID="btnPreviousConfirmList" runat="server" Text="Back" OnClick="btnPreviousConfirmList_Click" />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Next" /></asp:View>
         <asp:View ID="View5" runat="server">
                <div class="headfont">Habits Questions</div>
             <br />
             <table>
                <tr>
                    
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dlistEatingHabitsQ1"
                            ErrorMessage="Please choose at least 0-1." InitialValue="-1" ValidationGroup="HabitsQuestions"></asp:RequiredFieldValidator><br />
                        How many glasses of water did you drink yesterday?</td>
                    <td><asp:DropDownList ID="dlistEatingHabitsQ1" runat="server" >
                        <asp:ListItem Selected="True" Value="-1">Select a value</asp:ListItem>
                            <asp:ListItem Value="0">0-1</asp:ListItem>
                            <asp:ListItem Value="1">2</asp:ListItem>
                            <asp:ListItem Value="2">3</asp:ListItem>
                            <asp:ListItem Value="3">4</asp:ListItem>
                            <asp:ListItem Value="4">5+</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                   
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dlistEatingHabitsQ2"
                            ErrorMessage="Please choose at least 0-1." InitialValue="-1" ValidationGroup="HabitsQuestions"></asp:RequiredFieldValidator><br />
                        How many meals or snacks did you eat yesterday, including beverages and candy?</td>
                    <td><asp:DropDownList ID="dlistEatingHabitsQ2" runat="server">
                            <asp:ListItem Selected="True" Value="-1">Select a value</asp:ListItem>
                            <asp:ListItem Value="0">0-1</asp:ListItem>
                            <asp:ListItem Value="1">2</asp:ListItem>
                            <asp:ListItem Value="2">3</asp:ListItem>
                            <asp:ListItem Value="3">4</asp:ListItem>
                            <asp:ListItem Value="4">5+</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dlistEatingHabitsQ3"
                            ErrorMessage="Please choose at least 0-1." InitialValue="-1" ValidationGroup="HabitsQuestions"></asp:RequiredFieldValidator><br />
                        How many times did you eat when your hunger rating was a 3 or 4?</td>
                    <td><asp:DropDownList ID="dlistEatingHabitsQ3" runat="server">
                            <asp:ListItem Selected="True" Value="-1">Select a value</asp:ListItem>
                            <asp:ListItem Value="0">0-1</asp:ListItem>
                            <asp:ListItem Value="1">2</asp:ListItem>
                            <asp:ListItem Value="2">3</asp:ListItem>
                            <asp:ListItem Value="3">4</asp:ListItem>
                            <asp:ListItem Value="4">5+</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    
                    <td>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="rbtnlistEatingHabitsQ4" Display="Static" ErrorMessage="Please choose Yes or No" InitialValue="" Width="31%" runat="server" ValidationGroup="HabitsQuestions"></asp:RequiredFieldValidator><br />
                        Did you eat breakfast yesterday?</td>
                    <td>
                        &nbsp;<asp:RadioButtonList ID="rbtnlistEatingHabitsQ4" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="4">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    
                    <td >
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rbtnlistEatingHabitsQ5"
                            Display="Static" ErrorMessage="Please choose Yes or No" InitialValue="" ValidationGroup="HabitsQuestions"
                            Width="31%"></asp:RequiredFieldValidator><br />
                        Did you eat while watching television yesterday?</td>
                    <td><asp:RadioButtonList ID="rbtnlistEatingHabitsQ5" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="4">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
             </table>
             <br />
             <br /><asp:Button ID="btnPreviousHabits" runat="server" Text="Back" OnClick="btnPreviousHabits_Click" />
                <asp:Button ID="HabitsNextButton" runat="server" Text="Next" OnClick="HabitsNextButton_Click" ValidationGroup="HabitsQuestions" /></asp:View>
            <asp:View ID="View6" runat="server"><br /><div>
                <asp:Label ID="lblNameSub" runat="server" Text="Name"></asp:Label>,<br />
                <br />
                <asp:Label ID="lblKudo" runat="server" Text="kudo"></asp:Label><br />
                <br />
                <asp:Label ID="lblMin1" runat="server" Text="min1"></asp:Label><br />
                <br />
                <asp:Label ID="lblMin2" runat="server" Text="Min2"></asp:Label><br />
                <br />
                Which of these
                eating areas would you like to work on?<br />
                &nbsp;<asp:RadioButtonList ID="rbtnlistEatingArea" runat="server" DataSourceID="EatingAreaDataSource"
                    DataTextField="nutrient_name" DataValueField="nutrient_id" RepeatDirection="Horizontal" >
                </asp:RadioButtonList><asp:ObjectDataSource ID="EatingAreaDataSource" runat="server"
                    SelectMethod="LoadEatingArea" TypeName="DataAccess"></asp:ObjectDataSource>
                <asp:Label ID="lblErrorEatingAreas" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
                <br /><asp:Button ID="btnPreviousEatingAreas" runat="server" Text="Back" OnClick="btnPreviousEatingAreas_Click" />
                <asp:Button ID="EatingAreaNextButton" runat="server" Text="Next" OnClick="EatingAreaNextButton_Click" /><br /></div>
            </asp:View>
            <asp:View ID="View7" runat="server"><br />
                <div><br />
                <div>Great Choice!</div><br />
                    <asp:Label ID="lblIsMajorGoal" runat="server" Text="Major Label"></asp:Label>
                    is a major goal.<br />
                <br />
                To accomplish this you must set minor goals to help you get there one step at a
                time.<br />
                <br />
                Which minor goal would you like to work on?<br />
                <br />
                <asp:RadioButtonList ID="MinorGoalList" runat="server" DataSourceID="MinorGoalsDataSource" DataTextField="before_text" DataValueField="goal_statement_id" >
                </asp:RadioButtonList><asp:ObjectDataSource ID="MinorGoalsDataSource" runat="server"
                    SelectMethod="LoadMinorGoals" TypeName="DataAccess" ></asp:ObjectDataSource>
                <asp:Label ID="lblErrorMinorGoals" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
                <br /><asp:Button ID="btnPreviousMinorGoals" runat="server" Text="Back" OnClick="btnPreviousMinorGoals_Click" />
                <asp:Button ID="MinorGoalNextButton" runat="server" Text="Next" OnClick="MinorGoalNextButton_Click"/></div></asp:View>
            <asp:View ID="View8" runat="server">
                <br />
            <div  id="Div1">
                <br />
                
<table><tr><td>
<div id="print_area">
<table><tr><td>
    <table>
      <tr>
        <td colspan="2">
        <h2>Eating Goal Contract</h2>
        </td></tr>
                <tr>
        <td class="cinfo3" colspan="2"><br />
          
          I,&nbsp;<strong><asp:Substitution ID="NameSub" runat="server" MethodName="SetNameSub" /></strong>, promise to do my best to reach my
          eating goal
          by <strong>
                <asp:Label ID="lblContractMinorGoal" runat="server" Text="Label"></asp:Label></strong><br /><br />

          
          My major goal is 
          <strong><asp:Label ID="lblContractMajorGoal" runat="server"
                    Text="Label"></asp:Label>
          </strong>.<br /><br />

            
                This is because <strong>
                <asp:Label ID="lblContractWhy" runat="server" Text="Label"></asp:Label></strong>
          <br />&nbsp;</td>
      </tr>
                         <tr>
        <td class="cinfo3" align="center"><span class="bold">__________________</span><br />
          Your Signature</td>
        <td width="50%" class="cinfo3"><span class="bold">__________________</span><br />
          Friend's Signature</td>
      </tr>
      <tr>
        <td colspan="2" class="cinfo3" align="center"><br /><span class="bold">
          __________________</span><br />
          Parent's Signature</td>
      </tr>
      <tr>
        <td colspan="2" class="cinfo3" align="center"><br /><span class="bold">Extreme Challenge 
          ____________________________________</span></td>
      </tr>
    </table>
    </td></tr></table></div>
    </td></tr></table>
    
                <br />
                <br />
                <span class="bold"></span>
                <br />
                <asp:Button ID="btnPrintContract" runat="server" OnClick="btnPrintContract_Click" Text="Print Contract" CommandArgument="print_area" CommandName="getPrint" OnClientClick="getPrint('print_area')" />
                <asp:Button ID="RestartButton" runat="server" OnClick="resetAnalysis"
                    Text="Start Over" /></div></asp:View> 
    </asp:MultiView>
    <br />
    <br />
    <!--<asp:Button ID="btnHelp" runat="server" Text="Help" />-->
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    &nbsp;&nbsp;
    &nbsp;

    </div></div></div>
    <div class="row">
      <div class="col-md-10 col-md-offset-1 footer">
        <p>&copy; 2013 - Regents of the University of California  &mdash;
        If any of these links no longer work, please let us know at <a href="mailto:eatfit@ucdavis.edu">eatfit@ucdavis.edu</a> </p>
      </div><!--/.footer-->
    </div><!--/.row-->

      <AjaxControlToolkit:AutoCompleteExtender
                runat="server" 
                Enabled="false"
                ID="autoComplete1" 
                TargetControlID="FoodSearchBox"
                ServicePath="foodList.asmx"
                ServiceMethod="GetFoodList"
                MinimumPrefixLength="2" 
                CompletionInterval="1000"
                EnableCaching="true"
                CompletionSetCount="12" />  
</asp:Content>

