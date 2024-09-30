<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterPersonHiddenPage.aspx.cs" Inherits="MasterPersonHiddenPage" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Search User Logins</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Employee Masters</li>
                                            <li class="breadcrumb-item active">Search User Logins</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Search User Logins</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divScheme" runat="server">
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="Label13" runat="server" Text="Designation" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server"  CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <%--<label class="control-label no-padding-right">Search (MobileNo / Name)</label>--%>
                                                        <asp:Label ID="lblMobileNoName" runat="server" Text="Search (MobileNo / Name)" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:TextBox ID="txtSearchMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-primary"></asp:Button>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Person Master</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                
                                                <div class="clearfix" id="Div1" runat="server">
                                                    <div class="pull-right tableTools-container">
                                                    </div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" ShowFooter="true" EmptyDataText="No Records Found" OnPageIndexChanging="OnPageIndexChanging">
                                                         <Columns>
                                                            <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="ULB" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="ULB Type" DataField="Division_Type" />
                                                            <asp:BoundField HeaderText="Name" DataField="Person_Name" />
                                                            <asp:BoundField HeaderText="Mobile 1" DataField="Person_Mobile1" />
                                                            <asp:BoundField HeaderText="Mobile 2" DataField="Person_Mobile2" />
                                                            <asp:BoundField HeaderText="Email Id" DataField="Person_EmailId" />
                                                            <asp:BoundField HeaderText="User Name" DataField="Login_UserName" />
                                                            <asp:BoundField HeaderText="Password" DataField="Login_password" />
                                                            <asp:BoundField DataField="Designation_DesignationName" HeaderText="Designation" />
                                                            <asp:BoundField DataField="UserType_Desc_E" HeaderText="User Type" />
                                                            <asp:BoundField DataField="Project_Names" HeaderText="Mapped to Schemes" />
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
        </div>
    </div>
</asp:Content>
