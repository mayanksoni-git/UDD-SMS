<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWork_DataEntry2.aspx.cs" Inherits="MasterProjectWork_DataEntry2" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
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
                                                <li class="breadcrumb-item active">Create / Update Project (ULB)</li>
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
                                                            <asp:Label ID="Label2" runat="server" Text="Sanctioned Cost (In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
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
                                                            <%--<button class="btn btn-danger btn-md" onclick="return downloadGO(this);" title="Download GO" runat="server" id="aGO">
                                                                <i class="ri-file-download-line icon-only"></i>
                                                            </button>--%>
                                                            <button type="button" class="btn btn-outline-success" onclick="return downloadGO(this);" title="Download GO" runat="server" id="aGO">
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
                                                                            <cc1:CalendarExtender ID="CalendarExtender11" runat="server" CssClass="cal_Theme1" TargetControlID="txtFinancialTrans_GO_Date" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GO Number">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtFinancialTrans_GO_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkGO_GO_Number") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Central Share (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCentralShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_CentralShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="State Share (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtStateShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_StateShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Centage (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_Centage") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ULB / Other Share Released (In Lakhs)">
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
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Agreement / Work Order Details      </h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label6" runat="server" Text="Agreement Amount(In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtAgreementAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label8" runat="server" Text="Date Of Start As Per Agreement *" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtAgreementDate" runat="server" CssClass="form-control" autocomplete="off" ></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1" TargetControlID="txtAgreementDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>

                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label9" runat="server" Text="Actual Date Of Start" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtActualDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1" TargetControlID="txtActualDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label4" runat="server" Text="Date Of Completion As Per Agreement" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1" TargetControlID="txtDueDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label11" runat="server" Text="Actual / Expected Date Of Completion" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtextenddate" runat="server" CssClass="form-control" autocomplete="off" ></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="cal_Theme1" TargetControlID="txtextenddate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label12" runat="server" Text="GST Type*" CssClass="form-label"></asp:Label>
                                                            <asp:RadioButtonList ID="rbtGSTType" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Exclude GST" Value="Exclude GST"></asp:ListItem>
                                                                <asp:ListItem Text="Include GST" Value="Include GST" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label16" runat="server" Text="Total Expenditure Till Date In Lakhs [Including GST]*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtExpenditureRABill" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <label class="form-label">Contractor (Lead)* </label>
                                                            <asp:DropDownList ID="ddlVendor1" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor1_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div runat="server" id="divVendorPAN">
                                                            <label class="form-label">PAN Of Lead Contractor * </label>
                                                            <asp:TextBox ID="txtLeadContractorPAN" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div runat="server" id="divVendorName">
                                                            <label class="form-label">Name Of Lead Contractor * </label>
                                                            <asp:TextBox ID="txtLeadContractorName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Button ID="btnAddAdditionalPackage" OnClick="btnAddAdditionalPackage_Click" Text="Add New Agreement / Work Order" runat="server" CssClass="btn-filter mt-4"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div style="display: none;">
                                                            <asp:Label ID="Label13" runat="server" Text="GST Percentage*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlGSTPercentage" runat="server" CssClass="form-select">
                                                                <asp:ListItem Text="5" Value="5.00"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12.00"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18.00" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28.00"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6" style="display: none">
                                                        <div>
                                                            <label class="form-label">Agreement / Work Order Code* </label>
                                                            <asp:TextBox ID="txtPackageCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6" style="display: none">
                                                        <div>
                                                            <label class="form-label">Agreement / Work Order Name* </label>
                                                            <asp:TextBox ID="txtPackageName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div style="display: none">
                                                            <asp:Label ID="Label7" runat="server" Text="Agreement No*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtAgreementNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdPackageDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender" OnRowDataBound="grdPackageDetails_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectWorkPkg_Work_Id" HeaderText="ProjectWorkPkg_Work_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Edit">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnPackageEdit" Width="20px" Height="20px" OnClick="btnPackageEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Agreement Amount(In Lakhs)" DataField="ProjectWorkPkg_AgreementAmount" />
                                                                    <asp:BoundField HeaderText="Date Of Start As Per Agreement" DataField="ProjectWorkPkg_Agreement_Date" />
                                                                    <asp:BoundField HeaderText="Actual Date Of Start" DataField="ProjectWorkPkg_Start_Date" />
                                                                    <asp:BoundField HeaderText="Date Of Completion As Per Agreement" DataField="ProjectWorkPkg_Due_Date" />
                                                                    <asp:BoundField HeaderText="Actual / Expected Date Of Completion" DataField="ProjectWorkPkg_ExtendDate" />
                                                                    <asp:BoundField HeaderText="PAN of Lead Contractor" DataField="ProjectWorkPkg_Lead_Vendor_PAN" />
                                                                    <asp:BoundField HeaderText="Name Of Lead Contractor" DataField="ProjectWorkPkg_Lead_Vendor_Name" />
                                                                    <asp:BoundField HeaderText="Total Expenditure Till Date In Lakhs [Including GST]" DataField="ProjectWorkPkg_PreviousRA" />
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnPackageDelete" Width="25px" Height="25px" OnClick="btnPackageDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
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
                                            <h4 class="card-title mb-0 flex-grow-1">Physical Component Progress Details</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdPhysicalProgress" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdPhysicalProgress_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="PhysicalProgressComponent_Id" HeaderText="PhysicalProgressComponent_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectPkg_PhysicalProgress_Id" HeaderText="ProjectPkg_PhysicalProgress_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PhysicalProgressComponent_EnableList" HeaderText="PhysicalProgressComponent_EnableList">
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
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAllApproveH" runat="server" Text="Select" AutoPostBack="True" OnCheckedChanged="chkSelectAllApproveH_CheckedChanged" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkPostPhysicalProgress" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="PhysicalProgressComponent_Component" HeaderText="Component" />
                                                                    <asp:BoundField DataField="Unit_Name" HeaderText="Unit" />
                                                                    <asp:TemplateField HeaderText="Proposed (Number) As Per Origional">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtProposedNumberO" runat="server" CssClass="form-control " Text='<%# Eval("ProjectPkg_PhysicalProgress_MasterValueF") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Proposed (Number) As Per Actual">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtProposedNumber" runat="server" CssClass="form-control " Text='<%# Eval("ProjectPkg_PhysicalProgress_MasterValue") %>' onkeyup="isNumericVal(this);" ToolTip='<%# Eval("ProjectPkg_PhysicalProgress_MasterValue") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed (Number)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtProgressNumber" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_PhysicalProgress") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_Remarks") %>' TextMode="MultiLine"></asp:TextBox>
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
                                            <h4 class="card-title mb-0 flex-grow-1">Upload Utilization Certificate</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdUC" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdUC_PreRender" OnRowDataBound="grdUC_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectUC_Id" HeaderText="ProjectUC_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectUC_Document" HeaderText="ProjectUC_Document">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UC Date">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtUCDate" runat="server" CssClass="form-control" autocomplete="off" Text='<%# Eval("ProjectUC_SubmitionDate") %>'></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender21" runat="server" CssClass="cal_Theme1" TargetControlID="txtUCDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UC Number">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtUC_Number" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_Comments") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="% Utilization against Released Amount">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtUCP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Text='<%# Eval("ProjectUC_Achivment") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload UC Document">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="flUploadUC" runat="server" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="btnAddUC" OnClick="btnAddUC_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                            <asp:ImageButton ID="imgdeleteUC" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdeleteUC_Click" Width="30px" Height="30px" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload UC Document">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkUCDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectUC_Document") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDeleteUC" OnClick="btnDeleteUC_Click" runat="server" ImageUrl="~/assets/images/delete.png" ToolTip="Click to delete" Width="25px" Height="25px" />
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
                                <div class="col-xs-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Project Related Issues (If Any)</h4>
                                            <asp:CheckBox runat="server" ID="chkNoIssue" Text="Select If No Issue." Font-Bold="true" Font-Size="16px" Style="float: right" />
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdIssue" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdIssue_PreRender" OnRowDataBound="grdIssue_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Id" HeaderText="ProjectWorkIssueDetails_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Path" HeaderText="ProjectWorkIssueDetails_Path">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Dependency_Id" HeaderText="ProjectWorkIssueDetails_Dependency_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblRowNumber" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Issuing Type">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueType_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sub Issue">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlDependency" runat="server" CssClass="form-select">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Discription" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtIssuingCategory" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkIssueDetails_Category") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Issuing Effective Date">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtIssuingDate" runat="server" CssClass="form-control" autocomplete="off" Text='<%# Eval("ProjectWorkIssueDetails_Date") %>'></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender31" runat="server" CssClass="cal_Theme1" TargetControlID="txtIssuingDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Comments">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Eval("ProjectWorkIssueDetails_Comments") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="btnQuestionnaireU" OnClick="btnQuestionnaireU_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                            <asp:ImageButton ID="btnDeleteQuestionnaireU" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnDeleteQuestionnaireU_Click" Width="30px" Height="30px" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload Issue Document" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="flUploadIssue" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Download Document" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkIssueDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkIssueDetails_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Resolved Date">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtResolvedDate" runat="server" CssClass="form-control" autocomplete="off" Text='<%# Eval("ProjectWorkIssueDetails_DateResolved") %>'></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender32" runat="server" CssClass="cal_Theme1" TargetControlID="txtResolvedDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Resolved">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDeleteIssue" ToolTip="Resolved" OnClick="btnDeleteIssue_Click" runat="server" ImageUrl="~/assets/images/resolved.jpg" Width="30px" Height="30px" />
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
                        <asp:HiddenField ID="hf_GO_Path" runat="server" Value="0" />
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
