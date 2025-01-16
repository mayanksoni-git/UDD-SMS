<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" 
    AutoEventWireup="true" CodeFile="DrainageDashboard.aspx.cs" Inherits="DrainageDashboard" EnableEventValidation="false" 
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        .card-animate:hover {
            transform: scale(1);
        }

        .card-animate h6 {
            font-weight: 400;
            color: #000 !important;
            font-weight: 500;
            font-size: 14px;
        }
    </style>
    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>

                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Storm Water Drainage  Dashboard</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Storm Water Drainage</li>
                                            <li class="breadcrumb-item active">Storm Water Drainage  Dashboard</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divData" class="tblheader">
                            <div class="row">
                                <div class="col-lg-12">

                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Storm Water Drainage  Dashboard</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">

                                                    <div class="card-body">

                                                        <ul class="nav nav-pills arrow-navtabs nav-success mb-3" role="tablist">
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link active" data-bs-toggle="tab" href="#arrow-dashboard" role="tab" aria-selected="true">

                                                                    <asp:Button ID="btnDashboard" Text="Dashboard" OnClick="btnDashboard_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                        <!-- Tab panes -->
                                                        <div class="tab-content text-muted">
                                                            <div class="tab-pane active" id="arrow-dashboard" role="tabpanel">
                                                                <div id="divDashboard" runat="server" visible="true">
                                                                    <div class="page-body">
                                                                        <div class="row">
                                                                            <!-- task, page, download counter  start -->

                                                                            <%--Tile 1--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-primary-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Master Plans Received</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button1" type="button" runat="server" CssClass="plan-btn" Text="Open List" />

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <%--Tile 2--%>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-success-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                        0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Master Plans Approved</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button2" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <%--Tile 3--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-warning-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Master Plan Reverted</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                               <asp:Button ID="Button8" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            <%--Tile 1--%>
                                                                            <div class="col-xl-3 col-md-6"></div>

                                                                            <%--Tile 3--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-warning-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Action Plans Received</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                               <asp:Button ID="Button3" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            


                                                                            <%--Tile 4--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-info-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Action Plans Approved</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button4" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <%--Tile 3--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-warning-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Action Plan Reverted</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                               <asp:Button ID="Button9" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            <%--Tile 1--%>
                                                                            <div class="col-xl-3 col-md-6"></div>

                                                                            


                                                                            <%--Tile 7--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-primary-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple"> ₹
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Fund Allocation</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button7" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                            <%--Tile 2--%>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-success-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple"> ₹
                                                                                        0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Fund Sanctioned</h6> 
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button11" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>


                                                                            <%--Tile 4--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-info-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">  ₹
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total Fund Utilized</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button12" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <!-- task, page, download counter  end -->
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <!-- div.dataTables_borderWrap -->
                                                    <div runat="server" id="divReport" class="tblheader">


                                                        <!-- Page-body start -->


                                                        <div id="divGrid" runat="server" visible="false">
                                                            <asp:GridView ID="gridDashboard" runat="server" CssClass="display table table-bordered reportGrid" ShowFooter="true" AutoGenerateColumns="True" EmptyDataText="No Records Found">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <tr>
                                                                        <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                                    </tr>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                        <!-- Page-body end -->
                                                    </div>
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div runat="server" id="div1" class="tblheader">
                            <div class="row">
                                <div class="col-lg-12">

                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Storm Water Drainage DPR Dashboard</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="card-body">

                                                        <div class="tab-content text-muted">
                                                            <div class="tab-pane active" id="arrow-dashboard2" role="tabpanel">
                                                                <div id="div2" runat="server" visible="true">
                                                                    <div class="page-body">
                                                                        <div class="row">
                                                                                <%--Tile 5--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-danger-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total DPRs Received</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button5" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <%--Tile 6--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-dark-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total DPRs Approved</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button6" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                             <%--Tile 5--%>
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-danger-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    0</h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    Total DPR Reverted</h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <asp:Button ID="Button10" type="button" runat="server" CssClass="plan-btn" Text="Open List" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDashboard" />
                    
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
