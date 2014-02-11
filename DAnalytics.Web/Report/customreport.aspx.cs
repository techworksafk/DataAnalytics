using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAnalytics.MO;
using DAnalytics.UTIL;
using System.Data;
using DAnalytics.Web.UserControls;
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
            BoreHoleTree1.OnNavigate += new UserControls.Navigate(BoreHoleTree1_OnNavigate);
            GridViewPager1.OnGridViewPagerChange += new UserControls.GridViewPagerChange(GridViewPager1_OnGridViewPagerChange);
        }

        protected override void OnPreRender(EventArgs e)
        {
            GridViewPager1.Visible = gvBoreHoles.Rows.Count > 0;
            base.OnPreRender(e);
        }

        void GridViewPager1_OnGridViewPagerChange()
        {
            BindGrid(GridViewPager1.CurrentPageNumber);
        }

        void BoreHoleTree1_OnNavigate(UserControls.NavigateAction Action)
        {
            if (Action == UserControls.NavigateAction.Backward)
            {
                gvBoreHoles.DataSource = null;
                gvBoreHoles.DataBind();
            }
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

            GridViewPager1.TotalPages = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["TotalPages"].ConvertToInt32() : 1;
        }

        protected void gvBoreHoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BoreHoleAllChart BoreHoleAllChart1 = e.Row.FindControl("BoreHoleAllChart1") as BoreHoleAllChart;
                BoreHoleAllChart1.FromDate = BoreHoleTree1.FromDate.HasValue? BoreHoleTree1.FromDate.Value.ToString("MM/dd/yyyy"):"";
                BoreHoleAllChart1.ToDate = BoreHoleTree1.ToDate.HasValue ? BoreHoleTree1.ToDate.Value.ToString("MM/dd/yyyy") : "";
            }
        }
    }
}