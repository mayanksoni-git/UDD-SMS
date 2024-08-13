<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html>
<html lang="en" data-layout="vertical" data-topbar="light" data-sidebar="light" data-sidebar-size="lg" data-sidebar-image="none" data-preloader="disable">
<head runat="server">
    <meta charset="utf-8" />
   
    <title>404! Page Not Found</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta content="Premium Multipurpose Admin & Dashboard Template" name="description" />
    <meta content="Themesbrand" name="author" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="assets/images/favicon.ico">

    <!-- Layout config Js -->
    <script src="assets/js/layout.js"></script>
    <!-- Bootstrap Css -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Icons Css -->
    <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <!-- App Css-->
    <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
    <!-- custom Css-->
    <link href="assets/css/custom.min.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <div  class="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
    <div class="position-relative">
        <div id="login-box" class="login-box visible widget-box no-border">
            <div class="widget-body">
                <div class="widget-main">
                    <strong class="header blue lighter bigger" id="texdec">
                        <i class="ace-icon fa fa-coffee green"></i>
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;404<br />
                        Page Not Found
                    </strong>
                    <br />
                     <a class="header blue lighter bigger" id="BtnHome"  href="/Index.aspx">
                        <i class="ace-icon fa fa-coffee green"></i>
                      Home
                    </a>
                    <div class="space-6"></div>
                    <fieldset>
                        <%--<label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="User Name" autocomplete="off" >
                                       
                                </asp:TextBox>
                                <i class="ace-icon fa fa-user"></i>
                            </span>
                        </label>--%>


                        <div class="space-4"></div>
                    </fieldset>


                </div>
                <!-- /.widget-main -->

               
            </div>
            <!-- /.widget-body -->
        </div>
        <!-- /.login-box -->

    </div>

         <footer class="footer">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="text-sm-center text-md-start">
                                <p class="mb-0">
                                    ©
                            <script>document.write(new Date().getFullYear())</script>
                                    SMS | MARGSOFT Technologies (P) Ltd
                                </p>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="text-sm-center text-md-end">
                                <p class="mb-0">Powered by <a href="Javascript:void(0)">MARGSOFT Technologies (P) Ltd.</a> </p>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    <!-- /.position-relative -->
    <style>

        #BtnHome{
                background: #e85122;
    padding: 15px;
    margin-top: 53px;
    line-height: 76px;
    color: white;
    font-size: 18px;
    border-radius: 10px;
    margin-left: 40%;
        }
        #texdec {
            font-size: 70px;
            color: wheat;
            font-weight: bold;
            text-shadow: 1px 1px 10px;
            animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: infinite;
        }

        @keyframes example {
            0% {
                text-shadow: 2px 5px 10px red;
            }

            25% {
                text-shadow: 3px 10px 13px White;
            }

            50% {
                text-shadow: 2px 5px 10px Orange;
            }

            100% {
                text-shadow: 1px 1px 10px blue;
            }
        }
    </style>
    <script src="assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/libs/simplebar/simplebar.min.js"></script>
    <script src="assets/libs/node-waves/waves.min.js"></script>
    <script src="assets/libs/feather-icons/feather.min.js"></script>
    <script src="assets/js/pages/plugins/lord-icon-2.1.0.js"></script>
    <script src="assets/js/plugins.js"></script>

    <!-- particles js -->
    <script src="assets/libs/particles.js/particles.js"></script>
    <!-- particles app js -->
    <script src="assets/js/pages/particles.app.js"></script>
    <!-- password-addon init -->
    <script src="assets/js/pages/password-addon.init.js"></script>
</body>
</html>
