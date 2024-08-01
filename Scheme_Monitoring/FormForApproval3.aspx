<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval3.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="FormForApproval" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="WorkProposalIds" runat="server" />
    <asp:HiddenField ID="hfWorkProposalId" runat="server" />
    
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                   <cc1:modalpopupextender id="mp1" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShowPopup"
                        cancelcontrolid="btnclose" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Approve Work Plan</h4> 
                                    <%--<asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Create New" CssClass="btn btn-warning"></asp:Button>--%>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Work Plan Detail</h4>
                                        <u><b> Proposal Code:<asp:Label runat="server" id="lblWrokProposalId" Text=""></asp:Label></b></u>
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
                                                
                                                <div class="col-xxl-3 col-md-6" runat="server" id="divSubScheme" visible="false">
                                                    <div class="d-flex">
                                                        <asp:Label ID="lblSubScheme" runat="server" Text="SubScheme :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblSubSchemeValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-9 col-md-6">
                                                    <div id="divWorkType" class="d-flex" runat="server">
                                                        <asp:Label ID="lblWorkType" runat="server" Text="Work Type :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="lblWorkTypeValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                    </div>
                                                </div>


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
                                                        <asp:Label ID="lblExpectedAmount" runat="server" Text="Expected Amount(In Rupees) :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:Label ID="txtExpectedAmount" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 d-flex col-md-6">
                                                    <asp:Label ID="lblRole" runat="server" Text="Proposer Type :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="lblRoleValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                </div>

                                                <div class="col-xxl-3 col-md-6 d-flex" id="divMPMLA" style="display: block;" runat="server">
                                                    <asp:Label ID="lblMPMLA" runat="server" Text="Proposer :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="lblMPMLAValue" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                </div>

                                                <%--<div class="col-xxl-3 col-md-6 d-flex" id="divOthers" visible="false" runat="server">
                                                    <asp:Label ID="lblOther" runat="server" Text="Name of Proposer :" CssClass="form-label fw-bold me-1"></asp:Label>
                                                    <asp:Label ID="txtOthers" runat="server" CssClass="form-label" Enabled="false"></asp:Label>
                                                </div>--%>

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
                                                        <asp:Label ID="lblPStatus" runat="server" Text="Work Plan Status :" CssClass="form-label fw-bold me-1"></asp:Label>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Action On Work Plan</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="lblStatus" runat="server" Text="Work Plan Status" CssClass="form-label fw-bold me-1"></asp:Label>
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
                   
                    <div runat="server" visible="true" id="div1">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project details from previous years of the ULB.</h4>
                                        <%--<a href="#" id="exportToExcel" runat="server" onclick="ExportToExcel('xlsx')" class="filter-btn" style="float:right"><i class="icon-download"></i> Export To Excel</a>--%>

                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body" >
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="d-flex"style="margin-top:20px;margin-bottom:20px">
                                                <asp:Button ID="btnFYWise" Text="Financial Year Wise" OnClick="btnFYWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnMPWise" Text="MP Wise" OnClick="btnMPWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnMLAWise" Text="MLA Wise" OnClick="btnMLAWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="BtnDistrictWise" Text="District Wise" OnClick="BtnDistrictWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnRegionWise" Text="Division Wise" OnClick="btnDivisionWise_Click" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                                <asp:Button ID="btnProjectWise" OnClick="btnWorkPlanWise_Click" Text="Work Plan Wise" runat="server" CssClass="btn tab_btn bg-success text-white" ></asp:Button>
                                               </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div runat="server" id="divFYWise" class="tblheader" visible="false" style="overflow: auto">
                                                   
                                       <div class="row">
                                                    <div class="col-lg-10">  
                                                    <h3>Financial Year Wise Data</h3>
                                                        </div>
                                                     <div class="col-lg-2"> <asp:Button ID="Button5" runat="server" Text="Export to Excel Of Financial Year Wise"  CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" /></div>
                                                     </div>
                                                    <asp:GridView ID="gridFyWise" runat="server" CssClass="display table table-bordered reportGrid" ShowFooter="true" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingFyWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ULB Name" DataField="ULBName" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="SessionYear" />
                                                             <asp:TemplateField HeaderText="Financial Year">
                                                                <ItemTemplate>
                                                                    
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("SessionYear") %></span>
                                                                    <%--<asp:BoundField HeaderText="Total Sactioned Amount (In  Lacs)" DataField="TotalSactionedAmount" />--%>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldAmountOfFY" Text="Total Sanctioned Amount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Total Sactioned Amount (In  Lacs)">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("TotalSactionedAmount") %></span>
                                                                    <%--<asp:BoundField HeaderText="Total Sactioned Amount (In  Lacs)" DataField="TotalSactionedAmount" />--%>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmountOfFY" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="No Of Projects" DataField="NoOfProjects" />--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divMPWise" class="tblheader" visible="false" style="overflow: auto">
                                                 <div class="row">
                                                    <div class="col-lg-10">  <h3>MP Wise Data</h3></div>
                                        <div class="col-lg-2"> <asp:Button ID="Button1" runat="server" Text="Export to Excel Of MP"  CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" /></div>
                                                     </div>
                                                    <asp:GridView ID="gridMPWise" runat="server" CssClass="display table table-bordered reportGrid" ShowFooter="true" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                        OnPageIndexChanging="OnPageIndexChangingMPWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="MP Name" DataField="MPName" />
                                                            <asp:BoundField HeaderText=" Constituency " DataField="ParliamentaryConstName" />
                                                            <asp:BoundField HeaderText="Party Name" DataField="PartyName" />
                                                            
                                                          
                                                              
                                                           <asp:TemplateField HeaderText="No of ULB">
                                                                   <ItemTemplate>
                                                                          <asp:Label ID="lblULBCount" Text='<%# Eval("ulb_count") %>' runat="server" CssClass="hidden-column" />
                                                                    </ItemTemplate>
                                                               <ItemTemplate>
                                                                    <%--<asp:HiddenField ID="HiddenField1" Value='<%# Eval("ulb_count") %>' runat="server" />--%>
                                                              
                                                                    <asp:Button ID="btnAction" runat="server" Text='<%# Eval("ulb_count") %>' CommandName="Action2" OnCommand="GetULBWiseData"  CommandArgument='<%# Eval("ParliamentaryConstID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="No of Financial year">
                                                                  <ItemTemplate>
                                                                  
                                                                    <asp:Button ID="btnAction2" runat="server" Text='<%# Eval("session_count") %>' CommandName="Action2" OnCommand="GetYearWiseData"  CommandArgument='<%# Eval("ParliamentaryConstID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalAmountOfMp" Text="Total Sanctioned Amount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Sactioned Amount(In Lacs)">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction6" runat="server" Text='<%# Eval("total_amount") %>' CommandName="Action2" OnCommand="GetAmountWiseData"  CommandArgument='<%# Eval("ParliamentaryConstID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmountOfMp" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divMLAWise" class="tblheader" visible="false" style="overflow: auto">
                                                    <div class="row">
                                                    <div class="col-lg-10">  
                                                    <h3>MLA Wise Data</h3>
                                                        </div>
                                                     <div class="col-lg-2"> <asp:Button ID="Button2" runat="server" Text="Export to Excel Of MLA"  CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" /></div>
                                                     </div>
                                                    <asp:GridView ID="gridMLAWise" runat="server" CssClass="display table table-bordered reportGrid" AutoGenerateColumns="False" ShowFooter="true" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingMLAWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="MLA Name" DataField="MLAName" />
                                                             <asp:BoundField HeaderText=" Constituency  " DataField="AssemblyConstName" />
                                                            <asp:BoundField HeaderText="Party Name" DataField="PartyName" />
                                                          

                                                             <asp:TemplateField HeaderText="No of ULB">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction3" runat="server" Text='<%# Eval("ulb_count") %>' CommandName="Action" OnCommand="GetULBWiseData"  CommandArgument='<%# Eval("AssemblyConstID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="No of Financial year">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction4" runat="server" Text='<%# Eval("session_count") %>' CommandName="Action" OnCommand="GetYearWiseData"  CommandArgument='<%# Eval("AssemblyConstID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                                  <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmounttextOfMLA" Text="Total Sanctioned Amount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                          
                                                            <asp:TemplateField HeaderText="Total Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction5" runat="server" Text='<%# Eval("total_amount") %>' CommandName="Action" OnCommand="GetAmountWiseData"  CommandArgument='<%# Eval("AssemblyConstID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                           
                                                               <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmountOfMLA" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                             </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divDivisionWise" class="tblheader" visible="false" style="overflow: auto">
                                                   
                                                      <div class="row">
                                                    <div class="col-lg-10">  
                                                    <h3>Division Wise Data</h3>
                                                        </div>
                                                     <div class="col-lg-2"> <asp:Button ID="Button3" runat="server" Text="Export to Excel Of Division Wise"  CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" /></div>
                                                     </div>

                                                    <asp:GridView ID="gridDivisionWise" runat="server" ShowFooter="true" CssClass="display table table-bordered reportGrid" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingDivisionWise" PageSize="18">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Division Name" DataField="DivName" />
                                                          
                                                             <asp:TemplateField HeaderText="No of ULB">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction3" runat="server" Text='<%# Eval("ulb_count") %>' CommandName="Division" OnCommand="GetULBWiseData"  CommandArgument='<%# Eval("DivisionID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                           
                                                           <asp:TemplateField HeaderText="No of Financial year">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction7" runat="server" Text='<%# Eval("session_count") %>' CommandName="Division" OnCommand="GetYearWiseData"  CommandArgument='<%# Eval("DivisionID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmounttextOfDvision" Text="Total Sanctioned Amount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Total Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction6" runat="server" Text='<%# Eval("total_amount") %>' CommandName="Division" OnCommand="GetAmountWiseData"  CommandArgument='<%# Eval("DivisionID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>

                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmountOfDivision" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Percent b/w Division" DataField="percentage_of_total_amount" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>

                                                 <div runat="server" id="divDistrictWise" class="tblheader" visible="false" style="overflow: auto">
                                                   
                                                      <div class="row">
                                                    <div class="col-lg-10">  
                                                    <h3>District Wise Data</h3>
                                                        </div>
                                                     <div class="col-lg-2"> <asp:Button ID="Button6" runat="server" Text="Export to Excel Of District Wise"  CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" /></div>
                                                     </div>

                                                    <asp:GridView ID="GrdDistrictWise" runat="server" ShowFooter="true" CssClass="display table table-bordered reportGrid" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true" OnPageIndexChanging="GrdDistrictWise_PageIndexChanging" PageSize="20">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="District Name" DataField="DistName" />
                                                          
                                                             <asp:TemplateField HeaderText="No of ULB">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction3" runat="server" Text='<%# Eval("ulb_count") %>' CommandName="Action3" OnCommand="GetULBWiseData"  CommandArgument='<%# Eval("DistID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                           
                                                           <asp:TemplateField HeaderText="No of Financial year">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction7" runat="server" Text='<%# Eval("session_count") %>' CommandName="Action3" OnCommand="GetYearWiseData"  CommandArgument='<%# Eval("DistID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmounttextOfDistrict" Text="Total Sanctioned Amount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Total Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAction6" runat="server" Text='<%# Eval("total_amount") %>' CommandName="Action3" OnCommand="GetAmountWiseData"  CommandArgument='<%# Eval("DistID") %>'  CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>

                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalSactionedAmountOfDistrict" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Percent b/w District" DataField="percentage_of_total_amount" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>

                                                <div runat="server" id="divWorkPlanWise" class="tblheader" visible="false" style="overflow: auto">
                                                   
                                                    <div class="row">
                                                    <div class="col-lg-10">  
                                                     <h3>Work Plan Data</h3>
                                                        </div>
                                                     <div class="col-lg-2"> <asp:Button ID="Button4" runat="server" Text="Export to Excel Of Work Plan"  CommandName="Financial Year Wise Data" OnClick="btnExportToExcel_Click" CssClass="btn btn-success" /></div>
                                                     </div>
                                                    <asp:GridView ID="gridWorkPlanWise" runat="server" ShowFooter="true" CssClass="display table table-bordered reportGrid" AutoGenerateColumns="False" EmptyDataText="No Records Found" AllowPaging="true"
                                                    OnPageIndexChanging="OnPageIndexChangingWorkPlanWise" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          <%--  <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />--%>
                                                             <asp:TemplateField HeaderText="Financial Year">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("FinancialYear_Comments") %></span>
                                                                    <%--<asp:BoundField HeaderText="Total Sactioned Amount (In  Lacs)" DataField="TotalSactionedAmount" />--%>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label  Text="Total :" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>

                                                            <%--<asp:BoundField HeaderText="No Of Proposals" DataField="NoOfProposals" />--%>
                                                             <asp:TemplateField HeaderText="No Of Proposals">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("NoOfProposals") %></span>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposals"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>


                                                            <%--<asp:BoundField HeaderText="All Proposals Amount(In Rupees)" DataField="TotalAmount" />--%>

                                                             <asp:TemplateField HeaderText="All Proposals Amount(In Rupees)">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("TotalAmount") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsAmount"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>


                                                            <%--<asp:BoundField HeaderText="PendingProposals" DataField="PendingProposals" />--%>

                                                             <asp:TemplateField HeaderText="PendingProposals">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("PendingProposals") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsPending"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>



                                                            <%--<asp:BoundField HeaderText="Pending Proposals Amount(In Rupees)" DataField="PendingProposalsAmount" />--%>

                                                              <asp:TemplateField HeaderText="Pending Proposals Amount(In Rupees)">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("PendingProposalsAmount") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsPendingAmount"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>



                                                            <%--<asp:BoundField HeaderText="Approved Proposals" DataField="ApprovedProposals" />--%>


                                                               <asp:TemplateField HeaderText="Approved Proposals">
                                                                <ItemTemplate>
                                                                    <span ID="btnAction5" runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("ApprovedProposals") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsApproved"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>

                                                            <%--<asp:BoundField HeaderText="Approved Proposals Amount(In Rupees)" DataField="ApprovedProposalsAmount" />--%>
                                                             <asp:TemplateField HeaderText="Approved Proposals Amount(In Rupees)">
                                                                <ItemTemplate>
                                                                    <span  runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("ApprovedProposalsAmount") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsApprovedAmount"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>

                                                            <%--<asp:BoundField HeaderText="Reject Proposals" DataField="RejectProposals" />--%>
                                                             <asp:TemplateField HeaderText="Reject Proposals">
                                                                <ItemTemplate>
                                                                    <span  runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("RejectProposals") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsRejected"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>

                                                            <%--<asp:BoundField HeaderText="Reject Proposals Amount(In Rupees)" DataField="RejectProposalsAmount" />--%>
                                                             <asp:TemplateField HeaderText="Reject Proposals Amount(In Rupees)">
                                                                <ItemTemplate>
                                                                    <span  runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("RejectProposalsAmount") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsRejectedAmount"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>

                                                            <%--<asp:BoundField HeaderText="Hold Proposals" DataField="HoldProposals" />--%>

                                                            <asp:TemplateField HeaderText="Hold Proposals">
                                                                <ItemTemplate>
                                                                    <span  runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("HoldProposals") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsHold"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

                                                              </asp:TemplateField>

                                                            <%--<asp:BoundField HeaderText="Hold Proposals Amount (In Rupees)" DataField="HoldProposalsAmount" />--%>
                                                            <asp:TemplateField HeaderText="Hold Proposals Amount(In Rupees)">
                                                                <ItemTemplate>
                                                                    <span  runat="server"  CssClass="btn btn-primary drill_btn" ><%# Eval("HoldProposalsAmount") %></span>
                                                                   
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotaldProposalsHoldAmount"  runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>

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
                            <!--end col-->



                            

                            
                         

                        </div>
                    </div>
                    </div>

                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1 p-0" Style="display: none; width: 800px; margin-left: -32px;height:800px;overflow:auto">
                            <div class="row  bg-warning" style="border-radius: 11px; padding:10px;">
                                <div class="col-xs-12">
                                    <div class="table-header fw-bold fs-4">
                                        Fund Detail                                      
                                    </div>
                                </div>
                            </div>

                        <div class="row" style="padding:10px">
                            <div class="col-xl-12">
                                <h5 id="MPMLAName" style="text-align:center" runat="server"></h5>
                            </div>
                             <div class="col-xl-12">
                                <h5 id="SchemeNamesof" style="text-align:center" runat="server"></h5>
                            </div>
                        </div>
                        <div class="row p-3">
                            <div class="col-md-12" id="dvPJ" runat="server">
                                <div class="form-group">
                                    <asp:GridView ID="gridULBCount" runat="server" ShowFooter="true" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="District Name" DataField="DistName" />
                                             <asp:TemplateField HeaderText="ULB Name">
                                                                <ItemTemplate>
                                                                   <span><%# Eval("ULBName") %></span>
                                                                </ItemTemplate>
                                                                  <FooterTemplate>
                                                                    <asp:Label  Text="Total :" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                            <%--<asp:BoundField HeaderText="ULB Name" DataField="ULBName" />--%>

                                             <asp:TemplateField HeaderText=" Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                   <span><%# Eval("amount") %></span>
                                                                </ItemTemplate>
                                                                

                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalUlbsAmount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                             </asp:TemplateField>
                                           <%-- <asp:BoundField HeaderText=""  DataField="amount" />--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                     <asp:GridView ID="GridYearViseSingle" ShowFooter="true" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="No of Financial year" DataField="SessionYear" />--%>
                                           
                                             <asp:TemplateField HeaderText="No of Financial year">
                                                                <ItemTemplate>
                                                                   <span><%# Eval("SessionYear") %></span>
                                                                </ItemTemplate>
                                                                  <FooterTemplate>
                                                                    <asp:Label  Text="Total :" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                             </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Total " DataField="" />--%>

                                             <asp:TemplateField HeaderText=" Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                   <span><%# Eval("amount") %></span>
                                                                </ItemTemplate>
                                                                

                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalUlbsyearAmount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                             </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:GridView>


                                    <asp:GridView ID="GridViewOfAmountWise1" runat="server" ShowFooter="true" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:BoundField HeaderText="District Name" DataField="DistName" />

                                            <asp:BoundField HeaderText="ULB NAme" DataField="ULBName" />
                                            <%--<asp:BoundField HeaderText="No of Financial year" DataField="SessionYear" />--%>
                                             <asp:TemplateField HeaderText="No of Financial year">
                                                                <ItemTemplate>
                                                                   <span><%# Eval("SessionYear") %></span>
                                                                </ItemTemplate>
                                                                  <FooterTemplate>
                                                                    <asp:Label  Text="Total :" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                             </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Total Sactioned Amount(In Lacs)" DataField="AmtInLac" />--%>
                                        <asp:TemplateField HeaderText=" Sactioned Amount (In Lacs)">
                                                                <ItemTemplate>
                                                                   <span><%# Eval("AmtInLac") %></span>
                                                                </ItemTemplate>
                                                                

                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblTotalAmount" runat="server" Style="font-weight: 700; font-weight:bold; font-size: 20px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
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
                            <div class="col-md-12">
                                <div class="form-group mt-2 text-end">
                                    <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="text-light btn bg-danger p-2" OnClick="btnclose_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAction" />
                    <%--<asp:PostBackTrigger ControlID="exportToExcel" />--%>
                    <%--<asp:PostBackTrigger ControlID="btnExportToExcel" />--%>
                    
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
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.16.2/xlsx.full.min.js"></script>

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>
     <script type="text/javascript">



         function ExportToExcel(type, fn, dl) {
             debugger
             const currentDate = new Date();

             // Get the current date
             const year = currentDate.getFullYear();
             const month = currentDate.getMonth() + 1; // Months are zero-based
             const day = currentDate.getDate();
             var h2Element = document.querySelector('.tblheader h3');
             var h2Value = h2Element ? h2Element.innerText : 'DefaultHeader';
             // Format the date as desired (e.g., YYYY-MM-DD)
             const formattedDate = "Work Plan Detail_" + `${year}-${month}-${day}`;

             var elt = document.getElementById('ctl00_ContentPlaceHolder1_gvRecords');
             var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
             return dl ?
                 XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                 XLSX.writeFile(wb, fn || (formattedDate + "." + (type || 'xlsx')));
         }


         function ExportToExcel(type, fn, dl) {
             debugger;
             const currentDate = new Date();

             // Get the current date
             const year = currentDate.getFullYear();
             const month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based
             const day = String(currentDate.getDate()).padStart(2, '0');

             // Get the text from the h2 element
             var h2Element = document.querySelector('.tblheader h3');
             var h2Value = h2Element ? h2Element.innerText : 'DefaultHeader';

             // Format the date as desired (e.g., YYYY-MM-DD)
             const formattedDate = `${h2Value}_${year}-${month}-${day}`;

             // Get the GridView table element
             var tables = document.getElementsByTagName('table');
             if (tables.length === 0) {
                 console.error('No table elements found');
                 return;
             }

             var table = tables[0];

             //// Clone the table to avoid modifying the original
             //var tableClone = table.cloneNode(true);

             //// Convert button text to plain text
             //var inputs = tableClone.querySelectorAll('input[type="submit"]');
             //inputs.forEach(input => {
             //    input.outerHTML = input.value; // Replace input with its value
             //});


             // Convert the cleaned table to a workbook
             var wb = XLSX.utils.table_to_book(table, { sheet: "sheet1" });

             // Write the workbook to file or return base64
             return dl ?
                 XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                 XLSX.writeFile(wb, fn || (formattedDate + "." + (type || 'xlsx')));
         }



         //function ExportToExcel(type, fn, dl) {
         //    debugger
         //    const currentDate = new Date();

         //    // Get the current date
         //    const year = currentDate.getFullYear();
         //    const month = currentDate.getMonth() + 1; // Months are zero-based
         //    const day = currentDate.getDate();
         //    var h2Element = document.querySelector('.tblheader h3');
         //    // Get the inner text of the h2 element
         //    var h2Value = h2Element.innerText;
         //    // Format the date as desired (e.g., YYYY-MM-DD)
         //    const formattedDate = h2Value + `${year}-${month}-${day}`;


         //    var tables = document.getElementsByTagName('table').cloneNode(true);
         //    if (tables.length === 0) {
         //        console.error('No table elements found');
         //        return;
         //    }
         //    var buttons = tables.getElementsByTagName('button');
         //    while (buttons.length > 0) {
         //        buttons[0].parentNode.removeChild(buttons[0]);
         //    }
         //    // Select the first table
         //    var table = tables[0];




         //    //var elt = document.getElementById('ctl00_ContentPlaceHolder1_gvRecords');
         //    var wb = XLSX.utils.table_to_book(table, { sheet: "sheet1" });
         //    return dl ?
         //        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
         //        XLSX.writeFile(wb, fn || (formattedDate + "." + (type || 'xlsx')));
         //}
        
     </script>
    <style>
       /*  table:nth-of-type(1) tbody:nth-of-type(1) tr:first-child {
            background: linear-gradient(180deg, rgba(251, 108, 71, 1) 0%, rgba(242, 139, 42, 1) 100%);
            color:white;
        }*/
       .hidden-column {
    display: none;
}
        .form-control img {
            height: 20px;
            width: 20px;
            vertical-align: middle;
            margin-right: 10px;
        }

        .reportGrid tbody tbody td {
            height: 35px;
            width: 35px;
            line-height: 35px;
            display: inline-block;
            background: #dce9f7;
            border: 1px solid #d1e3f7;
            text-align: center;
            margin: 0 2px;
        }
        .tblheader h3{
            margin-bottom:20px
        }
        .reportGrid tbody tbody td a {
            height: 35px;
            color: #000;
            width: 35px;
            display: block;
        }

        .reportGrid tbody tbody td:hover {
            background: #c5dffb;
            border: 1px solid #bbdbff;
        }
       .drill_btn {
    width: 75px;
    height: 33px;
    padding: 0;
}
        .tab_btn {
    margin: 2px;
}
    </style>
</asp:Content>
