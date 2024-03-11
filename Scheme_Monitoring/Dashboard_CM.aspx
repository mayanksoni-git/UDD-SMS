<%@ Page Language="C#" MasterPageFile="~/TemplateCM.master" AutoEventWireup="true"
    CodeFile="Dashboard_CM.aspx.cs" Inherits="Dashboard_CM" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">

                        <div class="row">
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-green">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Total Projects</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/Project.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkTotalProjects" runat="server" Font-Bold="true" Text="0" OnClick="lnkTotalProjects_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-blue">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Water Supply</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/WaterSupply.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkWater" runat="server" OnClick="lnkWater_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-dark">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Sewarage</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/Sewerage.jpg" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkSewarage" runat="server" Font-Bold="true" OnClick="lnkSewarage_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-blue">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Building Works</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/building-construction.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkBuilding" runat="server" Font-Bold="true" OnClick="lnkBuilding_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-orange">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter"><b>Septage</b></h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/Septage.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkSeptage" runat="server" Font-Bold="true" OnClick="lnkSeptage_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-green3">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Drainage</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/dranage.jpg" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkDranage" runat="server" OnClick="lnkDranage_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-3 pricing-box">
                                <div class="widget-box widget-color-red">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Solid Waste</h5>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-icon">
                                                            <i>
                                                                <img src="assets/images/pmis/solid_waste.png" width="60px" height="60px" />
                                                            </i>
                                                        </div>
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                <asp:LinkButton ID="lnkSolidWaste" runat="server" Font-Bold="true" OnClick="lnkSolidWaste_Click"></asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Projects Completion Status According To Target Month
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-red">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Completed Projects</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Completed.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkCompleted" runat="server" Font-Bold="true" Text="0" OnClick="lnkCompleted_Click"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-grey">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Ongoing Projects</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/ongoing.jpg" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkOnGoing" runat="server" Font-Bold="true" Text="0" OnClick="lnkOnGoing_Click"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-green">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Completing In Current Month</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Progress_C.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkTarget_C" runat="server" Font-Bold="true" Text="0" OnClick="lnkTarget_C_Click"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-blue">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Completing In Next Month</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/pmis/Progress_N.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkTarget_N" runat="server" OnClick="lnkTarget_N_Click"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
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
                                        <div class="table-header">
                                            District Wise Physical And Financial Progress Report (AMRUT)
                                        </div>
                                        <!-- div.table-responsive -->
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" ShowFooter="true" OnRowDataBound="grdPost_RowDataBound">
                                                <Columns>
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
                                                    <asp:TemplateField HeaderText="District">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDistrict" runat="server" Text='<%# Eval("Jurisdiction_Name_Eng") %>' OnClick="lnkDistrict_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Total_Projects" HeaderText="Total Projects" />
                                                    <asp:BoundField DataField="Sanction_Cost" HeaderText="Sanctioned Cost (In Lakhs)" />
                                                    <asp:BoundField DataField="tender_cost" HeaderText="Tender Cost (In Lakhs)" />
                                                    <asp:BoundField DataField="Expenditure" HeaderText="Expenditure (In Lakhs)" />
                                                    <asp:BoundField DataField="AVG_Physical_Progress" HeaderText="Average Physical Progress (%)" />
                                                    <asp:BoundField DataField="AVG_Financial_Progress" HeaderText="Average Financial Progress" />
                                                    <asp:BoundField DataField="Financial_Progress_Per" HeaderText="Average Financial Progress (%)" />
                                                </Columns>
                                                <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!-- PAGE CONTENT ENDS -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 700px; height: 300px; margin-left: -32px" ScrollBars="Auto">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Choose Data To Be Displayed For
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:ImageButton runat="server" ID="btnAmrut1" ImageUrl="~/assets/images/amrut_1.png" OnClick="btnAmrut1_Click" Width="260px" Height="160px" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:ImageButton runat="server" ID="btnAmrut2" ImageUrl="~/assets/images/amrut_2.png" OnClick="btnAmrut2_Click" Width="260px" Height="160px" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                </ContentTemplate>
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
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic1').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
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
                                    "aoColumns": dt_Options_Dynamic1,
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
