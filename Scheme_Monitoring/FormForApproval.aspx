<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FormForApproval.aspx.cs" Inherits="FormForApproval" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

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
                                    <h4 class="mb-sm-0">Create Work Proposal</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Proposal Management System</li>
                                            <li class="breadcrumb-item active">Create Work Proposal</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Detail</h4>
                                        <%--OnClientClick="BindChart(); return false;"--%>
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"  runat="server" CssClass="btn bg-success text-white"></asp:Button>
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
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
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

                                                <div class="col-xxl-3 col-md-6" id="divZoneOfULB">
                                                    <asp:Label ID="lblZoneOfULB" runat="server" Text="Zone" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtZoneOfULB" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divWard">
                                                    <asp:Label ID="lblWard" runat="server" Text="Ward" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtWard" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectMaster_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divSubScheme" visible="false" runat="server">
                                                    <asp:Label ID="lblSubScheme" runat="server" Text="Choose Sub Scheme*" CssClass="form-label"></asp:Label>
                                                    <asp:RadioButtonList ID="rblSubScheme" runat="server" CssClass="form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblRoles_SelectedIndexChanged">
                                                        <asp:ListItem Text="Anudaan 37 " Value="37"></asp:ListItem>
                                                        <asp:ListItem Text="Anudaan 83 " Value="83"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>


                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divWorkType" runat="server">
                                                        <asp:Label ID="lblWorkType" runat="server" Text="Type of Work*" CssClass="form-label"></asp:Label>
                                                        <%--<asp:DropDownList ID="ddlWorkType" runat="server" CssClass="form-select"></asp:DropDownList>--%>
                                                        <asp:ListBox ID="ddlWorkType" runat="server" SelectionMode="Multiple" class="chosen-select form-control multiselect" data-placeholder="Choose a Work Type..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProposalName" runat="server">
                                                        <asp:Label ID="lblProposalName" runat="server" Text="Proposal Name (Letter subject)" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtProposalName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProposalDetail" runat="server">
                                                        <asp:Label ID="lblProposalDetail" runat="server" Text="Proposal Detail (Work details)" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtProposalDetail" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProposalDate" runat="server">
                                                        <asp:Label ID="lblProposalDate" runat="server" Text="Proposal Date" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtProposalDate" type="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="lblExpectedAmount" runat="server" Text="Expected Amount(In Lakhs)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtExpectedAmount" runat="server" placeholder="31.89" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblRole" runat="server" Text="Select Proposer Type*" CssClass="form-label"></asp:Label>
                                                    <asp:DropDownList ID="rblRoles" runat="server" CssClass="form-select" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblRoles_SelectedIndexChanged">
                                                        <asp:ListItem Text="-Select-" Value="-1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Central Minister" Value="Central Minister"></asp:ListItem>
                                                        <asp:ListItem Text="Minister" Value="Minister"></asp:ListItem>
                                                        <asp:ListItem Text="MP" Value="MP"></asp:ListItem>
                                                        <asp:ListItem Text="MLA" Value="MLA"></asp:ListItem>
                                                        <asp:ListItem Text="MLC" Value="MLC"></asp:ListItem>
                                                        <asp:ListItem Text="Mayor" Value="Mayor"></asp:ListItem>
                                                        <asp:ListItem Text="Municipal Commissioner" Value="Municipal Commissioner"></asp:ListItem>
                                                        <asp:ListItem Text="District Magistrate" Value="District Magistrate"></asp:ListItem>
                                                        <asp:ListItem Text="Divisional Commissioner" Value="Divisional Commissioner"></asp:ListItem>
                                                        <asp:ListItem Text="President Nagar Panchayat" Value="President Nagar Panchayat"></asp:ListItem>
                                                        <asp:ListItem Text="EO Nagar Panchayat" Value="EO Nagar Panchayat"></asp:ListItem>
                                                        <asp:ListItem Text="President Nagar Palika Parishad" Value="President Nagar Palika Parishad"></asp:ListItem>
                                                        <asp:ListItem Text="EO Nagar Palika Parishad" Value="EO Nagar Palika Parishad"></asp:ListItem>
                                                        <asp:ListItem Text="उत्तर प्रदेश जल निगम (नगरीय)" Value="उत्तर प्रदेश जल निगम (नगरीय)"></asp:ListItem>
                                                        <asp:ListItem Text="C&DS (नगरीय)" Value="C&DS (नगरीय)"></asp:ListItem>
                                                        <asp:ListItem Text="Ex-MLA" Value="Ex-MLA"></asp:ListItem>
                                                        <asp:ListItem Text="Ex-MP" Value="Ex-MP"></asp:ListItem>
                                                        <asp:ListItem Text="प्रदेश अध्यक्ष" Value="प्रदेश अध्यक्ष"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divMPMLA" style="display: block;" runat="server">
                                                    <asp:Label ID="lblMPMLA" runat="server" Text="Select Proposer*" CssClass="form-label"></asp:Label>
                                                    <asp:DropDownList ID="ddlMPMLA" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMPMLA_SelectedIndexChanged"></asp:DropDownList>
                                                </div>



                                                <div class="col-xxl-3 col-md-6" id="divOthers" visible="false" runat="server">
                                                    <asp:Label ID="lblOther" runat="server" Text="Name of Proposer*" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divParty" style="display: block;" runat="server">
                                                    <asp:Label ID="lblParty" runat="server" Text="Political Party" CssClass="form-label" ></asp:Label>
                                                    <asp:TextBox ID="lblParyOfMPMLA" runat="server" Text="" CssClass="form-control" Enabled="false"></asp:TextBox>

                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="divConstituency" style="display: block;" runat="server">
                                                    <asp:Label ID="lblConstituency" runat="server" Text="Constituency" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="lblConstituencyName" runat="server" Text="" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" TextMode="Phone" MaxLength="10"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revMobileNo" runat="server"
                                                        ControlToValidate="txtMobileNo"
                                                        ErrorMessage="Please enter a valid mobile number."
                                                        CssClass="text-danger"
                                                        ValidationExpression="^\d{10}$">
                                                    </asp:RegularExpressionValidator>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDesignation" runat="server">
                                                        <asp:Label ID="lblDesignation" runat="server" Text="Designation (If Any)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblRecommendationLetter" runat="server" Text="Upload Recommendation Letter" CssClass="form-label"></asp:Label>
                                                    <asp:FileUpload ID="fileUploadRecommendationLetter" runat="server" CssClass="form-control" />
                                                    <asp:HyperLink ID="hypRecommendationLetterEdit" runat="server" Target="_blank" Text="Click To View" Visible="false">
                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                    </asp:HyperLink>
                                                    <asp:HiddenField id="hfPDFUrl" runat="server"/>
                                                    <asp:RegularExpressionValidator ID="revFileUpload" runat="server"
                                                        ControlToValidate="fileUploadRecommendationLetter"
                                                        ErrorMessage="Only PDF files are allowed."
                                                        CssClass="text-danger"
                                                        ValidationExpression="^.*\.(pdf)$">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:CustomValidator ID="cvFileSize" runat="server"
                                                        ControlToValidate="fileUploadRecommendationLetter"
                                                        ErrorMessage="File size cannot exceed 5MB."
                                                        CssClass="text-danger"
                                                        OnServerValidate="cvFileSize_ServerValidate">
                                                    </asp:CustomValidator>
                                                </div>

                                                <div class="col-xxl-2 offset-xxl-1 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
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
                    <div runat="server" visible="false" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Detail</h4>
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                       
                                                    </div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found"
                                                    OnPageIndexChanging="OnPageIndexChanging" PageSize="10" OnPreRender="grdPost_PreRender">
                                                        <Columns>
                                                            <asp:BoundField DataField="WorkProposalId" HeaderText="Work Proposal Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Proposal Code" DataField="ProposalCode" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="FinYear" />
                                                            <%--<asp:BoundField HeaderText="State" DataField="Zone_Name" />--%>
                                                            <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="ULB" DataField="Division_Name" />
                                                            <%--<asp:BoundField HeaderText="Zone" DataField="ZoneOfULB" />--%>
                                                            <%--<asp:BoundField HeaderText="Ward" DataField="Ward" />--%>
                                                            <%--<asp:BoundField HeaderText="Scheme" DataField="ShortNameCode" />--%>
                                                            <asp:TemplateField HeaderText="Scheme">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblShortNameCode" runat="server" Text='<%# Eval("ShortNameCode") %>' ToolTip='<%# Eval("Project_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Project Types" DataField="ProjectType_Names" />
                                                            <asp:BoundField HeaderText="Expected Amount" DataField="ExpectedAmount" />
                                                            <asp:BoundField HeaderText="Proposer Type" DataField="ProposerType" />
                                                            <asp:BoundField HeaderText="Proposer" DataField="MPMLAName" />
                                                            <%--<asp:BoundField HeaderText="Proposer Name" DataField="ProposerName" />--%>
                                                            <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                                                            <%--<asp:BoundField HeaderText="Designation" DataField="Designation" />--%>
                                                            <asp:TemplateField HeaderText="Recommendation Letter">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypRecommendationLetter" runat="server" Target="_blank" NavigateUrl='<%# Eval("RecomendationLetter") %>' Text="Click To View" Visible='<%# !string.IsNullOrEmpty(Eval("RecomendationLetter").ToString()) %>'>
                                                                        <asp:Image ID="imgViewPDF" runat="server" ImageUrl="~/assets/images/ViewPdf.png" AlternateText="View PDF" Height="30" Width="30" />
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <%--<asp:BoundField HeaderText="Status" DataField="ProposalStatus" />--%>

                                                            <asp:BoundField HeaderText="Added On" DataField="AddedOn" DataFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
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
                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnExportToExcel" />
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlProjectMaster" />
                    <asp:PostBackTrigger ControlID="rblRoles" />
                </Triggers>
            </asp:UpdatePanel>
           
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#<%= ddlMPMLA.ClientID %>').select2({
                placeholder: "Search...",
                allowClear: true
            });
        });
    </script>
    <style>
        .form-control img {
            height: 20px;
            width: 20px;
            vertical-align: middle;
            margin-right: 10px;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td {
            height: 35px;
            width: 35px;
            line-height: 35px;
            display: inline-block;
            background: #dce9f7;
            border: 1px solid #d1e3f7;
            text-align: center;
            margin: 0 2px;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td a {
            height: 35px;
            color: #000;
            width: 35px;
            display: block;
        }

        #ctl00_ContentPlaceHolder1_gvRecords tbody tbody td:hover {
            background: #c5dffb;
            border: 1px solid #bbdbff;
        }
    </style>
</asp:Content>
