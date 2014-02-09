<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoreHoleTreeSurvey.ascx.cs"
    Inherits="DAnalytics.Web.UserControls.BoreHoleTreeSurvey" %>
<div class="transcripts_details_header" id="divStep1" runat="server" style="display: block;">
    <div style="width: 300px; height: 500px;" class="inner_float_div">
        <label>
            Year:</label><asp:DropDownList ID="ddlYear" runat="server">
            </asp:DropDownList>
        <label>
            Area:</label><asp:DropDownList ID="ddlArea" runat="server">
            </asp:DropDownList>
        <div class="button_holder">
            <asp:Button ID="btnView" runat="server" Text="Next" OnClick="btnView_Click" />
        </div>
    </div>
</div>
<div class="transcripts_details_header" id="divTree" runat="server" style="display: block;">
    <div style="width: 300px; height: 500px;" class="inner_float_div">
        <asp:TreeView ID="tvBoreHole" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="true"
            ShowLines="true">
        </asp:TreeView>
    </div>
    <div class="multi_row_elements">
        <asp:CheckBox ID="chkAutoPick" runat="server" Text="Autopick" />
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
        <asp:Button ID="btnBack" runat="server" CommandName="Print" Text="Back" OnClick="btnBack_Click" />
        <asp:Button ID="btnNext" runat="server" CommandName="Print" Text="Next" OnClick="btnNext_Click" />
    </div>
</div>
<div class="transcripts_details_header" id="divSelectionDiv" runat="server" style="display: block;">
    <div class="template_head" style="width: 100%">
        <%=SelectionTable %>
    </div>
    <div class="cls">
    </div>
    <br />
    <div class="multi_row_elements">
        <label>
            Contract No:</label>
        <asp:TextBox ID="txtContractNo" runat="server" CssClass="input_text input_small"></asp:TextBox>
        <label>
            Contract Title:</label>
        <asp:TextBox ID="txtContractTitle" runat="server" CssClass="input_text input_wide"
            Width="500px"></asp:TextBox>
    </div>
    <div class="cls">
    </div>
    <div class="button_holder">
        <asp:Button ID="btnGenerate" runat="server" CommandName="Print" Text="Generate" OnClick="btnGenerate_Click" />
        <asp:Button ID="btnBack2" runat="server" CommandName="Print" Text="Back" 
            onclick="btnBack2_Click" />
    </div>
</div>
<input type="hidden" id="hdnGenerateReport" />
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
