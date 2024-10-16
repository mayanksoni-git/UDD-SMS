<%@ Page MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true"  Language="C#" AutoEventWireup="true" CodeFile="ULBDataSubmitReport.aspx.cs" Inherits="ULBDataSubmitReport" EnableEventValidation="false" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
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
                                    <h4 class="mb-sm-0">Park Adoption Status And progress Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Construction Of Parks</li>
                                            <li class="breadcrumb-item active">ULB Submitted Data Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">ULB Submitted Data Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divMonth" runat="server">
                                                        <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                           <%-- <div class="row gy-4">
                                                <div class="col-xxl-2 offset-xxl-10 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div runat="server" visible="true" id="divData" class="tblheader"  style="overflow: auto">
                        <div class="row">
                            <div class="col-lg-12">
                                
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">ULB Submitted Data Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                    </div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <table class="display table table-bordered">
    <thead>
        <tr>
            <th>Sr. No.</th>
            <th>District</th>
            <th>Financial Year</th>
            <th>Month</th>
            <th>No. of ULB Submitted The data </th>
            <th>No. of ULB not Submitted the data</th>
          
        </tr>
    </thead>
    <tbody>
        <!-- Example static rows -->
        <tr>
            <td>1</td>
            <td>District 1</td>
            <td>2024-25</td>
            <td>January</td>
            <td>50</td>
            <td>24</td>
            
        </tr>
        <tr>
            <td>2</td>
             <td>District 2</td>
            <td>2023-24</td>
            <td>March</td>
            <td>20</td>
            <td>10</td>
          
        </tr>
        <!-- Add more rows as needed -->
    </tbody>
  <%--  <tfoot>
        <tr>
            <td colspan="3" style="text-align: center;">Total:</td>
            <td><label id="lblTotalNoOfProposal">12</label></td>
            <td><label id="lblTotalAmount"></label></td>
            <td><label id="lblApprovedProposal">35</label></td>
            <td><label id="lblHoldProposal"></label></td>
            <td><label id="lblPendingProposal"></label></td>
        </tr>
    </tfoot>--%>
</table>

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
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <%--<asp:PostBackTrigger ControlID="ddlDivision" />--%>
                    <%--<asp:PostBackTrigger ControlID="ddlProjectMaster" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
