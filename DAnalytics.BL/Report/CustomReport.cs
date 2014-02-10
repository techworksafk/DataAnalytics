using System;
using System.Data;

namespace DAnalytics.BL.Report
{
    public class CustomReport
    {
        public static DataSet GetBoreholeReport(int BoreHoleID, DateTime? FromDate, DateTime? ToDate)
        {
            return DA.Report.CustomReport.GetBoreholeReport(BoreHoleID, FromDate, ToDate);
        }

        public static DataSet GetReportBoreholes(int ReportID, int PageSize, int PageNo)
        {
            return DA.Report.CustomReport.GetReportBoreholes(ReportID, PageSize, PageNo);
        }
    }
}
