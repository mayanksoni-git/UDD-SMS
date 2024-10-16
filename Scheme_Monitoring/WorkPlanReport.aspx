<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="WorkPlanReport.aspx.cs" Inherits="WorkPlanReport" EnableEventValidation="false" ValidateRequest="false" %>
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
    <%--<script>
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
                            label: 'Number of Work Proposals',
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
                                        text: 'Work Proposal Status'
                                    }
                                },
                                y: {
                                    title: {
                                        display: true,
                                        text: 'Number of Work Proposals'
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
                            label: 'Number of Work Proposals',
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
                            label: 'Percentage of Work Proposals',
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
                            label: 'Amount of Work Proposals',
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
                                        text: 'Work Proposals Amount Status'
                                    }
                                },
                                y: {
                                    title: {
                                        display: true,
                                        text: 'Amount of Work Proposals'
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
                            label: 'Amount of Work Proposals',
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
    </script>--%>
    <script>
        $(document).ready(function () {
            function fetchChartData(url, data) {
                return $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                });
            }

            function createBarChart(ctx, labels, data, title) {
                return new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: title,
                            backgroundColor: 'rgba(54, 162, 235, 0.6)',
                            borderColor: 'rgba(54, 162, 235, 1)',
                            borderWidth: 1,
                            data: data
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            tooltip: {
                                callbacks: {
                                    label: (tooltipItem) => `${tooltipItem.dataset.label}: ${tooltipItem.raw}`
                                }
                            }
                        },
                        scales: {
                            x: { title: { display: true, text: 'Work Proposals Status' } },
                            y: { title: { display: true, text: 'Number of Work Proposals' } }
                        }
                    }
                });
            }

            function createPieChart(ctx, labels, data, title, backgroundColors, borderColors) {
                return new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: title,
                            backgroundColor: backgroundColors,
                            borderColor: borderColors,
                            borderWidth: 1,
                            data: data
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            datalabels: {
                                formatter: (value, context) => `${context.chart.data.labels[context.dataIndex]}: ${value}`,
                                color: '#666666',
                                font: { weight: 'bold' }
                            },
                            tooltip: {
                                callbacks: {
                                    label: (tooltipItem) => `${tooltipItem.label}: ${tooltipItem.raw}`
                                }
                            }
                        }
                    },
                    plugins: [ChartDataLabels]
                });
            }

            var Fy = $("#ctl00_ContentPlaceHolder1_ddlFY").val();
            var Zone_Id = $("#ctl00_ContentPlaceHolder1_ddlZone").val();
            var Circle_Id = $("#ctl00_ContentPlaceHolder1_ddlCircle").val();
            var Division_Id = $("#ctl00_ContentPlaceHolder1_ddlDivision").val();
            var Scheme = $("#ctl00_ContentPlaceHolder1_ddlProjectMaster").val();

            fetchChartData("WorkPlanReport.aspx/GetProposalData", { Fy, Zone_Id, Circle_Id, Division_Id, Scheme })
                .done(function (response) {
                    var data = JSON.parse(response.d);

                    createBarChart(document.getElementById('proposalChart').getContext('2d'),
                        ['Total', 'Pending', 'Approved', 'Rejected', 'Hold'],
                        [data.totalProposals, data.pendingProposals, data.approvedProposals, data.rejectedProposals, data.holdProposals],
                        'Number of Work Proposals'
                    );

                    createPieChart(document.getElementById('proposalPieChart').getContext('2d'),
                        ['Pending', 'Approved', 'Rejected', 'Hold'],
                        [data.pendingProposals, data.approvedProposals, data.rejectedProposals, data.holdProposals],
                        'Number of Work Proposals',
                        ['rgba(255, 206, 86, 0.6)', 'rgba(75, 192, 192, 0.6)', 'rgba(255, 99, 132, 0.6)', 'rgba(153, 102, 255, 0.6)'],
                        ['rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)', 'rgba(153, 102, 255, 1)']
                    );

                    createPieChart(document.getElementById('proposalPieChartPercent').getContext('2d'),
                        ['Pending(%)', 'Approved(%)', 'Rejected(%)', 'Hold(%)'],
                        [data.pendingProposalsPercentage, data.approvedProposalsPercentage, data.rejectedProposalsPercentage, data.holdProposalsPercentage],
                        'Percentage of Work Proposals',
                        ['rgba(255, 206, 86, 0.6)', 'rgba(75, 192, 192, 0.6)', 'rgba(255, 99, 132, 0.6)', 'rgba(153, 102, 255, 0.6)'],
                        ['rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)', 'rgba(153, 102, 255, 1)']
                    );

                    createBarChart(document.getElementById('proposalChart1').getContext('2d'),
                        ['Total Amount', 'Pending Amount', 'Approved Amount', 'Rejected Amount', 'Hold Amount'],
                        [data.TotalAmount, data.PendingProposalsAmount, data.ApprovedProposalsAmount, data.RejectProposalsAmount, data.HoldProposalsAmount],
                        'Amount of Work Proposals'
                    );

                    createPieChart(document.getElementById('proposalPieChart1').getContext('2d'),
                        ['Pending Amount', 'Approved Amount', 'Rejected Amount', 'Hold Amount'],
                        [data.PendingProposalsAmount, data.ApprovedProposalsAmount, data.RejectProposalsAmount, data.HoldProposalsAmount],
                        'Amount of Work Proposals',
                        ['rgba(255, 206, 86, 0.6)', 'rgba(75, 192, 192, 0.6)', 'rgba(255, 99, 132, 0.6)', 'rgba(153, 102, 255, 0.6)'],
                        ['rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)', 'rgba(153, 102, 255, 1)']
                    );

                    createPieChart(document.getElementById('proposalPieChartPercent1').getContext('2d'),
                        ['Pending Amount(%)', 'Approved Amount(%)', 'Rejected Amount(%)', 'Hold Amount(%)'],
                        [data.PendingProposalsAmountPercentage, data.ApprovedProposalsAmountPercentage, data.RejectProposalsAmountPercentage, data.HoldProposalsAmountPercentage],
                        'Percentage Amount of Proposals',
                        ['rgba(255, 206, 86, 0.6)', 'rgba(75, 192, 192, 0.6)', 'rgba(255, 99, 132, 0.6)', 'rgba(153, 102, 255, 0.6)'],
                        ['rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)', 'rgba(153, 102, 255, 1)']
                    );
                })
                .fail(function (error) {
                    console.log("Error fetching data: ", error);
                });

            window.BindChart = fetchChartData;
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
                                    <h4 class="mb-sm-0">Work Proposal Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Proposal Management</li>
                                            <li class="breadcrumb-item active">Work Proposal Report</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Report</h4>
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
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divMandal" runat="server">
                                                        <asp:Label ID="lblMandal" runat="server" Text="Division" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-select"  AutoPostBack="true" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="lblULBType" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select" RepeatDirection="Horizontal"  AutoPostBack="true" OnSelectedIndexChanged="ddlULBType_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Nigam" Value="NN"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Panchayat" Value="NP"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Palika Parishad" Value="NPP"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divSection" runat="server">
                                                        <asp:Label ID="lblSectin" runat="server" Text="Section" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divRole" runat="server">
                                                        <asp:Label ID="lblRole" runat="server" Text="Proposer Type*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="rblRoles" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="-Select-" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Central Minister" Value="Central Minister"></asp:ListItem>
                                                            <asp:ListItem Text="Minister" Value="Minister"></asp:ListItem>
                                                            <asp:ListItem Text="MP" Value="MP"></asp:ListItem>
                                                            <asp:ListItem Text="MLA" Value="MLA"></asp:ListItem>
                                                            <asp:ListItem Text="MLC" Value="MLC"></asp:ListItem>
                                                            <asp:ListItem Text="Mayor" Value="Mayor"></asp:ListItem>
                                                            <asp:ListItem Text="Municipal Commissioner" Value="Municipal Commissioner"></asp:ListItem>
                                                            <asp:ListItem Text="District Magistrate" Value="District Magistrate"></asp:ListItem>
                                                            <asp:ListItem Text="Divisional Commissioner" Value="Divisional Commissioner"></asp:ListItem>
                                                            <asp:ListItem Text="President Nagar Panchayat" Value="President Nagar Panchayat"></asp:ListItem>
                                                            <asp:ListItem Text="EO Nagar Panchayat" Value="EO Nagar Panchayat"></asp:ListItem>
                                                            <asp:ListItem Text="President Nagar Palika Parishad" Value="President Nagar Palika Parishad"></asp:ListItem>
                                                            <asp:ListItem Text="EO Nagar Palika Parishad" Value="EO Nagar Palika Parishad"></asp:ListItem>
                                                            <asp:ListItem Text="उत्तर प्रदेश जल निगम (नगरीय)" Value="उत्तर प्रदेश जल निगम (नगरीय)"></asp:ListItem>
                                                            <asp:ListItem Text="C&DS (नगरीय)" Value="C&DS (नगरीय)"></asp:ListItem>
                                                            <asp:ListItem Text="Ex-MLA" Value="Ex-MLA"></asp:ListItem>
                                                            <asp:ListItem Text="Ex-MP" Value="Ex-MP"></asp:ListItem>
                                                            <asp:ListItem Text="प्रदेश अध्यक्ष" Value="प्रदेश अध्यक्ष"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divExpAmtLess" runat="server">
                                                        <asp:Label ID="lblExpAmtLess" runat="server" Text="Expected Amount(In Lakhs) Less Than*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtExpAmtLess" runat="server" placeholder="31.89" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divExpAmtGret" runat="server">
                                                        <asp:Label ID="lblExpAmtGret" runat="server" Text="Expected Amount(In Lakhs) Greater Than*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtExpAmtGret" runat="server" placeholder="31.89" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblIsAkanshiULB" CssClass="control-label no-padding-right" Text="Akanshi ULB" runat="server"></asp:Label>
                                                        <asp:CheckBox ID="chkIsAkanshiULB" runat="server" CssClass="form-control mb-2" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-2 offset-xxl-10 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
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
                                    <h4 class="card-title mb-0 flex-grow-1">Work Proposal Stats</h4>
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
                                        <%--<asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />--%>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                        
                                                    </div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <asp:GridView  ID="grdPost" runat="server" CssClass="display table table-bordered"  AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
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
                                                            <asp:BoundField HeaderText="Proposal Code" DataField="ProposalCode" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="FinYear" />
                                                            <%--<asp:BoundField HeaderText="State" DataField="Zone_Name" />--%>
                                                            <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="ULB" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="Zone" DataField="ZoneOfULB" />
                                                            <asp:BoundField HeaderText="Ward" DataField="Ward" />
                                                            <%--<asp:BoundField HeaderText="Scheme" DataField="ShortNameCode" />--%>
                                                            <asp:BoundField HeaderText="Section" DataField="Section_Name" />
                                                            <asp:TemplateField HeaderText="Scheme">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblShortNameCode" runat="server" Text='<%# Eval("ShortNameCode") %>' ToolTip='<%# Eval("Project_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Sub Scheme" DataField="SubScheme" />
                                                            <asp:BoundField HeaderText="Project Types" DataField="ProjectType_Names" />
                                                            <asp:BoundField HeaderText="Expected Amount" DataField="ExpectedAmount" />
                                                            <asp:BoundField HeaderText="Proposer Type" DataField="ProposerType" />
                                                            <asp:BoundField HeaderText="Proposer" DataField="MPMLAName" />
                                                            <%--<asp:BoundField HeaderText="Proposer Name" DataField="ProposerName" />--%>
                                                            <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                                                            <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                                            <asp:TemplateField HeaderText="Rec. Letter">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypRecommendationLetter" runat="server" Target="_blank" NavigateUrl='<%# Eval("RecomendationLetter") %>' Text="" Visible='<%# !string.IsNullOrEmpty(Eval("RecomendationLetter").ToString()) %>'>
                                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ProposalDate" HeaderText="Proposal Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                             <%--<asp:BoundField HeaderText="Status" DataField="ProposalStatus" />--%>
                                                            <%--<asp:BoundField HeaderText="Added On" DataField="AddedOn" DataFormatString="{0:dd/MM/yyyy}" />--%>
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
                            <!--end col-->
                        </div>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnExportToExcel" />--%>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlProjectMaster" />
                </Triggers>
            </asp:UpdatePanel>
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
</asp:Content>


