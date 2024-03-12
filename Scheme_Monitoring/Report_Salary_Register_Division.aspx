<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="Report_Salary_Register_Division.aspx.cs" Inherits="Report_Salary_Register_Division" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
                EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Salary Register Division Wise</h3>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Month" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Year" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="form-control datepicker" autocomplete="off" Width="80px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
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
                                            Division Wise Salary Details
                                       
                                        </div>

                                        <!-- div.table-responsive -->

                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender"
                                                ShowFooter="true" OnRowDataBound="grdPost_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="Zone_Id" HeaderText="Zone_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Circle_Id" HeaderText="Circle_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Division_Id" HeaderText="Division_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Total Employee" DataField="Total_Employee" />
                                                    <asp:BoundField HeaderText="Basic Pay (1)" DataField="Basic">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Grade Pay (2)" DataField="Grade_Pay">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="D.A. (3)" DataField="DA">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="H.R.A. (4)" DataField="HRA">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="M.A. (5)" DataField="MA">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Personal Pay (6)" DataField="Personal_Pay">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Special Pay (7)" DataField="Special_Pay">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Other All. (8)" DataField="Other_All">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Gross Salary (9 = 1+2+3+4+5+6+7+8)" DataField="Gross_Sal">
                                                        <HeaderStyle BackColor="Cornsilk" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Employer's NPS cont. Current (10)" DataField="Employer_NPS_cont" />
                                                    <asp:BoundField HeaderText="Employer's NPS cont. Arrear (11)" DataField="Employer_NPS_cont_arr" />
                                                    <asp:BoundField HeaderText="Gross Salary Including NPS Contribution Total (12 = 9+10+11)" DataField="Total_Gross_Sal" />
                                                    <asp:BoundField HeaderText="G.P.F. (13)" DataField="GPF">
                                                        <HeaderStyle BackColor="#ff9933" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="G.P.F. Advance (14)" DataField="GPF_Adv">
                                                        <HeaderStyle BackColor="#ff9933" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="G.I.S. (15)" DataField="GIS">
                                                        <HeaderStyle BackColor="#ff9933" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Total Deduction to be Invested at HQ level (16 = 13+14+15)" DataField="Deduction_Total_HQ">
                                                        <HeaderStyle BackColor="#ff9933" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Income Tax (17)" DataField="Income_Tax">
                                                        <HeaderStyle BackColor="#6699ff" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="NPS Current (Employee) (18)" DataField="NPS_Employee">
                                                        <HeaderStyle BackColor="#6699ff" />
                                                    </asp:BoundField>
                                                     <asp:BoundField HeaderText="NPS Arrear (Employee) (19)" DataField="NPS_Employee_Arr">
                                                        <HeaderStyle BackColor="#6699ff" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Total Deduction to be Paid (20 = 17+18+19)" DataField="Deduction_Total_Paid">
                                                        <HeaderStyle BackColor="#6699ff" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="HRA For Jal Nigam Colony Employee (21)" DataField="HRA1">
                                                        <HeaderStyle BackColor="#cccccc" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Colony Maintance (22)" DataField="Colony_Maintance">
                                                        <HeaderStyle BackColor="#cccccc" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Motor Vehicle Deduction (23)" DataField="Motor_Vehicle_Deduction">
                                                        <HeaderStyle BackColor="#cccccc" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Other Deduction (24)" DataField="Other_Deduction">
                                                        <HeaderStyle BackColor="#cccccc" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Total Deduction to be not Paid (25 = 21+22+23+24)" DataField="Deduction_Total_Not_Paid">
                                                        <HeaderStyle BackColor="#cccccc" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Total Deduction (26 = 10+11+16+20+25)" DataField="Total_Deduction" />
                                                    <asp:BoundField HeaderText="Net Salary Payble To Division (27 = 12-16-25)" DataField="Net_Salary" />
                                                    <asp:BoundField HeaderText="Net Salary Payble To Employee (28 = 12-16-25-10-11-20)" DataField="Net_Salary_Employee" />
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnView" runat="server" Height="20px" ImageUrl="~/assets/images/View.png" OnClick="btnView_Click" Width="20px" ToolTip="View Details" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#666666" ForeColor="White" Font-Bold="true" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!-- PAGE CONTENT ENDS -->
                            </div>
                            <!-- /.col -->
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
    <%-- <script src="assets/js/bootstrap.min.js"></script>--%>
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
                                        { "bSortable": false },
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
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

