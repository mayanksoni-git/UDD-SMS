<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_PPA_Analysis.aspx.cs" Inherits="Report_PPA_Analysis" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                                        <h3 class="header smaller lighter blue">PPA Analysis Between ePayment Portal and PFMS / PNB
                                            
                                        </h3>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-xs-6 col-sm-3 pricing-box">
                                                <div class="widget-box widget-color-dark">
                                                    <div class="widget-header">
                                                        <h5 class="widget-title bigger lighter">Total In Pipeline</h5>
                                                    </div>

                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <ul class="list-unstyled spaced2">
                                                                <li>
                                                                    <div class="infobox infobox-blue">
                                                                        <div class="infobox-icon">
                                                                            <i>
                                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                                            </i>
                                                                        </div>
                                                                        <div class="infobox-data">
                                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                                <asp:LinkButton ID="lnkInPipeline" runat="server"></asp:LinkButton></span>
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
                                                        <h5 class="widget-title bigger lighter">Total PPA Pending At Bank</h5>
                                                    </div>

                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <ul class="list-unstyled spaced2">
                                                                <li>
                                                                    <div class="infobox infobox-blue">
                                                                        <div class="infobox-icon">
                                                                            <i>
                                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                                            </i>
                                                                        </div>
                                                                        <div class="infobox-data">
                                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                                <asp:LinkButton ID="lnkPPAPNB" runat="server"></asp:LinkButton></span>
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
                                                        <h5 class="widget-title bigger lighter">PPA Pending At EE To Upload 
                                                        </h5>
                                                    </div>
                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <ul class="list-unstyled spaced2">
                                                                <li>
                                                                    <div class="infobox infobox-blue">
                                                                        <div class="infobox-icon">
                                                                            <i>
                                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                                            </i>
                                                                        </div>
                                                                        <div class="infobox-data">
                                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                                <asp:LinkButton ID="lnkPPAEE" runat="server"></asp:LinkButton></span>
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
                                                        <h5 class="widget-title bigger lighter">PPA Pending To Generate 
                                                        </h5>
                                                    </div>

                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <ul class="list-unstyled spaced2">
                                                                <li>
                                                                    <div class="infobox infobox-blue">
                                                                        <div class="infobox-icon">
                                                                            <i>
                                                                                <img src="assets/images/rupee.png" width="60px" height="60px" />
                                                                            </i>
                                                                        </div>
                                                                        <div class="infobox-data">
                                                                            <span class="infobox-data-number" style="margin-left: 15px;">
                                                                                <asp:LinkButton ID="lnkPPAPending" runat="server"></asp:LinkButton></span>
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
                                            <div class="col-sm-12">
                                                <div class="tabbable">
                                                    <ul class="nav nav-tabs" id="myTab2">
                                                        <li class="active" id="w_1">
                                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                PPA Generated From PFMS and Submitted To Bank For Payment
                                                            </a>
                                                        </li>

                                                        <li class="" id="w_2">
                                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                PPA Cleared From PNB and Not Uploaded On ePayment Portal
                                                            </a>
                                                        </li>

                                                        <li class="" id="w_3">
                                                            <a data-toggle="tab" href="#doc3" aria-expanded="false" id="wt_3">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                PPA Pending To Generate
                                                            </a>
                                                        </li>

                                                        <li class="" id="w_4">
                                                            <a data-toggle="tab" href="#doc4" aria-expanded="false" id="wt_4">
                                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                                Invalid PPA Linked
                                                            </a>
                                                        </li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div id="doc1" class="tab-pane fade active in">
                                                            <div class="clearfix">
                                                                <div class="pull-right tableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
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
                                                                        <asp:BoundField HeaderText="PPA No" DataField="PFMS_Batch_Number" />
                                                                        <asp:BoundField HeaderText="Amount (In Lakhs)" DataField="Batch_Amount" />
                                                                        <asp:BoundField HeaderText="Received In Bank On" DataField="File_Received_Date" />
                                                                        <asp:BoundField HeaderText="PPA Expiry Date" DataField="Expiry_Date" />
                                                                        <asp:BoundField HeaderText="Days Elapsed" DataField="Date_Diff" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div id="doc2" class="tab-pane fade">
                                                            <div class="clearfix">
                                                                <div class="pull-right grdFinancialFulltableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdFinancialFull" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancialFull_PreRender" OnRowDataBound="grdFinancialFull_RowDataBound" ShowFooter="true">
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
                                                                        <asp:BoundField HeaderText="PPA No" DataField="PFMS_Batch_Number" />
                                                                        <asp:BoundField HeaderText="Amount (In Lakhs)" DataField="Batch_Amount" />
                                                                        <asp:BoundField HeaderText="Transaction Date" DataField="CBSTranDate" />
                                                                        <asp:BoundField HeaderText="Transaction Ref No" DataField="CBSTxnId" />
                                                                        <asp:BoundField HeaderText="Days Elapsed" DataField="Date_Diff" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div id="doc3" class="tab-pane fade">
                                                            <div class="clearfix">
                                                                <div class="pull-right grdPPAPendingtableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdPPAPending" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPPAPending_PreRender" OnRowDataBound="grdPPAPending_RowDataBound" ShowFooter="true">
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
                                                                        <asp:BoundField HeaderText="Oldest Invoice Pending Since Days" DataField="ProjectSNA_Max_Pendency" />
                                                                        <asp:BoundField HeaderText="Total PPA Pending" DataField="Total_PPA" />
                                                                        <asp:BoundField HeaderText="Amount (In Lakhs)" DataField="Total_Amount" />
                                                                        <asp:BoundField HeaderText="Available Limit As Per ePayment" DataField="ProjectSNA_SNAAccountAvailableLimit" />
                                                                        <asp:BoundField HeaderText="Available Limit As Per PNB" DataField="SNAAccountMaster_Balance" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div id="doc4" class="tab-pane fade">
                                                            <div class="clearfix">
                                                                <div class="pull-right grdPPAInvalidtableTools-container"></div>
                                                            </div>
                                                            <div style="overflow: auto">
                                                                <asp:GridView ID="grdPPAInvalid" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPPAInvalid_PreRender" OnRowDataBound="grdPPAInvalid_RowDataBound" ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="PackageInvoice_Id" HeaderText="PackageInvoice_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Usage_Type" HeaderText="Usage_Type">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Division_Id" HeaderText="Division_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="S No.">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:BoundField HeaderText="Invoice Type" DataField="Usage_Type_Text" />
                                                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                                        <asp:BoundField HeaderText="Invoice Date" DataField="PackageInvoice_Date" />
                                                                        <asp:BoundField HeaderText="Invoice no" DataField="PackageInvoice_VoucherNo" />
                                                                        <asp:BoundField HeaderText="PPA Amount (In Lakhs)" DataField="SNAAccountLimitUsed_PPAAmount" />
                                                                        <asp:BoundField HeaderText="PPA Number" DataField="SNAAccountLimitUsed_PPANumber" />
                                                                        <asp:BoundField HeaderText="PPA Date" DataField="SNAAccountLimitUsed_PPADate" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
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
                    </div>
                   
                    <asp:HiddenField ID="hf_dt_Options_Dynamic1" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic2" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic3" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic4" runat="server" Value="0" />
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
                        //And for the first simple table, which doesn't have grdFinancialFulltableTools or dataTables
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPPAPending').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPPAPending')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic3').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPPAPending')
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
                        myTable.buttons().container().appendTo($('.grdPPAPendingtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdPPAPendingtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdPPAPendingtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPPAPending .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have grdPPAPendingtableTools or dataTables
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPPAInvalid').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPPAInvalid')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        var hf_dt_Options_Dynamic1 = $('#ctl00_ContentPlaceHolder1_hf_dt_Options_Dynamic4').val();
                        var dt_Options_Dynamic1;
                        dt_Options_Dynamic1 = JSON.parse(hf_dt_Options_Dynamic1);
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPPAInvalid')
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
                        myTable.buttons().container().appendTo($('.grdPPAInvalidtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdPPAInvalidtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdPPAInvalidtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPPAInvalid .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have grdPPAPendingtableTools or dataTables
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



