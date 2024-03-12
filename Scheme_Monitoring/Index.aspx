<%@ Page Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
    </cc1:ToolkitScriptManager>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>


    <div class="row">
        <div class="col-lg-12">
            <div class="text-center mt-sm-5 mb-4 text-white-50">
                <div>
                    <a href="index.html" class="d-inline-block auth-logo">
                        <img src="assets/images/logo-light.png" alt="" height="20">
                    </a>
                </div>
                <p class="mt-3 fs-15 fw-medium">Urban Development Department, Government of Uttar Pradesh</p>
            </div>
        </div>
    </div>
    <!-- end row -->

    <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6 col-xl-5">
            <div class="card mt-4">

                <div class="card-body p-4">
                    <div class="text-center mt-2">
                        <h5 class="text-primary">Welcome Back !</h5>
                        <p class="text-muted">Sign in to continue to SMS.</p>
                    </div>
                    <div class="p-2 mt-4">
                        <div class="mb-3">
                            <label for="username" class="form-label">Username</label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name" autocomplete="off">
                            </asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <div class="float-end">
                                <a href="auth-pass-reset-basic.html" class="text-muted">Forgot password?</a>
                            </div>
                            <label class="form-label" for="password-input">Password</label>
                            <div class="position-relative auth-pass-inputgroup mb-3">
                                <asp:TextBox ID="txtPassowrd" runat="server" CssClass="form-control pe-5 password-input" placeholder="Password" autocomplete="off" TextMode="Password">
                                </asp:TextBox>
                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="password-addon"><i class="ri-eye-fill align-middle"></i></button>
                            </div>
                        </div>
                        <div class="mt-4">
                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder="Captcha">
                            </asp:TextBox>
                        </div>

                        <div class="mt-4">
                            <div class="float-end">
                                <asp:ImageButton ImageUrl="~/refresh.png" runat="server" CausesValidation="false" />
                            </div>
                            <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                        </div>
                        <div class="mt-4">
                            <asp:CustomValidator ErrorMessage="Invalid. Please try again." OnServerValidate="ValidateCaptcha"
                                runat="server" />
                        </div>

                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="auth-remember-check">
                            <label class="form-check-label" for="auth-remember-check">Remember me</label>
                        </div>

                        <div class="mt-4">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success w-100" OnClick="btnLogin_Click" />
                        </div>

                        <div class="mt-4 text-center">
                            <div class="signin-other-title">
                                <h5 class="fs-13 mb-4 title">Sign In with</h5>
                            </div>
                            <div>
                                <button type="button" class="btn btn-primary btn-icon waves-effect waves-light"><i class="ri-facebook-fill fs-16"></i></button>
                                <button type="button" class="btn btn-danger btn-icon waves-effect waves-light"><i class="ri-google-fill fs-16"></i></button>
                                <button type="button" class="btn btn-dark btn-icon waves-effect waves-light"><i class="ri-github-fill fs-16"></i></button>
                                <button type="button" class="btn btn-info btn-icon waves-effect waves-light"><i class="ri-twitter-fill fs-16"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end card body -->
            </div>
            <!-- end card -->

            <div class="mt-4 text-center">
                <p class="mb-0">Don't have an account ? <a href="auth-signup-basic.html" class="fw-semibold text-primary text-decoration-underline">Signup </a></p>
            </div>

        </div>
    </div>
    <!-- end row -->

    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; position: center; width: 700px; height: 500px; margin-top: 90px;">
        <div class="row">
            <div class="col-xs-12">
                <div class="table-header">
                    Additional Charge Details              
                </div>
            </div>
        </div>
        <div class="position-relative">
            <div id="login-box-division" class="login-box visible widget-box no-border">
                <div class="widget-body">
                    <div class="widget-main">
                        <h4 class="header blue lighter bigger">
                            <i class="ace-icon fa fa-coffee green"></i>
                            Please Select Role
                        </h4>

                        <div class="space-4"></div>

                        <fieldset>
                            <label class="block clearfix" runat="server" id="Label1">
                                <span class="block input-icon input-icon-right">
                                    <asp:DropDownList ID="ddlAdditionalDivisionAndRole" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="ePayment and PMIS" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Payroll HR & Financial Accounting" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </label>
                            <div class="clearfix">
                                <asp:Button ID="btnLogin2" runat="server" Text="Login" CssClass="width-35 pull-right btn btn-sm btn-primary" OnClick="btnLogin2_Click" CausesValidation="false" />
                            </div>
                        </fieldset>

                    </div>
                    <div class="space-4"></div>
                </div>
                <!-- /.widget-main -->
            </div>
            <!-- /.widget-body -->
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-12">
                    <div class="form-group">
                        <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- /.position-relative -->
    <script src="assets/js/md5.js"></script>
    <script type="text/javascript">
        function md5auth(seed) {
            var username1 = document.getElementById('<%=txtUserName.ClientID %>');
            var password1 = document.getElementById('<%=txtPassowrd.ClientID %>');
            var password = password1.value;

            if (username1.value === '') {
                alert("Please enter valid username"); username1.focus();
                return false;
            }
            if (password1.value === '') {
                alert("Please enter valid password"); password1.focus();
                return false;
            }
            var hash = calcMD5(seed + (calcMD5(password).toUpperCase()));
            password1.value = hash.toUpperCase();
            return true;
        }
    </script>

    <script type="text/javascript">
        //Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
        //    jQuery(function ($) {
        //        debugger;
        //        $('#ctl00_ContentPlaceHolder1_btnLogin').onclick(function () {
        //            var password = $('#ctl00_ContentPlaceHolder1_txtPassowrd').val();
        //            $.ajax({
        //                type: "POST",
        //                url: "WebService.aspx/EncryptPassword",
        //                data: JSON.stringify({ 'Password': password }),
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                success: function (data) {
        //                    $('#ctl00_ContentPlaceHolder1_txtPassowrd').text(data.d);
        //                },
        //                error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                    alert(textStatus + ': ' + errorThrown);
        //                }
        //            });
        //        });
        //    })
        //})
    </script>

</asp:Content>
