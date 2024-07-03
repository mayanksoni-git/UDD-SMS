<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval2.aspx.cs" Inherits="FormForApproval2" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    <h4 class="mb-sm-0">Work Proposals</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Plan Management System</li>
                                            <li class="breadcrumb-item active">Work Proposals</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Search Proposal</h4>
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

                                                <div class="col-xxl-2 col-md-6" id="divParty">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Proposal Status*" CssClass="form-label"></asp:Label>
                                                    <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-select">
                                                        <asp:ListItem Text="--Select Status--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Hold/Archive" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 offset-xxl-1 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Button ID="btnShowModal" runat="server" Visible="false" CssClass="btn bg-success text-white" Text="Show" OnClick="btnShowModal_Click" />
                                                        <asp:Button ID="btnHideModal" runat="server" Visible="false" CssClass="btn bg-black text-white" Text="Hide" OnClick="btnHideModal_Click" />
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfFormApproval_Id" runat="server" />
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



                    <div runat="server" visible="false" id="divFundStatus">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Fund Status of ULB</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="Div1" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <div id="fundStatusModal" runat="server" class="modal-content">
                                                        <span id="btnClose" runat="server" class="close" onserverclick="btnClose_Click"></span>
                                                        <table class="table">
                                                            <thead>
                                                                <tr>
                                                                    <th>Financial Year</th>
                                                                    <th>
                                                                        <asp:DropDownList ID="ddlFY1" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                    </th>
                                                                    <th>
                                                                        <asp:DropDownList ID="ddlFY2" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>Fund Received</td>
                                                                    <td>Yes/No</td>
                                                                    <td>Yes/No</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Utilization Status</td>
                                                                    <td>Utilized/Unutilized</td>
                                                                    <td>Utilized/Unutilized</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>UC Status</td>
                                                                    <td>Received/Not Received</td>
                                                                    <td>Received/Not Received</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
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

                    <%--place grid here--%>

                    <div runat="server" visible="true" id="divData">
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
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">

                                                    <asp:GridView ID="gvRecords" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="false" OnRowCommand="gvData_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Sr. No." DataField="SrNo" />
                                                            <asp:BoundField HeaderText="District" DataField="District" />
                                                            <asp:BoundField HeaderText="ULB" DataField="ULB" />
                                                            <asp:BoundField HeaderText="Scheme Name" DataField="SchemeName" />
                                                            <asp:BoundField HeaderText="Type of Work" DataField="WorkType" />
                                                            <asp:BoundField HeaderText="Expected Amount" DataField="ExpectedAmount" />
                                                            <asp:BoundField HeaderText="Proposer" DataField="Proposer" />
                                                            <asp:BoundField HeaderText="Political Party" DataField="PoliticalParty" />
                                                            <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
                                                            <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                                            <asp:TemplateField HeaderText="Recommendation Letter">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download" CommandArgument='<%# Eval("RecommendationLetter") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction" runat="server" Text="Take Action" CommandName="Action" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Status" DataField="Status" />
                                                        </Columns>
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

                    
                    <!-- ModalPopup for Action on Proposal -->

                    <asp:Panel ID="pnlActionProposal" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Action on Proposal</h5>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Hold/Archive" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSubmitAction" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitAction_Click" />
                                    <asp:Button ID="btnCloseActionProposal" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnCloseActionProposal_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="mpActionProposal" runat="server" PopupControlID="pnlActionProposal" TargetControlID="btnDummyActionProposal"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnDummyActionProposal" runat="server" Style="display: none;" />
                   
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
