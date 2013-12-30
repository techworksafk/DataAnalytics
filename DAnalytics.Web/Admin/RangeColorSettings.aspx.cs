using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAnalytics.Web.Admin
{
    public partial class RangeColorSettings : BasePage.DAnalBase
    {
        #region Page Events

        /// <summary>
        /// Handles the OnInit event of the Page control.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MenuID = 4;
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
           
        }

        /// <summary>
        /// Handles the Click event of the btnSave button control.
        /// Saves the role details to the database
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
          
         
        }

        /// <summary>
        /// Handles the Click event of the btnCancel button control.      
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Dashboard.aspx");
        }

        #endregion
    }
}