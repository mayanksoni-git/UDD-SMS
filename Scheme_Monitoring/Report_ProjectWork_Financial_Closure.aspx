<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_ProjectWork_Financial_Closure.aspx.cs" Inherits="Report_ProjectWork_Financial_Closure" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                                        <h3 class="header smaller lighter blue">Financial Closure Pending Report
                                            <div class="form-group" style="float: right; padding-right: 10px">
                                                <asp:RadioButtonList ID="rbtMappingWith" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtMappingWith_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="Project For Division" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="Project For ULB" Value="U"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </h3>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Scheme </label>
                                                        <asp:ListBox ID="ddlScheme" runat="server" SelectionMode="Multiple" class="chosen-select form-control"
                                                            data-placeholder="Choose a Scheme..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divZone" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDistrict" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">District* </label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divCircle" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divULB" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDivision" runat="server">
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
                                                        <label class="control-label no-padding-right">Physical Progress Completed Days </label>
                                                        <asp:TextBox ID="txtPendencyDays" Text="0" OnClick="btnSearch_Click" runat="server" CssClass="form-control"></asp:TextBox>
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


                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="tabbable">
                                                    <ul class="nav nav-tabs" id="myTab2">
                                                        <li class="active" id="w_1">
                                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                Summery Sheet
                                                            </a>
                                                        </li>

                                                        <li class="" id="w_2">
                                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                Details
                                                            </a>
                                                        </li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div id="doc1" class="tab-pane fade active in">
                                                            <!-- div.table-responsive -->
                                                            <div class="clearfix" id="dtOptions" runat="server">
                                                                <div class="pull-right tableTools-container"></div>
                                                            </div>
                                                            <!-- div.dataTables_borderWrap -->
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
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
                                                                        <asp:TemplateField HeaderText="S No.">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:TemplateField HeaderText="Schemes Completed (A)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSchemesCompleted" runat="server" Text='<%# Eval("Schemes_Completed") %>' OnClick="lnkSchemesCompleted_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkSchemesCompletedF" runat="server" OnClick="lnkSchemesCompletedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Handover Note Sent (B)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverNoteSend" runat="server" Text='<%# Eval("HandoverNoteSend") %>' OnClick="lnkHandoverNoteSend_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverNoteSendF" runat="server" OnClick="lnkHandoverNoteSendF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Handover Done (C)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverDone" runat="server" Text='<%# Eval("HandoverDone") %>' OnClick="lnkHandoverDone_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverDoneF" runat="server" OnClick="lnkHandoverDoneF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Handover In Process (D = B-C)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverPending" runat="server" Text='<%# Eval("HandoverPending") %>' OnClick="lnkHandoverPending_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverPendingF" runat="server" OnClick="lnkHandoverPendingF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Hanover Not Initiated (E = A-B)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverNotInitiated" runat="server" Text='<%# Eval("Hanover_Not_Initiated") %>' OnClick="lnkHandoverNotInitiated_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkHandoverNotInitiatedF" runat="server" OnClick="lnkHandoverNotInitiatedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Under Defect Libelty Period (F)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkUnderDefectLibeltyPeriod" runat="server" Text='<%# Eval("Under_Defect_Libelty_Period") %>' OnClick="lnkUnderDefectLibeltyPeriod_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkUnderDefectLibeltyPeriodF" runat="server" OnClick="lnkUnderDefectLibeltyPeriodF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Financial Closure Done (G)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkFinancialClosureDone" runat="server" Text='<%# Eval("FinancialClosureDone") %>' OnClick="lnkFinancialClosureDone_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkFinancialClosureDoneF" runat="server" OnClick="lnkFinancialClosureDoneF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Financial Closure Pending (H = C-F-G)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkFinancialClosurePending" runat="server" Text='<%# Eval("FinancialClosurePending") %>' OnClick="lnkFinancialClosurePending_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkFinancialClosurePendingF" runat="server" OnClick="lnkFinancialClosurePendingF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Financial Closure Not Initiated (I = C-F)">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkFinancialClosureNotInitiated" runat="server" Text='<%# Eval("FinancialClosure_Not_Initiated") %>' OnClick="lnkFinancialClosureNotInitiated_Click" ></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkFinancialClosureNotInitiatedF" runat="server" OnClick="lnkFinancialClosureNotInitiatedF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>--%>
                                                                    </Columns>
                                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div id="doc2" class="tab-pane fade">
                                                            <div class="clearfix">
                                                                <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdFinancialFull" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancialFull_PreRender" OnRowDataBound="grdFinancialFull_RowDataBound">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="S No.">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                                        <asp:BoundField HeaderText="Physical Progress (%)" DataField="Physical_Progress" />
                                                                        <asp:BoundField HeaderText="Financial Progress (%)" DataField="Financial_Progress" />
                                                                        <asp:BoundField HeaderText="Date When Physical Marked 100%" DataField="ProjectWorkFinancialTarget_AddedOn" />
                                                                        <asp:BoundField HeaderText="Days Diffrence" DataField="Days_Since_Update" />
                                                                        <asp:BoundField HeaderText="Physical Closure Applicable" DataField="PhysicalClosureApplicable" />
                                                                        <asp:BoundField HeaderText="Handover Note Send To Local Body" DataField="HandoverNoteSend" />

                                                                        <asp:BoundField HeaderText="Physical Closure Handover Note Send Letter Date" DataField="ProjectClosure_HandoverNote_Yes_LetterDate" />
                                                                        <asp:BoundField HeaderText="Physical Closure Handover Note Send By Letter No" DataField="ProjectClosure_HandoverNote_Yes_LetterNo" />

                                                                        <asp:BoundField HeaderText="Physical Closure If No Then Tentitive Date" DataField="ProjectClosure_HandoverNote_No_TentitiveDate" />
                                                                        <asp:BoundField HeaderText="Physical Closure If No Then Comments" DataField="ProjectClosure_HandoverNote_No_Comments" />
                                                                                                                                                
                                                                        <asp:TemplateField HeaderText="Physical Closure Handover Note Send Letter">
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href='<%# Eval("ProjectClosure_HandoverNote_Yes_LetterPath") %>'>
                                                                                    <button class="btn btn-danger btn-sm" title="Download Letter">
                                                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                                                    </button>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Defect Libelity Period" DataField="ProjectClosure_HandoverDone_Yes_DefectLibelityPeriod" />
                                                                        <asp:BoundField HeaderText="Handover Done" DataField="HandoverDone" />
                                                                        <asp:BoundField HeaderText="Handover Done Yes By Letter No" DataField="ProjectClosure_HandoverDone_Yes_LetterNo" />
                                                                        <asp:BoundField HeaderText="Handover Done Yes Letter Date" DataField="ProjectClosure_HandoverDone_Yes_LetterDate" />
                                                                        <asp:TemplateField HeaderText="Handover Done Yes Letter">
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href='<%# Eval("ProjectClosure_HandoverDone_Yes_LetterPath") %>'>
                                                                                    <button class="btn btn-danger btn-sm" title="Download Letter">
                                                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                                                    </button>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Handover Done if No Then Tentitive Date" DataField="ProjectClosure_HandoverDone_No_TentitiveDate" />
                                                                        <asp:BoundField HeaderText="Handover Done if No Then Comments" DataField="ProjectClosure_HandoverDone_No_Comments" />
                                                                        <asp:BoundField HeaderText="Financial Closure Applicable" DataField="FinancialClosureApplicable" />
                                                                        <asp:BoundField HeaderText="Financial Closure By Letter No" DataField="ProjectClosure_FinancialClosureApplicable_Yes_LetterNo" />
                                                                        <asp:BoundField HeaderText="Financial Closure On Letter Date" DataField="ProjectClosure_FinancialClosureApplicable_Yes_LetterDate" />
                                                                        <asp:TemplateField HeaderText="Financial Closure Letter">
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href='<%# Eval("ProjectClosure_FinancialClosureApplicable_Yes_LetterPath") %>'>
                                                                                    <button class="btn btn-danger btn-sm" title="Download Letter">
                                                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                                                    </button>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Financial Closure If No Then Tentitive Date" DataField="ProjectClosure_FinancialClosureApplicable_No_TentitiveDate" />
                                                                        <asp:BoundField HeaderText="Financial Closure If No Then Comments" DataField="ProjectClosure_FinancialClosureApplicable_No_Comments" />
                                                                        <asp:BoundField HeaderText="Issue Reported (If Any)" DataField="Issue" />
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
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
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
                                    colReorder: false,
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



