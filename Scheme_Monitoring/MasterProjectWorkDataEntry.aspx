<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkDataEntry.aspx.cs" Inherits="MasterProjectWorkDataEntry" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Create/Update Project</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Project Master</li>
                                            <li class="breadcrumb-item active">Create/Update Project</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Create/Update Project</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <%--<label class="control-label no-padding-right">Scheme </label>--%>
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchScheme" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="divCircle" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <%--<label class="control-label no-padding-right">Project Code</label>--%>
                                                        <asp:Label ID="lblProjectCode" runat="server" Text="Project Code" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                  <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                  <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnCreateNew" Text="Create New Project" OnClick="btnCreateNew_Click" runat="server" CssClass="btn btn-primary"></asp:Button>
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

                        <div runat="server" visible="false" id="divData">

                            <div class="row" runat="server" visible="false">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Project Summery</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-4 col-md-6">
                                                        <div>
                                                            <ul class="list-group list-group-flush border-dashed">
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <h5 class="mb-0">
                                                                                        <asp:LinkButton ID="lnkTotal" runat="server" OnClick="lnkTotal_Click"></asp:LinkButton></h5>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">Total Projects</h5>
                                                                            <p class="text-reset fs-14 mb-0">Total No Of Projects Under Searched Criteria</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <h5 class="mb-0"></h5>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                                <!-- end -->
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkPhyCompleted" runat="server" OnClick="lnkPhyCompleted_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">Physically Completed / Not completed</h5>
                                                                            <p class="text-reset fs-14 mb-0 pull-right">Projects With Phisical Progress is 100 will be assumed as Physically Completed.</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkPhyNotCompleted" runat="server" OnClick="lnkPhyNotCompleted_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                                <!-- end -->
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkFinCompleted" runat="server" OnClick="lnkFinCompleted_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">Financially Completed / Not completed</h5>
                                                                            <p class="text-reset fs-14 mb-0 pull-right">Projects With financial Progress is 100 will be assumed as Financially Completed.</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkFinNotCompleted" runat="server" OnClick="lnkFinNotCompleted_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div>
                                                            <ul class="list-group list-group-flush border-dashed">
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkGalleryUpdated" runat="server" OnClick="lnkGalleryUpdated_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">Gallery Available</h5>
                                                                            <p class="text-reset fs-14 mb-0">Total No Of Projects where Photo Gallery is Uploaded</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkGalleryNotUpdated" runat="server" OnClick="lnkGalleryNotUpdated_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                                <!-- end -->
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkInspectionUpdated" runat="server" OnClick="lnkGalleryNotUpdated_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">Site Inspection Completed / Not completed</h5>
                                                                            <p class="text-reset fs-14 mb-0 pull-right">Projects For With Site Visit / Inspection Has Been Done.</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkInspectionNotUpdated" runat="server" OnClick="lnkGalleryNotUpdated_Click"></asp:LinkButton></h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                                <!-- end -->
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkAgreementUpdated" runat="server" OnClick="lnkAgreementUpdated_Click"></asp:LinkButton>
                                                                                        </h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">Agreement Details Updated / Not Updated</h5>
                                                                            <p class="text-reset fs-14 mb-0 pull-right">Projects With Details of Agreement With Vendor Has Been Updated.</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkAgreementNotUpdated" runat="server" OnClick="lnkAgreementNotUpdated_Click"></asp:LinkButton>
                                                                                        </h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div>
                                                            <ul class="list-group list-group-flush border-dashed">
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkUCUpload" runat="server" OnClick="lnkUCUpload_Click"></asp:LinkButton>
                                                                                        </h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">UC Uploaded / UC Not Uploaded</h5>
                                                                            <p class="text-reset fs-14 mb-0">Total Projects With Financial Progress More Than 60 and UC Uploaded</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkUCNotUpload" runat="server" OnClick="lnkUCNotUpload_Click"></asp:LinkButton>
                                                                                        </h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                                <!-- end -->
                                                                <li class="list-group-item ps-0">
                                                                    <div class="row align-items-center g-3">
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #e0f7ff !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkUCApproved" runat="server" OnClick="lnkUCApproved_Click"></asp:LinkButton>
                                                                                        </h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col">
                                                                            <h5 class="text-muted mt-0 mb-1 fs-13">UC Status (Approved / Not Approved)</h5>
                                                                            <p class="text-reset fs-14 mb-0">Total No UC and Its Approval Status</p>
                                                                        </div>
                                                                        <div class="col-auto">
                                                                            <div class="avatar-sm p-1 py-2 h-auto bg-light rounded-3" style="background-color: #ffe2d8 !important;">
                                                                                <div class="text-center">
                                                                                    <a href="#">
                                                                                        <h5 class="mb-0">
                                                                                            <asp:LinkButton ID="lnkUCNotApproved" runat="server" OnClick="lnkUCNotApproved_Click"></asp:LinkButton>
                                                                                        </h5>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- end row -->
                                                                </li>
                                                                <!-- end -->
                                                            </ul>
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
                                            <h4 class="card-title mb-0 flex-grow-1">Project List</h4>
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
                                                        <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWork_ULB_Id" HeaderText="ProjectWork_ULB_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                                <asp:BoundField HeaderText="Sanctioned Cost (In Lakhs)" DataField="ProjectWork_Budget" />
                                                                <asp:BoundField HeaderText="Agreement Cost (In Lakhs)" DataField="tender_cost" />
                                                                <asp:BoundField HeaderText="Released Amount (In Lakhs)" DataField="Total_Release" />
                                                                <asp:BoundField HeaderText="Total Expenditure (In Lakhs)" DataField="Total_Expenditure" />
                                                                <asp:BoundField HeaderText="Physical Progress" DataField="Physical_Progress" />
                                                                <asp:BoundField HeaderText="Financial Progress" DataField="Financial_Progress" />
                                                                <asp:BoundField HeaderText="Start Date As Per Agreement" DataField="ProjectWorkPkg_Agreement_Date" />
                                                                <asp:BoundField HeaderText="End Date As Per Agreement" DataField="ProjectWorkPkg_Due_Date" />
                                                                <asp:BoundField HeaderText="Issue" DataField="Issue" />
                                                                <asp:TemplateField HeaderText="Importent Timeline (Specific To This Project)">
                                                                    <ItemTemplate>
                                                                        <div class="list-group">
                                                                            <a href="#" class="list-group-item list-group-item-action">Last Updated On (Data): <%# Eval("ProjectWork_ModifiedOn") %></a>
                                                                            <a href="ProjectWorkGalleryView.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>" class="list-group-item list-group-item-action list-group-item-primary">Last Updated On (Gallery Photo): <%# Eval("Last_Updated") %></a>
                                                                            <a href="ProjectWorkInspectionUpdate.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>" class="list-group-item list-group-item-action list-group-item-secondary">Last Inspection / Field Visit: <%# Eval("Inspection_Submitted_Date") %></a>
                                                                            <a href="MasterProjectWorkMIS_6.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>" class="list-group-item list-group-item-action list-group-item-success">Last UC Submitted On: <%# Eval("UC_Submitted_Date") %></a>
                                                                            <a href="MasterProjectWorkMIS_4.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>" class="list-group-item list-group-item-action list-group-item-danger">Physical Progress As On: <%# Eval("Physical_As_On") %></a>
                                                                            <a href="MasterProjectWorkMIS_4.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>" class="list-group-item list-group-item-action list-group-item-warning">Financial Progress As On: <%# Eval("Financial_As_On") %></a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="200" />
                                                                    <ItemStyle Width="200" />
                                                                    <FooterStyle Width="200" />
                                                                </asp:TemplateField>
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



