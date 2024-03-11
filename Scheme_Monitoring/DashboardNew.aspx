<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="DashboardNew.aspx.cs" Inherits="DashboardNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <!-- /.ace-settings-container -->
                        <div class="page-header">
                            <div class="col-md-12">
                                <div class="col-md-9">
                                    <h1>Dashboard
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    Overview &amp; Stats
                                </small>
                                    </h1>
                                </div>
                                <div class="col-md-3 blink" id="divMIS" runat="server">
                                    <div class="blink" style="margin-right: 200px;">
                                        <asp:ImageButton ID="btnMIS" runat="server" OnClick="btnMIS_Click" ImageUrl="~/assets/images/Update_PMIS.png" Height="60px"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3" id="divZone" runat="server">
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
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-6 col-sm-4 col-md-3">
                                <asp:GridView ID="grdScheme_1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdScheme_1_PreRender" ShowFooter="false" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <div class="thumbnail search-thumbnail">
                                                    <img class="media-object" src="<%# Eval("Project_Icon_Path") %>" data-holder-rendered="true" style="height: 200px; width: 100%; display: block;">
                                                    <div class="caption" style="height: 180px">
                                                        <h3 class="search-title">
                                                            <a href="Dashboard_Master.aspx?Scheme_Id=<%# Eval("Project_Id") %>" class="blue"><%# Eval("Project_Name") %></a>
                                                        </h3>
                                                        <p>Total Projects: <%# Eval("Total_Count") %></p>
                                                        <p>Completed Projects: <%# Eval("Completed_Count") %></p>
                                                        <p>Ongoing Projects: <%# Eval("OnGoing_Count") %></p>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnEMBDashbaord1" Text="Go To Billing Dashboard (EMB)" runat="server" CssClass="btn btn-inverse" OnClick="btnEMBDashbaord1_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="space-6"></div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnAnalysisReport1" Text="Open Analysis Reports" runat="server" CssClass="btn btn-danger" OnClick="btnAnalysisReport1_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-xs-6 col-sm-4 col-md-3">
                                <asp:GridView ID="grdScheme_2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdScheme_2_PreRender" ShowFooter="false" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <div class="thumbnail search-thumbnail">
                                                    <img class="media-object" src="<%# Eval("Project_Icon_Path") %>" data-holder-rendered="true" style="height: 200px; width: 100%; display: block;">
                                                    <div class="caption" style="height: 180px">
                                                        <%--<div class="clearfix">
                                                            <span class="pull-right label label-grey info-label">fffffff</span>
                                                        </div>--%>

                                                        <h3 class="search-title">
                                                            <a href="Dashboard_Master.aspx?Scheme_Id=<%# Eval("Project_Id") %>" class="blue"><%# Eval("Project_Name") %></a>
                                                        </h3>
                                                        <p>Total Projects: <%# Eval("Total_Count") %></p>
                                                        <p>Completed Projects: <%# Eval("Completed_Count") %></p>
                                                        <p>Ongoing Projects: <%# Eval("OnGoing_Count") %></p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnEMBDashbaord2" Text="Go To Billing Dashboard (EMB)" runat="server" CssClass="btn btn-inverse" OnClick="btnEMBDashbaord2_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="space-6"></div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnAnalysisReport2" Text="Open Analysis Reports" runat="server" CssClass="btn btn-danger" OnClick="btnAnalysisReport2_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-xs-6 col-sm-4 col-md-3">
                                <asp:GridView ID="grdScheme_3" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdScheme_3_PreRender" ShowFooter="false" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <div class="thumbnail search-thumbnail">
                                                    <img class="media-object" src="<%# Eval("Project_Icon_Path") %>" data-holder-rendered="true" style="height: 200px; width: 100%; display: block;">
                                                    <div class="caption" style="height: 180px">
                                                        <%--<div class="clearfix">
                                                            <span class="pull-right label label-grey info-label">fffffff</span>
                                                        </div>--%>

                                                        <h3 class="search-title">
                                                            <a href="Dashboard_Master.aspx?Scheme_Id=<%# Eval("Project_Id") %>" class="blue"><%# Eval("Project_Name") %></a>
                                                        </h3>
                                                        <p>Total Projects: <%# Eval("Total_Count") %></p>
                                                        <p>Completed Projects: <%# Eval("Completed_Count") %></p>
                                                        <p>Ongoing Projects: <%# Eval("OnGoing_Count") %></p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnEMBDashbaord3" Text="Go To Billing Dashboard (EMB)" runat="server" CssClass="btn btn-inverse" OnClick="btnEMBDashbaord3_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="space-6"></div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnAnalysisReport3" Text="Open Analysis Reports" runat="server" CssClass="btn btn-danger" OnClick="btnAnalysisReport3_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-xs-6 col-sm-4 col-md-3">
                                <asp:GridView ID="grdScheme_4" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdScheme_4_PreRender" ShowFooter="false" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <div class="thumbnail search-thumbnail">
                                                    <img class="media-object" src="<%# Eval("Project_Icon_Path") %>" data-holder-rendered="true" style="height: 200px; width: 100%; display: block;">
                                                    <div class="caption" style="height: 180px">
                                                        <%--<div class="clearfix">
                                                            <span class="pull-right label label-grey info-label">fffffff</span>
                                                        </div>--%>

                                                        <h3 class="search-title">
                                                            <a href="Dashboard_Master.aspx?Scheme_Id=<%# Eval("Project_Id") %>" class="blue"><%# Eval("Project_Name") %></a>
                                                        </h3>
                                                        <p>Total Projects: <%# Eval("Total_Count") %></p>
                                                        <p>Completed Projects: <%# Eval("Completed_Count") %></p>
                                                        <p>Ongoing Projects: <%# Eval("OnGoing_Count") %></p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnEMBDashbaord4" Text="Go To Billing Dashboard (EMB)" runat="server" CssClass="btn btn-inverse" OnClick="btnEMBDashbaord4_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="space-6"></div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnAnalysisReport4" Text="Open Analysis Reports" runat="server" CssClass="btn btn-danger" OnClick="btnAnalysisReport4_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>

