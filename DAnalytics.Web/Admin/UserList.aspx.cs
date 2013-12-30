using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utilities = DAnalytics.UTIL;
using Model = DAnalytics.MO.Admin;
using BLAdmin = DAnalytics.BL.Admin;
using MO = DAnalytics.MO;

namespace DAnalytics.Web.Admin
{
    /// <summary>
    /// WebForm for listing Users
    /// </summary>
    public partial class UserList : BasePage.DAnalBase
    {

        public int PageNum
        {
            get
            {
                return Convert.ToInt32(ViewState["PageNum"]);
            }
        }

        public string SortExp
        {
            get
            {
                return Convert.ToString(ViewState["SortExpression"]);
            }
        }

        public string SortDir
        {
            get
            {
                return Convert.ToString(ViewState["SortDirection"]);
            }
        }

        private int totalRows = 0;
        public int TotalRows
        {
            get
            {
                return Convert.ToInt32(ViewState["TotalRecords"]);
            }
            set
            {
                totalRows = value;
            }
        }

        public int PageSize { get { return gvUsers.PageSize; } }
        private int totalPages = 0;
        public int TotalPages
        {
            get
            {
                return Convert.ToInt32(ViewState["TotalPages"]);
            }
            set
            {
                totalPages = value;
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
            if (!IsPostBack)
            {
                FillDropdowns();

                ViewState["SortDirection"] = "Ascending";
                ViewState["PageNum"] = 1;
                ViewState["SortExpression"] = "FirstName";
                GridPager1.PageNum = this.PageNum;
                GetUsers();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSearch button control.       
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["SortDirection"] = "Ascending";
            ViewState["PageNum"] = 1;
            ViewState["SortExpression"] = "FirstName";
            GridPager1.PageNum = this.PageNum;
            GetUsers();
        }

        /// <summary>
        /// Handles the Click event of the btnNewUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("./UserManager.aspx");
        }

        /// <summary>
        /// Handles the RowCommand event of the gvUsers gridview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Utilities.DAnalHelper.ConvertToInt32(e.CommandArgument);
            if (e.CommandName.ToLower().Equals("editusers"))
            {
                Response.Redirect("./UserManager.aspx?UserID=" + userId);
            }
        }

        /// <summary>
        /// Handles the OnSorting event of the gvUsers gridview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvUsers_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string lastDir = ViewState["SortDirection"] as string;
            if (e.SortDirection.ToString().Equals("Ascending"))
                ViewState["SortDirection"] = "Ascending";
            if (lastDir != null && lastDir.Equals("Ascending"))
                ViewState["SortDirection"] = "Descending";
            ViewState["SortExpression"] = e.SortExpression;
            GetUsers();
        }

        /// <summary>
        /// Handles the OnGridPagerChanged event of the GridPager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Changed(object sender, DAnalytics.UserControls.GridPagerChangedEventArgs e)
        {
            ViewState["PageNum"] = e.CurrentPage.ToString();
            GetUsers();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the filter dropdowns
        /// </summary>
        private void FillDropdowns()
        {
            MO.Admin.DAnalyticsUserRoles roles = BLAdmin.UserRole.GetRoles();
            //Fill dept ddl
            if (roles != null && roles.Count > 0)
            {
                Utilities.DAnalHelper.BindData<MO.Admin.DAnalyticsUserRoles>(ddlRoles, roles, "RoleName", "RoleID", "Select", "0");
            }
        }

        /// <summary>
        /// Gets all the users from the database based on the filter criteria
        /// and binds to gvUsers control
        /// </summary>
        private void GetUsers()
        {
            Model.DAnalyticsUsers users = BL.Admin.DAnalyticsUser.GetUsers(this.PageNum, this.PageSize, this.SortExp, this.SortDir, txtName.Text.Trim(), Utilities.DAnalHelper.ConvertToInt32(ddlRoles.SelectedValue), Utilities.DAnalHelper.ConvertToInt32(ddlActive.SelectedValue), out totalPages);
            //if (users != null && users.Count > 0)
            //{
            //    totalRows = Convert.ToInt32();
            //}
            if (users != null && users.Count > 0)
            {

                ViewState["TotalPages"] = totalPages.ToString();
                gvUsers.DataSource = users;
                gvUsers.DataBind();
                GridPager1.TotalPages = this.TotalPages;
                GridPager1.Bind();
            }
            else
            {
                GridPager1.Visible = false;
                gvUsers.DataSource = null;
                gvUsers.DataBind();

            }
        }

        #endregion

      


    }
}