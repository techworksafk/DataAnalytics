<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="Borehole.aspx.cs" Inherits="DAnalytics.Web.Report.Borehole" %>

<%@ Register Src="../UserControls/BoreHoleChart.ascx" TagName="BoreHoleChart" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/BoreHoleTree.ascx" TagName="BoreHoleTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <uc2:BoreHoleTree ID="BoreHoleTree1" runat="server" IsCustomReport="false" />

            <div class="cls">
                <input type="hidden" runat="server" id="hdnUserID" />
                <input type="hidden" runat="server" id="hdnReportID" />
            </div>
            <script language="javascript" type="text/javascript" src="../Scripts/dailyreport.js"></script>
            <script language="javascript" type="text/javascript">



                

            </script>
            <div class="cls">
            </div>
        </div>
    </div>
</asp:Content>
