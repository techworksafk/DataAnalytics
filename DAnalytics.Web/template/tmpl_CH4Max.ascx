<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tmpl_CH4Max.ascx.cs"
    Inherits="DAnalytics.Web.template.tmpl_CH4Max" %>
<div id='<%=this.ID + "_CH4Max" %>' style="height: 400px; width: 100%">
</div>
<br />
<div class="page_brk">
</div>
<script language="javascript" type="text/javascript">
    $(function () {
        $('<%="#" + this.ID + "_CH4Max" %>').highcharts({
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
                name: 'CH4',
                color: '#4572A7',
                type: 'spline',
                yAxis: 0,
                data: [<%=this.CH4 %>],
                tooltip: {
                    valueSuffix: ' nm'
                }
            }]
        });
    });
</script>
