<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EatingReport2.aspx.cs" Inherits="EatingReport2" Title="EatFit - Eating Report" Debug="true"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    <asp:Panel id="authorized" visible="false" runat="server">
        Choose Report: <asp:DropDownList ID="ddlReportToView" runat="server" OnSelectedIndexChanged="ddlReportToView_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="Eating Sessions by User">Eating Sessions by User</asp:ListItem>
            <asp:ListItem>Soft Drink Consumption by Session</asp:ListItem>
            <asp:ListItem>Top 20 Most Reported Foods</asp:ListItem>
            <asp:ListItem Value="Major Goals">Major and Minor Goal Codes</asp:ListItem>
        </asp:DropDownList><br /><br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        Height="410px" ProcessingMode="Remote" Width="900px">
<ServerReport></ServerReport>
</rsweb:ReportViewer>
    </asp:Panel>
    <asp:Panel id="not_authorized" visible="true" runat="server">
    <p>
    You are not authorized to view this report.<br /><br />
    In order to view reports you must be logged in as an auditor or as a user with report viewing priviledges.
    <br /><br />
    You may either login or logout and log back in as an auditor to view this report by selecting the <asp:LinkButton ID="lbLoginPage" runat="server" PostBackUrl="~/LoginPage.aspx">Login</asp:LinkButton> page.
        </p>
    </asp:Panel>
    <br />
</asp:Content>

