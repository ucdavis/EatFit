using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CreateRole : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Display roles when page is first loaded:
        if (!IsPostBack)
        {
            // Load the roles into a list and then sort it.
            System.Collections.Generic.List<string> roleList = new System.Collections.Generic.List<string>(Roles.GetAllRoles());
            roleList.Sort();

            lboxRoles.DataSource = roleList;
            lboxRoles.DataBind();

        }
    }

    protected void butCreateRole_Click(object sender, EventArgs e)
    {
        string roleName = tbTxtRole.Text;
        if (!Roles.RoleExists(roleName))
        {
            Roles.CreateRole(tbTxtRole.Text);
            lboxRoles.Items.Add(tbTxtRole.Text);
            errorMessage.Visible = false;
        }
        else
        {
            errorMessage.Visible = true;
        }
    }
    protected void lboxRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lboxUsers.Items.Clear();
        if (lboxRoles.SelectedItem != null)
        {
            System.Collections.Generic.List<string> userList = new System.Collections.Generic.List<string>(Roles.GetUsersInRole(lboxRoles.SelectedItem.Text));
            userList.Sort();
            lboxUsers.DataSource = userList;
            lboxUsers.DataBind();

        }
    }
    protected void butDeleteRole_Click(object sender, EventArgs e)
    {
        // Will also need to add logic for removing the deleted role from
        // all the users who had it!
        ListItem roleItem = lboxRoles.SelectedItem;
        Roles.DeleteRole(roleItem.Text);
        lboxRoles.Items.Remove(roleItem);
    }
}
