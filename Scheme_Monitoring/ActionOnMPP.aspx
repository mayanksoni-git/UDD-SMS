<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="ActionOnMPP.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="ActionOnMPP" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HF_MasterPlanProposalID" runat="server" />

    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Action on Master Plan Proposal</h4>
                                    <%--<asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Create New" CssClass="btn btn-warning"></asp:Button>--%>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Storm Water Drainage</li>
                                            <li class="breadcrumb-item active">Action on Master Plan Proposal</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Master Plan Proposal Detail</h4>
                                        <u><b>Master Plan Proposal Code:<asp:Label runat="server" ID="lblMPProposalId" Text=""></asp:Label></b></u>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" class="d-flex" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblFYValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" class="d-flex" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblZoneValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div> 
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divMandal" class="d-flex" runat="server">
                                                        <asp:Label ID="lblMandalH" runat="server" Text="Division :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblMandalValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" class="d-flex" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblCircleValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" class="d-flex" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblDivisionValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 d-flex col-md-6" id="divMPPCode">
                                                    <asp:Label ID="lblMPPCode" runat="server" Text="Master Plan Proposal Code :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtMPPCode" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProposalName" class="d-flex" runat="server">
                                                        <asp:Label ID="lblProposalName" runat="server" Text="Proposal Name :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblProposalNameValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-6 col-md-6">
                                                    <div id="divProposalDetail" class="d-flex" runat="server">
                                                        <asp:Label ID="lblProposalDetail" runat="server" Text="Proposal Detail :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblProposalDetailValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div8" class="d-flex" runat="server">
                                                        <asp:Label ID="lblExpectedAmount" runat="server" Text="Expected Amount(In Lakhs) :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="txtExpectedAmount" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6 d-flex">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtMobileNo" runat="server" CssClass="form-label" TextMode="Phone" MaxLength="10" Enabled="false"></asp:Label>
                                                </div>

                                               
                                                <div class="col-xxl-3 col-md-6 ">
                                                    <div id="divPStatus" class="d-flex" runat="server">
                                                        <asp:Label ID="lblPStatus" runat="server" Text="Master Plan Proposal Status :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="txtPStatus" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 d-flex">
                                                    <asp:Label ID="lblRecommendationLetter" runat="server" Text="Master Plan Proposal Doc:" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:HyperLink ID="hypRecommendationLetterEdit" runat="server" Target="_blank" Text="Click To View" Visible="false">
                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                    </asp:HyperLink>
                                                    <asp:HiddenField ID="hfPDFUrl" runat="server" />
                                                </div>

                                                <div class="col-xxl-2 offset-xxl-1 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
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
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Action On Master Plan Proposal</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="lblStatus" runat="server" Text="Master Plan Proposal Status" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="--Select Status--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Hold/Archive" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Revert" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div3" runat="server">
                                                        <asp:Label ID="lblRemark" runat="server" Text="Remarks" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div4" runat="server">
                                                        <asp:Label ID="lblDateOfAction" runat="server" Text="Action Date" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtDateOfAction" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                                        <%--<asp:TextBox ID="txtDateOfAction" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>--%>
                                                        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtDateOfAction" Format="dd/MM/yyyy"></cc1:CalendarExtender>--%>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-2 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnAction" Text="Update" OnClick="btnSubmitAction_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <!--end row-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
 
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAction" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
