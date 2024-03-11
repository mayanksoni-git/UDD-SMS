<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Dashboard_Master.aspx.cs" Inherits="Dashboard_Master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <style>
            .bbbtn:hover {
                cursor: pointer;
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=' #85B6F0', endColorstr='#579AEB');
                /* for IE */
                -ms-filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=' #85B6F0', endColorstr='#579AEB');
                /* for IE 8 and above */
                background: -webkit-gradient(linear, left top, left bottom, from(#85B6F0), to(#579AEB));
                /* for webkit browsers */
                background: -moz-linear-gradient(top, #85B6F0, #579AEB);
                /* for firefox 3.6+ */
                background: -o-linear-gradient(top, #85B6F0, #579AEB);
                /* for Opera */
            }

            .alerts-border {
                border: 6px #ff0000 solid;
                animation: blink 1s;
                animation-iteration-count: 10;
            }

            @keyframes blink {
                50% {
                    border-color: #fff;
                }
            }

            .blink {
                animation: blinker 1.2s linear infinite;
                font-family: sans-serif;
            }

            @keyframes blinker {
                50% {
                    opacity: 0.4;
                }
            }

            .ERP {
                background: -webkit-linear-gradient(180deg,teal, Darkblue);
                -webkit-background-clip: text;
                -webkit-text-fill-color: transparent;
            }
        </style>
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
                                <div class="col-md-9">
                                    <h1>PMIS Dashboard							
                                        <small>
                                            <i class="ace-icon fa fa-angle-double-right"></i>
                                            Overview &amp; Stats
                                        </small>
                                        <div style="float: right">
                                            <span class="label label-warning arrowed arrowed-right">ALL FIGURES IN LAKHS ONLY</span>
                                        </div>
                                    </h1>
                                </div>
                                <div class="col-md-3 pull-right">
                                    <div style="margin-right: 50px;">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="~/assets/images/mb/Filter.svg" Height="60px" Width="60px"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.page-header -->

                        <div class="row">
                            <div class="col-xs-12">

                                <div class="table-header">
                                    At A Glance
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-6 col-sm-4 pricing-box">
                                <div class="widget-box widget-color-orange">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter"><b>Total Schemes</b></h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/Dept.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkTotalScheme" OnClick="lnkTotalScheme_Click" runat="server" Font-Bold="true">0</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Total Projects</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/ma.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkTotalProjects" runat="server" OnClick="lnkTotalProjects_Click">0</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Sanctioned Cost (In Lakhs)</h5>
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
                                                                <asp:LinkButton ID="lnkSanctioned" runat="server" OnClick="lnkSanctioned_Click">0</asp:LinkButton></span>
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
                                <div class="widget-box widget-color-dark">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter"><b>Total Release (In Lakhs)</b></h5>
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
                                                                <asp:LinkButton ID="lnkTotalRelease" OnClick="lnkTotalRelease_Click" runat="server" Font-Bold="true">0</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Total Expenditure (In Lakhs)</h5>
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
                                                                <asp:LinkButton ID="lnkTotalExpenditure" runat="server" OnClick="lnkTotalExpenditure_Click">0</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Remaining Amount To Be Released (In Lakhs)</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/Bill_Generate.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkRemaining" runat="server" OnClick="lnkRemaining_Click">0</asp:LinkButton></span>
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

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Scheme Wise Analysis
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <asp:GridView ID="grdScheme_Wise_Details" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdScheme_Wise_Details_PreRender" OnRowDataBound="grdScheme_Wise_Details_RowDataBound" ShowFooter="false" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <div class="col-xs-12">
                                                            <h3 class="header smaller red"><%# Container.DataItemIndex + 1 %>) <%# Eval("Project_Name") %>
                                                            </h3>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <div class="col-xs-9">
                                                            <div class="row">
                                                                <div class="row">
                                                                    <div class="col-xs-3 pricing-span-header">
                                                                        <div class="widget-box transparent">
                                                                            <div class="widget-header">
                                                                                <h5 class="widget-title bigger lighter">Desctiption
                                            <br />
                                                                                </h5>
                                                                            </div>

                                                                            <div class="widget-body" style="margin-top: 18px;">
                                                                                <div class="widget-main no-padding">
                                                                                    <ul class="list-unstyled list-striped pricing-table-header">
                                                                                        <li style="font-weight: bold; font-size: medium;">Total Project</li>
                                                                                        <li style="font-weight: bold; font-size: medium;">Ongoing Project</li>
                                                                                        <li style="font-weight: bold; font-size: medium;">Completed Project</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-xs-9 pricing-span-body">
                                                                        <div class="pricing-span">
                                                                            <div class="widget-box pricing-box-small widget-color-red3">
                                                                                <div class="widget-header">
                                                                                    <center>
                                                                                        <h5 class="widget-title bigger lighter">Project
                                                <br />
                                                                                            Count</h5>
                                                                                    </center>
                                                                                </div>

                                                                                <div class="widget-body">
                                                                                    <div class="widget-main no-padding">
                                                                                        <ul class="list-unstyled list-striped pricing-table">
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnkTotal_Projects" runat="server" OnClick="lnkTotal_Projects_Click" Font-Bold="true" Text='<%# Eval("Total_Count") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnkTotal_Projects_Ongoing" runat="server" OnClick="lnkTotal_Projects_Ongoing_Click" Font-Bold="true" Text='<%# Eval("OnGoing_Count") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnkTotal_Projects_Completed" runat="server" OnClick="lnkTotal_Projects_Completed_Click" Font-Bold="true" Text='<%# Eval("completed_Count") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="pricing-span">
                                                                            <div class="widget-box pricing-box-small widget-color-orange">
                                                                                <div class="widget-header">
                                                                                    <center>
                                                                                        <h5 class="widget-title bigger lighter">Sanctioned Cost (In Lakhs)</h5>
                                                                                    </center>
                                                                                </div>

                                                                                <div class="widget-body">
                                                                                    <div class="widget-main no-padding">
                                                                                        <ul class="list-unstyled list-striped pricing-table">
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Total_Sanction_Cost" runat="server" OnClick="lnk_Total_Sanction_Cost_Click" Font-Bold="true" Text='<%# Eval("Total_Sanction") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Ongoing_Sanction_Cost" runat="server" OnClick="lnk_Ongoing_Sanction_Cost_Click" Font-Bold="true" Text='<%# Eval("OnGoing_Sanction") %>' Font-Size="Medium"></asp:LinkButton></li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Completed_Sanction_Cost" runat="server" OnClick="lnk_Completed_Sanction_Cost_Click" Font-Bold="true" Text='<%# Eval("Completed_Sanction") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="pricing-span">
                                                                            <div class="widget-box pricing-box-small widget-color-green">
                                                                                <div class="widget-header">
                                                                                    <center>
                                                                                        <h5 class="widget-title bigger lighter">Total Released Till Date</h5>
                                                                                    </center>
                                                                                </div>

                                                                                <div class="widget-body">
                                                                                    <div class="widget-main no-padding">
                                                                                        <ul class="list-unstyled list-striped pricing-table">
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Total_Release" runat="server" OnClick="lnk_Total_Release_Click" Font-Bold="true" Text='<%# Eval("Total_Release") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Ongoing_Release" runat="server" OnClick="lnk_Ongoing_Release_Click" Font-Bold="true" Text='<%# Eval("OnGoing_Release") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Completed_Release" runat="server" OnClick="lnk_Completed_Release_Click" Font-Bold="true" Text='<%# Eval("Completed_Release") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="pricing-span">
                                                                            <div class="widget-box pricing-box-small widget-color-grey">
                                                                                <div class="widget-header">
                                                                                    <center>
                                                                                        <h5 class="widget-title bigger lighter">Total Expenditure Till Date</h5>
                                                                                    </center>
                                                                                </div>

                                                                                <div class="widget-body">
                                                                                    <div class="widget-main no-padding">
                                                                                        <ul class="list-unstyled list-striped pricing-table">
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Total_Expenditure" runat="server" OnClick="lnk_Total_Expenditure_Click" Font-Bold="true" Text='<%# Eval("Total_Expenditure") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Ongoing_Expenditure" runat="server" OnClick="lnk_Ongoing_Expenditure_Click" Font-Bold="true" Text='<%# Eval("OnGoing_Expenditure") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Completed_Expenditure" runat="server" OnClick="lnk_Completed_Expenditure_Click" Font-Bold="true" Text='<%# Eval("Completed_Expenditure") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="pricing-span">
                                                                            <div class="widget-box pricing-box-small widget-color-dark">
                                                                                <div class="widget-header">
                                                                                    <center>
                                                                                        <h5 class="widget-title bigger lighter">Remaining
                                                    <br />
                                                                                            Amount</h5>
                                                                                    </center>
                                                                                </div>

                                                                                <div class="widget-body">
                                                                                    <div class="widget-main no-padding">
                                                                                        <ul class="list-unstyled list-striped pricing-table">
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Total_Remaining" runat="server" OnClick="lnk_Total_Remaining_Click" Font-Bold="true" Text='<%# Eval("Total_Remaining_Amount") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Ongoing_Remaining" runat="server" OnClick="lnk_Ongoing_Remaining_Click" Font-Bold="true" Text='<%# Eval("OnGoing_Remaining_Amount") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_Completed_Remaining" runat="server" OnClick="lnk_Completed_Remaining_Click" Font-Bold="true" Text='<%# Eval("Completed_Remaining_Amount") %>' Font-Size="Medium"></asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="space-6"></div>
                                                            <div class="row">
                                                                <div class="col-xs-12">
                                                                    <div class="col-xs-4">
                                                                        <asp:Button ID="btnDetailedDash" Text="Go To Detailed Analysis (PMIS Dashbaord)" runat="server" CssClass="btn btn-info" OnClick="btnDetailedDash_Click"></asp:Button>
                                                                    </div>
                                                                    <div class="col-xs-4">
                                                                        <asp:Button ID="btnEMBDashbaord" Text="Go To Billing Dashboard (EMB)" runat="server" CssClass="btn btn-inverse" OnClick="btnEMBDashbaord_Click"></asp:Button>
                                                                    </div>
                                                                    <div class="col-xs-4">
                                                                        <asp:Button ID="btnDistrictWiseDetails" Text="Go To District Wise Details" runat="server" CssClass="btn btn-danger" OnClick="btnDistrictWiseDetails_Click"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-xs-3">
                                                            <asp:Chart ID="Chart1" runat="server" EnableViewState="true">
                                                                <Series>
                                                                    <asp:Series Name="Default" />
                                                                </Series>
                                                                <ChartAreas>
                                                                    <asp:ChartArea Name="ChartAreaClass">
                                                                        <AxisX Title="Data">
                                                                            <MajorGrid Enabled="false" />
                                                                        </AxisX>
                                                                        <AxisY Title="Amount In lakhs">
                                                                            <MajorGrid Enabled="false" />
                                                                        </AxisY>
                                                                    </asp:ChartArea>
                                                                </ChartAreas>
                                                                <Legends>
                                                                    <asp:Legend Docking="Bottom" Name="LegendClass"></asp:Legend>
                                                                </Legends>
                                                            </asp:Chart>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">

                            <div class="space-6"></div>
                            <h3 class="header smaller red">Apply Filter
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkSelectAllScheme" runat="server" Text="Scheme (Click To Select All)" AutoPostBack="true" OnCheckedChanged="chkSelectAllScheme_CheckedChanged"></asp:CheckBox>
                                        <asp:CheckBoxList ID="ddlScheme" runat="server" SelectionMode="Multiple" RepeatColumns="3" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" id="divZone" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" id="divCircle" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" id="divDivision" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">
                                                Sanction Year
                                            <div style="float: right; margin-left: 30px">
                                                <asp:CheckBox runat="server" ID="chkFY_Wise" OnCheckedChanged="chkFY_Wise_CheckedChanged" AutoPostBack="true" ToolTip="Select To Switch Between Year Wise and Fnancial Year Wise Filter" />
                                            </div>
                                            </label>
                                            <asp:RadioButtonList SelectionMode="Multiple" ID="ddlSanctionYear" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField runat="server" ID="hf_Data_Filter" Value="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <script src="canvasjs/canvasjs.min.js"></script>
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

