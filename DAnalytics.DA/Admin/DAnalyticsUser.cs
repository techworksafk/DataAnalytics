using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MO = DAnalytics.MO;
namespace DAnalytics.DA.Admin
{
    /// <summary>
    /// Database parameters
    /// </summary>
    struct Params
    {
        public static string USERID = "@UserID";

        public static string UserName = "@UserName";

        public static string Password = "@Password";

        public static string RetValue = "@RetValue";

        public static string User_FirstName = "@FirstName";

        public static string User_LastName = "@LastName";

        public static string User_RoleID = "@RoleID";

        public static string User_Email = "@Email";

        public static string User_Mobile = "@Mobile";

        public static string User_Status = "@IsActive";

        public static string User_PageNum = "@PageNum";

        public static string User_PageSize = "@PageSize";

        public static string User_SortExp = "@SortExpression";

        public static string User_SortDir = "@SortDirection";

        public static string User_TotalPages = "@TotalPages";

        public static string User_NewPwd = "@NewPwd";

        public static string User_Roles = "@Roles";
        
        public static string User_RoleCode = "@RoleCode";

        public static string User_RoleCodes = "@RoleCodes";
    }

    /// <summary>
    /// Stored procedures
    /// </summary>
    struct Sprocs
    {
        public const string User_Authenticate = "usp_User_Authenticate";

        public const string User_Save = "usp_Adm_User_Save";

        public const string User_Delete = "usp_Adm_User_Delete";

        public const string User_Get = "usp_Adm_User_Get";

        public const string Adm_User_GetUserNames = "usp_Adm_User_GetUserNames";

        public const string User_GetByID = "usp_Adm_User_GetByID";

        public const string usp_Adm_RoleCode_Get = "usp_Adm_RoleCode_Get";

        public const string User_ChangePwd = "usp_Adm_User_ChangePassword";

        public const string User_GetByRoleCode = "usp_Adm_User_GetByRoleCode";

        public const string User_Image_Get = "usp_User_Image_Get";

        public const string User_ForgotPassword = "usp_User_ForgotPassword";
    }

