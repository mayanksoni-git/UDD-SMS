<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="FundSanctionMaster.aspx.cs" Inherits="FundSanctionMaster" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h4 class="mb-sm-0">Fund Sanction Master</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Work Plan Management System</li>
                                            <li class="breadcrumb-item active">Fund Sanction Master</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Fund Sanction Master</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFY_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDistrict" runat="server">
                                                        <asp:Label ID="lblDistrict" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="lblULBType" runat="server" Text="ULB Type*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlULBType_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULB" runat="server">
                                                        <asp:Label ID="lblULB" runat="server" Text="ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                 <div class="col-xxl-2 col-md-6">
                                                        <div id="divSactionAmt" runat="server">
                                                            <asp:Label ID="lblSactionAmt" runat="server" Text="Saction Amount (In Lakhs)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtSactionAmt" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                <div class="col-xxl-3 offset-xxl-1 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfId" runat="server" />
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


                    <div runat="server" visible="true" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Work Proposal Detail</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">

                                                    <asp:GridView ID="grdPost" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="false" OnPreRender="grdPost_PreRender">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="Fund Sanctioned Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:BoundField HeaderText="District" DataField="DistName" />
                                                            <asp:BoundField HeaderText="ULB Type" DataField="ULBType" />
                                                            <asp:BoundField HeaderText="ULB Name" DataField="ULBName" />
                                                            <asp:BoundField HeaderText="Parliament Name" DataField="ParliamentaryConstName" />
                                                            <%--<asp:BoundField HeaderText="MP Name" DataField="MPName" />--%>
                                                            <asp:BoundField HeaderText="Assembly Name" DataField="AssemblyConstName" />
                                                            <%--<asp:BoundField HeaderText="MLA Name" DataField="MLAName" />--%>
                                                            <asp:BoundField HeaderText="Scheme Name" DataField="SchemeName" />
                                                            <asp:BoundField HeaderText="Session" DataField="SessionYear" />
                                                            <asp:BoundField HeaderText="Amount (In Lakhs)" DataField="AmtInLac" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
