<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="OLDProj_TeamMemberTranscriptsReport.aspx.cs" Inherits="DAnalytics.Web.Report.TeamMemberTranscriptsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Data Analytics  Team Member Transcripts</span></div>
                <div class="button_holder">
                    <asp:Button ID="btnPrint" runat="server" Text="View" OnClientClick="return PrintReport();" CommandName="Print" />
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
                <label>
                    Company:</label>
                <asp:DropDownList ID="ddlAdjCompany" runat="server" CssClass="medium" AutoPostBack="false">
                    <asp:ListItem Text="All Adjusters" Value="ALL" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Pilot Adjusters" Value="PLT"></asp:ListItem>
                    <asp:ListItem Text="AllState Adjusters" Value="ALT"></asp:ListItem>
                </asp:DropDownList>
                <div class="cls">
                    <input type="hidden" runat="server" id="hdnUserID" />
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
                        var CompanyId = $("[id$='ddlAdjCompany']").val();
                        return OpenReport("TMTRANSCRIPTS", "&From=" + From + "&To=" + To + "&UID=" + UID + "&CID=" + CompanyId);
                    }

                    function OpenReport(ReportName, QueryString) {
                        javascript: window.open("../Report/ReportViewer.aspx?Rpt=" + ReportName + QueryString);
                        return false;
                    }

                </script>
            </div>
        </div>
    </div>
</asp:Content>
