<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" 
    AutoEventWireup="true" CodeFile="CreateMasterPlanProposal.aspx.cs" Inherits="CreateMasterPlanProposal" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Storm Water Drainage</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Storm Water Drainage</li>
                                            <li class="breadcrumb-item active">Create Master Plan Proposal</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1" id="head2" runat="server">Create Master Plan Proposal</h4>
                                        <a href="MasterPlanProposal.aspx" class="filter-btn" style="float: right"><i class="icon-download"></i>Go To List</a>
                                    </div>
                                    <!-- end card header -->
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
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
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


                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divProposalName" runat="server">
                                                        <asp:Label ID="lblProposalName" runat="server" Text="Proposal Name" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtProposalName" runat="server" CssClass="form-control" ></asp:TextBox>
                                                    </div>
                                                </div> 

                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divProposalDetail" runat="server">
                                                        <asp:Label ID="lblProposalDetail" runat="server" Text="Proposal Detail" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtProposalDetail" runat="server" CssClass="form-control" ></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divExpAmt" runat="server">
                                                        <asp:Label ID="lblExpAmt" runat="server" Text="ExpAmt(In Lakhs)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtExpAmt" runat="server" placeholder="1000.5" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                
                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divMobileNo" runat="server">
                                                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblMasterPlanProposalDoc" runat="server" Text="Upload Master Plan Doc" CssClass="form-label"></asp:Label>
                                                    <asp:FileUpload ID="fileUploadMasterPlanProposalDoc" runat="server" CssClass="form-control" />
                                                    
                                                    <asp:RegularExpressionValidator ID="revFileUpload" runat="server"
                                                        ControlToValidate="fileUploadMasterPlanProposalDoc"
                                                        ErrorMessage="Only PDF files are allowed."
                                                        CssClass="text-danger"
                                                        ValidationExpression="^.*\.(pdf)$">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:CustomValidator ID="cvFileSize" runat="server"
                                                        ControlToValidate="fileUploadMasterPlanProposalDoc"
                                                        ErrorMessage="File size cannot exceed 5MB."
                                                        CssClass="text-danger"
                                                        OnServerValidate="cvFileSize_ServerValidate">
                                                    </asp:CustomValidator>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:HyperLink ID="hypMasterPlanProposalDocEdit" runat="server" Target="_blank" Text="Click To View Master Plan Doc" Visible="false">
                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                    </asp:HyperLink>
                                                    <asp:HiddenField ID="hfPDFUrl" runat="server" />
                                                </div>

                                                 <div class="col-xxl-12 col-md-6 text-center">
                                                    <div>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-warning text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-danger text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfMasterPlanProposalId" runat="server" />
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
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
