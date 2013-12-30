using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MO = DAnalytics.MO;

namespace DAnalytics.DA.Admin
{
    /// <summary>
    /// Data Access Layer class for managing User Roles
    /// </summary>
    public class UserRole
    {
        struct Params
        {
            public static string RetValue = "@ReturnValue";
            public static string RoleID = "@RoleID";
            public static string RoleName = "@RoleName";
            public static string Role_Description = "@Description";
            public static string Role_Status = "@IsActive";
            public static string Role_HasFullRights = "@HasFullRights";
            public static string Role_Permissions = "@Permissions";
            public static string Role_CanEditClaim = "@CanEditClaim";
        }

        struct Sprocs
        {
            public const string Role_Save = "usp_Adm_Role_Save";
            public const string Role_Delete = "usp_Adm_Role_Delete";
            public const string Role_Get = "usp_Adm_Role_Get";
            public const string Role_GetPermissions = "usp_Adm_Role_Permissions_Get";
            public const string Role_CheckForUsers = "usp_Adm_Role_CheckForUsers";            
        }

        /// <summary>
        /// Save role
        /// </summary>
        /// <param name="_userRole">User role class object</param>
        /// <returns>ID of the updated role</returns>
        public static int Save(MO.Admin.UserRole _userRole)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];           

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.RoleID, _userRole.RoleID);

            sqlParams[1] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.RoleName, _userRole.RoleName);

            sqlParams[2] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.Role_Description, _userRole.Description);

            sqlParams[3] = UTIL.SqlHelper.CreateParameter(SqlDbType.Bit, ParameterDirection.Input, Params.Role_Status, _userRole.IsActive);

            sqlParams[4] = UTIL.SqlHelper.CreateParameter(SqlDbType.Bit, ParameterDirection.Input, Params.Role_HasFullRights, _userRole.HasFullRights);

            sqlParams[5] = UTIL.SqlHelper.CreateParameter(Params.Role_Permissions, UTIL.DAnalHelper.Serialize(_userRole.RolePermissions), SqlDbType.NText, ParameterDirection.Input);

          return (UTIL.DAnalHelper.ConvertToInt32(UTIL.SqlHelper.ExecuteScalar(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure,
                Sprocs.Role_Save, sqlParams).ToString()));            
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="_roleId">Role ID</param>
        public static void Delete(int _roleId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.RoleID, _roleId);

            UTIL.SqlHelper.ExecuteNonQuery(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.Role_Delete, sqlParams);
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns>UserRole collection class object</returns>
        public static MO.Admin.DAnalyticsUserRoles GetRoles()
        {
            MO.Admin.DAnalyticsUserRoles _userRoles = new MO.Admin.DAnalyticsUserRoles();
            using (SqlDataReader reader = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.Role_Get, null))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _userRoles.Add(new MO.Admin.UserRole
                        {
                            RoleID = UTIL.DAnalHelper.ConvertToInt32(reader["RoleID"]),
                            RoleName = UTIL.DAnalHelper.ConvertToString(reader["RoleName"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            IsSystemDefined = Convert.ToBoolean(reader["IsSystemDefined"])
                        });
                    }
                }
            };
            return _userRoles;
        }

        /// <summary>
        /// Get role
        /// </summary>
        /// <param name="_roleId">Role ID</param>
        /// <returns>UserRole class object</returns>
        public static MO.Admin.UserRole GetRole(int _roleId)
        {
            MO.Admin.UserRole _userRole = null;

            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.RoleID, _roleId);

            using (SqlDataReader reader = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.Role_Get, sqlParams))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _userRole = new MO.Admin.UserRole
                        {
                            RoleID = UTIL.DAnalHelper.ConvertToInt32(reader["RoleID"]),
                            RoleName = UTIL.DAnalHelper.ConvertToString(reader["RoleName"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }
                }
            };
            return _userRole;
        }

        /// <summary>
        /// Get role permissions
        /// </summary>
        /// <param name="roleId">Role ID</param>
        /// <returns>Dataset with user roles</returns>
        public static DataSet GetRolePermissions(int roleId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.RoleID, roleId);
            ds = UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.Role_GetPermissions, sqlParams);
            return ds;         
        }

        /// <summary>
        /// Check if role is mapped to users
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>Boolean value indicating whether role is mapped to users</returns>
        public static bool CheckForUsers(int roleId)
        {
            bool groupExists = false;
            SqlParameter[] oSqlParams = new SqlParameter[1];
            oSqlParams[0] = UTIL.SqlHelper.CreateParameter(Params.RoleID, roleId, SqlDbType.Int, ParameterDirection.Input);
            groupExists = Convert.ToBoolean(UTIL.SqlHelper.ExecuteScalar(UTIL.DAnalHelper.ConnectionString,
                    CommandType.StoredProcedure, Sprocs.Role_CheckForUsers, oSqlParams));            
            return groupExists;
        }
    }
}
