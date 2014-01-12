using System;
using System.Data;
using System.Text;
using System.Web.UI;

namespace DAnalytics.Web.template
{
    public partial class tmpl_BoreHoleAttributes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private string GetControlString()
        {
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            this.RenderControl(htmlWrite);
            return stringWrite.ToString();
        }

        public int BoreHoleID { get; set; }

        public DataSet DataSource { get; set; }

        StringBuilder _CH4 = new StringBuilder();
        StringBuilder _CO2 = new StringBuilder();
        StringBuilder _O2 = new StringBuilder();
        StringBuilder _VOC = new StringBuilder();
        StringBuilder _H2S = new StringBuilder();
        StringBuilder _CO = new StringBuilder();
        StringBuilder _Borehole_Pressure = new StringBuilder();
        StringBuilder _Atmospheric_Pressure = new StringBuilder();
        StringBuilder _Pressure_Diff = new StringBuilder();
        StringBuilder _Temperature = new StringBuilder();
        StringBuilder _Water_Level = new StringBuilder();
        StringBuilder _Battery = new StringBuilder();

        StringBuilder _Category = new StringBuilder();

        public string CH4 { get { return _CH4.ToString(); } }
        public string CO2 { get { return _CO2.ToString(); } }
        public string O2 { get { return _O2.ToString(); } }
        public string VOC { get { return _VOC.ToString(); } }
        public string H2S { get { return _H2S.ToString(); } }
        public string CO { get { return _CO.ToString(); } }
        public string Borehole_Pressure { get { return _Borehole_Pressure.ToString(); } }
        public string Atmospheric_Pressure { get { return _Atmospheric_Pressure.ToString(); } }
        public string Pressure_Diff { get { return _Pressure_Diff.ToString(); } }
        public string Temperature { get { return _Temperature.ToString(); } }
        public string Water_Level { get { return _Water_Level.ToString(); } }
        public string Battery { get { return _Battery.ToString(); } }
        public string Category { get { return _Category.ToString(); } }

        public string PlotGraph()
        {
            bool _IsFirstRow = true;

            if (DataSource != null)
            {
                foreach (DataRow dr in  DataSource.Tables[0].Select(" BoreHoleID = " + BoreHoleID.ToString()))
                {
                    if (!_IsFirstRow)
                    {
                        _Category.Append(",");
                        _CH4.Append(",");
                        _CO2.Append(",");
                        _O2.Append(",");
                        _VOC.Append(",");
                        _H2S.Append(",");
                        _CO.Append(",");
                        _Borehole_Pressure.Append(",");
                        _Atmospheric_Pressure.Append(",");
                        _Pressure_Diff.Append(",");
                        _Temperature.Append(",");
                        _Water_Level.Append(",");
                        _Battery.Append(",");

                    }

                    _IsFirstRow = false;

                    _Category.Append("'").Append(Convert.ToDateTime(dr["ReadingDateTime"]).ToString("dd-MMM HH:mm")).Append("'");
                    _CH4.Append(Convert.ToString(dr["CH4"]));
                    _CO2.Append(Convert.ToString(dr["CO2"]));
                    _O2.Append(Convert.ToString(dr["O2"]));
                    _VOC.Append(Convert.ToString(dr["VOC"]));
                    _H2S.Append(Convert.ToString(dr["H2S"]));
                    _CO.Append(Convert.ToString(dr["CO"]));
                    _Borehole_Pressure.Append(Convert.ToString(dr["Borehole_Pressure"]));
                    _Atmospheric_Pressure.Append(Convert.ToString(dr["Atmospheric_Pressure"]));
                    _Pressure_Diff.Append(Convert.ToString(dr["Pressure_Diff"]));
                    _Temperature.Append(Convert.ToString(dr["Temperature"]));
                    _Water_Level.Append(Convert.ToString(dr["Water_Level"]));
                    _Battery.Append(Convert.ToString(dr["Battery"]));
                }
            }
            return GetControlString();
        }

    }
}