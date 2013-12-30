using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAnalytics.DA.Admin
{
    /// <summary>
    ///  Data Access Layer class for managing Application Environment Settings
    /// </summary>
    public class AppSetting
    {
        struct Params
        {
            public static string Key = "@Key";
        }

        struct Sprocs
        {
            public const string AppSetting_GetValue = "usp_AppSetting_GetValue";   
        }
        /// <summary>
        /// Get app setting value from DB
        /// </summary>
        /// <param name="keyName">Name of the key to fetch the value</param>
        /// <returns>Setting value</returns>
        public static string GetAppSettingValue(string keyName)
        {
            string keyValue = "";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.Key, keyName);
            keyValue = UTIL.SqlHelper.ExecuteScalar(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.AppSetting_GetValue, sqlParams).ToString();
            return keyValue;
        }
    
    }
}
