<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectDPR_CNDS.aspx.cs" Inherits="MasterProjectDPR_CNDS" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" enablepartialrendering="true" enablepagemethods="true" asyncpostbacktimeout="6000">
            </cc1:toolkitscriptmanager>
            <asp:UpdatePanel ID="up" runat="server">
                <contenttemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Create New Project 
                                   
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">District* </label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
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
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3" id="divDivision" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divNodal" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" Text="Nodal Department" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlNodalDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNodalDepartment_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlNodalDeptScheme" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label51" runat="server" Text="Tentitive Cost Of DPR (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtTentitiveDPRCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Project Name* </label>
                                        <asp:TextBox ID="txtProjectWorkName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Project Location / Landmark* </label>
                                        <asp:TextBox ID="txtProjectLocation" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Preliminary Estimate (PE) / DPR Details   
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:RadioButtonList runat="server" ID="rbtPE_DPR" RepeatDirection="Horizontal">
                                            <asp:ListItem Text=" PE " Value="PE" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text=" DPR " Value="DPR"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label37" runat="server" Text="Architect (Firm Name)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlArchitect" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArchitect_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divArchtict" runat="server" visible="false">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label47" runat="server" Text="Firm Name" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtFirmName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label48" runat="server" Text="Firm GSTIN" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtFirmGSTIN" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label50" runat="server" Text="Contact Person Name" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label49" runat="server" Text="Contact No" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtFirmContact" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Authorization Letter Date for Preliminary Estimate" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtPEAuthorizationDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="Upload Authorization Letter for Preliminary Estimate" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:FileUpload ID="flUploadPE" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" Text="Preliminary Estimate Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtPEDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" Text="Nodal Agency Nomination Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtNodalNominationDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Land Identified / Availability Details 
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" Text="Land Status:" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:CheckBoxList ID="chkLandStatus" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Land Identified" Value="I"></asp:ListItem>
                                            <asp:ListItem Text="Land Transffered" Value="T"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Remarks For Land Not Identified / Not Available* </label>
                                        <asp:TextBox ID="txtLandNotRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label46" runat="server" Text="Upload Land Related Document" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:FileUpload ID="flLandDocument" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="Land Availability Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtLandAvailablityDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" Text="DPR Prepared" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:RadioButtonList ID="rbtDPRPrepared" runat="server">
                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" Text="Date of sending DPR to Client department" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtDPRSubmissionDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" Text="Status of DPR approval from client department" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:RadioButtonList ID="rbtDPRApprovalStatus" runat="server">
                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    GO Details 
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="GO Number" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtGONumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="GO Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtGODate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Upload GO" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:FileUpload ID="flGO" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Form J (as per sanctioned DPR)												
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" Text="Basic Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtBasicCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" Text="Contigency (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtContigency" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label42" runat="server" Text="Contigency Rate (%)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtContigencyPer" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server" Text="Net Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtNetCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" Text="Less 5.0% Proficiency cost from abstract of cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtProficiencyCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server" Text="Work Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtWorkCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" Text="Centage (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label43" runat="server" Text="Centage Rate (%)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCentagePer" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" Text="GST Cost 18% (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label21" runat="server" Text="Labour Cess 1.0% (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtLabourCess" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label22" runat="server" Text="External Electric connection Cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtElectricCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label23" runat="server" Text="Bought out Item cost (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtBoughtOut" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label24" runat="server" Text="Total Cost [Grand Total] (In Lakhs)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtTotalCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Current Status Of TS & Tender	
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label25" runat="server" Text="Date of DPR sent to HQ for TS" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtHQ_TS_Date" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label26" runat="server" Text="TS Approval from HQ Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtHQ_TS_Approval_Date" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label27" runat="server" Text="NIT Issuance Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtNITDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label28" runat="server" Text="NIT Cost" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtNITCost" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label29" runat="server" Text="Tender Uploading Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtTenderUploadDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label30" runat="server" Text="Pre/Bid Meeting Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtPreBidMeetingDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label31" runat="server" Text="Pre/Bid Response Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtPreBidResponseDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label32" runat="server" Text="Date Extention Corrigendum (1) Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCorrigendumDate1" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label33" runat="server" Text="Date Extention Corrigendum (2) Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCorrigendumDate2" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label34" runat="server" Text="Date Extention Corrigendum (3) Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCorrigendumDate3" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label35" runat="server" Text="Technical Bid Opening Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtTechnicalBidOpeningDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label36" runat="server" Text="Financial Bid Opening Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtFinancialBidOpeningDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label44" runat="server" Text="Total No Of Bidders" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtNoOfBidders" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="txtNoOfBidders_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Vendor Details									
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdNGTDtls" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdNGTDtls_PreRender" ShowFooter="true" OnRowDataBound="grdNGTDtls_RowDataBound">
                                        <columns>
                                            <asp:BoundField DataField="ProjectDPRBidder_Id" HeaderText="ProjectDPRBidder_Id">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_DPR_Id" HeaderText="ProjectDPRBidder_DPR_Id">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_TechnicalQualified" HeaderText="ProjectDPRBidder_TechnicalQualified">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_FinancialQualified" HeaderText="ProjectDPRBidder_FinancialQualified">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_Is_JV" HeaderText="ProjectDPRBidder_Is_JV">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_BidderGSTIN_Available" HeaderText="ProjectDPRBidder_BidderGSTIN_Available">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_BidderGSTINP_Available" HeaderText="ProjectDPRBidder_BidderGSTINP_Available">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectDPRBidder_Qualified_Status" HeaderText="ProjectDPRBidder_Qualified_Status">
                                                <headerstyle cssclass="displayStyle" />
                                                <itemstyle cssclass="displayStyle" />
                                                <footerstyle cssclass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Bidder">
                                                <itemtemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is JV">
                                                <itemtemplate>
                                                    <asp:RadioButtonList ID="ddlISJV" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="ddlISJV_SelectedIndexChanged">
                                                        <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bidder Details">
                                                <itemtemplate>
                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                        <tbody>
                                                            <tr>
                                                                <td>Lead Bidder<asp:CheckBox runat="server" ID="chkGSTNA" ToolTip="GST Not Available" AutoPostBack="true" OnCheckedChanged="chkGSTNA_CheckedChanged" /></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirmGSTIN" runat="server" CssClass="form-control" placeholder="GSTIN" Text='<%#Eval("ProjectDPRBidder_BidderGSTIN") %>' MaxLength="15" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtFirmGSTIN_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirmName" runat="server" CssClass="form-control" placeholder="Bidder Firm Name" Text='<%#Eval("ProjectDPRBidder_BidderName") %>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirmPAN" runat="server" CssClass="form-control" MaxLength="10" placeholder="PAN Card No" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderPAN") %>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Mobile No" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderMobile") %>' MaxLength="10"></asp:TextBox>
                                                                </td>
                                                                <td runat="server" id="tdShare" visible="false">
                                                                    <asp:TextBox ID="txtShare" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" placeholder="Share > 51%" Text='<%#Eval("ProjectDPRBidder_BidderShare") %>' MaxLength="4"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="trPartnerBidder" visible="false">
                                                                <td>Partner Bidder<asp:CheckBox runat="server" ID="chkGSTNAP" ToolTip="GST Not Available" AutoPostBack="true" OnCheckedChanged="chkGSTNAP_CheckedChanged" /></td>
                                                                <td>

                                                                    <asp:TextBox ID="txtFirmGSTINP" runat="server" CssClass="form-control" MaxLength="15" placeholder="GSTIN" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderGSTINP") %>' AutoPostBack="true" OnTextChanged="txtFirmGSTINP_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirmNameP" runat="server" CssClass="form-control" placeholder="Bidder Firm Name" Text='<%#Eval("ProjectDPRBidder_BidderNameP") %>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirmPANP" runat="server" CssClass="form-control" MaxLength="10" placeholder="PAN Card No" Style="text-transform: uppercase" Text='<%#Eval("ProjectDPRBidder_BidderPANP") %>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactNoP" runat="server" CssClass="form-control" placeholder="Mobile No" onkeyup="isNumericVal(this);" Text='<%#Eval("ProjectDPRBidder_BidderMobileP") %>' MaxLength="10"></asp:TextBox>
                                                                </td>
                                                                <td runat="server" id="tdShareP" visible="false">
                                                                    <asp:TextBox ID="txtShareP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" placeholder="Share > 30%" Text='<%#Eval("ProjectDPRBidder_BidderShareP") %>' MaxLength="4"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </itemtemplate>
                                                <footertemplate>
                                                    <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnAdd" OnClick="btnAdd_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnMinus" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnMinus_Click" Width="30px" Height="30px" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </footertemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Technically Qualified?">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkQualifiedT" runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Financially Qualified?">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkQualifiedF" runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financially Qualified Status">
                                                <itemtemplate>
                                                    <asp:DropDownList ID="ddlFinanciallyQualifiedStatus" runat="server">
                                                        <asp:ListItem Text="-Select-" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="L1" Value="L1"></asp:ListItem>
                                                        <asp:ListItem Text="L2" Value="L2"></asp:ListItem>
                                                        <asp:ListItem Text="L3" Value="L3"></asp:ListItem>
                                                        <asp:ListItem Text="L4" Value="L4"></asp:ListItem>
                                                        <asp:ListItem Text="L5" Value="L5"></asp:ListItem>
                                                        <asp:ListItem Text="L6" Value="L6"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quoted Rate (In Lakhs)">
                                                <itemtemplate>
                                                    <asp:TextBox ID="txtQuotedRate" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRBidder_BidderAmount") %>'></asp:TextBox>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comments">
                                                <itemtemplate>
                                                    <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" Text='<%#Eval("ProjectDPRBidder_Comments") %>'></asp:TextBox>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                        </columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    LOA and Order Details / Dates									
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label38" runat="server" Text="LoA Issuance Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtLOADate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label39" runat="server" Text="Date of work start" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtWorkStartDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label40" runat="server" Text="Contract bond Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCBDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy">
                                        </asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label45" runat="server" Text="Project Code (Will be Alloted after Completion of Step)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label41" runat="server" Text="Remarks (If Any)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtRemarksFinal" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnGenerateCode" Text="Generate Project Code" OnClick="btnGenerateCode_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hf_GOFile" Value="0" />
                        <asp:HiddenField runat="server" ID="hf_PE_Letter" Value="0" />
                        <asp:HiddenField runat="server" ID="hf_ProjectDPR_Id" Value="0" />
                    </div>
                </contenttemplate>
                <triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <progresstemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </progresstemplate>
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
</asp:Content>



