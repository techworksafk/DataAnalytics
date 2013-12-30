<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="RoleList.aspx.cs" Inherits="DAnalytics.Web.Admin.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    Roles</div>
                <div class="button_holder">
                    <asp:Button ID="btnNew" runat="server" Text="New Role" TabIndex="1" CommandName="NewRole"
                        OnClick="btnNew_Click" />
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="grid">
                <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="false" TabIndex="2"
                    Width="100%" OnRowCommand="gvRoles_RowCommand" OnRowDataBound="gvRoles_RowDataBound">
                    <HeaderStyle CssClass="grid_header" />
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="Role Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="editrole" Text='<%# Eval("RoleName") %>'
                                    CommandArgument='<%# Eval("RoleId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Active" DataField="IsActive"></asp:BoundField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="align_center" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/Images/btnDel.gif" OnClientClick="javascript: return confirm('Are you sure you want to delete this role?');"
                                    CommandArgument='<%#Eval("RoleId") %>' CommandName="DeleteRole" />
                                <asp:HiddenField ID="hdnSystemDefined" runat="server" Value='<%#Eval("IsSystemDefined") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="cls">
            </div>
        </div>
        <div class="cls">
        </div>
    </div>
</asp:Content>
