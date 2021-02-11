<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="PasswordReset, App_Web_1ej4uip3" title="Password Reset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SuccessPageUrl="~/LoginPage.aspx">
        <MailDefinition From="eatfit@ucdavis.edu" Subject="Password Reset">
        </MailDefinition>
    </asp:PasswordRecovery>
</asp:Content>

