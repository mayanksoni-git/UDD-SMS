<%@ Page Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="IndexUserOTP.aspx.cs" Inherits="IndexUserOTP" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
    </cc1:ToolkitScriptManager>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
    <div class="position-relative">
        <div id="login-box" class="login-box visible widget-box no-border">
            <div class="widget-body">
                <div class="widget-main">
                    <h4 class="header blue lighter bigger">
                        <i class="ace-icon fa fa-coffee green"></i>
                        Please Select Your Details
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
                                <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                            </span>
                        </label>
                        <div class="clearfix">
                            <asp:Button ID="btnGetEmployee" runat="server" Text="Get Employee" CssClass="width-35 pull-right btn btn-sm btn-inverse" OnClick="btnGetEmployee_Click" />
                        </div>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:DropDownList ID="ddlPerson" runat="server" class="chosen-select form-control" data-placeholder="Choose a User..." AutoPostBack="true" OnSelectedIndexChanged="ddlPerson_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>
                        </label>
                        <label class="block clearfix">
                            <span class="block input-icon input-icon-right">
                                <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="OTP" MaxLength="5" autocomplete="off"></asp:TextBox>
                                <i class="ace-icon fa fa-phone"></i>
                            </span>
                        </label>

                        <div class="clearfix">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="width-35 pull-right btn btn-sm btn-primary" OnClick="btnLoginULB_Click" />

                        </div>

                        <div class="space-4"></div>
                    </fieldset>


                </div>
                <!-- /.widget-main -->

                <div class="toolbar clearfix">
                    <div>
                        <a href="#" data-target="#forgot-box" class="forgot-password-link">&nbsp;
                        </a>
                    </div>

                    <div>
                        <a href="Index.aspx" class="user-signup-link">Switch To Password Login
													<i class="ace-icon fa fa-arrow-right"></i>
                        </a>
                    </div>
                </div>
            </div>
            <!-- /.widget-body -->
        </div>
        <!-- /.login-box -->

    </div>
    <!-- /.position-relative -->

    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">
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
</asp:Content>
