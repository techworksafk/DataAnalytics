using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DAnalytics.UTIL;
using System.Data;
namespace DAnalytics.Web.Report
{
    public partial class Borehole : DAnalytics.web.DailyReport
    {
        CultureInfo _enGB = new CultureInfo("en-GB");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MenuID = 7;
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

                BoreHoleTree1.Bind();
            }

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DateTime? _FromDate = null, _ToDate = null;
            if (!string.IsNullOrEmpty(txtDtFrom.Text))
                _FromDate = Convert.ToDateTime(txtDtFrom.Text, _enGB);

            if (!string.IsNullOrEmpty(txtDtTo.Text))
                _ToDate = Convert.ToDateTime(txtDtTo.Text, _enGB);

            DataTable dt = new DataTable();
            dt.Columns.Add("BoreHoleID", typeof(int));

            List<DAnalytics.MO.Borehole> _lst = DAnalytics.BL.Report.DailyReport.SearchBorehole("");

            for (int iCount = 0; iCount < _lst.Count; iCount++)
            {
                DataRow dr = dt.NewRow();
                dr["BoreHoleID"] = _lst[iCount].BoreHoleID;
                dt.Rows.Add(dr);
            }

            System.Data.DataSet _ds = BL.Report.DailyReport.GetBoreholeReport(dt, _FromDate, _ToDate);

            gvBoreHole.DataSource = _ds;
            gvBoreHole.DataBind();

            BoreHoleChart1.DataSource = _ds;
            BoreHoleChart1.PlotGraph();
        }
    }
}