<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterEMB_New.aspx.cs" Inherits="MasterEMB_New" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpViewBOQ_Breakup" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">

                                <div class="table-header">
                                    Select A Work Package TO Create EMB
                                    
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Scheme </label>
                                        <asp:DropDownList ID="ddlSearchScheme" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-3" id="divCircle" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divDivision" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>

                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnCreateOld" Text="Create MB In Old Format" OnClick="btnCreateOld_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h3 class="header smaller lighter blue">Project Package List</h3>

                                            <!-- div.table-responsive -->
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_ULB_Id" HeaderText="ProjectWork_ULB_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkPkg_LastRABillNo" HeaderText="ProjectWorkPkg_LastRABillNo">
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
                                                                <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Project" DataField="Project_Name" />
                                                        <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                        <asp:BoundField HeaderText="Budget" DataField="ProjectWork_Budget" />
                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                        <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                        <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                        <asp:BoundField HeaderText="Due Date Of Completion" DataField="ProjectWorkPkg_Due_Date" />
                                                        <asp:BoundField HeaderText="Vendor / Contractor" DataField="Vendor_Name" />
                                                        <asp:BoundField HeaderText="Vendor / Contractor (Mobile)" DataField="Vendor_Mobile" />
                                                        <asp:BoundField HeaderText="Reporting Staff (JE / APE)" DataField="List_ReportingStaff_JEAPE_Name" />
                                                        <asp:BoundField HeaderText="Reporting Staff (AE / PE)" DataField="List_ReportingStaff_AEPE_Name" />
                                                        <asp:BoundField HeaderText="Total BOQ Count" DataField="Total_BOQ" />
                                                        <asp:TemplateField HeaderText="Invoice">
                                                            <ItemTemplate>
                                                                <a href="MasterGenerateInvoice_View.aspx?Package_Id=<%# Eval("ProjectWorkPkg_Id") %>&Invoice_Id=0" target="_blank">View Invoice</a>
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

                        <div id="divEntry" runat="server" visible="false">

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Details Of EMB Items
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdEMB" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdEMB_PreRender" OnRowDataBound="grdEMB_RowDataBound" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="PackageEMB_Id" HeaderText="PackageEMB_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageBOQ_Id" HeaderText="PackageBOQ_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMB_Package_Id" HeaderText="PackageEMB_Package_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMB_Unit_Id" HeaderText="PackageEMB_Unit_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMB_Approval_Id" HeaderText="PackageEMB_Approval_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageBOQ_RateEstimated" HeaderText="PackageBOQ_RateEstimated">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageBOQ_RateQuoted" HeaderText="PackageBOQ_RateQuoted">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMB_PackageEMB_Master_Id" HeaderText="PackageEMB_PackageEMB_Master_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTType" HeaderText="GSTType">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTPercenatge" HeaderText="GSTPercenatge">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PackageEMB_PackageBOQ_OrderNo" HeaderText="PackageEMB_PackageBOQ_OrderNo">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PercentageValuePaidTillDate" HeaderText="PercentageValuePaidTillDate">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <br />
                                                            <asp:ImageButton ID="btnViewDetails" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" ToolTip="View Details" OnClick="btnViewDetails_Click"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description Of Goods">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecification" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageEMB_Specification") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" Enabled="false" Width="60px"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageBOQ_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PackageBOQ_RateEstimated_T" HeaderText="Rate Estimated (Excluding GST)" />
                                                    <asp:BoundField DataField="PackageBOQ_RateQuoted_T" HeaderText="Rate Quoted (Excluding GST)" />
                                                    <asp:BoundField DataField="PackageBOQ_QtyPaid" HeaderText="Quantity executed Since Previous" />
                                                    <asp:BoundField DataField="PackageEMB_Amount_UpToDate" HeaderText="Payment on the basis of actual measurements (Since Previous)" />
                                                    <asp:TemplateField HeaderText="Quantity executed up to date as per MB">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_QtyPaid") %>' Width="80px" MaxLength="10" onkeyup="isNumericVal(this);" onchange="return calculateQty(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage Amount To Be Released">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPercentageToBeReleased" runat="server" CssClass="form-control" MaxLength="11" onkeyup="isNumericVal(this);" Text='<%# Eval("PackageEMB_PercentageToBeReleased_N") %>' onchange="return calculateQty(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="" HeaderText="Payment on the basis of actual measurements (Up To Date)" />
                                                    <asp:BoundField DataField="" HeaderText="Quantity executed (As Per Current MB)" />
                                                    <asp:BoundField DataField="" HeaderText="GST" />
                                                    <asp:BoundField DataField="" HeaderText="Payment (As Per Current MB)" />
                                                </Columns>
                                                <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="RA Bill No" CssClass="control-label no-padding-right"></asp:Label>
                                            <%--<asp:TextBox ID="txtRABillNo" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlRABillNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lblDeduction" runat="server" Text="MB Ref. No (Physical)" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtMB_No" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="MB Date" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtMBDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:CheckBox ID="chkOverAllGST" runat="server" CssClass="form-control" Font-Bold="true" Text="Apply Over All GST"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-12">
                                        <span class="label label-danger arrowed">
                                            <i class="ace-icon fa fa-angle-double-right"></i>
                                            MB With Same RA Bill Number will be combined into One Invoice After Approval.
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDeductions" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductions_PreRender" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="Deduction_Id" HeaderText="Deduction_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Deduction" DataField="Deduction_Name" />
                                                <asp:TemplateField HeaderText="Deduction Type">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rblDeductionType" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="%" Value="Per" Selected="True" />
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduction">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeductionValue" runat="server" CssClass="form-control" Width="80px" MaxLength="6" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Amount" DataField="" />
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdProjectStatus" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProjectStatus_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWorkPkg_Work_Id" HeaderText="ProjectWorkPkg_Work_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Overall_Status" HeaderText="Overall_Status">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Data For" DataField="Type" />
                                                <asp:BoundField HeaderText="Budget" DataField="Budget" />
                                                <asp:BoundField HeaderText="Agreement Amount" DataField="AgreementAmount" />
                                                <asp:BoundField HeaderText="Total Packages" DataField="Total_Packages" />
                                                <asp:BoundField HeaderText="Central Share" DataField="Central_Share" />
                                                <asp:BoundField HeaderText="State Share" DataField="State_Share" />
                                                <asp:BoundField HeaderText="ULB Share" DataField="ULB_Share" />
                                                <asp:BoundField HeaderText="Total Invoice" DataField="Total_Invoice" />
                                                <asp:BoundField HeaderText="Total Invoice Value" DataField="Total_Value" />
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnCalculate" Text="Calculate Total" OnClick="btnCalculate_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <%--<asp:Button ID="btnSaveEMB" Text="Save EMB" OnClick="btnSaveEMB_Click" runat="server" CssClass="btn btn-warning" Visible="false"></asp:Button>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSaveAndForward" Text="Save EMB and Forward For Approval" OnClick="btnSaveAndForward_Click" runat="server" CssClass="btn btn-info" Visible="false"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnEdit" Text="Edit EMB" OnClick="btnEdit_Click1" runat="server" CssClass="btn btn-round" Visible="false"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdBOQItemBreakup" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBOQItemBreakup_PreRender" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="PackageBOQ_Id" HeaderText="PackageBOQ_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageEMB_Id" HeaderText="PackageEMB_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="MB Date" DataField="PackageEMB_Master_Date" />
                                                <asp:BoundField HeaderText="Physical MB No" DataField="PackageEMB_Master_VoucherNo" />
                                                <asp:BoundField HeaderText="RA Bill No" DataField="PackageEMB_Master_RA_BillNo" />
                                                <asp:BoundField HeaderText="Total Qty Paid" DataField="PackageBOQ_QtyPaid" />
                                                <asp:BoundField HeaderText="Percentage Paid" DataField="PackageBOQ_PercentageValuePaidTillDate" />
                                                <asp:BoundField HeaderText="Amount Paid" DataField="PackageBOQ_AmountPaid" />
                                                <asp:TemplateField HeaderText="Total Qty Paid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyP" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_QtyPaid") %>' Width="80px" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Percentage Paid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPerP" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_PercentageValuePaidTillDate") %>' Width="80px" MaxLength="10"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount Paid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountP" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_AmountPaid") %>' Width="80px" MaxLength="10"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <span class="label label-danger arrowed" id="sp_NewFormat" runat="server">MB Created In New Format: No</span>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnUpdateBOQ" runat="server" Text="Update" CssClass="btn btn-round" OnClick="btnUpdateBOQ_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ProjectWorkPkg_LastRABillNo" runat="server" Value="0" />                        
                        <asp:HiddenField ID="hf_Total_Amount_UptoDate" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <%--<script src="assets/js/dataTables.colReorder.min.js"></script>--%>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPost').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPost')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPost')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: false,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
                                    ],
                                    "aaSorting": [],
                                    //"bProcessing": true,
                                    //"bServerSide": true,
                                    //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                    //,
                                    //"sScrollY": "200px",
                                    //"bPaginate": false,
                                    //"sScrollX": "100%",
                                    //"sScrollXInner": "120%",
                                    //"bScrollCollapse": true,
                                    //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                    //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                    "iDisplayLength": 100,
                                    select: {
                                        style: 'multi'
                                    }
                                });
                        $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
                        new $.fn.dataTable.Buttons(myTable, {
                            buttons: [
                                {
                                    "extend": "colvis",
                                    "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    columns: ':not(:first):not(:last)'
                                },
                                {
                                    "extend": "copy",
                                    "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "csv",
                                    "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "excel",
                                    "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "pdf",
                                    "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "print",
                                    "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    autoPrint: true,
                                    message: 'This print was produced using the Print button for DataTables',
                                    exportOptions: {
                                        columns: ':visible'
                                    }
                                }
                            ]
                        });
                        myTable.buttons().container().appendTo($('.tableTools-container'));

                        //style the message box
                        var defaultCopyAction = myTable.button(1).action();
                        myTable.button(1).action(function (e, dt, button, config) {
                            defaultCopyAction(e, dt, button, config);
                            $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
                        });
                        var defaultColvisAction = myTable.button(0).action();
                        myTable.button(0).action(function (e, dt, button, config) {

                            defaultColvisAction(e, dt, button, config);
                            if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                                $('.dt-button-collection')
                                    .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                                    .find('a').attr('href', '#').wrap("<li />")
                            }
                            $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.tableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPost .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have TableTools or dataTables
                        //select/deselect all rows according to table header checkbox
                        var active_class = 'active';
                        /********************************/
                        //add tooltip for small view action buttons in dropdown menu
                        $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

                        //tooltip placement on right or left
                        function tooltip_placement(context, source) {
                            var $source = $(source);
                            var $parent = $source.closest('table')
                            var off1 = $parent.offset();
                            var w1 = $parent.width();

                            var off2 = $source.offset();
                            //var w2 = $source.width();

                            if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                            return 'left';
                        }
                        /***************/
                        $('.show-details-btn').on('click', function (e) {
                            e.preventDefault();
                            $(this).closest('tr').next().toggleClass('open');
                            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
                        });
                    }
                }
            })
        });
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

        var Rate_Type = 1;

        function calculateQty(obj) {
            var id = obj.id;
            var Total_Amount_UptoDate = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hf_Total_Amount_UptoDate").value);
            var row = obj.parentNode.parentNode;
            var txtQty = id.replace('txtQty', '').replace('txtPercentageToBeReleased', '') + 'txtQty';
            var txtPercentageToBeReleased = id.replace('txtQty', '').replace('txtPercentageToBeReleased', '') + 'txtPercentageToBeReleased';
            var GST = 0;
            try {
                GST = parseFloat(row.cells[9].innerHTML);
            }
            catch (ee) {
                GST = 0;
            }
            var Rate_Estimate = 0;
            try {
                Rate_Estimate = parseFloat(row.cells[16].innerHTML);
            }
            catch (ee) {
                Rate_Estimate = 0;
            }

            var Rate_Quoted = 0;
            try {
                Rate_Quoted = parseFloat(row.cells[17].innerHTML);
            }
            catch (ee) {
                Rate_Quoted = 0;
            }

            var Payment_Since_Previous = 0;
            try {
                Payment_Since_Previous = parseFloat(row.cells[19].innerHTML);
            }
            catch (ee) {
                Payment_Since_Previous = 0;
            }

            var Qty_Previous = 0;
            try {
                Qty_Previous = parseFloat(row.cells[18].innerHTML);
            }
            catch (ee) {
                Qty_Previous = 0;
            }

            var Quantity_executed_up_to_date = 0;
            try {
                Quantity_executed_up_to_date = parseFloat(document.getElementById(txtQty).value);
            }
            catch (ee) {
                Quantity_executed_up_to_date = 0;
            }
            var Percentage_Prev = 0;
            try {
                Percentage_Prev = parseFloat(row.cells[11].innerHTML);
            }
            catch (ee) {
                Percentage_Prev = 0;
            }
            var Percentage = 0;
            try {
                Percentage = parseFloat(document.getElementById(txtPercentageToBeReleased).value);
            }
            catch (ee) {
                Percentage = 0;
            }
            var Current_Qty = 0;
            if (Percentage_Prev == Percentage && Quantity_executed_up_to_date == Qty_Previous) {
                Current_Qty = 0;
                row.cells[22].innerHTML = Payment_Since_Previous;
            }
            else {
                Current_Qty = Quantity_executed_up_to_date - Qty_Previous;
                if (Rate_Type == 1) {
                    row.cells[22].innerHTML = ((Quantity_executed_up_to_date * Rate_Quoted * Percentage) / 100).toFixed(2);
                }
                else {
                    row.cells[22].innerHTML = ((Quantity_executed_up_to_date * Rate_Estimate * Percentage) / 100).toFixed(2);
                }
            }
            debugger;
            Total_Amount_UptoDate += (parseFloat(row.cells[22].innerHTML) - Payment_Since_Previous).toFixed(2);
            row.cells[23].innerHTML = Current_Qty.toFixed(4);
            row.cells[25].innerHTML = (parseFloat(row.cells[22].innerHTML) - Payment_Since_Previous).toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_hf_Total_Amount_UptoDate").value = Total_Amount_UptoDate;
            if (Rate_Type == 1) {
                row.cells[24].innerHTML = (row.cells[25].innerHTML * GST / 100).toFixed(2);
            }
            else {
                row.cells[24].innerHTML = 0;
            }
            //calculate_Total_MB_Value();
        }

        //function re_Calculate_Total(obj) {
        //    var id = obj.id;
        //    var Quated_Rate = 0;
        //    try {
        //        Quated_Rate = parseFloat(obj.value);
        //    }
        //    catch (ee) {
        //        Quated_Rate = 0;
        //    }
        //    if (Quated_Rate == 0) {
        //        Rate_Type = 1;
        //    }
        //    else {
        //        Rate_Type = 0;
        //    }
        //    var grid_Table = document.getElementById('ctl00_ContentPlaceHolder1_grdEMB');
        //    var grid_Rows = grid_Table.getElementsByTagName("tr");
        //    for (var i = 1; i < grid_Rows.length; i++) {
        //        var row = grid_Rows[i];

        //        var txtQty = "ctl00_ContentPlaceHolder1_grdEMB_ctl0" + (i + 1) + "_txtQty";
        //        var txtPercentageToBeReleased = "ctl00_ContentPlaceHolder1_grdEMB_ctl0" + (i + 1) + "_txtPercentageToBeReleased";
        //        var GST = 0;
        //        try {
        //            GST = parseFloat(row.cells[9].innerHTML);
        //        }
        //        catch (ee) {
        //            GST = 0;
        //        }
        //        var Rate_Estimate = 0;
        //        try {
        //            Rate_Estimate = parseFloat(row.cells[16].innerHTML);
        //        }
        //        catch (ee) {
        //            Rate_Estimate = 0;
        //        }

        //        var Rate_Quoted = 0;
        //        try {
        //            Rate_Quoted = parseFloat(row.cells[17].innerHTML);
        //        }
        //        catch (ee) {
        //            Rate_Quoted = 0;
        //        }

        //        var Payment_Since_Previous = 0;
        //        try {
        //            Payment_Since_Previous = parseFloat(row.cells[19].innerHTML);
        //        }
        //        catch (ee) {
        //            Payment_Since_Previous = 0;
        //        }

        //        var Qty_Previous = 0;
        //        try {
        //            Qty_Previous = parseFloat(row.cells[18].innerHTML);
        //        }
        //        catch (ee) {
        //            Qty_Previous = 0;
        //        }

        //        var Quantity_executed_up_to_date = 0;
        //        try {
        //            Quantity_executed_up_to_date = parseFloat(document.getElementById(txtQty).value);
        //        }
        //        catch (ee) {
        //            Quantity_executed_up_to_date = 0;
        //        }

        //        var Percentage = 0;
        //        try {
        //            Percentage = parseFloat(document.getElementById(txtPercentageToBeReleased).value);
        //        }
        //        catch (ee) {
        //            Percentage = 0;
        //        }
        //        if ($.isNumeric(Quantity_executed_up_to_date) && Quantity_executed_up_to_date != 0) {
        //            var Current_Qty = Quantity_executed_up_to_date - Qty_Previous;
        //            if (Rate_Type == 1) {
        //                row.cells[22].innerHTML = ((Quantity_executed_up_to_date * Rate_Quoted * Percentage) / 100).toFixed(2);
        //            }
        //            else {
        //                row.cells[22].innerHTML = ((Quantity_executed_up_to_date * Rate_Estimate * Percentage) / 100).toFixed(2);
        //            }
        //            row.cells[23].innerHTML = Current_Qty.toFixed(4);
        //            row.cells[25].innerHTML = (parseFloat(row.cells[22].innerHTML) - Payment_Since_Previous).toFixed(2);
        //            if (Rate_Type == 1) {
        //                row.cells[24].innerHTML = (row.cells[25].innerHTML * GST / 100).toFixed(2);
        //            }
        //            else {
        //                row.cells[24].innerHTML = 0;
        //            }
        //        }
        //    }
        //    calculate_Total_MB_Value();
        //}

        //function calculate_Total_MB_Value() {
        //    var totalVal = 0;
        //    var grid_Table = document.getElementById('ctl00_ContentPlaceHolder1_grdEMB');
        //    var grid_Rows = grid_Table.getElementsByTagName("tr");
        //    for (var i = 1; i < grid_Rows.length; i++) {
        //        var row = grid_Rows[i];
        //        var current_total = 0;
        //        var current_gst = 0;
        //        try {
        //            current_gst = parseFloat(row.cells[24].innerHTML);
        //            current_total = parseFloat(row.cells[25].innerHTML);
        //            if ($.isNumeric(current_total)) {

        //            }
        //            else {
        //                current_total = 0;
        //            }
        //            if ($.isNumeric(current_gst)) {

        //            }
        //            else {
        //                current_gst = 0;
        //            }
        //            totalVal = totalVal + current_total + current_gst;
        //        }
        //        catch
        //        {
        //            totalVal += 0;
        //        }
        //    }
        //    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value = totalVal.toFixed(2);
        //}
    </script>
</asp:Content>

