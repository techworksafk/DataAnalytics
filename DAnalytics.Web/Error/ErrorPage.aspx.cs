using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTIL = DAnalytics.UTIL;
namespace DAnalytics.Web.Error
{
    /// <summary>
    /// webform to display the handled errors
    /// </summary>
    public partial class ErrorPage : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[UTIL.DAnalyticsKeys.USERID] != null)
            {
                int userId = Convert.ToInt32(Session[UTIL.DAnalyticsKeys.USERID]);
                UTIL.ActionLog.UpdateLoginHistory(userId, UTIL.LoginAction.LOGOUT);
                Session.Remove(UTIL.DAnalyticsKeys.USERID);
            }
            if (Session[UTIL.DAnalyticsKeys.EXCEPTION] != null)
            {
                lblError.Text = Convert.ToString(Session[UTIL.DAnalyticsKeys.EXCEPTION]);
            }
        }
    }
}