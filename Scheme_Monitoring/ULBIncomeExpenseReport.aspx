<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="ULBIncomeExpenseReport.aspx.cs" Inherits="ULBIncomeExpenseReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="ULBFundId" runat="server" />

    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
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
                                    <h4 class="mb-sm-0">ULB Income And Expenses Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                              <li class="breadcrumb-item">Annual Action Plan</li>

                                            <li class="breadcrumb-item active">ULB Income And Expenses Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                          <div class="row" id="SectionFilter" runat="server">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Filter By : </h4>
                                        
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
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
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        <%--<asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="NN">Nagar Nigam</asp:ListItem>
                                                            <asp:ListItem Value="NPP">Nagar Palika Parishad</asp:ListItem>
                                                            <asp:ListItem Value="NP">Nagar Panchayat</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-1  col-md-3 text-center">
                                                    <div><label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>

                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
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
                    </ContentTemplate>
                 <Triggers>
                   
                   
                   
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlZone" />
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                   
                </Triggers>
                </asp:UpdatePanel>
            <div class="container-fluid">
               
            </div>

            </div>
        </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
 <style>
     #sample-table-2 th{
         text-align:center;
     }
 </style>
      <style>
        @media print {
            body, table {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
                width: 100%;
                height: 100%;
            }
            table {
                page-break-inside: auto;
            }
            tr {
                page-break-inside: avoid;
                page-break-after: auto;
            }
            @page {
                size: A4 landscape;
                margin: 10mm;
            }
        }

        .print-container {
            width: 100%;
            margin: auto;
            text-align: center;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-view th, .grid-view td {
            border: 1px solid #000;
            padding: 8px;
            text-align: left;
        }

        .btn-print {
            margin-top: 20px;
        }
    </style>
    <script>
        $(document).ready(function () {
            //$(" #sample-table-2").DataTable();
        })
    </script>
    </asp:Content>