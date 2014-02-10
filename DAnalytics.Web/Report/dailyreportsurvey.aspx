<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="dailyreportsurvey.aspx.cs" Inherits="DAnalytics.Web.Report.dailyreportsurvey" %>

<%@ Register Src="../UserControls/BoreHoleTreeSurvey.ascx" TagName="BoreHoleTreeSurvey"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <uc1:BoreHoleTreeSurvey ID="BoreHoleTreeSurvey1" runat="server" />
            <div class="cls">
                <input type="hidden" runat="server" id="hdnUserID" />
                <input type="hidden" runat="server" id="hdnReportID" />
            </div>
            <script language="javascript" type="text/javascript" src="../Scripts/dailyreport.js"></script>
            <script language="javascript" type="text/javascript">



                function PrintReport() {
                    var From = $("[id$='txtDtFrom']").val();
                    var To = $("[id$='txtDtTo']").val();
                    var BoreHole = $("[id$='hdnBoreHoleID']").val();
                    //return CallReport("MinMaxRpt", "&From=" + From + "&To=" + To + "&BoreHole=" + BoreHole); // + "&Sort=" + SortExp);
                }

            </script>
            <div class="cls">
            </div>
        </div>
    </div>
</asp:Content>
