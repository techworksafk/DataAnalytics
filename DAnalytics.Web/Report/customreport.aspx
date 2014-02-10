<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="customreport.aspx.cs" Inherits="DAnalytics.Web.Report.customreport" %>

<%@ Register Src="../UserControls/BoreHoleTree.ascx" TagName="BoreHoleTree" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/BoreHoleAllChart.ascx" TagName="BoreHoleAllChart"
    TagPrefix="uc1" %>
<%@ Register src="../UserControls/GridPager.ascx" tagname="GridPager" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <uc2:BoreHoleTree ID="BoreHoleTree1" runat="server" />
            <div class="cls">
                <input type="hidden" runat="server" id="hdnUserID" />
                <input type="hidden" runat="server" id="hdnReportID" />
            </div>
            <script language="javascript" type="text/javascript" src="../Scripts/dailyreport.js"></script>
            <div class="cls">
            </div>
            <script language="javascript" type="text/javascript" src="../Scripts/highcharts.js"></script>
            <script language="javascript" type="text/javascript" src="../Scripts/exporting.js"></script>
            <div class="grid">
                <asp:GridView ID="gvBoreHoles" runat="server" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <uc1:BoreHoleAllChart ID="BoreHoleAllChart1" runat="server" BoreHoleID='<%#Eval("BoreHoleID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <uc3:GridPager ID="GridPager1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
