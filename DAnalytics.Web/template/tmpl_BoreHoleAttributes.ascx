<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tmpl_BoreHoleAttributes.ascx.cs"
    Inherits="DAnalytics.Web.template.tmpl_BoreHoleAttributes" %>
<h1>
    <%=this.BoreHoleName %></h1>
<br />
<%=this.BoreHoleTable %>
<br />
<div class="page_brk">
</div>
<div id='<%=this.ID + "_Pressure" %>' style="height: 400px; width: 100%">
</div>
<br />
<div id='<%=this.ID + "_CH4VOC" %>' style="height: 400px; width: 100%">
</div>
<br />
<div class="page_brk">
</div>
<div id='<%=this.ID + "_CO2O2" %>' style="height: 300px; width: 100%">
</div>
<br />
<div id='<%=this.ID + "_Temperature" %>' style="height: 300px; width: 100%">
</div>
<br />
<div id='<%=this.ID + "_CH4WaterLevelPressure" %>' style="height: 300px; width: 100%">
</div>
<br />
<div class="page_brk">
</div>
<script language="javascript" type="text/javascript">

    $(function () {
        $('<%="#" + this.ID + "_Pressure" %>').highcharts({
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
                    text: 'Borehole Pressure',
                    style: {
                        color: '#89A54E'
                    }
                }
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
                name: 'Borehole_Pressure',
                color: '#4572A7',
                type: 'spline',
                yAxis: 0,
                data: [<%=this.Borehole_Pressure %>],
                tooltip: {
                    valueSuffix: ' nm'
                }
            }]
        });
    


    
        $('<%="#" + this.ID + "_CH4VOC" %>').highcharts({
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
                        return this.value + 'C';
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
                }
            },{ 
                labels: {
                    formatter: function () {
                        return this.value + 'C';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'VOC',
                    style: {
                        color: '#89A54E'
                    }
                },
                opposite:true
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
                data: [<%=this.CH4%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            },
            {
                name: 'VOC',
                color: '#4572A7',
                type: 'spline',
                yAxis: 1,
                data: [<%=this.VOC%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            }]
        });
    


    
        $('<%="#" + this.ID + "_CO2O2" %>').highcharts({
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
                        return this.value + 'C';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'CO2',
                    style: {
                        color: '#89A54E'
                    }
                }
            },{ 
                labels: {
                    formatter: function () {
                        return this.value + 'C';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'O2',
                    style: {
                        color: '#89A54E'
                    }
                },
                opposite:true
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
                name: 'CO2',
                color: '#4572A7',
                type: 'spline',
                yAxis: 0,
                data: [<%=this.CO2%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            },
            {
                name: 'O2',
                color: '#4572A7',
                type: 'spline',
                yAxis: 1,
                data: [<%=this.O2%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            }]
        });
    



    
        $('<%="#" + this.ID + "_CH4WaterLevelPressure" %>').highcharts({
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
                        return this.value + 'C';
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
                }
            },{ 
                labels: {
                    formatter: function () {
                        return this.value + 'C';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'Water Level',
                    style: {
                        color: '#89A54E'
                    }
                },
                opposite:true
            },{ 
                labels: {
                    formatter: function () {
                        return this.value + 'C';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'Pressure',
                    style: {
                        color: '#89A54E'
                    }
                },
                opposite:true
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
                data: [<%=this.CH4%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            },
            {
                name: 'WaterLevel',
                color: '#4572A7',
                type: 'spline',
                yAxis: 1,
                data: [<%=this.Water_Level%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            },
            {
                name: 'Pressure',
                color: '#4572A7',
                type: 'spline',
                yAxis: 2,
                data: [<%=this.Borehole_Pressure%>],
                tooltip: {
                    valueSuffix: ' C'
                }
            }]
        });
    


    
        $('<%="#" + this.ID + "_Temperature" %>').highcharts({
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
                        return this.value + 'C';
                    },
                    style: {
                        color: '#89A54E'
                    }
                },
                title: {
                    text: 'Temperature',
                    style: {
                        color: '#89A54E'
                    }
                }
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
                name: 'Temperature',
                color: '#4572A7',
                type: 'spline',
                yAxis: 0,
                data: [<%=this.Temperature %>],
                tooltip: {
                    valueSuffix: ' C'
                }
            }]
        });

    });


</script>
