using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MO = DAnalytics.MO;

namespace DAnalytics.BL.Admin
{
    /// <summary>
    ///  Business Layer class for managing UserRole
    /// </summary>
    public class UserRole
    {
        static DataSet dsRoles;
        static DataSet dsRolesFormatted;

        /// <summary>
        /// Saves the UserRole
        /// </summary>
        /// <param name="_userRole"></param>
        /// <returns>returns the sve status</returns>
        public static int Save(MO.Admin.UserRole _userRole)
        {
            return DA.Admin.UserRole.Save(_userRole);
        }

        /// <summary>
        /// Deletes the UserRole based on ID
        /// </summary>
        /// <param name="_roleId"></param>
        public static void Delete(int _roleId)
        {
            DA.Admin.UserRole.Delete(_roleId);
        }

        /// <summary>
        /// Retrieves the list of User Roles 
        /// </summary>
        /// <returns>Return DAnalyticsUserRoles collection</returns>
        public static MO.Admin.DAnalyticsUserRoles GetRoles()
        {
            return DA.Admin.UserRole.GetRoles();
        }

        /// <summary>
        /// Retrieves Role object based on RoleId.
        /// </summary>
        /// <param name="_roleId"></param>
        /// <returns></returns>
        public static MO.Admin.UserRole GetRole(int _roleId)
        {
            return DA.Admin.UserRole.GetRole(_roleId);
        }

        /// <summary>
        /// Retrieves Role Permissions based on RoleID
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Returns Role Permission</returns>
        public static DataSet GetRolePermissions(int roleId)
        {
            dsRoles = DA.Admin.UserRole.GetRolePermissions(roleId);

            dsRolesFormatted = dsRoles.Clone();

            foreach (DataRow dr in dsRoles.Tables[0].Select("ParentMenu = 0"))
            {
                DataRow drFormatted = dsRolesFormatted.Tables[0].NewRow();

                drFormatted["MenuID"] = dr["MenuID"];
                drFormatted["MenuName"] = dr["MenuName"];
                drFormatted["LEVEL"] = dr["LEVEL"];
                drFormatted["ParentMenu"] = dr["ParentMenu"];
                drFormatted["IsScreen"] = dr["IsScreen"];
                drFormatted["RoleName"] = dr["RoleName"];
                drFormatted["RoleID"] = dr["RoleID"];
                drFormatted["IsActive"] = dr["IsActive"];
                drFormatted["HasFullRights"] = dr["HasFullRights"];
                drFormatted["Description"] = dr["Description"];
                drFormatted["IsSystemDefined"] = dr["IsSystemDefined"];
                drFormatted["AddRights"] = dr["AddRights"];
                drFormatted["DeleteRights"] = dr["DeleteRights"];
                drFormatted["PrintRights"] = dr["PrintRights"];
                drFormatted["UpdateRights"] = dr["UpdateRights"];
                drFormatted["ViewRights"] = dr["ViewRights"];
                drFormatted["ScreenID"] = dr["ScreenID"];


                dsRolesFormatted.Tables[0].Rows.Add(drFormatted);

                FormatRoleDataGrid(Convert.ToInt32(dr["MenuID"]));
            }

            return dsRolesFormatted;
        }

        /// <summary>
        /// Formats the Roles Grid based on MenuID
        /// </summary>
        /// <param name="MenuID"></param>
        static void FormatRoleDataGrid(int MenuID)
        {
            foreach (DataRow dr in dsRoles.Tables[0].Select("ParentMenu = " + MenuID.ToString()))
            {
                DataRow drFormatted = dsRolesFormatted.Tables[0].NewRow();

                drFormatted["MenuID"] = dr["MenuID"];
                drFormatted["MenuName"] = dr["MenuName"];
                drFormatted["LEVEL"] = dr["LEVEL"];
                drFormatted["ParentMenu"] = dr["ParentMenu"];
                drFormatted["IsScreen"] = dr["IsScreen"];
                drFormatted["RoleName"] = dr["RoleName"];
                drFormatted["RoleID"] = dr["RoleID"];
                drFormatted["IsActive"] = dr["IsActive"];
                drFormatted["HasFullRights"] = dr["HasFullRights"];             
                drFormatted["Description"] = dr["Description"];
                drFormatted["IsSystemDefined"] = dr["IsSystemDefined"];
                drFormatted["AddRights"] = dr["AddRights"];
                drFormatted["DeleteRights"] = dr["DeleteRights"];
                drFormatted["PrintRights"] = dr["PrintRights"];
                drFormatted["UpdateRights"] = dr["UpdateRights"];
                drFormatted["ViewRights"] = dr["ViewRights"];
                drFormatted["ScreenID"] = dr["ScreenID"];

                dsRolesFormatted.Tables[0].Rows.Add(drFormatted);

                FormatRoleDataGrid(Convert.ToInt32(dr["MenuID"]));
            }
        }


        /// <summary>
        /// Checks for Users based on given RoleID.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool CheckForUsers(int roleId)
        {
            return DA.Admin.UserRole.CheckForUsers(roleId);
        }
    }
}
