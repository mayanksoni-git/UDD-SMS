<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWork_DataEntry.aspx.cs" Inherits="MasterProjectWork_DataEntry" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <div id="divCreate" runat="server">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Project Details
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">District* </label>
                                            <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">ULB </label>
                                            <asp:DropDownList ID="ddlULB" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Block / Town </label>
                                            <asp:DropDownList ID="ddlBlock" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Project Name*</label>
                                            <asp:TextBox ID="txtProjectWorkName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label25" runat="server" Text="GO No" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtGONo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label26" runat="server" Text="GO Date*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtGODate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Origional Sanctioned Cost Including GST & Centage (In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" runat="server" Text="Revised Sanctioned Cost Including GST & Centage [If Any] (In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtBudgetR" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Centage (In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Nodal Department" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlNodalDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNodalDepartment_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label19" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlNodalDeptScheme" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNodalDeptScheme_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divNodalDeptScheme" runat="server" visible="false">
                                        <div class="form-group">
                                            <asp:Label ID="Label20" runat="server" Text="Create New Scheme*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtNodalDeptScheme" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Project Code* </label>
                                            <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-9" style="background-color: gold">
                                        <div class="form-group">
                                            <asp:Label ID="Label17" runat="server" Text="Other Dtls (Multiple Selection Allowed)" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:CheckBoxList ID="chkNGT" runat="server" RepeatColumns="9" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="&nbsp;Not Applicable&nbsp;&nbsp;" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;Under NGT&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;DCU Pattern Work&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;CM Ghoshna&nbsp;&nbsp;" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;MSDP&nbsp;&nbsp;" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;eTender&nbsp;&nbsp;" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;eProcurement&nbsp;&nbsp;" Value="6"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label15" runat="server" Text="Physical Progress (%)" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtPhysicalTarget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Total Released Amount Till Date (In Lakhs) [Except any Moblization Advance Released by HQ]*</label>
                                            <asp:TextBox ID="txtTotalReleseAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Any Moblization Advance Released by HQ Till Date (In Lakhs)*</label>
                                            <asp:TextBox ID="txtTotalReleseMA" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3" style="background-color: burlywood">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right" style="font-weight: bold">Project Sanctioned During Tax Regime (Please Select One)*</label>
                                            <asp:RadioButtonList ID="rbtTaxRegime" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="During VAT" Value="V"></asp:ListItem>
                                                <asp:ListItem Text="During 12% GST" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="During 18% GST" Value="18"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="col-md-3" style="background-color: lightgoldenrodyellow">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkPhysicalCompleted" Text="Physical Completed With Reduced Scope." Font-Bold="true" runat="server"></asp:CheckBox>
                                            <br />
                                            <asp:CheckBox ID="chkNewProjects" Text="Currently Under Tendering (New Project)" Font-Bold="true" runat="server"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="background-color: lightgoldenrodyellow">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right" style="font-weight: bold">Plysically Closure Date</label>
                                            <asp:TextBox ID="txtPhysicallyClosed" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="background-color: lightgoldenrodyellow">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right" style="font-weight: bold">Financially Closure Date</label>
                                            <asp:TextBox ID="txtFinanciallyClosureDate" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Agreement / Work Order Details                               
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" Text="Agreement Amount(In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtAgreementAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Text="Date Of Start As Per Agreement *" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtAgreementDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Text="Actual Date Of Start" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtActualDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Text="Date Of Completion As Per Agreement" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" Text="Actual / Expected Date Of Completion" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtextenddate" runat="server" CssClass="form-control date-picker"
                                                autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" Text="GST Type*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:RadioButtonList ID="rbtGSTType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Exclude GST" Value="Exclude GST"></asp:ListItem>
                                                <asp:ListItem Text="Include GST" Value="Include GST" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label16" runat="server" Text="Total Expenditure Till Date In Lakhs [Including GST]*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtExpenditureRABill" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Contractor (Lead)* </label>
                                            <asp:DropDownList ID="ddlVendor1" runat="server" class="chosen-select form-control" data-placeholder="Choose a Contractor / Vendor..." AutoPostBack="true" OnSelectedIndexChanged="ddlVendor1_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-md-3" runat="server" id="divVendorPAN">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">PAN Of Lead Contractor * </label>
                                            <asp:TextBox ID="txtLeadContractorPAN" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" id="divVendorName">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Name Of Lead Contractor * </label>
                                            <asp:TextBox ID="txtLeadContractorName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnAddAdditionalPackage" OnClick="btnAddAdditionalPackage_Click" Text="Add New Agreement / Work Order" runat="server" CssClass="btn btn-info"></asp:Button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3" style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label13" runat="server" Text="GST Percentage*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlGSTPercentage" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="5" Value="5.00"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12.00"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18.00" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="28" Value="28.00"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="display: none">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Agreement / Work Order Code* </label>
                                            <asp:TextBox ID="txtPackageCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="display: none">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Agreement / Work Order Name* </label>
                                            <asp:TextBox ID="txtPackageName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="display: none">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Agreement No*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtAgreementNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
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
                                                        <asp:ImageButton ID="btnPackageDelete" Width="20px" Height="30px" OnClick="btnPackageDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div style="display: none;">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Fund Release Installment Details For Central, State and Other Share                                  
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div style="overflow: auto">
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
                                                                    <asp:TextBox ID="txtFinancialTrans_GO_Date" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkGO_GO_Date") %>'></asp:TextBox>
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
                                                                    <asp:ImageButton ID="btnDeleteGO" OnClick="btnDeleteGO_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Project Related Issues (If Any)

                                        <div class="blink" style="float: right">
                                            <asp:CheckBox runat="server" ID="chkNoIssue" Text="Select If No Issue." ForeColor="Yellow" Font-Bold="true" Font-Size="30px" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
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
                                                        <asp:TemplateField HeaderText="Type Of Issue">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueType_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Issue">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlDependency" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Discription" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuingCategory" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkIssueDetails_Category") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Effective Start Date of Issue">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuingDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkIssueDetails_Date") %>'></asp:TextBox>
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
                                                        <asp:TemplateField HeaderText="Date of Resolution">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtResolvedDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkIssueDetails_DateResolved") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Resolved">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDeleteIssue" OnClick="btnDeleteIssue_Click" runat="server" ImageUrl="~/assets/images/resolved.jpg" Width="50px" Height="50px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Upload Utilization Certificate                                
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
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
                                                                <asp:TextBox ID="txtUCDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectUC_SubmitionDate") %>'></asp:TextBox>
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
                                                                <asp:ImageButton ID="btnDeleteUC" OnClick="btnDeleteUC_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Upload GEO Tagged Images / Photographs In Project Gallery 
                       
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label21" runat="server" Text="View Photograph 1" CssClass="control-label no-padding-right"></asp:Label>
                                            <br />
                                            <asp:Image runat="server" ID="imgPhoto1" Width="400px" Height="500px" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label22" runat="server" Text="View Photograph 2" CssClass="control-label no-padding-right"></asp:Label>
                                            <br />
                                            <asp:Image runat="server" ID="imgPhoto2" Width="400px" Height="500px" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label23" runat="server" Text="Upload Photograph 1" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:FileUpload runat="server" ID="flPhotoUpload1" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label24" runat="server" Text="Upload Photograph 2" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:FileUpload runat="server" ID="flPhotoUpload2" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Salient Features About The Projects (Max 3 Min 2)
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdSalientFeatures" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdSalientFeatures_PreRender">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectSalientFeatures_Id" HeaderText="ProjectSalientFeatures_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Heading">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtHeading" runat="server" CssClass="form-control" Text='<%# Eval("ProjectSalientFeatures_Heading") %>' Width="200px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Salient Features Description">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSalientFeatures" runat="server" CssClass="form-control" Text='<%# Eval("ProjectSalientFeatures_Comments") %>' Width="900px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Updation Of Shilanyas / Lokarpan
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="table-header">
                                            शिलान्यास
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-header">
                                            लोकार्पण
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rbtShilanyas" runat="server" RepeatDirection="Vertical">
                                                <asp:ListItem Text="किया जा चूका है" Value="SD"></asp:ListItem>
                                                <asp:ListItem Text="किया जा सकता है" Value="SP"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rbtLokarpan" runat="server" RepeatDirection="Vertical">
                                                <asp:ListItem Text="किया जा चूका है" Value="LD"></asp:ListItem>
                                                <asp:ListItem Text="किया जा सकता है" Value="LP"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 200px; margin-left: -32px" ScrollBars="Auto">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="table-header">
                                            Instructions
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="alert alert-danger">
                                            <strong>
                                                <i class="ace-icon fa fa-times"></i>
                                                Pleae Note!
                                            </strong>

                                            All Financial Data Should be Entered In Lakhs Only.
										
                                <br>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField ID="hf_ProjectWorkPkg_Id" runat="server" Value="0" />
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

</asp:Content>



