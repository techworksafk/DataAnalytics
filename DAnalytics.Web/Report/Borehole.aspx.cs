using System;
using System.Data;
using System.Globalization;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using DAnalytics.UTIL;
using System.Collections.Generic;
using DAnalytics.Web.UserControls;
using System.Web.UI;
using System.IO;
using System.Threading;
using DAnalytics.MO;
namespace DAnalytics.Web.Report
{
    public partial class Borehole : BasePage.DAnalBase
    {
        CultureInfo _enGB = new CultureInfo("en-GB");
        List<string> _Files;

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
                hdnReportID.Value = DateTime.Now.ToString("ddMMHHmmss");
                BoreHoleTree1.Bind();
            }

            BoreHoleTree1.OnGenerateReport += new UserControls.GenerateReport(BoreHoleTree1_OnGenerateReport);
        }

        void BoreHoleTree1_OnGenerateReport(GenerateReportArgs args)
        {
            BL.Report.DailyReport.SaveParam(hdnReportID.Value.ConvertToInt32(), args);
            _Files = new List<string>();

            Thread _trdMinMax = new Thread(new ThreadStart(delegate()
            {
                GenerateMinMaxSummary(args);
            }));
            _trdMinMax.Start();
            
            Thread _trdBoreHole = new Thread(new ThreadStart(delegate()
            {
                GenerateBoreHole(args);
            }));

            _trdBoreHole.Start();

            _trdMinMax.Join();
            _trdBoreHole.Join();

            string OPFile = new PDFUtil().CombinePDF(_Files);

            if (File.Exists(OPFile))
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.AddHeader("Content-Disposition", "attachment; filename=Dailyreport.pdf");
                Response.ContentType = "application/pdf";
                Response.TransmitFile(OPFile);
            }
        }

        private ReportViewer GetReportViewer()
        {
            ReportViewer objViewer = new ReportViewer();
            objViewer.ProcessingMode = ProcessingMode.Remote;
            objViewer.AsyncRendering = true;
            objViewer.PromptAreaCollapsed = true;
            objViewer.ShowParameterPrompts = false;
            objViewer.ExportContentDisposition = ContentDisposition.AlwaysAttachment;

            objViewer.ServerReport.ReportServerCredentials = new DAnalytics.UTIL.DAnalyticsCredentials(
                   ConfigurationManager.AppSettings["SSRS_USERNAME"],
                   ConfigurationManager.AppSettings["SSRS_PASSWORD"],
                   ConfigurationManager.AppSettings["SSRS_DOMAIN"]);
            objViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["SSRS_SERVER"]);
            return objViewer;
        }

        void GenerateMinMaxSummary(GenerateReportArgs args)
        {
            ReportViewer objViewer = GetReportViewer();
            objViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_PATH"] + "dailyreport_minmax";

            List<ReportParameter> parameters = new List<ReportParameter>();

            parameters.Add(new ReportParameter("ReportID", hdnReportID.Value));
            parameters.Add(new ReportParameter("DoAutoPick", args.DoAutoPick.ToString()));
            if (args.FromDate.HasValue)
                parameters.Add(new ReportParameter("FromDate", args.FromDate.Value.ToString("MM/dd/yyyy")));
            if (args.ToDate.HasValue)
                parameters.Add(new ReportParameter("ToDate", args.ToDate.Value.ToString("MM/dd/yyyy")));
            objViewer.ServerReport.SetParameters(parameters);

            DownloadReport(objViewer);
        }

        void GenerateBoreHole(GenerateReportArgs args)
        {
            ReportViewer objViewer = GetReportViewer();
            objViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_PATH"] + "dailyreport_borehole";

            List<ReportParameter> parameters = new List<ReportParameter>();

            parameters.Add(new ReportParameter("ReportID", hdnReportID.Value));
            parameters.Add(new ReportParameter("DoAutoPick", args.DoAutoPick.ToString()));
            if (args.FromDate.HasValue)
                parameters.Add(new ReportParameter("FromDate", args.FromDate.Value.ToString("MM/dd/yyyy")));
            if (args.ToDate.HasValue)
                parameters.Add(new ReportParameter("ToDate", args.ToDate.Value.ToString("MM/dd/yyyy")));
            objViewer.ServerReport.SetParameters(parameters);

            DownloadReport(objViewer);
        }

        void DownloadReport(ReportViewer objViewer)
        {
            string mimeType = string.Empty; string encoding = string.Empty; string extension = string.Empty; string deviceInfo = string.Empty;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string format = "PDF";
            deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>";
            byte[] bytes = null;
            try { bytes = objViewer.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings); }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Message", "<script>alert('Unexpected error occurred on rending SSRS.Please contact the system administrator.')</script>");
            }
            try
            {
                string FileName = System.Guid.NewGuid().ToString() + ".pdf";
                string Dir = Server.MapPath("~/reports/");
                string FileFullName = Path.Combine(Dir, FileName);

                using (FileStream stream = File.Create(FileFullName, bytes.Length))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                _Files.Add(FileFullName);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Message", "<script>alert('Unexpected error occurred on writing file.Please contact the system administrator.')</script>");
            }
        }

    }
}