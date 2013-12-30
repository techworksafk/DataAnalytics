using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.BL.Admin
{
    /// <summary>
    /// Business Layer class for managing Application Environment Settings
    /// </summary>
    public class AppSetting
    {

        /// <summary>
        /// Accepts Appsetting Key and returns the associated value
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns>Returns the Appsetting value as string</returns>
        public static string GetAppSettingValue(string keyName)
        {
            return DA.Admin.AppSetting.GetAppSettingValue(keyName);
        }
               
    }
}
