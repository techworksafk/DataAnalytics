using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model = DAnalytics.MO.Admin;
using UTIL = DAnalytics.UTIL;

namespace DAnalytics.DA.Admin
{
    /// <summary>
    ///  Data Access Layer class for managing Common entities such as Menu 
    /// </summary>
    public class Common
    {
        #region Stored Procedures

        private const string USP_GET_MENUCOLLECTION = "usp_Adm_MenuTree";
        private const string USP_GET_ACCESSRIGHTS = "usp_Adm_ScreenPermissions_Get";

        #endregion

        #region Parameters 
      
        private const string PARAM_USERID = "@UserId";

        private const string PARAM_MENUID = "@MenuID";

        #endregion

        #region Static Methods
       
        /// <summary>
        /// Get menu items
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Menu collection class object</returns>
        public static Model.MenuItemCollection GetMenuItems(Int32 userId)
        {
            SqlParameter[] oSqlParams = new SqlParameter[1];
            oSqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, PARAM_USERID, userId);
            Model.MenuItemCollection menuItems = new Model.MenuItemCollection();
            using (IDataReader idr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, USP_GET_MENUCOLLECTION,
                oSqlParams))
            {
                while (idr.Read())
                {
                    menuItems.Add(new Model.MenuItem
                    {
                        MenuID = Convert.ToInt32(idr["MenuID"]),
                        MenuName = Convert.ToString(idr["MenuName"]),
                        ParentMenu = Convert.ToInt32(idr["ParentMenu"]),
                        SeqNo = Convert.ToInt32(idr["SeqNo"]),
                        IsScreen = Convert.ToBoolean(idr["IsScreen"]),
                        MenuLevel = Convert.ToInt32(idr["MenuLevel"]),
                        Url = Convert.ToString(idr["Url"])
                    });
                }
            };
            return menuItems;
        }

        /// <summary>
        /// Get access rights
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="menuId">Menu ID</param>
        /// <returns>Access rights value</returns>
        public static string AccessRights(Int32 userId, Int32 menuId)
        {
            string rights = string.Empty;
            SqlParameter[] oSqlParams = new SqlParameter[2];
            oSqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, PARAM_USERID, userId);
            oSqlParams[1] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, PARAM_MENUID, menuId);          

            using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, USP_GET_ACCESSRIGHTS, oSqlParams))
            {
                while (rdr.Read())
                {
                    rights = Convert.ToString(rdr["AccessRights"]);
                }
            };
            return rights;
        }

        #endregion Static Methods
    }
}
