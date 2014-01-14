using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace DAnalytics.Web.template
{
    public partial class tmpl_CH4Max : System.Web.UI.UserControl
    {
        public tmpl_CH4Max()
        {
            ID = Guid.NewGuid().ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        List<PlotValue> _PlotValues = new List<PlotValue>();

        public List<PlotValue> PlotValues
        {
            get { return _PlotValues; }
            set { _PlotValues = value; }
        }

        private string GetControlString()
        {
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            this.RenderControl(htmlWrite);
            return stringWrite.ToString();
        }

        StringBuilder _CH4 = new StringBuilder();
        StringBuilder _Category = new StringBuilder();

        public string CH4 { get { return _CH4.ToString(); } }
        public string Category { get { return _Category.ToString(); } }
        public bool HasPlotted { get; set; }

        public string PlotGraph()
        {
            for (int iCount = 0; iCount < _PlotValues.Count; iCount++)
            {
                PlotValue _obj = _PlotValues[iCount];

                if (_obj.CH4.ToUpper() != "NA")
                {
                    if (_Category.ToString().Length > 0 && !_Category.ToString().EndsWith(","))
                    {
                        _Category.Append(",");
                        _CH4.Append(",");
                    }
                    _Category.Append("'").Append(_obj.BoreHoleName).Append("'");
                    _CH4.Append(_obj.CH4);
                }
            }
            HasPlotted = true;
            return GetControlString();
        }
    }

    public class PlotValue
    {
        public string BoreHoleName { get; set; }
        public string CH4 { get; set; }
    }
}