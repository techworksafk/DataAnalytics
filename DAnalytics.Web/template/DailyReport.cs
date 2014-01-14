using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAnalytics.Web.template;
using System.Text;

namespace DAnalytics.web
{
    public class DailyReport
    {
        public void Generate(DataTable dt, DateTime FromDate, DateTime ToDate)
        {
            DataSet dsBoreHole = BL.Report.DailyReport.GetBoreholeReport(dt, FromDate, ToDate);
            DataSet dsMinMax = BL.Report.DailyReport.GetMinMaxSummary(dt, FromDate, ToDate);

            StringBuilder _CH4Max = new StringBuilder();
            StringBuilder _CH4MaxPlot = new StringBuilder();
            StringBuilder _MinMax = new StringBuilder();
            StringBuilder _Graph = new StringBuilder();

            _MinMax.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\">");
            AppendMinMaxHeader(_MinMax);

            _CH4Max.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"1\">");
            AppendCH4MaxHeader(_CH4Max);

            
            tmpl_CH4Max _uc_tmpl_CH4Max = null;
            foreach (DataRow dr in dsMinMax.Tables[0].Rows)
            {
                AppendMinMaxValues(dr, _MinMax);
                if (_uc_tmpl_CH4Max == null || _uc_tmpl_CH4Max.PlotValues.Count > 24)
                    _uc_tmpl_CH4Max = new System.Web.UI.UserControl().LoadControl("../template/tmpl_CH4Max.ascx") as tmpl_CH4Max;

                if (Convert.ToString(dr["MinOrMax"]).ToUpper() == "MAX")
                {
                    AppendCH4MaxValues(dr, _CH4Max);

                    tmpl_BoreHoleAttributes _uc_BoreHoleAttributes = new System.Web.UI.UserControl().LoadControl("../template/tmpl_BoreHoleAttributes.ascx") as tmpl_BoreHoleAttributes;
                    _uc_BoreHoleAttributes.ID = Convert.ToString(dr["BoreHoleID"]);
                    _uc_BoreHoleAttributes.DataSource = dsBoreHole.Tables[0].Select(" BoreHoleID = " + Convert.ToString(dr["BoreHoleID"]));
                    _Graph.Append(_uc_BoreHoleAttributes.PlotGraph());

                    _uc_tmpl_CH4Max.PlotValues.Add(new PlotValue { CH4 = Convert.ToString(dr["CH4"]), BoreHoleName = Convert.ToString(dr["BoreHoleName"]) });
                }

                if (_uc_tmpl_CH4Max.PlotValues.Count > 24)
                    _CH4MaxPlot.Append(_uc_tmpl_CH4Max.PlotGraph());
            }
            _MinMax.Append("<table>");
            _CH4Max.Append("<table>");
        }

        void AppendMinMaxHeader(StringBuilder _sb)
        {
            _sb.Append("<th>")
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
                .Append("</th>");
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
            _sb.Append("<th>")
                .Append("<td>BoreHole</td>")
                .Append("<td>CH4</td>")
                .Append("</th>");
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