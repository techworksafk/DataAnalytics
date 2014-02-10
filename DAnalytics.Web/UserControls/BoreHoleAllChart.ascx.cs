using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAnalytics.Web.UserControls
{
    public partial class BoreHoleAllChart : System.Web.UI.UserControl
    {
        public int BoreHoleID { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}