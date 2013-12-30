<%@ Page Title="Data Analytics::Dashboard" Language="C#" MasterPageFile="~/Master/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DAnalytics.Web.Home.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right">
        <div class="middle_box float_left">
            <div class="middle_box_middle">
                <div class="middle_box_head">
                    Data Analytics </div>
                <div class="middle_box_content">
                    <div class="status v_separator" id="mStatus" runat="server">
                        <div class="status_title">
                            General Queue</div>
                        <div class="status_flag task">
                            <span class="status_text">Unassigned</span><span class="task_count" id="mUnAssTaskCount"
                                runat="server"></span></div>
                        <div class="status_flag overdue">
                            <span class="status_text">Overdue</span><span class="task_count" id="mOverDueTaskCount"
                                runat="server"></span></div>
                        <div class="status_flag critical">
                            <span class="status_text">Critical</span><span class="task_count" id="mCriticalTaskCount"
                                runat="server"></span></div>
                    </div>
                    <div class="status no_margin" id="uStatus" runat="server">
                        <div class="status_title">
                            My Transcripts</div>
                        <div class="status_flag task">
                            <span class="status_text">Assigned</span><span class="task_count" id="uAssTaskCount"
                                runat="server"></span></div>
                        <div class="status_flag overdue">
                            <span class="status_text">Overdue</span><span class="task_count" id="uOverDueTaskCount"
                                runat="server"></span></div>
                        <div class="status_flag critical">
                            <span class="status_text">Critical</span><span class="task_count" id="uCriticalTaskCount"
                                runat="server"></span></div>
                    </div>
                    <div class="cls">
                    </div>
                </div>
            </div>
            <div class="middle_box_right">
            </div>
        </div>
        <div class="cls">
        </div>
    </div>
</asp:Content>
