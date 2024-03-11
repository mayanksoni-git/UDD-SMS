<%@ Page Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="IndexUser.aspx.cs" Inherits="IndexUser" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       
    <div class="position-relative">
        <div id="login-box" class="login-box visible widget-box no-border">
            <div class="widget-body">
                <div class="widget-main">
                    <h4 class="header blue lighter bigger">
                        <i class="ace-icon fa fa-coffee green"></i>
                        Please Enter Credentials
                    </h4>

                    <div class="space-6"></div>
                    <fieldset>
                        <%--<label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="User Name" autocomplete="off" >
                                       
                                </asp:TextBox>
                                <i class="ace-icon fa fa-user"></i>
                            </span>
                        </label>--%>

                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtMobileNo" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control" placeholder="Mobile Number" autocomplete="off" MaxLength="10">
                                       
                                </asp:TextBox>
                                <i class="ace-icon fa fa-user"></i>
                            </span>
                        </label>

                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">

                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" autocomplete="off" TextMode="Password">
                                </asp:TextBox>
                                <i class="ace-icon fa fa-lock"></i>
                            </span>
                        </label>
                        
                        <div class="clearfix">
                            
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="width-35 pull-right btn btn-sm btn-primary" OnClick="btnLogin_Click" style="left: 0px; top: 0px" />

                        </div>

                        <div class="space-4"></div>
                    </fieldset>


                </div>
                <!-- /.widget-main -->

                <div class="toolbar clearfix">
                    <div>
                        <a href="#" data-target="#forgot-box" class="forgot-password-link">
                            <i class="ace-icon fa fa-arrow-left"></i>
                            I Want To Register
                        </a>
                    </div>

                    <div>
                        <a href="Index.aspx" class="user-signup-link">Switch To Admin Login
													<i class="ace-icon fa fa-arrow-right"></i>
                        </a>
                    </div>
                </div>
            </div>
            <!-- /.widget-body -->
        </div>
        <!-- /.login-box -->

        <div id="forgot-box" class="forgot-box widget-box no-border">
            <div class="widget-body">
                <div class="widget-main">
                    <h4 class="header red lighter bigger">
                        <i class="ace-icon fa fa-key"></i>
                        New Vendor Registration
                    </h4>

                    <div class="space-6"></div>
                    <p>
                        Enter your details to begin:
                    </p>

                    <fieldset>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtMobileReg" runat="server" CssClass="form-control" placeholder="Registred Mobile No" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                <i class="ace-icon fa fa-phone"></i>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:DropDownList ID="ddlFirmType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Properiter" Value="P" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Joint Venture" Value="J"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtFirmName" runat="server" CssClass="form-control" placeholder="Name Of Properiter Firm"></asp:TextBox>
                                <i class="ace-icon fa fa-newspaper-o"></i>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtMobileNoAltername1" runat="server" CssClass="form-control" placeholder="Mobile No (Properiter Firm)" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                <i class="ace-icon fa fa-phone"></i>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtFirmNameJV" runat="server" CssClass="form-control" placeholder="Name Of JV Firm"></asp:TextBox>
                                <i class="ace-icon fa fa-newspaper-o"></i>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtMobileNoAltername2" runat="server" CssClass="form-control" placeholder="Mobile No (JV Firm)" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                <i class="ace-icon fa fa-phone"></i>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control" placeholder="Contact Person Name"></asp:TextBox>
                                <i class="ace-icon fa fa-lock"></i>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtPasswordReg" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                <i class="ace-icon fa fa-lock"></i>
                            </span>
                        </label>

                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtPasswordRegRe" runat="server" CssClass="form-control" placeholder="Repeate Password" TextMode="Password"></asp:TextBox>
                                <i class="ace-icon fa fa-retweet"></i>
                            </span>
                        </label>

                        <label class="block">
                            <input type="checkbox" class="ace" />
                            <span class="lbl">I accept the
															<a href="#">User Agreement</a>
                            </span>
                        </label>

                        <div class="space-24"></div>

                        <div class="clearfix">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="width-35 pull-right btn btn-sm btn-danger" OnClick="btnRegister_Click" />
                        </div>
                    </fieldset>
                </div>
                <!-- /.widget-main -->

                <div class="toolbar center">
                    <a href="#" data-target="#login-box" class="back-to-login-link">Back to Login
												<i class="ace-icon fa fa-arrow-right"></i>
                    </a>
                </div>
            </div>
            <!-- /.widget-body -->
        </div>
        <!-- /.forgot-box -->
    </div>
    <!-- /.position-relative -->

<%--    <script>
        function openPage(obj) {
            window.open("Emagazine.aspx", "_blank", "", true);
        }
    </script>--%>
</asp:Content>
