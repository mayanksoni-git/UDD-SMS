<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" 
    AutoEventWireup="true" CodeFile="MasterPlanProposal.aspx.cs" Inherits="MasterPlanProposal" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnplanId" runat="server" />
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <style>
        .right-align {
    text-align: right;
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
                                    <h4 class="mb-sm-0">Master Plan Proposal</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Storm Water Drainage</li>
                                            <li class="breadcrumb-item active">Master Plan Proposal</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Filter :
                                            <label id="message" runat="server" style="float: right; color: red; font-weight: bold"></label>
                                        </h4>
                                        <a href="CreateMasterPlanProposal.aspx" class="filter-btn" style="float: right"><i class="icon-download"></i>Create New</a>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label fw-bold me-1"></asp:Label>
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
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="lblULBType" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="ddlULBType_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Nigam" Value="NN"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Panchayat" Value="NP"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Palika Parishad" Value="NPP"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-12 col-md-12 text-center">
                                                    
                                                        <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    
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
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlZone" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Master Plan Proposal Report</h4>
                            </div>
                            <!-- end card header -->
                            <div class="card-body">
                                <div class="live-preview">
                                    <div class="row gy-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div style="overflow: auto">
                                            <asp:GridView runat="server" ID="grdPost" AllowPaging="false" CssClass="display table table-bordered" 
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found"  OnPreRender="grdPost_PreRender" 
                                                OnRowDataBound="grdPost_RowDataBound" ShowFooter="false">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>                                                               
                                                                <a href="CreateMasterPlanProposal.aspx?MasterPlanProposalID=<%# Eval("SrNo") %>" CssClass="btn btn-primary editBTN">Edit</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="State" DataField="Zone_Name" />
                                                    <asp:BoundField HeaderText="Mandal" DataField="DivName" />
                                                    <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                                                    <asp:BoundField HeaderText="Proposal Name" DataField="ProposalName" />
                                                    <asp:BoundField HeaderText="Proposal Detail" DataField="ProposalDetail" />
                                                    <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
                                                    <asp:BoundField HeaderText="ExpAmt" DataField="ExpAmt" />
                                                    <asp:BoundField HeaderText="Master Plan Proposal Code" DataField="MasterPlanProposalCode" />
                                                    <asp:TemplateField HeaderText="Master Plan Proposal Doc">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypMasterPlanProposalDoc" runat="server" Target="_blank" NavigateUrl='<%# Eval("MasterPlanProposalFilePath") %>' Text="Click To View" Visible='<%# !string.IsNullOrEmpty(Eval("MasterPlanProposalFilePath").ToString()) %>'>
                                                                <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <tr>
                                                        <td colspan="11" style="text-align: center; font-weight: bold; color: red;">No records found</td>
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
        </div>
    </div>
</asp:Content>
