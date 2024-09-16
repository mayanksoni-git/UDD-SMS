<%@ page language="C#" masterpagefile="~/TemplateMasterAdmin.master" autoeventwireup="true" codefile="DashboardNew.aspx.cs" inherits="DashboardNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .table > :not(caption) > * > * {
            padding: 0px;
        }

        table {
            background-color: transparent;
        }
    </style>
    <div class="main-content">
        <div class="page-content">
            <div class="container-fluid">

                <!-- start page title -->
                <div class="row">
                    <div class="col-12 mb-0">
                        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                            <h4 class="mb-sm-0">Dashboard</h4>
                        </div>
                    </div>
                </div>
                <!-- end page title -->

                <div class="card">
                 <div class="card-body">
                <div class="row mb-0">
                    <div class="col-md-3" id="divZone" runat="server">
                        <div class="form-group">
                            <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divCircle" runat="server">
                        <div class="form-group">
                            <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divDivision" runat="server">
                        <div class="form-group">
                            <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group mt-4">
                            <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn-filter" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </div>
                       </div>  </div>
                <div class="row">
                    <div class="col-xxl-12">
                        <div class="d-flex flex-column h-100">
                            <!-- end row-->
                           <div class="row">
                                <div class="col-md-3">
                                    <asp:GridView ID="grdScheme_1" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdScheme_1_PreRender" ShowFooter="false" ShowHeader="false">
                                        <Columns>
                                            <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div class="card card-animate dash-box">
                                                        <div class="card-body">
                                                            <div class="d-flex justify-content-between">

                                                                <a href="Dashboard_PMIS.aspx?Scheme_Id=<%# Eval("Project_Id") %>">
                                                                    <p class="fw-medium text-muted mb-0"><%# Eval("Project_Name") %></p>

                                                                    <h2 class="ff-secondary fw-semibold"><span class="counter-value" data-target='<%# Eval("Total_Count") %>'>0</span></h2>
                                                                </a>

                                                                <div class="avatar-sm flex-shrink-0"><span class="avatar-title bg-danger bg-gradient rounded-circle fs-2"><%--<i class="bx bx-spreadsheet text-white"></i>--%><img src="<%# Eval("Project_Icon_Path") %>" class="img-fluid"/></span></div>

                                                            </div>


                                                            <div class="px-0 py-2 mt-1">
                                                                <p class="mb-1">Ongoing Projects<span class="float-end"><%# Eval("OnGoing_Count") %>- (<%# Eval("Ongoing_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Ongoing_Per") %>%" aria-valuenow="<%# Eval("Ongoing_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Ongoing_Per") %>">
                                                                    </div>
                                                                </div>

                                                                <p class="mt-3 mb-1">Completed Projects<span class="float-end"><%# Eval("Completed_Count") %>- (<%# Eval("Completed_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Completed_Per") %>%; background-color: #4CAF50 !important;" aria-valuenow="<%# Eval("Completed_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Completed_Per") %>">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Button ID="btnDistWise" Text="Open District Wise Details" runat="server" CssClass="dash-btn-details" OnClick="btnDistWise_Click"></asp:Button>
                                                        </div>
                                                        <!-- end card body -->
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <!-- end card-->
                                </div>
                                <div class="col-md-3">
                                    <asp:GridView ID="grdScheme_2" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdScheme_2_PreRender" ShowFooter="false" ShowHeader="false">
                                        <Columns>
                                            <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div class="card card-animate dash-box">
                                                        <div class="card-body">
                                                            <div class="d-flex justify-content-between">

                                                                <a href="Dashboard_PMIS.aspx?Scheme_Id=<%# Eval("Project_Id") %>">
                                                                    <p class="fw-medium text-muted mb-0"><%# Eval("Project_Name") %></p>

                                                                    <h2 class="ff-secondary fw-semibold"><span class="counter-value" data-target='<%# Eval("Total_Count") %>'>0</span></h2>
                                                                </a>

                                                                <div class="avatar-sm flex-shrink-0"><span class="avatar-title bg-warning bg-gradient rounded-circle fs-2"><img src="<%# Eval("Project_Icon_Path") %>" class="img-fluid" /></span></div>

                                                            </div>
                                                            <div class="px-0 py-2 mt-1">
                                                                <p class="mb-1">Ongoing Projects<span class="float-end"><%# Eval("OnGoing_Count") %>- (<%# Eval("Ongoing_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Ongoing_Per") %>%" aria-valuenow="<%# Eval("Ongoing_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Ongoing_Per") %>">
                                                                    </div>
                                                                </div>

                                                                <p class="mt-3 mb-1">Completed Projects<span class="float-end"><%# Eval("Completed_Count") %>- (<%# Eval("Completed_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Completed_Per") %>%; background-color: #4CAF50 !important;" aria-valuenow="<%# Eval("Completed_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Completed_Per") %>">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Button ID="btnDistWise" Text="Open District Wise Details" runat="server" CssClass="dash-btn-details" OnClick="btnDistWise_Click"></asp:Button>
                                                        </div>
                                                        <!-- end card body -->
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-3">
                                    <asp:GridView ID="grdScheme_3" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdScheme_3_PreRender" ShowFooter="false" ShowHeader="false">
                                        <Columns>
                                            <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div class="card card-animate dash-box">
                                                        <div class="card-body">
                                                            <div class="d-flex justify-content-between">

                                                                <a href="Dashboard_PMIS.aspx?Scheme_Id=<%# Eval("Project_Id") %>">
                                                                    <p class="fw-medium text-muted mb-0"><%# Eval("Project_Name") %></p>

                                                                    <h2 class="ff-secondary fw-semibold"><span class="counter-value" data-target='<%# Eval("Total_Count") %>'>0</span></h2>
                                                                </a>

                                                                <div class="avatar-sm flex-shrink-0"><span class="avatar-title bg-primary bg-gradient rounded-circle fs-2"><img src="<%# Eval("Project_Icon_Path") %>" class="img-fluid" /></span></div>

                                                            </div>
                                                            <div class="px-0 py-2 mt-1">
                                                                <p class="mb-1">Ongoing Projects<span class="float-end"><%# Eval("OnGoing_Count") %>- (<%# Eval("Ongoing_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Ongoing_Per") %>%" aria-valuenow="<%# Eval("Ongoing_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Ongoing_Per") %>">
                                                                    </div>
                                                                </div>

                                                                <p class="mt-3 mb-1">Completed Projects<span class="float-end"><%# Eval("Completed_Count") %>- (<%# Eval("Completed_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Completed_Per") %>%; background-color: #4CAF50 !important;" aria-valuenow="<%# Eval("Completed_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Completed_Per") %>">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Button ID="btnDistWise" Text="Open District Wise Details" runat="server" CssClass="dash-btn-details" OnClick="btnDistWise_Click"></asp:Button>
                                                        </div>
                                                        <!-- end card body -->
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-3">
                                    <asp:GridView ID="grdScheme_4" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdScheme_4_PreRender" ShowFooter="false" ShowHeader="false">
                                        <Columns>
                                            <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div class="card card-animate dash-box">
                                                        <div class="card-body">
                                                            <div class="d-flex justify-content-between">

                                                                <a href="Dashboard_PMIS.aspx?Scheme_Id=<%# Eval("Project_Id") %>">
                                                                    <p class="fw-medium text-muted mb-0"><%# Eval("Project_Name") %></p>

                                                                    <h2 class="ff-secondary fw-semibold"><span class="counter-value" data-target='<%# Eval("Total_Count") %>'>0</span></h2>
                                                                </a>

                                                                <div class="avatar-sm flex-shrink-0"><span class="avatar-title bg-success bg-gradient rounded-circle fs-2"><img src="<%# Eval("Project_Icon_Path") %>" class="img-fluid" /></span></div>

                                                            </div>
                                                            <div class="px-0 py-2 mt-1">
                                                                <p class="mb-1">Ongoing Projects<span class="float-end"><%# Eval("OnGoing_Count") %>- (<%# Eval("Ongoing_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Ongoing_Per") %>%" aria-valuenow="<%# Eval("Ongoing_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Ongoing_Per") %>">
                                                                    </div>
                                                                </div>

                                                                <p class="mt-3 mb-1">Completed Projects<span class="float-end"><%# Eval("Completed_Count") %>- (<%# Eval("Completed_Per") %>%)</span></p>
                                                                <div class="progress mt-2" style="height: 6px;">
                                                                    <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" style="width: <%# Eval("Completed_Per") %>%; background-color: #4CAF50 !important;" aria-valuenow="<%# Eval("Completed_Per") %>" aria-valuemin="0" aria-valuemax="<%# Eval("Completed_Per") %>">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Button ID="btnDistWise" Text="Open District Wise Details" runat="server" CssClass="dash-btn-details" OnClick="btnDistWise_Click"></asp:Button>
                                                        </div>
                                                        <!-- end card body -->
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <!-- end card-->
                                </div>
                                <!-- end col-->
                            </div>

                            <!-- end row-->
                        </div>
                    </div>
                    <!-- end col -->
                </div>
                <!-- end row-->
            </div>
            <!-- container-fluid -->
        </div>
        <!-- End Page-content -->
    </div>
</asp:Content>