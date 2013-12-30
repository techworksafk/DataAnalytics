using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.MO.Admin
{   
    /// <summary>
    /// Model Layer class which defines attributes and methods of eachScreen
    /// </summary>
    public class Screen
    {
        public enum Category
        {
            Add,
            Edit,
            Delete,
            View,
            Print
        }

        public int ScreenID { get; set; }

        public string ScreenUrl { get; set; }

        public string FunctionName { get; set; }

        public string HandlerID { get; set; }

        public Category RoleCategory { get; set; }

        public object Handler { get; set; }

    }

    public class Screens : System.Collections.Generic.List<Screen>
    {
    }
}
