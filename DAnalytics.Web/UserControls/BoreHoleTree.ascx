<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoreHoleTree.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.BoreHoleTree" %>
<div class="transcripts_details_header" id="divTreeDiv" runat="server" style="display: block;">
    <div style="width: 98%; max-height: 500px; height: auto; overflow: auto;">
        <asp:TreeView ID="tvBoreHole" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="true"
            ShowLines="true">
        </asp:TreeView>
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
    <div class="button_holder">
        <asp:Button ID="btnView" runat="server" CommandName="Print" Text="View" OnClick="btnView_Click" />
    </div>
</div>
<div class="transcripts_details_header" id="divSelectionDiv" runat="server" style="display: none;">
    <div class="template_head" style="width: 98%">
        <%=SelectionTable %>
    </div>
    <div class="cls">
    </div>
    <br />
    <%if (!IsCustomReport)
      { %>
    <div class="multi_row_elements">
        <label>
            Contract No:</label>
        <asp:TextBox ID="txtContractNo" runat="server" CssClass="input_text input_small"></asp:TextBox>
        <label>
            Prepare By:</label>
        <asp:TextBox ID="txtPrepareName" runat="server" CssClass="input_text input_wide"
            Width="500px"></asp:TextBox>
        <label>
            Prepare Designation:</label>
        <asp:TextBox ID="txtPrepareDesig" runat="server" CssClass="input_text input_wide"
            Width="500px"></asp:TextBox>
    </div>
    <div class="cls">
    </div>
    <%} %>
    <div class="button_holder">
        <asp:Button ID="btnGenerate" runat="server" CommandName="Print" Text="Generate" OnClick="btnGenerate_Click" />
        <asp:Button ID="btnBack" runat="server" CommandName="Print" Text="Back" OnClick="btnBack_Click" />
    </div>
</div>
<input type="hidden" id="hdnIsCustomReport" runat="server" value="0" />
<input type="hidden" id="hdnSelectionTable" runat="server" />
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
