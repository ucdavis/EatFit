<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PasswordReset.aspx.cs" Inherits="PasswordReset" Title="Password Reset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SuccessPageUrl="~/LoginPage.aspx">
        <MailDefinition From="eatfit@ucdavis.edu" Subject="Password Reset">
        </MailDefinition>
    </asp:PasswordRecovery>
</asp:Content>

