<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_DPR_BPM_Report_CNDS_View.aspx.cs" Inherits="Report_DPR_BPM_Report_CNDS_View" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">DPR / New Works Status Report
                                            <div style="float: right">
                                                <asp:CheckBox runat="server" ID="chkShowDetailedReport" Text=" Show Detailed Report" AutoPostBack="true" OnCheckedChanged="chkShowDetailedReport_CheckedChanged"></asp:CheckBox>
                                            </div>


                                        </h3>



                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-4" id="divZone" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4" id="divCircle" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4" id="divDivision" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-4" id="divNodal" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" Text="Nodal Department" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:ListBox SelectionMode="Multiple" ID="ddlNodalDept" runat="server" class="chosen-select form-control" data-placeholder="Choose a Nodal Department..." AutoPostBack="true" OnSelectedIndexChanged="ddlNodalDept_SelectedIndexChanged"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4" runat="server" id="divScheme" visible="false">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">
                                                            Scheme
                                                        </label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label71" runat="server" Text="Nodal Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:ListBox SelectionMode="Multiple" ID="ddlNodalDeptScheme" runat="server" class="chosen-select form-control" data-placeholder="Choose a Scheme..."></asp:ListBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>






                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="clearfix">
                                                    <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                </div>
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdFinancialFull" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancialFull_PreRender" OnRowDataBound="grdFinancialFull_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="ProjectDPR_Id" HeaderText="ProjectDPR_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_ProjectTypeId" HeaderText="ProjectDPR_ProjectTypeId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_Project_Id" HeaderText="ProjectDPR_Project_Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_DistrictId" HeaderText="ProjectDPR_DistrictId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_ULBId" HeaderText="ProjectDPR_ULBId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectDPR_DivisionId" HeaderText="ProjectDPR_DivisionId">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="S No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                    <br />
                                                                    <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                            <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                            <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                            <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                            <asp:BoundField HeaderText="ULB" DataField="ULB_Name" />
                                                            <asp:BoundField HeaderText="Work" DataField="ProjectDPR_Name" />
                                                            <asp:BoundField HeaderText="Authorization Letter Date for Preliminary Estimate" DataField="ProjectDPR_Other_PE_Auth_Letter_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="Preliminary Estimate Date" DataField="ProjectDPR_Other_PE_Date" />
                                                            <asp:BoundField HeaderText="Nodal Agency Nomination Date" DataField="ProjectDPR_Other_Nodal_Agency_Nomination_Date" />
                                                            <asp:BoundField HeaderText="Land Availability Date" DataField="ProjectDPR_Other_LandAvailability_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="DPR Prepared" DataField="ProjectDPR_Other_DPR_Prepared" Visible="false" />
                                                            <asp:BoundField HeaderText="Date of sending DPR to Client department" DataField="ProjectDPR_Other_Date_Sending_Client_Department" Visible="false" />
                                                            <asp:BoundField HeaderText="Status of DPR approval from client department/PFAD/EFC" DataField="ProjectDPR_Other_DPRApproval_Status" Visible="false" />
                                                            <asp:BoundField HeaderText="GO Date" DataField="GO_Date" />
                                                            <asp:BoundField HeaderText="GO Number" DataField="GO_Number" Visible="false" />

                                                            <asp:BoundField HeaderText="Basic Cost (In Lakhs)" DataField="ProjectDPR_Form_Work_BasicCost" Visible="false" />
                                                            <asp:BoundField HeaderText="Contigency (In Lakhs)" DataField="ProjectDPR_Form_Work_Contigency" Visible="false" />
                                                            <asp:BoundField HeaderText="Net Cost (In Lakhs)" DataField="ProjectDPR_Form_Work_NetCost" Visible="false" />
                                                            <asp:BoundField HeaderText="Less 5.0% Proficiency cost from abstract of cost (In Lakhs)" DataField="ProjectDPR_Form_Work_ProficiencyCost" Visible="false" />
                                                            <asp:BoundField HeaderText="Work Cost (In Lakhs)" DataField="ProjectDPR_Form_Work_WorkCost" Visible="false" />
                                                            <asp:BoundField HeaderText="Centage (In Lakhs)" DataField="ProjectDPR_Form_Work_Centage" Visible="false" />
                                                            <asp:BoundField HeaderText="GST Cost 18% (In Lakhs)" DataField="ProjectDPR_Form_Work_GST" Visible="false" />
                                                            <asp:BoundField HeaderText="Labour Cess 1.0% (In Lakhs)" DataField="ProjectDPR_Form_Work_LabourCess" Visible="false" />
                                                            <asp:BoundField HeaderText="External Electric connection Cost (In Lakhs)" DataField="ProjectDPR_Form_Work_ElectricCost" Visible="false" />
                                                            <asp:BoundField HeaderText="Bought out Item cost (In Lakhs)" DataField="ProjectDPR_Form_Work_BoughtOut" Visible="false" />
                                                            <asp:BoundField HeaderText="Total Cost [Grand Total] (In Lakhs)" DataField="ProjectDPR_Form_Work_TotalCost" />

                                                            <asp:BoundField HeaderText="Total Cost [Grand Total] (In Lakhs)" DataField="ProjectDPR_Other_HQ_TS_Date" />

                                                            <asp:BoundField HeaderText="Date of DPR sent to HQ for TS" DataField="ProjectDPR_Other_HQ_TS_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="TS Approval from HQ Date" DataField="ProjectDPR_Other_TS_Approval_Date" Visible="false" />

                                                            <asp:BoundField HeaderText="NIT Issue Date" DataField="ProjectDPR_Other_NIT_Issue_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="NIT Cost" DataField="ProjectDPR_Other_NIT_Cost" />
                                                            <asp:BoundField HeaderText="Tender Uploading Date" DataField="ProjectDPR_Other_Tender_Uploading_Date" />
                                                            <asp:BoundField HeaderText="Pre/Bid Meeting Date" DataField="ProjectDPR_Other_PreBid_Meeting_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="Pre/Bid Response Date" DataField="ProjectDPR_Other_PreBid_Response_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="Date Extention Corrigendum (1) Date" DataField="ProjectDPR_Other_Corrigendum1_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="Technical Bid Opening Date" DataField="ProjectDPR_Other_Technical_Bid_Opening_Date" />
                                                            <asp:BoundField HeaderText="Financial Bid Opening Date" DataField="ProjectDPR_Other_Financial_Bid_Opening_Date" />

                                                            <asp:BoundField HeaderText="LoA Issuance Date" DataField="ProjectDPR_Other_LOA_Issue_Date" />
                                                            <asp:BoundField HeaderText="Date of work start" DataField="ProjectDPR_Other_CB_Date" Visible="false" />
                                                            <asp:BoundField HeaderText="Contract bond Date" DataField="ProjectDPR_Other_Work_Start_Date" Visible="false" />

                                                            <asp:BoundField HeaderText="Total Bidders" DataField="Total_Bidders" Visible="false" />
                                                            <asp:BoundField HeaderText="Bidders Technically Qualified" DataField="TechnicalQualified" Visible="false" />
                                                            <asp:BoundField HeaderText="Bidders Financially Qualified" DataField="FinancialQualified" Visible="false" />
                                                            <asp:BoundField HeaderText="L1 Selected" DataField="Qualified_Status" Visible="false" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_ProjectDPR_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic2" runat="server" Value="0" />
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdFinancialFull').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdFinancialFull')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic2').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdFinancialFull')
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
                        myTable.buttons().container().appendTo($('.grdFinancialFulltableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdFinancialFulltableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdFinancialFulltableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdFinancialFull .dropdown-toggle', function (e) {
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

        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }
    </script>
</asp:Content>



