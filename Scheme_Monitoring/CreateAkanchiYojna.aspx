<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="CreateAkanchiYojna.aspx.cs" Inherits="CreateAkanchiYojna" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Akanchi Nagar Yojana</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Akanchi Nagar Yojana</li>
                                            <li class="breadcrumb-item active">Add Akanchi Nagar Yojana</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1" id="head2" runat="server">Add Akanchi Nagar Yojana</h4>
                                        <a href="AkanchiYojnaDashBoard.aspx" class="filter-btn" style="float: right"><i class="icon-download"></i>Go To List</a>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4 mt-0">

                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divCMFellowName" runat="server">
                                                        <asp:Label ID="lblCMFellowName" runat="server" Text="CM Fellow Name" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtCMFellowName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4 mt-0">

                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divCMAbhyudaySchool" runat="server">
                                                        <asp:Label ID="lblCMAbhyudaySchool" runat="server" Text="CM Abhyuday School" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtCMAbhyudaySchool" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div> 
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCMAbhyudaySchoolWP" runat="server">
                                                        <asp:Label ID="lblCMAbhyudaySchoolWP" runat="server" Text="CM Abhyuday School Work Progress" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtCMAbhyudaySchoolWP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divTotalCMAbhyudaySchoolCost" runat="server">
                                                        <asp:Label ID="lblTotalCMAbhyudaySchoolCost" runat="server" Text="Total CM Abhyuday  Cost @1.42 Crore each" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtTotalCMAbhyudaySchoolCost" runat="server" placeholder="31.89" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row gy-4 mt-0">

                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divAnganwadiConstructionOnRent" runat="server">
                                                        <asp:Label ID="lblAnganwadiConstructionOnRent" runat="server" Text="Anganwadi Construction(On Rent)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtAnganwadiConstructionOnRent" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divAnganwadiConstructionOnOtherPlace" runat="server">
                                                        <asp:Label ID="lblAnganwadiConstructionOnOtherPlace" runat="server" Text="Anganwadi Construction (On Other place)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtAnganwadiConstructionOnOtherPlace" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div> 
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divAnganwadiConstructionWP" runat="server">
                                                        <asp:Label ID="lblAnganwadiConstructionWP" runat="server" Text="Anganwadi Construction Work Progress" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtAnganwadiConstructionWP" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divTotalAnganwadiCost" runat="server">
                                                        <asp:Label ID="lblTotalAnganwadiCost" runat="server" Text="Total Anganwadi Cost @11.84 Lakh each" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtTotalAnganwadiCost" runat="server" placeholder="31.89" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4 align-items-end mt-0">
                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divSmartClassFurniture" runat="server">
                                                        <asp:Label ID="lblSmartClassFurniture" runat="server" Text="Smart Class + Furniture" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtSmartClassFurniture" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div> 
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divSmartClassFurnitureWP" runat="server">
                                                        <asp:Label ID="lblSmartClassFurnitureWP" runat="server" Text="Smart Class + Furniture  Work Progress" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtSmartClassFurnitureWP" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divTotalSmartClassCost" runat="server">
                                                        <asp:Label ID="lblTotalSmartClassCost" runat="server" Text="Total Smart Class Cost Smart class @2.505L &Furniture @0.7195L each" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtTotalSmartClassCost" runat="server" placeholder="31.89" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4 mt-0">

                                                <div class="col-xxl-2 col-md-6">
                                                    <div id="divAdditionalClassRoom" runat="server">
                                                        <asp:Label ID="lblAdditionalClassRoom" runat="server" Text="Additional Class Room" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtAdditionalClassRoom" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divAdditionalClassRoomWP" runat="server">
                                                        <asp:Label ID="lblAdditionalClassRoomWP" runat="server" Text="Additional Class Room  Work Progress" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtAdditionalClassRoomWP" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateTotalAmount" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divAdditionalClassRoomCost" runat="server">
                                                        <asp:Label ID="lblAdditionalClassRoomCost" runat="server" Text="Total Additional Class Room @9.27 Lakh each" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtAdditionalClassRoomCost" runat="server" placeholder="31.89" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4 mt-0">

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divTotalAmount" runat="server">
                                                        <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtTotalAmount" runat="server" placeholder="311.89" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divTotalAmountTransferred" runat="server">
                                                        <asp:Label ID="lblTotalAmountTransferred" runat="server" Text="Total Amount Transferred" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtTotalAmountTransferred" runat="server" placeholder="311.89" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-12 col-md-6 text-center">
                                                    <div>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-warning text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-danger text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfAkanshiYoujnaId" runat="server" />
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="txtCMAbhyudaySchool" />
                    <asp:PostBackTrigger ControlID="txtAnganwadiConstructionOnRent" />
                    <asp:PostBackTrigger ControlID="txtAnganwadiConstructionOnOtherPlace" />
                    <asp:PostBackTrigger ControlID="txtSmartClassFurniture" />
                    <asp:PostBackTrigger ControlID="txtAdditionalClassRoom" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
