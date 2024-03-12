<%@ Page Language="C#" MasterPageFile="~/TemplatePopup.master" AutoEventWireup="true" CodeFile="Jal_Prahari_Vendor.aspx.cs" Inherits="Jal_Prahari_Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpProjects" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpReplace" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2" CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpWorkOrder" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3" CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header" runat="server" id="divVendorName" style="font-weight: bold;">
                                    Vendor_Name
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <h1 id="divGSTIN" runat="server"></h1>
                                </div>
                                <div class="col-md-4">
                                    <h1 id="divPAN" runat="server"></h1>
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:DropDownList ID="ddlDPRListSearch" runat="server" class="chosen-select form-control" data-placeholder="Choose a DPR...">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-dark">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Participated As Lead Bidder</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkLeadBidder" runat="server" OnClick="lnkLeadBidder_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-blue">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Participated As Partner Bidder</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkPartnerBidder" OnClick="lnkPartnerBidder_Click" runat="server"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-green">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Qualified in Technical Evaluation 
                                        </h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkTechnical" runat="server" OnClick="lnkTechnical_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-red">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Qualified in Financial Evaluation
                                        </h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkFinancial" runat="server" OnClick="lnkFinancial_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Document Vault
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" ID="btnWorkOrder" Width="120px" Height="120px" ImageUrl="assets\images\jalprahari\Work_Order.png" OnClick="btnWorkOrder_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" ID="btnBalanceSheet" Width="120px" Height="120px" ImageUrl="assets\images\jalprahari\Balance_Sheet.png" OnClick="btnBalanceSheet_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" ID="btnITR" Width="120px" Height="120px" ImageUrl="assets\images\jalprahari\ITR.png" OnClick="btnITR_Click" />
                                </div>

                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" ID="btnNetWorth" Width="120px" Height="120px" ImageUrl="assets\images\jalprahari\Net_Worth.png" OnClick="btnNetWorth_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" ID="btnSolvency" Width="120px" Height="120px" ImageUrl="assets\images\jalprahari\Solvency.png" OnClick="btnSolvency_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" ID="btnBidCapacity" Width="120px" Height="120px" ImageUrl="assets\images\jalprahari\Bid_Capacity.png" OnClick="btnBidCapacity_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" visible="false" id="divWorkOrderAdd">
                            <div class="col-xs-12">
                                <div class="col-md-3 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnAddWorkOrder" runat="server" CssClass="btn btn-purple" OnClick="btnAddWorkOrder_Click" Text="Add New Work Order"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" visible="false" id="divWorkOrder">

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <!-- div.table-responsive -->
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Id" HeaderText="JalPrahariBidder_Order_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_OrderPath" HeaderText="JalPrahariBidder_Order_OrderPath">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_VerificationPath" HeaderText="JalPrahariBidder_Order_VerificationPath">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_VerificationLetterPath" HeaderText="JalPrahariBidder_Order_VerificationLetterPath">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Division_Name" HeaderText="Division" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_BidderType" HeaderText="Bidder Type" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Name_Of_Work" HeaderText="Name Of Work" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_StartDate" HeaderText="Date of Start" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_EndDate" HeaderText="Actual Date of Completion" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Amount" HeaderText="Total Value Of work Done (Without GST) [In Lakh]" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_JV_Share" HeaderText="% Share (In Case of JV)" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_JV_Contract_Value" HeaderText="Proportionate Value Of Work [In Lakh]" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Inflation" HeaderText="Inflation (%)" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Amount_After_Inflation" HeaderText="Value After Inflation [In Lakh]" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Simmilar_Nature" HeaderText="Is Work of Similar Nature" />
                                                        <asp:BoundField DataField="JalPrahariBidder_Order_Completed" HeaderText="Completed & Comissioned" />
                                                        <asp:TemplateField HeaderText="Download Order">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadWO" runat="server" Text="Download" GO_FilePath='<%#Eval("JalPrahariBidder_Order_OrderPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                <br />
                                                                <asp:LinkButton ID="lnkReplaceWO" runat="server" Text="Replace" ForeColor="Red" OnClick="lnkReplaceWO_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Verification">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadVer" runat="server" Text="Download" GO_FilePath='<%#Eval("JalPrahariBidder_Order_VerificationPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                <br />
                                                                <asp:LinkButton ID="lnkReplaceVer" runat="server" Text="Replace" ForeColor="Red" OnClick="lnkReplaceVer_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Verification Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadVerLetter" runat="server" Text="Download" GO_FilePath='<%#Eval("JalPrahariBidder_Order_VerificationLetterPath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                <br />
                                                                <asp:LinkButton ID="lnkReplaceVerLetter" runat="server" Text="Replace" ForeColor="Red" OnClick="lnkReplaceVerLetter_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDeleteWorkOrder" Width="20px" Height="30px" OnClick="btnDeleteWorkOrder_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Person_Name" HeaderText="Added By" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div runat="server" visible="false" id="divNetWorth">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <!-- div.table-responsive -->
                                            <div class="clearfix" id="Div2" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdNetWorth" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdNetWorth_PreRender">
                                                    <Columns>
                                                        <asp:BoundField DataField="JalPrahariBidderDoc_Id" HeaderText="JalPrahariBidderDoc_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="JalPrahariBidderDoc_BidderInfo_Id" HeaderText="JalPrahariBidderDoc_BidderInfo_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Division_Name" HeaderText="Division" />
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("JalPrahariBidderDoc_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                <br />
                                                                <asp:LinkButton ID="lnkReplace" runat="server" Text="Replace" ForeColor="Red" OnClick="lnkReplace_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDelete" Width="20px" Height="30px" OnClick="btnDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Person_Name" HeaderText="Added By" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hf_JalPrahariBidder_Order_Id" Value="0" />
                        <asp:HiddenField runat="server" ID="hf_FileType" Value="0" />
                        <asp:HiddenField runat="server" ID="hf_JalPrahariBidderDoc_Id" Value="0" />

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1200px; height: 90%; margin-left: -32px" ScrollBars="Auto">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdProjectsParticipated" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdProjectsParticipated_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectDPR_Id" HeaderText="ProjectDPR_Id">
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
                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                <asp:BoundField HeaderText="Project Type" DataField="ProjectType_Name" />
                                                <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                <asp:BoundField HeaderText="Capex Cost (In Lakhs)" DataField="ProjectDPR_CapexCost" />
                                                <asp:BoundField HeaderText="O & M  (In Lakhs)" DataField="ProjectDPR_OandM_Cost" />
                                                <asp:BoundField HeaderText="ACA Cost  (In Lakhs)" DataField="ProjectDPR_ACA_Cost" />
                                                <asp:BoundField HeaderText="Project Cost  (In Lakhs)" DataField="ProjectDPR_Project_Cost" />
                                                <asp:BoundField HeaderText="Project Code" DataField="ProjectDPR_Code" />
                                            </Columns>
                                        </asp:GridView>
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

                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 900px; height: 80%; margin-left: -32px" ScrollBars="Auto">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSelectFile" runat="server" Text="Select File To Replace" CssClass="control-label no-padding-right"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:FileUpload ID="flReplace" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnUpload" Text="Replace File" OnClick="btnUpload_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
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

                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 900px; height: 80%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">Details Of Work Experience for Above Bidder
                            </h3>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdBidderOrder" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBidderOrder_PreRender" ShowFooter="true" OnRowDataBound="grdBidderOrder_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name Of Work">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNameOfWork" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="btnAddWork" OnClick="btnAddWork_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date(s)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div class="form-group">
                                                                            <label class="control-label no-padding-right">Date of Start</label>
                                                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="form-group">
                                                                            <label class="control-label no-padding-right">Actual Date of Completion</label>
                                                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Value Of work Done (Without GST) [In Lakh]">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOrderAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="% Share (In Case of JV)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtJVShare" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proportionate Value Of Work [In Lakh]">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtJVContractValue" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged" ReadOnly="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Values In Lakh">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div class="form-group">
                                                                            <label class="control-label no-padding-right">Inflation (%)</label>
                                                                            <asp:TextBox ID="txtInflation" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="form-group">
                                                                            <label class="control-label no-padding-right">Value After Inflation</label>
                                                                            <asp:TextBox ID="txtValueAfterInflation" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="Calculate_TextChanged" ReadOnly="true"></asp:TextBox>
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
                                                <asp:TemplateField HeaderText="Select DPR">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlDPRList" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Upload Documents">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
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
                                                                            <asp:TextBox ID="txtVerificationLetter" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" runat="server"></asp:TextBox>
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
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnSaveBidderOrder" runat="server" CssClass="btn btn-warning" OnClick="btnSaveBidderOrder_Click" Text="Save Bidder Work Order"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose3" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
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
        <!-- /.main-content -->

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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                                    "iDisplayLength": 100,
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdNetWorth').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdNetWorth')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdNetWorth')
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
                                        null, null, null, null, null, null, null
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

                                    "iDisplayLength": 100,
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

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdNetWorth .dropdown-toggle', function (e) {
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

