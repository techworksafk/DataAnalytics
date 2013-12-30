using System.Data.SqlClient;
using System.Data;
using System;
using Utilites = DAnalytics.UTIL;

namespace DAnalytics.UTIL
{
    /// <summary>
    /// Util Layer class for ActionLog
    /// </summary>
    public class ActionLog
    {
        #region Properties

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string DataBaseEntity { get; set; }
        public char Operation { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public System.Nullable<System.DateTime> LoggedDate { get; set; }
        public System.Nullable<System.DateTime> Date { get; set; }

        #endregion

        struct Params
        {
            public const string UserID = "@UserID";
            public const string DataBaseEntity = "@DataBaseEntity";
            public const string Operation = "@Operation";
            public const string EntityName = "@EntityName";

            public const string DateFrom = "@DateFrom";
            public const string DateTo = "@DateTo";

            public const string ReturnValue = "@ReturnValue";
            public const string PageNum = "@PageNum";
            public const string PageSize = "@PageSize";
            public const string SortExpression = "@SortExpression";
            public const string SortDirection = "@SortDirection";

            public const string ActionType = "@ActionType";
            public const string GetAll = "@GetAll";
        }

        struct Sprocs
        {
            public const string AuditTrail_Save = "usp_AuditTrail_Save";
            public const string AuditTrial_Get = "usp_AuditTrial_Get";
            public const string LoginHistory_Update = "usp_LoginHistory_Update";
            public const string LoginHistory_GetLoggedInUsers = "usp_LoginHistory_GetLoggedInUsers";
        }


        #region Stored Proc



        #endregion

        #region Parameter Names



        #endregion

        #region Static Methods
        /// <summary>
        /// To log DML operations into the Database
        /// </summary>
        /// <param name="nUserID">User currently logged in</param>
        /// <param name="strDataBaseEntity">Entity modified</param>
        /// <param name="strEntityID">UniqueID of entity</param>
        /// <param name="cOperation">Type of operation</param>
        public static void LogOperations(int userID, string dataBaseEntity, string entityName, string operation)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = SqlHelper.CreateParameter(Params.UserID, userID, SqlDbType.Int, ParameterDirection.Input);
            sqlParams[1] = SqlHelper.CreateParameter(Params.DataBaseEntity, dataBaseEntity, SqlDbType.Text, ParameterDirection.Input);
            sqlParams[2] = SqlHelper.CreateParameter(Params.Operation, operation, SqlDbType.Char, ParameterDirection.Input);
            sqlParams[3] = SqlHelper.CreateParameter(Params.EntityName, entityName, SqlDbType.Text, ParameterDirection.Input);

            try
            {
                SqlHelper.ExecuteNonQuery(Utilites.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.AuditTrail_Save, sqlParams);
            }
            catch (System.Exception ex)
            {
                // throw new ExpoNet.Utils.ExpoNetException(ex.Message, "LogDML", "LogOperations");
            }
        }

        public static DataSet AuditTrailDetails(int UserID, DateTime FromDate, DateTime ToDate, int pageNum, int pageSize)
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = SqlHelper.CreateParameter(Params.UserID, UserID, SqlDbType.Int, ParameterDirection.Input);
            sqlParams[1] = SqlHelper.CreateParameter(Params.DateFrom, FromDate, SqlDbType.DateTime, ParameterDirection.Input);
            sqlParams[2] = SqlHelper.CreateParameter(Params.DateTo, ToDate, SqlDbType.DateTime, ParameterDirection.Input);
            sqlParams[3] = SqlHelper.CreateParameter(Params.PageNum, pageNum, SqlDbType.Int, ParameterDirection.Input);
            sqlParams[4] = SqlHelper.CreateParameter(Params.PageSize, pageSize, SqlDbType.Int, ParameterDirection.Input);

            try
            {
                ds = Utilites.SqlHelper.ExecuteDataset(Utilites.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.AuditTrial_Get, sqlParams);
            }
            catch (System.Exception ex)
            {
                //throw new ExpoNet.Utils.ExpoNetException(ex.Message, "LogDML", "AuditTrailDetails");
            }
            finally
            {
                sqlParams = null;
            }
            return ds;
        }

        public static void UpdateLoginHistory(int userId, char action)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
             SqlHelper.CreateParameter(Params.UserID, userId, SqlDbType.Int, ParameterDirection.Input),
             SqlHelper.CreateParameter(Params.ActionType, action, SqlDbType.Char, ParameterDirection.Input)
            };
            Utilites.SqlHelper.ExecuteNonQuery(Utilites.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.LoginHistory_Update, sqlParams);
        }

        public static DataSet GetUsers(bool getAll)
        {
            DataSet users = new DataSet();
            SqlParameter[] sqlParams = new SqlParameter[]
            {
             SqlHelper.CreateParameter(Params.GetAll,getAll , SqlDbType.Bit, ParameterDirection.Input)             
            };
            users = Utilites.SqlHelper.ExecuteDataset(Utilites.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.LoginHistory_GetLoggedInUsers, sqlParams);
            return users;
        }

        #endregion
    }
}
