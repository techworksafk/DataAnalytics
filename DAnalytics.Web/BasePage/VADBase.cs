using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTIL = DAnalytics.UTIL;
namespace BasePage
{
    /// <summary>
    /// Access permissions for a logged in user
    /// </summary>
    struct AccessPermissions
    {
        public const string ViewAccess = "V";
        public const string AddAccess = "A";
        public const string UpdateAccess = "U";
        public const string DeleteAccess = "D";
        public const string PrintAccess = "P";
    }

    /// <summary>
    /// All other pages inheritting this page
    /// </summary>
    public class DAnalBase:System.Web.UI.Page
    {       
        public string CurrentPage
        {
            get
            {
                string uri = string.Empty;
                uri = Request.Url.AbsoluteUri; 
                if (uri.IndexOf("?") > 0) { uri = uri.Substring(0, uri.LastIndexOf("?")); }               
                uri = uri.Substring((uri.LastIndexOf("/") + 1), uri.Length - (uri.LastIndexOf("/") + 1));                
                return uri;
            }
        }
       
        public bool IsAdmin
        {
            get
            {
                bool _isAdmin = false;
                if (this.UserID != 0) { _isAdmin =DAnalytics.BL.Admin.DAnalyticsUser.IsAdmin(this.UserID); }
                return _isAdmin;
            }
        }

        public DAnalytics.MO.Admin.DAnalyticsUser CurrentUser
        {
            get
            {
                if (Session[UTIL.DAnalyticsKeys.USER] != null)
                {
                    return (DAnalytics.MO.Admin.DAnalyticsUser)Session[UTIL.DAnalyticsKeys.USER];
                }
                else { return null; }
            }
        }

        public Int32 UserID
        {
            get
            {
                if (Session[UTIL.DAnalyticsKeys.USER] != null)
                {
                    DAnalytics.MO.Admin.DAnalyticsUser _user = (DAnalytics.MO.Admin.DAnalyticsUser)Session[UTIL.DAnalyticsKeys.USER];
                    return _user.UserID;
                }
                else { return 0; }
            }
        }

        public bool IsViewAllowed
        {
            get { return (Convert.ToString(ViewState[UTIL.DAnalyticsKeys.RIGHTS]).Contains(AccessPermissions.ViewAccess) ? true : false); }
        }

        public bool IsAddAllowed
        {
            get { return (Convert.ToString(ViewState[UTIL.DAnalyticsKeys.RIGHTS]).Contains(AccessPermissions.AddAccess) ? true : false); }
        }

        public bool IsUpdateAllowed
        {
            get { return (Convert.ToString(ViewState[UTIL.DAnalyticsKeys.RIGHTS]).Contains(AccessPermissions.UpdateAccess) ? true : false); }
        }

        public bool IsDeleteAllowed
        {
            get { return (Convert.ToString(ViewState[UTIL.DAnalyticsKeys.RIGHTS]).Contains(AccessPermissions.DeleteAccess) ? true : false); }
        }

        public bool IsPrintAllowed
        {
            get { return (Convert.ToString(ViewState[UTIL.DAnalyticsKeys.RIGHTS]).Contains(AccessPermissions.PrintAccess) ? true : false); }
        }

        private Int32 _menuId = 0;
        public Int32 MenuID
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        public DAnalBase()
        {
            this.ErrorPage = "~\\Error\\ErrorPage.aspx";
            this.Load += new EventHandler(DAnalBase_Load);
        }

        /// <summary>
        /// Handles the Init event of base page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the base page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        void DAnalBase_Load(object sender, EventArgs e)
        {
            if (this.UserID == 0) { string loginPage = ResolveUrl("~/SignIn.aspx"); Response.Redirect(loginPage, true); }
            UTIL.DAnalHelper.NoCacheHeaders();
        }

        /// <summary>
        /// Handles the OnError of base page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected override void OnError(EventArgs e)
        {
            string ErrorID = string.Empty;
            if (Server.GetLastError() != null)
            {
                string errorMsg = Server.GetLastError().Message;
                UTIL.DAnalHelper.LogError(errorMsg, CurrentPage);
                Session[UTIL.DAnalyticsKeys.EXCEPTION] = errorMsg;
            }
            Response.Redirect(ErrorPage);
        }

