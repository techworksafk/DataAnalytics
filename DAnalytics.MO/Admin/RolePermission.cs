using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.MO.Admin
{
    /// <summary>
    /// Model Layer class which defines Roles Permission  
    /// </summary>
    public class RolePermission
    {
        public int RoleMappingId { get; set; }

        public int RoleID { get; set; }

        public int ScreenID { get; set; }

        public bool ViewRights { get; set; }

        public bool AddRights { get; set; }

        public bool UpdateRights { get; set; }

        public bool DeleteRights { get; set; }

        public bool PrintRights { get; set; }

        public string ScreenName { get; set; }

        public int ParentMenu { get; set; }

        public int MenuLevel { get; set; }

        public bool IsScreen { get; set; }
    }

  
}
