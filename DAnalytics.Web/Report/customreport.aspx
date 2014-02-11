<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="customreport.aspx.cs" Inherits="DAnalytics.Web.Report.customreport" %>

<%@ Register Src="../UserControls/BoreHoleTree.ascx" TagName="BoreHoleTree" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/BoreHoleAllChart.ascx" TagName="BoreHoleAllChart"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <uc2:BoreHoleTree ID="BoreHoleTree1" runat="server" IsCustomReport="true" />
            <div class="cls">
                <input type="hidden" runat="server" id="hdnUserID" />
                <input type="hidden" runat="server" id="hdnReportID" />
            </div>
            <script language="javascript" type="text/javascript" src="../Scripts/dailyreport.js"></script>
            <div class="cls">
            </div>
            <script language="javascript" type="text/javascript" src="../Scripts/highstock.js"></script>
            <script language="javascript" type="text/javascript" src="../Scripts/exporting.js"></script>
            <div class="grid">
                <asp:GridView ID="gvBoreHoles" runat="server" AutoGenerateColumns="false" 
                    Width="100%" onrowdatabound="gvBoreHoles_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <uc1:BoreHoleAllChart ID="BoreHoleAllChart1" runat="server" BoreHoleID='<%#Eval("BoreHoleID") %>'
                                    BoreHoleName='<%#Eval("BoreHoleName") %>' Depth='<%#Eval("Depth") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <uc4:GridViewPager ID="GridViewPager1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
