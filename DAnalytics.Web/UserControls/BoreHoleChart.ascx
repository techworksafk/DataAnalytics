<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoreHoleChart.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.BoreHoleChart" %>
<div id="container" style="height: 400px; width: 100%">
</div>
<script language="javascript" type="text/javascript" src="../Scripts/highcharts.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/exporting.js"></script>
<%--<script src="http://code.highcharts.com/highcharts.js"></script>
<script src="http://code.highcharts.com/modules/exporting.js"></script>--%>
<script language="javascript" type="text/javascript">

    $(function () {
        $('#container').highcharts({
            chart: {
                zoomType: 'xy'
            },
            title: {
                text: 'Daily Report'
            },
            subtitle: {
                text: 'Borehole Daily Report'
            },
            xAxis: [{
                categories: [<%=this.Category %>]
            }],
            yAxis: [{ // Primary yAxis
                labels: {
                    formatter: function () {
                        return this.value + 'nm';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'CH4',
                    style: {
                        color: '#89A54E'
                    }
                },
                opposite: true

            }, { // Secondary yAxis
                gridLineWidth: 0,
                title: {
                    text: 'CO2',
                    style: {
                        color: '#4572A7'
                    }
                },
                labels: {
                    formatter: function () {
                        return this.value + ' nm';
                    },
                    style: {
                        color: '#4572A7'
                    }
                }

            }, { // Tertiary yAxis
                gridLineWidth: 0,
                title: {
                    text: 'CO',
                    style: {
                        color: '#AA4643'
                    }
                },
                labels: {
                    formatter: function () {
                        return this.value + ' nm';
                    },
                    style: {
                        color: '#AA4643'
                    }
                },
                opposite: true
            }],
            tooltip: {
                shared: true
            },
            legend: {
                layout: 'vertical',
                align: 'left',
                x: 120,
                verticalAlign: 'top',
                y: 80,
                floating: true,
                backgroundColor: '#FFFFFF'
            },
            series: [{
                name: 'CH4',
                color: '#4572A7',
                type: 'spline',
                yAxis: 0,
                data: [<%=this.CH4 %>],
                tooltip: {
                    valueSuffix: ' nm'
                }

            }, {
                name: 'CO2',
                type: 'spline',
                color: '#AA4643',
                yAxis: 1,
                data: [<%=this.CO2 %>],
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot',
                tooltip: {
                    valueSuffix: ' nm'
                }

            }, {
                name: 'CO',
                color: '#89A54E',
                yAxis: 2,
                type: 'spline',
                data: [<%=this.CO %>],
                tooltip: {
                    valueSuffix: ' nm'
                }
            }]
        });
    });



</script>
