using System;
using System.Data;
using System.Globalization;
using System.Web;
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
            if (!IsPostBack)
            {
                hdnUserID.Value = UserID.ToString();
                BoreHoleTree1.Bind();
            }

            BoreHoleTree1.OnGenerateReport+=new UserControls.GenerateReport(BoreHoleTree1_OnGenerateReport);
        }

        void BoreHoleTree1_OnGenerateReport(DataTable dt, DateTime? From, DateTime? To)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AddHeader("Content-Disposition", "attachment; filename=Dailyreport.pdf");
            Response.ContentType = "application/pdf";
            Response.TransmitFile(GenerateReport(dt, From.Value, To.Value));
        }
    }
}