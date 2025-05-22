<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWork_DataEntrySection.aspx.cs" Inherits="MasterProjectWork_DataEntrySection" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <div class="main-content">
        <div class="page-content">
            <div class="container-fluid">
                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                </cc1:ToolkitScriptManager>
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <div id="divCreate" runat="server">
                            <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Create / Update Project (ULB)</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Project Master</li>
                                                <li class="breadcrumb-item active">Create / Update Project (Section)</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Create / Update Project</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <%--<label for="basiInput" class="form-label">Scheme</label>--%>
                                                            <asp:Label ID="lblSchemeH" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectMaster_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divZone" runat="server">
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divCircle" runat="server">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divDivision" runat="server">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label14" runat="server" Text="Lok Sabha" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlLokSabha" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlLokSabha_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label17" runat="server" Text="Vidhan Sabha" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlVidhanSabha" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <%--<label class="form-label">Project Name*</label>--%>
                                                            <asp:Label ID="lblProjectName" runat="server" Text="Project Name*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtProjectWorkName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label4" runat="server" Text="Project Cost (In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtProjectCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="txtProjectCost_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label5" runat="server" Text="A&OE (In Rupees)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtAAndOE" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="txtAAndOE_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label2" runat="server" Text="Sanctioned Cost (In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" Enabled="false" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label3" runat="server" Text="GO No*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtGONo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label1" runat="server" Text="GO Date*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Style="display: none;" autocomplete="off" data-provider="flatpickr" data-date-format="d/m/Y"></asp:TextBox>
                                                            <asp:TextBox ID="txtGODate2" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtGODate2" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label10" runat="server" Text="Upload Budget Sanctioned GO" CssClass="form-label"></asp:Label>
                                                            <asp:FileUpload ID="flUploadGO" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <button class="btn btn-outline-success" onclick="return downloadGO(this);" title="Download GO" runat="server" id="aGO">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-download" viewBox="0 0 16 16">
                                                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5"></path>
                                                                    <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708z"></path>
                                                                </svg>
                                                            Download GO
                                                            </button>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label18" runat="server" Text="Project Type*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                     <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label19" runat="server" Text="Implementing Agency" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlImplAgency" runat="server" CssClass="form-select">
                                                                <asp:ListItem Value="" Text="-- Select Implementing Agency --" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="C&DS">C&DS</asp:ListItem>
                                                                <asp:ListItem Value="Irrigation Department">Irrigation Department</asp:ListItem>
                                                                <asp:ListItem Value="NN">NN</asp:ListItem>
                                                                <asp:ListItem Value="NP">NP</asp:ListItem>
                                                                <asp:ListItem Value="NPP">NPP</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label15" runat="server" Text="Physical Progress (%)" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtPhysicalTarget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
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
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Tender Detail</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblTenderAppDate" runat="server" Text="Tender Approval Date*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtTenderAppDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderAppDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblTenderIssueDate" runat="server" Text="Tender Issue Date*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtTenderIssueDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderIssueDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>


                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblTenderEndDate" runat="server" Text="Tender End Date*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtTenderEndDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1" TargetControlID="txtTenderEndDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblBidSecurityAmount" runat="server" Text="Bid Security Amount (In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtBidSecurityAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblTenderFileUpload" runat="server" Text="Upload Tender File" CssClass="form-label"></asp:Label>
                                                            <asp:FileUpload ID="fuTenderFileUpload" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <button class="btn btn-outline-success" onclick="return downloadTenderFile(this);" title="Download Tender File" runat="server" id="aTenderFile">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-download" viewBox="0 0 16 16">
                                                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5"></path>
                                                                    <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708z"></path>
                                                                </svg>
                                                            Download Tender File
                                                            </button>                                                                                                                                
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Work Order Detail</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblWorkOrderNo" runat="server" Text="Work Order No*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtWorkOrderNo" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblWorkOrderDate" runat="server" Text="Work Order Date*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtWorkOrderDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="cal_Theme1" TargetControlID="txtWorkOrderDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblWorkOrderCopy" runat="server" Text="Work Order Copy" CssClass="form-label"></asp:Label>
                                                            <asp:FileUpload ID="fuWorkOrderCopy" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <button class="btn btn-outline-success" onclick="return downloadWorkOrderCopy(this);" title="Download Order Copy File" runat="server" id="aOrderCopyFile">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-download" viewBox="0 0 16 16">
                                                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5"></path>
                                                                    <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708z"></path>
                                                                </svg>
                                                            Download Order Copy
                                                            </button>                                                                                                                                
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Contractor Detail</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblContractorName" runat="server" Text="Contractor Name*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtContractorName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">

                                                        <div>
                                                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">

                                                        <div>
                                                            <asp:Label ID="lblContactNo" runat="server" Text="Contact Number*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblEmailId" runat="server" Text="Email Id" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblContractorAddress" runat="server" Text="Contractor Address" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtContractorAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="display:none">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Funding Pattern Breakup (Grant / Loan)</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdFundingPattern" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFundingPattern_PreRender" OnRowDataBound="grdFundingPattern_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="FundingPattern_Id" HeaderText="FundingPattern_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="FundingPattern_Name" HeaderText="Funding Pattern Name" />
                                                                    <asp:TemplateField HeaderText="Percentage (%)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtShareP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" MaxLength="3" Text='<%#Eval("ProjectWorkFundingPattern_Percentage") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtShareV" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectWorkFundingPattern_Value") %>'></asp:TextBox>
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
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Fund Release Installment Details For Central, State and Other Share</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdCallProductDtls_PreRender" OnRowDataBound="grdCallProductDtls_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectWorkGO_Id" HeaderText="ProjectWorkGO_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GO Date">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtFinancialTrans_GO_Date" runat="server" CssClass="form-control" autocomplete="off" Text='<%# Eval("ProjectWorkGO_GO_Date") %>'></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1" TargetControlID="txtFinancialTrans_GO_Date" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GO Number">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtFinancialTrans_GO_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkGO_GO_Number") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Project Fund (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCentralShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_CentralShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="A&OE Fund (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtStateShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_StateShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Centage (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_Centage") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ULB / Other Share Released (In Lakhs)" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtULBShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_ULBShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload GO Document">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="flUploadGO" runat="server" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                            <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ProjectWorkGO_Document_Path" HeaderText="ProjectWorkGO_Document_Path">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Download Document">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkULBShr" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkGO_Document_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDeleteGO" OnClick="btnDeleteGO_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="25px" Height="25px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle Font-Bold="true" ForeColor="White" />
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

                                <div class="col-xxl-12 col-md-12">
                                    <div>
                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <asp:HiddenField ID="hf_ProjectWorkPkg_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_GO_Path" runat="server" Value="" />
                        <asp:HiddenField ID="hfTenderFileUploadPath" runat="server" Value="" />
                        <asp:HiddenField ID="hfWorkOrderCopyPath" runat="server" Value="" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                            <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                                <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function downloadGO(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_GO_Path').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }
        function downloadTenderFile(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hfTenderFileUploadPath').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        } 
        function downloadWorkOrderCopy(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hfWorkOrderCopyPath').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
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
