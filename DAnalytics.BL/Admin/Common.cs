using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model=DAnalytics.MO.Admin;
using DAnalytics.DA.Admin;
using System.Data;

namespace DAnalytics.BL.Admin
{
    /// <summary>
    ///  Business Layer class for managing common entities such as Menu and AccessRights
    /// </summary>
   public class Common
    {      
        /// <summary>
        /// accepts UserID and returns MenuItems collection which user has access to
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns MenuItems collection </returns>
        public static Model.MenuItemCollection GetMenuItems(Int32 userId)
        {
            return DAnalytics.DA.Admin.Common.GetMenuItems(userId);            
        }
       /// <summary>
       /// accepts UserID and MenuID and returns access rights as string
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="menuId"></param>
        /// <returns>returns access rights as string</returns>

        public static string AccessRights(Int32 userId, Int32 menuId)
        {
            return DA.Admin.Common.AccessRights(userId, menuId);
        }
    }
}
