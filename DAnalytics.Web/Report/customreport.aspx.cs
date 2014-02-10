using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAnalytics.MO;
using DAnalytics.UTIL;
using System.Data;
namespace DAnalytics.Web.Report
{
    public partial class customreport : BasePage.DAnalBase
    {
        int PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnUserID.Value = UserID.ToString();
                hdnReportID.Value = DateTime.Now.ToString("ddMMHHmmss");
                BoreHoleTree1.Bind();
            }

            BoreHoleTree1.OnGenerateReport += new UserControls.GenerateReport(BoreHoleTree1_OnGenerateReport);
            GridPager1.GridPagerChanged += new DAnalytics.UserControls.GridPagerChangedEventHandler(GridPager1_GridPagerChanged);
        }

        void GridPager1_GridPagerChanged(object sender, DAnalytics.UserControls.GridPagerChangedEventArgs e)
        {
            BindGrid(e.CurrentPage);
        }

        void BoreHoleTree1_OnGenerateReport(GenerateReportArgs args)
        {
            BL.Report.DailyReport.SaveParam(hdnReportID.Value.ConvertToInt32(), args);
            BindGrid(1);
        }

        void BindGrid(int PageNumber)
        {
            DataSet ds = BL.Report.CustomReport.GetReportBoreholes(hdnReportID.Value.ConvertToInt32(), PageSize, PageNumber);

            gvBoreHoles.DataSource = ds;
            gvBoreHoles.DataBind();

            GridPager1.ControlToBind = "gvBoreHoles";
            GridPager1.TotalPages = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["TotalPages"].ConvertToInt32() : 0;
            GridPager1.Bind();

            GridPager1.Visible = ds != null && ds.Tables[0].Rows.Count > 0;
        }
    }
}