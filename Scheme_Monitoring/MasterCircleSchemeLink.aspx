﻿<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterCircleSchemeLink.aspx.cs" Inherits="MasterCircleSchemeLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Circle*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlCircle" runat="server" class="chosen-select form-control" data-placeholder="Choose a Product..." AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Circle Scheme Linking</h3>
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdCircle" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" OnRowDataBound="grdCircle_RowDataBound" EmptyDataText="No Records Found" OnPreRender="grdCircle_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="Circle_Id" HeaderText="Circle_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="List_Project_Id" HeaderText="List_Project_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                                    <asp:TemplateField HeaderText="Scheme">
                                                        <ItemTemplate>
                                                            <asp:CheckBoxList ID="chk_SchemeLink" runat="server"></asp:CheckBoxList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/MIDAS/logo_120.jpg" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <!-- /.main-content -->

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

    <script type="text/javascript">
//        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
//            jQuery(function ($) {
// var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPost').length;
//                if (DataTableLength > 0) {
//                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPost')[0].outerText;
//                    if (outerHTML !== "No Records Found") {
//                //initiate dataTables plugin
//                var myTable =
//                    $('#ctl00_ContentPlaceHolder1_grdPost')
//                        //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
//                        .DataTable({
//                            mark: true,
//                            colReorder: true,
//                            fixedHeader: {
//                                header: true,
//                                footer: false
//                            },
//                            bAutoWidth: false,
//                            "aoColumns": [
//                                //{ "bSortable": false },
//                                null, null, null, null, null
//                                //{ "bSortable": false }
//                            ],
//                            "aaSorting": [],
//                            //"bProcessing": true,
//                            //"bServerSide": true,
//                            //"sAjaxSource": "http://127.0.0.1/table.php"	,

//                            //,
//                            //"sScrollY": "200px",
//                            //"bPaginate": false,
//                            //"sScrollX": "100%",
//                            //"sScrollXInner": "120%",
//                            //"bScrollCollapse": true,
//                            //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
//                            //you may want to wrap the table inside a "div.dataTables_borderWrap" element

//                            "iDisplayLength": 100,
//                            select: {
//                                style: 'multi'
//                            }
//                        });
//                $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
//                new $.fn.dataTable.Buttons(myTable, {
//                    buttons: [
//                        {
//                            "extend": "colvis",
//                            "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
//                            "className": "btn btn-white btn-primary btn-bold",
//                            columns: ':not(:first):not(:last)'
//                        },
//                        {
//                            "extend": "copy",
//                            "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
//                            "className": "btn btn-white btn-primary btn-bold"
//                        },
//                        {
//                            "extend": "csv",
//                            "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
//                            "className": "btn btn-white btn-primary btn-bold"
//                        },
//                        {
//                            "extend": "excel",
//                            "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
//                            "className": "btn btn-white btn-primary btn-bold"
//                        },
//                        {
//                            "extend": "pdf",
//                            "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
//                            "className": "btn btn-white btn-primary btn-bold"
//                        },
//                        {
//                            "extend": "print",
//                            "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
//                            "className": "btn btn-white btn-primary btn-bold",
//                            autoPrint: true,
//                            message: 'This print was produced using the Print button for DataTables',
//                            exportOptions: {
//                                columns: ':visible'
//                            }
//                        }
//                    ]
//                });
//                myTable.buttons().container().appendTo($('.tableTools-container'));

//                //style the message box
//                var defaultCopyAction = myTable.button(1).action();
//                myTable.button(1).action(function (e, dt, button, config) {
//                    defaultCopyAction(e, dt, button, config);
//                    $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
//                });
//                var defaultColvisAction = myTable.button(0).action();
//                myTable.button(0).action(function (e, dt, button, config) {

//                    defaultColvisAction(e, dt, button, config);
//                    if ($('.dt-button-collection > .dropdown-menu').length == 0) {
//                        $('.dt-button-collection')
//                            .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
//                            .find('a').attr('href', '#').wrap("<li />")
//                    }
//                    $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
//                });
//                ////
//                setTimeout(function () {
//                    $($('.tableTools-container')).find('a.dt-button').each(function () {
//                        var div = $(this).find(' > div').first();
//                        if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
//                        else $(this).tooltip({ container: 'body', title: $(this).text() });
//                    });
//                }, 500);

//                $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPost .dropdown-toggle', function (e) {
//                    e.stopImmediatePropagation();
//                    e.stopPropagation();
//                    //e.preventDefault();
//                });
//                //And for the first simple table, which doesn't have TableTools or dataTables
//                //select/deselect all rows according to table header checkbox
//                var active_class = 'active';
//                /********************************/
//                //add tooltip for small view action buttons in dropdown menu
//                $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

//                //tooltip placement on right or left
//                function tooltip_placement(context, source) {
//                    var $source = $(source);
//                    var $parent = $source.closest('table')
//                    var off1 = $parent.offset();
//                    var w1 = $parent.width();

//                    var off2 = $source.offset();
//                    //var w2 = $source.width();

//                    if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
//                    return 'left';
//                }
//                /***************/
//                $('.show-details-btn').on('click', function (e) {
//                    e.preventDefault();
//                    $(this).closest('tr').next().toggleClass('open');
//                    $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
//                });
//}}
//            })
//        });

    </script>


</asp:Content>

