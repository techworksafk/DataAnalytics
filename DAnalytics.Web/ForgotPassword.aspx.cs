using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MO = DAnalytics.MO;
namespace DAnalytics.Web
{
    /// <summary>
    /// Webfor to handle forgot password
    /// </summary>
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Generate password event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetPswd_Click(object sender, EventArgs e)
        {
            MO.Admin.DAnalyticsUser user = BL.Admin.DAnalyticsUser.ForgotPassword(txtEmail.Text.Trim());
            if (user != null)
            {
                UTIL.Email email = new UTIL.Email();
                email.ToName = user.FirstName + ' ' + user.LastName;
                email.ToEmail = user.Email;
                email.Subject = "Data Analytics User Password";

                StringBuilder emailBody = new StringBuilder();
                emailBody.Append("<p>Dear " + email.ToName + "</p>");
                emailBody.Append("<p>Your Data Analytics  login details are as below:</p>");
                emailBody.Append("<p>Username: ");
                emailBody.Append(user.UserName);
                emailBody.Append("</p>");
                emailBody.Append("<p>Password: ");
                emailBody.Append(user.UserPassword);
                emailBody.Append("</p>");
                emailBody.Append("<p>Data Analytics Administrator</p>");

                email.Body = emailBody.ToString();

                if (UTIL.Email.SendMail(email))
                {
                    UTIL.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", "Your password has been sent to your email address.");
                }
                else
                {
                    UTIL.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", "Error in sending email");
                }
            }
            else
            {
                UTIL.DAnalHelper.GenerateJavaScriptAlert(Page.GetType(), Page, "Message", "A user with this email address does not exist.");
            }
        }
    }
}