<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="OLDProj_ResubmittedClaimsReport.aspx.cs" Inherits="DAnalytics.Web.Report.ResubmittedClaimsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Resubmitted Claims Report</span></div>
                <div class="button_holder">
                    <asp:Button ID="btnPrint" runat="server" CommandName="Print" Text="View" OnClientClick="return PrintReport();" />
                    <input type="hidden" runat="server" id="hdnUserID" />
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="multi_row_elements">
                <label>
                    Date From:</label>
                <asp:TextBox ID="txtDtFrom" runat="server" CssClass="input_text input_small"></asp:TextBox>
                <label>
                    Date To:</label>
                <asp:TextBox ID="txtDtTo" runat="server" CssClass="input_text input_small"></asp:TextBox>                
            </div>
            <div class="cls">
            </div>
        </div>
    <script language="javascript" type="text/javascript">

        $(function () {
            $("[id$='txtDtFrom']").datepicker({ dateFormat: "mm/dd/yy" });
            $("[id$='txtDtTo']").datepicker({ dateFormat: "mm/dd/yy" });
        });

        function PrintReport() {
            var From = $("[id$='txtDtFrom']").val();
            var To = $("[id$='txtDtTo']").val();
            var UID = $("[id$='hdnUserID']").val();
            return OpenReportViewer("RESUBMITTEDCLAIMS", "&From=" + From + "&To=" + To + "&UID=" + UID);
        }
    </script>
</asp:Content>
