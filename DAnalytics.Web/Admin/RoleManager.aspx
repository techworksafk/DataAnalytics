<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="RoleManager.aspx.cs" Inherits="DAnalytics.Web.Admin.RoleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">

        window.onload = function () {
            var txtRoleName = document.getElementById('<%=txtRoleName.ClientID%>');
            txtRoleName.focus();
        }


        function CheckFullPermission(ele) {
            $.find("input:checkbox").each(function () {
                this.checked = ele.checked;
            });
        }

        function CheckUncheckPermissions() {
            var activeChk = document.getElementById('<%=chkActive.ClientID%>');
            var fullRightsChk = document.getElementById('<%=chkFullRights.ClientID%>');
            var elm = document.getElementsByTagName('input');
            if (fullRightsChk.checked) {
                for (var i = 0; i < elm.length; i++) {
                    if (elm.item(i).type == "checkbox" && elm.item(i) != activeChk && elm.item(i) != fullRightsChk) {
                        elm.item(i).checked = true;
                        elm.item(i).disabled = false;
                    }
                }
            }
            else {
                for (var i = 0; i < elm.length; i++) {
                    if (elm.item(i).type == "checkbox" && elm.item(i) != activeChk && elm.item(i) != fullRightsChk) {
                        elm.item(i).checked = false;                      
                    }
                }
            }
        }

        function checkFullRights() {
            var retValue;
            if (DenyMalInput()) {
                var activeChk = document.getElementById('<%=chkActive.ClientID%>');
                var fullRightsChk = document.getElementById('<%=chkFullRights.ClientID%>');
                var elm = document.getElementsByTagName('input');

                if (fullRightsChk.checked) {
                    for (var i = 0; i < elm.length; i++) {
                        if (elm.item(i).type == "checkbox" && elm.item(i) != activeChk && elm.item(i) != fullRightsChk) {
                            if (!elm.item(i).checked) {
                                alert('Since some rights are denied for this role, full rights cannot be granted. Please uncheck Full rights and save.');
                                retValue = false;
                                break;
                            }
                            else {
                                retValue = true;
                            }
                        }

                    }
                }
                else {
                    retValue = true;
                }
            }
            else {
                retValue = false;
            }
            return retValue;
        }

        function checkViewPermission(viewId, addId, editId, delId, printId) {

            if (!viewId.checked) {
                if (addId.checked || editId.checked || delId.checked || printId.checked) {
                    viewId.checked = true;
                }
                else {
                    viewId.checked = false;
                }
            }
        }           

    </script>
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Manage Roles</span></div>
                <div class="button_holder">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="roleSave" TabIndex="5" CommandName="SaveRole"
                        OnClientClick="return checkFullRights();" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" OnClick="btnCancel_Click">
                    </asp:Button>
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="multi_row_elements">
                <label>
                    Role Name:</label>
                <asp:TextBox ID="txtRoleName" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox><asp:HiddenField
                    ID="hdnRoleId" runat="server" />
                <span class="mandatory_sign">*</span>
                <asp:RequiredFieldValidator ID="reqRoleName" ControlToValidate="txtRoleName" runat="server"
                    ErrorMessage="Role name is required" ValidationGroup="roleSave" Display="None"></asp:RequiredFieldValidator>
                <div class="cls">
                </div>
                <label>
                    Description:</label>
                <asp:TextBox ID="txtDesc" runat="server" MaxLength="250" TextMode="MultiLine" TabIndex="2" />
                <div class="cls">
                </div>
                <label>
                    Active</label>
                <asp:CheckBox ID="chkActive" runat="server" Checked="true" TabIndex="3" />
                <div class="cls">
                </div>            
                <label>
                    Full Rights</label>
                <asp:CheckBox ID="chkFullRights" runat="server" Text="&nbsp;&nbsp;Full Rights" TabIndex="5" />
                <div class="cls">
                </div>
                <asp:UpdatePanel ID="upRoles" runat="server">
                    <ContentTemplate>
                        <div class="grid">
                            <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="false" DataKeyNames="MenuID,IsScreen"
                                OnRowDataBound="gvRoles_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Menu Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="align_left cell_padding"
                                        ItemStyle-CssClass="align_left cell_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMenuName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderText="View" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" CssClass="chkViewClass" Checked='<%# Eval("ViewRights") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAdd"  runat="server" Checked='<%# Eval("AddRights") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEdit"  runat="server" Checked='<%# Eval("UpdateRights") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete"  runat="server" Checked='<%# Eval("DeleteRights") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPrint" runat="server" Checked='<%# Eval("PrintRights") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="grid_header" />
                            </asp:GridView>
                            <asp:CustomValidator ID="cvRoles" runat="server" ErrorMessage=" " ValidationGroup="roleSave"
                                Display="None" OnServerValidate="IsPermissionChecked"></asp:CustomValidator>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:ValidationSummary ID="vsRoles" ValidationGroup="roleSave" runat="server" DisplayMode="BulletList"
                    ShowMessageBox="true" ShowSummary="false" />
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
       
        var chkFullRightsBox = document.getElementById('<%=chkFullRights.ClientID%>');

        function pageLoad(sender, args) {
            $(function () {
                $("span.chkViewClass input").each(function () {
                    if (!$(this).is(":checked")) {
                        var $tr = $(this).parent().parent().parent();
                        $("td input[type='checkbox']", $tr).not("span.chkViewClass input").each(function () {
                            $(this)[0].checked = false;
                            $(this)[0].disabled = true;
                        });
                    }
                    else {
                        var $tr = $(this).parent().parent().parent();
                        $("td input[type='checkbox']", $tr).not("span.chkViewClass input").each(function () {
                            $(this)[0].disabled = false;
                        });
                    }
                });

                $("span.chkViewClass input").click(function () {
                    if (!$(this).is(":checked")) {
                        var $tr = $(this).parent().parent().parent();
                        $("td input[type='checkbox']", $tr).not("span.chkViewClass input").each(function () {
                            $(this)[0].checked = false;
                            $(this)[0].disabled = true;
                            chkFullRightsBox.checked = false;
                        });
                    }
                    else {
                        var $tr = $(this).parent().parent().parent();
                        $("td input[type='checkbox']", $tr).not("span.chkViewClass input").each(function () {
                            $(this)[0].disabled = false;
                        });
                    }
                });

                $("table tbody tr td input").click(function () {
                    if (!$(this).is(":checked")) {
                        chkFullRightsBox.checked = false;
                    }
                });
            });
        }
    </script>
</asp:Content>
