<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tmpl_CH4Max.ascx.cs"
    Inherits="DAnalytics.Web.template.tmpl_CH4Max" %>
<asp:GridView ID="gvBoreHoles" runat="server" AutoGenerateColumns="false" Width="100%"
    EmptyDataText="No records to display">
    <Columns>
        <asp:TemplateField HeaderText="Borehole">
            <ItemTemplate>
                <span title="Borehole">
                    <%#Eval("BoreHoleName")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CH4">
            <ItemTemplate>
                <span title="CH4">
                    <%#Eval("CH4")%></span>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="grid_header" />
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView>
