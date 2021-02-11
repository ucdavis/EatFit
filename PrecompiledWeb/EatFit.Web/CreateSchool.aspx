<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="CreateSchool, App_Web_1ej4uip3" title="School Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="EatFitTest" Runat="Server">
    &nbsp;
    <table id="tabSchool">
    <tr><th align="left" style="width: 318px"><asp:Literal ID="litSchools" runat="server" Text="Schools:"></asp:Literal></th>
    <th align="left"><asp:Literal ID="litUsersInSchool" runat="server" Text="Users In School:"></asp:Literal></th></tr>
    <tr><td style="width: 318px"><asp:ListBox ID="lboxSchools" runat="server" OnSelectedIndexChanged="lboxSchools_SelectedIndexChanged" AutoPostBack="True" DataTextField="NameCityState" DataValueField="SchoolId">
    </asp:ListBox>
        <asp:Button ID="butDeleteSchool" runat="server" OnClick="butDeleteSchool_Click" Text="Delete School" ValidationGroup="DeleteSchoolValidation" /></td>
    <td><asp:ListBox ID="lboxUsers" runat="server" DataTextField="UserName" DataValueField="UserId"></asp:ListBox></td></tr>
    <tr><td colspan="2" style="color: red">
        &nbsp;<asp:RequiredFieldValidator ID="DeleteSchoolValidator" runat="server" ControlToValidate="lboxSchools"
            ErrorMessage="You must select a school!" SetFocusOnError="True" ValidationGroup="DeleteRoleValidation"></asp:RequiredFieldValidator></td></tr>
    <tr><th align="left" style="width: 318px"><asp:Literal ID="litSchool" runat="server" Text="School:"></asp:Literal></th>
    </tr>
    <tr>
     <td align="left" colspan="2" style="color: red"><asp:Literal ID="errorMessage" runat="server" EnableViewState="False" Text="School already exists!" Visible="False"></asp:Literal></td>
     </tr>
    <tr><td style="width: 318px"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbTxtSchool"
            ErrorMessage="You must enter a school name!" SetFocusOnError="True" ValidationGroup="CreateSchoolValidation"></asp:RequiredFieldValidator><br />
            School Name:<asp:TextBox ID="tbTxtSchool" runat="server" Width="174px"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="CreateSchoolValidator2" runat="server" ControlToValidate="tbSchoolCity"
            ErrorMessage="You must enter a city!" SetFocusOnError="True" ValidationGroup="CreateSchoolValidation"></asp:RequiredFieldValidator> <asp:RequiredFieldValidator ID="CreateSchoolValidator3" runat="server" ControlToValidate="tbSchoolState"
            ErrorMessage="You must enter a state!" SetFocusOnError="True" ValidationGroup="CreateSchoolValidation"></asp:RequiredFieldValidator><br />
        City:<asp:TextBox ID="tbSchoolCity" runat="server"></asp:TextBox>
        State:<asp:TextBox ID="tbSchoolState" runat="server" MaxLength="2" Width="31px"></asp:TextBox></td>
    <td><asp:Button ID="butCreateSchool" runat="server" Text="Create School" OnClick="butCreateSchool_Click" ValidationGroup="CreateSchoolValidation" /></td>
    </tr>
    </table>   
</asp:Content>

