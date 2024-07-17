<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval3.aspx.cs" Inherits="FormForApproval" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h4 class="mb-sm-0">Approve Work Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Plan Management System</li>
                                            <li class="breadcrumb-item active">Approve Work Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Detail</h4>
                                        <u><b>Work Proposal Code:<asp:Label runat="server" id="lblWrokProposalId" Text=""></asp:Label></b></u>
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
                                                    <div id="divCircle" class="d-flex" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblCircleValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" class="d-flex" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblDivisionValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 d-flex col-md-6" id="divZoneOfULB">
                                                    <asp:Label ID="lblZoneOfULB" runat="server" Text="Zone :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtZoneOfULB" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                </div>

                                                <div class="col-xxl-3 col-md-6  d-flex" id="divWard">
                                                    <asp:Label ID="lblWard" runat="server" Text="Ward :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtWard" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                </div>
                                                </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" class="d-flex" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblProjectMasterValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divWorkType" class="d-flex" runat="server">
                                                        <asp:Label ID="lblWorkType" runat="server" Text="Work Type :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblWorkTypeValue" runat="server" Text="" CssClass="form-label"></asp:Label>

                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div8" class="d-flex" runat="server">
                                                        <asp:Label ID="lblExpectedAmount" runat="server" Text="Expected Amount(In Rupees) :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="txtExpectedAmount" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 d-flex col-md-6">
                                                    <asp:Label ID="lblRole" runat="server" Text="Proposer :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="lblRoleValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                </div>

                                                <div class="col-xxl-3 col-md-6 d-flex" id="divMPMLA" style="display: block;" runat="server">
                                                    <asp:Label ID="lblMPMLA" runat="server" Text="MP/MLA Name :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="lblMPMLAValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                </div>

                                                <div class="col-xxl-3 col-md-6 d-flex" id="divOthers" visible="false" runat="server">
                                                    <asp:Label ID="lblOther" runat="server" Text="Name of Proposer :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtOthers" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                </div>

                                                <div class="col-xxl-3 col-md-6 d-flex" id="divParty" style="display: block;" runat="server">
                                                    <asp:Label ID="lblParty" runat="server" Text="Political Party :" CssClass="form-label fw-bold me-1" ></asp:Label>
                                                    <asp:Label ID="lblParyOfMPMLA" runat="server" Text="" CssClass="form-label" Enabled="false"></asp:Label>

                                                </div>
                                                <div class="col-xxl-3 col-md-6 d-flex" id="divConstituency" style="display: block;" runat="server">
                                                    <asp:Label ID="lblConstituency" runat="server" Text="Constituency :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="lblConstituencyName" runat="server" Text="" CssClass="form-label" Enabled="false"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6 d-flex">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtMobileNo" runat="server" CssClass="form-label" TextMode="Phone" MaxLength="10" Enabled="false"></asp:Label>
                                                </div>

                                                <div class="col-xxl-3 col-md-6 ">
                                                    <div id="divDesignation" class="d-flex" runat="server">
                                                        <asp:Label ID="lblDesignation" runat="server" Text="Designation :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="txtDesignation" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                    </div>
                                                </div> 
                                                <div class="col-xxl-3 col-md-6 ">
                                                    <div id="divPStatus" class="d-flex" runat="server">
                                                        <asp:Label ID="lblPStatus" runat="server" Text="Proposal Status :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="txtPStatus" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 d-flex">
                                                    <asp:Label ID="lblRecommendationLetter" runat="server" Text="Recommendation Letter :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:HyperLink ID="hypRecommendationLetterEdit" runat="server" Target="_blank" Text="Click To View" Visible="false">
                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                    </asp:HyperLink>
                                                    <asp:HiddenField id="hfPDFUrl" runat="server"/>
                                                </div>

                                                <div class="col-xxl-2 offset-xxl-1 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
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
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Action On Work Proposal</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="lblStatus" runat="server" Text="Proposal Status" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Hold/Archive" Value="3"></asp:ListItem>
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
                                                        <asp:TextBox ID="txtDateOfAction" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtDateOfAction" Format="dd/MM/yyyy"></cc1:CalendarExtender>
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
                    </div>
                    <div class="row">
                        <div class="col-xxl-12 col-md-12">
                            <div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" visible="true" id="div1">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project details from previous years of the ULB.</h4>
                                        
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="d-flex">
                                                <asp:Button ID="btnFYWise" Text="Financial Year Wise" OnClick="btnFYWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnMPWise" Text="MP Wise" OnClick="btnMPWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnMLAWise" Text="MLA Wise" OnClick="btnMLAWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnRegionWise" Text="Division Wise" OnClick="btnDivisionWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnProjectWise" OnClick="btnWorkPlanWise_Click" Text="Work Plan Wise" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                               </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div runat="server" id="divFYWise" visible="false" style="overflow: auto">
                                                    <h3>Financial Year Wise Data</h3>
                                                    <asp:GridView ID="gridFyWise" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingFyWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ULB Name" DataField="ULBName" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="SessionYear" />
                                                            <asp:BoundField HeaderText="Total Sactioned Amount" DataField="TotalSactionedAmount" />
                                                            <asp:BoundField HeaderText="No Of Projects" DataField="NoOfProjects" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divMPWise" visible="false" style="overflow: auto">
                                                    <h3>MP Wise Data</h3>
                                                    <asp:GridView ID="gridMPWise" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingMPWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="MP Name" DataField="MPName" />
                                                            <asp:BoundField HeaderText="No of ULB" DataField="ulb_count" />
                                                            <asp:BoundField HeaderText="No of Years" DataField="session_count" />
                                                            <asp:BoundField HeaderText="No Of Projects" DataField="projectCount" />
                                                            <asp:BoundField HeaderText="Total Sactioned Amount" DataField="total_amount" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divMLAWise" visible="false" style="overflow: auto">
                                                     
                                                    <h3>MLA Wise Data</h3>
                                                    <asp:GridView ID="gridMLAWise" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingMLAWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="MLA Name" DataField="MLAName" />
                                                            <asp:BoundField HeaderText="No of ULB" DataField="ulb_count" />
                                                            <asp:BoundField HeaderText="No of Years" DataField="session_count" />
                                                            <asp:BoundField HeaderText="No Of Projects" DataField="projectCount" />
                                                            <asp:BoundField HeaderText="Total Sactioned Amount" DataField="total_amount" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divDivisionWise" visible="false" style="overflow: auto">
                                                    <h3>Division Wise Data</h3>
                                                    <asp:GridView ID="gridDivisionWise" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingDivisionWise" PageSize="18">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Division Name" DataField="DivName" />
                                                            <asp:BoundField HeaderText="No of ULB" DataField="ulb_count" />
                                                            <asp:BoundField HeaderText="No of Years" DataField="session_count" />
                                                            <asp:BoundField HeaderText="No Of Projects" DataField="projectCount" />
                                                            <asp:BoundField HeaderText="Total Sactioned Amount" DataField="total_amount" />
                                                            <asp:BoundField HeaderText="Percent b/w Division" DataField="percentage_of_total_amount" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divWorkPlanWise" visible="false" style="overflow: auto">
                                                    <h3>Work Plan Data</h3>
                                                    <asp:GridView ID="gridWorkPlanWise" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingWorkPlanWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                                                            <asp:BoundField HeaderText="No Of Proposals" DataField="NoOfProposals" />
                                                            <asp:BoundField HeaderText="All Proposals Amount" DataField="TotalAmount" />
                                                            <asp:BoundField HeaderText="PendingProposals" DataField="PendingProposals" />
                                                            <asp:BoundField HeaderText="Pending Proposals Amount" DataField="PendingProposalsAmount" />
                                                            <asp:BoundField HeaderText="Approved Proposals" DataField="ApprovedProposals" />
                                                            <asp:BoundField HeaderText="Approved Proposals Amount" DataField="ApprovedProposalsAmount" />
                                                            <asp:BoundField HeaderText="Reject Proposals" DataField="RejectProposals" />
                                                            <asp:BoundField HeaderText="Reject Proposals Amount" DataField="RejectProposalsAmount" />
                                                            <asp:BoundField HeaderText="Hold Proposals" DataField="HoldProposals" />
                                                            <asp:BoundField HeaderText="Hold Proposals Amount" DataField="HoldProposalsAmount" />
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
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAction" />
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
        .tab_btn {
    margin: 2px;
}
    </style>
</asp:Content>
