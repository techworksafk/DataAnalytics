 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities = DAnalytics.UTIL;
using Model = DAnalytics.MO.Admin;
using BLAdmin = DAnalytics.BL.Admin;

namespace DAnalytics.Web.Admin
{
    /// <summary>
    /// WebForm for listing the Roles
    /// </summary>
    public partial class Roles : BasePage.DAnalBase
    {

        #region Page Events 

        /// <summary>
        /// Handles the OnInit event of the Page control.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MenuID = 2;
        }

        /// <summary>
        /// Handles the OnPreRender event of the Page control.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.SetAccessRoles();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnNew.UniqueID;
            if (!IsPostBack)
            {
                GetAllRoles();
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gvRoles gridview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int roleId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.ToLower().Equals("editrole"))
            {
                Response.Redirect("./RoleManager.aspx?roleid=" + roleId.ToString());
            }
            else if (e.CommandName.ToLower().Equals("deleterole"))
            {
                string msg = string.Empty;
                bool userExists = BLAdmin.UserRole.CheckForUsers(roleId);
                if (!userExists)
                {
                    //get role name from row
                    LinkButton lnkRoleName = (LinkButton)((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).FindControl("lnkEdit");
                    string roleName = lnkRoleName.Text;

                    BLAdmin.UserRole.Delete(roleId);
                    GetAllRoles();
                    msg = "Role deleted successfully";
                    Utilities.ActionLog.LogOperations(this.UserID, "Role", roleName, Utilities.DMLOperations.DELETED);
                }
                else
                {
                    msg = "This role has associated users and cannot be deleted.";
                }
                Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", msg);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("./RoleManager.aspx");
        }

        /// <summary>
        ///  Handles the RowDataBound event of the gvRoles gridview control.
        ///  Hides/Shows the delete button based on whether the role is system-defined/user-defined
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibDelete = (ImageButton)e.Row.FindControl("ibDelete");
                HiddenField hdnSystemDefined = (HiddenField)e.Row.FindControl("hdnSystemDefined");
                if (Convert.ToBoolean(hdnSystemDefined.Value))
                {
                    ibDelete.Visible = false;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all roles from the database and binds to the gvRoles gridview control
        /// </summary>
        private void GetAllRoles()
        {
            gvRoles.DataSource = BLAdmin.UserRole.GetRoles();
            gvRoles.DataBind();
        }

        #endregion
    }
}