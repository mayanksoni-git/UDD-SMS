<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterGenerateInvoice_Detail.aspx.cs" Inherits="MasterGenerateInvoice_Detail" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpViewCoverLetter" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpViewSummery" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpViewPaymentOrder" runat="server" PopupControlID="Panel5" TargetControlID="btnShowPopup5"
                        CancelControlID="btnclose5" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup5" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpTimeLine" runat="server" PopupControlID="Panel4" TargetControlID="btnShowPopup4"
                        CancelControlID="btnclose4" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup4" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Details Of Invoice Items
                               <div class="form-group" style="float: right; padding-right: 10px">
                                   <div class="row">
                                       <div class="col-sm-12">
                                           <div class="col-md-3">
                                               <img src="assets/images/rupee.png" width="20px" height="25px" />
                                           </div>
                                           <div class="col-md-3">
                                               <div class="price" style="font-weight: bold" id="divBalance" runat="server">
                                                   0.00														
                                               </div>
                                           </div>
                                           <div class="col-md-3">
                                               <small><b>INR</b></small>
                                           </div>
                                       </div>
                                       <small id="divAccountNo" runat="server"><b>Account No: 0294***110***2*7</b></small>
                                   </div>
                               </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdInvoice" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdInvoice_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="PackageInvoice_Id" HeaderText="PackageInvoice_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageInvoice_ProcessType" HeaderText="PackageInvoice_ProcessType">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:ImageButton ID="btnOpenTimeline1" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimeline1_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnOpenInvoice" Width="20px" Height="20px" OnClick="btnOpenInvoice_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="List_EMBNo" HeaderText="EMB No" />
                                                <asp:BoundField DataField="PackageInvoice_Date" HeaderText="Invoice Date" />
                                                <asp:BoundField DataField="PackageInvoice_VoucherNo" HeaderText="Voucher No" />
                                                <asp:BoundField DataField="Total_Line_Items" HeaderText="Total Line Items" />
                                                <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                <asp:BoundField DataField="Total_Amount_D" HeaderText="Deduction" />
                                                <asp:BoundField DataField="Total_Amount_F" HeaderText="Total Amount" />
                                                <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                <asp:BoundField DataField="OfficeBranch_Name" HeaderText="Pending at Organization" />
                                                <asp:BoundField DataField="Invoice_Status" HeaderText="Current Status" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus" HeaderText="Reason (If Any)" />
                                                <asp:BoundField DataField="List_EMBMaster_Id" HeaderText="List_EMBMaster_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Invoice">
                                                    <ItemTemplate>
                                                        <a href="MasterGenerateInvoice_View.aspx?Package_Id=0&Invoice_Id=<%# Eval("PackageInvoice_Id") %>" target="_blank">View Invoice</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View EMB">
                                                    <ItemTemplate>
                                                        <a href="ViewEMBDetails.aspx?Invoice_Id=<%# Eval("PackageInvoice_Id") %>" target="_blank">View EMB</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View BOQ">
                                                    <ItemTemplate>
                                                        <a href="View_BOQ_Details.aspx?Package_Id=<%# Eval("PackageInvoice_Package_Id") %>" target="_blank">View BOQ</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PackageInvoice_Type" HeaderText="PackageInvoice_Type">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="space-6"></div>

                        <hr />
                        <div id="divEntry" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPackageDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_ULB_Id" HeaderText="ProjectWork_ULB_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="" HeaderText="">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Project" DataField="Project_Name" />
                                                    <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                    <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                    <asp:BoundField HeaderText="Budget" DataField="ProjectWork_Budget" />
                                                    <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                    <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                    <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                    <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                    <asp:BoundField HeaderText="Due Date Of Completion" DataField="ProjectWorkPkg_Due_Date" />
                                                    <asp:BoundField HeaderText="Vendor / Contractor" DataField="Vendor_Name" />
                                                    <asp:BoundField HeaderText="Vendor / Contractor (Mobile)" DataField="Vendor_Mobile" />
                                                    <asp:BoundField HeaderText="Reporting Staff (JE / APE)" DataField="List_ReportingStaff_JEAPE_Name" />
                                                    <asp:BoundField HeaderText="Reporting Staff (AE / PE)" DataField="List_ReportingStaff_AEPE_Name" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-12 infobox-container">
                                    <div class="infobox infobox-grey infobox-small infobox-dark">

                                        <a class="infobox-icon" target="_blank" href="#" onclick="openDoc(this, 'CB');">
                                            <i class="ace-icon fa fa-download"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Contract</div>
                                            <div class="infobox-content">Bond</div>
                                        </div>
                                    </div>

                                    <div class="infobox infobox-grey infobox-small infobox-dark">
                                        <a class="infobox-icon" target="_blank" href="#" onclick="openDoc(this, 'PS');">
                                            <i class="ace-icon fa fa-download"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Performance</div>
                                            <div class="infobox-content">Security</div>
                                        </div>
                                    </div>

                                    <div class="infobox infobox-grey infobox-small infobox-dark">
                                        <a class="infobox-icon" target="_blank" href="#" onclick="openDoc(this, 'BG');">
                                            <i class="ace-icon fa fa-download"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Bank</div>
                                            <div class="infobox-content">Guarantee</div>
                                        </div>
                                    </div>

                                    <div class="infobox infobox-grey infobox-small infobox-dark">
                                        <a class="infobox-icon" runat="server" target="_blank" href="#" onclick="openDoc(this, 'MA');">
                                            <i class="ace-icon fa fa-download"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Mobilization</div>
                                            <div class="infobox-content">Advance</div>
                                        </div>
                                    </div>

                                    <div class="infobox infobox-blue infobox-small infobox-dark">
                                        <a class="infobox-icon" runat="server" id="lnk_CoverLetter" onserverclick="lnk_CoverLetter_ServerClick">
                                            <i class="ace-icon fa fa-envelope-o"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Cover </div>
                                            <div class="infobox-content">Letter</div>
                                        </div>
                                    </div>

                                    <div class="infobox infobox-green infobox-small infobox-dark">
                                        <a class="infobox-icon" runat="server" id="lnk_PaymentSummery" onserverclick="lnk_PaymentSummery_ServerClick">
                                            <i class="ace-icon fa fa-rupee"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Payment</div>
                                            <div class="infobox-content">Summery</div>
                                        </div>
                                    </div>

                                    <div class="infobox infobox-green infobox-small infobox-dark">
                                        <a class="infobox-icon" runat="server" id="link_PaymentOrder" onserverclick="link_PaymentOrder_ServerClick">
                                            <i class="ace-icon fa fa-rupee"></i>
                                        </a>

                                        <div class="infobox-data">
                                            <div class="infobox-content">Payment</div>
                                            <div class="infobox-content">Order</div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdProjectStatus" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProjectStatus_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWorkPkg_Work_Id" HeaderText="ProjectWorkPkg_Work_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Overall_Status" HeaderText="Overall_Status">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Data For" DataField="Type" />
                                                <asp:BoundField HeaderText="Budget" DataField="Budget" />
                                                <asp:BoundField HeaderText="Agreement Amount" DataField="AgreementAmount" />
                                                <asp:BoundField HeaderText="Total Packages" DataField="Total_Packages" />
                                                <asp:BoundField HeaderText="Central Share" DataField="Central_Share" />
                                                <asp:BoundField HeaderText="State Share" DataField="State_Share" />
                                                <asp:BoundField HeaderText="ULB Share" DataField="ULB_Share" />
                                                <asp:BoundField HeaderText="Total Invoice" DataField="Total_Invoice" />
                                                <asp:BoundField HeaderText="Total Invoice Value" DataField="Total_Value" />
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdBOQ" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBOQ_PreRender" OnRowDataBound="grdBOQ_RowDataBound" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="PackageInvoiceItem_Id" HeaderText="PackageInvoiceItem_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceItem_Invoice_Id" HeaderText="PackageInvoiceItem_Invoice_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMB_Unit_Id" HeaderText="PackageEMB_Unit_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceItem_PackageEMB_Id" HeaderText="PackageInvoiceItem_PackageEMB_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="BOQ Sr.No." DataField="PackageBOQ_Sr_No" />
                                                    <asp:TemplateField HeaderText="Specification / Description" HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                                        FooterStyle-Width="40%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecification" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageEMB_Specification") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Unit" DataField="Unit_Name" />
                                                    <asp:BoundField HeaderText="Quantity as per BOQ" DataField="PackageInvoiceItem_Total_Qty" />
                                                    <asp:BoundField HeaderText="Rate" DataField="Total_Rate" />
                                                    <asp:TemplateField HeaderText="Quantity To Be Released">
                                                        <ItemTemplate>
                                                            <table class="display table table-bordered">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="">
                                                                            <asp:TextBox ID="lblQty" runat="server" Text='<%# Eval("PackageInvoiceItem_Total_Qty_BOQ") %>' AutoPostBack="true" OnTextChanged="lblQty_TextChanged" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="">
                                                                            <asp:Label ID="lblQtyExtra" runat="server" CssClass="label label-danger arrowed" Font-Bold="true" Text='<%# Eval("PackageInvoiceItem_QtyExtra") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="">
                                                                            <asp:Label ID="lblQtyExtraPer" runat="server" CssClass="label label-danger arrowed" Font-Bold="true" Text='<%# Eval("PackageInvoiceItem_QtyExtraPer") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Percentage Amount To Be Released">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblPercentageToBeReleased" runat="server" Text='<%# Eval("PackageInvoiceItem_PercentageToBeReleased") %>' AutoPostBack="true" OnTextChanged="lblQty_TextChanged" Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                                    <asp:BoundField DataField="PackageInvoiceItem_RateQuoted" HeaderText="Quoted Rate">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceItem_RateEstimated" HeaderText="Estimated Rate">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceItem_Total_Qty" HeaderText="Current Quantity">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Taxes / (%)" HeaderStyle-Width="250px" ItemStyle-Width="250px">
                                                        <HeaderStyle Width="250px" />
                                                        <ItemStyle Width="250px" />
                                                        <ItemTemplate>
                                                            <asp:GridView ID="grdTaxes" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" OnPreRender="grdTaxes_PreRender" ShowHeader="false" OnRowDataBound="grdTaxes_RowDataBound" Width="250px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Deduction_Id" HeaderText="Deduction_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PackageInvoiceItem_Tax_Deduction_Id" HeaderText="PackageInvoiceItem_Tax_Deduction_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlTaxes" runat="server" CssClass="form-control" Width="80px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtTaxesP" runat="server" CssClass="form-control" MaxLength="10" onkeyup="isNumericVal(this);" Text='<%# Eval("PackageInvoiceItem_Tax_Value") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmountQuoted" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("Total_Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Width="100px" TextMode="MultiLine" Text='<%# Eval("PackageInvoiceItem_Remarks") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDeductionsM" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true" OnRowDataBound="grdDeductionsM_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="PackageInvoiceAdditional2_Deduction_Id" HeaderText="PackageInvoiceAdditional2_Deduction_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Deduction" DataField="Deduction_Name" />
                                                <asp:TemplateField HeaderText="Deduction Type">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rblDeductionType" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="%" Value="Per" Selected="True" />
                                                            <%--<asp:ListItem Text="Flat" Value="Flat" />--%>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduction Value">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeductionValue" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control" Text='<%# Eval("PackageInvoiceAdditional2_Deduction_Value_Master") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                                <asp:BoundField HeaderText="Total Amount" DataField="Total" />
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="tabbable">
                                        <ul class="nav nav-tabs" id="myTab">
                                            <li class="active">
                                                <a data-toggle="tab" href="#home" aria-expanded="true">
                                                    <i class="green ace-icon fa fa-home bigger-120"></i>
                                                    Deductions
                                                </a>
                                            </li>
                                            <li class="">
                                                <a data-toggle="tab" href="#messages" aria-expanded="false">Additions
                                                </a>
                                            </li>

                                            <li class="">
                                                <a data-toggle="tab" href="#qty" aria-expanded="false">Quantity Variation Document
                                                </a>
                                            </li>
                                        </ul>

                                        <div class="tab-content">
                                            <div id="home" class="tab-pane fade active in">
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdDeductions" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductions_PreRender" ShowFooter="true" OnRowDataBound="grdDeductions_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="Deduction_Id" HeaderText="Deduction_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PackageInvoiceAdditional_Id" HeaderText="PackageInvoiceAdditional_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PackageInvoiceAdditional_Deduction_isFlat" HeaderText="PackageInvoiceAdditional_Deduction_isFlat">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Deduction_Type" HeaderText="Deduction_Type">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Category" DataField="Deduction_Category" />
                                                            <asp:BoundField HeaderText="Deduction" DataField="Deduction_Name" />
                                                            <asp:BoundField HeaderText="Deduction Type" DataField="Deduction_Type" />
                                                            <asp:TemplateField HeaderText="Deduction Value (%)">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkFlat" AutoPostBack="true" OnCheckedChanged="chkFlat_CheckedChanged" ToolTip="Select This Checkbox for Flat Value" runat="server" />

                                                                    <asp:TextBox ID="txtDeductionValue" runat="server" CssClass="form-control" Width="120px" Text='<%# Eval("Deduction_Value") %>' OnTextChanged="txtDeductionValue_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Deduction Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDeductionAmount" runat="server" CssClass="form-control" Width="120px" Text='<%# Eval("PackageInvoiceAdditional_Deduction_Value_Final") %>' AutoPostBack="true" OnTextChanged="txtDeductionAmount_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Comments">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDeductionComments" runat="server" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("PackageInvoiceAdditional_Comments") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div id="messages" class="tab-pane fade">
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdDeductions2" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductions2_PreRender" ShowFooter="true" OnRowDataBound="grdDeductions2_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="Deduction_Id" HeaderText="Deduction_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PackageInvoiceAdditional_Id" HeaderText="PackageInvoiceAdditional_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PackageInvoiceAdditional_Deduction_isFlat" HeaderText="PackageInvoiceAdditional_Deduction_isFlat">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Deduction_Type" HeaderText="Deduction_Type">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect2" AutoPostBack="true" OnCheckedChanged="chkSelect2_CheckedChanged" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Category" DataField="Deduction_Category" />
                                                            <asp:BoundField HeaderText="Deduction" DataField="Deduction_Name" />
                                                            <asp:BoundField HeaderText="Deduction Type" DataField="Deduction_Type" />
                                                            <asp:TemplateField HeaderText="Deduction Value (%)">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkFlat2" AutoPostBack="true" OnCheckedChanged="chkFlat2_CheckedChanged" ToolTip="Select This Checkbox for Flat Value" runat="server" />

                                                                    <asp:TextBox ID="txtDeductionValue2" runat="server" CssClass="form-control" Width="120px" Text='<%# Eval("Deduction_Value") %>' OnTextChanged="txtDeductionValue2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Deduction Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDeductionAmount2" runat="server" CssClass="form-control" Width="120px" Text='<%# Eval("PackageInvoiceAdditional_Deduction_Value_Final") %>' AutoPostBack="true" OnTextChanged="txtDeductionAmount2_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Comments">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDeductionComments2" runat="server" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("PackageInvoiceAdditional_Comments") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                            <div id="qty" class="tab-pane fade">
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdExtraItemApprove" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdExtraItemApprove_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="PackageEMB_ExtraItem_ApprovalFilePath" HeaderText="PackageEMB_ExtraItem_ApprovalFilePath">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Download Approval Attachment">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAgreementFile2" runat="server" OnClick="lnkAgreementFile2_Click" Text="File"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Approve No" DataField="PackageEMB_ExtraItem_ApproveNo" />
                                                            <asp:BoundField HeaderText="Approve Date" DataField="PackageEMB_ExtraItem_ApproveDate" />
                                                            <asp:BoundField HeaderText="Comment" DataField="PackageEMB_ExtraItem_Comment" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Comments and Approvals
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Status</label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Total Amount (Invoice Total - Deductions + Additions)</label>
                                            <br />
                                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="label label-xlg label-inverse arrowed arrowed-right"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="false" id="divAmountTransfered">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Total Amount Transfered</label>
                                            <asp:TextBox ID="txtFudTransfered" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="false" id="divPPA">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">PPA No Generated On PFMS:</label>
                                            <asp:TextBox ID="txtPPANo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPPANo_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divPPAMessage" runat="server">
                                <div class="col-md-12">
                                    <asp:Label ID="lblPPAMessage" runat="server" CssClass="label label-danger arrowed" Text="Please Input A Valid PPA No and Amount Of The Generated PPA through PFMA will be same as Invoice Amount Excluding Deduction."></asp:Label>
                                </div>
                            </div>
                            <div class="row" id="divPPAVerification" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdPPAVerification" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Documents Configured To Upload" OnPreRender="grdPPAVerification_PreRender">
                                        <Columns>
                                            <asp:BoundField HeaderText="PFMS Batch Number" DataField="PFMS_Batch_Number" />
                                            <asp:BoundField HeaderText="Amount" DataField="Batch_Amount" />
                                            <asp:BoundField HeaderText="Debit AC No" DataField="Debit_Account_Number" />
                                            <asp:BoundField HeaderText="PPA Received On" DataField="File_Received_Date" />
                                            <asp:BoundField HeaderText="PPA Expiry Date" DataField="Expiry_Date" />
                                            <asp:BoundField HeaderText="Batch Status" DataField="Batch_Status" />
                                            <asp:BoundField HeaderText="Failure Reason Code" DataField="Failure_Reason_Code" />
                                            <asp:BoundField HeaderText="Failure Reason Description" DataField="Failure_Reason_Description" />
                                            <asp:BoundField HeaderText="Response Status" DataField="Response_Status" />
                                            <asp:BoundField HeaderText="CBSTxnId" DataField="CBSTxnId" />
                                            <asp:BoundField HeaderText="CBSTranDate" DataField="CBSTranDate" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <hr />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="tabbable">
                                        <ul class="nav nav-tabs" id="myTab2">
                                            <li class="active">
                                                <a data-toggle="tab" href="#doc1" aria-expanded="true">
                                                    <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                    Upload Document (If Any)
                                                </a>
                                            </li>

                                            <li class="">
                                                <a data-toggle="tab" href="#doc2" aria-expanded="false">
                                                    <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                    Download / View Previous Uploaded Document
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="form-group form-check" style="float: right; padding-right: 10px">
                                            <asp:CheckBox ID="chkUpdateExisting" runat="server" Text="Update Existing Uploaded Document" />
                                        </div>
                                        <div class="tab-content">
                                            <div id="doc1" class="tab-pane fade active in">
                                                <asp:GridView ID="grdDocumentMaster" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Documents Configured To Upload" OnPreRender="grdDocumentMaster_PreRender" OnRowDataBound="grdDocumentMaster_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="TradeDocument_Id" HeaderText="TradeDocument_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProcessConfigDocumentLinking_DocumentMaster_Id" HeaderText="ProcessConfigDocumentLinking_DocumentMaster_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Document To Upload" DataField="TradeDocument_Name" />
                                                        <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDocumentOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select PDF File To Attach">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUpload" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDocumentComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div id="doc2" class="tab-pane fade">
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdMultipleFiles" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdMultipleFiles_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="PackageInvoiceDocs_FileName" HeaderText="PackageInvoiceDocs_FileName">
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
                                                            <asp:BoundField HeaderText="Order No" DataField="PackageInvoiceDocs_OrderNo" />
                                                            <asp:BoundField HeaderText="Comments" DataField="PackageInvoiceDocs_Comments" />
                                                            <asp:TemplateField HeaderText="Download">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("PackageInvoiceDocs_FileName") %>' OnClientClick="return downloadFile(this);"></asp:LinkButton>
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

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6" id="divAdditionalReason" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Status</label>
                                            <asp:DropDownList ID="ddlAdditionalReason" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="---Select---" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Funds Not Available In This Project"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="UC Not Received"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="ULB Share Not Received"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Mismatch Of Account"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Document Not Proper"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Mismatch Of Cover Letter Details"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="Others"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Reason / Comments</label>
                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnApproveInvoice" Text="Approve Invoice" OnClick="btnApproveInvoice_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnRevert" Text="Revert To Division" OnClick="btnRevert_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnSaveInvoice" Text="Only Save Invoice" OnClick="btnSaveInvoice_Click" runat="server" CssClass="btn btn-warning"></asp:Button>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <iframe style="width: 910px; height: 600px;" id="Iframe1" src="BillPaymentSummeryView.aspx" runat="server"></iframe>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/assets/images/print.png" Width="50px" Height="60px" OnClientClick=" return Print();" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose1" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <iframe style="width: 910px; height: 600px;" id="Iframe2" src="BillPaymentOrderView.aspx" runat="server"></iframe>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/assets/images/print.png" Width="50px" Height="60px" OnClientClick=" return Print();" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose5" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <iframe style="width: 910px; height: 600px;" id="ifrm1" src="BillCoverLetterView.aspx" runat="server"></iframe>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="/assets/images/print.png" Width="50px" Height="60px" OnClientClick=" return Print();" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose2" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="700px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Document
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Literal ID="ltEmbed" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">Timeline Analysis 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdTimeLine" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdTimeLine_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="PackageEMBApproval_Id" HeaderText="PackageEMBApproval_Id">
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
                                                <asp:BoundField DataField="PackageEMBApproval_Status_Text" HeaderText="Action Taken" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Action Reason" />
                                                <asp:BoundField DataField="PackageEMBApproval_Comments" HeaderText="Comments (If Any)" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Action By (Designation)" />
                                                <asp:BoundField DataField="Person_Name" HeaderText="Action By (Name)" />
                                                <asp:BoundField DataField="Designation_Next" HeaderText="Next Action (Designation)" />
                                                <asp:BoundField DataField="PackageEMBApproval_AddedOn" HeaderText="Action Taken On" />
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
                                            <asp:Button ID="btnclose4" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_Invoice_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Doc_Agreement" runat="server" Value="" />
                    <asp:HiddenField ID="hf_Doc_Mobelization" runat="server" Value="" />
                    <asp:HiddenField ID="hf_Doc_BG" runat="server" Value="" />
                    <asp:HiddenField ID="hf_Doc_PerformanceSecurity" runat="server" Value="" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="" />
                    <asp:HiddenField ID="hf_Loop" runat="server" Value="" />
                    <asp:HiddenField ID="hf_PackageInvoice_ProcessType" runat="server" Value="" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveInvoice" />
                    <asp:PostBackTrigger ControlID="btnApproveInvoice" />
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
        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }

        function openDoc(obj, fileType) {
            var path = '';
            debugger;
            if (fileType == "CB") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_Agreement').value;
            }
            else if (fileType == "PS") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_PerformanceSecurity').value;
            }
            else if (fileType == "BG") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_BG').value;
            }
            else if (fileType == "MA") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_Mobelization').value;
            }
            else {
                path = "";
            }
            if (path != '') {
                obj.href = window.location.origin + path;
                window.open(window.location.origin + path, "_blank", "", false);
            }
            else {
                obj.href = "#";
                alert("No Document Found");
            }
            return false;
        }
    </script>
</asp:Content>





