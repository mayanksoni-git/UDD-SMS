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
                    <cc1:modalpopupextender id="mp1" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShowPopup"
                        cancelcontrolid="btnclose" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
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
                        <div runat="server" id="divData" class="tblheader" style="overflow: auto">
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
                                                    <div runat="server" id="divReport" class="tblheader" style="overflow: auto">
                                                        <div class="row">
                                                            <div class="col-lg-10">
                                                                <h3>Reports</h3>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <%--<asp:Button ID="Button7" runat="server" Text="Export to Excel Of Financial Year Wise" CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" />--%>
                                                            </div>
                                                        </div>

                                                        <!-- Page-body start -->
                                                        <div id="divDashboard" runat="server" visible="true">
                                                            <div class="page-body">
                                                                <div class="row">
                                                                    <!-- task, page, download counter  start -->
                                                                    <div class="col-xl-3 col-md-6">
                                                                        <div class="card">
                                                                            <div class="card-block">
                                                                                <div class="row align-items-center">
                                                                                    <div class="col-8">
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label1" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel1" Text="" runat="server"></asp:Label></h6>
                                                                                    </div>
                                                                                    <div class="col-4 text-right">
                                                                                        <i class="fa fa-bar-chart f-28"></i>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="card-footer bg-c-organge">
                                                                                <div class="row align-items-center">
                                                                                    <div class="col-9">
                                                                                       <asp:Button ID="btnTotalProjects" Text="View Details" OnClick="btnTotalProjects_Click"  runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
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
                                                                                        <h4 class="text-c-purple">₹
                                                                                        <asp:Label ID="Label2" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel2" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label3" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel3" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label4" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel4" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label5" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel5" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label6" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel6" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label7" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel7" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label8" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel8" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label9" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel9" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label10" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel10" Text="" runat="server"></asp:Label></h6>
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
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label11" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel11" Text="" runat="server"></asp:Label></h6>
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
                                                        </div>
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
                    <asp:PostBackTrigger ControlID="btnCircleWise" />
                    <asp:PostBackTrigger ControlID="btnPriorityWise" />
                    <asp:PostBackTrigger ControlID="btnProjectType" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1 p-0" Style="display: none; width: 1000px; margin-left: -32px; height: 800px; overflow: auto;">
        <div class="row  bg-warning" style="border-radius: 11px; padding: 10px;">
            <div class="col-xs-12">
                <div class="table-header fw-bold fs-4">
                    Vision Plan Dashboard
                </div>
            </div>
        </div>

        <div class="row" style="padding: 10px">
            <div class="col-xl-12">
                <h5 id="tableFor" style="text-align: center" runat="server"></h5>
            </div>
            <div class="col-xl-12">
                <h5 id="tableDescription" style="text-align: center" runat="server"></h5>
            </div>
        </div>
        <div class="row p-3">
            <div class="col-md-12" id="divTotalProjects" runat="server">
                <div class="form-group">
                    <asp:GridView ID="grdTotalProjects" runat="server" CssClass="display table table-bordered reportGrid" ShowFooter="true" AutoGenerateColumns="false" EmptyDataText="No Records Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <itemtemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                            <asp:TemplateField HeaderText="No of Projects">
                                <ItemTemplate>
                                    <asp:Button ID="btnNoOfProjects" runat="server" Text='<%# Eval("NoOfProjects") %>' CommandName="NoOfProjects" OnCommand="GetNoOfProjects" CommandArgument='<%# Eval("FYID") %>' CssClass="btn btn-primary drill_btn" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalNoOfProjects" Text="Total No Of Projects" runat="server"></asp:Label>
                                </FooterTemplate>
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
            <div class="col-md-12">
                <div class="form-group mt-2 text-end">
                    <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="text-light btn bg-danger p-2" OnClick="btnclose_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

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
