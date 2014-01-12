using System;
using System.Data;
using System.Collections.Generic;

namespace DAnalytics.BL.Report
{
    public class DailyReport
    {
        public static DataSet GetMinMaxSummary(DataTable dt, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            return DA.Report.DailyReport.GetMinMaxSummary(dt, FromDate, ToDate);
        }

        public static DataSet GetBoreholeReport(DataTable dt, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            return DA.Report.DailyReport.GetBoreholeReport(dt, FromDate, ToDate);
        }

        public static List<DAnalytics.MO.Borehole> SearchBorehole(string SearchString)
        {
            return DA.Report.DailyReport.SearchBorehole(SearchString);
        }

    }
}
