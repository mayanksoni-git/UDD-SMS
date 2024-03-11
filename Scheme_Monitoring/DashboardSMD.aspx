<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="DashboardSMD.aspx.cs" Inherits="DashboardSMD" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <!-- /.ace-settings-container -->
                        <div class="page-header">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <h1>Dashboard
                                        <small>
                                            <i class="ace-icon fa fa-angle-double-right"></i>
                                            Overview &amp; Stats
                                        </small>
                                    </h1>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtMappingWith" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtMappingWith_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Project For Division" Value="D"></asp:ListItem>
                                            <asp:ListItem Text="Project For ULB" Value="U"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.page-header -->
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Scheme</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divZone" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divDistrict" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">District* </label>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divCircle" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divULB" runat="server" visible="false">
                                <div class="form-group">
                                    <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divDivision" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Project Code" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-green">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Total Projects</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/Project.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkTotalProjects" runat="server" Font-Bold="true" Text="0" OnClick="lnkTotalProjects_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-blue">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Water Supply</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/WaterSupply.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkWater" runat="server" OnClick="lnkWater_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-dark">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Sewarage</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/Sewerage.jpg" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkSewarage" runat="server" Font-Bold="true" OnClick="lnkSewarage_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-orange">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter"><b>Septage</b></h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/Septage.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkSeptage" runat="server" Font-Bold="true" OnClick="lnkSeptage_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-green3">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Drainage</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/dranage.jpg" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkDranage" runat="server" OnClick="lnkDranage_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-red">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Solid Waste</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/solid_waste.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkSolidWaste" runat="server" Font-Bold="true" OnClick="lnkSolidWaste_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="space-6"></div>
                        <h3 class="header smaller red">SNA Status
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-6 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Limit is Available and Payment Can Be Done</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox-data">
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Total Project</td>
                                                                        <td>Pipeline For Payment</td>
                                                                        <td>Available Limit</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkSNALimitAvailable" runat="server" Font-Bold="true" Text="0" OnClick="lnkSNALimitAvailable_Click"></asp:LinkButton></td>
                                                                        <td><asp:LinkButton ID="lnkSNALimitAvailablePipeline" runat="server" Font-Bold="true" Text="0" OnClick="lnkSNALimitAvailable_Click"></asp:LinkButton></td>
                                                                        <td><asp:LinkButton ID="lnkSNALimitAvailableLimit" runat="server" Font-Bold="true" Text="0" OnClick="lnkSNALimitAvailable_Click"></asp:LinkButton></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-6 pricing-box">
                                    <div class="widget-box widget-color-blue">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Payment is Pending Due To Unavailability Of SNA Limit</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox-data">
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Total Project</td>
                                                                        <td>Pipeline For Payment</td>
                                                                        <td>Available Limit</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkSNALimitNotAvailable" runat="server" OnClick="lnkSNALimitNotAvailable_Click"></asp:LinkButton></td>
                                                                        <td><asp:LinkButton ID="lnkSNALimitNotAvailablePipeline" runat="server" OnClick="lnkSNALimitNotAvailable_Click"></asp:LinkButton></td>
                                                                        <td><asp:LinkButton ID="lnkSNALimitNotAvailableLimit" runat="server" OnClick="lnkSNALimitNotAvailable_Click"></asp:LinkButton></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="space-6"></div>
                        <h3 class="header smaller red">Physical Component & Its Progress
                           
                        <div class="pull-right">
                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectType_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="dtOptions" runat="server" class="clearfix">
                                    <div class="pull-right grdPhysicalComponenttableTools-container">
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:GridView ID="grdPhysicalComponent" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPhysicalComponent_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="Component_Id" HeaderText="Component_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Projects">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblTotalProjectComp" runat="server" OnClick="lblTotalProjectComp_Click" Font-Bold="true" Text='<%# Eval("Total_Project") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Component">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkComponentName" runat="server" OnClick="lnkComponentName_Click" Font-Bold="true" Text='<%# Eval("Component_Unit") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Proposed" HeaderText="Proposed" />
                                            <asp:BoundField DataField="PhysicalProgress" HeaderText="Completed" />
                                            <asp:BoundField DataField="Functional" HeaderText="Functional" />
                                            <asp:BoundField DataField="NonFunctional" HeaderText="Non Functional" />
                                            <asp:BoundField DataField="Percentage_Cpmpleted" HeaderText="Completed Percentage" />
                                            <asp:BoundField DataField="Percentage_Cpmpleted_Functional" HeaderText="Completed Percentage (Functional)" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>


                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">

                            <div class="space-6"></div>
                            <h3 class="header smaller red">Projects Completion Status According To Target Month
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-red">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Completed Projects</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-icon">
                                                                    <i>
                                                                        <img src="assets/images/pmis/Completed.jpg" width="60px" height="60px" />
                                                                    </i>
                                                                </div>
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:LinkButton ID="lnkCompleted" runat="server" Font-Bold="true" Text="0" OnClick="lnkCompleted_Click"></asp:LinkButton></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-grey">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Ongoing Projects</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-icon">
                                                                    <i>
                                                                        <img src="assets/images/pmis/ongoing.jpg" width="60px" height="60px" />
                                                                    </i>
                                                                </div>
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:LinkButton ID="lnkOnGoing" runat="server" Font-Bold="true" Text="0" OnClick="lnkOnGoing_Click"></asp:LinkButton></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-green">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Completing In Current Month</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-icon">
                                                                    <i>
                                                                        <img src="assets/images/pmis/Progress_C.png" width="60px" height="60px" />
                                                                    </i>
                                                                </div>
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:LinkButton ID="lnkTarget_C" runat="server" Font-Bold="true" Text="0" OnClick="lnkTarget_C_Click"></asp:LinkButton></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-blue">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Completing In Next Month</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-icon">
                                                                    <i>
                                                                        <img src="assets/images/pmis/Progress_N.png" width="60px" height="60px" />
                                                                    </i>
                                                                </div>
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:LinkButton ID="lnkTarget_N" runat="server" OnClick="lnkTarget_N_Click"></asp:LinkButton></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row" runat="server" id="divCompletedBreakup" visible="false">
                                <div class="col-md-12">
                                    <div class="col-xs-6 col-sm-6 pricing-box">
                                        <div class="widget-box widget-color-red">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Completed Projects Physical</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-icon">
                                                                    <i>
                                                                        <img src="assets/images/pmis/Physical_Completed.png" width="60px" height="60px" />
                                                                    </i>
                                                                </div>
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:LinkButton ID="lnkPhysicalCompleted" runat="server" Font-Bold="true" Text="0" OnClick="lnkPhysicalCompleted_Click"></asp:LinkButton></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-6 pricing-box">
                                        <div class="widget-box widget-color-grey">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Completed Projects Financial</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-icon">
                                                                    <i>
                                                                        <img src="assets/images/pmis/Financial_Completed.png" width="60px" height="60px" />
                                                                    </i>
                                                                </div>
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:LinkButton ID="lnkFinancialCompleted" runat="server" Font-Bold="true" Text="0" OnClick="lnkFinancialCompleted_Click"></asp:LinkButton></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <%--<script src="assets/js/dataTables.colReorder.min.js"></script>--%>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPhysicalComponent').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPhysicalComponent')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPhysicalComponent')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: false,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null
                                    ],
                                    "aaSorting": [],
                                    //"bProcessing": true,
                                    //"bServerSide": true,
                                    //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                    //,
                                    //"sScrollY": "200px",
                                    //"bPaginate": false,
                                    //"sScrollX": "100%",
                                    //"sScrollXInner": "120%",
                                    //"bScrollCollapse": true,
                                    //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                    //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                    "iDisplayLength": 100,
                                    select: {
                                        style: 'multi'
                                    }
                                });
                        $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
                        new $.fn.dataTable.Buttons(myTable, {
                            buttons: [
                                {
                                    "extend": "colvis",
                                    "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    columns: ':not(:first):not(:last)'
                                },
                                {
                                    "extend": "copy",
                                    "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "csv",
                                    "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "excel",
                                    "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "pdf",
                                    "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "print",
                                    "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    autoPrint: true,
                                    message: 'This print was produced using the Print button for DataTables',
                                    exportOptions: {
                                        columns: ':visible'
                                    }
                                }
                            ]
                        });
                        myTable.buttons().container().appendTo($('.grdPhysicalComponenttableTools-container'));

                        //style the message box
                        var defaultCopyAction = myTable.button(1).action();
                        myTable.button(1).action(function (e, dt, button, config) {
                            defaultCopyAction(e, dt, button, config);
                            $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
                        });
                        var defaultColvisAction = myTable.button(0).action();
                        myTable.button(0).action(function (e, dt, button, config) {

                            defaultColvisAction(e, dt, button, config);
                            if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                                $('.dt-button-collection')
                                    .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                                    .find('a').attr('href', '#').wrap("<li />")
                            }
                            $('.dt-button-collection').appendTo('.grdPhysicalComponenttableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdPhysicalComponenttableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPhysicalComponent .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have TableTools or dataTables
                        //select/deselect all rows according to table header checkbox
                        var active_class = 'active';
                        /********************************/
                        //add tooltip for small view action buttons in dropdown menu
                        $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

                        //tooltip placement on right or left
                        function tooltip_placement(context, source) {
                            var $source = $(source);
                            var $parent = $source.closest('table')
                            var off1 = $parent.offset();
                            var w1 = $parent.width();

                            var off2 = $source.offset();
                            //var w2 = $source.width();

                            if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                            return 'left';
                        }
                        /***************/
                        $('.show-details-btn').on('click', function (e) {
                            e.preventDefault();
                            $(this).closest('tr').next().toggleClass('open');
                            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
                        });
                    }
                }
            })
        });
    </script>
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>
</asp:Content>

