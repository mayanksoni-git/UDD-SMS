<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="AdvancedDashboard.aspx.cs" Inherits="AdvancedDashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="ULBFundId" runat="server" />
    <asp:HiddenField ID="ULBID" runat="server" />
    <asp:HiddenField ID="FYID" runat="server" />
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <!-- Add Chart.js for visualization -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        .dashboard-card {
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            transition: all 0.3s ease;
            height: 100%;
        }
        .dashboard-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 6px 12px rgba(0,0,0,0.15);
        }
        .card-value {
            font-size: 24px;
            font-weight: bold;
            margin: 10px 0;
        }
        .card-title {
            font-size: 14px;
            color: #6c757d;
        }
        .chart-container {
            position: relative;
            height: 300px;
            margin-bottom: 20px;
        }
        .know-more-btn {
            position: absolute;
            bottom: 10px;
            right: 10px;
            font-size: 12px;
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
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Advanced Dashboard</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">All Scheme</li>
                                            <li class="breadcrumb-item active">Advanced Dashboard</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card" id="sectionFilter" runat="server">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Filter :</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProjectType" runat="server">
                                                        <asp:Label ID="lblProjectType" runat="server" Text="Type of Work*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-select" data-placeholder="Choose a Project Type..."></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divMandal" runat="server">
                                                        <asp:Label ID="lblMandal" runat="server" Text="Division" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="Label19" runat="server" Text="Implementing Agency" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlImplAgency" runat="server" CssClass="form-select">
                                                            <asp:ListItem Value="" Text="-- Select Implementing Agency --" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="C&DS">C&DS</asp:ListItem>
                                                            <asp:ListItem Value="Irrigation Department">Irrigation Department</asp:ListItem>
                                                            <asp:ListItem Value="NN">NN</asp:ListItem>
                                                            <asp:ListItem Value="NP">NP</asp:ListItem>
                                                            <asp:ListItem Value="NPP">NPP</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-12 col-md-12 text-right">
                                                    <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>

                                <!-- Dashboard Tiles -->
                                <div class="row mt-3">
                                    <div class="col-md-4 col-lg-2">
                                        <div class="card dashboard-card bg-primary text-white">
                                            <div class="card-body">
                                                <h6 class="card-title">Divisions Reported</h6>
                                                <div class="card-value" id="divDivisionCount" runat="server">0</div>
                                                <asp:Button ID="btnKnowMoreDivisions" Visible="false" runat="server" Text="Know More" CssClass="btn btn-sm btn-light know-more-btn" OnClick="btnKnowMoreDivisions_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4 col-lg-2">
                                        <div class="card dashboard-card bg-info text-white">
                                            <div class="card-body">
                                                <h6 class="card-title">Districts Reported</h6>
                                                <div class="card-value" id="divDistrictCount" runat="server">0</div>
                                                <asp:Button ID="btnKnowMoreDistricts" Visible="false" runat="server" Text="Know More" CssClass="btn btn-sm btn-light know-more-btn" OnClick="btnKnowMoreDistricts_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4 col-lg-2">
                                        <div class="card dashboard-card bg-success text-white">
                                            <div class="card-body">
                                                <h6 class="card-title">ULBs Reported</h6>
                                                <div class="card-value" id="divULBCount" runat="server">0</div>
                                                <asp:Button ID="btnKnowMoreULBs" Visible="false" runat="server" Text="Know More" CssClass="btn btn-sm btn-light know-more-btn" OnClick="btnKnowMoreULBs_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4 col-lg-2">
                                        <div class="card dashboard-card bg-warning text-dark">
                                            <div class="card-body">
                                                <h6 class="card-title">Total Projects</h6>
                                                <div class="card-value" id="divProjectCount" runat="server">0</div>
                                                <asp:Button ID="btnKnowMoreProjects" Visible="false" runat="server" Text="Know More" CssClass="btn btn-sm btn-dark know-more-btn" OnClick="btnKnowMoreProjects_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4 col-lg-2">
                                        <div class="card dashboard-card bg-danger text-white">
                                            <div class="card-body">
                                                <h6 class="card-title">Sanctioned Cost</h6>
                                                <div class="card-value" id="divSanctionedCost" runat="server">₹0</div>
                                                <asp:Button ID="btnKnowMoreSanctionedCost" Visible="false" runat="server" Text="Know More" CssClass="btn btn-sm btn-light know-more-btn" OnClick="btnKnowMoreSanctionedCost_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4 col-lg-2">
                                        <div class="card dashboard-card bg-secondary text-white">
                                            <div class="card-body">
                                                <h6 class="card-title">Total Release</h6>
                                                <div class="card-value" id="divReleaseAmount" runat="server">₹0</div>
                                                <asp:Button ID="btnKnowMoreRelease" Visible="false" runat="server" Text="Know More" CssClass="btn btn-sm btn-light know-more-btn" OnClick="btnKnowMoreRelease_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Charts Row 1 -->
                                <div class="row mt-4">
                                    
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">Division vs Project Cost(In Lakhs)</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartDivisionCost" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">Division vs No of Projects</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartDivisionProjects" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Charts Row 2 -->
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">Division vs No of ULBs</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartDivisionULBs" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">ULB Type vs No of ULBs</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartULBType" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Charts Row 3 -->
                                <div class="row mt-4">
                                     <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">ULB Type vs Project Count</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartULBTypeCount" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">ULB Type vs Project Cost(In Lakhs)</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartULBTypeCost" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div> 
                                   
                                    
                                </div>

                                <!-- Charts Row 4 -->
                                <div class="row mt-4">
                                    
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">Projects by Implementing Agency</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartImplAgencyProjects" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">Project Cost(In Lakhs) by Implementing Agency</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartImplAgencyCost" height="300"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 d-none">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">District-wise Project Summary</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartDistrictSummary" height="600"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Charts Row 5 -->
                                <div class="row mt-4">
                                    <div class="col-md-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">District-wise Project Summary</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartDistrictProject" height="600"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div> 

                                <!-- Charts Row 6 -->
                                <div class="row mt-4">
                                    <div class="col-md-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <h5 class="card-title">District-wise Project Cost(In Lakhs) Summary</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="chart-container">
                                                    <canvas id="chartDistrictProjectCost" height="600"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Modal for Know More -->
                                <div class="modal fade" runat="server" id="knowMoreModal" tabindex="-1" role="dialog" aria-labelledby="knowMoreModalLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-lg" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="knowMoreModalLabel" runat="server">Detailed Information</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:GridView ID="gvDrillDown" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="true">
                                                </asp:GridView>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                    <asp:PostBackTrigger ControlID="ddlScheme" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlFY" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="btnKnowMoreDivisions" />
                    <asp:PostBackTrigger ControlID="btnKnowMoreDistricts" />
                    <asp:PostBackTrigger ControlID="btnKnowMoreULBs" />
                    <asp:PostBackTrigger ControlID="btnKnowMoreProjects" />
                    <asp:PostBackTrigger ControlID="btnKnowMoreSanctionedCost" />
                    <asp:PostBackTrigger ControlID="btnKnowMoreRelease" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2"></script>
    <script type="text/javascript">
        function initializeCharts(data) {
            // Pie charts
            createPieChartold('chartDivisionProjects', data.DivisionProjects.labels, data.DivisionProjects.data, 'Number of Projects');
            createPieChartold('chartDivisionCost', data.DivisionCost.labels, data.DivisionCost.data, 'Project Cost(In Lakhs) (₹)');
            createPieChartold('chartDivisionULBs', data.DivisionULBs.labels, data.DivisionULBs.data, 'Number of ULBs');
            createPieChart('chartULBType', data.ULBType.labels, data.ULBType.data, 'Number of ULBs');
            createPieChart('chartULBTypeCost', data.ULBTypeCost.labels, data.ULBTypeCost.data, 'Project Cost(In Lakhs) (₹)');
            createPieChart('chartULBTypeCount', data.ULBTypeCount.labels, data.ULBTypeCount.data, 'Project Count');

            // Bar charts
            createBarChart('chartImplAgencyProjects', data.ImplAgencyProjects.labels, [{
                label: 'Number of Projects',
                data: data.ImplAgencyProjects.data,
                backgroundColor: 'rgba(75, 192, 192, 0.7)'
            }]);

            createBarChart('chartImplAgencyCost', data.ImplAgencyCost.labels, [{
                label: 'Project Cost(In Lakhs) (₹)',
                data: data.ImplAgencyCost.data,
                backgroundColor: 'rgba(153, 102, 255, 0.7)'
            }]);

            createBarChart('chartDistrictProject', data.DistrictProject.labels, [{
                label: 'No of Projects',
                data: data.DistrictProject.data,
                backgroundColor: 'rgba(200, 102, 255, 0.7)'
            }]);

            createBarChart2('chartDistrictProjectCost', data.DistrictProjectCost.labels, [{
                label: 'Project Cost(In Lakhs) (₹)',
                data: data.DistrictProjectCost.data,
                backgroundColor: 'rgba(50, 102, 255, 0.7)'
            }]);

            // District summary chart (grouped bar chart)
            createBarChart('chartDistrictSummary', data.DistrictSummary.labels, data.DistrictSummary.datasets);
        }
        function getRandomColors(count) {
            const colors = [
                '#4dc9f6', '#f67019', '#f53794', '#537bc4',
                '#acc236', '#166a8f', '#00a950', '#58595b',
                '#8549ba', '#e6194b', '#3cb44b', '#ffe119'
            ];
            return Array.from({ length: count }, (_, i) => colors[i % colors.length]);
        }

        function createPieChart(canvasId, labels, data, label) {
            const ctx = document.getElementById(canvasId).getContext('2d');
            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        label: label,
                        data: data,
                        backgroundColor: getRandomColors(labels.length),
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right',
                        },
                        datalabels: {
                            color: '#000',
                            formatter: function (value, context) {
                                let percentage = ((value / context.chart._metasets[0].total) * 100).toFixed(1);
                                return value > 0 ? `${value} (${percentage}%)` : '';
                            },
                            anchor: 'end',
                            align: function (context) {
                                const angle = context.chart.getDatasetMeta(0).data[context.dataIndex].startAngle;
                                const size = context.chart.getDatasetMeta(0).data[context.dataIndex].outerRadius;
                                const value = context.chart.data.datasets[0].data[context.dataIndex];
                                return value > 10 ? 'center' : 'end'; // Adjust threshold as needed
                            },
                            offset: 10,
                            clamp: true
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    const label = context.label || '';
                                    const val = context.parsed;
                                    if (canvasId.includes('Cost')) {
                                        return `${label}: ₹${val.toLocaleString('en-IN')}`;
                                    }
                                    return `${label}: ${val}`;
                                }
                            }
                        }
                    }
                },
                plugins: [ChartDataLabels]
            });
        }


        function createBarChart(canvasId, labels, datasets) {
            const ctx = document.getElementById(canvasId).getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: datasets
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                callback: function (value) {
                                    if (canvasId.toLowerCase().includes('cost')) {
                                        return '₹' + value.toLocaleString('en-IN');
                                    }
                                    return value;
                                }
                            }
                        }
                    },
                    plugins: {
                        datalabels: {
                            anchor: 'end',
                            align: 'top',
                            color: '#000',
                            formatter: function (value, context) {
                                if (canvasId.toLowerCase().includes('cost')) {
                                    return '₹' + value.toLocaleString('en-IN');
                                }
                                return value;
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.dataset.label || '';
                                    if (label) label += ': ';
                                    const val = context.parsed.y;
                                    if (canvasId.toLowerCase().includes('cost')) {
                                        label += '₹' + val.toLocaleString('en-IN');
                                    } else {
                                        label += val;
                                    }
                                    return label;
                                }
                            }
                        }
                    }
                },
                plugins: [ChartDataLabels]
            });
        }

        function createBarChart2(canvasId, labels, datasets) {
            const ctx = document.getElementById(canvasId).getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: datasets
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                callback: function (value) {
                                    if (canvasId.toLowerCase().includes('cost')) {
                                        return '₹' + value.toLocaleString('en-IN');
                                    }
                                    return value;
                                }
                            }
                        }
                    },
                    plugins: {
                        datalabels: {
                            anchor: 'end',
                            align: 'top',
                            color: '#000',
                            rotation: -90, // Rotates label vertically
                            textAlign: 'center',
                            font: {
                                size: 10,
                                weight: 'bold'
                            },
                            formatter: function (value, context) {
                                if (canvasId.toLowerCase().includes('cost')) {
                                    return '₹' + value.toLocaleString('en-IN');
                                }
                                return value;
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.dataset.label || '';
                                    if (label) label += ': ';
                                    const val = context.parsed.y;
                                    if (canvasId.toLowerCase().includes('cost')) {
                                        label += '₹' + val.toLocaleString('en-IN');
                                    } else {
                                        label += val;
                                    }
                                    return label;
                                }
                            }
                        }
                    }
                },
                plugins: [ChartDataLabels]
            });
        }


























        function createPieChartold(canvasId, labels, data, label) {
            var ctx = document.getElementById(canvasId).getContext('2d');
            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        label: label,
                        data: data,
                        backgroundColor: getRandomColors(labels.length),
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right',
                        },
                        tooltip: {
                            callbacks: {
                                label: function(context) {
                                    var label = context.label || '';
                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed !== null) {
                                        if (canvasId.includes('Cost')) {
                                            label += '₹' + context.parsed.toLocaleString('en-IN');
                                        } else {
                                            label += context.parsed;
                                        }
                                    }
                                    return label;
                                }
                            }
                        }
                    }
                }
            });
        }
        function createBarChartold(canvasId, labels, datasets) {
            var ctx = document.getElementById(canvasId).getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: datasets
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                callback: function (value) {
                                    if (canvasId.includes('Cost') || canvasId.includes('cost')) {
                                        return '₹' + value.toLocaleString('en-IN');
                                    }
                                    return value;
                                }
                            }
                        }
                    },
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    var label = context.dataset.label || '';
                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed.y !== null) {
                                        if (canvasId.includes('Cost') || canvasId.includes('cost')) {
                                            label += '₹' + context.parsed.y.toLocaleString('en-IN');
                                        } else {
                                            label += context.parsed.y;
                                        }
                                    }
                                    return label;
                                }
                            }
                        }
                    }
                }
            });
        }
        function getRandomColors(count) {
            var colors = [];
            for (var i = 0; i < count; i++) {
                colors.push('rgba(' + 
                    Math.floor(Math.random() * 256) + ',' + 
                    Math.floor(Math.random() * 256) + ',' + 
                    Math.floor(Math.random() * 256) + ',0.7)');
            }
            return colors;
        }
        function showModal() {
            $('#knowMoreModal').modal('show');
        }
    </script>
</asp:Content>