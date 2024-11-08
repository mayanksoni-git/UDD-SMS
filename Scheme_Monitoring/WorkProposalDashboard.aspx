<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="WorkProposalDashboard.aspx.cs" Inherits="WorkProposalDashboard" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    <h4 class="mb-sm-0">Work Proposal Dashboard</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Proposal Management</li>
                                            <li class="breadcrumb-item active">Work Proposal Dashboard</li>
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
                                            <h4 class="card-title mb-0 flex-grow-1">Vision Plan Dashboard</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">

                                                    <div class="card-body">

                                                        <ul class="nav nav-pills arrow-navtabs nav-success mb-3" role="tablist">
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link active" data-bs-toggle="tab" href="#arrow-btnDashboard" role="tab" aria-selected="true">

                                                                    <asp:Button ID="btnDashboard" Text="Dashboard" OnClick="btnDashboard_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                                </div>
                                                            </li>
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnULBWise" role="tab" aria-selected="false" tabindex="-1">

                                                                    <asp:Button ID="btnULBWise" Text="ULB Wise" OnClick="btnULBWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                                </div>
                                                            </li>
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnULBType" role="tab" aria-selected="false" tabindex="-1">

                                                                    <asp:Button ID="btnULBType" Text="ULB Type Wise" OnClick="btnULBType_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                                </div>
                                                            </li>
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnProjectType" role="tab" aria-selected="false" tabindex="-1">

                                                                    <asp:Button ID="btnProjectType" Text="Project Type Wise" OnClick="btnProjectType_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                                </div>
                                                            </li>
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnProposerType" role="tab" aria-selected="false" tabindex="-1">

                                                                    <asp:Button ID="btnProposerType" Text="Proposer Type Wise" OnClick="btnProposerType_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                                </div>
                                                            </li>
                                                            <li class="nav-item" role="presentation">
                                                                <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnSchemeWise" role="tab" aria-selected="false" tabindex="-1">

                                                                    <asp:Button ID="btnSchemeWise" Text="Scheme Wise" OnClick="btnSchemeWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
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
                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-primary-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel1" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <%--<asp:Button ID="btnTotalProjects" Text="View Details" OnClick="btnTotalProjects_Click"  runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>--%>
                                                                                                <asp:Button ID="btnTotalProjects" type="button" runat="server" CssClass="plan-btn" Text="Open List" OnClientClick="btnTotalProjects_Click(1); return false;" />

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-success-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">₹
                                                                                        <asp:Label ID="Label2" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel2" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-warning-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    <asp:Label ID="Label3" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel3" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-info-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    <asp:Label ID="Label4" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel4" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-danger-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    <asp:Label ID="Label5" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel5" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-dark-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    <asp:Label ID="Label6" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel6" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                            <div class="col-xl-3 col-md-6">
                                                                                <div class="card card-animate bg-primary-subtle">
                                                                                    <div class="card-body">
                                                                                        <div class="row align-items-center">
                                                                                            <div class="col-8">
                                                                                                <h4 class="text-c-purple">
                                                                                                    <asp:Label ID="Label7" Text="" runat="server"></asp:Label></h4>
                                                                                                <h6 class="text-muted m-b-0">
                                                                                                    <asp:Label ID="HeadLabel7" Text="" runat="server"></asp:Label></h6>
                                                                                            </div>
                                                                                            <div class="col-4 text-end">
                                                                                                <i class="ri-article-line display-3 text-danger"></i>
                                                                                            </div>
                                                                                            <div class="col-12">
                                                                                                <button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>
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
                                                            <div class="tab-pane" id="arrow-btnULBWise" role="tabpanel">
                                                            </div>
                                                            <div class="tab-pane" id="arrow-btnULBType" role="tabpanel">
                                                            </div>
                                                            
                                                            <div class="tab-pane" id="arrow-btnProjectType" role="tabpanel">
                                                            </div>
                                                            <div class="tab-pane" id="arrow-btnPriorityWise" role="tabpanel">
                                                            </div>
                                                            <div class="tab-pane" id="arrow-btnProposerType" role="tabpanel">
                                                            </div>
                                                            <div class="tab-pane" id="arrow-btnSchemeWise" role="tabpanel">
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

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDashboard" />
                    <asp:PostBackTrigger ControlID="btnULBWise" />
                    <asp:PostBackTrigger ControlID="btnULBType" />
                    <asp:PostBackTrigger ControlID="btnProjectType" />
                    <asp:PostBackTrigger ControlID="btnSchemeWise" />
                    <asp:PostBackTrigger ControlID="btnProposerType" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
