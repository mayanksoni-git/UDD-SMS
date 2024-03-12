<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_DPR_Status_Report.aspx.cs" Inherits="Report_DPR_Status_Report" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" enablepartialrendering="true" enablepagemethods="true" asyncpostbacktimeout="6000">
            </cc1:toolkitscriptmanager>
            <asp:UpdatePanel ID="up" runat="server">
                <contenttemplate>
                    <cc1:modalpopupextender id="mp1" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShowPopup1"
                        cancelcontrolid="btnclose1" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:modalpopupextender id="mpTimeLine" runat="server" popupcontrolid="Panel3" targetcontrolid="btnShowPopup3"
                        cancelcontrolid="btnclose3" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">DPR Status Report
                                           
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
                                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
                                                                    <columns>
                                                                        <asp:BoundField DataField="Zone_Id" HeaderText="Zone_Id">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Circle_Id" HeaderText="Circle_Id">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="S No.">
                                                                            <itemtemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:TemplateField HeaderText="Total DPR">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkDPR" runat="server" Text='<%# Eval("TotalDPR") %>' OnClick="lnkDPR_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkDPRF" runat="server" OnClick="lnkDPRF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Executive Eng / Project Manager">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkExPM" runat="server" Text='<%# Eval("Ex_PM") %>' OnClick="lnkExPM_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkExPMF" runat="server" OnClick="lnkExPMF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At SE / GM">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkSEGM" runat="server" Text='<%# Eval("SE_GM") %>' OnClick="lnkSEGM_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkSEGMF" runat="server" OnClick="lnkSEGMF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Zonal Chief Engineer / CGM">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkZCE" runat="server" Text='<%# Eval("ZCE") %>' OnClick="lnkZCE_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkZCEF" runat="server" OnClick="lnkZCEF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Chief Engineer HQ (Mark)">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkCE" runat="server" Text='<%# Eval("CE1") %>' OnClick="lnkCE_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkCEF" runat="server" OnClick="lnkCEF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At PPRBD Cell">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkPPRBD" runat="server" Text='<%# Eval("PPRBD_SE") %>' OnClick="lnkPPRBD_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnPPRBDF" runat="server" OnClick="lnPPRBDF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Chief Engineer HQ (Recomend)">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkCE2" runat="server" Text='<%# Eval("CE2") %>' OnClick="lnkCE2_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkCEF2" runat="server" OnClick="lnkCEF2_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At SMD (AMRUT)">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkSMD" runat="server" Text='<%# Eval("SMD") %>' OnClick="lnkSMD_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkSMDF" runat="server" OnClick="lnkSMDF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Directorate (Nagar Vikas)">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkSMD1" runat="server" Text='<%# Eval("DUD") %>' OnClick="lnkSMD1_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkSMDF1" runat="server" OnClick="lnkSMDF1_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At SLTC">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkSLTC" runat="server" Text='<%# Eval("SLTC") %>' OnClick="lnkSLTC_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkSLTCF" runat="server" OnClick="lnkSLTCF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At SHPSC">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkHSPSC" runat="server" Text='<%# Eval("HSPC") %>' OnClick="lnkHSPSC_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkHSPSCF" runat="server" OnClick="lnkHSPSCF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Process Completed">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkBPM" runat="server" Text='<%# Eval("BPM") %>' OnClick="lnkBPM_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkBPMF" runat="server" OnClick="lnkBPMF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DPR Droped">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkDroped" runat="server" Text='<%# Eval("DROPED") %>' OnClick="lnkDroped_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <footertemplate>
                                                                                <asp:LinkButton ID="lnkDropedF" runat="server" OnClick="lnkDropedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </footertemplate>
                                                                        </asp:TemplateField>
                                                                    </columns>
                                                                    <footerstyle font-bold="true" backcolor="Black" forecolor="White" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div id="doc2" class="tab-pane fade">
                                                            <div class="clearfix">
                                                                <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdFinancialFull" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancialFull_PreRender" OnRowDataBound="grdFinancialFull_RowDataBound">
                                                                    <columns>
                                                                        <asp:BoundField DataField="ProjectDPR_Id" HeaderText="ProjectDPR_Id">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ProjectDPR_ProjectTypeId" HeaderText="ProjectDPR_ProjectTypeId">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ProjectDPR_Project_Id" HeaderText="ProjectDPR_Project_Id">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ProjectDPR_DistrictId" HeaderText="ProjectDPR_DistrictId">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ProjectDPR_ULBId" HeaderText="ProjectDPR_ULBId">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ProjectDPR_DivisionId" HeaderText="ProjectDPR_DivisionId">
                                                                            <headerstyle cssclass="displayStyle" />
                                                                            <itemstyle cssclass="displayStyle" />
                                                                            <footerstyle cssclass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="S No.">
                                                                            <itemtemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:ImageButton ID="btnOpenTimeline" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimeline_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                        <asp:BoundField HeaderText="ULB" DataField="ULB_Name" />
                                                                        <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectDPR_Code" />
                                                                        <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                                        <asp:BoundField HeaderText="Capex Cost (In Lakhs)" DataField="ProjectDPR_CapexCost" />
                                                                        <asp:BoundField HeaderText="O & M  (In Lakhs)" DataField="ProjectDPR_OandM_Cost" />
                                                                        <asp:BoundField HeaderText="ACA Cost  (In Lakhs)" DataField="ProjectDPR_ACA_Cost" />
                                                                        <asp:BoundField HeaderText="Project Cost  (In Lakhs)" DataField="ProjectDPR_Project_Cost" />
                                                                        <asp:BoundField HeaderText="Tentitive Date of DPR Preperation" DataField="ProjectDPR_TentitiveDate" />
                                                                        <asp:BoundField HeaderText="Date Diff Last Action" DataField="Date_Diff_Action" />
                                                                        <asp:BoundField HeaderText="Last Action Taken On" DataField="ProjectDPRApproval_AddedOn" />
                                                                        <asp:BoundField HeaderText="Current Status" DataField="Designation_Current" />
                                                                        <asp:TemplateField HeaderText="View Documents">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkViewDocs" runat="server" Text='View Docs' OnClick="lnkViewDocs_Click"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>
                                                                    </columns>
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
                                                        <columns>
                                                            <asp:BoundField DataField="ProjectDPRDocs_FileName" HeaderText="ProjectDPRDocs_FileName">
                                                                <headerstyle cssclass="displayStyle" />
                                                                <itemstyle cssclass="displayStyle" />
                                                                <footerstyle cssclass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <itemtemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="File Name" DataField="TradeDocument_Name" />
                                                            <asp:BoundField HeaderText="Order No" DataField="ProjectDPRDocs_OrderNo" />
                                                            <asp:BoundField HeaderText="Comments" DataField="ProjectDPRDocs_Comments" />
                                                            <asp:TemplateField HeaderText="Download">
                                                                <itemtemplate>
                                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("ProjectDPRDocs_FileName") %>' OnClientClick="return downloadFile(this);"></asp:LinkButton>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                        </columns>
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
                                                <columns>
                                                    <asp:BoundField DataField="ProjectDPRApproval_Id" HeaderText="ProjectDPRApproval_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <itemtemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </itemtemplate>
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
                                                        <itemtemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" OnClick="btnDelete_Click"></asp:ImageButton>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                </columns>
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
                    </div>
                    <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic2" runat="server" Value="0" />
                </contenttemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <progresstemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </progresstemplate>
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



