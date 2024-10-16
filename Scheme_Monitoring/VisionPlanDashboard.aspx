<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VisionPlanDashboard.aspx.cs" Inherits="VisionPlanDashboard" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>


<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Vision Plan Dashboard</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Vision Plan Management</li>
                                                <li class="breadcrumb-item active">Vision Plan Dashboard</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div runat="server" visible="true" id="divData" class="tblheader" style="overflow: auto">
                            <div class="row">
                                <div class="col-lg-12">

                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Vision Plan Dashboard</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="d-flex" style="margin-top: 20px; margin-bottom: 20px">
                                                        <asp:Button ID="btnDashboard" Text="Dashboard" OnClick="btnDashboard_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnULBWise" Text="ULB Wise" OnClick="btnULBWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>

                                                        <asp:Button ID="btnCircleWise" Text="District Wise" OnClick="btnCircleWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnPriorityWise" Text="Priority Wise" OnClick="btnPriorityWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnProjectType" Text="Project Type Wise" OnClick="btnProjectType_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>

                                                    </div>
                                                    <!-- div.dataTables_borderWrap -->
                                                    <div runat="server" id="divDashboard" class="tblheader" visible="true" style="overflow: auto">
                                                        <div class="row">
                                                            <div class="col-lg-10">
                                                                <h3>Reports</h3>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <%--<asp:Button ID="Button7" runat="server" Text="Export to Excel Of Financial Year Wise" CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" />--%>
                                                            </div>
                                                        </div>

                                                        <!-- Page-body start -->
                                                        <div class="page-body">
                                                            <div class="row">
                                                                <!-- task, page, download counter  start -->



                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label1" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel1" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal1">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple">₹ <asp:Label id="Label2" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel2" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label3" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel3" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label4" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel4" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label5" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel5" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label6" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel6" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label7" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel7" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label8" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel8" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label9" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel9" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label10" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel10" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-xl-3 col-md-6">
                                                                    <div class="card">
                                                                        <div class="card-block">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-8">
                                                                                    <h4 class="text-c-purple"><asp:Label id="Label11" Text="" runat="server"></asp:Label></h4>
                                                                                    <h6 class="text-muted m-b-0"><asp:Label id="HeadLabel11" Text="" runat="server"></asp:Label></h6>
                                                                                </div>
                                                                                <div class="col-4 text-right">
                                                                                    <i class="fa fa-bar-chart f-28"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-footer bg-c-organge">
                                                                            <div class="row align-items-center">
                                                                                <div class="col-9">
                                                                                    <button type="button" class="btn btn-light btn-sm" data-toggle="modal" data-target="#earningsModal2">View Details</button>
                                                                                </div>
                                                                                <div class="col-3 text-right">
                                                                                    <i class="fa fa-line-chart text-white f-16"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>



                                                                <!-- task, page, download counter  end -->
                                                            </div>
                                                        </div>
                                                        <!-- Page-body end -->
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
                    <asp:PostBackTrigger ControlID="btnCircleWise" />
                    <asp:PostBackTrigger ControlID="btnPriorityWise" />
                    <asp:PostBackTrigger ControlID="btnProjectType" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="earningsModal1" tabindex="-1" role="dialog" aria-labelledby="earningsModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="earningsModalLabel1">Earnings Details1</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!-- Modal content goes here -->
                    <p>Here are the details of your earnings...</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="earningsModal2" tabindex="-1" role="dialog" aria-labelledby="earningsModalLabel2" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="earningsModalLabel2">Earnings Details2</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!-- Modal content goes here -->
                    <p>Here are the details of your earnings...</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <style>
        .card {
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.15);
            overflow: hidden;
        }

        .card-block {
            padding: 20px;
            background-color: #fff;
        }

        .text-c-purple {
            color: #7C4DFF; /* Adjust the color to match the design */
            font-size: 24px;
            font-weight: bold;
        }

        .text-muted {
            color: #6c757d; /* Use a muted grey color */
        }

        .m-b-0 {
            margin-bottom: 0;
        }

        .f-28 {
            font-size: 28px;
        }

        .fa-bar-chart {
            color: #7C4DFF; /* Adjust to match the card design */
        }

        .card-footer {
            padding: 10px 20px;
        }

        .bg-c-organge {
            background-color: #f8753e; /* Purple background color */
        }

        .text-white {
            color: #ffffff;
        }

        .f-16 {
            font-size: 16px;
        }

        .row {
            display: flex;
            align-items: center;
        }

        .btn-light {
            background-color: #f8f9fa;
            color: #7C4DFF;
            border: none;
            padding: 5px 10px;
            font-size: 14px;
            border-radius: 4px;
            cursor: pointer;
        }

            .btn-light:hover {
                background-color: #e2e6ea;
            }

        /* Ensure the modal has a smooth appearance */
        .modal-content {
            border-radius: 8px;
            padding: 20px;
        }
    </style>
</asp:Content>
