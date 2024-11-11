<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_DPR_Status_Report.aspx.cs" Inherits="Report_DPR_Status_Report" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpTimeLine" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12 mb-4">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">DPR Status Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">DPR</li>
                                            <li class="breadcrumb-item active">DPR Status Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                      <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">DPR Status Report </h4>
                                    </div>

                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="control-label no-padding-right">Scheme </label>
                                                        <asp:ListBox ID="ddlScheme" runat="server" SelectionMode="Multiple" class="chosen-select form-control multiselect"
                                                            data-placeholder="Choose a Scheme..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                               
                                                <div class="col-xxl-12 col-md-12 text-center">
                                                    
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                  
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div class="row">
                            <div class="col-xl-12 col-lg-8">
                                <div>
                                    <div class="card">
                                        <div class="card-header">
                                            <div class="row align-items-center">
                                                <div class="col">
                                                    <ul class="nav nav-tabs-custom card-header-tabs border-bottom-0" role="tablist">
                                                        <li class="nav-item" role="presentation">
                                                            <a class="nav-link fw-semibold" data-bs-toggle="tab" href="#productnav-all" role="tab" aria-selected="false" tabindex="-1">Summery Sheet
                                                            </a>
                                                        </li>
                                                        <li class="nav-item" role="presentation">
                                                            <a class="nav-link fw-semibold active" data-bs-toggle="tab" href="#productnav-published" role="tab" aria-selected="true">Details
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">

                                            <div class="tab-content text-muted">
                                                <div class="tab-pane" id="productnav-all" role="tabpanel">
                                                    <div id="table-product-list-all" class="table-card gridjs-border-none">
                                                        <div class="clearfix" id="dtOptions" runat="server">
                                                            <div class="pull-right tableTools-container"></div>
                                                        </div>
                                                        <!-- div.dataTables_borderWrap -->
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Zone_Id" HeaderText="Zone_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Circle_Id" HeaderText="Circle_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                    <asp:TemplateField HeaderText="Total DPR">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDPR" runat="server" Text='<%# Eval("TotalDPR") %>' OnClick="lnkDPR_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkDPRF" runat="server" OnClick="lnkDPRF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At Executive Eng / Project Manager">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkExPM" runat="server" Text='<%# Eval("Ex_PM") %>' OnClick="lnkExPM_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkExPMF" runat="server" OnClick="lnkExPMF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At SE / GM">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSEGM" runat="server" Text='<%# Eval("SE_GM") %>' OnClick="lnkSEGM_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkSEGMF" runat="server" OnClick="lnkSEGMF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At Zonal Chief Engineer / CGM">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkZCE" runat="server" Text='<%# Eval("ZCE") %>' OnClick="lnkZCE_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkZCEF" runat="server" OnClick="lnkZCEF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At Chief Engineer HQ (Mark)">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkCE" runat="server" Text='<%# Eval("CE1") %>' OnClick="lnkCE_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkCEF" runat="server" OnClick="lnkCEF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At PPRBD Cell">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPPRBD" runat="server" Text='<%# Eval("PPRBD_SE") %>' OnClick="lnkPPRBD_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnPPRBDF" runat="server" OnClick="lnPPRBDF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At Chief Engineer HQ (Recomend)">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkCE2" runat="server" Text='<%# Eval("CE2") %>' OnClick="lnkCE2_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkCEF2" runat="server" OnClick="lnkCEF2_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At SMD (AMRUT)">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSMD" runat="server" Text='<%# Eval("SMD") %>' OnClick="lnkSMD_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkSMDF" runat="server" OnClick="lnkSMDF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At Directorate (Nagar Vikas)">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSMD1" runat="server" Text='<%# Eval("DUD") %>' OnClick="lnkSMD1_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkSMDF1" runat="server" OnClick="lnkSMDF1_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At SLTC">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSLTC" runat="server" Text='<%# Eval("SLTC") %>' OnClick="lnkSLTC_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkSLTCF" runat="server" OnClick="lnkSLTCF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="At SHPSC">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkHSPSC" runat="server" Text='<%# Eval("HSPC") %>' OnClick="lnkHSPSC_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkHSPSCF" runat="server" OnClick="lnkHSPSCF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Process Completed">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBPM" runat="server" Text='<%# Eval("BPM") %>' OnClick="lnkBPM_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkBPMF" runat="server" OnClick="lnkBPMF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DPR Droped">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDroped" runat="server" Text='<%# Eval("DROPED") %>' OnClick="lnkDroped_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkDropedF" runat="server" OnClick="lnkDropedF_Click" ForeColor="White"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- end tab pane -->

                                                <div class="tab-pane active show" id="productnav-published" role="tabpanel">
                                                    <div id="table-product-list-published" class="table-card gridjs-border-none">
                                                        <div class="clearfix">
                                                            <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                        </div>
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdFinancialFull" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancialFull_PreRender" OnRowDataBound="grdFinancialFull_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectDPR_Id" HeaderText="ProjectDPR_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectDPR_ProjectTypeId" HeaderText="ProjectDPR_ProjectTypeId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectDPR_Project_Id" HeaderText="ProjectDPR_Project_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectDPR_DistrictId" HeaderText="ProjectDPR_DistrictId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectDPR_ULBId" HeaderText="ProjectDPR_ULBId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectDPR_DivisionId" HeaderText="ProjectDPR_DivisionId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            <asp:ImageButton ID="btnOpenTimeline" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimeline_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />--%>
                                                                    <%--<asp:BoundField HeaderText="Zone" DataField="Zone_Name" />--%>
                                                                    <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                                    <asp:BoundField HeaderText="ULB" DataField="Division_Name" />
                                                                    <%--<asp:BoundField HeaderText="ULB" DataField="ULB_Name" />--%>
                                                                    <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                                    <asp:BoundField HeaderText="Scheme Short Name" DataField="ShortNameCode" />
                                                                    <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                                    <asp:BoundField HeaderText="Capex Cost (In Lakhs)" DataField="ProjectDPR_CapexCost" />
                                                                    <asp:BoundField HeaderText="O & M  (In Lakhs)" DataField="ProjectDPR_OandM_Cost" />
                                                                    <%--<asp:BoundField HeaderText="ACA Cost  (In Lakhs)" DataField="ProjectDPR_ACA_Cost" />--%>
                                                                    <asp:BoundField HeaderText="Project Cost  (In Lakhs)" DataField="ProjectDPR_Project_Cost" />
                                                                    <asp:BoundField HeaderText="Tentitive Date of DPR Preperation" DataField="ProjectDPR_TentitiveDate" />
                                                                    <asp:BoundField HeaderText="Date Diff Last Action" DataField="Date_Diff_Action" />
                                                                    <asp:BoundField HeaderText="Last Action Taken On" DataField="ProjectDPRApproval_AddedOn" />
                                                                    <asp:BoundField HeaderText="Current Status" DataField="Designation_Current" />
                                                                    <asp:TemplateField HeaderText="View Documents">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkViewDocs" runat="server" Text='View Docs' OnClick="lnkViewDocs_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- end tab pane -->
                                            </div>
                                            <!-- end tab content -->
                                        </div>
                                        <!-- end card body -->
                                    </div>
                                    <!-- end card -->
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Related Documents Attached                                 
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdMultipleFiles" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdMultipleFiles_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectDPRDocs_FileName" HeaderText="ProjectDPRDocs_FileName">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="File Name" DataField="TradeDocument_Name" />
                                                        <asp:BoundField HeaderText="Order No" DataField="ProjectDPRDocs_OrderNo" />
                                                        <asp:BoundField HeaderText="Comments" DataField="ProjectDPRDocs_Comments" />
                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("ProjectDPRDocs_FileName") %>' OnClientClick="return downloadFile(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <button id="btnclose1" runat="server" text="Close" class="btn btn-warning"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">Timeline Analysis 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdTimeLine" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdTimeLine_PreRender" OnRowDataBound="grdTimeLine_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectDPRApproval_Id" HeaderText="ProjectDPRApproval_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Mode" HeaderText="Mode" />
                                                <asp:BoundField DataField="ProjectDPRApproval_Status_Text" HeaderText="Action Taken" />
                                                <asp:BoundField DataField="ProjectDPRApproval_Comments" HeaderText="Comments (If Any)" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Action By (Designation)" />
                                                <asp:BoundField DataField="Person_Name" HeaderText="Action By (Name)" />
                                                <asp:BoundField DataField="Designation_Next" HeaderText="Next Action (Designation)" />
                                                <asp:BoundField DataField="ProjectDPRApproval_AddedOn" HeaderText="Action Taken On" />
                                                <asp:BoundField DataField="t1" HeaderText="Time Elapsed (Overall)" />
                                                <asp:BoundField DataField="t2" HeaderText="Time Elapsed (Step Wise)" />
                                                <asp:TemplateField HeaderText="Rollback">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" OnClick="btnDelete_Click"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose3" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                    <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic2" runat="server" Value="0" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });

        function downloadGO(obj) {
            var GO_FilePath;
            GO_FilePath = obj.attributes.GO_FilePath.nodeValue;
            if (GO_FilePath.trim() == "") {
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + GO_FilePath, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }

        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }
    </script>
</asp:Content>



