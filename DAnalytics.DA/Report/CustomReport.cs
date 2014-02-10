using System;
using System.Data;
using System.Data.SqlClient;

namespace DAnalytics.DA.Report
{
    public class CustomReport
    {
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
                _ds = UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_CustomReport_Get", sqlParams);
            }
            finally
            {
                sqlParams = null;
            }
            return _ds;
        }


        public static DataSet GetReportBoreholes(int ReportID, int PageSize, int PageNo)
        {
            DataSet _ds;
            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@ReportID",ReportID,SqlDbType.Int,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@PageSize",PageSize,SqlDbType.Int,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@PageNo",PageNo,SqlDbType.Int,ParameterDirection.Input)
            };
            try
            {
                _ds = UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_CustomReport_BoreHoleGet", sqlParams);
            }
            finally
            {
                sqlParams = null;
            }
            return _ds;
        }
    }
}
