using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAnalytics.Web.Home
{
    /// <summary>
    /// Landing page of the Data Analytics  applicaion
    /// </summary>
    public partial class Dashboard : BasePage.DAnalBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateForm();
            }
        }

        /// <summary>
        /// Method to populate dashboard form.
        /// </summary>
        void PopulateForm()
        {
            try
            {                
                if (this.CurrentUser.UserRoleMappings != null && this.CurrentUser.UserRoleMappings.Count > 0)
                {
                     mStatus.Visible = true;                   
                }                
            }
            catch { }
        }

        /// <summary>
        /// Method to set staus fields.
        /// </summary>
        /// <param name="ds"></param>
        void SetStatus(System.Data.DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    System.Data.DataRow dr = ds.Tables[0].Rows[0];
                    mUnAssTaskCount.InnerText = UTIL.DAnalHelper.ConvertToString(dr["UnAssigned"]);
                    mOverDueTaskCount.InnerText = UTIL.DAnalHelper.ConvertToString(dr["OverDue"]);
                    mCriticalTaskCount.InnerText = UTIL.DAnalHelper.ConvertToString(dr["Critical"]);
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    System.Data.DataRow dr = ds.Tables[1].Rows[0];
                    uAssTaskCount.InnerText = UTIL.DAnalHelper.ConvertToString(dr["Assigned"]);
                    uOverDueTaskCount.InnerText = UTIL.DAnalHelper.ConvertToString(dr["OverDue"]);
                    uCriticalTaskCount.InnerText = UTIL.DAnalHelper.ConvertToString(dr["Critical"]);
                }
            }
        }       
    }
}