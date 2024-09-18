<%@ Page Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %>

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
                                        <h5 class="text-login">Forget Password</h5>
                                    </div>
                                    <div class="mt-4">
                                        <div class="mb-3">
                                            <label for="username" class="form-label">Enter Email or Mobile No.</label>
                                            <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" CssClass="form-control" placeholder="User Name" autocomplete="off" onkeydown="return submitOnEnter(event);">
                                            </asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <div class="float-end"><a href="Index.aspx" class="text-muted">Back To Login</a> </div>
                                          
                                        </div>
                                        
                                        <div class="mt-4">
                                            <asp:Button ID="btnForgetPassword" TabIndex="3" runat="server" Text="Forget" CssClass="btn btn-success w-100" OnClick="btnForgetPassword_Click" onkeydown="return submitOnEnter(event);" />
                                        </div>
                                         <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
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


    <!-- /.position-relative -->
    <script src="assets/js/md5.js"></script>
    <script type="text/javascript">
        function md5auth(seed) {
            var username1 = document.getElementById('<%=txtUserName.ClientID %>');
          
            if (username1.value === '') {
                alert("Please enter valid username"); username1.focus();
                return false;
            }
        }
    </script>
 

</asp:Content>
