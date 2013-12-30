using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.MO.Admin
{
    /// <summary>
    /// Model Layer class which defines User  
    /// </summary>
    public class DAnalyticsUser
    {
        public int UserID { get; set; }
      
        public string FirstName { get; set; }

        public string DeptName { get; set; }

        public string StateName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }               

        public bool IsActive { get; set; }

        public string LastLoginDate { get; set; }

        public List<UserRoleMapping> UserRoleMappings { get; set; }

        public DAnalyticsUser(int _userId) { this.UserID = _userId; }

        public DAnalyticsUser() { }
    }

    public class DAnalyticsUsers:List<DAnalyticsUser>    {    }

    public class UserRoleMapping
    {        

        public int UserID { get; set; }

        public int RoleID { get; set; }

        public string RoleCode { get; set; }
    }
}
