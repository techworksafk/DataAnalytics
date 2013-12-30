<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="DAnalytics.Web.ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>   
    Forgot Password 
    </title>
     <link type="text/css" rel="Stylesheet" href="../Styles/Login_Style.css" />
    <link type="text/css" rel="Stylesheet" href="../Styles/Site.css" />
</head>
<body class="popupBody">
    <form id="form1" runat="server">
    <div class="float_div_popup">
        <div class="inner_content_holder marginLeft7">
            <div class="transcripts_details_header">
                <div class="template_head">
                    <span>Forgot&nbsp;</span>Password</div>
                <div class="button_holder">
                    <asp:Button ID="btnGetPswd" runat="server" Text="Get Password" 
                        ValidationGroup="Save" CssClass="button_large" onclick="btnGetPswd_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="window.close();" />
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="inner_float_div">
                <label>
                    Email address:</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <span class="mandatory_sign">*</span>
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="Please enter email address"
                    Display="None" ControlToValidate="txtEmail" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regxEmail" runat="server" ErrorMessage="Invalid email address"
                    ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="None" ValidationGroup="Save"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="vsPswd" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="Save" />
                <div class="cls">
                </div>
            </div>
            <div class="cls">
            </div>
        </div>
        <div class="cls">
        </div>
    </div>
    </form>
</body>
</html>
