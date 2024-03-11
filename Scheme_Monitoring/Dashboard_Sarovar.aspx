<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Dashboard_Sarovar.aspx.cs" Inherits="Dashboard_Sarovar" MaintainScrollPositionOnPostback="true" %>

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

                    <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

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
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-blue">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Total GO Done</h5>
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
                                                                <asp:LinkButton ID="lnkTotalGODone" runat="server" OnClick="lnkTotalGODone_Click">159</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Total SLTC Done</h5>
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
                                                                <asp:LinkButton ID="lnkSLTC" runat="server" OnClick="lnkSLTC_Click">159</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Total SHPSC Done</h5>
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
                                                                <asp:LinkButton ID="lnkSHPSC" runat="server" OnClick="lnkSHPSC_Click">159</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter"><b>Under Tendering</b></h5>
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
                                                                <asp:LinkButton ID="lnkUnderTendering" OnClick="lnkUnderTendering_Click" runat="server" Font-Bold="true">9</asp:LinkButton></span>
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
                                <div class="widget-box widget-color-blue">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Tender Published</h5>
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
                                                                <asp:LinkButton ID="lnkTenderPublihed" runat="server" OnClick="lnkTenderPublihed_Click">95</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">LOA Issued</h5>
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
                                                                <asp:LinkButton ID="lnkLOAIssued" runat="server" OnClick="lnkLOAIssued_Click">23</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Work Started</h5>
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
                                                                <asp:LinkButton ID="lnkWorkStarted" runat="server" OnClick="lnkWorkStarted_Click">23</asp:LinkButton></span>
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
                                        <h5 class="widget-title bigger lighter">Work Completed</h5>
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
                                                                <asp:LinkButton ID="lnkWorkCompleted" runat="server" OnClick="lnkWorkCompleted_Click">0</asp:LinkButton></span>
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

                            <div class="col-xs-6 col-sm-3 pricing-box">
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

                            <div class="col-xs-6 col-sm-3 pricing-box">
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

                            <div class="col-xs-6 col-sm-3 pricing-box">
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


                        <div class="space-6"></div>
                        <h3 class="header smaller red">Projects Progress Status (Based On Step 3 [Physical & Financial Progress])
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdPMISUpdation" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPMISUpdation_PreRender">
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

                                <div class="col-xs-6 col-sm-4 pricing-box">
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
                                </div>
                            </div>
                        </div>


                        <div class="space-6"></div>
                        <h3 class="header smaller red">Issue Analysis
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <asp:GridView ID="grdIssueReportedGlobal" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdIssueReportedGlobal_PreRender">
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
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
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

