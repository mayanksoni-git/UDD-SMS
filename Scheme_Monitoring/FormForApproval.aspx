<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval.aspx.cs" Inherits="FormForApproval" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <style>
        canvas {
            max-width: 500px;
            max-height: 300px;
        }
    </style>
    <script>
        $(document).ready(function () {
            //function BindChart() {
                
                var Fy = $("#ctl00_ContentPlaceHolder1_ddlFY").val();
                var Zone_Id = $("#ctl00_ContentPlaceHolder1_ddlZone").val();
                var Circle_Id = $("#ctl00_ContentPlaceHolder1_ddlCircle").val();
                var Division_Id = $("#ctl00_ContentPlaceHolder1_ddlDivision").val();
                var Scheme = $("#ctl00_ContentPlaceHolder1_ddlProjectMaster").val();

                $.ajax({
                    type: "POST",
                    url: "FormForApproval.aspx/GetProposalData",
                    data: "{Fy:'" + Fy + "',Zone_Id:'" + Zone_Id + "',Circle_Id:'" + Circle_Id + "',Division_Id:'" + Division_Id + "',Scheme:'" + Scheme + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        //Bar Chart for No of Work Proposals--------------------------------------------------------------------------------
                        var data = JSON.parse(response.d);

                        // Prepare data for Chart.js
                        var chartData = {
                            labels: ['Total', 'Pending', 'Approved', 'Rejected', 'Hold'],
                            datasets: [{
                                label: 'Number of Work Plans',
                                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                                borderColor: 'rgba(54, 162, 235, 1)',
                                borderWidth: 1,
                                data: [
                                    data.totalProposals,
                                    data.pendingProposals,
                                    data.approvedProposals,
                                    data.rejectedProposals,
                                    data.holdProposals
                                ]
                            }]
                        };

                        // Configuration for Chart.js
                        var config = {
                            type: 'bar',
                            data: chartData,
                            options: {
                                responsive: true,
                                plugins: {
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                return tooltipItem.dataset.label + ': ' + tooltipItem.raw;
                                            }
                                        }
                                    }
                                },
                                scales: {
                                    x: {
                                        title: {
                                            display: true,
                                            text: 'Work Plan Status'
                                        }
                                    },
                                    y: {
                                        title: {
                                            display: true,
                                            text: 'Number of Work Plans'
                                        }
                                    }
                                }
                            }
                        };

                        // Create the chart
                        var ctx = document.getElementById('proposalChart').getContext('2d');
                        var myChart = new Chart(ctx, config);



                        //Pie Chart for No of Work Proposals--------------------------------------------------------------------------------
                        // Prepare data for Chart.js
                        var chartData = {
                            labels: [/*'Total', */'Pending', 'Approved', 'Rejected', 'Hold'],
                            datasets: [{
                                label: 'Number of Work Plans',
                                backgroundColor: [
                                    /*'rgba(54, 162, 235, 0.6)',*/
                                    'rgba(255, 206, 86, 0.6)',
                                    'rgba(75, 192, 192, 0.6)',
                                    'rgba(255, 99, 132, 0.6)',
                                    'rgba(153, 102, 255, 0.6)'
                                ],
                                borderColor: [
                                    /*'rgba(54, 162, 235, 1)',*/
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(153, 102, 255, 1)'
                                ],
                                borderWidth: 1,
                                data: [
                                    /*data.totalProposals,*/
                                    data.pendingProposals,
                                    data.approvedProposals,
                                    data.rejectedProposals,
                                    data.holdProposals
                                ]
                            }]
                        };

                        // Configuration for Chart.js
                        var config = {
                            type: 'pie',
                            data: chartData,
                            options: {
                                responsive: true,
                                plugins: {
                                    datalabels: {
                                        formatter: (value, context) => {
                                            let label = context.chart.data.labels[context.dataIndex];
                                            return label + ': ' + value;
                                        },
                                        color: '#666666',
                                        font: {
                                            weight: 'bold'
                                        }
                                    },
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                let label = tooltipItem.label;
                                                let value = tooltipItem.raw;
                                                return label + ': ' + value;
                                            }
                                        }
                                    }
                                }
                            },
                            plugins: [ChartDataLabels]
                        };

                        // Create the chart
                        var ctx = document.getElementById('proposalPieChart').getContext('2d');
                        var myChart = new Chart(ctx, config);


                        //Pie Chart for Percent of Work Proposals-------------------------------------------------------------------------------
                        // Prepare data for Chart.js
                        var chartData = {
                            labels: [/*'Total', */'Pending(%)', 'Approved(%)', 'Rejected(%)', 'Hold(%)'],
                            datasets: [{
                                label: 'Percentage of Work Plans',
                                backgroundColor: [
                                    /*'rgba(54, 162, 235, 0.6)',*/
                                    'rgba(255, 206, 86, 0.6)',
                                    'rgba(75, 192, 192, 0.6)',
                                    'rgba(255, 99, 132, 0.6)',
                                    'rgba(153, 102, 255, 0.6)'
                                ],
                                borderColor: [
                                    /*'rgba(54, 162, 235, 1)',*/
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(153, 102, 255, 1)'
                                ],
                                borderWidth: 1,
                                data: [
                                    /*data.totalProposals,*/
                                    data.pendingProposalsPercentage,
                                    data.approvedProposalsPercentage,
                                    data.rejectedProposalsPercentage,
                                    data.holdProposalsPercentage
                                ]
                            }]
                        };

                        // Configuration for Chart.js
                        var config = {
                            type: 'pie',
                            data: chartData,
                            options: {
                                responsive: true,
                                plugins: {
                                    datalabels: {
                                        formatter: (value, context) => {
                                            let label = context.chart.data.labels[context.dataIndex];
                                            return label + ': ' + value;
                                        },
                                        color: '#666666',
                                        font: {
                                            weight: 'bold'
                                        }
                                    },
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                let label = tooltipItem.label;
                                                let value = tooltipItem.raw;
                                                return label + ': ' + value;
                                            }
                                        }
                                    }
                                }
                            },
                            plugins: [ChartDataLabels]
                        };

                        // Create the chart
                        var ctx = document.getElementById('proposalPieChartPercent').getContext('2d');
                        var myChart = new Chart(ctx, config);



                        //Bar Chart for Amount of Work Proposals--------------------------------------------------------------------------------

                        // Prepare data for Chart.js
                        var chartData = {
                            labels: ['Total Amount', 'Pending Amount', 'Approved Amount', 'Rejected Amount', 'Hold Amount'],
                            datasets: [{
                                label: 'Amount of Work Plans',
                                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                                borderColor: 'rgba(54, 162, 235, 1)',
                                borderWidth: 1,
                                data: [
                                    data.TotalAmount,
                                    data.PendingProposalsAmount,
                                    data.ApprovedProposalsAmount,
                                    data.RejectProposalsAmount,
                                    data.HoldProposalsAmount
                                ]
                            }]
                        };

                        // Configuration for Chart.js
                        var config = {
                            type: 'bar',
                            data: chartData,
                            options: {
                                responsive: true,
                                plugins: {
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                return tooltipItem.dataset.label + ': ' + tooltipItem.raw;
                                            }
                                        }
                                    }
                                },
                                scales: {
                                    x: {
                                        title: {
                                            display: true,
                                            text: 'Work Plan Amount Status'
                                        }
                                    },
                                    y: {
                                        title: {
                                            display: true,
                                            text: 'Amount of Work Plans'
                                        }
                                    }
                                }
                            }
                        };

                        // Create the chart
                        var ctx = document.getElementById('proposalChart1').getContext('2d');
                        var myChart = new Chart(ctx, config);


                        //Pie Chart for Amount of Work Proposals--------------------------------------------------------------------------------

                        // Prepare data for Chart.js
                        var chartData = {
                            labels: [/*'Total', */'Pending Amount', 'Approved Amount', 'Rejected Amount', 'Hold Amount'],
                            datasets: [{
                                label: 'Amount of Work Plans',
                                backgroundColor: [
                                    /*'rgba(54, 162, 235, 0.6)',*/
                                    'rgba(255, 206, 86, 0.6)',
                                    'rgba(75, 192, 192, 0.6)',
                                    'rgba(255, 99, 132, 0.6)',
                                    'rgba(153, 102, 255, 0.6)'
                                ],
                                borderColor: [
                                    /*'rgba(54, 162, 235, 1)',*/
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(153, 102, 255, 1)'
                                ],
                                borderWidth: 1,
                                data: [
                                    /*data.totalProposals,*/
                                    data.PendingProposalsAmount,
                                    data.ApprovedProposalsAmount,
                                    data.RejectProposalsAmount,
                                    data.HoldProposalsAmount
                                ]
                            }]
                        };

                        // Configuration for Chart.js
                        var config = {
                            type: 'pie',
                            data: chartData,
                            options: {
                                responsive: true,
                                plugins: {
                                    datalabels: {
                                        formatter: (value, context) => {
                                            let label = context.chart.data.labels[context.dataIndex];
                                            return label + ': ' + value;
                                        },
                                        color: '#666666',
                                        font: {
                                            weight: 'bold'
                                        }
                                    },
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                let label = tooltipItem.label;
                                                let value = tooltipItem.raw;
                                                return label + ': ' + value;
                                            }
                                        }
                                    }
                                }
                            },
                            plugins: [ChartDataLabels]
                        };

                        // Create the chart
                        var ctx = document.getElementById('proposalPieChart1').getContext('2d');
                        var myChart = new Chart(ctx, config);


                        //Pie Chart for Percent Amount of Work Proposals------------------------------------------------------------------------

                        // Prepare data for Chart.js
                        var chartData = {
                            labels: [/*'Total', */'Pending Amount(%)', 'Approved Amount(%)', 'Rejected Amount(%)', 'Hold Amount(%)'],
                            datasets: [{
                                label: 'Percetage Amount of Proposals',
                                backgroundColor: [
                                    /*'rgba(54, 162, 235, 0.6)',*/
                                    'rgba(255, 206, 86, 0.6)',
                                    'rgba(75, 192, 192, 0.6)',
                                    'rgba(255, 99, 132, 0.6)',
                                    'rgba(153, 102, 255, 0.6)'
                                ],
                                borderColor: [
                                    /*'rgba(54, 162, 235, 1)',*/
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(153, 102, 255, 1)'
                                ],
                                borderWidth: 1,
                                data: [
                                    /*data.totalProposals,*/
                                    data.PendingProposalsAmountPercentage,
                                    data.ApprovedProposalsAmountPercentage,
                                    data.RejectProposalsAmountPercentage,
                                    data.HoldProposalsAmountPercentage
                                ]
                            }]
                        };

                        // Configuration for Chart.js
                        var config = {
                            type: 'pie',
                            data: chartData,
                            options: {
                                responsive: true,
                                plugins: {
                                    datalabels: {
                                        formatter: (value, context) => {
                                            let label = context.chart.data.labels[context.dataIndex];
                                            return label + ': ' + value;
                                        },
                                        color: '#666666',
                                        font: {
                                            weight: 'bold'
                                        }
                                    },
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                let label = tooltipItem.label;
                                                let value = tooltipItem.raw;
                                                return label + ': ' + value;
                                            }
                                        }
                                    }
                                }
                            },
                            plugins: [ChartDataLabels]
                        };

                        // Create the chart
                        var ctx = document.getElementById('proposalPieChartPercent1').getContext('2d');
                        var myChart = new Chart(ctx, config);

                    },
                    error: function (error) {
                        console.log("Error fetching data: ", error);
                    }
                });
            //}

            window.BindChart = BindChart;
            
        });
