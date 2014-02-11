<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewPager.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.GridViewPager" %>
<div class="multi_row_elements">
    <asp:LinkButton ID="lnkFirst" runat="server" OnClick="lnkFirst_Click">First</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">
                Previous</asp:LinkButton>&nbsp;&nbsp;
    <label>
        Page&nbsp;</label>
    <asp:TextBox ID="txtPageNumber" runat="server" Width="25px" onkeydown="Change(event)" ValidationGroup="pagechange"></asp:TextBox>
    <label>
        &nbsp;of&nbsp;</label>
    <asp:Label ID="lblTotalPages" runat="server"></asp:Label>
    <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lnkLast" runat="server" OnClick="lnkLast_Click">Last</asp:LinkButton>&nbsp;&nbsp;
</div>
<div style="display: none;">
    <asp:Button ID="btnChangePage" runat="server" OnClick="btnChangePage_Click" ValidationGroup="pagechange" />
    <input type="hidden" id="hdnPageNumber" runat="server" value="1" />
    <input type="hidden" id="hdnTotalPages" runat="server" />
</div>
<script language="javascript" type="text/javascript">

    $("#<%=txtPageNumber.ClientID%>").keydown(function (event) {

        if (event.which == 13) {
            if (isNaN(parseInt($(this).val()))) {
                if (parseInt($(this).val()) <= parseInt($("#<%=hdnTotalPages.ClientID%>").val())) {
                    $("#<%=hdnPageNumber.ClientID%>").val(parseInt($(this).val()));
                }
                else {
                    $("#<%=hdnPageNumber.ClientID%>").val(parseInt($("#<%=hdnTotalPages.ClientID%>").val()));
                }
                $("#<%=btnChangePage.ClientID%>").click();
                return false;
            }
        }

    });

</script>
