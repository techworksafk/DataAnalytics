<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="UserManager.aspx.cs" Inherits="DAnalytics.Web.Admin.UserManager" %>

<%@ Register Assembly="skmValidators" Namespace="skmValidators" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="float_div_right_wide">
        <div class="inner_content_holder">
            <div class="transcripts_details_header">
                <div class="template_head">
                    User Management</div>
                <div class="button_holder">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click"
                        CommandName="SaveUser" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="Cancel"
                        OnClick="btnCancel_Click" />
                </div>
            </div>
            <div class="cls">
            </div>
            <div class="inner_float_div">
                <label>
                    Username:</label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="input_text2 input_medium"
                    MaxLength="50"></asp:TextBox>
                <span class="mandatory_sign">*</span>
                <asp:RequiredFieldValidator ID="reqUserName" runat="server" ErrorMessage="" Display="None"
                    ControlToValidate="txtUserName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regxUserName" runat="server" ErrorMessage=""
                    Display="None" ControlToValidate="txtUserName" ValidationExpression="^[a-zA-Z0-9][a-zA-Z0-9_.]{2,29}$"
                    ValidationGroup="Save"></asp:RegularExpressionValidator>
                <div class="cls">
                </div>
                <label>
                    First Name:</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="input_text2 input_medium"
                    MaxLength="50"></asp:TextBox>
                <span class="mandatory_sign">*</span>
                <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="" Display="None"
                    ControlToValidate="txtFirstName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <div class="cls">
                </div>
                <label>
                    Last Name:</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="input_text2 input_medium"
                    MaxLength="50"></asp:TextBox>
                <span class="mandatory_sign">*</span>
                <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="" Display="None"
                    ControlToValidate="txtLastName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <div class="cls">
                </div> 
                <label>
                    Select Roles:</label>
                <div class="checkbox_holder">
                    <asp:CheckBoxList ID="chklRoles" runat="server">
                    </asp:CheckBoxList>
                    <div class="cls">
                    </div>
                </div>
                <span class="mandatory_sign">*</span>
                <cc1:CheckBoxListValidator ID="chkListValidator" runat="server" ControlToValidate="chklRoles"
                                ErrorMessage="Select at least one role." SetFocusOnError="true" Display="None"
                                MinimumNumberOfSelectedCheckBoxes="1" ValidationGroup="Save">                           
                            </cc1:CheckBoxListValidator>
                <div class="cls">
                </div>
                <label>
                    Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input_text2 input_medium" MaxLength="100"></asp:TextBox>
                <span class="mandatory_sign">*</span>
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="" Display="None"
                    ControlToValidate="txtEmail" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regxEmail" runat="server" ErrorMessage="" ControlToValidate="txtEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"
                    ValidationGroup="Save"></asp:RegularExpressionValidator>
            </div>
            <div class="inner_float_div no_right_margin">
                <label>
                    Mobile:</label>
                <asp:TextBox ID="txtMobile" runat="server" CssClass="input_text2 input_medium" MaxLength="25"></asp:TextBox>
                <div class="cls">
                </div>         
            
                <label>
                    Active</label>
                <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                <div class="cls">
                </div>
                <%if (QUserID == 0)
                  {%>
                <div runat="server" id="divPassword">
                    <label>
                        Password:</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15"></asp:TextBox>
                    <span class="mandatory_sign">*</span>
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage="" Display="None"
                        ControlToValidate="txtPassword" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <div class="cls">
                    </div>
                    <label>
                        Retype Password:</label>
                    <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" MaxLength="15"></asp:TextBox>
                    <span class="mandatory_sign">*</span>
                    <asp:RequiredFieldValidator ID="reqRetypePassword" runat="server" ErrorMessage=""
                        Display="None" ControlToValidate="txtRetypePassword" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cmpPasswords" runat="server" ControlToCompare="txtPassword"
                        Display="None" ControlToValidate="txtRetypePassword" ErrorMessage="" ValidationGroup="Save"></asp:CompareValidator>
                </div>
                <%} %>
                <asp:LinkButton ID="lnkChangePwd" runat="server" Text="Change Password" CssClass="change_password"></asp:LinkButton>
                <asp:ValidationSummary ID="valSummState" runat="server" DisplayMode="BulletList"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
            </div>
            <div class="cls">
            </div>
        </div>
        <div runat="server" id="div1">
            <%if (QUserID != 0)
              {%>
            <div id="divChangePwd" style="display: none;">
                <h3>
                    Change Password</h3>
                <a href="#" class="close_popup" onclick="javascipt: Popup.hide('divChangePwd');">
                </a>
                <asp:Label ID="lblNewPwd" Text="Enter New Password:" runat="server"></asp:Label>
                <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Label ID="lblRetypeNewPwd" Text="Re-Type New Password:" runat="server"></asp:Label>
                <asp:TextBox ID="txtRetypeNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                <br clear="all" />
                <div class="popdiv_button_holder">
                    <input type="button" value="Save" id="btnChangePwd" />
                    <%--<input type="button" value="Cancel" id="btnCancelChangePwd" onclick="javascipt: Popup.hide('divChangePwd');" />--%></div>
            </div>
            <%} %>
        </div>
    </div>
    <div class="cls">
    </div>
    <%-- <uc1:usersjsmsg id="UsersJSMsg1" runat="server" showmessage="false" showerrormessage="false" />--%>
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPWD" runat="server" />
    <script language="javascript" type="text/javascript">

        $(function () {
            $("[id$='lnkChangePwd']").click(function () {
                Popup.ShowPopup('divChangePwd');
                return false;
            });

            $("[id$='btnChangePwd']").click(function () {              
                var alertmsg = "";
                var pswdflag = true;
                if ($("[id$='txtNewPwd']").val() == "") {
                    alertmsg += "- Enter New password";
                    pswdflag = false;
                }
                if ($("[id$='txtRetypeNewPwd']").val() == "") {
                    alertmsg += "\n- Enter confirm new password";
                    pswdflag = false;
                }
                if (pswdflag == false) {
                    alert(alertmsg);
                    return false;
                }

                if ($("[id$='txtNewPwd']").val() != $("[id$='txtRetypeNewPwd']").val()) {
                    alert("New password and confirm password do not match.");
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "../Admin/UserManager.aspx/ChangePassword",
                    async: false,
                    data: "{userId:'" + $("[id$='hdnUserID']").val() + "',newPwd:'" + $("[id$='txtRetypeNewPwd']").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.d == "1") { alert("-Password changed successfully."); Popup.hide('divChangePwd'); }
                            else if (data.d == "0") {
                                alert("-Password changing failed.");
                                $("[id$='txtNewPwd']").val("");
                                $("[id$='txtRetypeNewPwd']").val("");
                            }
                        }
                    }
                });
            });

            $("[class$='close_popup']").click(function () {
                Popup.hide('divChangePwd');
            });

            $("[id$='txtNewPwd']").val('<%= password %>');
            $("[id$='txtRetypeNewPwd']").val('<%= password %>');
        });
    </script>
</asp:Content>
