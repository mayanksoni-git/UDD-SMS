<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VisionPlanDashboard.aspx.cs" Inherits="VisionPlanDashboard" EnableEventValidation="false" ValidateRequest="false" %>

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
                                            <div class="nav-link active" data-bs-toggle="tab" href="#arrow-dashboard" role="tab" aria-selected="true">
                                             
                                                 <asp:Button ID="btnDashboard" Text="Dashboard" OnClick="btnDashboard_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                            </div>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnULBWise" role="tab" aria-selected="false" tabindex="-1">
                                               
                                                <asp:Button ID="btnULBWise" Text="ULB Wise" OnClick="btnULBWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                            </div>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnCircleWise" role="tab" aria-selected="false" tabindex="-1">
                                                
                                                <asp:Button ID="btnCircleWise" Text="District Wise" OnClick="btnCircleWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                            </div>
                                        </li>
                                         <li class="nav-item" role="presentation">
                                            <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnPriorityWise" role="tab" aria-selected="false" tabindex="-1">
                                                
                                      <asp:Button ID="btnPriorityWise" Text="Priority Wise" OnClick="btnPriorityWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
                                            </div>
                                        </li>
                                         <li class="nav-item" role="presentation">
                                            <div class="nav-link" data-bs-toggle="tab" href="#arrow-btnProjectType" role="tab" aria-selected="false" tabindex="-1">
                                                
                                             <asp:Button ID="btnProjectType" Text="Project Type Wise" OnClick="btnProjectType_Click" runat="server" CssClass="btn tab_btn bg-success text-white"></asp:Button>
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
                                                                                      <asp:Button ID="btnTotalAmounts" type="button" runat="server" CssClass="plan-btn" Text="Open List" OnClientClick="btnTotalAmounts_Click(1); return false;" />
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
                                                                                      <asp:Button ID="btnTotalULBRepoted" type="button" runat="server" CssClass="plan-btn" Text="Open List" OnClientClick="btnTotalULBRepoted_Click(1); return false;" />
                                                                                        <%--<button type="button" class="plan-btn" data-toggle="modal" data-target="#earningsModal2">View Details</button>--%>
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

                                                                    <div class="col-xl-3 col-md-6">
                                                                        <div class="card card-animate bg-success-subtle">
                                                                            <div class="card-body">
                                                                                <div class="row align-items-center">
                                                                                    <div class="col-8">
                                                                                        <h4 class="text-c-purple">
                                                                                            <asp:Label ID="Label8" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel8" Text="" runat="server"></asp:Label></h6>
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
                                                                                            <asp:Label ID="Label9" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel9" Text="" runat="server"></asp:Label></h6>
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
                                                                                            <asp:Label ID="Label10" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel10" Text="" runat="server"></asp:Label></h6>
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
                                                                                            <asp:Label ID="Label11" Text="" runat="server"></asp:Label></h4>
                                                                                        <h6 class="text-muted m-b-0">
                                                                                            <asp:Label ID="HeadLabel11" Text="" runat="server"></asp:Label></h6>
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
                                        <div class="tab-pane" id="arrow-btnCircleWise" role="tabpanel">
                                          
                                        </div>
                                        <div class="tab-pane" id="arrow-btnPriorityWise" role="tabpanel">
                                          
                                        </div>
                                        <div class="tab-pane" id="arrow-btnProjectType" role="tabpanel">
                                          
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
                             buttons: ["csv", "excel", "print"]
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
                         //$("#TotalProjectsFinancialYearWise").modal('hide');
                         $("#TotalProjectsULBWiseByFYID").modal('show');
                            

                         $('#VPTable2').DataTable({
                             destroy: true, // Ensures re-initialization on each AJAX load
                             dom: 'Blfrtip', // Adds button container at the top
                             
                             buttons: ["csv", "excel", "print"]
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
                                 html += '<button type="button" class="btn btn-info btn-sm" onclick="window.open(\'' + item.VPDoc + '\', \'_blank\')"><i class="bx bxs-file-pdf"></i></button>';
                             } else {
                                 html += 'No Document';
                             }
                             html += '</td></tr>';
                         }

                         $('#HeadData3').html(html);
                         $('#VPTableFooter31').text(totalProjectCost.toFixed(2)); // Display total in footer
                         //$('#VPTableFooter32').text(result.length); // Display total in footer

                         //$("#TotalProjectsULBWiseByFYID").modal('hide');
                         $("#ProjectByFYIDandULB").modal('show');
                         

                         $('#VPTable3').DataTable({
                             destroy: true, // Ensures re-initialization on each AJAX load
                             dom: 'Blfrtip', // Adds button container at the top
                             buttons: ["csv", "excel", "print"]
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
    <script>
        function btnTotalAmounts_Click() {
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
                            html += '<button type="button" class="btn btn-primary btn-sm" onclick="openTotalAmountUlbWiseByFYID(' + item.FYID + ')">' + (item.NoOfProjects || '') + '</button>';
                            html += '</td>';
                            html += '<td>' + (item.TotalProjectCost || '') + '</td>';
                            html += '</tr>';
                        }

                        $('#HeadDataA1').html(html);
                        $('#VATableFooter12').text(totalProjectCost.toFixed(2));
                        $('#VATableFooter11').text(totalProjectCount);
                        $("#TotalAmountsFinancialYearWise").modal('show');


                        $('#VATable1').DataTable({
                            destroy: true, // Ensures re-initialization on each AJAX load
                            dom: 'Blfrtip', // Adds button container at the top
                            buttons: ["csv", "excel", "print"]
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

        function openTotalAmountUlbWiseByFYID(FYID) {

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
                        if ($.fn.DataTable.isDataTable('#VATable2')) {
                            $('#VATable2').DataTable().clear().destroy();
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
                            html += '<button type="button" class="btn btn-primary btn-sm" onclick="AmountByFYIDandULB(' + item.FYID + ',' + item.ULBID + ')">' + (item.NoOfProjects || '') + '</button>';
                            html += '</td>';
                            html += '<td>' + (item.TotalProjectCost || '') + '</td>';
                            html += '</tr>';
                        }

                        $('#HeadDataA2').html(html);
                        $('#VATableFooter22').text(totalProjectCost.toFixed(2));
                        $('#VATableFooter21').text(totalProjectCount);
                        //$("#TotalProjectsFinancialYearWise").modal('hide');
                        $("#TotalAmountsULBWiseByFYID").modal('show');


                        $('#VATable2').DataTable({
                            destroy: true, // Ensures re-initialization on each AJAX load
                            dom: 'Blfrtip', // Adds button container at the top

                            buttons: ["csv", "excel", "print"]
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
        function AmountByFYIDandULB(FYID, ULBID) {
            $('#HeadDataA3').empty();
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
                        if ($.fn.DataTable.isDataTable('#VATable3')) {
                            $('#VATable3').DataTable().clear().destroy();
                        }

                        var html = '';
                        var totalProjectCost = 0;

                        for (var i = 0; i < result.length; i++) {
                            var item = result[i];
                            var projectCost = parseFloat(item.ProjectCost) || 0;
                            totalProjectCost += projectCost; // Accumulate the project cost

                            html += '<tr>';
                            html += '<td>' + (i + 1 || '') + '</td>';
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
                                html += '<button type="button" class="btn btn-info btn-sm" onclick="window.open(\'' + item.VPDoc + '\', \'_blank\')"><i class="bx bxs-file-pdf"></i></button>';
                            } else {
                                html += 'No Document';
                            }
                            html += '</td></tr>';
                        }

                        $('#HeadDataA3').html(html);
                        $('#VATableFooter31').text(totalProjectCost.toFixed(2)); // Display total in footer
                        //$('#VPTableFooter32').text(result.length); // Display total in footer

                        //$("#TotalProjectsULBWiseByFYID").modal('hide');
                        $("#AmountByFYIDandULB").modal('show');


                        $('#VATable3').DataTable({
                            destroy: true, // Ensures re-initialization on each AJAX load
                            dom: 'Blfrtip', // Adds button container at the top
                            buttons: ["csv", "excel", "print"]
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

    </script>

    <script>
        function btnTotalULBRepoted_Click() {
            $.ajax({
                url: "VisionPlanDashboard.aspx/btnTotalULBRepoted_Click",
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
                            var projectCount = parseInt(item.NoOfULB) || 0;
                            totalProjectCount += projectCount; //Count Total Project
                            html += '<tr>';
                            html += '<td>' + (item.FYID || '') + '</td>';
                            html += '<td>' + (item.FinancialYear_Comments || '') + '</td>';
                            html += '<td>';
                            html += '<button type="button" class="btn btn-primary btn-sm" onclick="openUlbDetailsByFYID(' + item.FYID + ')">' + (item.NoOfULB || '') + '</button>';
                            html += '</td>';
                       /*     html += '<td>' + (item.TotalProjectCost || '') + '</td>';*/
                            html += '</tr>';
                        }

                        $('#HeadDataULB1').html(html);
                        //$('#VULBTableFooter12').text(totalProjectCost.toFixed(2));
                        $('#VULBTableFooter11').text(totalProjectCount);
                        $("#TotalULBFinancialYearWise").modal('show');


                        $('#VULBTable1').DataTable({
                            destroy: true, // Ensures re-initialization on each AJAX load
                            dom: 'Blfrtip', // Adds button container at the top
                            buttons: ["csv", "excel", "print"]
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

        function openUlbDetailsByFYID(FYID) {
            debugger
            $.ajax({
                url: "VisionPlanDashboard.aspx/GetUlbDetailsByFYID",
                type: "POST",
                contentType: "application/json;charset=utf-8;",
                dataType: "json",
                data: JSON.stringify({ FYID: FYID }),
                success: function (data) {
                    console.log(data); // Inspect the response
                    if (data.d) {
                        var result = JSON.parse(data.d);

                        // Check if DataTable is already initialized and clear its contents
                        if ($.fn.DataTable.isDataTable('#VULBTable2')) {
                            $('#VULBTable2').DataTable().clear().destroy();
                        }

                        //$('#ULBData').empty(); // Clear existing rows
                        var html = '';
                        //var totalProjectCost = 0;
                        //var totalProjectCount = 0;
                        for (var i = 0; i < result.length; i++) {
                            var item = result[i];
                            //var projectCost = parseFloat(item.TotalProjectCost) || 0;
                            //totalProjectCost += projectCost; // Accumulate the project cost
                            //var projectCount = parseInt(item.NoOfProjects) || 0;
                            //totalProjectCount += projectCount; //Count Total Project
                            html += '<tr>';
                            html += '<td>' + (item.Circle_Name || '') + '</td>';
                            html += '<td>' + (item.Division_Name || '') + '</td>';
                            //html += '<td>';
                            //html += '<button type="button" class="btn btn-primary btn-sm" onclick="AmountByFYIDandULB(' + item.FYID + ',' + item.ULBID + ')">' + (item.NoOfProjects || '') + '</button>';
                            //html += '</td>';
                            //html += '<td>' + (item.TotalProjectCost || '') + '</td>';
                            html += '</tr>';
                        }

                        $('#HeadDataULB2').html(html);
                        //$('#VULBTableFooter22').text(totalProjectCost.toFixed(2));
                        //$('#VULBTableFooter21').text(totalProjectCount);
                        $("#TotalULBFinancialYearWise").modal('hide');
                        $("#TotalULBWiseByFYID").modal('show');


                        $('#VULBTable2').DataTable({
                            destroy: true, // Ensures re-initialization on each AJAX load
                            dom: 'Blfrtip', // Adds button container at the top

                            buttons: ["csv", "excel", "print"]
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
    </script>
    <!-- Modal for TotalProjectsFinancialYearWise -->
    <div class="modal fade" id="TotalProjectsFinancialYearWise" tabindex="1" aria-labelledby="TotalProjectsFinancialYearWiseLabel" aria-hidden="true">
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
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    <!-- Modal for TotalProjectsULBWiseByFYID -->
    <div class="modal fade" id="TotalProjectsULBWiseByFYID" tabindex="2" aria-labelledby="TotalProjectsULBWiseByFYIDLabel" aria-hidden="true">
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
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    <!-- Modal for ProjectByFYIDandULB -->
    <div class="modal fade" id="ProjectByFYIDandULB" tabindex="3" aria-labelledby="ProjectByFYIDandULBLabel" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="ProjectByFYIDandULBLabel">List of Projects by ULB and Financial Year</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="VPTable3" class="table table-bordered">
                            <thead>
                                <tr>
                                    <th style="width: 2%;">Sr.No.</th>
                                    <th style="width: 8%;">District Name</th>
                                    <th style="width: 7%;">ULB Name</th>
                                    <th style="width: 18%;">Project Name</th>
                                    <th style="width: 14%;">Project Type</th>
                                    <th style="width: 7%;">Project Cost</th>
                                    <th style="width: 11%;">Location</th>
                                    <th style="width: 6%;">FY</th>
                                    <th style="width: 9%;">Construction</th>
                                    <th style="width: 3%;">Priority</th>
                                    <th style="width: 8%;">Project Status</th>
                                    <th style="width: 7%;">VPDoc</th>
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
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    

     <!-- Modal for TotalAmountsFinancialYearWise -->
    <div class="modal fade" id="TotalAmountsFinancialYearWise" tabindex="1" aria-labelledby="TotalAmountFinancialYearWiseLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="TotalAmountsFinancialYearWiseLabel">Total Amounts Financial Year Wise</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VATable1" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Sr.No.</th>
                                <th>Financial Year</th>
                                <th>No Of Projects</th>
                                <th>Total Project Cost</th>
                            </tr>
                        </thead>
                        <tbody id="HeadDataA1">
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="2" style="text-align: right">Total Amount Count/Cost:</th>
                                <th id="VATableFooter11"></th>
                                <th  id="VATableFooter12"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    <!-- Modal for TotalProjectsULBWiseByFYID -->
    <div class="modal fade" id="TotalAmountsULBWiseByFYID" tabindex="2" aria-labelledby="TotalAmountsULBWiseByFYIDLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="TotalAmountsULBWiseByFYIDLabel">Total Amount ULB Wise By Financial Year</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VATable2" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>District Name</th>
                                <th>ULB Name</th>
                                <th>No of Projects</th>
                                <th>Total Project Cost</th>
                            </tr>
                        </thead>
                        <tbody id="HeadDataA2">
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="2" style="text-align: right">Total Project Count/Cost:</th>
                                <th id="VATableFooter21"></th>
                                <th  id="VATableFooter22"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    <!-- Modal for ProjectByFYIDandULB -->
    <div class="modal fade" id="AmountByFYIDandULB" tabindex="3" aria-labelledby="AmountByFYIDandULBLabel" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="AmountByFYIDandULBLabel">List of Amounts by ULB and Financial Year</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="VATable3" class="table table-bordered">
                            <thead>
                                <tr>
                                    <th style="width: 2%;">Sr.No.</th>
                                    <th style="width: 8%;">District Name</th>
                                    <th style="width: 7%;">ULB Name</th>
                                    <th style="width: 18%;">Project Name</th>
                                    <th style="width: 14%;">Project Type</th>
                                    <th style="width: 7%;">Project Cost</th>
                                    <th style="width: 11%;">Location</th>
                                    <th style="width: 6%;">FY</th>
                                    <th style="width: 9%;">Construction</th>
                                    <th style="width: 3%;">Priority</th>
                                    <th style="width: 8%;">Project Status</th>
                                    <th style="width: 7%;">VPDoc</th>
                                </tr>
                            </thead>
                            <tbody id="HeadDataA3">
                            </tbody>
                            <tfoot>
                                <tr>
                                    <%--<th style="text-align: right">Total Project:</th>--%>
                                    <%--<th  id="VPTableFooter32" style="text-align: right"></th>--%>
                                    <th colspan="5" style="text-align: right">Total Project Cost:</th>
                                    <th id="VATableFooter31"></th>
                                    <th colspan="6"></th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>


    
     <!-- Modal for TotalULBFinancialYearWise -->
    <div class="modal fade" id="TotalULBFinancialYearWise" tabindex="1" aria-labelledby="TotalULBFinancialYearWiseLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="TotalULBFinancialYearWiseLabel">Total ULB Financial Year Wise</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VULBTable1" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Sr.No.</th>
                                <th>Financial Year</th>
                                <th>No Of ULB</th>
                                <%--<th>Total Project Cost</th>--%>
                            </tr>
                        </thead>
                        <tbody id="HeadDataULB1">
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="2" style="text-align: right">Total ULB:</th>
                                <th id="VULBTableFooter11"></th>
                                <%--<th  id="VULBTableFooter12"></th>--%>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    <!-- Modal for TotalProjectsULBWiseByFYID -->
    <div class="modal fade" id="TotalULBWiseByFYID" tabindex="2" aria-labelledby="TotalULBWiseByFYIDLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="TotalULBWiseByFYIDLabel">Total  ULB  By Financial Year</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table id="VULBTable2" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>District Name</th>
                                <th>ULB Name</th>
                                <%--<th>No of Projects</th>
                                <th>Total Project Cost</th>--%>
                            </tr>
                        </thead>
                        <tbody id="HeadDataULB2">
                        </tbody>
                       <%-- <tfoot>
                            <tr>
                                <th colspan="2" style="text-align: right">Total ULB:</th>
                              <%--  <th id="VULBTableFooter21"></th>
                                <th  id="VULBTableFooter22"></th>
                            </tr>
                        </tfoot>--%>
                    </table>
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

</asp:Content>
