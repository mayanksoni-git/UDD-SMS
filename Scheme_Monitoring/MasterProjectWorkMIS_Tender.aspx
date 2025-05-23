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
                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                            CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
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
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdTenderDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdTenderDetails_PreRender" OnRowDataBound="grdTenderDetails_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectTender_Id" HeaderText="ProjectTender_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectTender_Document" HeaderText="ProjectTender_Document">
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
                                                                        <asp:TextBox ID="txtNITDate" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("ProjectTender_NITDate") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtNITDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender Issue Date*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTenderIssueDate" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("ProjectTender_IssueDate") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderIssueDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender End Date*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTenderEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("ProjectTender_EndDate") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderEndDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EMD (In Lakhs)*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtEMD" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Text='<%# Eval("ProjectTender_EMD") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Text='<%# Eval("ProjectTender_Remarks") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender Status">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlTenderStatus" runat="server" CssClass="form-select">
                                                                            <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                                            <asp:ListItem Text="Failed" Value="F"></asp:ListItem>
                                                                            <asp:ListItem Text="Completed" Value="C"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Failure Reason">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlFailureReason" runat="server" CssClass="form-select">
                                                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                            <asp:ListItem Text="No Bids Received" Value="No Bids Received"></asp:ListItem>
                                                                            <asp:ListItem Text="Bids Not Qualified" Value="Bids Not Qualified"></asp:ListItem>
                                                                            <asp:ListItem Text="Technical Issues" Value="Technical Issues"></asp:ListItem>
                                                                            <asp:ListItem Text="Budget Constraints" Value="Budget Constraints"></asp:ListItem>
                                                                            <asp:ListItem Text="Project Cancelled" Value="Project Cancelled"></asp:ListItem>
                                                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Upload Tender File*">
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="fuTenderFile" runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnAddTender" OnClick="btnAddTender_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                        <asp:ImageButton ID="imgDeleteTender" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgDeleteTender_Click" Width="30px" Height="30px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tender Document">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkTenderDoc" runat="server" Text="Download" Tender_FilePath='<%#Eval("ProjectTender_Document") %>' OnClientClick="return downloadTenderDoc(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAction" OnClick="btnAction_Click" runat="server" ToolTip="Click to edit" ImageUrl="~/assets/images/edit.png" Width="25px" Height="25px" />
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
                                                <!--end col-->
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div class="row">
                            <div class="col-xxl-12 col-md-12 text-center">
                                <div>
                                    <asp:Button ID="btnSave" Text="Save Tender Details" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                </div>
                            </div>
                        </div>
                        
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Action On Tender</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label5" runat="server" Text="Action*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList CssClass="form-select" ID="ddlAction" runat="server">
                                                                <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label19" runat="server" Text="Comments*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <asp:Button ID="btnUpdateAction" Text="Update" OnClick="btnUpdateAction_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Sr_No" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
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
</asp:Content>