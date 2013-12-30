using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAnalytics.MO.Admin
{
    /// <summary>
    /// Model Layer class which defines MenuItem  
    /// </summary>
    public class MenuItem
    {
        public int MenuID{get;set;}

        public string MenuName{get;set;}        

        public int ParentMenu{get;set;}        

        public int SeqNo{get;set;}        

        public bool IsScreen{get;set;}       

        public int RoleID{get;set;}        

        public int MenuLevel{get;set;}        

        public string Url{get;set;}
       
    }

    public class MenuItemCollection : List<MenuItem>
    {
    }
}
