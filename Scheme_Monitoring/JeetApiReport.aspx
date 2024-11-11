<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="JeetApiReport.aspx.cs" Inherits="JeetApiReport" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    <h4 class="mb-sm-0">Jeet Api Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Proposal Management</li>
                                            <li class="breadcrumb-item active">Jeet Api Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Jeet Api Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDistrict" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divFromDate" runat="server">
                                                        <asp:Label ID="lblFromDate" runat="server" Text="From Date*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtFromDate" type="Date" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divToDate" runat="server">
                                                        <asp:Label ID="lblToDate" runat="server" Text="To Date*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtToDate" type="Date" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divLGTDCode" runat="server">
                                                        <asp:Label ID="lblLgTdCode" runat="server" Text="LG TD Code" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtLgTdCode" type="number" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divComplainant" runat="server">
                                                        <asp:Label ID="lblComplainant" runat="server" Text="Complainant Like" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtComplainant" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divLetterSubject" runat="server">
                                                        <asp:Label ID="lblLetterSubject" runat="server" Text="Letter Subject Like" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtLetterSubject" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDetail" runat="server">
                                                        <asp:Label ID="lblDetail" runat="server" Text="Detail Like" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtDetail" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divMemberName" runat="server">
                                                        <asp:Label ID="lblMemberName" runat="server" Text="Member Name Like" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtMemberName" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div class="row gy-4">
                                                <div class="col-xxl-12 offset-xxl-12 col-md-12 text-center">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-danger text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
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
                                        <h4 class="card-title mb-0 flex-grow-1">Jeet Api Detail</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                    </div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Letter Ref No" DataField="LetterRefNo" />
                                                            <asp:BoundField HeaderText="Complainant" DataField="Complainant" />
                                                            <asp:BoundField HeaderText="letter Subject" DataField="letterSubject" />
                                                            <asp:BoundField HeaderText="Detail" DataField="Detail" />
                                                            <asp:BoundField HeaderText="Member Name" DataField="MemberName" />
                                                            <asp:BoundField HeaderText="Received Date" DataField="ReceivedDate"  DataFormatString="{0:dd/MM/yyyy}"  />
                                                            <asp:BoundField HeaderText="AcknowledgeDate" DataField="AcknowledgeDate"  DataFormatString="{0:dd/MM/yyyy}"  />
                                                            <asp:BoundField HeaderText="District Name" DataField="DISTRICT_NAME" />
                                                            <asp:BoundField HeaderText="LG DT Code" DataField="LG_DT_Code" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
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
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>