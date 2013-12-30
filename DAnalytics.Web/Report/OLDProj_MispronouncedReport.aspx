<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="OLDProj_MispronouncedReport.aspx.cs" Inherits="DAnalytics.Web.Report.MispronouncedReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Mispronounciation Trends</span></div>
                <div class="button_holder">
                    <asp:Button ID="btnPrint" runat="server" CommandName="Print" Text="View" OnClientClick="return PrintReport();" />
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
                    Group:</label>
                <asp:DropDownList ID="ddlReportGroup" runat="server">
                </asp:DropDownList>
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
                        var RptGroupID = $("[id$='ddlReportGroup']").val();
                        var CompanyId = $("[id$='ddlAdjCompany']").val();
                        return OpenReportViewer("MISPRONOUNCED", "&From=" + From + "&To=" + To + "&UID=" + UID + "&RptGroupID=" + RptGroupID + "&CID=" + CompanyId);
                    }

                </script>
            </div>
        </div>
    </div>
</asp:Content>
