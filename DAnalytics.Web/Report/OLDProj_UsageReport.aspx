<%@ Page Title="Usage Report" Language="C#" MasterPageFile="~/Master/Site.Master"
    AutoEventWireup="true" CodeBehind="OLDProj_UsageReport.aspx.cs" Inherits="DAnalytics.Web.Report.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Usage Report</span></div>
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
                <label >
                    Adjuster Name:</label>
                <asp:TextBox ID="txtAdjSearch" runat="server" CssClass="input_wide"></asp:TextBox>
                <asp:HiddenField ID="hdnAdjusterID" runat="server" Value="0" />
                <label >
                    Claim #:</label>
                <asp:TextBox ID="txtClaimNo" runat="server" CssClass="input_wide"></asp:TextBox>
                <label >
                    Template #:</label>
                <asp:TextBox ID="txtTemplateNo" runat="server" CssClass="input_wide"></asp:TextBox>
                <label>
                    Sort By:</label>
                <asp:DropDownList ID="ddlSort" runat="server" CssClass="medium" AutoPostBack="false">
                    <asp:ListItem Text="AdjusterName Ascending" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="AdjusterName Descending" Value="2"></asp:ListItem>
                    <asp:ListItem Text="AdjusterID Ascending" Value="3"></asp:ListItem>
                    <asp:ListItem Text="AdjusterID Descending" Value="4"></asp:ListItem>
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
                <script language="javascript" type="text/javascript" src="../Jquery/jquery.autocomplete.js"></script>
                <script language="javascript" type="text/javascript">
                    $(function () {
                        $("[id$='txtDtFrom']").datepicker({ dateFormat: "mm/dd/yy" });
                        $("[id$='txtDtTo']").datepicker({ dateFormat: "mm/dd/yy" });
                    });

                    $("[id$='txtAdjSearch']").autocomplete("../GenericHandler.ashx?Action=SearchAdjusterNameWithID", {
                        pageName: "ReportingGroup",
                        dataType: 'json',
                        minChars: 1,
                        cache: false,
                        parse: function (data) {
                            var parsed = [];
                            if (data != null) {
                                data = data.Adjusters;
                                for (var i = 0; i < data.length; i++) {
                                    parsed[parsed.length] = {
                                        data: data[i],
                                        value: data[i].AdjusterID,
                                        result: data[i].AdjusterName
                                    };
                                }
                            }
                            return parsed;
                        },
                        autoFill: true,
                        cacheLength: 0,
                        formatItem: function (row) {
                            $("[id$='hdnAdjusterID']").val("0");
                            $("[id$='txtSearch']").val("");
                            $("[id$='txtSearch']").autocomplete("destroy");
                            return row.AdjusterName;
                        }
                    }).result(function (e, data, formatted) {
                        $("[id$='hdnAdjusterID']").val(data.AdjusterID);
                    });

                    function PrintReport() {
                        var From = $("[id$='txtDtFrom']").val();
                        var To = $("[id$='txtDtTo']").val();
                        var UID = $("[id$='hdnUserID']").val();
                        var RptGroupID = $("[id$='ddlReportGroup']").val();
                        var CompanyId = $("[id$='ddlAdjCompany']").val();
                        var AdjID;
                        if ($("[id$='txtAdjSearch']").val() == "") {
                            AdjID = "0";
                        }
                        else {
                            AdjID = $("[id$='hdnAdjusterID']").val();
                        }
                        var ClaimNo = $("[id$='txtClaimNo']").val();
                        var TemplateNo = $("[id$='txtTemplateNo']").val();
                        var SortExp = $("[id$='ddlSort']").val();
                        return OpenReportViewer("USAGEREPORT", "&From=" + From + "&To=" + To + "&UID=" + UID + "&RptGroupID=" + RptGroupID + "&CID=" + CompanyId + "&AdjID=" + AdjID + "&ClaimNo=" + ClaimNo + "&TNo=" + TemplateNo + "&Sort=" + SortExp);
                    }

                </script>
            </div>
        </div>
    </div>
</asp:Content>
