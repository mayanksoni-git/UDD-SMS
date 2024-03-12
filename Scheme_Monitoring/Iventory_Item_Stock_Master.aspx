<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="Iventory_Item_Stock_Master.aspx.cs" Inherits="Iventory_Item_Stock_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <style>
                        .control-label {
                            text-transform: uppercase;
                            color: #081748;
                            font-weight: 500;
                            font-family: Verdana, Geneva, Tahoma, sans-serif;
                            transition: 0.7s;
                        }

                            .control-label:hover {
                                color: blueviolet;
                                font-weight: bold;
                                letter-spacing: 0.8px;
                            }
                    </style>
                    <div class="page-content">
                        <div class="page-header">
                            <div class="row">
                                <h1>Create / Update Item Stock (Opening Stock Quantity)*</h1>
                            </div>
                        </div>
                        </br>
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Choose Office
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        </br>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="divEntry" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="tabbable">
                                                <ul class="nav nav-tabs" id="myTab2">
                                                    <li class="active" id="w_1">
                                                        <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                            <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                            Add New Item Stock In Opening Balance
                                                        </a>
                                                    </li>

                                                    <li class="" id="w_2">
                                                        <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                            <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                            List Of Opening Stock Available
                                                        </a>
                                                    </li>
                                                </ul>
                                                <div class="tab-content">
                                                    <div id="doc1" class="tab-pane fade active in">
                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                                <div class="row">
                                                                    <div style="overflow: auto">
                                                                        <asp:GridView ID="grdAddNewStock" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdAddNewStock_PreRender" OnRowDataBound="grdAddNewStock_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S No.">
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"></asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlProduct" runat="server" class="chosen-select form-control" data-placeholder="Choose a Item..."></asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Rate / Valuation (In Rs)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtItemRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Depriciation Rate (+/-) %">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtDepriciationRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Depriciation Rate (+/-) %">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control">
                                                                                            <asp:ListItem Text="Servicable" Value="1" Selected="True"></asp:ListItem>
                                                                                            <asp:ListItem Text="Surplus" Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Un-Serviciable" Value="3"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                                <div class="row">
                                                                    <div class="col-xs-3">
                                                                        <div class="form-group">
                                                                            <br />
                                                                            <asp:Button ID="btnSave" Text="Create New Stock" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="doc2" class="tab-pane fade">
                                                        <div class="clearfix">
                                                            <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                        </div>
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="InventoryItemStockDetails_Id" HeaderText="InventoryItemStockDetails_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemDetails_Id" HeaderText="InventoryItemDetails_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemStockDetails_ZoneId" HeaderText="InventoryItemStockDetails_ZoneId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemStockDetails_CircleId" HeaderText="InventoryItemStockDetails_CircleId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemStockDetails_DivisionId" HeaderText="InventoryItemStockDetails_DivisionId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemDetails_UnitId" HeaderText="InventoryItemDetails_UnitId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemDetails_CategoryId" HeaderText="InventoryItemDetails_CategoryId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemDetails_TypeId" HeaderText="InventoryItemDetails_TypeId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemDetails_CompanyId" HeaderText="InventoryItemDetails_CompanyId">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InventoryItemDetails_ClassId" HeaderText="InventoryItemDetails_ClassId">
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
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                    <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                    <asp:BoundField HeaderText="Company" DataField="Inventory_Company_Name" />
                                                                    <asp:BoundField HeaderText="Item Name" DataField="InventoryItemDetails_ItemName" />
                                                                    <asp:BoundField HeaderText="DIA" DataField="InventoryItemDetails_DIA" />
                                                                    <asp:BoundField HeaderText="Guage" DataField="InventoryItemDetails_Guage" />
                                                                    <asp:BoundField HeaderText="Length" DataField="InventoryItemDetails_Length" />
                                                                    <asp:BoundField HeaderText="Bredth" DataField="InventoryItemDetails_Bredth" />
                                                                    <asp:BoundField HeaderText="Height" DataField="InventoryItemDetails_Height" />
                                                                    <asp:BoundField HeaderText="Class" DataField="Class_Name" />
                                                                    <asp:BoundField HeaderText="Item Rate / Valuation (In Rs)" DataField="InventoryItemStockDetails_Rate" />
                                                                    <asp:BoundField HeaderText="Quantity" DataField="InventoryItemStockDetails_Quantity" />
                                                                    <asp:BoundField HeaderText="Depriciation Rate (+/-) %" DataField="InventoryItemStockDetails_Depriciation" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div runat="server" visible="false" id="divStockEntry">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Update Item Stock Details
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            </br>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Product Category:</label>
                                                        <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="Servicable" Value="1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Surplus" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Un-Serviciable" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Item Rate / Valuation (In Rs):</label>
                                                        <asp:TextBox ID="txtItemRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Quantity:</label>
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Depriciation Rate (+/-) %:</label>
                                                        <asp:TextBox ID="txtDepriciationRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnUpdate" Text="Update Existing" OnClick="btnUpdate_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hf_InventoryItemDetails_Id" Value="0" />
                        <asp:HiddenField runat="server" ID="hf_InventoryItemStockDetails_Id" Value="0" />
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
</asp:Content>

