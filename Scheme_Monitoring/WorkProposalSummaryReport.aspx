<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="WorkProposalSummaryReport.aspx.cs" Inherits="WorkProposalSummaryReport" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    <h4 class="mb-sm-0">Work Proposal Summary Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Proposal Management</li>
                                            <li class="breadcrumb-item active">Work Proposal Summary Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State" CssClass="form-label"></asp:Label>
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
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
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
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB" CssClass="form-label"></asp:Label>
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
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblIsAkanshiULB" CssClass="control-label no-padding-right" Text="Akanshi ULB" runat="server"></asp:Label>
                                                        <asp:CheckBox ID="chkIsAkanshiULB" runat="server" CssClass="form-check mb-2" />
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-2 col-md-6">
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
                    
                    <div runat="server" visible="false" id="divData" class="tblheader"  style="overflow: auto">
                        <div class="row">
                            <div class="col-lg-12">
                                
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Detail</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                    </div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" ShowFooter="true" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                        <Columns>
                                                           <%-- <asp:BoundField DataField="WorkProposalId" HeaderText="Work Proposal Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>--%>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalText" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Section" DataField="Section_Name" />
                                                            <asp:BoundField HeaderText="Project Name" DataField="Project_Name" />
                                                            <asp:BoundField HeaderText="FinancialYear Comments" DataField="FinancialYear_Comments" />
                                                            <asp:TemplateField HeaderText="No Of Proposals">
                                                                <ItemTemplate>
                                                                    <span ID="Span2" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("NoOfProposals") %></span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalNoOfProposal" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="No Of Proposals" DataField="NoOfProposals" />--%>
                                                            <%--<asp:BoundField HeaderText="Total Amount (In Lacks)" DataField="TotalAmount" />--%>
                                                            <asp:TemplateField HeaderText="Total Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                    <span ID="Span1" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("TotalAmount") %></span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="Approved Proposals" DataField="ApprovedProposals" />--%>
                                                            <asp:TemplateField HeaderText="Approved Proposals">
                                                                <ItemTemplate>
                                                                    <span ID="Span3" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("ApprovedProposals") %></span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblApprovedProposal" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="Hold Proposals" DataField="HoldProposals" />--%>
                                                            <asp:TemplateField HeaderText="Hold Proposals">
                                                                <ItemTemplate>
                                                                    <span ID="Span4" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("HoldProposals") %></span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblHoldProposal" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="Pending Proposals" DataField="PendingProposals" />--%>
                                                            <asp:TemplateField HeaderText="Pending Proposals">
                                                                <ItemTemplate>
                                                                    <span ID="Span4" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("PendingProposals") %></span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblPendingProposal" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="Project_Name" HeaderText="Scheme">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>--%>
                                                            <%--<asp:BoundField HeaderText="Scheme Short Name" DataField="Project_Name"/>--%>
                                                           <%-- <asp:BoundField HeaderText="Scheme" DataField="ShortNameCode" />
                                                            <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                                            <asp:BoundField HeaderText="Expected Amount()" DataField="ExpectedAmount" />
                                                            <asp:TemplateField HeaderText="Rec. Letter">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypRecommendationLetter" runat="server" Target="_blank" NavigateUrl='<%# Eval("RecomendationLetter") %>' Text="" Visible='<%# !string.IsNullOrEmpty(Eval("RecomendationLetter").ToString()) %>'>
                                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
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
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlProjectMaster" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>