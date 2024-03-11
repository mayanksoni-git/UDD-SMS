<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterGenerateInvoice_View.aspx.cs" Inherits="MasterGenerateInvoice_View"
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" enablepartialrendering="true"
                enablepagemethods="true" asyncpostbacktimeout="6000">
            </cc1:toolkitscriptmanager>
            <asp:UpdatePanel ID="up" runat="server">
                <contenttemplate>
                    <cc1:modalpopupextender id="mpViewBill" runat="server" popupcontrolid="Panel2" targetcontrolid="btnShowPopup2"
                        cancelcontrolid="btnclose2" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:modalpopupextender id="mpDeduction" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShowPopup1"
                        cancelcontrolid="btnclose1" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>


                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Details Of Invoice Items
                               
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
                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-3" id="divCircle" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged">
                                        </asp:DropDownList>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server"
                                            CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix">
                                        <div class="pull-right grdInvoicetableTools-container"></div>
                                    </div>
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdInvoice" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdInvoice_PreRender"
                                            OnRowDataBound="grdInvoice_RowDataBound" ShowFooter="True">
                                            <columns>
                                                <asp:BoundField DataField="PackageInvoice_Id" HeaderText="PackageInvoice_Id">
                                                    <headerstyle cssclass="displayStyle" />
                                                    <itemstyle cssclass="displayStyle" />
                                                    <footerstyle cssclass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                    <headerstyle cssclass="displayStyle" />
                                                    <itemstyle cssclass="displayStyle" />
                                                    <footerstyle cssclass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                    <headerstyle cssclass="displayStyle" />
                                                    <itemstyle cssclass="displayStyle" />
                                                    <footerstyle cssclass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <itemtemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <itemtemplate>
                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click"
                                                            ImageUrl="~/assets/images/edit.png" runat="server" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                <asp:BoundField DataField="List_EMBNo" HeaderText="EMB No" />
                                                <asp:BoundField DataField="PackageInvoice_Date" HeaderText="Invoice Date" />
                                                <asp:BoundField DataField="PackageInvoice_VoucherNo" HeaderText="Voucher No" />
                                                <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                <asp:BoundField DataField="Total_Amount_Deduction" HeaderText="Deduction Total" />
                                                <asp:BoundField DataField="Total_Amount_Final" HeaderText="Total Amount" />
                                                <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                <asp:BoundField DataField="Invoice_Status" HeaderText="Current Status" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus" HeaderText="Reason (If Any)" />

                                                <asp:BoundField DataField="CGST" HeaderText="CGST">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SGST" HeaderText="SGST">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IGST" HeaderText="IGST">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VAT" HeaderText="VAT">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ITDS" HeaderText="ITDS">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Labour_Cess" HeaderText="Labour Cess">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Deposits" HeaderText="Deposits">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Advance_Recovery" HeaderText="Advance Recovery">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Hinderance_on_Mobilization_Advance" HeaderText="Hinderance On Mobilization Advance">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Miscelleneous_Advance" HeaderText="Miscelleneous Advance">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Mob_Interest" HeaderText="Mob. Interest">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Fund_Unavailability" HeaderText="Fund Unavailability">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HandOver" HeaderText="HandOver">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Maintenance" HeaderText="Maintenance">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Penalty" HeaderText="Penalty">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Testing" HeaderText="Testing">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TIME_EXTENSION" HeaderText="Time Extention">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TrailRun" HeaderText="TrailRun">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Variation_Deduction" HeaderText="Variation Deduction">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Deduction" HeaderText="Deduction">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Other_Deduction" HeaderText="Other Deduction">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WWC" HeaderText="WWC">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Withheld" HeaderText="Withheld">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Witheld_for_GST" HeaderText="Witheld For GST">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Workmanship_Deduction" HeaderText="Workmanship Deduction">
                                                    <headerstyle backcolor="#FF6666" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="View Invoice">
                                                    <itemtemplate>
                                                        <asp:ImageButton ID="btnOpenInvoice" Width="40px" Height="40px" OnClick="btnOpenInvoice_Click"
                                                            ImageUrl="~/assets/images/print.png" runat="server" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View EMB">
                                                    <itemtemplate>
                                                        <a href="ViewEMBDetails.aspx?Invoice_Id=<%# Eval("PackageInvoice_Id") %>" target="_blank">View EMB</a>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View BOQ">
                                                    <itemtemplate>
                                                        <a href="View_BOQ_Details.aspx?Package_Id=<%# Eval("PackageInvoice_Package_Id") %>"
                                                            target="_blank">View BOQ</a>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete Invoice">
                                                    <itemtemplate>
                                                        <asp:ImageButton ID="btnDeleteInvoice" Width="30px" Height="30px" OnClick="btnDeleteInvoice_Click"
                                                            ImageUrl="~/assets/images/delete.png" runat="server" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                            </columns>
                                            <footerstyle backcolor="Black" forecolor="White" font-bold="true" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divHistory" runat="server" visible="false">
                            <div class="space-6"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPackageDetails" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender" OnRowDataBound="grdPackageDetails_RowDataBound">
                                                <columns>
                                                    <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_ULB_Id" HeaderText="ProjectWork_ULB_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="" HeaderText="">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <itemtemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
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
                                                </columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdProjectStatus" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProjectStatus_PreRender">
                                            <columns>
                                                <asp:BoundField DataField="ProjectWorkPkg_Work_Id" HeaderText="ProjectWorkPkg_Work_Id">
                                                    <headerstyle cssclass="displayStyle" />
                                                    <itemstyle cssclass="displayStyle" />
                                                    <footerstyle cssclass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                    <headerstyle cssclass="displayStyle" />
                                                    <itemstyle cssclass="displayStyle" />
                                                    <footerstyle cssclass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Overall_Status" HeaderText="Overall_Status">
                                                    <headerstyle cssclass="displayStyle" />
                                                    <itemstyle cssclass="displayStyle" />
                                                    <footerstyle cssclass="displayStyle" />
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
                                            </columns>
                                            <footerstyle font-bold="true" backcolor="LightGray" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Details Of EMB Approval History
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdEMBHistory" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdEMBHistory_PreRender">
                                                <columns>
                                                    <asp:BoundField DataField="PackageEMBApproval_Id" HeaderText="PackageEMBApproval_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMBApproval_PackageEMBMaster_Id" HeaderText="PackageEMBApproval_PackageEMBMaster_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMBApproval_Package_Id" HeaderText="PackageEMBApproval_Package_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMBApproval_Status_Id" HeaderText="PackageEMBApproval_Status_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <itemtemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PackageEMBApproval_Date" HeaderText="Approval Date" />
                                                    <asp:BoundField DataField="PackageEMBApproval_Status_Text" HeaderText="Action Taken" />
                                                    <asp:BoundField DataField="PackageEMBApproval_Comments" HeaderText="Comments" />
                                                    <asp:BoundField DataField="Designation_Current" HeaderText="Action By (Designation)" />
                                                    <asp:BoundField DataField="Person_Name" HeaderText="Action By (Name)" />
                                                    <asp:BoundField DataField="Organisation_Next" HeaderText="Next Action (Organisation)" />
                                                    <asp:BoundField DataField="Designation_Next" HeaderText="Next Action (Designation)" />
                                                    <asp:BoundField DataField="PackageEMBApproval_AddedOn" HeaderText="Added On" />
                                                </columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Details Of Invoice Approval History
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdInvoiceHistory" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdInvoiceHistory_PreRender">
                                                <columns>
                                                    <asp:BoundField DataField="PackageInvoiceApproval_Id" HeaderText="PackageInvoiceApproval_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceApproval_PackageInvoice_Id" HeaderText="PackageInvoiceApproval_PackageInvoice_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceApproval_Package_Id" HeaderText="PackageInvoiceApproval_Package_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceApproval_AddedBy" HeaderText="PackageInvoiceApproval_AddedBy">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <itemtemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PackageInvoiceApproval_Date" HeaderText="Approval Date" />
                                                    <asp:BoundField DataField="PackageInvoiceApproval_Status_Text" HeaderText="Action Taken" />
                                                    <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Other Reason (If Any)" />
                                                    <asp:BoundField DataField="PackageInvoiceApproval_Comments" HeaderText="Comments" />
                                                    <asp:BoundField DataField="Designation_Current" HeaderText="Action By (Designation)" />
                                                    <asp:BoundField DataField="Person_Name" HeaderText="Action By (Name)" />
                                                    <asp:BoundField DataField="Organisation_Next" HeaderText="Next Action (Organisation)" />
                                                    <asp:BoundField DataField="Designation_Next" HeaderText="Next Action (Designation)" />
                                                    <asp:BoundField DataField="PackageInvoiceApproval_AddedOn" HeaderText="Added On" />
                                                    <asp:TemplateField HeaderText="Deduction Change History">
                                                        <itemtemplate>
                                                            <asp:LinkButton ID="lnkDeductionHistory" runat="server" Text='<%# Eval("PackageInvoiceApproval_Deduction") %>'
                                                                OnClick="lnkDeductionHistory_Click" />
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roll Back">
                                                        <itemtemplate>
                                                            <asp:ImageButton ID="btnRollBack" Width="30px" Height="30px" OnClick="btnRollBack_Click"
                                                                ImageUrl="~/assets/images/delete.png" runat="server" />
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                </columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Download / View Previous Uploaded Document
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdMultipleFiles" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdMultipleFiles_RowDataBound">
                                                <columns>
                                                    <asp:BoundField DataField="PackageInvoiceDocs_FileName" HeaderText="PackageInvoiceDocs_FileName">
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
                                                    <asp:TemplateField HeaderText="Download">
                                                        <itemtemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("PackageInvoiceDocs_FileName") %>'
                                                                OnClientClick="return downloadFile(this);">
                                                            </asp:LinkButton>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                </columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdDeductionHistory" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductionHistory_PreRender">
                                                <columns>
                                                    <asp:BoundField DataField="PackageInvoiceAdditional_Id" HeaderText="PackageInvoiceAdditional_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageInvoiceAdditional_Invoice_Id" HeaderText="PackageInvoiceAdditional_Invoice_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Deduction_Id" HeaderText="Deduction_Id">
                                                        <headerstyle cssclass="displayStyle" />
                                                        <itemstyle cssclass="displayStyle" />
                                                        <footerstyle cssclass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <itemtemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </itemtemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Deduction_Category" HeaderText="Category" />
                                                    <asp:BoundField DataField="Deduction_Name" HeaderText="Deduction" />
                                                    <asp:BoundField DataField="PackageInvoiceAdditional_Deduction_Value_Master" HeaderText="Value (Flat / Percentage)" />
                                                    <asp:BoundField DataField="PackageInvoiceAdditional_Deduction_Value_Final" HeaderText="Deduction Value" />
                                                    <asp:BoundField DataField="PackageInvoiceAdditional_Comments" HeaderText="Comments" />
                                                </columns>
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
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <iframe style="width: 910px; height: 600px;" id="ifrm1" src="BillView.aspx" runat="server"></iframe>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <%-- <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="/assets/images/print.png" Width="50px" Height="60px" OnClientClick=" return Print();" />
                                        </div>
                                    </div>--%>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose2" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField ID="hf_Invoice_Id" runat="server" Value="0" />
                    </div>
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
    <script src="assets/js/dataTables.colReorder.min.js"></script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdInvoice').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdInvoice')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdInvoice')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: true,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        { "bSortable": false },
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 
                                        { "bSortable": false }
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
                        myTable.buttons().container().appendTo($('.grdInvoicetableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdInvoicetableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdInvoicetableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdInvoice .dropdown-toggle', function (e) {
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
        function Print() {
            debugger;
            var frame1 = document.getElementById('ctl00_ContentPlaceHolder1_ifrm1').contentWindow.document.getElementById('dvReport').getElementsByTagName("iframe")[0];
            if (navigator.appName.indexOf("Internet Explorer") != -1) {
                frame1.name = frame1.id;
                window.frames[0].focus();
                window.frames[0].print();
            }
            else {
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.print();
            }
            return false;
        }
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
        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }


    </script>
</asp:Content>