    /// <summary>
    /// Data Access Layer class for managing Users
    /// </summary>
    public class DAnalyticsUser
    {
        public static int AuthenticateUser(string userName, string pwd)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.UserName, userName);
            sqlParams[1] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.Password, UTIL.CryptorEngine.Encrypt(pwd, true));
            return UTIL.DAnalHelper.ConvertToInt32(UTIL.SqlHelper.ExecuteScalar(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.User_Authenticate, sqlParams));
        }
        

        /// <summary>
        /// Data access method to change User password
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="pwd">New Password</param>
        /// <returns>Boolean value indicating whether password change is successful</returns>

        public static bool ChangePassword(int userId, string pwd)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, Params.USERID, userId);
            sqlParams[1] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_NewPwd,
                UTIL.CryptorEngine.Encrypt(pwd, true));
            try
            {
                UTIL.SqlHelper.ExecuteNonQuery(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure,
                    Sprocs.User_ChangePwd, sqlParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saves user
        /// </summary>
        /// <param name="_user">DAnalyticsUser class object</param>
        /// <returns>ID of updated user</returns>
        public static Int32 Save(MO.Admin.DAnalyticsUser _user)
        {
            SqlParameter[] sqlParams = new SqlParameter[17];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, Params.USERID, _user.UserID);

            sqlParams[1] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.UserName, _user.UserName);

            sqlParams[2] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.Password, _user.UserPassword);

            sqlParams[3] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_FirstName, _user.FirstName);

            sqlParams[4] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_LastName, _user.LastName);

            sqlParams[5] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_Email, _user.Email);

            sqlParams[6] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_Mobile, _user.Mobile);

            sqlParams[7] = UTIL.SqlHelper.CreateParameter(SqlDbType.Bit, ParameterDirection.Input, Params.User_Status, Convert.ToBoolean(_user.IsActive));

            sqlParams[8] = UTIL.SqlHelper.CreateParameter(Params.User_Roles, UTIL.DAnalHelper.Serialize(_user.UserRoleMappings), SqlDbType.VarChar,
                ParameterDirection.Input, 8000);

            return Convert.ToInt32(UTIL.SqlHelper.ExecuteScalar(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure,
                Sprocs.User_Save, sqlParams).ToString());
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="_userId">User ID</param>
        public static void Delete(int _userId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, Params.USERID, _userId);

            UTIL.SqlHelper.ExecuteNonQuery(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.User_Delete, sqlParams);
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="pageNum">Selected Page Number</param>
        /// <param name="pageSize">Selected Page Size</param>
        /// <param name="sortDir">Sort direction</param>
        /// <param name="sortExp">Sort expression</param>
        /// <param name="userName">User name search string</param>
        /// <param name="roleId">Role ID filter</param>
        /// <param name="isActive">Active filter</param>
        /// <param name="totalPages">Total pages</param>
        /// <returns>User collection class object</returns>
        public static MO.Admin.DAnalyticsUsers GetUsers(int pageNum, int pageSize, string sortExp, string sortDir, string userName, int roleId, int isActive, out int totalPages)
        {
            MO.Admin.DAnalyticsUsers _users = new MO.Admin.DAnalyticsUsers();

            totalPages = 0;

            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.User_PageNum, pageNum);

            sqlParams[1] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.User_PageSize, pageSize);

            sqlParams[2] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_SortExp, sortExp);

            sqlParams[3] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.User_SortDir, sortDir);

            sqlParams[4] = UTIL.SqlHelper.CreateParameter(SqlDbType.VarChar, ParameterDirection.Input, Params.UserName, userName);

            sqlParams[5] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.User_RoleID, roleId);

            sqlParams[6] = UTIL.SqlHelper.CreateParameter(SqlDbType.Int, ParameterDirection.Input, Params.User_Status, isActive);

            using (SqlDataReader reader = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.User_Get, sqlParams))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _users.Add(new MO.Admin.DAnalyticsUser
                        {
                            UserID = UTIL.DAnalHelper.ConvertToInt32(reader["UserID"]),
                            UserName = UTIL.DAnalHelper.ConvertToString(reader["UserName"]),
                            UserPassword = UTIL.DAnalHelper.ConvertToString(reader["UserPassword"]),
                            FirstName = UTIL.DAnalHelper.ConvertToString(reader["FirstName"]),
                            LastName = UTIL.DAnalHelper.ConvertToString(reader["LastName"]),                       
                            Email = UTIL.DAnalHelper.ConvertToString(reader["Email"]),
                            Mobile = UTIL.DAnalHelper.ConvertToString(reader["Mobile"]),                      
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        });
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            totalPages = UTIL.DAnalHelper.ConvertToInt32(reader["TotalPages"]);
                        }
                    }
                }
            };
            return _users;
        }

        /// <summary>
        /// Get single user
        /// </summary>
        /// <param name="_userId">User ID</param>
        /// <returns>DAnalyticsUser class object</returns>
        public static MO.Admin.DAnalyticsUser GetUserByID(int _userId)
        {
            MO.Admin.DAnalyticsUser _user = null;

            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter(SqlDbType.BigInt, ParameterDirection.Input, Params.USERID, _userId);

            using (SqlDataReader reader = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.User_GetByID, sqlParams))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _user = new MO.Admin.DAnalyticsUser();
                        _user.UserID = UTIL.DAnalHelper.ConvertToInt32(reader["UserID"]);
                        _user.UserName = UTIL.DAnalHelper.ConvertToString(reader["UserName"]);
                        _user.UserPassword = UTIL.DAnalHelper.ConvertToString(reader["UserPassword"]);
                        _user.FirstName = UTIL.DAnalHelper.ConvertToString(reader["FirstName"]);
                        _user.LastName = UTIL.DAnalHelper.ConvertToString(reader["LastName"]);               
                        _user.Email = UTIL.DAnalHelper.ConvertToString(reader["Email"]);
                        _user.Mobile = UTIL.DAnalHelper.ConvertToString(reader["Mobile"]);                    
                        _user.IsActive = Convert.ToBoolean(reader["IsActive"]);                    
                        _user.LastLoginDate = UTIL.DAnalHelper.ConvertToString(reader["LastLoginDate"]);

                        _user.UserRoleMappings = GetRoles(reader);
                    }
                }
            };
            return _user;
        }

        /// <summary>
        /// Get role mapping for selected user 
        /// </summary>
        /// <param name="rdr">Sqlreader containing user data</param>
        /// <returns>UserRoleMapping collection class object</returns>
        private static List<DAnalytics.MO.Admin.UserRoleMapping> GetRoles(SqlDataReader rdr)
        {
            List<DAnalytics.MO.Admin.UserRoleMapping> roles = new List<MO.Admin.UserRoleMapping>();
            DAnalytics.MO.Admin.UserRoleMapping role;
            if (rdr.NextResult())
            {
                while (rdr.Read())
                {
                    role = new MO.Admin.UserRoleMapping
                            {
                                UserID = UTIL.DAnalHelper.ConvertToInt32(rdr["UserID"]),
                                RoleID = UTIL.DAnalHelper.ConvertToInt32(rdr["RoleID"]),
                                RoleCode = UTIL.DAnalHelper.ConvertToString(rdr["RoleCode"])                               
                            };
                    roles.Add(role);
                }
            }
            return roles;
        }

        /// <summary>
        /// Check if user is admin
        /// </summary>
        /// <param name="_userId">User ID</param>
        /// <returns>Boolean value indicating whether user is admin</returns>
        public static bool IsAdmin(int _userId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = UTIL.SqlHelper.CreateParameter(Params.USERID, _userId, SqlDbType.Int, ParameterDirection.Input);
            using (SqlDataReader oReader = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.usp_Adm_RoleCode_Get, sqlParams))
            {
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (Convert.ToString(oReader["RoleCode"]).Equals("ADM"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

   
        /// <summary>
        /// Get user names
        /// </summary>
        /// <returns>Dataset of all usernames</returns>
        public static DataSet GetUserNames()
        {
            return UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.Adm_User_GetUserNames, null);
        }

        /// <summary>
        /// Get users bu role code
        /// </summary>
        /// <param name="roleCods">Role code</param>
        /// <returns>All users mapped to the role code</returns>
        public static DataTable GetUsersByRoleCode(string roleCods)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{
                 UTIL.SqlHelper.CreateParameter(Params.User_RoleCodes, roleCods, SqlDbType.VarChar, ParameterDirection.Input)
            };
            return UTIL.SqlHelper.ExecuteDataset(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.User_GetByRoleCode, sqlParams).Tables[0];
        }

        /// <summary>
        /// Get user details for forgot password
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User class object of the specific user</returns>
        public static MO.Admin.DAnalyticsUser ForgotPassword(string email)
        {
            int userId = 0;
            MO.Admin.DAnalyticsUser user = null;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = UTIL.SqlHelper.CreateParameter(Params.User_Email, email, SqlDbType.VarChar, ParameterDirection.Input);
            using (SqlDataReader rdr = UTIL.SqlHelper.ExecuteReader(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, Sprocs.User_ForgotPassword, sqlParams))
            {
                if (rdr.Read())
                {
                    userId = Convert.ToInt32(rdr["UserID"]);

                    if (userId > 0)
                    {
                        user = new MO.Admin.DAnalyticsUser();
                        user.UserID = userId;
                        user.Email = Convert.ToString(rdr["Email"]);
                        user.FirstName = Convert.ToString(rdr["FirstName"]);
                        user.LastName = Convert.ToString(rdr["LastName"]);
                        user.UserName = Convert.ToString(rdr["UserName"]);
                        user.UserPassword = UTIL.CryptorEngine.Decrypt(Convert.ToString(rdr["UserPassword"]), true);
                    }
                }
            }
            return user;
        }
    }
}
