using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAnalytics.Web.Report
{
    public partial class MinMaxSummary : BasePage.DAnalBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MenuID = 6;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.SetAccessRoles();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDtFrom.Attributes.Add("readonly", "readonly");
            txtDtTo.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                hdnUserID.Value = UserID.ToString();
                txtDtFrom.Text = UTIL.DAnalHelper.FirstDayOfMonth(System.DateTime.Now).ToString("dd/MM/yyyy");
                txtDtTo.Text = UTIL.DAnalHelper.LastDayOfMonth(System.DateTime.Now).ToString("dd/MM/yyyy");
            }

        }
    }
}