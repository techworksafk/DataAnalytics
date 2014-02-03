<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoreHoleTreeSurvey.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.BoreHoleTreeSurvey" %>
<div style="width: 300px; height: 500px;" class="inner_float_div">
    <label>
        Year:</label><asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList>
    <label>
        Area:</label><asp:DropDownList ID="ddlArea" runat="server">
        </asp:DropDownList>
    <asp:Button ID="btnView" runat="server" Text="Load Tree" 
        onclick="btnView_Click" />
    <asp:TreeView ID="tvBoreHole" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="true"
        ShowLines="true">
    </asp:TreeView>
    <input type="hidden" id="hdnGenerateReport" />
</div>
<script type="text/javascript">
    $(function () {
        $("[id$='tvBoreHole'] input[type=checkbox]").click(function () {
            var ChkID = $(this).attr("id");
            var isChecked = $("[id$='" + ChkID + "']").is(":checked");
            CheckChild(ChkID, isChecked);
        });
    });

    function CheckChild(ChkID, isChecked) {
        var DivNodesID = ChkID.replace("CheckBox", "Nodes");
        $("[id$='" + DivNodesID + "'] input[type=checkbox]").each(function () {
            var Chk = $(this).prop("checked", isChecked);
        });

        if (!isChecked) {
            DivNodesID = $("[id$='" + ChkID + "']").parents('div:eq(0)').attr("id");
            CheckChild(DivNodesID.replace("Nodes", "CheckBox"), isChecked);
        }
    }
</script>
