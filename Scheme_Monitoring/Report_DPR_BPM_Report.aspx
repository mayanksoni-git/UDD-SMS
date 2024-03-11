<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_DPR_BPM_Report.aspx.cs" Inherits="Report_DPR_BPM_Report" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpBidder" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">BID Process Management Status Report
                                           
                                            <div class="form-group" style="float: right; padding-right: 10px">
                                                <asp:RadioButtonList ID="rbtMappingWith" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtMappingWith_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="Project For Division" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="Project For ULB" Value="U"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </h3>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Scheme </label>
                                                        <asp:ListBox ID="ddlScheme" runat="server" SelectionMode="Multiple" class="chosen-select form-control"
                                                            data-placeholder="Choose a Scheme..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divZone" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDistrict" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">District* </label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divCircle" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divULB" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDivision" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server" Text="Tranche" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlTranche" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divNodal" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" Text="Nodal Department" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlNodalDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="tabbable">
                                                    <ul class="nav nav-tabs" id="myTab2">
                                                        <li class="active" id="w_1">
                                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                Summery Sheet
                                                            </a>
                                                        </li>

                                                        <li class="" id="w_2">
                                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                Details
                                                            </a>
                                                        </li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div id="doc1" class="tab-pane fade active in">
                                                            <!-- div.table-responsive -->
                                                            <div class="clearfix" id="dtOptions" runat="server">
                                                                <div class="pull-right tableTools-container"></div>
                                                            </div>
                                                            <!-- div.dataTables_borderWrap -->
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
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
                                                                        <asp:TemplateField HeaderText="Process Not Initiated">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkNotStarted" runat="server" Text='<%# Eval("Process_Not_Started") %>' OnClick="lnkNotStarted_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkNotStartedF" runat="server" OnClick="lnkNotStartedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EFC / PFAD Done">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEFC_PFAD" runat="server" Text='<%# Eval("EFC_PFAD") %>' OnClick="lnkEFC_PFAD_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkEFC_PFADF" runat="server" OnClick="lnkEFC_PFADF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="GO Issued">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkGO_Issued" runat="server" Text='<%# Eval("GO_Issued") %>' OnClick="lnkGO_Issued_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkGO_IssuedF" runat="server" OnClick="lnkGO_IssuedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NIT Issued">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkNIT_Issued" runat="server" Text='<%# Eval("NIT_Issued") %>' OnClick="lnkNIT_Issued_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkNIT_IssuedF" runat="server" OnClick="lnkNIT_IssuedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NIT Issued 2">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkNIT_Issued2" runat="server" Text='<%# Eval("NIT_Issued2") %>' OnClick="lnkNIT_Issued2_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkNIT_Issued2F" runat="server" OnClick="lnkNIT_Issued2F_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tender Published">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkTender_Published" runat="server" Text='<%# Eval("Tender_Published") %>' OnClick="lnkTender_Published_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkTender_PublishedF" runat="server" OnClick="lnkTender_PublishedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pre Bid Meeting">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPre_Bid_Meeting" runat="server" Text='<%# Eval("Pre_Bid_Meeting") %>' OnClick="lnkPre_Bid_Meeting_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkPre_Bid_MeetingF" runat="server" OnClick="lnkPre_Bid_MeetingF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Technical Bid Opened">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkTechnical_Bid_Opened" runat="server" Text='<%# Eval("Technical_Bid_Opened") %>' OnClick="lnkTechnical_Bid_Opened_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkTechnical_Bid_OpenedF" runat="server" OnClick="lnkTechnical_Bid_OpenedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bidders Evaluation (Technical)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkBidders_Evaluation_Technical" runat="server" Text='<%# Eval("Bidders_Evaluation_Technical") %>' OnClick="lnkBidders_Evaluation_Technical_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkBidders_Evaluation_TechnicalF" runat="server" OnClick="lnkBidders_Evaluation_TechnicalF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Financial Bid Opened">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkFinancial_Bid_Opened" runat="server" Text='<%# Eval("Financial_Bid_Opened") %>' OnClick="lnkFinancial_Bid_Opened_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkFinancial_Bid_OpenedF" runat="server" OnClick="lnkFinancial_Bid_OpenedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Send To SMD For Approval">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSend_To_SMD_For_Approval" runat="server" Text='<%# Eval("Send_To_SMD_For_Approval") %>' OnClick="lnkSend_To_SMD_For_Approval_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkSend_To_SMD_For_ApprovalF" runat="server" OnClick="lnkSend_To_SMD_For_ApprovalF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SLTC Meeting After Tender Approval">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSLTC_Meeting_After_Tender_Approval" runat="server" Text='<%# Eval("SLTC_Meeting_After_Tender_Approval") %>' OnClick="lnkSLTC_Meeting_After_Tender_Approval_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkSLTC_Meeting_After_Tender_ApprovalF" runat="server" OnClick="lnkSLTC_Meeting_After_Tender_ApprovalF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Work Order Issued">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkWork_Order_Issued" runat="server" Text='<%# Eval("Work_Order_Issued") %>' OnClick="lnkWork_Order_Issued_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkWork_Order_IssuedF" runat="server" OnClick="lnkWork_Order_IssuedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Agreement With L1 Bidder">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkAgreement_With_Bidder" runat="server" Text='<%# Eval("Agreement_With_Bidder") %>' OnClick="lnkAgreement_With_Bidder_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkAgreement_With_BidderF" runat="server" OnClick="lnkAgreement_With_BidderF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div id="doc2" class="tab-pane fade">
                                                            <div class="clearfix">
                                                                <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdFinancialFull" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancialFull_PreRender" OnRowDataBound="grdFinancialFull_RowDataBound">
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
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                        <asp:BoundField HeaderText="ULB" DataField="ULB_Name" />
                                                                        <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                                        <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectDPR_Code" />
                                                                        <asp:BoundField HeaderText="Capex Cost (In Lakhs)" DataField="ProjectDPR_CapexCost" />
                                                                        <asp:BoundField HeaderText="O & M  (In Lakhs)" DataField="ProjectDPR_OandM_Cost" />
                                                                        <asp:BoundField HeaderText="ACA Cost  (In Lakhs)" DataField="ProjectDPR_ACA_Cost" />
                                                                        <asp:BoundField HeaderText="Project Cost  (In Lakhs)" DataField="ProjectDPR_Project_Cost" />
                                                                        <asp:BoundField HeaderText="Tentitive Date Of DPR Submission" DataField="ProjectDPR_TentitiveDate" />
                                                                        <asp:TemplateField HeaderText="EFC / PFAD Done">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEFC_PFAD_D" runat="server" Text='<%# Eval("EFC_PFAD") %>' ToolTip='<%# Eval("EFC_PFAD_Id") %>' OnClick="lnkEFC_PFAD_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="GO Issued">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkGO_Issued_D" runat="server" Text='<%# Eval("GO_Issued") %>' ToolTip='<%# Eval("GO_Issued_Id") %>' OnClick="lnkGO_Issued_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NIT Issued">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkNIT_Issued_D" runat="server" Text='<%# Eval("NIT_Issued") %>' ToolTip='<%# Eval("NIT_Issued_Id") %>' OnClick="lnkNIT_Issued_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tender Published">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkTender_Published_D" runat="server" Text='<%# Eval("Tender_Published") %>' ToolTip='<%# Eval("Tender_Published_Id") %>' OnClick="lnkTender_Published_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pre Bid Meeting">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPre_Bid_Meeting_D" runat="server" Text='<%# Eval("Pre_Bid_Meeting") %>' ToolTip='<%# Eval("Pre_Bid_Meeting_Id") %>' OnClick="lnkPre_Bid_Meeting_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Technical Bid Opened">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkTechnical_Bid_Opened_D" runat="server" Text='<%# Eval("Technical_Bid_Opened") %>' ToolTip='<%# Eval("Technical_Bid_Opened_Id") %>' OnClick="lnkTechnical_Bid_Opened_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bidders Evaluation (Technical)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkBidders_Evaluation_Technical_D" runat="server" Text='<%# Eval("Bidders_Evaluation_Technical") %>' ToolTip='<%# Eval("Bidders_Evaluation_Technical_Id") %>' OnClick="lnkBidders_Evaluation_Technical_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Financial Bid Opened">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkFinancial_Bid_Opened_D" runat="server" Text='<%# Eval("Financial_Bid_Opened") %>' ToolTip='<%# Eval("Financial_Bid_Opened_Id") %>' OnClick="lnkFinancial_Bid_Opened_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Send To SMD For Approval">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSend_To_SMD_For_Approval_D" runat="server" Text='<%# Eval("Send_To_SMD_For_Approval") %>' ToolTip='<%# Eval("Send_To_SMD_For_Approval_Id") %>' OnClick="lnkSend_To_SMD_For_Approval_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SLTC Meeting After Tender Approval">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSLTC_Meeting_After_Tender_Approval_D" runat="server" Text='<%# Eval("SLTC_Meeting_After_Tender_Approval") %>' ToolTip='<%# Eval("SLTC_Meeting_After_Tender_Approval_Id") %>' OnClick="lnkSLTC_Meeting_After_Tender_Approval_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Work Order Issued">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkWork_Order_Issued_D" runat="server" Text='<%# Eval("Work_Order_Issued") %>' ToolTip='<%# Eval("Work_Order_Issued_Id") %>' OnClick="lnkWork_Order_Issued_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Agreement With L1 Bidder">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkAgreement_With_Bidder_D" runat="server" Text='<%# Eval("Agreement_With_Bidder") %>' ToolTip='<%# Eval("Agreement_With_Bidder_Id") %>' OnClick="lnkAgreement_With_Bidder_D_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
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
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 900px; height: 80%; margin-left: -32px" ScrollBars="Auto">
                            <div class="row" runat="server" id="div_Report_Step1">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdEFC_PFAD" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdEFC_PFAD_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectDPRTender_Id" HeaderText="ProjectDPRTender_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectDPRTender_DocumentPath" HeaderText="ProjectDPRTender_DocumentPath">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectDPRTender_ActionDate" HeaderText="Action Date" />
                                                <asp:BoundField DataField="ProjectDPRTender_CapexCostApproved" HeaderText="Capex Cost In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_OMCostApproved" HeaderText="O & M Cost In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_ACACostApproved" HeaderText="ACA Cost In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_CostApproved" HeaderText="Approved Cost As Per EFC / PFAD In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_Comments" HeaderText="Comments (If Any)" />
                                                <asp:TemplateField HeaderText="Download Document">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRTender_DocumentPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div_Report_Step2">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdGODetails" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdGODetails_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectDPRTender_Id" HeaderText="ProjectDPRTender_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectDPRTender_DocumentPath" HeaderText="ProjectDPRTender_DocumentPath">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectDPRTender_ActionDate" HeaderText="GO Date" />
                                                <asp:BoundField DataField="ProjectDPRTender_Comments" HeaderText="GO No" />
                                                <asp:BoundField DataField="ProjectDPRTender_CentralShare" HeaderText="Central Share In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_StateShare" HeaderText="State Share In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_ULBShare" HeaderText="ULB Share In Lakhs" />
                                                <asp:BoundField DataField="ProjectDPRTender_CentageShare" HeaderText="Centage In Lakhs" />
                                                <asp:TemplateField HeaderText="Download Document">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRTender_DocumentPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div_Report_Step_Common">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdCommonStep" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdCommonStep_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectDPRTender_Id" HeaderText="ProjectDPRTender_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectDPRTender_DocumentPath" HeaderText="ProjectDPRTender_DocumentPath">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectDPRTender_ActionDate" HeaderText="Action Date" />
                                                <asp:BoundField DataField="ProjectDPRTender_Comments" HeaderText="Comments (If Any)" />
                                                <asp:TemplateField HeaderText="Download Document">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRTender_DocumentPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="div_Report_Step5">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdTenderPublished" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdTenderPublished_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRTender_Id" HeaderText="ProjectDPRTender_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRTender_DocumentPath" HeaderText="ProjectDPRTender_DocumentPath">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRTender_DocumentPath_A" HeaderText="ProjectDPRTender_DocumentPath_A">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRTender_ActionDate" HeaderText="Tender Published On Date" />
                                                    <asp:BoundField DataField="ProjectDPRTender_Comments" HeaderText="Bid Refrence No" />
                                                    <asp:BoundField DataField="ProjectDPRTender_TenderEndDate" HeaderText="Tender Closing / End Date" />
                                                    <asp:BoundField DataField="ProjectDPRTender_TechnicalBidOpeningDate" HeaderText="Technical Bid Opening Date" />
                                                    <asp:BoundField DataField="ProjectDPRTender_TenderCost" HeaderText="Tender Cost In Lakhs" />
                                                    <asp:TemplateField HeaderText="Download RFP">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRTender_DocumentPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="x` Document">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload_A" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRTender_DocumentPath_A") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <h3 class="header smaller red">Qualification Criteria Details 
                                </h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdQualificationCriteria" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdQualificationCriteria_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRPQC_Id" HeaderText="ProjectDPRPQC_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_DPR_Id" HeaderText="ProjectDPRPQC_DPR_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQCName" HeaderText="Qualification Criteria Details" />
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQCMinVal" HeaderText="Minimim Qualification" />
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQCMaxVal" HeaderText="Maximum Qualification (If Any)" Visible="false" />
                                                    <asp:BoundField DataField="ProjectDPRPQC_Comments" HeaderText="Comments" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <h3 class="header smaller red">Pre Bid Meeting Response Documents (If Any) 
                                </h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdPBMResponseDoc" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPBMResponseDoc_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRBidResponse_Id" HeaderText="ProjectDPRBidResponse_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidResponse_DPR_Id" HeaderText="ProjectDPRBidResponse_DPR_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidResponse_DocumentPath" HeaderText="ProjectDPRBidResponse_DocumentPath">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRBidResponse_BidResponseName" HeaderText="Document Name" />
                                                    <asp:TemplateField HeaderText="View Document">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRBidResponse_DocumentPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divBidder">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdBidder" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdBidder_PreRender" OnRowDataBound="grdBidder_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRBidder_Id" HeaderText="ProjectDPRBidder_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_Is_JV" HeaderText="Is JV?" />
                                                    <asp:TemplateField HeaderText="Bidder Details">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Lead Bidder</td>
                                                                        <td>
                                                                            <asp:Label ID="txtFirmName" runat="server" CssClass="control-label no-padding-right" Text='<%#Eval("ProjectDPRBidder_BidderName") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="txtFirmPAN" runat="server" CssClass="control-label no-padding-right" MaxLength="10" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderPAN") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="txtFirmGSTIN" runat="server" CssClass="control-label no-padding-right" Text='<%#Eval("ProjectDPRBidder_BidderGSTIN") %>' MaxLength="15" Style="text-transform: uppercase"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="txtContactNo" runat="server" CssClass="control-label no-padding-right" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderMobile") %>' MaxLength="10"></asp:Label></td>
                                                                        <td runat="server" id="tdShare" visible="false">
                                                                            <asp:Label ID="txtShare" runat="server" CssClass="control-label no-padding-right" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderShare") %>' MaxLength="4"></asp:Label></td>
                                                                    </tr>
                                                                    <tr runat="server" id="trPartnerBidder" visible="false">
                                                                        <td>Partner Bidder</td>
                                                                        <td>
                                                                            <asp:Label ID="txtFirmNameP" runat="server" CssClass="control-label no-padding-right" Text='<%#Eval("ProjectDPRBidder_BidderNameP") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="txtFirmPANP" runat="server" CssClass="control-label no-padding-right" MaxLength="10" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderPANP") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="txtFirmGSTINP" runat="server" CssClass="control-label no-padding-right" MaxLength="15" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderGSTINP") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="txtContactNoP" runat="server" CssClass="control-label no-padding-right" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderMobileP") %>' MaxLength="10"></asp:Label></td>
                                                                        <td runat="server" id="tdShareP" visible="false">
                                                                            <asp:Label ID="txtShareP" runat="server" CssClass="control-label no-padding-right" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderShareP") %>' MaxLength="4"></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_BidderAmount" HeaderText="Bid Amount In Lakhs" />
                                                    <asp:BoundField DataField="ProjectDPRBidder_Comments" HeaderText="Comments" />
                                                    <asp:BoundField DataField="ProjectDPRBidder_TechnicalQualified" HeaderText="Technically Qualified" />
                                                    <asp:BoundField DataField="ProjectDPRBidder_FinancialQualified" HeaderText="Financially Qualified" />
                                                    <asp:BoundField DataField="ProjectDPRBidder_BidderAmount" HeaderText="Quoted Rate (In Lakhs)" />
                                                    <asp:TemplateField HeaderText="See Qualification">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnOpenQualification" runat="server" Text="Open Form" OnClick="btnOpenQualification_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <h3 class="header smaller red">Details Of Work Experience for Above Bidder
                                </h3>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdQualificationWorkOrder" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdQualificationWorkOrder_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Id" HeaderText="ProjectDPR_Bidder_Order_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_DPR_Id" HeaderText="ProjectDPR_Bidder_Order_DPR_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_DPRBidder_Id" HeaderText="ProjectDPR_Bidder_Order_DPRBidder_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_BidderType" HeaderText="Bidder Type" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Name_Of_Work" HeaderText="Name Of Work" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_StartDate" HeaderText="Date of Start" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_EndDate" HeaderText="Actual Date of Completion" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Amount" HeaderText="Total Value Of work Done (Without GST) [In Lakh]" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_JV_Share" HeaderText="% Share (In Case of JV)" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_JV_Contract_Value" HeaderText="Proportionate Value Of Work [In Lakh]" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Inflation" HeaderText="Inflation (%)" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Amount_After_Inflation" HeaderText="Value After Inflation [In Lakh]" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Simmilar_Nature" HeaderText="Is Work of Similar Nature" />
                                                    <asp:BoundField DataField="ProjectDPR_Bidder_Order_Completed" HeaderText="Completed & Comissioned" />
                                                    <asp:TemplateField HeaderText="Download Order">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownloadWO" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPR_Bidder_Order_OrderPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Download Verification">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownloadVer" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPR_Bidder_Order_VerificationPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Download Verification Letter">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownloadVerLetter" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPR_Bidder_Order_VerificationLetterPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <h3 class="header smaller red">Qualification Response 
                                </h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdQualificationResponse" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdQualificationResponse_PreRender" OnRowDataBound="grdQualificationResponse_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRPQC_Id" HeaderText="ProjectDPRPQC_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_DPR_Id" HeaderText="ProjectDPRPQC_DPR_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_Id" HeaderText="ProjectDPRPQCResponse_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_DPRBidder_Id" HeaderText="ProjectDPRPQCResponse_DPRBidder_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_FilePath1" HeaderText="ProjectDPRPQCResponse_FilePath1">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_FilePath2" HeaderText="ProjectDPRPQCResponse_FilePath2">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_FilePath3" HeaderText="ProjectDPRPQCResponse_FilePath3">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_FileVerified1" HeaderText="ProjectDPRPQCResponse_FileVerified1">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_FileVerified2" HeaderText="ProjectDPRPQCResponse_FileVerified2">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_FileVerified3" HeaderText="ProjectDPRPQCResponse_FileVerified3">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQC_Enable_Verification_Document" HeaderText="ProjectDPRPQC_PQC_Enable_Verification_Document">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQC_Upload_Document_Count" HeaderText="ProjectDPRPQC_PQC_Upload_Document_Count">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQC_Auto_Calculated" HeaderText="ProjectDPRPQC_PQC_Auto_Calculated">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQC_Id" HeaderText="ProjectDPRPQC_PQC_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQCName" HeaderText="Qualification Criteria Details" />
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQCMinVal" HeaderText="Min Value" />
                                                    <asp:BoundField DataField="ProjectDPRPQC_PQCMaxVal" HeaderText="Max Value" />
                                                    <asp:BoundField DataField="ProjectDPRPQC_Comments" HeaderText="Comments" />
                                                    <asp:BoundField DataField="ProjectDPRPQCResponse_Response" HeaderText="Bidder Value" />
                                                    <asp:TemplateField HeaderText="Response">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top" runat="server" id="tbl_Response">
                                                                <thead>
                                                                    <tr>
                                                                        <th>View Relavent Document</th>
                                                                        <th>Document Verification Done?</th>
                                                                        <th>Uploaded Verification Doc</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownload1" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRPQCResponse_FilePath1") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkVerified1" runat="server"></asp:CheckBox></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownloadV1" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRPQCResponse_FileVerified1") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownload2" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRPQCResponse_FilePath2") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkVerified2" runat="server"></asp:CheckBox></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownloadV2" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRPQCResponse_FileVerified2") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownload3" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRPQCResponse_FilePath3") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkVerified3" runat="server"></asp:CheckBox></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownloadV3" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRPQCResponse_FileVerified3") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top" runat="server" id="tblAdditionalData">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Mode</th>
                                                                        <th>Ref / UTR / BG No / UDIN No</th>
                                                                        <th>Bank Name / CA Name</th>
                                                                        <th>Date</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblMode" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_PaymentMode") %>' CssClass="control-label no-padding-right"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_RefrenceNo") %>' CssClass="control-label no-padding-right"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblBankName" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_BankName") %>' CssClass="control-label no-padding-right"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_RefDate") %>' CssClass="control-label no-padding-right"></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose1" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_ProjectDPR_Id" runat="server" Value="0" />
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

    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <%--<script src="assets/js/dataTables.colReorder.min.js"></script>--%>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPost').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPost')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic1').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPost')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: false,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": dt_Options_Dynamic1,
                                    "aaSorting": [],
                                    //"bProcessing": true,
                                    //"bServerSide": true,
                                    //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                    //,
                                    //"sScrollY": "200px",
                                    //"bPaginate": false,
                                    //"sScrollX": "100%",
                                    //"sScrollXInner": "120%",
                                    //"bScrollCollapse": true,
                                    //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                    //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                    "iDisplayLength": 25,
                                    select: {
                                        style: 'multi'
                                    }
                                });
                        $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
                        new $.fn.dataTable.Buttons(myTable, {
                            buttons: [
                                {
                                    "extend": "colvis",
                                    "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    columns: ':not(:first):not(:last)'
                                },
                                {
                                    "extend": "copy",
                                    "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "csv",
                                    "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "excel",
                                    "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "pdf",
                                    "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "print",
                                    "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    autoPrint: true,
                                    message: 'This print was produced using the Print button for DataTables',
                                    exportOptions: {
                                        columns: ':visible'
                                    }
                                }
                            ]
                        });
                        myTable.buttons().container().appendTo($('.tableTools-container'));

                        //style the message box
                        var defaultCopyAction = myTable.button(1).action();
                        myTable.button(1).action(function (e, dt, button, config) {
                            defaultCopyAction(e, dt, button, config);
                            $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
                        });
                        var defaultColvisAction = myTable.button(0).action();
                        myTable.button(0).action(function (e, dt, button, config) {

                            defaultColvisAction(e, dt, button, config);
                            if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                                $('.dt-button-collection')
                                    .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                                    .find('a').attr('href', '#').wrap("<li />")
                            }
                            $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.tableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPost .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have TableTools or dataTables
                        //select/deselect all rows according to table header checkbox
                        var active_class = 'active';
                        /********************************/
                        //add tooltip for small view action buttons in dropdown menu
                        $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

                        //tooltip placement on right or left
                        function tooltip_placement(context, source) {
                            var $source = $(source);
                            var $parent = $source.closest('table')
                            var off1 = $parent.offset();
                            var w1 = $parent.width();

                            var off2 = $source.offset();
                            //var w2 = $source.width();

                            if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                            return 'left';
                        }
                        /***************/
                        $('.show-details-btn').on('click', function (e) {
                            e.preventDefault();
                            $(this).closest('tr').next().toggleClass('open');
                            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
                        });
                    }
                }
            })
        });
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdFinancialFull').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdFinancialFull')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic2').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdFinancialFull')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: true,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": dt_Options_Dynamic1,
                                    "aaSorting": [],
                                    //"bProcessing": true,
                                    //"bServerSide": true,
                                    //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                    //,
                                    //"sScrollY": "200px",
                                    //"bPaginate": false,
                                    //"sScrollX": "100%",
                                    //"sScrollXInner": "120%",
                                    //"bScrollCollapse": true,
                                    //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                    //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                    "iDisplayLength": 25,
                                    select: {
                                        style: 'multi'
                                    }
                                });
                        $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
                        new $.fn.dataTable.Buttons(myTable, {
                            buttons: [
                                {
                                    "extend": "colvis",
                                    "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    columns: ':not(:first):not(:last)'
                                },
                                {
                                    "extend": "copy",
                                    "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "csv",
                                    "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "excel",
                                    "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "pdf",
                                    "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "print",
                                    "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    autoPrint: true,
                                    message: 'This print was produced using the Print button for DataTables',
                                    exportOptions: {
                                        columns: ':visible'
                                    }
                                }
                            ]
                        });
                        myTable.buttons().container().appendTo($('.grdFinancialFulltableTools-container'));

                        //style the message box
                        var defaultCopyAction = myTable.button(1).action();
                        myTable.button(1).action(function (e, dt, button, config) {
                            defaultCopyAction(e, dt, button, config);
                            $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
                        });
                        var defaultColvisAction = myTable.button(0).action();
                        myTable.button(0).action(function (e, dt, button, config) {

                            defaultColvisAction(e, dt, button, config);
                            if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                                $('.dt-button-collection')
                                    .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                                    .find('a').attr('href', '#').wrap("<li />")
                            }
                            $('.dt-button-collection').appendTo('.grdFinancialFulltableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdFinancialFulltableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdFinancialFull .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have TableTools or dataTables
                        //select/deselect all rows according to table header checkbox
                        var active_class = 'active';
                        /********************************/
                        //add tooltip for small view action buttons in dropdown menu
                        $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

                        //tooltip placement on right or left
                        function tooltip_placement(context, source) {
                            var $source = $(source);
                            var $parent = $source.closest('table')
                            var off1 = $parent.offset();
                            var w1 = $parent.width();

                            var off2 = $source.offset();
                            //var w2 = $source.width();

                            if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                            return 'left';
                        }
                        /***************/
                        $('.show-details-btn').on('click', function (e) {
                            e.preventDefault();
                            $(this).closest('tr').next().toggleClass('open');
                            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
                        });
                    }
                }
            })
        });
    </script>
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



