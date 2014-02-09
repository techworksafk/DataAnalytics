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
                
            }
        }

        /// <summary>
        /// Method to populate dashboard form.
        /// </summary>
        
    }
}