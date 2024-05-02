<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_2_CNDS.aspx.cs" Inherits="MasterProjectWorkMIS_2_CNDS" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Fund Release Installment Details  For Central, State Share and ULB Share</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdCallProductDtls_PreRender" OnRowDataBound="grdCallProductDtls_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectWorkGO_Id" HeaderText="ProjectWorkGO_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Image ID="imgNew" runat="server" ImageUrl="~/assets/images/new.gif" Width="48px" Height="37px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GO Date">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtFinancialTrans_GO_Date" runat="server" CssClass="form-control date-picker" autocomplete="off" Text='<%# Eval("ProjectWorkGO_GO_Date") %>'></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtFinancialTrans_GO_Date" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl1" ForeColor="Black" CssClass="control-label no-padding-right" runat="server" Text="Total GO Issued Till Date:" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GO Number">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtFinancialTrans_GO_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkGO_GO_Number") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtTotalGO" runat="server" CssClass="form-control" MaxLength="2" onkeyup="isNumericVal(this);" Width="80px"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Central Share (In Lakhs)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCentralShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_CentralShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State Share (In Lakhs)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtStateShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_StateShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Centage (In Lakhs)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_Centage") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ULB / Other Share Released (In Lakhs)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtULBShare" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkGO_ULBShare") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Upload GO Document">
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="flUploadGO" runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                        <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ProjectWorkGO_Document_Path" HeaderText="ProjectWorkGO_Document_Path">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Download Document">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkULBShr" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkGO_Document_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDeleteGO" OnClick="btnDeleteGO_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="25px" Height="25px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle Font-Bold="true" BackColor="white" ForeColor="White" />
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

                            <div class="col-xxl-12 col-md-12 text-center">
                                <div>
                                    <asp:Button ID="btnSave" Text="Update Details" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
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





