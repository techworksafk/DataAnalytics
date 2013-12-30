using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MO = DAnalytics.MO;
using BL = DAnalytics.BL.Admin;
using System.Text;
using UTIL = DAnalytics.UTIL;
using System.Data;

namespace DAnalytics.Web
{
    /// <summary>
    /// Data Analytics  applications master page.
    /// </summary>
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        MO.Admin.MenuItemCollection menuItems = new MO.Admin.MenuItemCollection();

        string strMenuTree = string.Empty;

        protected int UserID
        {
            get
            {
                if (Session[UTIL.DAnalyticsKeys.USER] != null)
                {
                    return ((MO.Admin.DAnalyticsUser)Session[UTIL.DAnalyticsKeys.USER]).UserID;
                }
                else
                {
                    return 0;
                }
            }
        }

        protected string CurPage
        {
            get
            {
                string reqUrl = Request.Url.ToString();
                reqUrl = reqUrl.Substring(reqUrl.LastIndexOf("/") + 1, reqUrl.Length - (reqUrl.LastIndexOf("/") + 1));
                return reqUrl;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { PageInit(); }
            LoadMenu();
        }

        /// <summary>
        /// Handles click event of logout button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            UTIL.ActionLog.UpdateLoginHistory(UserID, UTIL.LoginAction.LOGOUT);
            Session.RemoveAll();
            string loginPage = ResolveUrl("~/SignIn.aspx");
            Response.Redirect(loginPage, true);
        }
        #region Methods

        /// <summary>
        /// Initialize method.
        /// </summary>
        void PageInit()
        {
            if (Session[UTIL.DAnalyticsKeys.USER] != null)
            {
                MO.Admin.DAnalyticsUser _user = (MO.Admin.DAnalyticsUser)Session[UTIL.DAnalyticsKeys.USER];
                spnUserName.InnerText = (_user != null) ? _user.UserName : "";
           
            }
        }

        /// <summary>
        /// Method to load menu.
        /// </summary>
        private void LoadMenu()
        {
            menuItems = BL.Admin.Common.GetMenuItems(this.UserID);
            MO.Admin.MenuItem rootMenu = new MO.Admin.MenuItem();
            rootMenu.MenuID = -1;
            strMenuTree = PopulateMenu(rootMenu) + "</ul>";
            topnav.InnerHtml = strMenuTree;
           
        }

        /// <summary>
        /// Method to populate menu.
        /// </summary>
        /// <param name="oParent"></param>
        /// <returns></returns>
        private string PopulateMenu(MO.Admin.MenuItem oParent)
        {

            StringBuilder MenuBuilder = new StringBuilder();
            List<MO.Admin.MenuItem> childMenuItems = new List<MO.Admin.MenuItem>();
            if (oParent.MenuID == -1)
            {
                MenuBuilder.Append("<ul id='nav'>");
            }
            childMenuItems = GetChildItems(oParent);
            if (menuItems.Count > 0)
            {
                foreach (DAnalytics.MO.Admin.MenuItem oItem in childMenuItems)
                {
                    string strUrl = "#";
                    if (oItem.ParentMenu == 0)
                    {
                        MenuBuilder.Append("<li><a href='#'>" + oItem.MenuName + "</a>");
                        MenuBuilder.Append("<ul class='sub'>");
                        MenuBuilder.Append(PopulateMenu(oItem));
                    }
                    else if ((oItem.ParentMenu > 0) && (HasChild(oItem)))
                    {
                        MenuBuilder.Append("<li><a href='#'>" + oItem.MenuName + "</a>");
                        MenuBuilder.Append("<ul class='sub_ul'>");
                        MenuBuilder.Append(PopulateMenu(oItem));
                    }
                    else
                    {
                        if (oItem.IsScreen == true)
                        {
                            strUrl = oItem.Url.Trim();
                        }
                        MenuBuilder.Append("<li><a href='" + ResolveUrl(strUrl) + "'>" + oItem.MenuName + "</a></li>");
                    }
                }
                MenuBuilder.Append("</ul>");
                MenuBuilder.Append("</li>");
            }

            return MenuBuilder.ToString();
        }

        /// <summary>
        /// Check for child menu
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        private bool HasChild(DAnalytics.MO.Admin.MenuItem oItem)
        {
            var ChildItems = from item in menuItems
                             where item.ParentMenu == oItem.MenuID
                             select item;
            if (ChildItems.Count() > 0)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// Get child items
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        public List<DAnalytics.MO.Admin.MenuItem> GetChildItems(DAnalytics.MO.Admin.MenuItem oItem)
        {
            List<MO.Admin.MenuItem> childList = new List<MO.Admin.MenuItem>();
            var childItems = from item in menuItems
                             where (item.ParentMenu == oItem.MenuID)
                             select item;
            if (oItem.MenuID == -1)
            {
                childItems = from item in menuItems
                             where (item.MenuLevel == 0)
                             select item;
            }
            if (childItems.Count() > 0)
            {
                childItems.OrderBy(item => item.SeqNo);
                childList = childItems.ToList();
            }
            return childList;
        }


        #endregion Methods

    }
}