</script>

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
                                    <h4 class="mb-sm-0">Create Work Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Plan Management System</li>
                                            <li class="breadcrumb-item active">Create Work Plan</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Plan Detail</h4>
                                        <%--OnClientClick="BindChart(); return false;"--%>
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"  runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                        <%--<a class="btn btn-primary" href="#">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-arrow-left-circle-fill" viewBox="0 0 16 16">
                                                    <path d="M8 0a8 8 0 1 0 0 16A8 8 0 0 0 8 0m3.5 7.5a.5.5 0 0 1 0 1H5.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L5.707 7.5z" />
                                                </svg>
                                                Back to Report</a>--%>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divZoneOfULB">
                                                    <asp:Label ID="lblZoneOfULB" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtZoneOfULB" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divWard">
                                                    <asp:Label ID="lblWard" runat="server" Text="Ward*" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtWard" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectMaster_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divWorkType" runat="server">
                                                        <asp:Label ID="lblWorkType" runat="server" Text="Type of Work*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlWorkType" runat="server" CssClass="form-select"></asp:DropDownList>

                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="lblExpectedAmount" runat="server" Text="Expected Amount(In Rupees)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtExpectedAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblRole" runat="server" Text="Select Proposer*" CssClass="form-label"></asp:Label>
                                                    <asp:RadioButtonList ID="rblRoles" runat="server" CssClass="form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblRoles_SelectedIndexChanged">
                                                        <asp:ListItem Text="MP" Value="MP"></asp:ListItem>
                                                        <asp:ListItem Text="MLA" Value="MLA"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divMPMLA" style="display: block;" runat="server">
                                                    <asp:Label ID="lblMPMLA" runat="server" Text="MP/MLA*" CssClass="form-label"></asp:Label>
                                                    <asp:DropDownList ID="ddlMPMLA" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMPMLA_SelectedIndexChanged"></asp:DropDownList>
                                                </div>



                                                <div class="col-xxl-3 col-md-6" id="divOthers" visible="false" runat="server">
                                                    <asp:Label ID="lblOther" runat="server" Text="Name of Proposer*" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divParty" style="display: block;" runat="server">
                                                    <asp:Label ID="lblParty" runat="server" Text="Political Party" CssClass="form-label" ></asp:Label>
                                                    <asp:TextBox ID="lblParyOfMPMLA" runat="server" Text="" CssClass="form-control" Enabled="false"></asp:TextBox>

                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="divConstituency" style="display: block;" runat="server">
                                                    <asp:Label ID="lblConstituency" runat="server" Text="Constituency" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="lblConstituencyName" runat="server" Text="" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No*" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" TextMode="Phone" MaxLength="10"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revMobileNo" runat="server"
                                                        ControlToValidate="txtMobileNo"
                                                        ErrorMessage="Please enter a valid mobile number."
                                                        CssClass="text-danger"
                                                        ValidationExpression="^\d{10}$">
                                                    </asp:RegularExpressionValidator>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDesignation" runat="server">
                                                        <asp:Label ID="lblDesignation" runat="server" Text="Designation (If Any)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblRecommendationLetter" runat="server" Text="Upload Recommendation Letter" CssClass="form-label"></asp:Label>
                                                    <asp:FileUpload ID="fileUploadRecommendationLetter" runat="server" CssClass="form-control" />
                                                    <asp:HyperLink ID="hypRecommendationLetterEdit" runat="server" Target="_blank" Text="Click To View" Visible="false">
                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                    </asp:HyperLink>
                                                    <asp:HiddenField id="hfPDFUrl" runat="server"/>
                                                    <asp:RegularExpressionValidator ID="revFileUpload" runat="server"
                                                        ControlToValidate="fileUploadRecommendationLetter"
                                                        ErrorMessage="Only PDF files are allowed."
                                                        CssClass="text-danger"
                                                        ValidationExpression="^.*\.(pdf)$">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:CustomValidator ID="cvFileSize" runat="server"
                                                        ControlToValidate="fileUploadRecommendationLetter"
                                                        ErrorMessage="File size cannot exceed 5MB."
                                                        CssClass="text-danger"
                                                        OnServerValidate="cvFileSize_ServerValidate">
                                                    </asp:CustomValidator>
                                                </div>

                                                <div class="col-xxl-2 offset-xxl-1 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="container-fluid">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Statics of Work Plan</h4>
                                </div>
                                <div class="card-body">
                                    <div class="live-preview">
                                        <div class="row">
                                            <div class="col-xxl-4 col-md-12">
                                                <div>
                                                    <div>
                                                        <!-- Canvas element for Chart.js -->
                                                        <canvas id="proposalChart"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xxl-4 col-md-12">
                                                <div>
                                                    <div>
                                                        <canvas id="proposalPieChart"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xxl-4 col-md-12">
                                                <div>
                                                    <div>
                                                        <canvas id="proposalPieChartPercent"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <br />
                                        <div class="row">
                                            <div class="col-xxl-4 col-md-12">
                                                <div>
                                                    <div>
                                                        <!-- Canvas element for Chart.js -->
                                                        <canvas id="proposalChart1"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xxl-4 col-md-12">
                                                <div>
                                                    <div>
                                                        <canvas id="proposalPieChart1"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xxl-4 col-md-12">
                                                <div>
                                                    <div>
                                                        <canvas id="proposalPieChartPercent1"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>



                    
                    
                    <div runat="server" visible="false" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Detail</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                        <%--<asp:Chart ID="Chart1" runat="server" Width="500" Height="300">
                                                            <Titles>
                                                                <asp:Title Font="Bold, 14pt" Name="Title1" Text="Proposal Status"></asp:Title>
                                                            </Titles>
                                                            <Series>
                                                                <asp:Series Name="Series1" ChartType="Column">
                                                                </asp:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea1">
                                                                    <AxisX Title="Proposal Status">
                                                                        <MajorGrid Enabled="false" />
                                                                    </AxisX>
                                                                    <AxisY Title="Number of Proposals">
                                                                        <MajorGrid Enabled="true" />
                                                                    </AxisY>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                        </asp:Chart>--%>

                                                        
                                                    </div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="gvRecords" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChanging" PageSize="5">
                                                        <Columns>
                                                            <asp:BoundField DataField="WorkProposalId" HeaderText="Work Proposal Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Proposal Code" DataField="ProposalCode" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="FinYear" />
                                                            <asp:BoundField HeaderText="State" DataField="Zone_Name" />
                                                            <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="ULB" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="Zone" DataField="ZoneOfULB" />
                                                            <asp:BoundField HeaderText="Ward" DataField="Ward" />
                                                            <asp:BoundField HeaderText="Scheme" DataField="Project_Name" />
                                                            <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                            <asp:BoundField HeaderText="Expected Amount" DataField="ExpectedAmount" />
                                                            <asp:BoundField HeaderText="Proposer Type" DataField="ProposerType" />
                                                            <asp:BoundField HeaderText="MP/MLA Name" DataField="MPMLAName" />
                                                            <asp:BoundField HeaderText="Proposer Name" DataField="ProposerName" />
                                                            <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                                                            <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                                            <asp:TemplateField HeaderText="Recommendation Letter">
                                                                <ItemTemplate>
                                                                    
                                                                    <asp:HyperLink ID="hypRecommendationLetter" runat="server" Target="_blank" NavigateUrl='<%# Eval("RecomendationLetter") %>' Text="Click To View" Visible='<%# !string.IsNullOrEmpty(Eval("RecomendationLetter").ToString()) %>'>
                                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Added On" DataField="AddedOn" DataFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />
                                                        
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnExportToExcel" />
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlProjectMaster" />
                    <asp:PostBackTrigger ControlID="rblRoles" />
                </Triggers>
            </asp:UpdatePanel>
            <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                            <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                                <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
        </div>
    </div>
    <style>
        .form-control img {
            height: 20px;
            width: 20px;
            vertical-align: middle;
            margin-right: 10px;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td {
            height: 35px;
            width: 35px;
            line-height: 35px;
            display: inline-block;
            background: #dce9f7;
            border: 1px solid #d1e3f7;
            text-align: center;
            margin: 0 2px;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td a {
            height: 35px;
            color: #000;
            width: 35px;
            display: block;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td:hover {
            background: #c5dffb;
            border: 1px solid #bbdbff;
        }
    </style>

<%--    <script>
        $(document).ready(function () {
            // Fetch data from backend or use hardcoded data
            var data = {
                labels: ['Total', 'Pending', 'Approved', 'Rejected', 'Hold'],
                datasets: [{
                    label: 'Number of Proposals',
                    backgroundColor: 'rgba(54, 162, 235, 0.6)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1,
                    data: [15, 5, 5, 3, 2]
                }]
            };

            // Configuration for Chart.js
            var config = {
                type: 'bar',
                data: data,
                options: {
                    responsive: true,
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: function(tooltipItem) {
                                    return tooltipItem.dataset.label + ': ' + tooltipItem.raw;
                                }
                            }
                        }
                    },
                    scales: {
                        x: {
                            title: {
                                display: true,
                                text: 'Proposal Status'
                            }
                        },
                        y: {
                            title: {
                                display: true,
                                text: 'Number of Proposals'
                            }
                        }
                    }
                }
            };

            // Create the chart
            var ctx = document.getElementById('proposalChart').getContext('2d');
            var myChart = new Chart(ctx, config);
        });
    </script>--%>

    


</asp:Content>
