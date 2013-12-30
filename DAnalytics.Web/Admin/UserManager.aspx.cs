using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utilities = DAnalytics.UTIL;
using Model = DAnalytics.MO.Admin;
using BLAdmin = DAnalytics.BL.Admin;
using System.IO;

namespace DAnalytics.Web.Admin
{
    /// <summary>
    /// WebForm for managing Users 
    /// </summary>
    public partial class UserManager : BasePage.DAnalBase
    {

        private const string MSG_REQ = "<Value> is required.";
        protected string password = " "; 

        private int userId = 0;
        protected int QUserID
        {
            get
            {
                if (Request.QueryString["UserID"] != null) { userId = Utilities.DAnalHelper.ConvertToInt32(Request.QueryString["UserID"]); }
                return userId;
            }
            set
            {
                userId = value;
            }
        }       

        #region Page Events

        /// <summary>
        /// Handles the OnInit event of the Page control.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MenuID = 3;
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
            ValidationMessages();
            if (!IsPostBack)
            {
                GetAllRoles();
                if (this.QUserID != 0) { PopulateForm(); }
                else
                {
                    lnkChangePwd.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave button control.       
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Model.DAnalyticsUser oUser = new Model.DAnalyticsUser();

                oUser.UserID = hdnUserID.Value.Trim().Equals(string.Empty) ? 0 :  Utilities.DAnalHelper.ConvertToInt32(hdnUserID.Value.Trim());
                oUser.UserName = txtUserName.Text.Trim();
                oUser.UserPassword = txtPassword.Text.Trim() == string.Empty ? hdnPWD.Value : Utilities.CryptorEngine.Encrypt(txtPassword.Text.Trim(), true);
                oUser.FirstName = txtFirstName.Text.Trim();
                oUser.LastName = txtLastName.Text.Trim();            
                oUser.Email = txtEmail.Text.Trim();
                oUser.Mobile = txtMobile.Text.Trim();              
                oUser.IsActive = chkActive.Checked;


                oUser.UserRoleMappings = new List<Model.UserRoleMapping>();

                Model.UserRoleMapping role;
                for (int i = 0; i < chklRoles.Items.Count; i++)
                {
                    if (chklRoles.Items[i].Selected)
                    {
                        role = new Model.UserRoleMapping();
                        role.UserID = Utilities.DAnalHelper.ConvertToInt32(hdnUserID.Value);
                        role.RoleID = Convert.ToInt32(chklRoles.Items[i].Value);
                        oUser.UserRoleMappings.Add(role);
                    }
                }
                int updatedUserId = BLAdmin.DAnalyticsUser.Save(oUser);
                if (updatedUserId == 0) { Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", "Username already exists."); }
                else if (updatedUserId == -1) { Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", "Pilot number already exists."); }
                else
                {
                    hdnUserID.Value = updatedUserId.ToString();
                    this.QUserID = updatedUserId;  
                    txtUserName.Enabled = false;

                    divPassword.Visible = false;
                    lnkChangePwd.Visible = true;
                   
                    Utilities.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", "User details saved successfully.");
                    Utilities.ActionLog.LogOperations(this.UserID, "User", txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim(), Utilities.DMLOperations.SAVED);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel button control.      
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./UserList.aspx");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the validations message for each validation control
        /// </summary>
        private void ValidationMessages()
        {
            reqEmail.ErrorMessage = MSG_REQ.Replace("<Value>", "Email");
            reqFirstName.ErrorMessage = MSG_REQ.Replace("<Value>", "First Name");
            reqLastName.ErrorMessage = MSG_REQ.Replace("<Value>", "Last Name");       
            reqPassword.ErrorMessage = MSG_REQ.Replace("<Value>", "Password");
            reqRetypePassword.ErrorMessage = "ReType Password is required";
            reqUserName.ErrorMessage = MSG_REQ.Replace("<Value>", "UserName");
            cmpPasswords.ErrorMessage = "Password mismatch";
            regxUserName.ErrorMessage = "Invalid User name";
            regxEmail.ErrorMessage = "Invalid Email";
        }

     
        /// <summary>
        /// Populates roles to chklRoles checkboxlist control
        /// </summary>
        private void GetAllRoles()
        {

            Model.DAnalyticsUserRoles coll = new Model.DAnalyticsUserRoles();
            coll = BLAdmin.UserRole.GetRoles();
            chklRoles.DataSource = coll;
            chklRoles.DataTextField = "RoleName";
            chklRoles.DataValueField = "RoleID";
            chklRoles.DataBind();
        }

        /// <summary>
        /// In edit mode, gets the data associated with the user from database and populates respective controls
        /// </summary>
        private void PopulateForm()
        {
            Model.DAnalyticsUser oUser = BLAdmin.DAnalyticsUser.GetUserByID(this.QUserID);

            hdnUserID.Value = oUser.UserID.ToString();
            txtUserName.Text = oUser.UserName;
            hdnPWD.Value = oUser.UserPassword;
            txtFirstName.Text = oUser.FirstName;
            txtLastName.Text = oUser.LastName;           
            txtEmail.Text = oUser.Email;
            txtMobile.Text = oUser.Mobile;           
            chkActive.Checked = Convert.ToBoolean(oUser.IsActive);         
            divPassword.Visible = false;
            lnkChangePwd.Visible = true;        
            password = Utilities.CryptorEngine.Decrypt(oUser.UserPassword, true);
            txtNewPwd.Text = Utilities.CryptorEngine.Decrypt(oUser.UserPassword, true);
            txtRetypeNewPwd.Text = Utilities.CryptorEngine.Decrypt(oUser.UserPassword, true);         
            txtUserName.Enabled = false;
            chkActive.Checked = Convert.ToBoolean(oUser.IsActive);

            if (oUser.UserRoleMappings.Count > 0)
            {
                foreach (Model.UserRoleMapping role in oUser.UserRoleMappings)
                {
                    for (int i = 0; i < chklRoles.Items.Count; i++)
                    {
                        if (Convert.ToInt32(chklRoles.Items[i].Value) == role.RoleID)
                        {
                            chklRoles.Items[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Public method for changing password to be called from clientside ajax
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="newPwd">New password</param>
        /// <returns>Returns string indicating whether password change was successful or not</returns>
        [System.Web.Services.WebMethod]
        public static string ChangePassword(string userId, string newPwd)
        {
            if (BL.Admin.DAnalyticsUser.ChangePassword(UTIL.DAnalHelper.ConvertToInt32(userId), newPwd)) { return "1"; }
            else { return "0"; }
        }
        #endregion
    }
}