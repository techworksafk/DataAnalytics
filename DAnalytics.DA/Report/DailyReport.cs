using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DAnalytics.UTIL;
using DAnalytics.MO;
namespace DAnalytics.DA.Report
{
    public class DailyReport
    {
        public static DataSet GetMinMaxSummary(DataTable dt, DateTime? FromDate, DateTime? ToDate)
        {
            DataSet _ds;
            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@T_BoreHoleID",dt,SqlDbType.Structured,ParameterDirection.Input),
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

        public static DataSet GetBoreholeReport(DataTable dt, DateTime? FromDate, DateTime? ToDate)
        {
            DataSet _ds;
            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@T_BoreHoleID",dt,SqlDbType.Structured,ParameterDirection.Input),
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

        public static List<DAnalytics.MO.Borehole> SearchBorehole(string SearchString)
        {
            List<DAnalytics.MO.Borehole> _lst = null;

            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@SearchString",SearchString,SqlDbType.VarChar,ParameterDirection.Input)
            };
            try
            {
                using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_Borehole_Autocomplete", sqlParams))
                {
                    while (rdr.Read())
                    {
                        if (_lst == null) _lst = new List<MO.Borehole>();

                        MO.Borehole _obj = new MO.Borehole
                        {

                            BoreHoleID = rdr["BoreHoleID"].ConvertToInt32(),
                            BoreHoleName = Convert.ToString(rdr["BoreHoleName"]),
                            AreaName = Convert.ToString(rdr["AreaName"]),
                            BoreholeType = Convert.ToString(rdr["BoreholeType"]),
                            Depth = Convert.ToString(rdr["Depth"])
                        };
                        _lst.Add(_obj);
                    }
                }
            }
            finally
            {
                sqlParams = null;
            }
            return _lst;
        }

        public static List<DAnalytics.MO.Borehole> GetBorehole(string SearchString)
        {
            List<DAnalytics.MO.Borehole> _lst = null;

            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@SearchString",SearchString,SqlDbType.VarChar,ParameterDirection.Input)
            };
            try
            {
                using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_Borehole_Get", sqlParams))
                {
                    while (rdr.Read())
                    {
                        if (_lst == null) _lst = new List<MO.Borehole>();

                        MO.Borehole _obj = new MO.Borehole
                        {

                            BoreHoleID = rdr["BoreHoleID"].ConvertToInt32(),
                            BoreHoleName = Convert.ToString(rdr["BoreHoleName"]),
                            AreaName = Convert.ToString(rdr["AreaName"]),
                            LoopName = Convert.ToString(rdr["LoopName"]),

                            AreaID = rdr["AreaID"].ConvertToInt32(),
                            LoopID = rdr["LoopID"].ConvertToInt32(),

                            BoreholeType = Convert.ToString(rdr["BoreholeType"]),
                            Depth = Convert.ToString(rdr["Depth"])
                        };
                        _lst.Add(_obj);
                    }
                }
            }
            finally
            {
                sqlParams = null;
            }
            return _lst;
        }

        public static List<DAnalytics.MO.Borehole> GetBorehole(int SurveyYear, int AreaID)
        {
            List<DAnalytics.MO.Borehole> _lst = null;

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                UTIL.SqlHelper.CreateParameter("@SurveyYear",SurveyYear,SqlDbType.Int,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@AreaID",AreaID,SqlDbType.Int,ParameterDirection.Input)
            };
            try
            {
                using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_Borehole_GetBySurvey", sqlParams))
                {
                    while (rdr.Read())
                    {
                        if (_lst == null) _lst = new List<MO.Borehole>();

                        MO.Borehole _obj = new MO.Borehole
                        {

                            BoreHoleID = rdr["BoreHoleID"].ConvertToInt32(),
                            BoreHoleName = Convert.ToString(rdr["BoreHoleName"]),
                            AreaName = Convert.ToString(rdr["AreaName"]),
                            LoopName = Convert.ToString(rdr["LoopName"]),

                            AreaID = rdr["AreaID"].ConvertToInt32(),
                            LoopID = rdr["LoopID"].ConvertToInt32(),

                            BoreholeType = Convert.ToString(rdr["BoreholeType"]),
                            Depth = Convert.ToString(rdr["Depth"]),

                            SurveyID = rdr["SurveyID"].ConvertToInt32(),
                            SurveyName = Convert.ToString(rdr["SurveyName"])
                        };
                        _lst.Add(_obj);
                    }
                }
            }
            finally
            {
                sqlParams = null;
            }
            return _lst;
        }

        public static List<MO.Survey> GetSurveyYear()
        {
            List<MO.Survey> _lst = null;

            using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_DailyReport_SurveyYears"))
            {
                while (rdr.Read())
                {
                    if (_lst == null) _lst = new List<MO.Survey>();
                    _lst.Add(new MO.Survey { SurveyYear = rdr["SurveyYear"].ConvertToInt32() });
                }
            }
            return _lst;
        }

        public static List<MO.Area> GetArea()
        {
            List<MO.Area> _lst = null;

            using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_DailyReport_Area"))
            {
                while (rdr.Read())
                {
                    if (_lst == null) _lst = new List<MO.Area>();
                    _lst.Add(new MO.Area { AreaID = rdr["AreaID"].ConvertToInt32(), AreaName = Convert.ToString(rdr["AreaName"]) });
                }
            }
            return _lst;
        }

        public static bool SaveParam(int ReportID, GenerateReportArgs args)
        {
            bool _IsSaved = false;

            SqlParameter[] sqlParams = new SqlParameter[]{
                UTIL.SqlHelper.CreateParameter("@RptTable",args.BoreHoleTable,SqlDbType.Structured,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@ReportID",ReportID,SqlDbType.Int,ParameterDirection.Input)
            };
            try
            {
                UTIL.SqlHelper.ExecuteNonQuery(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_DailyReportParam_Save", sqlParams);
                _IsSaved = true;

                SaveHeader(ReportID, args);
            }
            catch
            {
                _IsSaved = false;
            }
            finally
            {
                sqlParams = null;
            }

            return _IsSaved;
        }

        static void SaveHeader(int ReportID, GenerateReportArgs args)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{
                
                UTIL.SqlHelper.CreateParameter("@ReportID",ReportID,SqlDbType.Int,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@ContractNo",args.ContractNo,SqlDbType.VarChar,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@PreparedName",args.PreparedName,SqlDbType.VarChar,ParameterDirection.Input),
                UTIL.SqlHelper.CreateParameter("@PreparedDesig",args.PreparedDesig,SqlDbType.VarChar,ParameterDirection.Input)
            };
            try
            {
                UTIL.SqlHelper.ExecuteNonQuery(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_DailyReport_SaveHeader", sqlParams);
            }
            finally
            {
                sqlParams = null;
            }
        }
    }



}
