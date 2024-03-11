<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterSNAChildAccount.aspx.cs" Inherits="MasterSNAChildAccount" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">
                                            Scheme
                                       
                                        </label>
                                        <asp:ListBox ID="ddlScheme" runat="server" class="chosen-select form-control" data-placeholder="Choose a Scheme..." SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblZoneH" runat="server" CssClass="control-label no-padding-right" Text="Zone"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchZone" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="div1" runat="server" class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblCircleH" runat="server" CssClass="control-label no-padding-right" Text="Circle"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="div2" runat="server" class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblDivisionH" runat="server" CssClass="control-label no-padding-right" Text="Division"></asp:Label>
                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="rbtDisplayType" runat="server" RepeatDirection="Vertical">
                                        <asp:ListItem Value="1">Limit is Available and Payment Can Be Done</asp:ListItem>
                                        <asp:ListItem Value="2">Payment is Pending Due To Unavailability Of SNA Limit</asp:ListItem>
                                        <asp:ListItem Value="3">Diffrence Between Actual Bank Balance and ePayment Balance</asp:ListItem>
                                        <asp:ListItem Value="4">All Projects</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkShowBreakup" runat="server" Checked="true" Text="Saperate Colums For Pipeline Values" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <span class="label label-danger arrowed" id="sp_Note" runat="server"></span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-warning" OnClick="btnSearch_Click" Text="Search" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnExport1" Text="Export To PDF (Summery)" OnClick="btnExport1_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnExport2" Text="Export To PDF (Detail)" OnClick="btnExport2_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <!-- div.table-responsive -->
                                <div id="dtOptions" runat="server" class="clearfix">
                                    <div class="pull-right tableTools-container">
                                    </div>
                                </div>
                                <!-- div.dataTables_borderWrap -->
                                <div style="overflow: auto">
                                    <asp:GridView ID="grdPost" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" ShowFooter="true" OnRowDataBound="grdPost_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="SNAAccountMaster_Id" HeaderText="SNAAccountMaster_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
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
                                                    <b><a href='Report_AccountStatement.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&AC=<%# Eval("SNAAccountMaster_ACCT_NO") %>' target="_blank"><span style="color: red">Statement</span></a></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Jurisdiction_Name_Eng" HeaderText="District" />
                                            <asp:BoundField DataField="Zone_Name" HeaderText="Zone" />
                                            <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                            <asp:BoundField DataField="Division_Name" HeaderText="Division" />
                                            <asp:BoundField DataField="ProjectWork_ProjectCode" HeaderText="Project Code" />
                                            <asp:BoundField DataField="ProjectWork_Name" HeaderText="Work" />
                                            <asp:BoundField HeaderText="ACCOUNT NAME" DataField="SNAAccountMaster_ACCT_NAME">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="ACCOUNT NUMBER" DataField="SNAAccountMaster_ACCT_NO">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="No Of Pendency Days" DataField="Max_Pendency" />
                                            <asp:BoundField HeaderText="Date Of Oldest Invoice" DataField="Min_Date" />
                                            <asp:BoundField HeaderText="Days Since Last Limit Assigned" DataField="Last_Assigned_Day_Diff" />
                                            <asp:TemplateField HeaderText="Total Invoice">
                                                <ItemTemplate>
                                                    <a target="_blank" href='Work_Detail_Public.aspx?fromdate=&tilldate=&Scheme_Id=<%# Eval("ProjectWork_Project_Id") %>&ProjectWork_Id=<%# Eval("SNAAccountMaster_ProjectWotk_Id") %>'><%# Eval("Total_Invoice_Count") %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Total Limit Assigned" DataField="SNAAccountLimit_AssignedLimit" />
                                            <asp:BoundField HeaderText="Total Limit Used" DataField="SNAAccountLimitUsed_UsedLimit" />
                                            <asp:BoundField HeaderText="Total Available Limit" DataField="SNAAccountAvailableLimit" />
                                            <asp:BoundField HeaderText="Total In Pipeline" DataField="SNAAccountPipelineLimit" />
                                            <asp:BoundField HeaderText="Total In Pipeline (Invoice)" DataField="SNAAccountPipelineLimitInvoice" />
                                            <asp:BoundField HeaderText="Total In Pipeline (Deduction Release)" DataField="SNAAccountPipelineLimitDR" />
                                            <asp:BoundField HeaderText="Total In Pipeline (Other Dept)" DataField="SNAAccountPipelineLimitADP" />
                                            <asp:BoundField HeaderText="Total In Pipeline (Mob Adv)" DataField="SNAAccountPipelineLimitMA" />
                                            <asp:BoundField HeaderText="Available Balance In Bank (PNB)" DataField="SNAAccountMaster_Balance" />
                                            <asp:BoundField HeaderText="PPA Cleared From Bank (PNB)" DataField="Batch_Amount" />
                                            <asp:BoundField HeaderText="Diffrence" DataField="Diffrence" />
                                        </Columns>
                                        <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="700px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Report Document
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Literal ID="ltEmbed" runat="server" />
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
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
