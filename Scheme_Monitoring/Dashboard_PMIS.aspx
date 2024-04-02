<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Dashboard_PMIS.aspx.cs" Inherits="Dashboard_PMIS" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
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

                    <div class="container-fluid">
                        <!-- start page title -->
                        <div class="row">
                            <div class="col-12 mb-4">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Dashboard</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item active">Dashboard</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Create / Update Project</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="control-label no-padding-right">Scheme</label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Projects Physical & Financial Progress Status</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <div style="overflow: auto">
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
                                                <!--end col-->
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>



                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-6 col-md-6">
                                                    <div id="chartContainerPhysical" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                                                </div>
                                                <div class="col-xxl-6 col-md-6">
                                                    <div id="chartContainerFinancial" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div class="row">
                            <div class="col-12 mb-4">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Projects Completion Status According To Target Month</h4>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-light-subtle shadow-none bg-opacity-40">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/images/pmis/Completed.jpg" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <asp:LinkButton ID="lnkCompleted" runat="server" Font-Bold="true" Text="0" OnClick="lnkCompleted_Click"></asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                        <h6 class="fs-15 fw-semibold">Completed Projects</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-dark-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/images/pmis/ongoing.jpg" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <asp:LinkButton ID="lnkOnGoing" runat="server" Font-Bold="true" Text="0" OnClick="lnkOnGoing_Click"></asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                        <h6 class="fs-15 fw-semibold">Ongoing Projects</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-primary-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/images/pmis/Progress_C.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <asp:LinkButton ID="lnkTarget_C" runat="server" Font-Bold="true" Text="0" OnClick="lnkTarget_C_Click"></asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                        <h6 class="fs-15 fw-semibold">Completing In Current Month</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-info-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/images/pmis/Progress_N.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <asp:LinkButton ID="lnkTarget_N" runat="server" OnClick="lnkTarget_N_Click"></asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                        <h6 class="fs-15 fw-semibold">Completing In Next Month</h6>
                                    </div>
                                </div>
                            </div>
                            <!-- end col-->
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
                            <div class="col-12 mb-4">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Issue Analysis</h4>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xl-6 col-md-6">
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
                            <div class="col-xl-6 col-md-6">
                                <div id="chartContainerIssue" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">

                            <div class="space-6"></div>
                            <h3 class="header smaller red">
                                <asp:LinkButton ID="lnkProjectStatusPopup" runat="server" Font-Bold="true" Text="Project Status" OnClick="lnkProjectStatusPopup_Click"></asp:LinkButton>
                            </h3>
                            <div class="row">
                                <div class="col-xl-3 col-md-6">
                                    <div class="card card-height-100 bg-light-subtle shadow-none bg-opacity-40">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/Completed.jpg" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:LinkButton ID="lnkCompletedP" runat="server" Font-Bold="true" Text="0" OnClick="lnkCompletedP_Click"></asp:LinkButton>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Completed Projects</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="card card-height-100 bg-dark-subtle shadow-none bg-opacity-10">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/ongoing.jpg" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:LinkButton ID="lnkOnGoingP" runat="server" Font-Bold="true" Text="0" OnClick="lnkOnGoingP_Click"></asp:LinkButton>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Ongoing Projects</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="card card-height-100 bg-primary-subtle shadow-none bg-opacity-10">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/Progress_C.png" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:LinkButton ID="lnkTargetP_C" runat="server" Font-Bold="true" Text="0" OnClick="lnkTargetP_C_Click"></asp:LinkButton>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Completing In Current Month</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="card card-height-100 bg-info-subtle shadow-none bg-opacity-10">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/Progress_N.png" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:LinkButton ID="lnkTargetP_N" runat="server" OnClick="lnkTargetP_N_Click"></asp:LinkButton>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Completing In Next Month</h6>
                                        </div>
                                    </div>
                                </div>
                                <!-- end col-->
                            </div>

                            <div class="row">
                                <div class="col-12 mb-4">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Issue Reported Analysis</h4>
                                    </div>
                                </div>
                            </div>

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
                                <div class="col-xl-4 col-md-6">
                                    <div class="card card-height-100 bg-light-subtle shadow-none bg-opacity-40">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/Expenditure_C.png" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:Label ID="lblSanctionedCost" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Sanctioned Cost</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-md-6">
                                    <div class="card card-height-100 bg-dark-subtle shadow-none bg-opacity-10">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/Expenditure_P.jpg" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:Label ID="lblTotalReleased" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Total Released Till Date</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-md-6">
                                    <div class="card card-height-100 bg-primary-subtle shadow-none bg-opacity-10">
                                        <div class="card-body">
                                            <div class="row gy-4">
                                                <div class="col-md-6">
                                                    <img src="assets/images/pmis/Financial_Completed.png" width="60px" height="60px" />
                                                </div>
                                                <div class="col-md-6">
                                                    <h4 class="fs-4 mb-3">
                                                        <asp:Label ID="lblTotalExpenditure" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                    </h4>
                                                </div>
                                            </div>
                                            <h6 class="fs-15 fw-semibold">Total Expenditure Till Date</h6>
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

