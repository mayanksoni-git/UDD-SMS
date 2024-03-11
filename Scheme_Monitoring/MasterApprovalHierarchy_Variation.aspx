﻿<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterApprovalHierarchy_Variation.aspx.cs" Inherits="MasterApprovalHierarchy_Variation" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Create Dynamic Approval Hierarchy
                                    <div class="pull-right">
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Organization* </label>
                                        <asp:DropDownList ID="ddlOrganization" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblDistrict" runat="server" Text="Department" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Designation*</label>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Designation</label>
                                        <asp:DropDownList ID="ddlDesignation1" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkCreate" runat="server" CssClass="form-control" Text="Allowed For Creation / Addition"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkUpdate" runat="server" CssClass="form-control" Text="Allowed For Updations / Approval"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkDeductionAllow" runat="server" CssClass="form-control" Text="Allowed For Updation in Deductions"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:CheckBox ID="chkAllowInputTransferAmount" runat="server" CssClass="form-control" Text="Input Transfer Amount Against Invoices"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Permission To Change Status* </label>
                                        <asp:CheckBoxList ID="chkInvoiceStatus" CellSpacing="50" CellPadding="10" Font-Bold="true" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:CheckBox ID="chkAllowEditingQty" runat="server" CssClass="form-control" Text="Allowed For Editing Qty"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:CheckBox ID="chkAllowDocumentUpdation" runat="server" CssClass="form-control" Text="Updation Of Documents Uploaded"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Document Mapping
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDocumentMaster" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDocumentMaster_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="TradeDocument_Id" HeaderText="TradeDocument_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Document Name" DataField="TradeDocument_Name" />
                                                <asp:TemplateField HeaderText="Display After Days">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDays" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" Text="Add Process" OnClick="btnAdd_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="Div1" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div class="table-header">
                                            Process Approval Hierarchy
                                        </div>

                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdComiteeMember" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdComiteeMember_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProcessConfigMaster_Id" HeaderText="ProcessConfigMaster_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessConfigMaster_OrgId" HeaderText="ProcessConfigMaster_OrgId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessConfigMaster_Department_Id" HeaderText="ProcessConfigMaster_Department_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessConfigMaster_Designation_Id" HeaderText="ProcessConfigMaster_Designation_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessConfigMaster_Designation_Id1" HeaderText="ProcessConfigMaster_Designation_Id1">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcessConfigInvStatus_InvoiceStatus_Id" HeaderText="ProcessConfigInvStatus_InvoiceStatus_Id">
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
                                                    <asp:BoundField HeaderText="Organization" DataField="Organization_Name" />
                                                    <asp:BoundField HeaderText="Department" DataField="Department_Name" />
                                                    <asp:BoundField HeaderText="Designation" DataField="Designation_Name" />
                                                    <asp:BoundField HeaderText="Designation1" DataField="Designation_Name1" />
                                                    <asp:BoundField HeaderText="Allowed For Creation" DataField="ProcessConfigMaster_Creation_Allowed" />
                                                    <asp:BoundField HeaderText="Allowed For Updation / Approval" DataField="ProcessConfigMaster_Updation_Allowed" />
                                                    <asp:BoundField HeaderText="Allowed For Updation in Deductions" DataField="ProcessConfigMaster_Deduction_Allowed" />
                                                    <asp:BoundField HeaderText="Allowed For Input Transfer Amount" DataField="ProcessConfigMaster_Transfer_Allowed" />
                                                    <asp:BoundField HeaderText="Allowed For Editing Qty" DataField="ProcessConfigMaster_Qty_Editing_Allowed" />
                                                    <asp:BoundField HeaderText="Allowed For Updation Of Documents Uploaded" DataField="ProcessConfigMaster_Document_Updation_Allowed" />
                                                    <asp:BoundField HeaderText="Permitted Status" DataField="InvoiceStatus_Name" />
                                                    <asp:BoundField HeaderText="Documents Uploading" DataField="TradeDocument_Name" />
                                                    <asp:BoundField HeaderText="Loop" DataField="ProcessConfigMaster_Loop" />
                                                    <asp:TemplateField HeaderText="Remove">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnRemove" Width="20px" Height="20px" OnClick="btnRemove_Click" ImageUrl="~/assets/images/minus-icon.png" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!-- PAGE CONTENT ENDS -->
                            </div>
                            <!-- /.col -->
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Comments* </label>
                                        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <br />
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btnUpload" Text="Save Process" OnClick="btnUpload_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
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
        <!-- /.main-content -->
    </div>

</asp:Content>



