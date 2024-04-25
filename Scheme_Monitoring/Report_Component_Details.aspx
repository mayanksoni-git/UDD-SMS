<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_Component_Details.aspx.cs" Inherits="Report_Component_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
                EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Component Wise Analysis Report</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Analysis</li>
                                                <li class="breadcrumb-item active">Component Wise Analysis Report</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Field Visit and Inspection Report</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblSchemeH" runat="server" Text="Scheme" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlSearchScheme" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchScheme_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divZone" runat="server">
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divCircle" runat="server">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divDivision" runat="server">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label1" runat="server" Text="Component" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-select">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Project Type</label>
                                                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Date Till</label>
                                                            <asp:TextBox ID="txtDateTill" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="card">
                                            <div class="card-header align-items-center d-flex">
                                                <h4 class="card-title mb-0 flex-grow-1">Component Progress Analysis Report</h4>
                                            </div>
                                            <!-- end card header -->
                                            <div class="card-body">
                                                <div class="live-preview">
                                                    <div class="row gy-12">
                                                        <div class="col-xxl-12 col-md-12">
                                                            <div class="clearfix" id="dtOptions" runat="server">
                                                                <div class="pull-right tableTools-container"></div>
                                                            </div>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered"
                                                                    AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Component_Id" HeaderText="Component_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="S No.">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                        <asp:BoundField HeaderText="Project" DataField="ProjectWork_Name" />
                                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                                        <asp:BoundField DataField="Component_Unit" HeaderText="Component" />
                                                                        <asp:BoundField DataField="ProposedF" HeaderText="Proposed As Per Origional" />
                                                                        <asp:BoundField DataField="Proposed" HeaderText="Proposed As Per Actual" />
                                                                        <asp:BoundField DataField="PhysicalProgress" HeaderText="Completed" />
                                                                        <asp:BoundField DataField="Percentage_Cpmpleted" HeaderText="Completed Percentage" />
                                                                        <asp:BoundField DataField="ProjectUC_PhysicalProgress_Remarks" HeaderText="Comments / Remarks" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#666666" ForeColor="White" Font-Bold="true" />
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
                            </div>
                        </div>
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

</asp:Content>

