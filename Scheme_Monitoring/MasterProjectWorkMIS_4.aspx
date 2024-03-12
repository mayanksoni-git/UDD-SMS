<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkMIS_4.aspx.cs" Inherits="MasterProjectWorkMIS_4" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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

                    <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div>
                            <ul class="steps" style="margin-left: 0">
                                <li data-step="1" class="active">
                                    <span class="step">1</span>
                                    <span class="title">Basic Details</span>
                                </li>

                                <li data-step="2" class="active">
                                    <span class="step">2</span>
                                    <span class="title">GO Release Details</span>
                                </li>

                                <li data-step="3" class="active">
                                    <span class="step">3</span>
                                    <span class="title">Target & Achivments</span>
                                </li>

                                <li data-step="4" class="active">
                                    <span class="step">4</span>
                                    <span class="title">Physical Components</span>
                                </li>

                                <li data-step="5">
                                    <span class="step">5</span>
                                    <span class="title">Document Vault</span>
                                </li>

                                <li data-step="6">
                                    <span class="step">6</span>
                                    <span class="title">UC Details and Issues</span>
                                </li>

                                <li data-step="7">
                                    <span class="step">7</span>
                                    <span class="title">Variation Details</span>
                                </li>
                            </ul>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Physical Progress Component
                                        <div style="float: right">
                                            <asp:Button runat="server" ID="btnExport" Text="Export" CssClass="btn btn-info" OnClick="btnExport_Click" />
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <span class="label label-danger arrowed">Work Should be mentioned in Comments for Withheld / Blocked Due to Other Work</span>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPhysicalProgress" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdPhysicalProgress_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="PhysicalProgressComponent_Id" HeaderText="PhysicalProgressComponent_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectPkg_PhysicalProgress_Id" HeaderText="ProjectPkg_PhysicalProgress_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PhysicalProgressComponent_EnableList" HeaderText="PhysicalProgressComponent_EnableList">
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
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAllApproveH" runat="server" Text="Select" AutoPostBack="True" OnCheckedChanged="chkSelectAllApproveH_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPostPhysicalProgress" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PhysicalProgressComponent_Component" HeaderText="Component" />
                                                    <asp:BoundField DataField="Unit_Name" HeaderText="Unit" />
                                                    <asp:TemplateField HeaderText="Proposed (Number) As Per Origional">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtProposedNumberO" runat="server" CssClass="form-control " Text='<%# Eval("ProjectPkg_PhysicalProgress_MasterValueF") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Proposed (Number) As Per Actual">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtProposedNumber" runat="server" CssClass="form-control " Text='<%# Eval("ProjectPkg_PhysicalProgress_MasterValue") %>' onkeyup="isNumericVal(this);" ToolTip='<%# Eval("ProjectPkg_PhysicalProgress_MasterValue") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Completed (Number)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtProgressNumber" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_PhysicalProgress") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Withheld / Blocked Due to Other Work (Number)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWithheldNumber" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_WithheldProgress") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Functional (Number)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFunctionalNumber" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_PhysicalFunctional") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Non-Functional (Number)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNonFunctionalNumber" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_PhysicalNonFunctional") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_PhysicalProgress_Remarks") %>' TextMode="MultiLine"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fill Beneficiary Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnBeneficiary" runat="server" Text="Add Beneficiary" OnClick="btnBeneficiary_Click" Font-Bold="true" ForeColor="Black"></asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton ID="btnViewBeneficiary" runat="server" Text="View Beneficiary" OnClick="btnViewBeneficiary_Click" Bold="true" ForeColor="Maroon"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br></br>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Text="Save and Next >>" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSkip" Text="Skip and Next >>" OnClick="btnSkip_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Fill Beneficiary Details
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lblName" runat="server" Text="Beneficiary Name*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtBeneficiaryName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Beneficiary Mobile No*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtBeneficiaryMobile" runat="server" CssClass="form-control" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Beneficiary Aadhar No*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtBeneficiaryAadhar" runat="server" CssClass="form-control" MaxLength="12" onkeyup="isNumericVal(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Upload Beneficiary Photo*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:FileUpload ID="flBeneficiaryPhoto" runat="server"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnSaveBeneficiary" Text="Save Beneficiary" OnClick="btnSaveBeneficiary_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose" Text="Close" runat="server" CssClass="btn btn-danger"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 900px; height: 400px; margin-left: -32px">
                            <div class="row">
                                <div class="col-xs-12">

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <div class="table-header">
                                                View Beneficiary Details
                                            </div>
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkComponentBenfDtls_Id" HeaderText="ProjectWorkComponentBenfDtls_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkComponentBenfDtls_Component_Id" HeaderText="ProjectWorkComponentBenfDtls_Component_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="ProjectWorkComponentBenfDtls_Path" HeaderText="ProjectWorkComponentBenfDtls_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectWorkComponentBenfDtls_Name" HeaderText="Beneficiary Name" />
                                                        <asp:BoundField DataField="ProjectWorkComponentBenfDtls_MobileNo" HeaderText="Beneficiary Mobile" />
                                                        <asp:BoundField DataField="ProjectWorkComponentBenfDtls_AadharNo" HeaderText="Beneficiary Aadhar" />
                                                        <asp:TemplateField HeaderText="Photo">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgPhoto" Width="100px" Height="140px" ImageUrl='<%# Eval("ProjectWorkComponentBenfDtls_Path") %>' runat="server" />
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
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose2" Text="Close" runat="server" CssClass="btn btn-danger"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_Project_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_RowIndx" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_PhysicalProgressComponent_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                    <asp:PostBackTrigger ControlID="btnSaveBeneficiary" />
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
                                        { "bSortable": false },
                                        null, null, null, null, null, null, null,
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
