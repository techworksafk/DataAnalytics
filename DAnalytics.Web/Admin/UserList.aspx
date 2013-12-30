<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="UserList.aspx.cs" Inherits="DAnalytics.Web.Admin.UserList" %>

<%@ Register Src="../UserControls/GridPager.ascx" TagName="GridPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Users</span></div>
                <div class="button_holder">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex="1" CommandName="ViewUser"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btnNewUser" runat="server" Text="New User" TabIndex="6" CommandName="NewUser"
                        OnClick="btnNewUser_Click" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="cls"></div>
            <div class="row_elements">
                <label>
                    Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="input_text input_small" TabIndex="2"
                    onkeypress="return DoSearch(event);"></asp:TextBox>
                <script language="javascript" type="text/javascript">
                    function DoSearch(event) {
                        var btnSearch = document.getElementById('<%=btnSearch.ClientID %>');
                        return OverrideDefaultEvent(event, btnSearch);
                    }
                </script>
                <label>
                    Role:</label>
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="dropdown_box" TabIndex="3">
                </asp:DropDownList>
                <label>
                    Active:</label>
                <asp:DropDownList ID="ddlActive" runat="server" CssClass="dropdown_box" TabIndex="4"
                    Width="50px">
                    <asp:ListItem Text="All" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
           <div class="cls"></div>
            </div>
            <div class="cls"></div>
            <div class="grid">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" PageSize="10" GridLines="None" ShowFooter="true" TabIndex="7"
                    AllowSorting="true" OnRowCommand="gvUsers_RowCommand" OnSorting="gvUsers_OnSorting"
                    AlternatingRowStyle-CssClass="alt">
                    <HeaderStyle CssClass="grid_header" />
                    <Columns>
                        <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            SortExpression="UserName" HeaderStyle-CssClass="grid_header">
                            <ItemTemplate>
                                <asp:LinkButton ID="hlUserID" runat="server" CommandName="EditUsers" Text='<%#Eval("UserName") %>'
                                    CommandArgument='<%#Eval("UserID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                            HeaderStyle-CssClass="grid_header" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" HeaderStyle-CssClass="grid_header" />                       
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-CssClass="grid_header" />                      
                        <asp:BoundField DataField="IsActive" HeaderText="Active" SortExpression="IsActive"
                            ItemStyle-CssClass="cell_last" HeaderStyle-CssClass="grid_header header_last" />
                    </Columns>
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            </div>
            <div class="cls">
            </div>
            <br clear="all" />
               <div class="pagination">
            <uc1:GridPager ID="GridPager1" runat="server" ControlToBind="gvUsers" OnGridPagerChanged="Page_Changed" PageNum="1" />
            </div>
            <div class="cls">
            </div>
        </div>
    </div>
</asp:Content>
