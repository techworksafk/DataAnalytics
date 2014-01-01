using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DAnalytics.UTIL;
namespace DAnalytics.Web.Report
{
    public partial class MinMaxSummary : BasePage.DAnalBase
    {
        CultureInfo _enGB = new CultureInfo("en-GB");

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
            //txtDtFrom.Attributes.Add("readonly", "readonly");
            //txtDtTo.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                hdnUserID.Value = UserID.ToString();
                txtDtFrom.Text = UTIL.DAnalHelper.FirstDayOfMonth(System.DateTime.Now).ToString("dd/MM/yyyy");
                txtDtTo.Text = UTIL.DAnalHelper.LastDayOfMonth(System.DateTime.Now).ToString("dd/MM/yyyy");
            }

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DateTime? _FromDate = null, _ToDate = null;
            if (!string.IsNullOrEmpty(txtDtFrom.Text))
                _FromDate = Convert.ToDateTime(txtDtFrom.Text, _enGB);

            if (!string.IsNullOrEmpty(txtDtTo.Text))
                _ToDate = Convert.ToDateTime(txtDtTo.Text, _enGB);

            gvBoreHoles.DataSource = BL.Report.DailyReport.GetMinMaxSummary(hdnBoreHoleID.Value.ConvertToInt32(), _FromDate, _ToDate);
            gvBoreHoles.DataBind();
        }

    }
}