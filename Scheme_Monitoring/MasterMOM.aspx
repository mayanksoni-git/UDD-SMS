<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterMOM.aspx.cs" Inherits="MasterMOM" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div id="divCreateNew" runat="server">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Create Minuts Of Meeting
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" CssClass="control-label no-padding-right">Meeting Title</asp:Label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="control-label no-padding-right">Meeting Type</asp:Label>
                                            <asp:DropDownList ID="ddlMeetingType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Meeting Date</label>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload PDF File</label>
                                            <br />
                                            <asp:FileUpload ID="flUpload" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Detailed Description
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <obout:Editor runat="server" ID="Editor1" Height="400px" Width="100%">
                                        <EditPanel FullHtml="true" />
                                    </obout:Editor>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSaveScheme_Click" CssClass="btn btn-info"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <h3 class="header smaller lighter blue">List Of Minuts Of Meeting

                                            <div class="pull-right">
                                                <asp:DropDownList ID="ddlMeetingTypeS" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMeetingTypeS_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </div>

                                        </h3>
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdRoutePlanView" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdRoutePlanView_PreRender" OnRowDataBound="grdRoutePlanView_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="MOMDetails_Id" HeaderText="MOMDetails_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MOMDetails_URL" HeaderText="MOMDetails_URL">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MOMDetails_Description" HeaderText="MOMDetails_Description">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MOMDetails_TypeId" HeaderText="MOMDetails_TypeId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Title" DataField="MOMDetails_Title" />
                                                    <asp:BoundField HeaderText="Meeting Date" DataField="MOMDetails_Date" />
                                                    <asp:BoundField HeaderText="Meeting Type" DataField="MOMType_Name" />
                                                    <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("MOMDetails_URL") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <div id="htmlDesc" runat="server">
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClick="btnDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
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


        <script src="assets/js/jquery-2.1.4.min.js"></script>
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
        <%--<script src="assets/js/dataTables.colReorder.min.js"></script>--%>

        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                jQuery(function ($) {
                    var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdRoutePlanView').length;
                    if (DataTableLength > 0) {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdRoutePlanView')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdRoutePlanView')
                                    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                    .DataTable({
                                        mark: true,
                                        colReorder: false,
                                        fixedHeader: {
                                            header: true,
                                            footer: false
                                        },
                                        bAutoWidth: false,
                                        "aoColumns": [
                                            { "bSortable": false },
                                            null, null, null, null, null, null, null, null, null,
                                            { "bSortable": false }
                                        ],
                                        "aaSorting": [],
                                        //"bProcessing": true,
                                        //"bServerSide": true,
                                        //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                        //,
                                        //"sScrollY": "200px",
                                        //"bPaginate": false,
                                        //"sScrollX": "100%",
                                        //"sScrollXInner": "120%",
                                        //"bScrollCollapse": true,
                                        //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                        "iDisplayLength": 25,
                                        select: {
                                            style: 'multi'
                                        }
                                    });
                            $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
                            new $.fn.dataTable.Buttons(myTable, {
                                buttons: [
                                    {
                                        "extend": "colvis",
                                        "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                                        "className": "btn btn-white btn-primary btn-bold",
                                        columns: ':not(:first):not(:last)'
                                    },
                                    {
                                        "extend": "copy",
                                        "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                                        "className": "btn btn-white btn-primary btn-bold"
                                    },
                                    {
                                        "extend": "csv",
                                        "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                                        "className": "btn btn-white btn-primary btn-bold"
                                    },
                                    {
                                        "extend": "excel",
                                        "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                                        "className": "btn btn-white btn-primary btn-bold"
                                    },
                                    {
                                        "extend": "pdf",
                                        "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                                        "className": "btn btn-white btn-primary btn-bold"
                                    },
                                    {
                                        "extend": "print",
                                        "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                                        "className": "btn btn-white btn-primary btn-bold",
                                        autoPrint: true,
                                        message: 'This print was produced using the Print button for DataTables',
                                        exportOptions: {
                                            columns: ':visible'
                                        }
                                    }
                                ]
                            });
                            myTable.buttons().container().appendTo($('.tableTools-container'));

                            //style the message box
                            var defaultCopyAction = myTable.button(1).action();
                            myTable.button(1).action(function (e, dt, button, config) {
                                defaultCopyAction(e, dt, button, config);
                                $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
                            });
                            var defaultColvisAction = myTable.button(0).action();
                            myTable.button(0).action(function (e, dt, button, config) {

                                defaultColvisAction(e, dt, button, config);
                                if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                                    $('.dt-button-collection')
                                        .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                                        .find('a').attr('href', '#').wrap("<li />")
                                }
                                $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.tableTools-container')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdRoutePlanView .dropdown-toggle', function (e) {
                                e.stopImmediatePropagation();
                                e.stopPropagation();
                                //e.preventDefault();
                            });
                            //And for the first simple table, which doesn't have TableTools or dataTables
                            //select/deselect all rows according to table header checkbox
                            var active_class = 'active';
                            /********************************/
                            //add tooltip for small view action buttons in dropdown menu
                            $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

                            //tooltip placement on right or left
                            function tooltip_placement(context, source) {
                                var $source = $(source);
                                var $parent = $source.closest('table')
                                var off1 = $parent.offset();
                                var w1 = $parent.width();

                                var off2 = $source.offset();
                                //var w2 = $source.width();

                                if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                                return 'left';
                            }
                            /***************/
                            $('.show-details-btn').on('click', function (e) {
                                e.preventDefault();
                                $(this).closest('tr').next().toggleClass('open');
                                $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
                            });
                        }
                    }
                })
            });
        </script>
    </div>
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
</asp:Content>

