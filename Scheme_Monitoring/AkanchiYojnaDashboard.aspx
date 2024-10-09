<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="AkanchiYojnaDashboard.aspx.cs" Inherits="AkanchiYojnaDashboard" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnplanId" runat="server" />
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <style>
        .right-align {
    text-align: right;
}
    </style>
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
                                    <h4 class="mb-sm-0">Akanshi Nagar Yojana</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Akanshi Nagar Yojana</li>
                                            <li class="breadcrumb-item active">Create Akanshi Nagar Yojana</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Filter :
                                            <label id="message" runat="server" style="float: right; color: red; font-weight: bold"></label>
                                        </h4>
                                        <a href="CreateAkanchiYojna.aspx" class="filter-btn" style="float: right"><i class="icon-download"></i>Create New</a>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divMandal" runat="server">
                                                        <asp:Label ID="lblMandal" runat="server" Text="Division" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="lblULBType" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="ddlULBType_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Nigam" Value="NN"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Panchayat" Value="NP"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Palika Parishad" Value="NPP"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-11  col-md-11">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="BtnSearch" Text="Search" Style="float: right" OnClick="BtnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlZone" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Akanshi Yojna Report</h4>
                            </div>
                            <!-- end card header -->
                            <div class="card-body">
                                <div class="live-preview">
                                    <div class="row gy-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div style="overflow: auto">
                                            <asp:GridView runat="server" ID="grdPost" AllowPaging="false" CssClass="display table table-bordered" 
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found"  OnPreRender="grdPost_PreRender" 
                                                OnRowDataBound="grdPost_RowDataBound" ShowFooter="True">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>                                                               
                                                                <a href="CreateAkanchiYojna.aspx?AkanshiID=<%# Eval("SrNo") %>" CssClass="btn btn-primary editBTN">Edit</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="State" DataField="Zone_Name" />
                                                    <asp:BoundField HeaderText="Mandal" DataField="DivName" />
                                                    <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                                                    <asp:BoundField HeaderText="CM Fellow Name " DataField="CMFellowName" />
                                                    <asp:BoundField HeaderText="CM Abhyuday School" DataField="CMAbhyudaySchool" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Total CM Abhyuday  Cost @1.42 Crore each" DataField="TotalCMAbhyudayCost" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Anganwadi Construction(On Rent)" DataField="AnganwadiConstructionOnRent" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Anganwadi Construction (On Other place)" DataField="AnganwadiConstructionOnOtherPlace" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Total Anganwadi Cost @11.84 Lakh each" DataField="TotalAnganwadiCost" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Smart Class+Furniture" DataField="SmartClassFurniture" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Total Smart Class Cost Smart class @2.505L &Furniture @0.7195L each" DataField="TotalSmartClassCost" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Additional Class Room" DataField="AdditionalClassRoom" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Total Additional Class Room @9.27 Lakh each" DataField="TotalAdditionalClassRoomCost" ItemStyle-CssClass="right-align" />
                                                    <asp:BoundField HeaderText="Total Amount Transferred" DataField="TotalAmountTransferred" ItemStyle-CssClass="right-align" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <tr>
                                                        <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
