<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="CreateRole, App_Web_ozkuslr0" title="Role Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    &nbsp;
    <table id="tabRole">
    <tr><th align="left"><asp:Literal ID="litRoles" runat="server" Text="Roles:"></asp:Literal></th>
    <th align="left"><asp:Literal ID="litUsersInRole" runat="server" Text="Users In Role:"></asp:Literal></th></TR>
    <TR><TD><asp:ListBox ID="lboxRoles" runat="server" OnSelectedIndexChanged="lboxRoles_SelectedIndexChanged" AutoPostBack="True">
    </asp:ListBox>
        <asp:Button ID="butDeleteRole" runat="server" OnClick="butDeleteRole_Click" Text="Delete Role" ValidationGroup="DeleteRoleValidation" /></TD>
    <td><asp:ListBox ID="lboxUsers" runat="server"></asp:ListBox></td></TR>
    <tr><td colspan="2" style="color: red">
        &nbsp;<asp:RequiredFieldValidator ID="DeleteRoleValidator" runat="server" ControlToValidate="lboxRoles"
            ErrorMessage="You must select a role!" SetFocusOnError="True" ValidationGroup="DeleteRoleValidation"></asp:RequiredFieldValidator></td></tr>
    <TR><th align="left"><asp:Literal ID="litRole" runat="server" Text="Role:"></asp:Literal>
        <asp:RequiredFieldValidator ID="CreateRoleValidator" runat="server" ControlToValidate="tbTxtRole"
            ErrorMessage="You must enter a role!" SetFocusOnError="True" ValidationGroup="CreateRoleValidation"></asp:RequiredFieldValidator></th>
    </TR>
    <tr>
     <td align="left" colspan="2" style="color: red"><asp:Literal ID="errorMessage" runat="server" EnableViewState="False" Text="Role already exists!" Visible="False"></asp:Literal></td>
     </TR>
    <TR><td><asp:TextBox ID="tbTxtRole" runat="server"></asp:TextBox></td>
    <td><asp:Button ID="butCreateRole" runat="server" Text="Create Role" OnClick="butCreateRole_Click" ValidationGroup="CreateRoleValidation" /></td>
    </TR>
    </table>   
</asp:Content>

