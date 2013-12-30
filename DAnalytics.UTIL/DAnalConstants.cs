using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.UTIL
{
    /// <summary>
    /// Utility Layer class for managing Reporting 
    /// </summary>
    public class SSRS
    {
        public const string REPORT_SERVER_PATH = "ReportServerPath";
        public const string REPORT_PATH = "ReportPath";
        public const string REPORT_SERVER_USER_NAME = "ReportServerUserName";
        public const string REPORT_SERVER_PASSWORD = "ReportServerPassword";
        public const string REPORT_SERVER_DOMAIN = "ReportServerDomain";
    }

    public class DAnalyticsConfig
    {
        public const string CONNECTION_STRING_KEY = "DAnalyticsCon";
        public const string RESOURCE_NAME = "";
    }

    public class DAnalyticsKeys
    {
        public const string USERID = "UserID";
        public const string EXCEPTION = "ExceptionDetail";
        public const string RIGHTS = "UserRights";
        public const string USER = "User";          
    }

    public class DMLOperations
    {
        public const string ADDED = "A";
        public const string UPDATED = "U";
        public const string DELETED = "D";
        public const string PRINTED = "P";
        public const string CANCELED = "C";
        public const string SAVED = "S";
        public const string ASSIGNED = "AS";       
    }

    public class LoginAction
    {
        public const char LOGIN = 'I';
        public const char LOGOUT = 'O';
    }

    public class UserRole
    {
        public const string ENGINEER = "ENG";
        public const string ADMINISTRATOR = "ADM";     
    }
}
