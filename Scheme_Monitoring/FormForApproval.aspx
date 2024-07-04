<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval.aspx.cs" Inherits="FormForApproval" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    
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
                                        <h4 class="mb-sm-0">Create Work Plan</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Work Plan Management System</li>
                                                <li class="breadcrumb-item active">Create Work Plan</li>
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
                                            <h4 class="card-title mb-0 flex-grow-1">Basic Detail</h4>
                                            <%--<a class="btn btn-primary" href="#">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-arrow-left-circle-fill" viewBox="0 0 16 16">
                                                    <path d="M8 0a8 8 0 1 0 0 16A8 8 0 0 0 8 0m3.5 7.5a.5.5 0 0 1 0 1H5.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L5.707 7.5z" />
                                                </svg>
                                                Back to Report</a>--%>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divFY" runat="server">
                                                            <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" ></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divZone" runat="server">
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divCircle" runat="server">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divDivision" runat="server">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="divScheme" runat="server">
                                                            <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectMaster_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="divWorkType" runat="server">
                                                            <asp:Label ID="lblWorkType" runat="server" Text="Type of Work*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlWorkType" runat="server" CssClass="form-select"></asp:DropDownList>

                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div8" runat="server">
                                                            <asp:Label ID="lblExpectedAmount" runat="server" Text="Expected Amount*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtExpectedAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <asp:Label ID="lblRole" runat="server" Text="Select Proposer*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblRoles" runat="server" CssClass="form-control" RepeatDirection="Horizontal" AutoPostBack="false">
                                                            <asp:ListItem Text="MP" Value="MP" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="MLA" Value="MLA"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6" id="divMPMLA" style="display: block;">
                                                        <asp:Label ID="lblMPMLA" runat="server" Text="MP/MLA*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMPMLA" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="Select MP/MLA" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    

                                                    <div class="col-xxl-4 col-md-6" id="divOthers" style="display: none;">
                                                        <asp:Label ID="lblOther" runat="server" Text="Name of Proposer*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6" id="divParty" style="display: block;">
                                                        <asp:Label ID="lblParty" runat="server" Text="Political Party*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="BJP" Value="BJP">BJP</asp:ListItem>
                                                            <asp:ListItem Text="Congress" Value="Congress">Congress</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" TextMode="Phone" MaxLength="10"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revMobileNo" runat="server"
                                                            ControlToValidate="txtMobileNo"
                                                            ErrorMessage="Please enter a valid mobile number."
                                                            CssClass="text-danger"
                                                            ValidationExpression="^\d{10}$">
                                                        </asp:RegularExpressionValidator>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="divDesignation" runat="server">
                                                            <asp:Label ID="lblDesignation" runat="server" Text="Designation (If Any)" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-4 col-md-6">
                                                        <asp:Label ID="lblRecommendationLetter" runat="server" Text="Upload Recommendation Letter*" CssClass="form-label"></asp:Label>
                                                        <asp:FileUpload ID="fileUploadRecommendationLetter" runat="server" CssClass="form-control" />
                                                        <asp:RegularExpressionValidator ID="revFileUpload" runat="server"
                                                            ControlToValidate="fileUploadRecommendationLetter"
                                                            ErrorMessage="Only PDF or image files are allowed."
                                                            CssClass="text-danger"
                                                            ValidationExpression="^.*\.(pdf|jpg|jpeg|png)$">
                                                        </asp:RegularExpressionValidator>
                                                    </div>

                                                    <div class="col-xxl-4 offset-xxl-4 col-md-6">
                                                        <div>
                                                            <label class="d-block"> &nbsp;</label>
                                                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                            <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                            <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                            <asp:HiddenField ID="hfFormApproval_Id" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xxl-12 col-md-12">
                                <div>
                                    
                                </div>
                            </div>
                        </div>
                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Released Loan Detail</h4>
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
                                                        
                                                        <asp:GridView ID="gvRecords" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                            <Columns>
                                                                <asp:BoundField DataField="LoanRelease_Id" HeaderText="Loan Release Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                
                                                                <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:BoundField HeaderText="Zone" DataField="ZoneName" />
                                                                <asp:BoundField HeaderText="District" DataField="CircleName" />
                                                                <asp:BoundField HeaderText="ULB" DataField="DivisionName" />

                                                                <asp:BoundField HeaderText="Release Amount" DataField="ReleaseAmount" />
                                                                <asp:BoundField HeaderText="Installment No" DataField="InstNo" />
                                                                <asp:BoundField HeaderText="Release Date" DataField="ReleaseDate" />
                                                                <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <tr>
                                                                    <td colspan="22" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                                </tr>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
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
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
                <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                            <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                                <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
        </div>
    </div>

    <script type="text/javascript">
        document.getElementById('<%= rblRoles.ClientID %>').addEventListener('change', function () {
            //alert("MP MLA selected")
            var ddlMPMLA = document.getElementById('divMPMLA');
            var divParty = document.getElementById('divParty');
            var txtOthers = document.getElementById('divOthers');
            var selectedValue = this.querySelector('input[type="radio"]:checked').value;

            if (selectedValue === 'MP' || selectedValue === 'MLA') {
                ddlMPMLA.style.display = 'block';
                divParty.style.display = 'block';
                txtOthers.style.display = 'none';
            } else if (selectedValue === 'Others') {
                ddlMPMLA.style.display = 'none';
                divParty.style.display = 'none';
                txtOthers.style.display = 'block';
            }
        });

        document.addEventListener('DOMContentLoaded', function () {
            var ddlParty = document.getElementById('<%= ddlParty.ClientID %>');
            for (var i = 0; i < ddlParty.options.length; i++) {
                var option = ddlParty.options[i];
                var imgUrl = option.getAttribute('DataImageUrl');
                if (imgUrl) {
                    var img = document.createElement('img');
                    img.src = imgUrl;
                    img.style.height = '20px';
                    img.style.width = '20px';
                    img.style.verticalAlign = 'middle';
                    img.style.marginRight = '10px';
                    option.textContent = '';
                    option.appendChild(img);
                    option.appendChild(document.createTextNode(option.text));
                }
            }
        });
</script>

    <style>
    .form-control img {
        height: 20px;
        width: 20px;
        vertical-align: middle;
        margin-right: 10px;
    }
</style>
</asp:Content>