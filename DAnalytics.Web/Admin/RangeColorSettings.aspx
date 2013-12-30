<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="RangeColorSettings.aspx.cs" Inherits="DAnalytics.Web.Admin.RangeColorSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Ranges and Color Settings</span></div>
                
                <div class="button_holder">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="roleSave" TabIndex="2"
                        CommandName="SaveRole" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="3" OnClick="btnCancel_Click">
                    </asp:Button>
                </div>
                </div>
                <div class="cls"></div>
                <div class="multi_row_elements">
                     <label style="width: 150px !important;">
                    Test Checkbox</label>
                <asp:CheckBox ID="chkTest" runat="server" Checked="false" TabIndex="2" />
                </div>
            
        </div>
    </div>
</asp:Content>
