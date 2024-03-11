<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkCertificate.aspx.cs" Inherits="MasterProjectWorkCertificate" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Project List
                                           
                                            <div class="form-group" style="float: right; padding-right: 10px">
                                                <asp:RadioButtonList ID="rbtMappingWith" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtMappingWith_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="Project For Division" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="Project For ULB" Value="U"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </h3>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Scheme </label>
                                                        <asp:ListBox ID="ddlScheme" runat="server" SelectionMode="Multiple" class="chosen-select form-control"
                                                            data-placeholder="Choose a Scheme..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divZone" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDistrict" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">District* </label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divCircle" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divULB" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDivision" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Project Code </label>
                                                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <!-- div.table-responsive -->
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                <Columns>
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
                                                    <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                    <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                    <asp:BoundField HeaderText="Start Date As Per GO" DataField="ProjectWork_StartDate" />
                                                    <asp:BoundField HeaderText="End Date As Per GO" DataField="ProjectWork_EndDate" />
                                                    <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                                    <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                                    <asp:BoundField HeaderText="Agreement Cost (In Lakhs)" DataField="tender_cost" />
                                                    <asp:BoundField HeaderText="Start Date As Per Agreement" DataField="ProjectWorkPkg_Agreement_Date" />
                                                    <asp:BoundField HeaderText="End Date As Per Agreement" DataField="ProjectWorkPkg_Due_Date" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                        <div runat="server" id="divPackage" visible="false">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="table-header">
                                                        Package Details                               
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdPackageDetails" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender">
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
                                                                    <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
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
                                                                            <asp:ImageButton ID="btnPackageEdit" Width="20px" Height="20px" OnClick="btnPackageEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                                    <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                                    <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                                    <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                                    <asp:BoundField HeaderText="Agreement Date" DataField="ProjectWorkPkg_Agreement_Date" />
                                                                    <asp:BoundField HeaderText="Date Of Completion" DataField="ProjectWorkPkg_Due_Date" />
                                                                    <asp:BoundField HeaderText="Vendor / Contractor" DataField="Vendor_Name" />
                                                                    <asp:BoundField HeaderText="Vendor / Contractor (Mobile)" DataField="Vendor_Mobile" />
                                                                    <asp:BoundField HeaderText="Reporting Staff (JE / APE)" DataField="List_ReportingStaff_JEAPE_Name" />
                                                                    <asp:BoundField HeaderText="Reporting Staff (AE / PE)" DataField="List_ReportingStaff_AEPE_Name" />
                                                                    <asp:BoundField HeaderText="Date Of Completion (Extended)" DataField="ProjectWorkPkg_ExtendDate" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:Button ID="btnGeneratecombinedCertificate" Text="Generate Combined Certificate" OnClick="btnGeneratecombinedCertificate_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                </div>
                                            </div>
                                        </div>

                                        <div runat="server" id="divCertificateDetails" visible="false">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 40%;">Description</th>
                                                                <th style="width: 60%;">Response</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td runat="server" id="Q1">Name of work (brief particulars of work):</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNameWork" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td5">Vendor As Per PMIS:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtVendorPMIS" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td6">Vendor Type:</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rbtVendorType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtVendorType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Single Vendor Only" Value="L" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Joint Venture" Value="P"></asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td7">Lead Vendor:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLeadVendor" runat="server" CssClass="form-control" placeholder="Vendor Name"></asp:TextBox>

                                                                    <asp:TextBox ID="txtLeadVendorShare" runat="server" CssClass="form-control" placeholder="Share (%)" Visible="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="trPartner1" visible="false">
                                                                <td runat="server" id="Td8">Partner Vendor (In Case Of JV):</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPartnerVendor1" runat="server" CssClass="form-control" placeholder="Partner Vendor Name"></asp:TextBox>

                                                                    <asp:TextBox ID="txtPartnerVendorShare1" runat="server" CssClass="form-control" placeholder="Partner Share (%)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="trPartner2" visible="false">
                                                                <td runat="server" id="Td11">Partner Vendor (In Case Of JV):</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPartnerVendor2" runat="server" CssClass="form-control" placeholder="Partner Vendor Name"></asp:TextBox>

                                                                    <asp:TextBox ID="txtPartnerVendorShare2" runat="server" CssClass="form-control" placeholder="Partner Share (%)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q2">Agreement No:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAgreementNo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q3">Agreement Date:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAgreementDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q4">Date of commencement of work:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q5">Stipulated date of completion:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q6">Actual Date of completion:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEndDateExtended" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q6A">Testing and Commissioned Date:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTestingCommissionedDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="D7">Details of compensation of levied for delay, if any:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLDApplied" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q8">Tendered amount:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTenderCost" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q9">Gross amount of the work completed:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrossAmountCompleted" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q10">Name and address of the authority under whom works executed:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactDetails" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q11">Whether the contractor employed qualified Engineer / Overseer during execution of work?:</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rbtQualifiedStaff" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q12">Quality of work (indicate grading):</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlQualityWork" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Outstanding" Value="Outstanding"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Good" Value="Very Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                                                        <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q13">Amount of work paid on reduced rate basis if any:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAmountPaidReducedRate" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q14">Did the contractor go for arbitration:</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rbtArbitration" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q15">If yes, total amount of claim:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtClaimAmount" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q16">Total amount awarded:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAwardedAmount" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr style="background-color: darkgray;">
                                                                <td runat="server" id="Q17A" colspan="2" style="font-weight: bold;">Comments on the capacities of the contractor:</td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q17">Technical Proficiency:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTechProficiency" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Outstanding" Value="Outstanding"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Good" Value="Very Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                                                        <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q18">Financial soundness:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlFinancialSounness" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Outstanding" Value="Outstanding"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Good" Value="Very Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                                                        <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q19">Mobilization of adequate T&P:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMobAdequate" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Outstanding" Value="Outstanding"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Good" Value="Very Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                                                        <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q20">Mobilization of manpower:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMobManpower" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Outstanding" Value="Outstanding"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Good" Value="Very Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                                                        <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Q21">General behavior:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBehavior" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Outstanding" Value="Outstanding"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Good" Value="Very Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                                                        <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr style="background-color: darkgray;">
                                                                <td runat="server" id="Td9" colspan="2" style="font-weight: bold;">Any Additional Comments / Remarks (Will be Printed On Certificate):</td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td10">Additional Comments / Remarks:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdditionalComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: darkgray;">
                                                                <td runat="server" id="Td2" colspan="2" style="font-weight: bold;">Office Details:</td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td1">Office Full Address:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOfficeAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td3">Office Contact Details:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOfficeContact" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" id="Td4">Office eMail id:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOfficeEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="table-header">
                                                        Component
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div style="overflow: auto">
                                                        <asp:GridView ID="dgvQuestionnaire" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true" OnRowDataBound="dgvQuestionnaire_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectWorkPkgCertComp_Id" HeaderText="ProjectWorkPkgCertComp_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWorkPkgCertComp_Unit_Id" HeaderText="ProjectWorkPkgCertComp_Unit_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Select Component">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Component Name">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtComponentName" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_CompName") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                        <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Cost As Per BOQ (In Lakhs)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCostAsPerBOQ" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_CostAsPerBOQ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Cost As Per Actual (In Lakhs)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCostAsPerActual" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_CostAsPerActual") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Proposed (Number) As Per Origional">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPraposedNoAsPerBOQ" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_PraposedNoAsPerBOQ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Proposed (Number) As Per Actual">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPraposedNoAsPerActual" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_PraposedNoAsPerActual") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Completed (Number)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCompleted" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_Completed") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Functional (Number)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtFunctional" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_Functional") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgCertComp_Remarks") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="space-4"></div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnSave" Text="Save Details and Generate Certificate" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-12">

                                                    <div class="table-header">
                                                        Pre Generated Certificate Details
                                                    </div>

                                                    <div style="overflow: auto">
                                                        <asp:GridView ID="grdCertificateDetails" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdCertificateDetails_PreRender" OnRowDataBound="grdCertificateDetails_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectWorkPkgCert_Id" HeaderText="ProjectWorkPkgCert_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWorkPkgCert_Pkg_Id" HeaderText="ProjectWorkPkgCert_Pkg_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                        <asp:ImageButton ID="btnPrint" runat="server" Width="50px" Height="40px" OnClick="btnPrint_Click" ImageUrl="~/assets/images/print.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Certificate No" DataField="ProjectWorkPkgCert_No" />
                                                                <asp:BoundField HeaderText="Certificate Generated On" DataField="ProjectWorkPkgCert_AddedOn" />
                                                                <asp:BoundField HeaderText="Name Of Work" DataField="ProjectWorkPkgCert_WorkName" />
                                                                <asp:BoundField HeaderText="Agreement Date" DataField="ProjectWorkPkgCert_AgreementDate" />
                                                                <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkgCert_AgrrementNo" />
                                                                <asp:BoundField HeaderText="Start Date as Per Agreement" DataField="ProjectWorkPkgCert_StartDate" />
                                                                <asp:BoundField HeaderText="End Date as Per Agreement" DataField="ProjectWorkPkgCert_EndDate" />
                                                                <asp:BoundField HeaderText="Actual Date Of Completion" DataField="ProjectWorkPkgCert_EndDateExtended" />
                                                                <asp:BoundField HeaderText="Tender Cost" DataField="ProjectWorkPkgCert_TenderCost" />
                                                                <asp:BoundField HeaderText="Actual Cost" DataField="ProjectWorkPkgCert_GrossAmountCompleted" />
                                                                <asp:BoundField HeaderText="Total amount awarded" DataField="ProjectWorkPkgCert_AmountAwarded" />
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClick="btnDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
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
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="700px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Report Document
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Literal ID="ltEmbed" runat="server" />
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_ProjectWorkPkg_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Name" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Division_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                </ContentTemplate>
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                                    "iDisplayLength": 25,
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
    </script>
</asp:Content>



