using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Utilities = DAnalytics.UTIL;
using BL = DAnalytics.BL;
using Model = DAnalytics.MO;
using System.Globalization;

namespace DAnalytics.Web.Admin
{
    /// <summary>
    /// WebForm for Audit Trail Report
    /// </summary>
    public partial class AuditTrail : BasePage.DAnalBase
    {
        #region Variables and Properties
        
        protected string fromErrMsg = "From Date is required";

        protected string toErrMsg =  "To date is required";

        protected static string DATE_FORMAT = "dd/MM/yyyy";

        public string CurrentDateTime = System.DateTime.Now.ToString(DATE_FORMAT);//default value for date controls 

        public string PrvDateTime = System.DateTime.Now.AddDays(-1).ToString(DATE_FORMAT);//default value for date controls 
        
        public int PageNum
        {
            get { return Convert.ToInt32(ViewState["PageNum"]); }
            set { ViewState["PageNum"] = value; }
        }        

        private Int32 totalRows = 0;
        public Int32 TotalRows
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

        public int PageSize { get { return gvAudit.PageSize; } }

        public int TotalPages
        {
            get
            {
                int total = 0;
                if (this.TotalRows < gvAudit.PageSize)
                {
                    total = 1;
                }
                else
                {
                    total = (this.TotalRows / gvAudit.PageSize);
                    if ((this.TotalRows % gvAudit.PageSize) > 0)
                    {
                        total = total + 1;
                    }
                }
                return total;
            }
        }
        #endregion

        #region Events       

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFrom.Attributes.Add("readonly", "readonly");
            txtTo.Attributes.Add("readonly", "readonly");
          
            if (!Page.IsPostBack)
            {              
                ViewState["PageNum"] = 1;              
                txtTo.Text = CurrentDateTime;
                txtFrom.Text = PrvDateTime;             
                this.PageNum = 1;
                GridPager1.PageNum = this.PageNum;
                BindDetails();
                Utilities.DAnalHelper.BindData<DataSet>(ddlUsers, BL.Admin.DAnalyticsUser.GetUserNames(), "UserName", "UserID", "ALL", "0");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSearch button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {          
            this.PageNum = 1;
            GridPager1.PageNum = this.PageNum;         
            if (txtFrom.Text != string.Empty && txtTo.Text != string.Empty)
            {
                BindDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("../Home/Dashboard.aspx"));
        }

        /// <summary>
        /// Handles the OnGridPagerChanged event of the GridPager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Changed(object sender, DAnalytics.UserControls.GridPagerChangedEventArgs e)
        {
            ViewState["PageNum"] = e.CurrentPage.ToString();
            BindDetails();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the parameters to filter audit trail data to be retrieved from database
        /// Gets audit trail details from database and calls the method to bind the grid
        /// </summary>
        private void BindDetails()
        {
            DateTime dtFrom = Convert.ToDateTime(txtFrom.Text, new CultureInfo("en-GB"));
            DateTime dtTo = Convert.ToDateTime(txtTo.Text, new CultureInfo("en-GB"));
            int userId = 0;
            if (ddlUsers.SelectedIndex != 0)
            {
                userId = Utilities.DAnalHelper.ConvertToInt32(ddlUsers.SelectedValue);
            }
            BindRequests(Utilities.ActionLog.AuditTrailDetails(userId, dtFrom, dtTo, this.PageNum, this.PageSize));
        }

        /// <summary>
        /// Binds the data returned from the database to gvAudit gridview control
        /// Binds the pager control
        /// </summary>
        /// <param name="ds">Dataset returned from database</param>
        private void BindRequests(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                totalRows = Convert.ToInt32(ds.Tables[1].Rows[0].ItemArray[0].ToString());
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["TotalRecords"] = totalRows.ToString();
                gvAudit.DataSource = ds;
                gvAudit.DataBind();

                GridPager1.TotalPages = this.TotalPages;
                GridPager1.Bind();
            }
            else
            {
                GridPager1.Visible = false;
                gvAudit.DataSource = null;
                gvAudit.DataBind();
            }
        }        
        
        #endregion
    }
}