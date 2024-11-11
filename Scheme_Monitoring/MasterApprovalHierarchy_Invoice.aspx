<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterApprovalHierarchy_Invoice.aspx.cs" Inherits="MasterApprovalHierarchy_Invoice" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h4 class="mb-sm-0">DPR Approval Configuration</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Process Config</li>
                                            <li class="breadcrumb-item active">DPR Approval Configuration</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Fund Sanction Master</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <label class="control-label no-padding-right">Create Dynamic Approval Hierarchy</label>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <label class="control-label no-padding-right">Organization* </label>
                                                    <asp:DropDownList ID="ddlOrganization" runat="server" CssClass="form-select"></asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="Department" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" class="form-select"></asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <label class="control-label no-padding-right">Designation*</label>
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-select"></asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <label class="control-label no-padding-right">Designation</label>
                                                    <asp:DropDownList ID="ddlDesignation1" runat="server" CssClass="form-select"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:CheckBox ID="chkCreate" runat="server" CssClass="form-control" Text="Allowed For Creation / Addition"></asp:CheckBox>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:CheckBox ID="chkUpdate" runat="server" CssClass="form-control" Text="Allowed For Updations / Approval"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-6 col-md-6">
                                                    <label class="control-label no-padding-right">Permission To Change Status* </label>
                                                    <asp:CheckBoxList ID="chkInvoiceStatus" CellSpacing="50" CellPadding="10" Font-Bold="true" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="true" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Document Mapping</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdDocumentMaster" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDocumentMaster_PreRender">
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
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-12">
                                                        <div class="form-group text-center">
                                                            <asp:Button ID="btnAdd" Text="Add Process" OnClick="btnAdd_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        </div>
                                                    </div>
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

                    <div runat="server" visible="true" id="div2">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Process Approval Hierarchy</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <asp:GridView ID="grdComiteeMember" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdComiteeMember_PreRender">
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

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-md-4">
                                                    <label class="control-label no-padding-right">Comments* </label>
                                                    <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                                </div>
                                                <div class="col-md-4 offset-xxl-4">
                                                    <br />
                                                    <label class="control-label no-padding-right" style="display:block"></label>
                                                    <asp:Button ID="btnUpload" Text="Save Process" OnClick="btnUpload_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>

                                                </div>
                                            </div>
                                        </div>

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



