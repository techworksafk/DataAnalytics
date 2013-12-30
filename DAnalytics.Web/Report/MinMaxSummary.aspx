<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="MinMaxSummary.aspx.cs" Inherits="DAnalytics.Web.Report.MinMaxSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Min and Max Summary</span></div>
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
                    Borehole:</label>
                <asp:DropDownList ID="ddlBoreHole" runat="server">
                </asp:DropDownList>              
             <%--   <label>Sort By:</label>
                <asp:DropDownList ID="ddlSort" runat="server" CssClass="medium" AutoPostBack="false">
                    <asp:ListItem Text="BoreHole Ascending" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="BoreHole Descending" Value="2"></asp:ListItem>             
                </asp:DropDownList>  --%>            
                <div class="cls">
                    <input type="hidden" runat="server" id="hdnUserID" />
                </div>
                <script language="javascript" type="text/javascript" src="../Jquery/jquery.autocomplete.js"></script>
                <script language="javascript" type="text/javascript">
                    $(function () {
                        $("[id$='txtDtFrom']").datepicker({ dateFormat: "dd/mm/yyyy" });
                        $("[id$='txtDtTo']").datepicker({ dateFormat: "dd/mm/yyyy" });
                    });

                    function PrintReport() {
                        var From = $("[id$='txtDtFrom']").val();
                        var To = $("[id$='txtDtTo']").val();
                        //var UID = $("[id$='hdnUserID']").val();
                        var BoreHole = $("[id$='ddlBoreHole']").val(); 
                        //return CallReport("MinMaxRpt", "&From=" + From + "&To=" + To + "&BoreHole=" + BoreHole); // + "&Sort=" + SortExp);
                    }

                </script>
            </div>
        </div>
    </div>
</asp:Content>
