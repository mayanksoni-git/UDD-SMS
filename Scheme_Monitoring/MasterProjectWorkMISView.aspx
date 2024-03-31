<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkMISView.aspx.cs" Inherits="MasterProjectWorkMISView" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpIssue" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="control-label no-padding-right">Filter By:</label>
                                                        <asp:RadioButtonList ID="rbtFilterBy" runat="server">
                                                            <asp:ListItem Text="Physical Progress" Value="P"></asp:ListItem>
                                                            <asp:ListItem Text="Financial Progress" Value="F"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lbl" runat="server" Text="From Range" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:TextBox ID="txtFromRange" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="Label1" runat="server" Text="Till Range" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:TextBox ID="txtTillRange" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" runat="server" CssClass="btn btn-primary"></asp:Button>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:ImageButton ID="btnDownload" OnClick="btnDownload_Click" runat="server" ImageUrl="~/assets/images/excel_import.png"
                                                            Width="60px" Height="50px"></asp:ImageButton>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:CheckBox runat="server" ID="chkShowIssue" Text=" Show Issue Column" AutoPostBack="true" OnCheckedChanged="chkShowIssue_CheckedChanged"></asp:CheckBox>
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
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project List

                                            <div style="float: right" id="divCreateNew" runat="server">
                                                <asp:Button ID="btnCreateNew" Text="Create New Project" OnClick="btnCreateNew_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                            </div>

                                        </h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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
                                                                    <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" /><br />
                                                                    <div class="btn-group" runat="server" id="divMISSteps">
                                                                        <button class="btn btn-app btn-pink btn-xs">
                                                                            <i class="ace-icon fa fa-share bigger-175"></i>
                                                                            MIS Steps
                                                                        </button>

                                                                        <button data-toggle="dropdown" class="btn btn-app btn-pink btn-xs dropdown-toggle" aria-expanded="false">
                                                                            <span class="bigger-110 ace-icon fa fa-caret-down icon-only"></span>
                                                                        </button>

                                                                        <ul class="dropdown-menu dropdown-pink">
                                                                            <li>
                                                                                <a target="_blank" href="Report_MasterProjectWorkMIS.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>">MIS Report</a>
                                                                            </li>
                                                                            <li class="divider"></li>
                                                                            <li>
                                                                                <a href="#" onclick="openBasicDetails(this);" project_id='<%# Eval("ProjectWork_Project_Id") %>' projectwork_id='<%# Eval("ProjectWork_Id") %>'>Go To Step 1: Basic Details</a>
                                                                            </li>

                                                                            <li>
                                                                                <a href="#" onclick="openGODetails(this);" project_id='<%# Eval("ProjectWork_Project_Id") %>' projectwork_id='<%# Eval("ProjectWork_Id") %>' district_id='<%# Eval("ProjectWork_DistrictId") %>'>Go To Step 2: GO Release Details</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="MasterProjectWorkMIS_3.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 3: Target & Achivments</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="MasterProjectWorkMIS_4.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 4: Physical Components</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="MasterProjectWorkMIS_5.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 5: Documents Upload</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="MasterProjectWorkMIS_6.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 6: UC Details and Other Issues</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="MasterProjectWorkMIS_7.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 7: Variation Details</a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div runat="server" id="divPhoto">
                                                                        <a target="_blank" href="ProjectWorkGalleryView.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Mode=P&App=false">View Gallery (<%# Eval("Total_Photo") %>)</a>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Zone" DataField="Zone_Name">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                            <asp:BoundField HeaderText="Sanctioned Cost (In Lakhs)" DataField="ProjectWork_Budget" />
                                                            <asp:BoundField HeaderText="Agreement Cost (In Lakhs)" DataField="tender_cost" />
                                                            <asp:BoundField HeaderText="Released Amount (In Lakhs)" DataField="Total_Release" />
                                                            <asp:BoundField HeaderText="Total Expenditure (In Lakhs)" DataField="Total_Expenditure" />
                                                            <asp:BoundField HeaderText="Physical Progress" DataField="Physical_Progress" />
                                                            <asp:BoundField HeaderText="Financial Progress" DataField="Financial_Progress" />
                                                            <asp:BoundField HeaderText="Start Date As Per Agreement" DataField="ProjectWorkPkg_Agreement_Date" />
                                                            <asp:BoundField HeaderText="Actual Start Date" DataField="ProjectWorkPkg_Start_Date" />
                                                            <asp:BoundField HeaderText="End Date As Per Agreement" DataField="ProjectWorkPkg_Due_Date" />
                                                            <asp:BoundField HeaderText="End Date As Per Agreement (Actual)" DataField="Target_Date_Agreement_Extended" />
                                                            <asp:BoundField HeaderText="Issue" DataField="Issue" />
                                                            <asp:TemplateField HeaderText="Issue Status">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnIssueList" OnClick="btnIssueList_Click" runat="server" Text='<%# Eval("Issue_Available") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Last Updated On (Data)" DataField="ProjectWork_ModifiedOn" />
                                                            <asp:BoundField HeaderText="Last Updated On (Photo)" DataField="Last_Updated" />
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
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="600px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Issues
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Issues
                                            </h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <!-- div.dataTables_borderWrap -->
                                                    <div style="overflow: auto">
                                                        <asp:GridView ID="grdIssue" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdIssue_PreRender">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Id" HeaderText="ProjectWorkIssueDetails_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Path" HeaderText="ProjectWorkIssueDetails_Path">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Dependency_Id" HeaderText="ProjectWorkIssueDetails_Dependency_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblRowNumber" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ProjectIssue_Name" HeaderText="Type Of Issue" />
                                                                <asp:BoundField DataField="Dependency_Name" HeaderText="Sub-Issue" />
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Date" HeaderText="Effective Start Date of Issue" />
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Comments" HeaderText="Comments" />
                                                                <asp:TemplateField HeaderText="Download Document">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkIssueDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkIssueDetails_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
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

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <button id="btnclose2" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDownload" />
                </Triggers>
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


    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>
    <script>
        function downloadGO(obj) {
            var GO_FilePath;
            GO_FilePath = obj.attributes.GO_FilePath.nodeValue;
            if (GO_FilePath.trim() == "") {
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + GO_FilePath, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }
    </script>
    <script>

        function openBasicDetails(obj) {
            var project_id = obj.attributes.project_id.nodeValue;
            var projectwork_id = obj.attributes.projectwork_id.nodeValue;
            if (project_id == "1014") {
                obj.href = "MasterProjectWorkMIS_1_Green.aspx?ProjectWork_Id=" + projectwork_id;
            }
            else if (project_id == "1015") {
                obj.href = "MasterProjectWorkMIS_1_DW.aspx?ProjectWork_Id=" + projectwork_id;
            }
            else {
                obj.href = "MasterProjectWorkMIS_1.aspx?ProjectWork_Id=" + projectwork_id;
            }
        }

        function openGODetails(obj) {
            var project_id = obj.attributes.project_id.nodeValue;
            var projectwork_id = obj.attributes.projectwork_id.nodeValue;
            var district_id = obj.attributes.district_id.nodeValue;
            if (project_id == "12") {
                obj.href = "MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else if (project_id == "3") {
                obj.href = "MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else if (project_id == "1015") {
                obj.href = "MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else {
                obj.href = "MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
        }
    </script>
</asp:Content>



