using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL = DAnalytics.BL;
using UTIL = DAnalytics.UTIL;
using MO = DAnalytics.MO;

namespace DAnalytics.Web
{
    /// <summary>
    /// Webform to handle the sign in to the application
    /// </summary>
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // lblRem.InnerText = UTIL.CryptorEngine.Encrypt("123", true);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int userId = 0;
            if (BL.Admin.DAnalyticsUser.AuthenticateUser(txtUserName.Text, txtPassword.Text, out userId))
            {
                MO.Admin.DAnalyticsUser _user = BL.Admin.DAnalyticsUser.GetUserByID(userId);
                Session[UTIL.DAnalyticsKeys.USER] = _user;
                UTIL.ActionLog.UpdateLoginHistory(userId, UTIL.LoginAction.LOGIN);             
                Response.Redirect("Home/Dashboard.aspx", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "m", "alert('-Invalid Login!!!')", true);
            }
        }
    }   
}