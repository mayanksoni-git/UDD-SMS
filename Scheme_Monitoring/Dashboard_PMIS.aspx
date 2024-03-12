<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Dashboard_PMIS.aspx.cs" Inherits="Dashboard_PMIS" MaintainScrollPositionOnPostback="true" %>

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

                    <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <!-- /.ace-settings-container -->
                        <div class="page-header">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <h1>PMIS Dashboard							
                                       
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
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divNodalDeptWise">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="Div1" runat="server" class="clearfix">
                                        <div class="pull-right grdNodalDepttableTools-container">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdNodalDept" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdNodalDept_PreRender" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWork_NodalDepartment_Id" HeaderText="ProjectWork_NodalDepartment_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nodal Department">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblNodalDept" runat="server" OnClick="lblNodalDept_Click" Font-Bold="true" Text='<%# Eval("Person_Name") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotalProjects" runat="server" OnClick="lblTotalProjects_Click" Font-Bold="true" Text='<%# Eval("Total_Projects") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblTotalProjectsF" runat="server" OnClick="lblTotalProjectsF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Completed Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblCompleted" runat="server" OnClick="lblCompleted_Click" Font-Bold="true" Text='<%# Eval("Projects_Completed") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblCompletedF" runat="server" OnClick="lblCompletedF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ongoing Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOngoing" runat="server" OnClick="lblOngoing_Click" Font-Bold="true" Text='<%# Eval("Projects_onGoing") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblOngoingF" runat="server" OnClick="lblOngoingF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="Black" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divProjectTypeWise">
                            <div class="row">
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Total Projects
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoTotalProject" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoTotalProject_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
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
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-blue">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Water Supply
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoWaterSupply" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoWaterSupply_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
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
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-dark">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Sewarage
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoSewarage" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoSewarage_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
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

                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-blue">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Building Works
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoBuildingWork" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoBuildingWork_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/building-construction.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkBuilding" runat="server" Font-Bold="true" OnClick="lnkBuilding_Click"></asp:LinkButton></span>
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
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-orange">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter"><b>Septage</b>
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoSeptage" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoSeptage_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
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
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-green3">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Drainage
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoDrainage" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoDrainage_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
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
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-red">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Solid Waste
                                                <div style="float: right">
                                                    <asp:ImageButton ID="btnInfoSolidWaste" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoSolidWaste_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                </div>
                                            </h5>
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
                        </div>
                        <div class="space-6"></div>
                        <h3 class="header smaller red">Projects Progress Status (Based On Step 3 [Physical & Financial Progress])
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdPMISUpdation" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdPMISUpdation_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="Data_Type" HeaderText="Project Status" />
                                            <asp:BoundField DataField="Total_Projects" HeaderText="Total Projects" />
                                            <asp:TemplateField HeaderText="Progress 0%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation0" runat="server" OnClick="lblUpdation0_Click" Font-Bold="true" Text='<%# Eval("Zero") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo0" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo0_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 0 to 10%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation10" runat="server" OnClick="lblUpdation10_Click" Font-Bold="true" Text='<%# Eval("Less_10") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo10" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo10_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 10% to 20%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation20" runat="server" OnClick="lblUpdation20_Click" Font-Bold="true" Text='<%# Eval("BW_10_20") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo20" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo20_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 20% to 30%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation30" runat="server" OnClick="lblUpdation30_Click" Font-Bold="true" Text='<%# Eval("BW_20_30") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo30" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo30_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 30% to 40%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation40" runat="server" OnClick="lblUpdation40_Click" Font-Bold="true" Text='<%# Eval("BW_30_40") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo40" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo40_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 40% to 50%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation50" runat="server" OnClick="lblUpdation50_Click" Font-Bold="true" Text='<%# Eval("BW_40_50") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo50" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo50_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 50% to 60%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation60" runat="server" OnClick="lblUpdation60_Click" Font-Bold="true" Text='<%# Eval("BW_50_60") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo60" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo60_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 60% to 70%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation70" runat="server" OnClick="lblUpdation70_Click" Font-Bold="true" Text='<%# Eval("BW_60_70") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo70" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo70_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 70% to 80%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation80" runat="server" OnClick="lblUpdation80_Click" Font-Bold="true" Text='<%# Eval("BW_70_80") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo80" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo80_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 80% to 90%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation90" runat="server" OnClick="lblUpdation90_Click" Font-Bold="true" Text='<%# Eval("BW_80_90") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo90" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo90_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress More Than 90% and Less Than 100%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdationMore90" runat="server" OnClick="lblUpdationMore90_Click" Font-Bold="true" Text='<%# Eval("More_90") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfoMore90" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfoMore90_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress 100%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation100" runat="server" OnClick="lblUpdation100_Click" Font-Bold="true" Text='<%# Eval("More_100") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo100" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo100_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div id="chartContainerPhysical" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="chartContainerFinancial" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                        </div>

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

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Time Extention Analysis 
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Packages With Extention of Timeline</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/contractend.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkBondDateDelay" runat="server" Font-Bold="true" Text="0" OnClick="lnkBondDateDelay_Click"></asp:LinkButton></span>

                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li><i class="ace-icon fa fa-check green"></i>
                                                        The packages where extension of timeline (EOT) has been given.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-blue">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Packages Which Require Extention</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/contractend.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkBondDateDelayNotExtended" runat="server" OnClick="lnkBondDateDelayNotExtended_Click"></asp:LinkButton></span>

                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li><i class="ace-icon fa fa-check green"></i>
                                                        Packages Where Contract Has already ended but extention has not being given</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-dark">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Packages where Extension Timeline is over</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/LD.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkLD" runat="server" Font-Bold="true" Text="0" OnClick="lnkLD_Click"></asp:LinkButton></span>

                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li><i class="ace-icon fa fa-check green"></i>
                                                        List of packages where Extension of time line is over and further extension is required.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" Text="Display Graph For Data" CssClass="control-label no-padding-right"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:RadioButtonList runat="server" ID="rbtGraphFor" AutoPostBack="true" OnSelectedIndexChanged="rbtGraphFor_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="&nbsp;&nbsp;Total Projects&nbsp;&nbsp;" Value="A" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp;&nbsp;Ongoing Projects&nbsp;&nbsp;" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp;&nbsp;Completed Projects&nbsp;&nbsp;" Value="C"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="chartContainerFilter" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Liquidated Damage Analysis
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">LD Imposed In Packaged count / Amount (In Lakh)</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/LD_Imposed.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkLDImposed" runat="server" Font-Bold="true" Text="0" OnClick="lnkLDImposed_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">LD Withdrawan From Packaged count / Amount (In Lakh)</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/LD_Withdrawan.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkLDWithdrawan" runat="server" OnClick="lnkLDWithdrawan_Click"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--<div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-dark">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Projects With Financial Closure Pending</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Proj_Closure.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkFinancialClosure" runat="server" Font-Bold="true" Text="0" OnClick="lnkFinancialClosure_Click"></asp:LinkButton></span>

                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li><i class="ace-icon fa fa-check green"></i>
                                                        List of Projects where Physical Progress is Complete before 6 Months and Financial Clusure is Pending.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>


                        <div class="space-6"></div>
                        <h3 class="header smaller red">Issue Analysis
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <asp:GridView ID="grdIssueReportedGlobal" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdIssueReportedGlobal_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProjectIssue_Name" HeaderText="Issue" />
                                            <asp:TemplateField HeaderText="Total Issues">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkTotalIssuesGlobal" runat="server" OnClick="lnkTotalIssuesGlobal_Click" Font-Bold="true" Text='<%# Eval("Total_Isues") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-6">
                                    <div id="chartContainerIssue" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                                </div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Expenditure
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Expenditure In Current Month (In Cr)</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Expenditure_C.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkExp_C" runat="server" Font-Bold="true" Text="0" OnClick="lnkExp_C_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Expenditure In Previous Month (In Cr)</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Expenditure_P.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkExp_P" runat="server" OnClick="lnkExp_P_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Expenditure Overall (In Cr)</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Expenditure_C.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkExp_O" runat="server" Font-Bold="true" Text="0" OnClick="lnkExp_O_Click"></asp:LinkButton></span>
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

                        <div class="space-6"></div>
                        <h3 class="header smaller red"><span runat="server" id="FundReleaseDocsHeader">Fund Release GO Document Not Uploaded </span>
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">First GO Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/GO_NA.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkGO1" runat="server" Font-Bold="true" Text="0" OnClick="lnkGO1_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Second GO Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/GO_NA.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkGO2" runat="server" OnClick="lnkGO2_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Third GO Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/GO_NA.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkGO3" runat="server" Font-Bold="true" OnClick="lnkGO3_Click"></asp:LinkButton></span>
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

                        <div class="space-6"></div>
                        <h3 class="header smaller red"><span runat="server" id="PackageRelatedBondDocs">Project Package Related Bond Documents Not Available</span>
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Total Packages</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Package.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkTotalPgg" runat="server" Font-Bold="true" Text="0" OnClick="lnkTotalPgg_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Contract Bond Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/CB.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkCB" runat="server" OnClick="lnkCB_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Letter Of Intent (LOI)</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/loi.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkLOI" runat="server" Font-Bold="true" OnClick="lnkLOI_Click"></asp:LinkButton></span>
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
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Agreement with Stamp</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/CB_Stamp.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkCB_Stamp" runat="server" Font-Bold="true" Text="0" OnClick="lnkCB_Stamp_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Front Page of Contract Bond</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/CB.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkCBFront" runat="server" OnClick="lnkCBFront_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Schedule G Of Contract Bond</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/BG.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkCB_ScheduleG" runat="server" Font-Bold="true" OnClick="lnkCB_ScheduleG_Click"></asp:LinkButton></span>
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

                        <div class="space-6"></div>
                        <h3 class="header smaller red"><span runat="server" id="PackageRelatedOtherDocs">Project Package Related Other Document Not Available</span>
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Bank Guarantee Document Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/BG.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkBG" runat="server" Font-Bold="true" Text="0" OnClick="lnkBG_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Performance Security Document Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/BG.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkPS" runat="server" OnClick="lnkPS_Click"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-dark">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Moblization Advance Document Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/BG.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkMA" runat="server" Font-Bold="true" OnClick="lnkMA_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Time Extention Document Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/BG.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkTE" runat="server" Font-Bold="true" OnClick="lnkTE_Click"></asp:LinkButton></span>
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

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Package Wise Variation Document Uploaded
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-4 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Total Project For Which Variation Document Uploaded</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/variation_proj.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkVariationProject" runat="server" Font-Bold="true" Text="0" OnClick="lnkVariationProject_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Total Package For Which Variation Document Uploaded</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/variation_pkg.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkVariationPackage" runat="server" OnClick="lnkVariationPackage_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Variation Document Not Available</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/BG.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkVariationPending" runat="server" Font-Bold="true" OnClick="lnkVariationPending_Click"></asp:LinkButton></span>
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
                                    <asp:GridView ID="grdPhysicalComponent" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdPhysicalComponent_PreRender">
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
                            <h3 class="header smaller red">
                                <asp:LinkButton ID="lnkProjectStatusPopup" runat="server" Font-Bold="true" Text="Project Status" OnClick="lnkProjectStatusPopup_Click"></asp:LinkButton>
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
                                                                        <asp:LinkButton ID="lnkCompletedP" runat="server" Font-Bold="true" Text="0" OnClick="lnkCompletedP_Click"></asp:LinkButton></span>
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
                                                                        <asp:LinkButton ID="lnkOnGoingP" runat="server" Font-Bold="true" Text="0" OnClick="lnkOnGoingP_Click"></asp:LinkButton></span>
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
                                                                        <asp:LinkButton ID="lnkTargetP_C" runat="server" Font-Bold="true" Text="0" OnClick="lnkTargetP_C_Click"></asp:LinkButton></span>
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
                                                                        <asp:LinkButton ID="lnkTargetP_N" runat="server" OnClick="lnkTargetP_N_Click"></asp:LinkButton></span>
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

                            <div class="space-6"></div>
                            <h3 class="header smaller red">Issue Reported
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdIssueReported" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdIssueReported_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Dependency_Id" HeaderText="ProjectWorkIssueDetails_Dependency_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectIssue_Name" HeaderText="Issue" />
                                                <asp:BoundField DataField="Dependency_Name" HeaderText="Sub Issue" />
                                                <asp:TemplateField HeaderText="Total Issues">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTotalIssues" runat="server" OnClick="lnkTotalIssues_Click" Font-Bold="true" Text='<%# Eval("Total_Isues") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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

                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 200px; margin-left: -32px" ScrollBars="Auto">

                            <div class="space-6"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-xs-6 col-sm-4 pricing-box">
                                        <div class="widget-box widget-color-red">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Sanctioned Cost</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblSanctionedCost" runat="server" Font-Bold="true" Text="0"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-4 pricing-box">
                                        <div class="widget-box widget-color-grey">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Total Released Till Date</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblTotalReleased" runat="server" Font-Bold="true" Text="0"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-4 pricing-box">
                                        <div class="widget-box widget-color-green">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Total Expenditure Till Date</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblTotalExpenditure" runat="server" Font-Bold="true" Text="0"></asp:Label></span>
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
                                            <button id="btnclose2" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField runat="server" ID="hf_Issue_Analysis" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Data_Filter" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Financal_Progress_Filter" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Physical_Progress_Filter" Value="" />
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

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdNodalDept').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdNodalDept')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdNodalDept')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: true,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdNodalDepttableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdNodalDepttableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdNodalDepttableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdNodalDept .dropdown-toggle', function (e) {
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

        function openBidders(obj) {
            var QuarterDtls_Id = obj.attributes.quarterdtls_id.nodeValue;
            var BiddingOrder_Id = obj.attributes.biddingorder_id.nodeValue;
            var ShowAlloted = obj.attributes.showalloted.nodeValue;
            obj.href = "Report_Check_Bidding_Status.aspx?QuarterDtls_Id=" + QuarterDtls_Id + "&BiddingOrder_Id=" + BiddingOrder_Id + "&ShowAlloted=" + ShowAlloted;
        }
    </script>
    <script src="canvasjs/canvasjs.min.js"></script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                debugger;
                var hf_Issue_Analysis = $('#ctl00_ContentPlaceHolder1_hf_Issue_Analysis').val();
                var Issue_Analysis;
                Issue_Analysis = JSON.parse(hf_Issue_Analysis);
                if (Issue_Analysis != undefined && Issue_Analysis != "") {
                    var chartP = new CanvasJS.Chart("chartContainerIssue", {
                        animationEnabled: true,
                        theme: "light1", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Total Issues Raised (Count)"
                        },
                        data: [{
                            type: "column",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "Issues Names",
                            dataPoints: [
                                { y: Issue_Analysis[0].Total_Issues, label: Issue_Analysis[0].Issue_Name },
                                { y: Issue_Analysis[1].Total_Issues, label: Issue_Analysis[1].Issue_Name },
                                { y: Issue_Analysis[2].Total_Issues, label: Issue_Analysis[2].Issue_Name },
                                { y: Issue_Analysis[3].Total_Issues, label: Issue_Analysis[3].Issue_Name },
                                { y: Issue_Analysis[4].Total_Issues, label: Issue_Analysis[4].Issue_Name }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Physical_Progress_Filter = $('#ctl00_ContentPlaceHolder1_hf_Physical_Progress_Filter').val();
                var Physical_Progress_Filter;
                Physical_Progress_Filter = JSON.parse(hf_Physical_Progress_Filter);
                if (Physical_Progress_Filter != undefined && Physical_Progress_Filter != "") {
                    var chartP = new CanvasJS.Chart("chartContainerPhysical", {
                        animationEnabled: true,
                        title: {
                            text: "Physical Progress",
                            horizontalAlign: "left"
                        },
                        data: [{
                            type: "doughnut",
                            startAngle: 60,
                            //innerRadius: 60,
                            indexLabelFontSize: 17,
                            indexLabel: "{label} - #percent%",
                            toolTipContent: "<b>{label}:</b> {y} (#percent%)",
                            dataPoints: [
                                { y: Physical_Progress_Filter.Zero, label: "Project Not Started" },
                                { y: Physical_Progress_Filter.BW_0_25, label: "Progress BW 1 to 25%" },
                                { y: Physical_Progress_Filter.BW_26_50, label: "Progress BW 26 to 50%" },
                                { y: Physical_Progress_Filter.BW_51_75, label: "Progress BW 51 to 75%" },
                                { y: Physical_Progress_Filter.BW_76_99, label: "Progress BW 76 to 99%" },
                                { y: Physical_Progress_Filter.More_100, label: "Progress 100%" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Financal_Progress_Filter = $('#ctl00_ContentPlaceHolder1_hf_Financal_Progress_Filter').val();
                var Financal_Progress_Filter;
                Financal_Progress_Filter = JSON.parse(hf_Financal_Progress_Filter);
                if (Financal_Progress_Filter != undefined && Financal_Progress_Filter != "") {
                    var chartP = new CanvasJS.Chart("chartContainerFinancial", {
                        animationEnabled: true,
                        title: {
                            text: "Financial Progress",
                            horizontalAlign: "left"
                        },
                        data: [{
                            type: "pie",
                            startAngle: 240,
                            yValueFormatString: "##0.00'%'",
                            indexLabel: "{label} {y}",
                            dataPoints: [
                                { y: Financal_Progress_Filter.Zero, label: "Project Not Started" },
                                { y: Financal_Progress_Filter.BW_0_25, label: "Progress BW 1 to 25%" },
                                { y: Financal_Progress_Filter.BW_26_50, label: "Progress BW 26 to 50%" },
                                { y: Financal_Progress_Filter.BW_51_75, label: "Progress BW 51 to 75%" },
                                { y: Financal_Progress_Filter.BW_76_99, label: "Progress BW 76 to 99%" },
                                { y: Financal_Progress_Filter.More_100, label: "Progress 100%" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Data_Filter = $('#ctl00_ContentPlaceHolder1_hf_Data_Filter').val();
                var Data_Filter;
                Data_Filter = JSON.parse(hf_Data_Filter);
                if (Data_Filter != undefined && Data_Filter != "") {
                    var chartP = new CanvasJS.Chart("chartContainerFilter", {
                        animationEnabled: true,
                        theme: "light1", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Amount (In Lakhs)"
                        },
                        data: [{
                            type: "column",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "All Figures In Lakhs Only",
                            dataPoints: [
                                { y: Data_Filter.Total_Sanction_Cost, label: "Sanctioned Cost" },
                                { y: Data_Filter.Total_Released_Amount, label: "Total Released" },
                                { y: Data_Filter.Total_Expenditure, label: "Total Expenditure" },
                                { y: Data_Filter.Total_Remaining_Amount, label: "Remaining Amount To Be Released" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>
</asp:Content>

