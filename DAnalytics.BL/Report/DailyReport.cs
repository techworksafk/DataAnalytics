using System;
using System.Data;

namespace DAnalytics.BL.Report
{
    public class DailyReport
    {
        public static DataSet GetMinMaxSummary(int BoreHoleID = 0, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            return DA.Report.DailyReport.GetMinMaxSummary(BoreHoleID, FromDate, ToDate);
        }

        public static DataSet GetBoreholeReport(int BoreHoleID, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            if (BoreHoleID == 0) throw new Exception("BoreHoleID cannot be zero");

            return DA.Report.DailyReport.GetBoreholeReport(BoreHoleID, FromDate, ToDate);
        }
    }
}
