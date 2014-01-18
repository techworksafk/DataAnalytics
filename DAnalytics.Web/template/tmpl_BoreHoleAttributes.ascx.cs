using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web;
using System.IO;
using System.Collections.Generic;

namespace DAnalytics.Web.template
{
    public partial class tmpl_BoreHoleAttributes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetControlString()
        {
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            this.RenderControl(htmlWrite);
            return stringWrite.ToString();
        }

        List<string> _DailyReportFiles;

        public List<string> DailyReportFiles
        {
            get { return _DailyReportFiles; }
            set { _DailyReportFiles = value; }
        }

        public int BoreHoleID { get; set; }

        public string BoreHoleName { get; set; }

        public DataRow[] DataSource { get; set; }

        StringBuilder _CH4 = new StringBuilder();
        StringBuilder _CO2 = new StringBuilder();
        StringBuilder _O2 = new StringBuilder();
        StringBuilder _VOC = new StringBuilder();
        StringBuilder _H2S = new StringBuilder();
        StringBuilder _CO = new StringBuilder();
        StringBuilder _Borehole_Pressure = new StringBuilder();
        StringBuilder _Atmospheric_Pressure = new StringBuilder();
        StringBuilder _Pressure_Diff = new StringBuilder();
        StringBuilder _Temperature = new StringBuilder();
        StringBuilder _Water_Level = new StringBuilder();
        StringBuilder _Battery = new StringBuilder();

        StringBuilder _Category = new StringBuilder();

        StringBuilder _BoreHoleTable = new StringBuilder();

        public string BoreHoleTable
        {
            get { return _BoreHoleTable.ToString(); }
        }

        public string CH4 { get { return _CH4.ToString(); } }
        public string CO2 { get { return _CO2.ToString(); } }
        public string O2 { get { return _O2.ToString(); } }
        public string VOC { get { return _VOC.ToString(); } }
        public string H2S { get { return _H2S.ToString(); } }
        public string CO { get { return _CO.ToString(); } }
        public string Borehole_Pressure { get { return _Borehole_Pressure.ToString(); } }
        public string Atmospheric_Pressure { get { return _Atmospheric_Pressure.ToString(); } }
        public string Pressure_Diff { get { return _Pressure_Diff.ToString(); } }
        public string Temperature { get { return _Temperature.ToString(); } }
        public string Water_Level { get { return _Water_Level.ToString(); } }
        public string Battery { get { return _Battery.ToString(); } }
        public string Category { get { return _Category.ToString(); } }

        void AppendHeader(StringBuilder _sb)
        {
            _sb.Append("<tr>")
                .Append("<td>BoreHole</td>")
                .Append("<td>Date Time</td>")
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

        void AppendValues(DataRow dr, StringBuilder _sb)
        {
            _sb.Append("<tr>")
                .Append("<td>").Append(Convert.ToString(dr["BoreHoleName"])).Append("</td>")
                .Append("<td>").Append(Convert.ToDateTime(dr["ReadingDateTime"]).ToString("dd-MMM-yy HH:mm")).Append("</td>")
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

        public string PlotGraph()
        {
            if (DataSource != null && DataSource.Length > 0)
            {

                string _HtmlTemplate = string.Empty;

                using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("../template/tmpl_BoreHoleAttributes.htm"), FileMode.Open, FileAccess.Read))
                {
                    StreamReader _sr = new StreamReader(fs);
                    _HtmlTemplate = _sr.ReadToEnd();
                    _sr.Close();
                }

                string _ReportHtml = this.BoreHoleName + ".htm";
                string _ReportDirectoryAbsolute = "../reports/";
                string _ReportDirectoryPhysical = HttpContext.Current.Server.MapPath(_ReportDirectoryAbsolute);
                string _ReportHtmlPath = Path.Combine(_ReportDirectoryPhysical, _ReportHtml);

                _DailyReportFiles.Add(_ReportHtmlPath);

                _BoreHoleTable.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\">");
                AppendHeader(_BoreHoleTable);
                for (int iCount = 0; iCount < DataSource.Length; iCount++)
                {
                    DataRow dr = DataSource[iCount];

                    AppendValues(dr, _BoreHoleTable);

                    if (iCount != 0)
                    {
                        _Category.Append(",");
                        _CH4.Append(",");
                        _CO2.Append(",");
                        _O2.Append(",");
                        _VOC.Append(",");
                        _H2S.Append(",");
                        _CO.Append(",");
                        _Borehole_Pressure.Append(",");
                        _Atmospheric_Pressure.Append(",");
                        _Pressure_Diff.Append(",");
                        _Temperature.Append(",");
                        _Water_Level.Append(",");
                        _Battery.Append(",");

                    }

                    _Category.Append("'").Append(Convert.ToDateTime(dr["ReadingDateTime"]).ToString("dd-MMM HH:mm")).Append("'");
                    _CH4.Append(FormatValue(Convert.ToString(dr["CH4"])));
                    _CO2.Append(FormatValue(Convert.ToString(dr["CO2"])));
                    _O2.Append(FormatValue(Convert.ToString(dr["O2"])));
                    _VOC.Append(FormatValue(Convert.ToString(dr["VOC"])));
                    _H2S.Append(FormatValue(Convert.ToString(dr["H2S"])));
                    _CO.Append(FormatValue(Convert.ToString(dr["CO"])));
                    _Borehole_Pressure.Append(FormatValue(Convert.ToString(dr["Borehole_Pressure"])));
                    _Atmospheric_Pressure.Append(FormatValue(Convert.ToString(dr["Atmospheric_Pressure"])));
                    _Pressure_Diff.Append(FormatValue(Convert.ToString(dr["Pressure_Diff"])));
                    _Temperature.Append(FormatValue(Convert.ToString(dr["Temperature"])));
                    _Water_Level.Append(FormatValue(Convert.ToString(dr["Water_Level"])));
                    _Battery.Append(FormatValue(Convert.ToString(dr["Battery"])));
                }
                _BoreHoleTable.Append("</table>");


                _HtmlTemplate = _HtmlTemplate.Replace("###BOREHOLE_ATTRIBUTES###", GetControlString());

                using (FileStream fs = new FileStream(_ReportHtmlPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter _sw = new StreamWriter(fs);
                    _sw.Write(_HtmlTemplate);
                    _sw.Flush();
                    _sw.Close();
                }
            }
            else
            {
                return string.Empty;
            }

            

            return GetControlString();
        }


        string FormatValue(string Value)
        {
            return Value.Trim() == string.Empty ? "0" : Value;
        }
    }
}