<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_ProjectWork_Gallery_Pic.aspx.cs" Inherits="Report_ProjectWork_Gallery_Pic" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project Gallery Report</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <%--<label class="control-label no-padding-right">Scheme </label>--%>
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:ListBox ID="ddlScheme" runat="server" SelectionMode="Multiple" class="chosen-select form-control"
                                                            data-placeholder="Choose a Scheme..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6" id="divZone" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6" id="divCircle" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:RadioButtonList runat="server" ID="rbtProjectType" OnSelectedIndexChanged="rbtProjectType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="O" Text="Ongoing"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="Completed"></asp:ListItem>
                                                            <asp:ListItem Value="A" Text="All" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:CheckBox runat="server" ID="ChkShowPIC_NA" Text=" Show Projects For Which Gallery Not Available" OnCheckedChanged="ChkShowPIC_NA_CheckedChanged" AutoPostBack="true" />
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:CheckBox runat="server" ID="chkShowPicApp" Text=" Show Projects For Which Photo Uploaded Via Mobile App" OnCheckedChanged="chkShowPicApp_CheckedChanged" AutoPostBack="true" />
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
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
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <!-- div.table-responsive -->
                                                    <div class="clearfix">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
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
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                                <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                                <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                                <asp:BoundField HeaderText="Physical Progress (%)" DataField="Physical_Progress" />
                                                                <asp:BoundField HeaderText="Financial Progress (%)" DataField="Financial_Progress" />
                                                                <asp:TemplateField HeaderText="Total Photo Uploaded">
                                                                    <ItemTemplate>
                                                                        <a target="_blank" href="ProjectWorkGalleryView.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Mode=P&App=<%# chkShowPicApp.Checked.ToString().Trim() %>"><%# Eval("Total_Photo") %></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Video Uploaded">
                                                                    <ItemTemplate>
                                                                        <a target="_blank" href="ProjectWorkGalleryView.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Mode=V&App=<%# chkShowPicApp.Checked.ToString().Trim() %>"><%# Eval("Total_Video") %></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Download ZIP">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="btnDownload" ImageUrl="~/assets/images/download.png" Width="30px" Height="30px" OnClick="btnDownload_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add More Photos">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/assets/images/add_image.png" Width="30px" Height="30px" OnClick="btnAdd_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
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

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Upload Images / Videos In Project Gallery (Max 25 Pics or video)</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12" style="overflow: auto">
                                                    <iframe id="IframeGallery" src="~/CommanFileUpload_Multiple.aspx" runat="server" width="100%"></iframe>
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
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Button ID="btnUpdateGallery" Text="Update Photos In Gallery" OnClick="btnUpdateGallery_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Button ID="btnDeleteGallery" Text="Delete Photos" OnClick="btnDeleteGallery_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
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
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
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
</asp:Content>



