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
                                                                                       <%--<asp:Button ID="btnTotalProjects" Text="View Details" OnClick="btnTotalProjects_Click"  runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>--%>
                                                                                        <asp:Button ID="btnTotalProjects" runat="server" Text="Open List" OnClientClick="btnTotalProjects_Click(1); return false;" />

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

     <script>
         function btnTotalProjects_Click() {
             $.ajax({
                 url: "VisionPlanDashboard.aspx/btnTotalProjects_Click",
                 type: "POST",
                 contentType: "application/json;charset=utf-8;",
                 dataType: "json",
                 data: JSON.stringify({ newAkanshi_Id: 1 }),
                 success: function (data) {
                     console.log(data); // Inspect the response
                     if (data.d) {
                         var result = JSON.parse(data.d);
                         $('#ULBData').empty(); // Clear existing rows
                         var html = '';
                         var totalProjectCost = 0;
                         var totalProjectCount = 0;

                         for (var i = 0; i < result.length; i++) {
                             var item = result[i];
                             var projectCost = parseFloat(item.TotalProjectCost) || 0;
                             totalProjectCost += projectCost; // Accumulate the project cost
                             var projectCount = parseInt(item.NoOfProjects) || 0;
                             totalProjectCount += projectCount; //Count Total Project
                             html += '<tr>';
                             html += '<td>' + (item.FYID || '') + '</td>';
                             html += '<td>' + (item.FinancialYear_Comments || '') + '</td>';
                             html += '<td>';
                             html += '<button type="button" class="btn btn-primary btn-sm" onclick="openTotalProjectUlbWiseByFYID(' + item.FYID + ')">' + (item.NoOfProjects || '') + '</button>';
                             html += '</td>';
                             html += '<td>' + (item.TotalProjectCost || '') + '</td>';
                             html += '</tr>';
                         }

                         $('#HeadData1').html(html);
                         $('#VPTableFooter12').text(totalProjectCost.toFixed(2));
                         $('#VPTableFooter11').text(totalProjectCount);
                         $("#TotalProjectsFinancialYearWise").modal('show');
                         
                         $('#VPTable1').DataTable({
                             destroy: true, // Ensures re-initialization on each AJAX load
                             dom: 'Blfrtip', // Adds button container at the top
                             buttons: ["copy", "csv", "excel", "print", "pdf"]
                         });
                     } else {
                         console.error("No data returned");
                     }
                 },
                 error: function (xhr, status, error) {
                     console.error("AJAX error: " + status + " - " + error);
                 }
             });
         }

         function openTotalProjectUlbWiseByFYID(FYID) {

             $.ajax({
                 url: "VisionPlanDashboard.aspx/GetTotalProjectsUlbWiseByFYID",
                 type: "POST",
                 contentType: "application/json;charset=utf-8;",
                 dataType: "json",
                 data: JSON.stringify({ FYID: FYID }),
                 success: function (data) {
                     console.log(data); // Inspect the response
                     if (data.d) {
                         var result = JSON.parse(data.d);

                         // Check if DataTable is already initialized and clear its contents
                         if ($.fn.DataTable.isDataTable('#VPTable2')) {
                             $('#VPTable2').DataTable().clear().destroy();
                         }

                         //$('#ULBData').empty(); // Clear existing rows
                         var html = '';
                         var totalProjectCost = 0;
                         var totalProjectCount = 0;
                         for (var i = 0; i < result.length; i++) {
                             var item = result[i];
                             var projectCost = parseFloat(item.TotalProjectCost) || 0;
                             totalProjectCost += projectCost; // Accumulate the project cost
                             var projectCount = parseInt(item.NoOfProjects) || 0;
                             totalProjectCount += projectCount; //Count Total Project
                             html += '<tr>';
                             html += '<td>' + (item.Circle_Name || '') + '</td>';
                             html += '<td>' + (item.Division_Name || '') + '</td>';
                             html += '<td>';
                             html += '<button type="button" class="btn btn-primary btn-sm" onclick="ProjectByFYIDandULB(' + item.FYID + ',' + item.ULBID + ')">' + (item.NoOfProjects || '') + '</button>';
                             html += '</td>';
                             html += '<td>' + (item.TotalProjectCost || '') + '</td>';
                             html += '</tr>';
                         }

                         $('#HeadData2').html(html);
                         $('#VPTableFooter22').text(totalProjectCost.toFixed(2));
                         $('#VPTableFooter21').text(totalProjectCount);
                         $("#TotalProjectsFinancialYearWise").modal('hide');
                         $("#TotalProjectsULBWiseByFYID").modal('show');
                         $('#VPTable2').DataTable({
                             destroy: true, // Ensures re-initialization on each AJAX load
                             dom: 'Blfrtip', // Adds button container at the top
                             
                             buttons: ["copy", "csv", "excel", "print", "pdf"]
                         });
                     } else {
                         console.error("No data returned");
                     }
                 },
                 error: function (xhr, status, error) {
                     console.error("AJAX error: " + status + " - " + error);
                 }
             });
         }


         function ProjectByFYIDandULB(FYID, ULBID) {
             $('#HeadData3').empty();
             debugger
             $.ajax({
                 url: "VisionPlanDashboard.aspx/GetProjectByFYIDandULB",
                 type: "POST",
                 contentType: "application/json;charset=utf-8;",
                 dataType: "json",
                 data: JSON.stringify({ FYID: FYID, ULBID: ULBID }),
                 success: function (data) {
                     console.log(data); // Inspect the response
                     if (data.d) {
                         var result = JSON.parse(data.d);

                         // Check if DataTable is already initialized and clear its contents
                         if ($.fn.DataTable.isDataTable('#VPTable3')) {
                             $('#VPTable3').DataTable().clear().destroy();
                         }

                         var html = '';
                         var totalProjectCost = 0;
                         
                         for (var i = 0; i < result.length; i++) {
                             var item = result[i];
                             var projectCost = parseFloat(item.ProjectCost) || 0;
                             totalProjectCost += projectCost; // Accumulate the project cost

                             html += '<tr>';
                             html += '<td>' + (i+1 || '') + '</td>';
                             html += '<td>' + (item.Circle_Name || '') + '</td>';
                             html += '<td>' + (item.Division_Name || '') + '</td>';
                             html += '<td>' + (item.ProjectName || '') + '</td>';
                             html += '<td>' + (item.ProjectType_Name || '') + '</td>';
                             html += '<td>' + (projectCost ? projectCost.toFixed(2) : '') + '</td>';
                             html += '<td>' + (item.Loactions || '') + '</td>';
                             html += '<td>' + (item.FinancialYear_Comments || '') + '</td>';
                             html += '<td>' + (item.Construction || '') + '</td>';
                             html += '<td>' + (item.selfPriority || '') + '</td>';
                             html += '<td>' + (item.ProjectStatus || '') + '</td>';
                             html += '<td>';
                             if (item.VPDoc) {
                                 html += '<button type="button" class="btn btn-info btn-xs" onclick="window.open(\'' + item.VPDoc + '\', \'_blank\')">Open Doc</button>';
                             } else {
                                 html += 'No Document';
                             }
                             html += '</td></tr>';
                         }

                         $('#HeadData3').html(html);
                         $('#VPTableFooter31').text(totalProjectCost.toFixed(2)); // Display total in footer
                         //$('#VPTableFooter32').text(result.length); // Display total in footer

                         $("#TotalProjectsULBWiseByFYID").modal('hide');
                         $("#ProjectByFYIDandULB").modal('show');

                         $('#VPTable3').DataTable({
                             destroy: true, // Ensures re-initialization on each AJAX load
                             dom: 'Blfrtip', // Adds button container at the top
                             buttons: ["copy", "csv", "excel", "print", "pdf"]
                         });



                         $('#VPTable3').DataTable({
                             destroy: true, // Ensures re-initialization on each AJAX load
                             dom: 'Blfrtip', // Adds button container at the top
                             buttons: [
                                 {
                                     extend: 'excelHtml5',
                                     text: 'Export to Excel',
                                     title: 'Project Data Export'
                                 }
                             ]
                             //buttons: ["copy", "csv", "excel", "print", "pdf"]
                         });
                     } else {
                         console.error("No data returned");
                     }
                 },
                 error: function (xhr, status, error) {
                     console.error("AJAX error: " + status + " - " + error);
                 }
             });
         }



         //function ProjectByFYIDandULB(FYID, ULBID) {

         //    $.ajax({
         //        url: "VisionPlanDashboard.aspx/GetProjectByFYIDandULB",
         //        type: "POST",
         //        contentType: "application/json;charset=utf-8;",
         //        dataType: "json",
         //        data: JSON.stringify({ FYID: FYID, ULBID:ULBID }),
         //        success: function (data) {
         //            console.log(data); // Inspect the response
         //            if (data.d) {
         //                var result = JSON.parse(data.d);
         //                var html = '';

         //                for (var i = 0; i < result.length; i++) {
         //                    var item = result[i];
         //                    html += '<tr>';
         //                    html += '<td>' + (item.Circle_Name || '') + '</td>';
         //                    html += '<td>' + (item.Division_Name || '') + '</td>';
         //                    html += '<td>' + (item.ProjectName || '') + '</td>';
         //                    html += '<td>' + (item.ProjectType_Name || '') + '</td>';
         //                    html += '<td>' + (item.ProjectCost || '') + '</td>';
         //                    html += '<td>' + (item.Loactions || '') + '</td>';
         //                    html += '<td>' + (item.FinancialYear_Comments || '') + '</td>';
         //                    html += '<td>' + (item.Construction || '') + '</td>';
         //                    html += '<td>' + (item.selfPriority || '') + '</td>';
         //                    html += '<td>' + (item.ProjectStatus || '') + '</td>';
         //                    html += '<td>';
         //                    if (item.VPDoc) {
         //                        html += '<button type="button" class="btn btn-info btn-xs" onclick="window.open(\'' + item.VPDoc + '\', \'_blank\')">Open Doc</button>';
         //                    } else {
         //                        html += 'No Document';
         //                    }
         //                    html += '</td></tr>';
         //                }

         //                $('#HeadData3').html(html);
         //                $("#TotalProjectsULBWiseByFYID").modal('hide');
         //                $("#ProjectByFYIDandULB").modal('show');
         //                $('#VPTable3').DataTable({
         //                    destroy: true, // Ensures re-initialization on each AJAX load
         //                    dom: 'Blfrtip', // Adds button container at the top
         //                    //buttons: [
         //                    //    {
         //                    //        extend: 'excelHtml5',
         //                    //        text: 'Export to Excel',
         //                    //        title: 'Project Data Export'
         //                    //    }
         //                    //]
         //                    buttons: ["copy", "csv", "excel", "print", "pdf"]
         //                });
         //            } else {
         //                console.error("No data returned");
         //            }
         //        },
         //        error: function (xhr, status, error) {
         //            console.error("AJAX error: " + status + " - " + error);
         //        }
         //    });
         //}

         //function ProjectByFYIDandULB(FYID, ULBID) {
         //    $.ajax({
         //        url: "VisionPlanDashboard.aspx/GetProjectByFYIDandULB",
         //        type: "POST",
         //        contentType: "application/json;charset=utf-8;",
         //        dataType: "json",
         //        data: JSON.stringify({ FYID: FYID, ULBID: ULBID }),
         //        success: function (data) {
         //            if (data.d) {
         //                var result = JSON.parse(data.d);
         //                loadDataIntoTable(result);
         //                $("#TotalProjectsULBWiseByFYID").modal('hide');
         //                $("#ProjectByFYIDandULB").modal('show');
         //            } else {
         //                console.error("No data returned");
         //            }
         //        },
         //        error: function (xhr, status, error) {
         //            console.error("AJAX error: " + status + " - " + error);
         //        }
         //    });
         //}


         //function loadDataIntoTable(data) {
         //    var html = '';

         //    for (var i = 0; i < data.length; i++) {
         //        var item = data[i];
         //        html += '<tr>';
         //        html += '<td>' + (item.Circle_Name || '') + '</td>';
         //        html += '<td>' + (item.Division_Name || '') + '</td>';
         //        html += '<td>' + (item.ProjectName || '') + '</td>';
         //        html += '<td>' + (item.ProjectType_Name || '') + '</td>';
         //        html += '<td>' + (item.ProjectCost || '') + '</td>';
         //        html += '<td>' + (item.Loactions || '') + '</td>';
         //        html += '<td>' + (item.FinancialYear_Comments || '') + '</td>';
         //        html += '<td>' + (item.Construction || '') + '</td>';
         //        html += '<td>' + (item.selfPriority || '') + '</td>';
         //        html += '<td>' + (item.ProjectStatus || '') + '</td>';
         //        html += '<td>';
         //        if (item.VPDoc) {
         //            html += '<button type="button" class="btn btn-info btn-sm" onclick="window.open(\'' + item.VPDoc + '\', \'_blank\')">Open PDF</button>';
         //        } else {
         //            html += 'No Document';
         //        }
         //        html += '</td></tr>';
         //    }

         //    $('#HeadData3').html(html);

         //    // Initialize DataTables on the populated table
         //    $('#projectTable').DataTable();
         //}

     </script>

    <!-- Modal for ProjectByFYIDandULB -->
    <div class="modal fade" id="ProjectByFYIDandULB" tabindex="-1" aria-labelledby="ProjectByFYIDandULBLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="ProjectByFYIDandULBLabel">List of Projects by ULB and Financial Year</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VPTable3" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Sr.No.</th>
                                <th>District Name</th>
                                <th>ULB Name</th>
                                <th>Project Name</th>
                                <th>Project Type</th>
                                <th>Project Cost</th>
                                <th>Location</th>
                                <th>FY</th>
                                <th>Construction</th>
                                <th>Priority</th>
                                <th>Project Status</th>
                                <th>VPDoc</th>
                            </tr>
                        </thead>
                        <tbody id="HeadData3">
                        </tbody>
                        <tfoot>
                            <tr>
                                <%--<th style="text-align: right">Total Project:</th>--%>
                                <%--<th  id="VPTableFooter32" style="text-align: right"></th>--%>
                                <th colspan="5" style="text-align: right">Total Project Cost:</th>
                                <th id="VPTableFooter31"></th>
                                <th colspan="6"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for TotalProjectsFinancialYearWise -->
    <div class="modal fade" id="TotalProjectsFinancialYearWise" tabindex="-1" aria-labelledby="TotalProjectsFinancialYearWiseLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="TotalProjectsFinancialYearWiseLabel">Total No of Projects Financial Year Wise</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VPTable1" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Sr.No.</th>
                                <th>Financial Year</th>
                                <th>No Of Projects</th>
                                <th>Total Project Cost</th>
                            </tr>
                        </thead>
                        <tbody id="HeadData1">
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="2" style="text-align: right">Total Project Count/Cost:</th>
                                <th id="VPTableFooter11"></th>
                                <th  id="VPTableFooter12"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for TotalProjectsULBWiseByFYID -->
    <div class="modal fade" id="TotalProjectsULBWiseByFYID" tabindex="-1" aria-labelledby="TotalProjectsULBWiseByFYIDLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="TotalProjectsULBWiseByFYIDLabel">Total Projects ULB Wise By Financial Year</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VPTable2" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>District Name</th>
                                <th>ULB Name</th>
                                <th>No of Projects</th>
                                <th>Total Project Cost</th>
                            </tr>
                        </thead>
                        <tbody id="HeadData2">
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="2" style="text-align: right">Total Project Count/Cost:</th>
                                <th id="VPTableFooter21"></th>
                                <th  id="VPTableFooter22"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
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