        /// <summary>
        /// Handles the Pre-render event of base page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //SetAccessRoles();
        }

        /// <summary>
        /// Method to set access roles.
        /// </summary>
        public void SetAccessRoles()
        {
            ViewState["UserRights"] = DAnalytics.BL.Admin.Common.AccessRights(this.UserID, this.MenuID);

            GetControlsInPage(this);

            //if (!IsDeleteAllowed) { RemoveDeleteColumnInGridView(this); }
        }

        /// <summary>
        /// Method to get controls in a page.
        /// </summary>
        /// <param name="ctrl"></param>
        private void GetControlsInPage(System.Web.UI.Control ctrl)
        {
            string commandName = string.Empty;
            foreach (System.Web.UI.Control objControl in ctrl.Controls)
            {
                if (objControl is System.Web.UI.WebControls.Button ||
                    objControl is System.Web.UI.WebControls.ImageButton ||
                    objControl is System.Web.UI.WebControls.LinkButton)
                {
                    commandName = objControl.GetType().GetProperty("CommandName").GetValue(objControl,
                        null).ToString();
                    if (commandName != string.Empty)
                    {
                        if (!IsAddAllowed)
                        {
                            if (commandName.ToUpper().Trim().Contains("NEW"))
                            {
                                objControl.GetType().GetProperty("Enabled").SetValue(objControl, false, null);
                            }
                            if (commandName.ToUpper().Trim().Contains("SAVE"))
                            {
                                objControl.GetType().GetProperty("Enabled").SetValue(objControl, false, null);
                            }
                        }
                        if (!IsUpdateAllowed)
                        {
                            if (commandName.ToUpper().Trim().Contains("EDIT"))
                            {
                                if (objControl is System.Web.UI.WebControls.LinkButton)
                                {
                                    //((System.Web.UI.WebControls.LinkButton)objControl).CssClass = "InactiveLink";
                                    ((System.Web.UI.WebControls.LinkButton)objControl).Attributes.Add("onclick",
                                        "return false;");
                                }
                                else
                                {
                                    objControl.GetType().GetProperty("Enabled").SetValue(objControl, false, null);
                                }
                            }
                        }
                        if (!IsDeleteAllowed)
                        {
                            if (commandName.ToUpper().Trim().Contains("DELETE"))
                            {
                                objControl.GetType().GetProperty("Enabled").SetValue(objControl, false, null);
                            }
                        }
                        if (!IsPrintAllowed)
                        {
                            if (commandName.ToUpper().Trim().Contains("PRINT"))
                            {
                                objControl.GetType().GetProperty("Enabled").SetValue(objControl, false, null);
                            }
                        }
                        if (!IsViewAllowed)
                        {
                            if (commandName.ToUpper().Trim().Contains("VIEW"))
                            {
                                objControl.GetType().GetProperty("Enabled").SetValue(objControl, false, null);
                            }
                        }
                    }
                }
                if (objControl.HasControls())
                {
                    GetControlsInPage(objControl);
                }
            }
        }

        private void RemoveDeleteColumnInGridView(System.Web.UI.Control ctrl)
        {
            foreach (System.Web.UI.Control objControl in ctrl.Controls)
            {
                if (objControl is System.Web.UI.WebControls.GridView && HasDelete((System.Web.UI.WebControls.GridView)objControl))
                {
                    ((System.Web.UI.WebControls.GridView)objControl).Columns[((System.Web.UI.WebControls.GridView)objControl).Columns.Count - 1].Visible = false;
                }
                if (objControl.HasControls())
                {
                    RemoveDeleteColumnInGridView(objControl);
                }
            }
        }

        /// <summary>
        /// Method to check a delete button exists in the gridview control.
        /// </summary>
        /// <param name="gv"></param>
        /// <returns></returns>
        private bool HasDelete(System.Web.UI.WebControls.GridView gv)
        {
            bool isDeleteExists = false;
            for (int cnt = 0; cnt < gv.Columns.Count; cnt++)
            {
                if (gv.Columns[cnt].HeaderText.Trim().ToUpper().Contains("DELETE"))
                {
                    isDeleteExists = true;
                    break;
                }
            }
            return isDeleteExists;
        }
    }
}