using System;
using System.Data;
using System.Data.SqlClient;

namespace DAnalytics.DA.Report
{
    public class DailyReport
    {
        public static DataSet GetMinMaxSummary(int BoreHoleID, DateTime? FromDate, DateTime? ToDate)
        {
            DataSet _ds;
            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@BoreHoleID",BoreHoleID,SqlDbType.Int,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@FromDate",FromDate,SqlDbType.Date,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@ToDate",ToDate,SqlDbType.Date,ParameterDirection.Input)
            };
            try
            {
                _ds = UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_DailyReport_MinMax", sqlParams);
            }
            finally
            {
                sqlParams = null;
            }
            return _ds;
        }

        public static DataSet GetBoreholeReport(int BoreHoleID, DateTime? FromDate, DateTime? ToDate)
        {
            DataSet _ds;
            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@BoreHoleID",BoreHoleID,SqlDbType.Int,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@FromDate",FromDate,SqlDbType.Date,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@ToDate",ToDate,SqlDbType.Date,ParameterDirection.Input)
            };
            try
            {
                _ds = UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_DailyReport_Borehole", sqlParams);
            }
            finally
            {
                sqlParams = null;
            }
            return _ds;
        }
    }
}
