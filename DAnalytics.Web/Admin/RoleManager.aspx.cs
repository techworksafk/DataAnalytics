using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utilities = DAnalytics.UTIL;
using Model = DAnalytics.MO.Admin;
using BLAdmin = DAnalytics.BL.Admin;

namespace DAnalytics.Web.Admin
{
    /// <summary>
    /// WebForm for managing the Roles
    /// </summary>
    public partial class RoleManager : BasePage.DAnalBase
    {

        #region Private constants

        private const string ENTITY_NAME = "Role";
        private const string REDIRECT_URL = "RoleList.aspx";

        #endregion

        #region Private Variables

        private int roleId = 0;

        #endregion

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
            chkFullRights.Attributes.Add("onclick", "CheckUncheckPermissions()");

            if (!IsPostBack)
            {
                int RoleID = Utilities.DAnalHelper.ConvertToInt32(Request.QueryString["roleid"]);
                hdnRoleId.Value = RoleID.ToString();

                DataSet ds = BLAdmin.UserRole.GetRolePermissions(RoleID);

                gvRoles.DataSource = ds;
                gvRoles.DataBind();

                if (Request.QueryString["roleid"] != null)
                {
                    Model.UserRole oRole = new Model.UserRole
                    {
                        Description = Convert.ToString(ds.Tables[0].Rows[0]["Description"]),
                        RoleName = Convert.ToString(ds.Tables[0].Rows[0]["RoleName"]),
                        HasFullRights = Convert.ToBoolean(ds.Tables[0].Rows[0]["HasFullRights"]),                       
                        IsActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]),
                        IsSystemDefined = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSystemDefined"]),
                        RoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"])                      
                    };
                    GetRoleDetails(oRole);
                }
                else
                {
                    hdnRoleId.Value = "0";
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave button control.
        /// Saves the role details to the database
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            roleId = Convert.ToInt32(hdnRoleId.Value);
            //bool groupExists = false;
            //if (roleId > 0 && chkActive.Checked == false)
            //{
            //    groupExists = BLAdmin.UserRole.CheckForGroups(roleId);
            //}
            //else
            //{
            //    groupExists = false;
            //}
            //if (!groupExists)
            //{
            if (Page.IsValid)
            {
                Model.UserRole oRole = new Model.UserRole();

                var coll = new List<Model.RolePermission>();

                oRole.RoleID = Convert.ToInt32(hdnRoleId.Value);

                //Save Role Details
                oRole.RoleName = txtRoleName.Text.Trim();
                oRole.Description = txtDesc.Text;
                oRole.IsActive = chkActive.Checked;
                oRole.HasFullRights = chkFullRights.Checked;
                for (int iCount = 0; iCount < gvRoles.Rows.Count; iCount++)
                {
                    bool IsScreen = Convert.ToBoolean(gvRoles.DataKeys[iCount].Values["IsScreen"]);
                    int MenuID = Convert.ToInt32(gvRoles.DataKeys[iCount].Values["MenuID"]);

                    Model.RolePermission permission = new Model.RolePermission();
                    permission.RoleID = oRole.RoleID;
                    permission.ScreenID = MenuID;

                    if (IsScreen)
                    {
                        permission.ViewRights = (gvRoles.Rows[iCount].Cells[1].FindControl("chkView") as CheckBox).Checked;
                        permission.AddRights = (gvRoles.Rows[iCount].Cells[2].FindControl("chkAdd") as CheckBox).Checked;
                        permission.UpdateRights = (gvRoles.Rows[iCount].Cells[3].FindControl("chkEdit") as CheckBox).Checked;
                        permission.DeleteRights = (gvRoles.Rows[iCount].Cells[4].FindControl("chkDelete") as CheckBox).Checked;
                        permission.PrintRights = (gvRoles.Rows[iCount].Cells[5].FindControl("chkPrint") as CheckBox).Checked;
                    }
                    else
                    {
                        permission.ViewRights = true;
                        permission.AddRights = false;
                        permission.UpdateRights = false;
                        permission.DeleteRights = false;
                        permission.PrintRights = false;
                    }
                    coll.Add(permission);
                }

                oRole.RolePermissions = coll;

                int updatedRoleId = BLAdmin.UserRole.Save(oRole);
                
                if (updatedRoleId > 0)
                {
                    if (hdnRoleId.Value == "0")
                    {
                        hdnRoleId.Value = updatedRoleId.ToString();
                        msg = " added successfully.";
                        Utilities.ActionLog.LogOperations(this.UserID, ENTITY_NAME, txtRoleName.Text.Trim(), Utilities.DMLOperations.ADDED);
                    }
                    else
                    {
                        msg = " updated successfully.";
                        Utilities.ActionLog.LogOperations(this.UserID, ENTITY_NAME, txtRoleName.Text.Trim(), Utilities.DMLOperations.UPDATED);
                    }

                    btnCancel.Text = "Back";
                }
                else
                {
                    msg = "with the same name already exists.";
                }
                Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", ENTITY_NAME + " " + msg);
            }
            else
            {
                // msg = Utilities.DAnalHelper.GetStringResource(Utilities.ExpoNetConstants.EXPONET_MSG_SELECT).Replace("<Value>", "Atleast one permission");
                msg = "Select at least one permission";
                Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", msg);
            }
            //}
            //else
            //{
            //    msg = Utilities.DAnalHelper.GetStringResource(Utilities.ExpoNetConstants.EXPONET_MSG_ACTIVE_CHILD_EXISTS);
            //    Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", ENTITY_NAME + " " + msg);
            //}
        }

