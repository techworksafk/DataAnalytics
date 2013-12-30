<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridPager.ascx.cs" Inherits="DAnalytics.UserControls.GridPager" %>
<div>
    <asp:LinkButton ID="lnkFirst" runat="server" OnClick="lnkFirst_Click">&nbsp;&nbsp;First</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">
                Previous</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkLast" runat="server" OnClick="lnkLast_Click">Last</asp:LinkButton>&nbsp;&nbsp;
    <span>
        <asp:DropDownList ID="drpSize" runat="server" Width="50px" 
        AutoPostBack="True"  OnSelectedIndexChanged="drpSize_SelectedIndexChanged">           
        </asp:DropDownList>
    </span><span><span id="spnPage" style="padding-left: 500px;">Page</span>&nbsp;<span
        id="spnPageNumber" runat="server" visible="false"></span>
        <input type="button" runat="server" id="btnChangePage" style="display: none" value="ChangePage"
            onserverclick="Page_Change" />
        <asp:HiddenField ID="hidPageNumber" runat="server" />
        <asp:TextBox ID="txtPageNumber" runat="server" Width="25px" ValidationGroup="textPaging"></asp:TextBox>
        &nbsp;<span id="spnOf">of</span> &nbsp;<span id="spnTotalPages" runat="server"></span></span>
    <div class="float_right">
        <asp:RangeValidator ID="rngPageNumber" runat="server" ValidationGroup="textPaging"
            ErrorMessage="Enter valid page number" MinimumValue="1" ControlToValidate="txtPageNumber"
            MaximumValue="1" Type="Integer"></asp:RangeValidator></div>
</div>
<%--<asp:ValidationSummary ID="pagerSummery" runat="server" ValidationGroup="textPaging"
    ShowMessageBox="true" />--%>
<br clear="all" />
<script language="javascript" type="text/javascript">

    function ChangeText(txt, event, hidPageNumberID, btnChangePageID) {
        var pageNumber = txt.value;
        var hidPageNumber = document.getElementById(hidPageNumberID);
        var btnChangePage = document.getElementById(btnChangePageID);
        if (event.keyCode == 13) {
            hidPageNumber.value = pageNumber;
            btnChangePage.click();
            return false;
        }
    }

</script>
