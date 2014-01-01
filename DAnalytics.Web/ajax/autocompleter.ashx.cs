using System;
using System.Collections.Generic;
using System.Web;

namespace DAnalytics.Web.ajax
{
    /// <summary>
    /// Summary description for autocompleter1
    /// </summary>
    public class autocompleter1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request.QueryString["act"].ToLower())
            {
                case "searchborehole":
                    SearchBorehole(context);
                    break;
            }
        }


        private void SearchBorehole(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["q"])) //  'q' is the query string from the autosuggest 
            {
                List<DAnalytics.MO.Borehole> lst = BL.Report.DailyReport.SearchBorehole(Convert.ToString(context.Request.QueryString["q"]));

                if (lst.Count > 0)
                {
                    context.Response.Write("{\"BoreHoles\" : [");
                    try
                    {

                        for (int cnt = 0; cnt < lst.Count; cnt++)
                        {
                            DAnalytics.MO.Borehole _obj = lst[cnt];
                            context.Response.Write("{ \"BoreHoleName\" : \"" + _obj.BoreHoleName +
                                            "\", \"AreaName\" : \"" + _obj.AreaName +
                                            "\", \"BoreholeType\" : \"" + _obj.BoreholeType +
                                            "\", \"Depth\" : \"" + _obj.Depth +
                                            "\", \"BoreHoleID\" : \"" + _obj.BoreHoleID.ToString() +
                                            "\" }");
                            if (cnt != lst.Count - 1) { context.Response.Write(","); }
                        }
                    }
                    catch { }
                    context.Response.Write("]}");
                }
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}