<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="AuditTrail.aspx.cs" Inherits="DAnalytics.Web.Admin.AuditTrail" %>

<%@ Register Src="../UserControls/GridPager.ascx" TagName="GridPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Audit Trail</span></div>
                <div class="button_holder">                
                    <asp:Button ID="btnSearch" runat="server" Text="Search"  CommandName="View"
                        TabIndex="4" OnClick="btnSearch_Click" OnClientClick="javascript:return ValidateConditions();" />    
                         <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" 
                        TabIndex="5" OnClick="btnCancel_Click" />               
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="row_elements">
                <label>
                    From Date:</label>
                <asp:TextBox ID="txtFrom" runat="server" Width="90px" TabIndex="1"></asp:TextBox>
                <label>
                    To Date:</label>
                <asp:TextBox ID="txtTo" runat="server" Width="90px" TabIndex="2"></asp:TextBox>
                <label>
                    Users:</label>
                <asp:DropDownList ID="ddlUsers" runat="server" class="dropdown_box" TabIndex="3">
                </asp:DropDownList>
                <asp:ValidationSummary ID="valSumm" runat="server" DisplayMode="BulletList" ShowSummary="false"
                    ShowMessageBox="true" />
                <div class="cls">
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="grid">
                <asp:GridView ID="gvAudit" runat="server" AutoGenerateColumns="False" Width="100%"
                    PageSize="10" TabIndex="6" EmptyDataText="No Records Found">
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataField="LogDate" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="LogDesc" HeaderText="Description" />
                    </Columns>
                    <HeaderStyle CssClass="grid-header-part" />
                    <RowStyle CssClass="grid-content-part1" />
                    <AlternatingRowStyle CssClass="grid-content-part2" />
                </asp:GridView>
            </div>
            <div class="cls">
            </div>
            <br clear="all" />
            <div class="pagination">
                <uc1:GridPager ID="GridPager1" runat="server" ControlToBind="gvAudit" OnGridPagerChanged="Page_Changed"
                    PageNum="1" />
            </div>
            <div class="cls">
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        var txtFrom = document.getElementById('<%=txtFrom.ClientID%>').id;
        var txtTo = document.getElementById('<%=txtTo.ClientID %>').id;
        var fromErrMsg = '<%= fromErrMsg %>';
        var toErrMsg = '<%= toErrMsg %>';



        $(function () {

            $('input#' + txtFrom).datepicker({ dateFormat: 'dd/mm/yy',
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August',
                            'September', 'October', 'November', 'December'], dayNamesShort: ['Sun', 'Mon', 'Tue',
                            'Wed', 'Thu', 'Fri', 'Sat'], dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                changeMonth: true,
                changeYear: true
            });
            $('input#' + txtTo).datepicker({ dateFormat: 'dd/mm/yy', monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October',
                        'November', 'December'], dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri',
                        'Sat'], dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                changeMonth: true,
                changeYear: true
            });
        });


        var txtFromDate = document.getElementById('<%=txtFrom.ClientID%>');
        var txtToDate = document.getElementById('<%=txtTo.ClientID %>');

        function ValidateConditions() {
            var alertMsg = "";
            if (txtFromDate.value == "") {
                alertMsg = fromErrMsg;
            }
            if (txtToDate.value == "") {
                alertMsg += "\n" + toErrMsg;
            }
            if (alertMsg != "") {
                alert(alertMsg);
                return false;
            }

            if (IsDateGreater(txtFromDate.value, txtToDate.value)) {
                alert("From Date Greater than To Date.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
