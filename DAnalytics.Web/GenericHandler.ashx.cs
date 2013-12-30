using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAnalytics.Web
{
    /// <summary>
    /// Handling the asynchronous requests in the application
    /// </summary>
    public class GenericHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

            
        private void GetLoggedInUsers(HttpContext context)
        {
            DataSet dsUsers = UTIL.ActionLog.GetUsers(false);
            System.Text.StringBuilder userList = new System.Text.StringBuilder();
            if (dsUsers != null && dsUsers.Tables[0].Rows.Count > 0)
            {
                userList.Append("<ul>");
                foreach (DataRow dr in dsUsers.Tables[0].Rows)
                {
                    userList.Append("<li><span class='staff'>");
                    userList.Append(dr["UserName"].ToString());
                    userList.Append("</span></li>");
                }
                userList.Append("</ul>");
            }
            context.Response.Write(userList.ToString());
        }

        private void GetAllLoggedInUsers(HttpContext context)
        {
            int totalUsers = 0;
            int cols = 0;
            int rows = 0;
            int cnt = 1;
            int rowspercol = 15;
            DataSet dsUsers = UTIL.ActionLog.GetUsers(true);
            System.Text.StringBuilder userList = new System.Text.StringBuilder();

            if (dsUsers != null && dsUsers.Tables[0].Rows.Count > 0)
            {
                totalUsers = dsUsers.Tables[0].Rows.Count;
                cols = totalUsers / rowspercol;
                if ((totalUsers % rowspercol) > 0)
                {
                    cols += 1;
                }
                string widthClass = "coldiv" + cols.ToString();
                while (cols > 0)
                {
                    userList.Append("<div class='col_div " + widthClass + "'>");
                    userList.Append("<ul>");
                    rows = 1;

                    foreach (DataRow dr in dsUsers.Tables[0].Rows)
                    {
                        if (cnt == Convert.ToInt32(dr["RowCnt"]))
                        {
                            userList.Append("<li>");
                            userList.Append(dr["UserName"].ToString());
                            userList.Append("</li>");
                            rows += 1;
                            cnt += 1;
                        }

                        if (rows > rowspercol)
                            break;
                    }
                    userList.Append("</ul>");
                    userList.Append("</div>");
                    cols -= 1;
                }
            }
            userList.Append("<div class='cls'></div>");
            context.Response.Write(userList.ToString());
        }

    }
}
