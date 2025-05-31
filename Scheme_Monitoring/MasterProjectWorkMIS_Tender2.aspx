<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_Tender2.aspx.cs" Inherits="MasterProjectWorkMIS_Tender2" 
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <style>
        .project-details {
            background-color: #f8f9fa;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }
        .project-details label {
            font-weight: bold;
            margin-right: 5px;
        }
        .form-section {
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .tender-table {
            margin-top: 20px;
        }
        .hidden {
            display: none;
        }
    </style>
    
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
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
                        </div>
                        
                        <!-- Project Details Section -->
                        <div class="row">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project Details</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="project-details">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label>Name of Project:</label>
                                                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Project Type:</label>
                                                    <asp:Label ID="lblProjectType" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Scheme Name:</label>
                                                    <asp:Label ID="lblSchemeName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>ULB Name:</label>
                                                    <asp:Label ID="lblULBName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>District Name:</label>
                                                    <asp:Label ID="lblDistrictName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Sanctioned Cost:</label>
                                                    <asp:Label ID="lblSanctionedCost" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Tender Form Section -->
                        <div class="row">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Tender Details Form</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="form-section">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">NIT Date *</label>
                                                        <asp:TextBox ID="txtNITDate" runat="server" CssClass="form-control date-picker" autocomplete="off"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="ceNITDate" runat="server" CssClass="cal_Theme1" 
                                                            TargetControlID="txtNITDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Tender Issue Date *</label>
                                                        <asp:TextBox ID="txtTenderIssueDate" runat="server" CssClass="form-control date-picker" autocomplete="off"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="ceTenderIssueDate" runat="server" CssClass="cal_Theme1" 
                                                            TargetControlID="txtTenderIssueDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Tender End Date *</label>
                                                        <asp:TextBox ID="txtTenderEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="ceTenderEndDate" runat="server" CssClass="cal_Theme1" 
                                                            TargetControlID="txtTenderEndDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">EMD (In Lakhs) *</label>
                                                        <asp:TextBox ID="txtEMD" runat="server" CssClass="form-control" 
                                                            onkeypress="return isDecimalNumber(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Tender Status *</label>
                                                        <asp:DropDownList ID="ddlTenderStatus" runat="server" CssClass="form-select" 
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlTenderStatus_SelectedIndexChanged">
                                                            <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="Failed" Value="F"></asp:ListItem>
                                                            <asp:ListItem Text="Completed" Value="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4" id="divFailureReason" runat="server" visible="false">
                                                    <div class="mb-3">
                                                        <label class="form-label">Tender Failure Reason *</label>
                                                        <asp:DropDownList ID="ddlFailureReason" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="No Bids Received" Value="No Bids Received"></asp:ListItem>
                                                            <asp:ListItem Text="Bids Not Qualified" Value="Bids Not Qualified"></asp:ListItem>
                                                            <asp:ListItem Text="Technical Issues" Value="Technical Issues"></asp:ListItem>
                                                            <asp:ListItem Text="Budget Constraints" Value="Budget Constraints"></asp:ListItem>
                                                            <asp:ListItem Text="Project Cancelled" Value="Project Cancelled"></asp:ListItem>
                                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" id="divFailureDetail" runat="server" visible="false">
                                                    <div class="mb-3">
                                                        <label class="form-label">Tender Failure Detail *</label>
                                                        <asp:TextBox ID="txtFailureDetail" runat="server" CssClass="form-control" 
                                                            TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="mb-3">
                                                        <label class="form-label">Tender Remark</label>
                                                        <asp:TextBox ID="txtTenderRemark" runat="server" CssClass="form-control" 
                                                            TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="mb-3">
                                                        <label class="form-label">Upload Tender File (PDF only) *</label>
                                                        <asp:FileUpload ID="fuTenderFile" runat="server" CssClass="form-control" 
                                                            accept=".pdf" />
                                                        <asp:Label ID="lblFileError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6" id="divExistingFile" runat="server" visible="false">
                                                    <div class="mb-3">
                                                        <label class="form-label">Existing Tender Document</label>
                                                        <asp:HyperLink ID="lnkExistingFile" runat="server" CssClass="btn btn-outline-primary" 
                                                            Target="_blank">View Document</asp:HyperLink>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="row mt-3">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary me-2" 
                                                        OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success me-2" 
                                                        Visible="false" OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" 
                                                        OnClick="btnReset_Click" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Tender List Section -->
                        <div class="row">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Tender History</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTenders" runat="server" CssClass="table table-bordered table-striped" 
                                                AutoGenerateColumns="false" EmptyDataText="No Tenders Found" 
                                                OnRowCommand="gvTenders_RowCommand" DataKeyNames="ProjectTender_Id,ProjectTender_FilePath">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectTender_NITDate" HeaderText="NIT Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="ProjectTender_IssueDate" HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="ProjectTender_EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="ProjectTender_EMD" HeaderText="EMD (Lakhs)" DataFormatString="{0:N2}" />
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <%# GetStatusText(Eval("ProjectTender_Status").ToString()) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectTender_FailureReason" HeaderText="Failure Reason" />
                                                    <asp:BoundField DataField="ProjectTender_Remarks" HeaderText="Remarks" />
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkDocument" runat="server" 
                                                                NavigateUrl='<%# Eval("ProjectTender_FilePath") %>' 
                                                                Target="_blank" CssClass="btn btn-sm btn-outline-primary"
                                                                Visible='<%# !string.IsNullOrEmpty(Eval("ProjectTender_FilePath").ToString()) %>'>
                                                                View
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditTender" 
                                                                CommandArgument='<%# Eval("ProjectTender_Id") %>' 
                                                                CssClass="btn btn-sm btn-outline-success">
                                                                <i class="fas fa-edit"></i> Edit
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
                    
                    <asp:HiddenField ID="hfProjectWorkId" runat="server" />
                    <asp:HiddenField ID="hfSchemeId" runat="server" />
                    <asp:HiddenField ID="hfTenderId" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <asp:AsyncPostBackTrigger ControlID="gvTenders" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTenderStatus" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <script type="text/javascript">
        function isDecimalNumber(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>