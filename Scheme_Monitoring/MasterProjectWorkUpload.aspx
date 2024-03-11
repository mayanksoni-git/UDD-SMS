<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkUpload.aspx.cs" Inherits="MasterProjectWorkUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="page-content">
                            <div class="alert alert-danger">
                                Upload Annexture(s) (1, 2, 3) for  
                                <strong>Project With Progress More than 90%.
                                </strong>
                                Water Supply and Sewarage
                                <br>
                            </div>
                            <h3 class="header smaller red">Download Reporting Format
                            </h3>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-4">
                                        <div class="btn-group">
                                            <button class="btn btn-inverse">Download Format for Sewarage</button>

                                            <button data-toggle="dropdown" class="btn btn-inverse dropdown-toggle" aria-expanded="false">
                                                <span class="ace-icon fa fa-caret-down icon-only"></span>
                                            </button>

                                            <ul class="dropdown-menu dropdown-inverse">
                                                <li>
                                                    <a href="Downloads/Format/sewage.pdf" target="_blank" style="font-weight: bold; font-size: large;">PDF File</a>
                                                </li>

                                                <li>
                                                    <a href="Downloads/Format/sewage.doc" target="_blank" style="font-weight: bold; font-size: large;">Word File</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="btn-group">
                                            <button class="btn btn-inverse">Download Format for Water Supply</button>

                                            <button data-toggle="dropdown" class="btn btn-inverse dropdown-toggle" aria-expanded="false">
                                                <span class="ace-icon fa fa-caret-down icon-only"></span>
                                            </button>

                                            <ul class="dropdown-menu dropdown-inverse">
                                                <li>
                                                    <a href="Downloads/Format/water.pdf" target="_blank" style="font-weight: bold; font-size: large;">PDF File</a>
                                                </li>

                                                <li>
                                                    <a href="Downloads/Format/water.doc" target="_blank" style="font-weight: bold; font-size: large;">Word File</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="btn-group">

                                            <%--<a href="Downloads/GO_GST.pdf" target="_blank" style="font-weight: bold; font-size: large;">
                                                <button class="btn btn-danger">Download GO for GST Calculation</button>
                                            </a>--%>
                                            <button class="btn btn-empty">
                                                <a href="Downloads/GO_GST.pdf" target="_blank" style="font-weight: bold; font-size: large;">Download GO for GST Calculation
                                                </a>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h3 class="header smaller lighter blue">Project List</h3>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Scheme </label>
                                                            <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="div1" runat="server">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="div2" runat="server">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label14" runat="server" Text="Project Type" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <br />
                                                            <asp:CheckBox ID="chkShow" Text="Show Only Thosse Projects Whose Documents Has been Uploaded" runat="server"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <br />
                                                            <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <!-- div.table-responsive -->
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                                <div class="btn-group">
                                                                    <button class="btn btn-app btn-pink btn-xs">
                                                                        <i class="ace-icon fa fa-share bigger-175"></i>
                                                                        Download										
                                                                    </button>

                                                                    <button data-toggle="dropdown" class="btn btn-app btn-pink btn-xs dropdown-toggle" aria-expanded="false">
                                                                        <span class="bigger-110 ace-icon fa fa-caret-down icon-only"></span>
                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-pink">
                                                                        <li>
                                                                            <a target="_blank" href="<%# Eval("Report") %>">Report</a>
                                                                        </li>
                                                                        <li class="divider"></li>
                                                                        <li>
                                                                            <a target="_blank" href="<%# Eval("Annexture_1") %>">Annexture 1</a>
                                                                        </li>

                                                                        <li>
                                                                            <a target="_blank" href="<%# Eval("Annexture_2") %>">Annexture 2</a>
                                                                        </li>
                                                                        <li>
                                                                            <a target="_blank" href="<%# Eval("Annexture_3") %>">Annexture 3</a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Project" DataField="Project_Name" />
                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                        <asp:BoundField HeaderText="Description" DataField="ProjectWork_Description" />
                                                        <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                                        <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                                        <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                                        <asp:BoundField HeaderText="Physical %" DataField="Physical_Progress" />
                                                        <asp:BoundField HeaderText="Financial %" DataField="Financial_Progress" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="divUpload" runat="server" visible="false">
                                <h3 class="header smaller red">Upload Reporting Format Annextures
                                </h3>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Report:</label>
                                                <asp:FileUpload ID="flReport" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Annexture 1 Of Report:</label>
                                                <asp:FileUpload ID="flAnnexture1" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Annexture 2 Of Report:</label>
                                                <asp:FileUpload ID="flAnnexture2" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Upload Annexture 3 Of Report:</label>
                                                <asp:FileUpload ID="flAnnexture3" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
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
    <script src="assets/js/dataTables.colReorder.min.js"></script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPost').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPost')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPost')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: true,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPost .dropdown-toggle', function (e) {
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
    </script>
</asp:Content>

