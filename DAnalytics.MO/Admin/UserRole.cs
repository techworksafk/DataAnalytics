using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.MO.Admin
{   
    /// <summary>
    /// Model Layer class which defines User Role  
    /// </summary>
    public class UserRole
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemDefined { get; set; }

        public bool HasFullRights { get; set; }

 
        public List<RolePermission> RolePermissions { get; set; }

        public UserRole()
        {
        }

        public UserRole(int _roleId)
        {
            this.RoleID = _roleId;
        }
    }

    public class DAnalyticsUserRoles : List<UserRole>
    {
    }
}
