<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="AddNewAkanshiData.aspx.cs" Inherits="AddNewAkanshiData" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
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
                                        <h4 class="mb-sm-0">Create/Update New Akanshi Data</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Akanshi Yojna</li>
                                                <li class="breadcrumb-item active">Create/Update New Akanshi Data</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Create/Update New Akanshi Data</h4>
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

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div1" runat="server">
                                                            <asp:Label ID="lblCMFellowName" runat="server" Text="CM Fellow Name" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtCMFellowName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="newAkanshi_Id" runat="server"/>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblTotalTransferred" runat="server" Text="Total Transferred (In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtTotalTransferred" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
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
                                            <h4 class="card-title mb-0 flex-grow-1">Add Akanchi Head Details</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdAkanshiHead" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdAkanshiHead_PreRender" OnRowDataBound="grdAkanshiHead_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="newAkanshiDetail_Id" HeaderText="newAkanshiDetail_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Akanshi Head">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlAkanshiHead" runat="server" CssClass="form-select" EnableViewState="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAkanshiHead_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="NoOfHead" runat="server" CssClass="form-control " Text='<%# Eval("NoOfHead") %>' AutoPostBack="true" OnTextChanged="NoOfHead_TextChanged"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                    <asp:TemplateField HeaderText="Cost Per Head">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="CostPerHead" runat="server" CssClass="form-control " Text='<%# Eval("CostPerHead") %>' ReadOnly="true"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                             
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount (In Lakhs)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="Amount" runat="server" CssClass="form-control" Text='<%# Eval("Amount") %>' ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                               <asp:TextBox ID="TotalAmount" runat="server" CssClass="form-control" Text='<%# Eval("TotalAmount") %>' ></asp:TextBox>
                                                                            <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                            <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                        </FooterTemplate>
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
                        <asp:HiddenField ID="hf_GO_Path" runat="server" Value="0" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <%--<asp:PostBackTrigger ControlID="NoOfHead" />--%>
                       <asp:AsyncPostBackTrigger ControlID="grdAkanshiHead" EventName="SelectedIndexChanged" />
                       <%--<asp:AsyncPostBackTrigger ControlID="NoOfHead" EventName="TextChanged" />--%>
                    </Triggers>
                </asp:UpdatePanel>
              
            </div>
        </div>
    </div>
</asp:Content>