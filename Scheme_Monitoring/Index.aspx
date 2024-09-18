<%@ Page Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
    </cc1:ToolkitScriptManager>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>

    <!-- auth-page content -->
    <div class="auth-page-content overflow-hidden pt-lg-5">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card overflow-hidden border-0">
                        <div class="row g-0">
                            <div class="col-lg-6">
                                <div class="p-lg-5 p-4 auth-one-bg h-100">
                                    <div class="bg-overlay"></div>
                                    <div class="position-relative h-100 d-flex flex-column">
                                        <div class="mb-4">
                                            <a href="Index.aspx" class="d-block">
                                                <img src="assets/images/logo-light.png" alt="" class="img-fluid">
                                            </a>
                                        </div>
                                        <p class="text-white">Introducing the Scheme Monitoring System: A Unified Portal for Government Schemes under the Directorate of Local Bodies, Jal Nigam, and SUDA. This innovative platform serves as an information hub, offering comprehensive details on schemes across various directorates. At its core lies a smart dashboard, providing real-time insights into critical metrics. </p>
                                        <p class="text-white">Within this dashboard, users can access two main categories of statistics: Dashboard Stats, divided into Physical Units and Financial Amounts. Under Physical Units, metrics include the total number of Approved Projects, Ongoing Projects, Completed Projects, and Utilization Certificates Received. In the Financial category, users can track the Total Approved Amount, Total Released Amount, Total Spent Amount, and the Balance Amount yet to be released. </p>
                                        <%--<p class="text-white">Moreover, this platform serves as a Gateway to Insights, enabling users to delve deeper into project statuses, financial standings, and scheme-wise name, values and units. By seamlessly navigating through data, stakeholders can make informed decisions and drive strategic direction effectively. </p>--%>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->

                            <div class="col-lg-6">
                                <div class="p-lg-5 p-4">
                                    <div>
                                        <h5 class="text-login">Signin</h5>
                                    </div>
                                    <div class="mt-4">
                                        <div class="mb-3">
                                            <label for="username" class="form-label">Username</label>
                                            <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" CssClass="form-control" placeholder="User Name" autocomplete="off" onkeydown="return submitOnEnter(event);">
                                            </asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <div class="float-end"><a href="ForgetPassword.aspx" class="text-muted">Forgot password?</a> </div>
                                            <label class="form-label" for="password-input">Password</label>
                                            <div class="position-relative auth-pass-inputgroup mb-3">
                                                <asp:TextBox ID="txtPassowrd" TabIndex="2" runat="server" CssClass="form-control pe-5 password-input" placeholder="Password" onkeydown="return submitOnEnter(event);" autocomplete="off" TextMode="Password">
                                                </asp:TextBox>
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="password-addon"><i class="ri-eye-fill align-middle"></i></button>
                                            </div>
                                        </div>
                                        <div class="form-check">
                                            <input class="form-check-input" type="checkbox" value="" id="auth-remember-check">
                                            <label class="form-check-label" for="auth-remember-check">Remember me</label>
                                        </div>
                                        <div class="mt-4">
                                            <asp:Button ID="btnLogin" TabIndex="3" runat="server" Text="Login" CssClass="btn btn-success w-100" OnClick="btnLogin_Click" onkeydown="return submitOnEnter(event);" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                    </div>
                    <!-- end card -->
                </div>
                <!-- end col -->

            </div>
            <!-- end row -->
        </div>
        <!-- end container -->
    </div>
    <!-- end auth page content -->

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
                            Please Select Role (Additional Charge)
                        </h4>

                        <div class="space-4"></div>

                        <fieldset>
                            <label class="block clearfix" runat="server" id="Label1">
                                <span class="block input-icon input-icon-right">
                                    <asp:DropDownList ID="ddlAdditionalDivisionAndRole" runat="server" CssClass="form-control">
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
        function submitOnEnter(event) {
            if (event.keyCode === 13) { // 13 is the Enter key
                event.preventDefault(); // Prevent the default action of Enter key
                document.getElementById('<%=btnLogin.ClientID%>').click(); // Trigger form submission
                return false; // Return false to ensure no other actions are triggered
            }
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