        /// <summary>
        /// Handles the Click event of the btnCancel button control.      
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(REDIRECT_URL);
        }

        /// <summary>
        /// Validates whether at least one permission is checked 
        /// </summary>
        /// <param name= sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void IsPermissionChecked(Object sender, ServerValidateEventArgs args)
        {
            if (chkActive.Checked)
            {
                args.IsValid = false;

                for (int iCount = 0; iCount < gvRoles.Rows.Count; iCount++)
                {
                    bool IsScreen = Convert.ToBoolean(gvRoles.DataKeys[iCount].Values["IsScreen"]);
                    int MenuID = Convert.ToInt32(gvRoles.DataKeys[iCount].Values["MenuID"]);

                    if (IsScreen)
                    {
                        bool ViewRights = (gvRoles.Rows[iCount].Cells[1].FindControl("chkView") as CheckBox).Checked;
                        bool AddRights = (gvRoles.Rows[iCount].Cells[2].FindControl("chkAdd") as CheckBox).Checked;
                        bool UpdateRights = (gvRoles.Rows[iCount].Cells[3].FindControl("chkEdit") as CheckBox).Checked;
                        bool DeleteRights = (gvRoles.Rows[iCount].Cells[4].FindControl("chkDelete") as CheckBox).Checked;
                        bool PrintRights = (gvRoles.Rows[iCount].Cells[5].FindControl("chkPrint") as CheckBox).Checked;

                        if (ViewRights || AddRights || UpdateRights || DeleteRights || PrintRights)
                        {
                            args.IsValid = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                args.IsValid = true;
            }
        }


        /// <summary>
        /// Handles the RowDataBound event of the gvRoles gridview control.        
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.Cells[0].FindControl("lblMenuName") as Label).Text = (e.Row.DataItem as DataRowView)["MenuName"].ToString();

                int iLevel = Convert.ToInt32((e.Row.DataItem as DataRowView)["LEVEL"]);
                bool IsScreen = Convert.ToBoolean((e.Row.DataItem as DataRowView)["IsScreen"]);
                for (int iSpace = 0; iSpace <= iLevel; iSpace++)
                {
                    (e.Row.Cells[0].FindControl("lblMenuName") as Label).Text = (e.Row.Cells[0].FindControl("lblMenuName") as Label).Text;
                }


                if (!IsScreen)
                {
                    (e.Row.Cells[0].FindControl("lblMenuName") as Label).Font.Bold = true;

                    (e.Row.Cells[1].FindControl("chkView") as CheckBox).Visible = false;
                    (e.Row.Cells[2].FindControl("chkAdd") as CheckBox).Visible = false;
                    (e.Row.Cells[3].FindControl("chkEdit") as CheckBox).Visible = false;
                    (e.Row.Cells[4].FindControl("chkDelete") as CheckBox).Visible = false;
                    (e.Row.Cells[5].FindControl("chkPrint") as CheckBox).Visible = false;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the details of the given role id from database
        /// </summary>
        /// <param name="roleId">Id of the role of which details are to be fetched</param>
        private void GetRoleDetails(Model.UserRole oRole)
        {
            if (oRole != null)
            {
                txtRoleName.Text = oRole.RoleName;
                txtDesc.Text = oRole.Description;
                chkActive.Checked = oRole.IsActive;

                if (oRole.IsSystemDefined)
                {
                    chkActive.Enabled = false;
                }
                if (oRole.HasFullRights)
                {
                    chkFullRights.Checked = true;
                }             
            }
        }

        #endregion
    }
}