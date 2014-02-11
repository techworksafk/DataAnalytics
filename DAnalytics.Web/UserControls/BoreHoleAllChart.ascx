<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoreHoleAllChart.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.BoreHoleAllChart" %>
<h1>
    <%=this.BoreHoleName + " [Depth:" + this.Depth + "]" %></h1>
<div id="hc_Container" runat="server" style="height: 500px; width: 100%">
</div>
<script language="javascript" type="text/javascript">

    $(function () {
        
        $.ajax({
            cache: false,
            async: true,
            type: "GET",
            url: "../ajax/dataanalytics.ashx?act=boreholecustomreport",
            dataType: "json",
            data:
            {
                bhid: <%=this.BoreHoleID %>,
                from:"<%=this.FromDate %>",
                to:"<%=this.ToDate %>"
            },
            success: function (data) {

                data.Chart.CH4.yAxis = "GasAttributes";
                data.Chart.CO2.yAxis = "GasAttributes";
                data.Chart.O2.yAxis = "GasAttributes";
                data.Chart.VOC.yAxis = "GasAttributes";
                data.Chart.H2S.yAxis = "GasAttributes";
                data.Chart.CO.yAxis = "GasAttributes";
                data.Chart.Borehole_Pressure.yAxis = "PressureAttributes";
                data.Chart.Atmospheric_Pressure.yAxis = "PressureAttributes";
                data.Chart.Pressure_Diff.yAxis = "PressureAttributes";
                data.Chart.Temperature.yAxis = "Temperature";
                data.Chart.Water_Level.yAxis = "WaterLevel";
                data.Chart.Battery.yAxis = "Battery";

               $("#<%=hc_Container.ClientID %>").highcharts("StockChart",{
                    chart: { zoomType: 'xy' },
                    title: { text: data.Chart.BoreHoleName },
                    subtitle: { text: 'Borehole Custom Report' },
                    yAxis:[YAxisGasAttributes,YAxisPressure,YAxisWater,YAxisTemperature,YAxisBattery],
                    tooltip: {
                        shared: true
                    },
                     legend: {
	    	            enabled: true,
	    	            align: 'right',
        	            backgroundColor: '#FCFFC5',
        	            borderColor: 'black',
        	            borderWidth: 2,
	    	            layout: 'vertical',
	    	            verticalAlign: 'top',
	    	            y: 100,
	    	            shadow: true
	                },
                    series:[data.Chart.CH4,data.Chart.CO2,data.Chart.O2,data.Chart.VOC,data.Chart.H2S
                    
                    ,data.Chart.CO
                    ,data.Chart.Borehole_Pressure
                    ,data.Chart.Atmospheric_Pressure
                    ,data.Chart.Pressure_Diff
                    ,data.Chart.Temperature
                    ,data.Chart.Water_Level
                    ,data.Chart.Battery
                    ]
                    });

            },
            error: function (ex) {
             $("#<%=hc_Container.ClientID %>").html("No records to display");
              $("#<%=hc_Container.ClientID %>").css("height","20px");
            }
        });

    });

</script>
