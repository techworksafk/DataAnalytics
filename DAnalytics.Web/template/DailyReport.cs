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
            StringBuilder _MinMax = new StringBuilder();

            _MinMax.Append("<table>");
            _CH4Max.Append("<table>");
            foreach(DataRow dr in dsMinMax.Tables[0].Rows)
            {
                _MinMax.Append("<tr>");
                
                if(Convert.ToString(dr["MinOrMax"]).ToUpper()=="MAX")
                {
                    _CH4Max.Append("<tr>");


                    _CH4Max.Append("</tr>");
                }
                _MinMax.Append("</tr>");
            }
            _MinMax.Append("<table>");
            _CH4Max.Append("<table>");

            tmpl_BoreHoleAttributes _uc_BoreHoleAttributes = new System.Web.UI.UserControl().LoadControl("../template/tmpl_BoreHoleAttributes.ascx") as tmpl_BoreHoleAttributes;
            _uc_BoreHoleAttributes.ID = 

        }
    }
}