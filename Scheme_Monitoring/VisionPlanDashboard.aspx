<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VisionPlanDashboard.aspx.cs" Inherits="VisionPlanDashboard" EnableEventValidation="false" ValidateRequest="false" %>

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
                                                   
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div runat="server" id="divDashboard" class="tblheader" visible="true" style="overflow: auto">
                                                    <div class="row">
                                                        <div class="col-lg-10">
                                                            <h3>
                                                                Reports</h3>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <%--<asp:Button ID="Button7" runat="server" Text="Export to Excel Of Financial Year Wise" CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" />--%>
                                                        </div>
                                                    </div>
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
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
