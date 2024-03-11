<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterDPR_NextAction.aspx.cs" Inherits="MasterDPR_NextAction" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpTimeLine" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpBidder" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpBidderResponse" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Update Bid Process Steps Details
                               
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Scheme </label>
                                        <asp:DropDownList ID="ddlSearchScheme" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divCircle" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divDivision" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Tranche" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlTranche" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divNodal" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" Text="Nodal Department" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlNodalDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h3 class="header smaller lighter blue">Project List</h3>

                                            <!-- div.table-responsive -->
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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
                                                        <asp:BoundField DataField="ProjectDPRApproval_Next_Designation_Id" HeaderText="ProjectDPRApproval_Next_Designation_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRApproval_Next_Organisation_Id" HeaderText="ProjectDPRApproval_Next_Organisation_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRApproval_Loop" HeaderText="ProjectDPRApproval_Loop">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRApproval_Id" HeaderText="ProjectDPRApproval_Id">
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
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                        <asp:BoundField HeaderText="Capex Cost (In Lakhs)" DataField="ProjectDPR_CapexCost" />
                                                        <asp:BoundField HeaderText="O & M  (In Lakhs)" DataField="ProjectDPR_OandM_Cost" />
                                                        <asp:BoundField HeaderText="ACA Cost  (In Lakhs)" DataField="ProjectDPR_ACA_Cost" />
                                                        <asp:BoundField HeaderText="Project Cost  (In Lakhs)" DataField="ProjectDPR_Project_Cost" />
                                                        <asp:BoundField DataField="DPRTenderSteps_StepName" HeaderText="Step Completed" />
                                                        <asp:BoundField DataField="ProjectDPRTender_ActionDate" HeaderText="Lat Action Date" />
                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectDPR_Code" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div runat="server" visible="false" id="divEntry">

                            <h3 class="header smaller red">Timeline Analysis For Selected DPR
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdDPRTimeline" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdDPRTimeline_PreRender" OnRowDataBound="grdDPRTimeline_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectDPRTender_Id" HeaderText="ProjectDPRTender_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectDPRTender_ProjectDPR_Id" HeaderText="ProjectDPRTender_ProjectDPR_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectDPRTender_Step_Status" HeaderText="ProjectDPRTender_Step_Status">
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
                                                <asp:BoundField DataField="DPRTenderSteps_StepName" HeaderText="Action Taken" />
                                                <asp:BoundField DataField="ProjectDPRTender_ActionDate" HeaderText="Action Date" />
                                                <asp:BoundField DataField="ProjectDPRTender_Comments" HeaderText="Comments (If Any)" />
                                                <asp:BoundField DataField="ProjectDPRTender_AddedOn" HeaderText="Action Taken On" />
                                                <asp:TemplateField HeaderText="View Details">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkBidder" Text="View" OnClick="lnkBidder_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Download Document">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectDPRTender_DocumentPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete Step">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClick="btnDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkActionCorrigendum" Text="Add Corrigendum" OnClick="lnkActionCorrigendum_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Action Status</label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divStep1" visible="false">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">EFC / PFAD Date</label>
                                                <asp:TextBox ID="txtEFCDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Capex Cost As Per DPR (In Lakhs)</label>
                                                <asp:TextBox ID="txtCapexCostApproved" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Approved Cost As Per EFC / PFAD (In Lakhs)</label>
                                                <asp:TextBox ID="txtCostApproved" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="display: none;">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">O & M Cost As Per DPR (In Lakhs)</label>
                                                <asp:TextBox ID="txtOMCostApproved" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload EFC / PFAD Document</label>
                                                <asp:FileUpload ID="fl_Docs_Step1" runat="server" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="display: none;">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">ACA Cost As Per DPR (In Lakhs)</label>
                                                <asp:TextBox ID="txtACACostApproved" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Comments</label>
                                                <asp:TextBox ID="txtCommentStep1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="divEFCPFADSkip">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnSkipEFC" runat="server" CssClass="btn btn-danger" Text="Skip And Proceed Next >>" OnClick="btnSkipEFC_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divStep2" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">GO Issued On Date</label>
                                            <asp:TextBox ID="txtGODate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step2" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">GO Number</label>
                                            <asp:TextBox ID="txtGONumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Total Share (In Lakhs)</label>
                                            <asp:TextBox ID="txtGOTotalShare" ReadOnly="true" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <div class="col-md-12">
                                        <span class="label label-danger arrowed">All Values Will Be Inclusive of GST</span>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Central Share (In Lakhs)</label>
                                            <asp:TextBox ID="txtGOCentralShare" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" onchange="return calculateTotal();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">State Share (In Lakhs)</label>
                                            <asp:TextBox ID="txtGOStateShare" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" onchange="return calculateTotal();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">ULB Share (In Lakhs)</label>
                                            <asp:TextBox ID="txtGOULBShare" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" onchange="return calculateTotal();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Centage (In Lakhs)</label>
                                            <asp:TextBox ID="txtGOCentage" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" onchange="return calculateTotal();"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" id="divStep3" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Action Date</label>
                                            <asp:TextBox ID="txtNITIssuedDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step3" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Comments</label>
                                            <asp:TextBox ID="txtCommentStep3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" id="divStep4" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">NIT Issued On Date</label>
                                            <asp:TextBox ID="txtNITIssuedDate1" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload NIT Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step4" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Comments</label>
                                            <asp:TextBox ID="txtCommentStep4" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" id="divStep5" visible="false">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Tender Published On Date</label>
                                                <asp:TextBox ID="txtTenderPublished" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Tender Closing / End Date</label>
                                                <asp:TextBox ID="txtTenderEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Technical Bid Opening Date</label>
                                                <asp:TextBox ID="txtTechnicalBidOpeningDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Bid Refrence No</label>
                                                <asp:TextBox ID="txtBidRefrenceNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Tender Document (RFP)</label>
                                                <asp:FileUpload ID="fl_Docs_Step5" runat="server" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Tender Document (eTender)</label>
                                                <asp:FileUpload ID="fl_Docs_Step5_A" runat="server" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Tender Cost (In Lakhs)</label>
                                                <asp:TextBox ID="txtTenderCost" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTenderCost_TextChanged" onkeyup="isNumericVal(this);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="grdPQC" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPQC_PreRender" ShowFooter="true" OnRowDataBound="grdPQC_RowDataBound">
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
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQC_Mandatory" HeaderText="ProjectDPRPQC_PQC_Mandatory">
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
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQCParent_Id" HeaderText="ProjectDPRPQC_PQCParent_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQC_Id" HeaderText="ProjectDPRPQC_PQC_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRPQC_Order" HeaderText="ProjectDPRPQC_Order">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Applicable">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkApplicable" runat="server" Checked="true"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQCParentName" HeaderText="Qualification Categery" />
                                                        <asp:TemplateField HeaderText="Qualification Criteria Details">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtPQCDetails" runat="server" CssClass="control-label no-padding-right" Text='<%#Eval("ProjectDPRPQC_PQCName") %>'></asp:Label>
                                                                <asp:TextBox ID="txtPQCDetails1" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_PQCName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAddPQC" OnClick="btnAddPQC_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                <span class="label label-danger arrowed">Click (+) To Add Additional Criteria / Special Conditions (If Any)</span>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Minimim Qualification">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMinValue" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_PQCMinVal") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnMinusPQC" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnMinusPQC_Click" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Maximum Qualification (If Any)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMaxValue" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_PQCMaxVal") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_Comments") %>' TextMode="MultiLine"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divStep6" visible="false">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Pre Bid Meeting On Date</label>
                                                <asp:TextBox ID="txtPreBidMeeting" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Corrigendum Document</label>
                                                <asp:FileUpload ID="fl_Docs_Step6" runat="server" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Corrigendum Refrence No</label>
                                                <asp:TextBox ID="txtCommentStep6" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Revised Closing / End Date</label>
                                                <asp:TextBox ID="txtTenderEndDateR" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Revised Technical Bid Opening Date</label>
                                                <asp:TextBox ID="txtTechnicalBidOpeningDateR" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnSkipCorrigendum" runat="server" CssClass="btn btn-danger" Text="Skip And Proceed Next >>" OnClick="btnSkipCorrigendum_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <h3 class="header smaller red">Pre Bid Meeting Response Documents (If Any)
                                </h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="grdPreBidResponseDocs" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPreBidResponseDocs_PreRender" ShowFooter="true" OnRowDataBound="grdPreBidResponseDocs_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Document Details">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDocumentDetails" runat="server" CssClass="control-label no-padding-right" Text='<%#Eval("ProjectDPRBidResponse_BidResponseName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAddResponse" OnClick="btnAddResponse_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Document">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flResponseDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnMinusResponse" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnMinusResponse_Click" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="grdQualificationCriteriaR" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdQualificationCriteriaR_PreRender" ShowFooter="true" OnRowDataBound="grdQualificationCriteriaR_RowDataBound">
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
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQC_Mandatory" HeaderText="ProjectDPRPQC_PQC_Mandatory">
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
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQCParent_Id" HeaderText="ProjectDPRPQC_PQCParent_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRPQC_PQC_Id" HeaderText="ProjectDPRPQC_PQC_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRPQC_Order" HeaderText="ProjectDPRPQC_Order">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qualification Criteria Details">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtPQCDetails" runat="server" CssClass="control-label no-padding-right" Text='<%#Eval("ProjectDPRPQC_PQCName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                <asp:ImageButton ID="btnAddPQCR" OnClick="btnAddPQCR_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                            </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Minimim Qualification">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMinValue" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_PQCMinVal") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                                                <asp:ImageButton ID="btnMinusPQCR" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnMinusPQCR_Click" Width="30px" Height="30px" />
                                                            </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Maximum Qualification (If Any)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMaxValue" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_PQCMaxVal") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQC_Comments") %>' TextMode="MultiLine"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" id="divStep7" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Tender Closing Date</label>
                                            <asp:TextBox ID="txtTenderClosingDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step7" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Comments</label>
                                            <asp:TextBox ID="txtCommentStep7" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divStep8" visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Technical Bid Opening Date</label>
                                                        <asp:TextBox ID="txtTechnicalBidOpening" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">
                                                            Upload Bid Opening Document (From e-tender Portal)
                                                        </label>
                                                        <asp:FileUpload ID="fl_Docs_Step8" runat="server" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnTenderCancelled2" runat="server" CssClass="btn btn-danger" Text="Tender Cancelled >>" OnClick="btnTenderCancelled2_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">
                                                            Reason For Tender Cancellation
                                                        </label>
                                                        <asp:TextBox ID="txtTenderCancellationReason2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <asp:GridView ID="grdNGTDtls" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdNGTDtls_PreRender" ShowFooter="true" OnRowDataBound="grdNGTDtls_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectDPRBidder_Id" HeaderText="ProjectDPRBidder_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRBidder_DPR_Id" HeaderText="ProjectDPRBidder_DPR_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRBidder_TechnicalQualified" HeaderText="ProjectDPRBidder_TechnicalQualified">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRBidder_FinancialQualified" HeaderText="ProjectDPRBidder_FinancialQualified">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRBidder_Is_JV" HeaderText="ProjectDPRBidder_Is_JV">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRBidder_BidderGSTIN_Available" HeaderText="ProjectDPRBidder_BidderGSTIN_Available">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectDPRBidder_BidderGSTINP_Available" HeaderText="ProjectDPRBidder_BidderGSTINP_Available">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Bidder">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is JV">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="ddlISJV" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="ddlISJV_SelectedIndexChanged">
                                                                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bidder Details">
                                                            <ItemTemplate>
                                                                <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Lead Bidder<asp:CheckBox runat="server" ID="chkGSTNA" ToolTip="GST Not Available" AutoPostBack="true" OnCheckedChanged="chkGSTNA_CheckedChanged" /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFirmGSTIN" runat="server" CssClass="form-control" placeholder="GSTIN" Text='<%#Eval("ProjectDPRBidder_BidderGSTIN") %>' MaxLength="15" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtFirmGSTIN_TextChanged"></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFirmName" runat="server" CssClass="form-control" placeholder="Bidder Firm Name" Text='<%#Eval("ProjectDPRBidder_BidderName") %>'></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFirmPAN" runat="server" CssClass="form-control" MaxLength="10" placeholder="PAN Card No" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderPAN") %>'></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Mobile No" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderMobile") %>' MaxLength="10"></asp:TextBox></td>
                                                                            <td runat="server" id="tdShare" visible="false">
                                                                                <asp:TextBox ID="txtShare" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" placeholder="Share > 51%" Text='<%#Eval("ProjectDPRBidder_BidderShare") %>' MaxLength="4"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr runat="server" id="trPartnerBidder" visible="false">
                                                                            <td>Partner Bidder<asp:CheckBox runat="server" ID="chkGSTNAP" ToolTip="GST Not Available" AutoPostBack="true" OnCheckedChanged="chkGSTNAP_CheckedChanged" /></td>
                                                                            <td>

                                                                                <asp:TextBox ID="txtFirmGSTINP" runat="server" CssClass="form-control" MaxLength="15" placeholder="GSTIN" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderGSTINP") %>' AutoPostBack="true" OnTextChanged="txtFirmGSTINP_TextChanged"></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFirmNameP" runat="server" CssClass="form-control" placeholder="Bidder Firm Name" Text='<%#Eval("ProjectDPRBidder_BidderNameP") %>'></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFirmPANP" runat="server" CssClass="form-control" MaxLength="10" placeholder="PAN Card No" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderPANP") %>'></asp:TextBox></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContactNoP" runat="server" CssClass="form-control" placeholder="Mobile No" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderMobileP") %>' MaxLength="10"></asp:TextBox></td>
                                                                            <td runat="server" id="tdShareP" visible="false">
                                                                                <asp:TextBox ID="txtShareP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" placeholder="Share > 30%" Text='<%#Eval("ProjectDPRBidder_BidderShareP") %>' MaxLength="4"></asp:TextBox></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnAdd" OnClick="btnAdd_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" /></td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnMinus" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnMinus_Click" Width="30px" Height="30px" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divStep9" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Bidders Evaluation (Technical) Date:</label>
                                                    <asp:TextBox ID="txtBiddersEvaluation" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">
                                                        Upload Bid Technical Opening Document (From e-tender Portal)
                                                    </label>
                                                    <asp:FileUpload ID="fl_Docs_Step9" runat="server" CssClass="form-control"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnTenderCancelled1" runat="server" CssClass="btn btn-danger" Text="Tender Cancelled >>" OnClick="btnTenderCancelled1_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">
                                                        Reason For Tender Cancellation
                                                    </label>
                                                    <asp:TextBox ID="txtTenderCancellationReason1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdBidderDetails" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBidderDetails_PreRender" OnRowDataBound="grdBidderDetails_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRBidder_Id" HeaderText="ProjectDPRBidder_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_DPR_Id" HeaderText="ProjectDPRBidder_DPR_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_TechnicalQualified" HeaderText="ProjectDPRBidder_TechnicalQualified">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_FinancialQualified" HeaderText="ProjectDPRBidder_FinancialQualified">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Total_Response" HeaderText="Total_Response">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Total_Question" HeaderText="Total_Question">
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
                                                    <asp:TemplateField HeaderText="Is Technically Qualified?">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQualified" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRBidder_Comments") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fill Qualification Critertia">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnUpdatePQC" runat="server" Text="Open Form" OnClick="btnUpdatePQC_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Filled Qualification Critertia">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPQCView" runat="server" Text="Open Details" OnClick="btnPQCView_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="divStep9_PQC" visible="false">
                                    <div class="row" runat="server" id="divStepsCounter">
                                        <ul class="steps" style="margin-left: 0">
                                            <li data-step="1" class="active">
                                                <span class="step">1</span>
                                                <span class="title">Tender Fees</span>
                                            </li>
                                            <li data-step="2">
                                                <span class="step">2</span>
                                                <span class="title">EMD</span>
                                            </li>
                                            <li data-step="3">
                                                <span class="step">3</span>
                                                <span class="title">Net Worth</span>
                                            </li>
                                            <li data-step="4">
                                                <span class="step">4</span>
                                                <span class="title">Solvency</span>
                                            </li>
                                            <li data-step="5">
                                                <span class="step">5</span>
                                                <span class="title">Turnover</span>
                                            </li>
                                            <li data-step="6">
                                                <span class="step">6</span>
                                                <span class="title">Last 3 Years ITR</span>
                                            </li>
                                            <li data-step="7">
                                                <span class="step">7</span>
                                                <span class="title">BID Capacity</span>
                                            </li>
                                            <li data-step="8">
                                                <span class="step">8</span>
                                                <span class="title">Similar completed and commissioned Work</span>
                                            </li>
                                        </ul>
                                    </div>
                                    <div runat="server" id="divStep9_PQC_Work_Order" visible="false">
                                        <h3 class="header smaller red">Details Of Work Experience for Above Bidder
                                        </h3>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-12">
                                                    <asp:GridView ID="grdBidderOrder" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBidderOrder_PreRender" ShowFooter="true" OnRowDataBound="grdBidderOrder_RowDataBound">
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
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_BidderType" HeaderText="ProjectDPR_Bidder_Order_BidderType">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_Completed" HeaderText="ProjectDPR_Bidder_Order_Completed">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_OrderPath" HeaderText="ProjectDPR_Bidder_Order_OrderPath">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_VerificationPath" HeaderText="ProjectDPR_Bidder_Order_VerificationPath">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_VerificationLetterPath" HeaderText="ProjectDPR_Bidder_Order_VerificationLetterPath">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_Simmilar_Nature" HeaderText="ProjectDPR_Bidder_Order_Simmilar_Nature">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_VerificationLetterPath1" HeaderText="ProjectDPR_Bidder_Order_VerificationLetterPath1">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Bidder_Order_VerificationLetterPath2" HeaderText="ProjectDPR_Bidder_Order_VerificationLetterPath2">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bidder Type">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="rbtBidderType" runat="server">
                                                                        <asp:ListItem Selected="True" Value="Lead" Text="Lead"></asp:ListItem>
                                                                        <asp:ListItem Value="Partner" Text="JV"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name Of Work">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNameOfWork" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPR_Bidder_Order_Name_Of_Work") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="btnAddWork" OnClick="btnAddWork_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date(s)">
                                                                <ItemTemplate>
                                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Date of Start</label>
                                                                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%#Eval("ProjectDPR_Bidder_Order_StartDate") %>'></asp:TextBox>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Actual Date of Completion</label>
                                                                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%#Eval("ProjectDPR_Bidder_Order_EndDate") %>'></asp:TextBox>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Value Of work Done (Without GST) [In Lakh]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtOrderAmount" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPR_Bidder_Order_Amount") %>' onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Share (In Case of JV)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtJVShare" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPR_Bidder_Order_JV_Share") %>' onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Proportionate Value Of Work [In Lakh]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtJVContractValue" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPR_Bidder_Order_JV_Contract_Value") %>' onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged" ReadOnly="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Values In Lakh">
                                                                <ItemTemplate>
                                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Inflation (%)</label>
                                                                                        <asp:TextBox ID="txtInflation" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPR_Bidder_Order_Inflation") %>' onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged" ReadOnly="true"></asp:TextBox>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Value After Inflation</label>
                                                                                        <asp:TextBox ID="txtValueAfterInflation" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPR_Bidder_Order_Amount_After_Inflation") %>' onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged" ReadOnly="true"></asp:TextBox>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Work of Similar Nature">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="ddlSimmilarNature" runat="server" RepeatDirection="Vertical">
                                                                        <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Completed & Commissioned">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="ddlCompleted" runat="server" RepeatDirection="Vertical">
                                                                        <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Upload Documents">
                                                                <ItemTemplate>
                                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Work Experience</label>
                                                                                        <asp:FileUpload ID="flWorkOrder" runat="server" CssClass="form-control"></asp:FileUpload>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Letter Sent For Verification</label>
                                                                                        <asp:FileUpload ID="flVerificationLetter" runat="server" CssClass="form-control"></asp:FileUpload>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Letter Date For Verification Request</label>
                                                                                        <asp:TextBox ID="txtVerificationLetter" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" runat="server" Text='<%#Eval("ProjectDPR_Bidder_Order_VerificationLetterDate") %>'></asp:TextBox>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label no-padding-right">Copy Of Exp Verification Document</label>
                                                                                        <asp:FileUpload ID="flOrderVerification" runat="server" CssClass="form-control"></asp:FileUpload>
                                                                                    </div>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <label class="control-label no-padding-right">Reminder For Verification</label>
                                                                                    <asp:RadioButtonList runat="server" ID="rbtLetterReminder" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Text="Reminder 1" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="Reminder 2" Value="2"></asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="btnMinusWork" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnMinusWork_Click" Width="30px" Height="30px" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnSaveBidderOrder" runat="server" CssClass="btn btn-warning" OnClick="btnSaveBidderOrder_Click" Text="Save Bidder Work Order"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="col-md-12">
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPQCAnswer" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPQCAnswer_PreRender" OnRowDataBound="grdPQCAnswer_RowDataBound">
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
                                                            <asp:BoundField DataField="ProjectDPRPQC_PQC_Mandatory" HeaderText="ProjectDPRPQC_PQC_Mandatory">
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
                                                            <asp:BoundField DataField="ProjectDPRPQC_PQCParent_Id" HeaderText="ProjectDPRPQC_PQCParent_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPRPQC_PQC_Id" HeaderText="ProjectDPRPQC_PQC_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPRPQCResponse_FilePathV1" HeaderText="ProjectDPRPQCResponse_FilePathV1">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPRPQCResponse_FilePathV2" HeaderText="ProjectDPRPQCResponse_FilePathV2">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPRPQCResponse_FilePathV3" HeaderText="ProjectDPRPQCResponse_FilePathV3">
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
                                                            <asp:BoundField DataField="ProjectDPRPQC_PQCMaxVal" HeaderText="Max Value" Visible="false" />
                                                            <asp:BoundField DataField="ProjectDPRPQC_Comments" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Bidder Value">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBidderValue" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRPQCResponse_Response") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:CheckBox runat="server" ID="chkBidderValueNA" AutoPostBack="true" Text="Bidder Value Not Submitted" OnCheckedChanged="chkBidderValueNA_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Response">
                                                                <ItemTemplate>
                                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top" runat="server" id="tbl_Response">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Upload Relavent Doc</th>
                                                                                <th>Document Verification Done?</th>
                                                                                <th>Upluad Verification Doc</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:FileUpload ID="flDocs1" runat="server" CssClass="form-control"></asp:FileUpload></td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkVerified1" runat="server"></asp:CheckBox></td>
                                                                                <td>
                                                                                    <asp:FileUpload ID="flVDocs1" runat="server" CssClass="form-control"></asp:FileUpload></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:FileUpload ID="flDocs2" runat="server" CssClass="form-control"></asp:FileUpload></td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkVerified2" runat="server"></asp:CheckBox></td>
                                                                                <td>
                                                                                    <asp:FileUpload ID="flVDocs2" runat="server" CssClass="form-control"></asp:FileUpload></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:FileUpload ID="flDocs3" runat="server" CssClass="form-control"></asp:FileUpload></td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkVerified3" runat="server"></asp:CheckBox></td>
                                                                                <td>
                                                                                    <asp:FileUpload ID="flVDocs3" runat="server" CssClass="form-control"></asp:FileUpload></td>
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
                                                                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="RTGS / NEFT" Text="RTGS / NEFT"></asp:ListItem>
                                                                                        <asp:ListItem Value="BG" Text="BG"></asp:ListItem>
                                                                                        <asp:ListItem Value="FDR" Text="FDR"></asp:ListItem>
                                                                                        <asp:ListItem Value="UDIN No" Text="UDIN No"></asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRefNo" CssClass="form-control" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_RefrenceNo") %>'></asp:TextBox></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBankName" CssClass="form-control" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_BankName") %>'></asp:TextBox></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRefDate" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" runat="server" Text='<%#Eval("ProjectDPRPQCResponse_RefDate") %>'></asp:TextBox>
                                                                                </td>
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
                                        <div class="col-xs-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chkOrderNotAvailable" runat="server" Text="No Work Order Submitted By Bidder"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="btnEvaluate" runat="server" CssClass="btn btn-danger" OnClick="btnEvaluate_Click" Text="Evaluate"></asp:Button>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="btnSaveBidAnswer" runat="server" CssClass="btn btn-warning" OnClick="btnSaveBidAnswer_Click" Text="Save Qualification Criteria Details" Visible="false"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divStep10" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Financial Bid Opening Date</label>
                                                    <asp:TextBox ID="txtFinancialBidOpeningDateF" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">
                                                        Upload Letter
                                                    </label>
                                                    <asp:FileUpload ID="fl_Docs_Step10" runat="server" CssClass="form-control"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnTenderCancelled3" runat="server" CssClass="btn btn-danger" Text="Tender Cancelled >>" OnClick="btnTenderCancelled3_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">
                                                        Reason For Tender Cancellation
                                                    </label>
                                                    <asp:TextBox ID="txtTenderCancellationReason3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdBiddersFinancial" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBidderDetails_PreRender" OnRowDataBound="grdBiddersFinancial_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectDPRBidder_Id" HeaderText="ProjectDPRBidder_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_DPR_Id" HeaderText="ProjectDPRBidder_DPR_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_TechnicalQualified" HeaderText="ProjectDPRBidder_TechnicalQualified">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_FinancialQualified" HeaderText="ProjectDPRBidder_FinancialQualified">
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
                                                    <asp:TemplateField HeaderText="Quoted Rate (In Lakhs)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuotedRate" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRBidder_BidderAmount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectDPRBidder_TechnicalQualified" HeaderText="Technical Qualified" />
                                                    <asp:TemplateField HeaderText="Is Financially Qualified?">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQualified" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRBidder_Comments") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divStep11" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Date of Dispatch</label>
                                            <asp:TextBox ID="txtSMDDispatchDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step11" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Comments</label>
                                            <asp:TextBox ID="txtCommentStep11" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divStep12" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Date of SLTC</label>
                                            <asp:TextBox ID="txtDateSLTC" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step12" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Comments</label>
                                            <asp:TextBox ID="txtCommentStep12" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" id="divSkipSLTC">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSkipSLTC" runat="server" CssClass="btn btn-danger" Text="Skip And Proceed Next >>" OnClick="btnSkipSLTC_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divStep13" visible="false">
                                <div class="col-xs-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Work Order Date</label>
                                            <asp:TextBox ID="txtWorkOrderDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Document</label>
                                            <asp:FileUpload ID="fl_Docs_Step13" runat="server" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Comments</label>
                                            <asp:TextBox ID="txtCommentStep13" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divStep14" visible="false">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Agreement Date</label>
                                                <asp:TextBox ID="txtAgreementDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Document</label>
                                                <asp:FileUpload ID="fl_Docs_Step14" runat="server" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Agreement Number</label>
                                                <asp:TextBox ID="txtAgreementNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
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
                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">Timeline Analysis 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdTimeLine" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdTimeLine_PreRender">
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

                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">Response Submitted For The Bidder 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdPQCAnswerView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPQCAnswerView_PreRender" OnRowDataBound="grdPQCAnswerView_RowDataBound">
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
                                                <asp:BoundField DataField="ProjectDPRPQC_PQCMaxVal" HeaderText="Max Value" Visible="false" />
                                                <asp:BoundField DataField="ProjectDPRPQC_Comments" HeaderText="Comments" />
                                                <asp:BoundField DataField="ProjectDPRPQCResponse_Response" HeaderText="Bidder Value" />
                                                <asp:TemplateField HeaderText="Response">
                                                    <ItemTemplate>
                                                        <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top" runat="server" id="tbl_Response">
                                                            <thead>
                                                                <tr>
                                                                    <th>Uploaded Doc</th>
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
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose2" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                    <asp:HiddenField ID="hf_BidderType" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectDPRBidder_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectDPR_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnSaveBidAnswer" />
                    <asp:PostBackTrigger ControlID="btnSaveBidderOrder" />
                    <asp:PostBackTrigger ControlID="btnTenderCancelled1" />
                    <asp:PostBackTrigger ControlID="btnTenderCancelled2" />
                    <asp:PostBackTrigger ControlID="btnTenderCancelled3" />
                </Triggers>
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
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
                                    ],
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

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });

        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }

        function calculateTotal() {
            debugger;
            var txtGOTotalShare = 0;
            var txtGOCentralShare = 0;
            var txtGOStateShare = 0;
            var txtGOULBShare = 0;
            var txtGOCentage = 0;

            try {
                txtGOCentralShare = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtGOCentralShare").value);
            }
            catch
            {
                txtGOCentralShare = 0;
            }

            try {
                txtGOStateShare = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtGOStateShare").value);
            }
            catch
            {
                txtGOStateShare = 0;
            }

            try {
                txtGOULBShare = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtGOULBShare").value);
            }
            catch
            {
                txtGOULBShare = 0;
            }

            try {
                txtGOCentage = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtGOCentage").value);
            }
            catch
            {
                txtGOCentage = 0;
            }

            if (isNaN(txtGOCentralShare)) {
                txtGOCentralShare = 0;
            }
            if (isNaN(txtGOStateShare)) {
                txtGOStateShare = 0;
            }
            if (isNaN(txtGOULBShare)) {
                txtGOULBShare = 0;
            }
            if (isNaN(txtGOCentage)) {
                txtGOCentage = 0;
            }
            txtGOTotalShare = txtGOCentralShare + txtGOStateShare + txtGOULBShare + txtGOCentage;
            document.getElementById("ctl00_ContentPlaceHolder1_txtGOTotalShare").value = txtGOTotalShare;
        }
    </script>

    <script>
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
    </script>
</asp:Content>



