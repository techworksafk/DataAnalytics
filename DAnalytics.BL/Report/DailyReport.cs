using System;
using System.Data;
using System.Collections.Generic;

namespace DAnalytics.BL.Report
{
    public class DailyReport
    {
        public static bool SaveParam(int ReportID, DataTable dt)
        {
            return DA.Report.DailyReport.SaveParam(ReportID, dt);
        }

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

        public static List<DAnalytics.MO.Borehole> GetBorehole(string SearchString)
        {
            return DA.Report.DailyReport.GetBorehole(SearchString);
        }

        public static List<MO.Survey> GetSurveyYear()
        {
            return DA.Report.DailyReport.GetSurveyYear();
        }

        public static List<MO.Area> GetArea()
        {
            return DA.Report.DailyReport.GetArea();
        }


        public static List<DAnalytics.MO.Borehole> GetBorehole(int SurveyYear, int AreaID)
        {
            return DA.Report.DailyReport.GetBorehole(SurveyYear, AreaID);
        }
    }
}
