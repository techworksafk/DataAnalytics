using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DAnalytics.DA.Report
{
    public class CustomReport
    {
        static object _locker = new object();

        public static DataSet GetBoreholeReport(int BoreHoleID, DateTime? FromDate, DateTime? ToDate)
        {
            lock (_locker)
            {
                DataSet _ds;

                List<SqlParameter> _sqlParams = new List<SqlParameter>();

                _sqlParams.Add(UTIL.SqlHelper.CreateParameter("@BoreHoleID", BoreHoleID, SqlDbType.Int, ParameterDirection.Input));
                if (FromDate.HasValue)
                    _sqlParams.Add(UTIL.SqlHelper.CreateParameter("@FromDate", FromDate.Value, SqlDbType.Date, ParameterDirection.Input));
                if (ToDate.HasValue)
                    _sqlParams.Add(UTIL.SqlHelper.CreateParameter("@ToDate", ToDate.Value, SqlDbType.Date, ParameterDirection.Input));
                try
                {
                    _ds = UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_CustomReport_Get", _sqlParams.ToArray());
                }
                finally
                {
                    _sqlParams = null;
                }
                return _ds;
            }
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
