<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_Tender.aspx.cs" Inherits="MasterProjectWorkMIS_Tender" 
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" 
                            TargetControlID="btnShowPopup" CancelControlID="btnclose" 
                            BackgroundCssClass="modalBackground1">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;" />
                        
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
                                                        <asp:GridView ID="grdTenderDetails" runat="server" 
                                                            CssClass="display table table-bordered" AutoGenerateColumns="false" 
                                                            EmptyDataText="No Records Found" ShowFooter="true" 
                                                            OnRowDataBound="grdTenderDetails_RowDataBound"
                                                            OnRowCommand="grdTenderDetails_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="NIT Date">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtNITDate" runat="server" CssClass="form-control date-picker"
                                                                            autocomplete="off" Text='<%# Eval("ProjectTender_NITDate") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="ceNITDate" runat="server" CssClass="cal_Theme1"
                                                                            TargetControlID="txtNITDate" Format="dd/MM/yyyy">
                                                                        </cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Tender Issue Date*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTenderIssueDate" runat="server" CssClass="form-control date-picker" 
                                                                            autocomplete="off" Text='<%# Eval("ProjectTender_IssueDate") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="ceIssueDate" runat="server" CssClass="cal_Theme1" 
                                                                            TargetControlID="txtTenderIssueDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Tender End Date*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTenderEndDate" runat="server" CssClass="form-control date-picker" 
                                                                            autocomplete="off" Text='<%# Eval("ProjectTender_EndDate") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="ceEndDate" runat="server" CssClass="cal_Theme1" 
                                                                            TargetControlID="txtTenderEndDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="EMD (In Lakhs)*">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtEMD" runat="server" CssClass="form-control" 
                                                                            onkeypress="return isDecimalNumber(event)" 
                                                                            Text='<%# Eval("ProjectTender_EMD") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" 
                                                                            Text='<%# Eval("ProjectTender_Remarks") %>'></asp:TextBox>
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
                                                                        <asp:HiddenField ID="hfFilePath" runat="server" Value='<%# Eval("ProjectTender_FilePath") %>' />
                                                                        <asp:HiddenField ID="hfFileName" runat="server" Value='<%# Eval("ProjectTender_FileName") %>' />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="btnAddTender" runat="server" CommandName="AddNew" 
                                                                            CssClass="btn btn-primary btn-sm">
                                                                            <i class="fa fa-plus"></i> Add
                                                                        </asp:LinkButton>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Tender Document">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="lnkDownload" runat="server" 
                                                                            NavigateUrl='<%# Eval("ProjectTender_FilePath") %>' 
                                                                            Target="_blank" Text='<%# Eval("ProjectTender_FileName") %>'
                                                                            Visible='<%# !string.IsNullOrEmpty(Eval("ProjectTender_FilePath").ToString()) %>'>
                                                                        </asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditTender" 
                                                                            CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-info">
                                                                            <i class="fa fa-edit"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteTender" 
                                                                            CommandArgument='<%# Eval("ProjectTender_Id") %>' CssClass="btn btn-sm btn-danger"
                                                                            OnClientClick="return confirm('Are you sure you want to delete this tender?');">
                                                                            <i class="fa fa-trash"></i>
                                                                        </asp:LinkButton>
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
                            
                            <div class="row">
                                <div class="col-xxl-12 col-md-12 text-center">
                                    <div>
                                        <asp:Button ID="btnSave" Text="Save Tender Details" OnClick="btnSave_Click" 
                                            runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_SelectedTenderId" runat="server" Value="0" />
                    </div>
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
    </div>
    
    <script type="text/javascript">
        function isDecimalNumber(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        
        function showModal() {
            $find('<%= mp1.ClientID %>').show();
            return false;
        }
    </script>
</asp:Content>