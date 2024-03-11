<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkMISView.aspx.cs" Inherits="MasterProjectWorkMISView" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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

                    <cc1:ModalPopupExtender ID="mpIssue" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-2" runat="server" id="divProjectType">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Project Type:</label>
                                        <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Filter By:</label>
                                        <asp:RadioButtonList ID="rbtFilterBy" runat="server">
                                            <asp:ListItem Text="Physical Progress" Value="P"></asp:ListItem>
                                            <asp:ListItem Text="Financial Progress" Value="F"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lbl" runat="server" Text="From Range" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtFromRange" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Till Range" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtTillRange" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                        &nbsp; &nbsp;
                                        <asp:ImageButton ID="btnDownload" OnClick="btnDownload_Click" runat="server" ImageUrl="~/assets/images/excel_import.png"
                                            Width="60px" Height="50px"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:CheckBox runat="server" ID="chkShowIssue" Text=" Show Issue Column" AutoPostBack="true" OnCheckedChanged="chkShowIssue_CheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divTimeOverRun" runat="server" visible="false">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnExport1" Text="Export Time Over-Run (Summery)" OnClick="btnExport1_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnExport2" Text="Export Over-Run (Detail)" OnClick="btnExport2_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Project List
                                            <div style="float: right" id="divCreateNew" runat="server">
                                                <asp:Button ID="btnCreateNew" Text="Create New Project" OnClick="btnCreateNew_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                            </div>

                                        </h3>
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
                                                    <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_ULB_Id" HeaderText="ProjectWork_ULB_Id">
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
                                                            <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" /><br />
                                                            <div class="btn-group" runat="server" id="divMISSteps">
                                                                <button class="btn btn-app btn-pink btn-xs">
                                                                    <i class="ace-icon fa fa-share bigger-175"></i>
                                                                    MIS Steps
                                                                </button>

                                                                <button data-toggle="dropdown" class="btn btn-app btn-pink btn-xs dropdown-toggle" aria-expanded="false">
                                                                    <span class="bigger-110 ace-icon fa fa-caret-down icon-only"></span>
                                                                </button>

                                                                <ul class="dropdown-menu dropdown-pink">
                                                                    <li>
                                                                        <a target="_blank" href="Report_MasterProjectWorkMIS.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>">MIS Report</a>
                                                                    </li>
                                                                    <li class="divider"></li>
                                                                    <li>
                                                                        <a href="#" onclick="openBasicDetails(this);" project_id='<%# Eval("ProjectWork_Project_Id") %>' projectwork_id='<%# Eval("ProjectWork_Id") %>'>Go To Step 1: Basic Details</a>
                                                                    </li>

                                                                    <li>
                                                                        <a href="#" onclick="openGODetails(this);" project_id='<%# Eval("ProjectWork_Project_Id") %>' projectwork_id='<%# Eval("ProjectWork_Id") %>' district_id='<%# Eval("ProjectWork_DistrictId") %>'>Go To Step 2: GO Release Details</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_3.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 3: Target & Achivments</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_4.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 4: Physical Components</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_5.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 5: Documents Upload</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_6.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 6: UC Details and Other Issues</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_7.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 7: Variation Details</a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div runat="server" id="divPhoto">
                                                                <a target="_blank" href="ProjectWorkGalleryView.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Mode=P&App=false">View Gallery (<%# Eval("Total_Photo") %>)</a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                    <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                    <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                    <asp:BoundField HeaderText="Sanctioned Cost (In Lakhs)" DataField="ProjectWork_Budget" />
                                                    <asp:BoundField HeaderText="Agreement Cost (In Lakhs)" DataField="tender_cost" />
                                                    <asp:BoundField HeaderText="Released Amount (In Lakhs)" DataField="Total_Release" />
                                                    <asp:BoundField HeaderText="Total Expenditure (In Lakhs)" DataField="Total_Expenditure" />
                                                    <asp:BoundField HeaderText="Physical Progress" DataField="Physical_Progress" />
                                                    <asp:BoundField HeaderText="Financial Progress" DataField="Financial_Progress" />
                                                    <asp:BoundField HeaderText="Start Date As Per Agreement" DataField="ProjectWorkPkg_Agreement_Date" />
                                                    <asp:BoundField HeaderText="Actual Start Date" DataField="ProjectWorkPkg_Start_Date" />
                                                    <asp:BoundField HeaderText="End Date As Per Agreement" DataField="ProjectWorkPkg_Due_Date" />
                                                    <asp:BoundField HeaderText="End Date As Per Agreement (Actual)" DataField="Target_Date_Agreement_Extended" />
                                                    <asp:BoundField HeaderText="Issue" DataField="Issue" />
                                                    <asp:TemplateField HeaderText="Issue Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnIssueList" OnClick="btnIssueList_Click" runat="server" Text='<%# Eval("Issue_Available") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Last Updated On (Data)" DataField="ProjectWork_ModifiedOn" />
                                                    <asp:BoundField HeaderText="Last Updated On (Photo)" DataField="Last_Updated" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
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


                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="600px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Issues
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdIssue" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdIssue_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Id" HeaderText="ProjectWorkIssueDetails_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Path" HeaderText="ProjectWorkIssueDetails_Path">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Dependency_Id" HeaderText="ProjectWorkIssueDetails_Dependency_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRowNumber" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectIssue_Name" HeaderText="Type Of Issue" />
                                                    <asp:BoundField DataField="Dependency_Name" HeaderText="Sub-Issue" />
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Date" HeaderText="Effective Start Date of Issue" />
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Comments" HeaderText="Comments" />
                                                    <asp:TemplateField HeaderText="Download Document">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkIssueDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkIssueDetails_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <button id="btnclose2" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDownload" />
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
    <%--<script src="assets/js/dataTables.colReorder.min.js"></script>--%>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPost').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPost')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic1').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPost')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: false,
                                    fixedHeader: {
                                        header: false,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": dt_Options_Dynamic1,
                                    "aaSorting": [],
                                    //"bProcessing": true,
                                    //"bServerSide": true,
                                    //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                    //,
                                    "sScrollY": "450px",
                                    //"bPaginate": false,
                                    "sScrollX": "100%",
                                    "sScrollXInner": "120%",
                                    "bScrollCollapse": true,
                                    //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                    //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                    "iDisplayLength": 25,
                                    select: {
                                        style: 'multi'
                                    },

                                    //initComplete: function (settings, json) {
                                    //    $('.dataTables_scrollBody').on('scroll', function () {
                                    //        $('.dataTables_scrollHeadInner').scrollLeft($(this).scrollLeft());
                                    //    });

                                    //    $(document).on('scroll', function () {
                                    //        var scroll_pos = $(this).scrollTop();
                                    //        var margin = 74; // Adjust it to your needs
                                    //        var cur_pos = $('.dataTables_scrollHeadInner').position();
                                    //        var header_pos = cur_pos.top;
                                    //        if (scroll_pos < margin)
                                    //            var header_pos = margin - scroll_pos;
                                    //        else
                                    //            header_pos = 0;
                                    //        $('.dataTables_scrollHeadInner').css({ "top": header_pos });
                                    //    });
                                    //},

                                    initComplete: function () {
                                        this.api().columns().every(function () {
                                            var column = this;
                                            var select = $('<select><option value=""></option></select>')
                                                .appendTo($(column.footer()).empty())
                                                .on('change', function () {
                                                    var val = $.fn.dataTable.util.escapeRegex(
                                                        $(this).val()
                                                    );

                                                    column
                                                        .search(val ? '^' + val + '$' : '', true, false)
                                                        .draw();
                                                });

                                            column.data().unique().sort().each(function (d, j) {
                                                select.append('<option value="' + d + '">' + d + '</option>')
                                            });
                                        });
                                    }
                                });
                        $('#ctl00_ContentPlaceHolder1_grdPost tfoot tr').insertAfter($('#ctl00_ContentPlaceHolder1_grdPost thead tr'));
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
    <script>

        function openBasicDetails(obj) {
            var project_id = obj.attributes.project_id.nodeValue;
            var projectwork_id = obj.attributes.projectwork_id.nodeValue;
            if (project_id == "1014") {
                obj.href = "MasterProjectWorkMIS_1_Green.aspx?ProjectWork_Id=" + projectwork_id;
            }
            else if (project_id == "1015") {
                obj.href = "MasterProjectWorkMIS_1_DW.aspx?ProjectWork_Id=" + projectwork_id;
            }
            else {
                obj.href = "MasterProjectWorkMIS_1.aspx?ProjectWork_Id=" + projectwork_id;
            }
        }

        function openGODetails(obj) {
            var project_id = obj.attributes.project_id.nodeValue;
            var projectwork_id = obj.attributes.projectwork_id.nodeValue;
            var district_id = obj.attributes.district_id.nodeValue;
            if (project_id == "12") {
                obj.href = "MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else if (project_id == "3") {
                obj.href = "MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else if (project_id == "1015") {
                obj.href = "MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else {
                obj.href = "MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
        }
    </script>
</asp:Content>



