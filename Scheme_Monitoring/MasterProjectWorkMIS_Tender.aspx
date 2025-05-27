<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_Tender.aspx.cs" Inherits="MasterProjectWorkMIS_Tender" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12 mb-3">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Tender Management</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Project Master</li>
                                            <li class="breadcrumb-item active">Tender Management</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Tender Details</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdTenderDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" 
                                                            EmptyDataText="No Records Found" ShowFooter="false" OnPreRender="grdTenderDetails_PreRender" 
                                                            OnRowDataBound="grdTenderDetails_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="Tender_Id" HeaderText="Tender_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Tender_DocumentPath" HeaderText="Tender_DocumentPath">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NIT Date">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtNITDate" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("NIT_Date", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtNITDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender Issue Date*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTenderIssueDate" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("Tender_IssueDate", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderIssueDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender End Date*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTenderEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("Tender_EndDate", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderEndDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EMD (In Lakhs)*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtEMD" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Text='<%# Eval("EMD_Amount") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender Status">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlTenderStatus" runat="server" CssClass="form-select">
                                                                            <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                                                            <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Failure Reason">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlFailureReason" runat="server" CssClass="form-select">
                                                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                            <asp:ListItem Text="No Bids Received" Value="No Bids Received"></asp:ListItem>
                                                                            <asp:ListItem Text="Bids Not Responsive" Value="Bids Not Responsive"></asp:ListItem>
                                                                            <asp:ListItem Text="Bids Above Budget" Value="Bids Above Budget"></asp:ListItem>
                                                                            <asp:ListItem Text="Technical Issues" Value="Technical Issues"></asp:ListItem>
                                                                            <asp:ListItem Text="Administrative Reasons" Value="Administrative Reasons"></asp:ListItem>
                                                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender Document">
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="fuTenderDocument" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Download">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkTenderDoc" runat="server" Text="Download" Tender_FilePath='<%#Eval("Tender_DocumentPath") %>' OnClientClick="return downloadTenderDoc(this);"></asp:LinkButton>
                                                                        <asp:HiddenField ID="hfDocumentPath" runat="server" Value='<%# Eval("Tender_DocumentPath") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDeleteTender" OnClick="btnDeleteTender_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="25px" Height="25px" />
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

                        <div class="row">
                            <div class="col-xxl-12 col-md-12 text-center">
                                <div>
                                    <asp:Button ID="btnAddNewTender" Text="Add New Tender" OnClick="btnAddNewTender_Click" runat="server" CssClass="btn btn-primary"></asp:Button>
                                    <asp:Button ID="btnSave" Text="Save Details" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnAddNewTender" />
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
    
    <script>
        function downloadTenderDoc(obj) {
            var Tender_FilePath;
            Tender_FilePath = obj.attributes.Tender_FilePath.nodeValue;
            if (Tender_FilePath.trim() == "") {
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + Tender_FilePath, "_blank", "", false);
                return false;
            }
        }
    </script>
</asp:Content>