<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoreHoleAllChart.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.BoreHoleAllChart" %>
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
                bhid: <%=this.BoreHoleID %>
                //from:"<%=this.FromDate %>",
                //to:"<%=this.ToDate %>"
            },
            success: function (data) {
                $("#<%=hc_Container.ClientID %>").highcharts({
                    chart: { zoomType: 'xy' },
                    title: { text: data.Chart.BoreHoleName },
                    subtitle: { text: 'Borehole Custom Report' },
                    xAxis: [{
                        categories: data.Chart.Category.Series
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
                        data: data.Chart.CH4.Series,
                        tooltip: {
                            valueSuffix: ' nm'
                        }

                    }, {
                        name: 'CO2',
                        type: 'spline',
                        color: '#AA4643',
                        yAxis: 1,
                        data: data.Chart.CO2.Series,
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
                        data: data.Chart.CO.Series,
                        tooltip: {
                            valueSuffix: ' nm'
                        }
                    }]
                });
            },
            error: function (ex) {
             
            }
        });

    });

</script>
