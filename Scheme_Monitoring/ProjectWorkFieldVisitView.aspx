<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="ProjectWorkFieldVisitView.aspx.cs" Inherits="ProjectWorkFieldVisitView" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">

                        <div class="row">
                            <div style="overflow: auto">
                                <asp:GridView ID="grdPost1" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost1_PreRender" OnRowDataBound="grdPost1_RowDataBound">
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
                                        <asp:BoundField DataField="ProjectWork_ProjectType_Id" HeaderText="ProjectWork_ProjectType_Id">
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
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnView1" Width="20px" Height="20px" OnClick="btnView1_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                        <asp:BoundField HeaderText="Sanctioned Cost (In Lakhs)" DataField="ProjectWork_Budget" />
                                        <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                        <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                        <asp:BoundField HeaderText="Physical %" DataField="Physical_Progress" />
                                        <asp:BoundField HeaderText="Financial %" DataField="Financial_Progress" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Date Wise Bifurcation Of Visit (Click A Date To View Its Field Visit) 
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div style="overflow: auto">
                                    <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectVisit_Id" HeaderText="ProjectVisit_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Visit Added On" DataField="ProjectVisit_SubmitionDate" />
                                            <asp:BoundField HeaderText="Visit Added By Designation" DataField="Designation_DesignationName" />
                                            <asp:BoundField HeaderText="Visit Added By Name" DataField="Person_Name" />
                                            <asp:BoundField HeaderText="Comments on Visit" DataField="ProjectVisit_Comments" />
                                            <asp:BoundField HeaderText="Physical Progress (%) During Visit" DataField="ProjectVisit_PhysicalProgress" />
                                            <asp:BoundField HeaderText="Financial Progress (%) During Visit" DataField="ProjectVisit_FinancialProgress" />
                                            <asp:TemplateField HeaderText="View Photos">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/assets/images/View.png" Width="40px" Height="40px" OnClick="btnView_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    View Field Visit Photographs
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-12" id="divGallery" runat="server">
                                </div>
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
        <!-- /.main-content -->
    </div>

    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <%--<script src="assets/js/bootstrap.min.js"></script>--%>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <script src="assets/js/dataTables.colReorder.min.js"></script>
    <script src="assets/js/jquery.colorbox.min.js"></script>


    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var $overflow = '';
                var colorbox_params = {
                    rel: 'colorbox',
                    reposition: true,
                    scalePhotos: true,
                    scrolling: false,
                    previous: '<i class="ace-icon fa fa-arrow-left"></i>',
                    next: '<i class="ace-icon fa fa-arrow-right"></i>',
                    close: '&times;',
                    current: '{current} of {total}',
                    maxWidth: '100%',
                    maxHeight: '100%',
                    onOpen: function () {
                        $overflow = document.body.style.overflow;
                        document.body.style.overflow = 'hidden';
                    },
                    onClosed: function () {
                        document.body.style.overflow = $overflow;
                    },
                    onComplete: function () {
                        $.colorbox.resize();
                    }
                };

                $('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);
                $("#cboxLoadingGraphic").html("<i class='ace-icon fa fa-spinner orange fa-spin'></i>");//let's add a custom loading icon


                $(document).one('ajaxloadstart.page', function (e) {
                    $('#colorbox, #cboxOverlay').remove();
                });
            })
        });

        function Poppuplose() {
            $find('details').hide();
        }
    </script>

    <style type="text/css">
        .MyColr-tooltip + .tooltip > .tooltip-inner {
            width: 200px;
            max-width: 300px !important;
            background-color: blue;
            color: white;
            text-align: center;
            font-weight: 600;
            text-transform: capitalize;
            opacity: 1;
            filter: alpha(opacity=100);
            -moz-box-shadow: 0 0 5px 2px black;
            -webkit-box-shadow: 0 0 5px 2px black;
            box-shadow: 0 0 5px 2px black;
        }
    </style>
</asp:Content>


