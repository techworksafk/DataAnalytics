<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="DAnalytics.Web.SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Data Analytics :Login</title>
    <link href="Styles/Login_Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Jquery/jquery-1.4.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mainwrap">
        <div class="loginbox">
            <label>
                <strong>Login</strong></label>
            <div class="input_text_holder">
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </div>
            <br clear="all" />
            <label>
                <strong>Password</strong></label>
            <div class="input_text_holder">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click" />
            <br clear="all" />
            <div class="action_options">
                <input name="" type="checkbox" value="" id="chkRememberMe" /><label class="auto_width">Remember Me</label>
                <a href="javascript:void(0);" onclick="OpenWin();">Forgot Password?</a>
                <div class="cls">
                </div>
            </div>
            <br clear="all" />
        </div>
        <!-- // FOOTER WRAPPER STARTS HERE \\ -->
        <div id="footer">&copy; 2014<strong> Geoesensys</strong>. Rights Reserved.<br clear="all" />
          </div>
        <!-- // FOOTER WRAPPER ENDS HERE \\ -->
        <div class="cls">
        </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            CheckCookie();
            $("[id$='btnLogin']").click(function () {
                if ($("[id$='chkRememberMe']").is(":checked")) {
                    var userName = $("[id$='txtUserName']").val();
                    var pwd = $("[id$='txtPassword']").val();
                    SetCookie("UserName", userName, 365);
                    SetCookie("Password", pwd, 365);
                }
            });

            function SetCookie(c_name, value, exdays) {
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + exdays);
                var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
                document.cookie = c_name + "=" + c_value;
            }

            function GetCookie(c_name) {
                var i, x, y, ARRcookies = document.cookie.split(";");
                for (i = 0; i < ARRcookies.length; i++) {
                    x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                    y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                    x = x.replace(/^\s+|\s+$/g, "");
                    if (x == c_name) {
                        return unescape(y);
                    }
                }
            }

            function CheckCookie() {
                var username = GetCookie("UserName");
                var pwd = GetCookie("Password");
                if (username != null && username != "") {
                    $("[id$='txtUserName']").val(username);
                }
                if (pwd != null && pwd != "") {
                    $("[id$='txtPassword']").val(pwd);
                }
            }
        });

        function OpenWin() {
            window.open("ForgotPassword.aspx", 'fgtpswd', "status=0,toolbar=0,height=200px,width=400px");
        }
    </script>
</body>
</html>
