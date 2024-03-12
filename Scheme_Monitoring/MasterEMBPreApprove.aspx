<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterEMBPreApprove.aspx.cs" Inherits="MasterEMBPreApprove" MaintainScrollPositionOnPostback="true" %>

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
                                <div class="col-md-12">
                                    <h1>Approve EMB	
                                    </h1>
                                </div>

                            </div>
                        </div>
                        <!-- /.page-header -->
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Scheme</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" data-placeholder="Choose a Scheme..."></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDivisionH" CssClass="control-label no-padding-right" Text="Division" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Search By</label>
                                    <asp:RadioButtonList ID="rbtSearchBy" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Till Date</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">Date Range</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divFromDate" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Date From</label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" id="divTillDate" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Date Till</label>
                                    <asp:TextBox ID="txtDateTill" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>

                        <h3 class="header smaller red">Pending Action(s) EMB
                           
                            <div style="float: right" runat="server" id="divChkShowEMB">
                                <asp:CheckBox runat="server" ID="ChkShowEMB" Text="Show EMB Which is Invoice Not Generate." OnCheckedChanged="ChkShowEMB_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix" id="dtOptions" runat="server">
                                        <div class="pull-right grdEMBtableTools-container"></div>
                                    </div>
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdEMB" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdEMB_PreRender" OnRowDataBound="grdEMB_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="PackageEMB_Master_Id" HeaderText="PackageEMB_Master_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageEMB_Master_Package_Id" HeaderText="PackageEMB_Master_Package_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageEMBApproval_Next_Organisation_Id" HeaderText="PackageEMBApproval_Next_Organisation_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageEMBApproval_Next_Designation_Id" HeaderText="PackageEMBApproval_Next_Designation_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageEMB_Master_Type" HeaderText="PackageEMB_Master_Type">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnOpenEMB" runat="server" Height="20px" ImageUrl="~/assets/images/edit.png" OnClick="btnOpenEMB_Click" Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PackageEMB_Master_Date" HeaderText="EMB Date" />
                                                <asp:BoundField DataField="PackageEMB_Master_VoucherNo" HeaderText="Physical MB No" />
                                                <asp:BoundField HeaderText="RA Bill No" DataField="PackageEMB_Master_RA_BillNo" />
                                                <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                <asp:BoundField HeaderText="Budget" DataField="ProjectWork_Budget" />
                                                <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                <asp:BoundField HeaderText="Total Items" DataField="Total_Items" />
                                                <asp:BoundField HeaderText="Total Amount" DataField="PackageEMB_Master_Total_Amount" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                            </Columns>
                                        </asp:GridView>
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
    </div>
    <!-- DataTable specific plugin scripts -->
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdEMB').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdEMB')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdEMB')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdEMBtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdEMBtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdEMBtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdEMB .dropdown-toggle', function (e) {
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

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });

        function setTabPageActive1(mainMenuId, subMenuId, contentPageId, totalCount) {
            debugger;
            for (var i = 0; i < totalCount; i++) {
                $("#w_" + (i + 1)).removeClass('active');
                $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1)).removeClass('active in');
            }

            $("#" + mainMenuId + "").addClass('active');
            $("#" + subMenuId + "").attr('aria-expanded', 'true');
            $("#" + contentPageId + "").addClass('active in');

            sessionStorage["_activeMainTabMenu1"] = mainMenuId;
            sessionStorage["_activeSubTabMenu1"] = subMenuId;
            sessionStorage["_activecontentPageId1"] = contentPageId;
            sessionStorage["_activetotalCount1"] = totalCount;
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                debugger;
                if (sessionStorage["_activeMainTabMenu"] == "" || sessionStorage["_activeSubTabMenu"] == undefined || sessionStorage["_activetotalCount"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#inv_" + (i + 1)).removeClass('active');
                        $("#t_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1) + (i + 1)).removeClass('active in');
                    }

                    $("#" + sessionStorage["_activeMainTabMenu"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu"] + "").attr('aria-expanded', 'true');
                }

                if (sessionStorage["_activeMainTabMenu1"] == "" || sessionStorage["_activeSubTabMenu1"] == undefined || sessionStorage["_activetotalCount1"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount1"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#w_" + (i + 1)).removeClass('active');
                        $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1)).removeClass('active in');
                    }

                    $("#" + sessionStorage["_activeMainTabMenu1"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId1"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu1"] + "").attr('aria-expanded', 'true');
                }
            });
        });
    </script>
</asp:Content>

