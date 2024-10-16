<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="AkanshiHeadMaster.aspx.cs" Inherits="AkanshiHeadMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnAkanshiHeadId" runat="server" />
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
                                    <h4 class="mb-sm-0">Create Akanshi Head Master</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item active">Create Akanshi Head Master</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Create Akanshi Head Master Form <label id="message" runat="server" style="float:right;color:red;font-weight:bold"></label></h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <!-- Financial Year Dropdown -->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <!-- Head Name Textbox -->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divHeadName" runat="server">
                                                        <asp:Label ID="lblHeadName" runat="server" Text="Head Name*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtHeadName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <!-- Cost of Per Head Textbox (Decimal) -->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCostPerHead" runat="server">
                                                        <asp:Label ID="lblCostPerHead" runat="server" Text="Cost of Per Head*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="txtCostPerHead" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <!-- Buttons -->
                                                <div class="col-xxl-10 col-md-10"></div>
                                                <div class="col-xxl-2 col-md-2">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Label ID="lblHiddenId" runat="server" Enabled="false" Visible="false"></asp:Label>
                                                        <asp:Button ID="BtnSave" Text="Save" OnClick="BtnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="BtnUpdate" Visible="false" Text="Update" OnClick="BtnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="BtnSearch" Text="Search" style="margin:10px" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-warning mb-2"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- GridView for displaying records -->
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Akanshi Head List</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <asp:GridView runat="server" ID="grdPost" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear" />
                                                            <asp:BoundField HeaderText="Akanshi Head Name" DataField="AkanshiHead" />
                                                            <asp:BoundField HeaderText="Cost of Per Head" DataField="CostPerHead" />

                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAkanshiHead" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("AkanshiHeadId") %>' CssClass="btn btn-primary drill_btn" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnDelete" OnClientClick="return confirm('Are You Sure !!!')" runat="server" Text='Delete' CommandName="DeleteAkanshiHead" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("AkanshiHeadId") %>' CssClass="btn btn-danger drill_btn" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSave" />
                    <asp:PostBackTrigger ControlID="BtnUpdate" />
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

