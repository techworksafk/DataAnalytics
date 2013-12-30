using System.Data;
using MO = DAnalytics.MO;
namespace DAnalytics.BL.Admin
{
    /// <summary>
    /// Business Layer class for managing User
    /// </summary>
    public class DAnalyticsUser
    {
        /// <summary>
        ///  Authenticates the Users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="userId"></param>
        /// <returns>return authentication status as boolean</returns>
        public static bool AuthenticateUser(string userName, string pwd, out int userId)
        {
            userId = 0;
            int status = DA.Admin.DAnalyticsUser.AuthenticateUser(userName, pwd);
            if (status > 0) { userId = status; return true; }
            else { return false; }
        }

        /// <summary>
        /// Saves the User
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        public static int Save(MO.Admin.DAnalyticsUser _user)
        {
            return DA.Admin.DAnalyticsUser.Save(_user);
        }
        
        /// <summary>
        /// Deletes the User
        /// </summary>
        /// <param name="_userId"></param>

        public static void Delete(int _userId)
        {
            DA.Admin.DAnalyticsUser.Delete(_userId);
        }

        /// <summary>
        /// Retrieves Users based on given criteria
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExp"></param>
        /// <param name="sortDir"></param>
        /// <param name="userName"></param>
        /// <param name="roleId"></param>
        /// <param name="isActive"></param>
        /// <param name="totalPages"></param>
        /// <returns>Return list of Users</returns>
        public static MO.Admin.DAnalyticsUsers GetUsers(int pageNum, int pageSize, string sortExp, string sortDir, string userName, int roleId, int isActive,
            out int totalPages)
        {
            return DA.Admin.DAnalyticsUser.GetUsers(pageNum, pageSize, sortExp, sortDir, userName, roleId, isActive, out totalPages);
        }

        /// <summary>
        /// Accepts userid and retrieves User object
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>returns User object</returns>
        public static MO.Admin.DAnalyticsUser GetUserByID(int _userId)
        {
            return DA.Admin.DAnalyticsUser.GetUserByID(_userId);
        }

        /// <summary>
        /// Checks whether User is admin or not
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>returns the boolean status</returns>
        public static bool IsAdmin(int _userId)
        {
            return DA.Admin.DAnalyticsUser.IsAdmin(_userId);
        }

      
        /// <summary>
        /// Retrieves password change status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns>return password change status as boolean</returns>
        public static bool ChangePassword(int userId, string pwd)
        {
            return DA.Admin.DAnalyticsUser.ChangePassword(userId, pwd);
        }

        /// <summary>
        /// Retrieves User Names
        /// </summary>
        /// <returns>Returns User Names</returns>
        public static DataSet GetUserNames()
        {
            return DA.Admin.DAnalyticsUser.GetUserNames();
        }

        /// <summary>
        /// Retrieves User by Rolecode
        /// </summary>
        /// <param name="roleCods"></param>
        /// <returns>Returns User</returns>
        public static DataTable GetUsersByRoleCode(string roleCods)
        {
            return DA.Admin.DAnalyticsUser.GetUsersByRoleCode(roleCods);
        }

     
        /// <summary>
        /// Sends forgotten password by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>returns User object</returns>
        public static MO.Admin.DAnalyticsUser ForgotPassword(string email)
        {
            return DA.Admin.DAnalyticsUser.ForgotPassword(email);
        }
    }
}
