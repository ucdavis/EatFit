<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="help2.aspx.cs" Inherits="help2" Title="EatFit - Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    <asp:Panel ID="pnlHelp" runat="server" Style="width:100%;">
        <div>   
        <Ajax:UpdatePanel runat="server" ID="updtpanelHelp" RenderMode="Inline">
        <ContentTemplate>
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" CellPadding="20" CellSpacing="20"
            Height="549px" FinishDestinationPageUrl="javascript:window.open('','_parent','');window.opener = self; window.close();">
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="Welcome">
                    <strong>Welcome to Eating Analysis </strong>
                    <br />
                    <br />
                    Why are you doing this? To find out how your eating choices are going. When you
                    eat, you have two choices: 1) to shove anything tasty in your mouth, or 2) to put
                    something tasty and fit in your mouth. To get from step 1 to step 2, you need to
                    find out what foods will help you to get fit. So by giving me all the foods you
                    choose to eat on a day, I can look it over, and see what you could change in the
                    future. Nothing major- an orange juice here, a piece of jerky there, and presto!
                    You're at step 2. Didn't even hurt, did it?
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="How It Works">
                    <strong>How it Works</strong>
                    <br />
                    <br />
                    First you supply some basic information: name, age, and gender, and select what
                    is most important to you. Your choices are<br />
                    <br />
                    <ul>
                        <li>Gain strength</li>
                        <li>Lose weight&nbsp;</li>
                        <li>Get fit&nbsp;</li>
                        <li>Improve appearance&nbsp;</li>
                        <li>Increase energy&nbsp;</li>
                        <li>Stabilize mood&nbsp;</li>
                        <li>Improve school performance&nbsp;</li>
                        <li>Improve athletic performance </li>
                    </ul>
                    Next you enter all the foods you ate for a day (one at a time, of course), then
                    I look at their nutrient values and tell you where you did the best and where you
                    might want to strive to improve. You will have two choices for the area of improvement
                    which we call goals. Once you select the area for improvement, you will have some
                    choices of for a minor goal. The minor goal you select becomes your Eating Contract.
                    You can print the eating contract and have it witnessed by a friend or parent.
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep3" runat="server" Title="Entering Foods">
                    <strong>Entering the Foods You Ate</strong>
                    <br />
                    <br />
                    There are 4 easy steps to enter a food. You just repeat them until you have entered
                    all the food you ate for the day. Your day may start anytime and run for 24 hours
                    to that time the next day.
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep4" runat="server" Title="Eating Habits">
                    <strong>Eating Habits</strong>
                    <br />
                    <br />
                    There are 3 multiple choice and 2 true/false questions about your eating habits.
                    Answer each one and click on the Click Here! button to continue. Then you will see
                    your Eating Analysis.
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep5" runat="server" Title="Eating Analysis">
                    <strong>Eating Analysis</strong>
                    <br />
                    <br />
                    Your Eating Analysis is based on the foods you ate and your answers to the Eating
                    Habits questions. The first part describes the area where you did best. The two
                    areas where you most need to strive for improvement are identified. You will have
                    two choices for the area of improvement which we call goals. Once you select the
                    area you want to concentrate on for the next several weeks and click Next&gt;&gt;.
                    You will have some choices of for a minor goal. The minor goal you select becomes
                    your Eating Contract. You can print the eating contract and have it witnessed by
                    a friend or parent.
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep6" runat="server" Title="Printing Your Contract">
                    <strong>How to Print the Eating Contract</strong><br />
                    <br />
                    Your Eating Contract is available for printing.  Select the Print Contract button
                    to preview your Eating Contract.  Select the Print button to display the printer
                    dialog box.  Select the printer you wish to have your Eating Contract printed on
                    and then select Print to have the Eating Contract sent to your printer.  Select
                    the Close button once you have printed your Eating Contract to return to the
                    previous page.
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
        </ContentTemplate>
        </Ajax:UpdatePanel>
    
    
        </div>
    </asp:Panel>
</asp:Content>

