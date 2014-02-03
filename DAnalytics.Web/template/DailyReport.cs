using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAnalytics.Web.template;
using System.Text;
using System.IO;

namespace DAnalytics.web
{
    public class DailyReport : BasePage.DAnalBase
    {
        public string GenerateReport(DataTable dt, DateTime FromDate, DateTime ToDate)
        {

            List<string> _DailyReportFiles = new List<string>();

            string _HtmlTemplate = string.Empty;

            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("../template/tmpl_DailyReport.htm"), FileMode.Open, FileAccess.Read))
            {
                StreamReader _sr = new StreamReader(fs);
                _HtmlTemplate = _sr.ReadToEnd();
                _sr.Close();
            }

            string _ReportHtml = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".htm";
            string _ReportDirectoryAbsolute = "../reports/";
            string _ReportDirectoryPhysical = HttpContext.Current.Server.MapPath(_ReportDirectoryAbsolute);
            string _ReportHtmlPath = Path.Combine(_ReportDirectoryPhysical, _ReportHtml);

            _DailyReportFiles.Add(_ReportHtmlPath);

            DataSet dsBoreHole = BL.Report.DailyReport.GetBoreholeReport(dt, FromDate, ToDate);
            DataSet dsMinMax = BL.Report.DailyReport.GetMinMaxSummary(dt, FromDate, ToDate);

            StringBuilder _CH4Max = new StringBuilder();
            StringBuilder _CH4MaxPlot = new StringBuilder();
            StringBuilder _MinMax = new StringBuilder();
            StringBuilder _Graph = new StringBuilder();
            StringBuilder _DailyReport = new StringBuilder();

            _MinMax.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\" width=\"100%\">");
            AppendMinMaxHeader(_MinMax);

            _CH4Max.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\" width=\"50%\">");
            AppendCH4MaxHeader(_CH4Max);


            tmpl_CH4Max _uc_tmpl_CH4Max = null;

            bool _IsFirst = true;

            foreach (DataRow dr in dsMinMax.Tables[0].Rows)
            {
                AppendMinMaxValues(dr, _MinMax);
                if (_uc_tmpl_CH4Max == null || _uc_tmpl_CH4Max.PlotValues.Count > 24)
                    _uc_tmpl_CH4Max = LoadControl("../template/tmpl_CH4Max.ascx") as tmpl_CH4Max;

                if (Convert.ToString(dr["MinOrMax"]).ToUpper() == "MAX")
                {
                    AppendCH4MaxValues(dr, _CH4Max);

                    tmpl_BoreHoleAttributes _uc_BoreHoleAttributes = LoadControl("../template/tmpl_BoreHoleAttributes.ascx") as tmpl_BoreHoleAttributes;
                    _uc_BoreHoleAttributes.ID = Convert.ToString(dr["BoreHoleID"]);
                    _uc_BoreHoleAttributes.BoreHoleName = Convert.ToString(dr["BoreHoleName"]);
                    _uc_BoreHoleAttributes.DailyReportFiles = _DailyReportFiles;
                    _uc_BoreHoleAttributes.DataSource = dsBoreHole.Tables[0].Select(" BoreHoleID = " + Convert.ToString(dr["BoreHoleID"]));

                    if (!_IsFirst)
                        _Graph.Append("<div class=\"page_brk\"></div>");
                    _Graph.Append(_uc_BoreHoleAttributes.PlotGraph());

                    if (Convert.ToString(dr["CH4"]).ToUpper() != "NA")
                        _uc_tmpl_CH4Max.PlotValues.Add(new PlotValue { CH4 = Convert.ToString(dr["CH4"]), BoreHoleName = Convert.ToString(dr["BoreHoleName"]) });
                }

                if (_uc_tmpl_CH4Max.PlotValues.Count > 24)
                {
                    _CH4MaxPlot.Append(_uc_tmpl_CH4Max.PlotGraph());
                    _uc_tmpl_CH4Max = null;
                }
            }

            if (_uc_tmpl_CH4Max != null && !_uc_tmpl_CH4Max.HasPlotted)
            {
                _CH4MaxPlot.Append(_uc_tmpl_CH4Max.PlotGraph());
                _uc_tmpl_CH4Max = null;
            }
            _MinMax.Append("</table>");
            _CH4Max.Append("</table>");

            //_DailyReport.Append(_MinMax.ToString())
            //    .Append(_CH4Max.ToString())
            //    .Append(_CH4MaxPlot.ToString())
            //    .Append(_Graph.ToString());

            _HtmlTemplate = _HtmlTemplate.Replace("###MINMAX_SUMMARY###", _MinMax.ToString());
            _HtmlTemplate = _HtmlTemplate.Replace("###CH4MAX_SUMMARY###", _CH4Max.ToString());
            _HtmlTemplate = _HtmlTemplate.Replace("###CH4MAX_SUMMARY_GRAPH###", _CH4MaxPlot.ToString());


            using (FileStream fs = new FileStream(_ReportHtmlPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                StreamWriter _sw = new StreamWriter(fs);
                _sw.Write(_HtmlTemplate);
                _sw.Flush();
                _sw.Close();
            }

            UTIL.Webkit _wk = new UTIL.Webkit();
            //UTIL.WebkitFile _wkFile = new UTIL.WebkitFile
            //{
            //    HtmlFilePath = "http://localhost/DAnalytics.Web/reports/" + _ReportHtml,
            //    PDFFilePath = Path.Combine(_ReportDirectoryPhysical, Path.GetFileNameWithoutExtension(_ReportHtml) + ".pdf")
            //};
            _wk.ConvertHtmlToPDF(_DailyReportFiles);
            return _wk.DailyReportFilePath;
        }

        void AppendMinMaxHeader(StringBuilder _sb)
        {
            _sb.Append("<tr>")
                .Append("<td>BoreHole</td>")
                .Append("<td>Min/Max</td>")
                .Append("<td>CH4</td>")
                .Append("<td>CO2</td>")
                .Append("<td>O2</td>")
                .Append("<td>VOC</td>")
                .Append("<td>H2S</td>")
                .Append("<td>CO</td>")
                .Append("<td>BoreHole Pressure</td>")
                .Append("<td>Atmospheric Pressure</td>")
                .Append("<td>Pressure Diff</td>")
                .Append("<td>Temperature</td>")
                .Append("<td>Water Level</td>")
                .Append("<td>Battery</td>")
                .Append("</tr>");
        }

        void AppendMinMaxValues(DataRow dr, StringBuilder _sb)
        {
            _sb.Append("<tr>")
                .Append("<td>").Append(Convert.ToString(dr["BoreHoleName"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["MinOrMax"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["CH4"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["CO2"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["O2"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["VOC"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["H2S"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["CO"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["Borehole_Pressure"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["Atmospheric_Pressure"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["Pressure_Diff"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["Temperature"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["Water_Level"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["Battery"])).Append("</td>")
                .Append("</tr>");
        }

        void AppendCH4MaxHeader(StringBuilder _sb)
        {
            _sb.Append("<tr>")
                .Append("<td>BoreHole</td>")
                .Append("<td>CH4</td>")
                .Append("</tr>");
        }

        void AppendCH4MaxValues(DataRow dr, StringBuilder _sb)
        {
            _sb.Append("<tr>")
                .Append("<td>").Append(Convert.ToString(dr["BoreHoleName"])).Append("</td>")
                .Append("<td>").Append(Convert.ToString(dr["CH4"])).Append("</td>")
                .Append("</tr>");
        }
    }
}