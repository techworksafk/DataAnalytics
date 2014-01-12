<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tmpl_BoreHoleAttributes.ascx.cs"
    Inherits="DAnalytics.Web.template.tmpl_BoreHoleAttributes" %>
<asp:GridView ID="gvBoreHole" runat="server" AutoGenerateColumns="false" Width="100%"
    EmptyDataText="No records to display">
    <Columns>
        <asp:TemplateField HeaderText="Borehole">
            <ItemTemplate>
                <span title="Borehole">
                    <%#Eval("BoreHoleName")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date and Time">
            <ItemTemplate>
                <span title="Reading Date and Time">
                    <%# String.Format("{0:dd-MMM-yyyy HH:mm}", Eval("ReadingDateTime"))%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Device">
            <ItemTemplate>
                <span title="DeviceID">
                    <%#Eval("DeviceID")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CH4">
            <ItemTemplate>
                <span title="CH4">
                    <%#Eval("CH4")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CO2">
            <ItemTemplate>
                <span title="CO2">
                    <%#Eval("CO2")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="O2">
            <ItemTemplate>
                <span title="O2">
                    <%#Eval("O2")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="VOC">
            <ItemTemplate>
                <span title="VOC">
                    <%#Eval("VOC")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="H2S">
            <ItemTemplate>
                <span title="H2S">
                    <%#Eval("H2S")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CO">
            <ItemTemplate>
                <span title="CO">
                    <%#Eval("CO")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Borehole Pressure">
            <ItemTemplate>
                <span title="Borehole Pressure">
                    <%#Eval("Borehole_Pressure")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Atmospheric Pressure">
            <ItemTemplate>
                <span title="Atmospheric Pressure">
                    <%#Eval("Atmospheric_Pressure")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pressure Diff">
            <ItemTemplate>
                <span title="Pressure Diff">
                    <%#Eval("Pressure_Diff")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Temperature">
            <ItemTemplate>
                <span title="Temperature">
                    <%#Eval("Temperature")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Water Level">
            <ItemTemplate>
                <span title="Water Level">
                    <%#Eval("Water_Level")%></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Battery">
            <ItemTemplate>
                <span title="Battery">
                    <%#Eval("Battery")%></span>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="grid_header" />
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView>
