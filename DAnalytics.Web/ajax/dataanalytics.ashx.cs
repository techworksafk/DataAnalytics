using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAnalytics.UTIL;
using System.Data;
using System.Text;
namespace DAnalytics.Web.ajax
{
    /// <summary>
    /// Summary description for dataanalytics
    /// </summary>
    public class dataanalytics : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request.QueryString["act"].ToLower())
            {
                case "boreholecustomreport":
                    BoreHoleCustomReport(context);
                    break;
            }
        }


        void BoreHoleCustomReport(HttpContext context)
        {
            int BoreHoleID = context.Request.QueryString["bhid"].ConvertToInt32();
            DateTime? _from = null;
            DateTime? _to = null;

            if (Convert.ToString(context.Request.QueryString["from"]).Length > 0)
                _from = Convert.ToDateTime(context.Request.QueryString["from"]);

            if (Convert.ToString(context.Request.QueryString["to"]).Length > 0)
                _to = Convert.ToDateTime(context.Request.QueryString["to"]);

            DataSet ds = BL.Report.CustomReport.GetBoreholeReport(BoreHoleID, _from, _to);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder _Chart = new StringBuilder();

                _Chart.Append("{ \"Chart\" : {");
                _Chart.Append(" \"BoreHoleName\" : \"").Append(ds.Tables[0].Rows[0]["BoreHoleName"].ToString()).Append("\",");

                _Chart.Append(" \"Category\" : {");
                _Chart.Append(" \"Series\" : [CategorySeries]");
                _Chart.Append("},");

                _Chart.Append(" \"CH4\" : {");
                _Chart.Append(" \"name\" : \"CH4\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["CH4Color"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [CH4Series]");
                _Chart.Append("},");

                _Chart.Append(" \"CO2\" : {");
                _Chart.Append(" \"name\" : \"CO2\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["CO2Color"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [CO2Series]");
                _Chart.Append("},");

                _Chart.Append(" \"O2\" : {");
                _Chart.Append(" \"name\" : \"O2\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["O2Color"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [O2Series]");
                _Chart.Append("},");

                _Chart.Append(" \"VOC\" : {");
                _Chart.Append(" \"name\" : \"VOC\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["VOCColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [VOCSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"H2S\" : {");
                _Chart.Append(" \"name\" : \"H2S\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["H2SColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [H2SSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"CO\" : {");
                _Chart.Append(" \"name\" : \"CO\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["COColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [COSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"Borehole_Pressure\" : {");
                _Chart.Append(" \"name\" : \"Borehole Pressure\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["Borehole_PressureColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [Borehole_PressureSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"Atmospheric_Pressure\" : {");
                _Chart.Append(" \"name\" : \"Atmospheric Pressure\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["Atmospheric_PressureColor"].ToString()).Append("\",");
                _Chart.Append(" \"Series\" : [Atmospheric_PressureSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"Pressure_Diff\" : {");
                _Chart.Append(" \"name\" : \"Pressure Diff\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["Pressure_DiffColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [Pressure_DiffSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"Temperature\" : {");
                _Chart.Append(" \"name\" : \"Temperature\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["TemperatureColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [TemperatureSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"Water_Level\" : {");
                _Chart.Append(" \"name\" : \"Water Level\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["Water_LevelColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [Water_LevelSeries]");
                _Chart.Append("},");

                _Chart.Append(" \"Battery\" : {");
                _Chart.Append(" \"name\" : \"Battery\",");
                //_Chart.Append(" color : \"").Append(ds.Tables[0].Rows[0]["BatteryColor"].ToString()).Append("\",");
                _Chart.Append(" \"data\" : [BatterySeries]");
                _Chart.Append("}");

                _Chart.Append(" }}");

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

                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                {
                    DataRow dr = ds.Tables[0].Rows[iRow];

                    string _xAxis = Convert.ToDateTime(dr["ReadingDateTime"]).ToUnixTimestamp().ToString();
                    _Category.Append(_xAxis);

                    _CH4.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["CH4"])).Append("]");
                    _CO2.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["CO2"])).Append("]");
                    _O2.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["O2"])).Append("]");
                    _VOC.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["VOC"])).Append("]");
                    _H2S.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["H2S"])).Append("]");
                    _CO.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["CO"])).Append("]");
                    _Borehole_Pressure.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["Borehole_Pressure"])).Append("]");
                    _Atmospheric_Pressure.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["Atmospheric_Pressure"])).Append("]");
                    _Pressure_Diff.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["Pressure_Diff"])).Append("]");
                    _Temperature.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["Temperature"])).Append("]");
                    _Water_Level.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["Water_Level"])).Append("]");
                    _Battery.Append("[").Append(_xAxis).Append(",").Append(Convert.ToString(dr["Battery"])).Append("]");

                    //_CO.Append(Convert.ToString(dr["CO"]));
                    //_Borehole_Pressure.Append(Convert.ToString(dr["Borehole_Pressure"]));
                    //_Atmospheric_Pressure.Append(Convert.ToString(dr["Atmospheric_Pressure"]));
                    //_Pressure_Diff.Append(Convert.ToString(dr["Pressure_Diff"]));
                    //_Temperature.Append(Convert.ToString(dr["Temperature"]));
                    //_Water_Level.Append(Convert.ToString(dr["Water_Level"]));
                    //_Battery.Append(Convert.ToString(dr["Battery"]));

                    if (iRow != ds.Tables[0].Rows.Count - 1)
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
                }

                _Chart.Replace("CategorySeries", _Category.ToString());
                _Chart.Replace("CH4Series", _CH4.ToString());
                _Chart.Replace("CO2Series", _CO2.ToString());
                _Chart.Replace("O2Series", _O2.ToString());
                _Chart.Replace("VOCSeries", _VOC.ToString());
                _Chart.Replace("H2SSeries", _H2S.ToString());
                _Chart.Replace("COSeries", _CO.ToString());
                _Chart.Replace("Borehole_PressureSeries", _Borehole_Pressure.ToString());
                _Chart.Replace("Atmospheric_PressureSeries", _Atmospheric_Pressure.ToString());
                _Chart.Replace("Pressure_DiffSeries", _Pressure_Diff.ToString());
                _Chart.Replace("TemperatureSeries", _Temperature.ToString());
                _Chart.Replace("Water_LevelSeries", _Water_Level.ToString());
                _Chart.Replace("BatterySeries", _Battery.ToString());

                context.Response.Write(_Chart.ToString());
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