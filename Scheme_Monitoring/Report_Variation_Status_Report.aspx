<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_Variation_Status_Report.aspx.cs" Inherits="Report_Variation_Status_Report" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpTimeLine" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Variation Status Report
                                           
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
                                                                        <asp:TemplateField HeaderText="Total Packages">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDPR" runat="server" Text='<%# Eval("TotalPkg") %>' OnClick="lnkDPR_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkDPRF" runat="server" OnClick="lnkDPRF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Executive Eng / Project Manager">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkExPM" runat="server" Text='<%# Eval("Ex_PM") %>' OnClick="lnkExPM_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkExPMF" runat="server" OnClick="lnkExPMF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At SE / GM">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSEGM" runat="server" Text='<%# Eval("SE_GM") %>' OnClick="lnkSEGM_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkSEGMF" runat="server" OnClick="lnkSEGMF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Zonal Chief Engineer / CGM">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkZCE" runat="server" Text='<%# Eval("ZCE") %>' OnClick="lnkZCE_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkZCEF" runat="server" OnClick="lnkZCEF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At Chief Engineer HQ">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkCE" runat="server" Text='<%# Eval("CE") %>' OnClick="lnkCE_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnkCEF" runat="server" OnClick="lnkCEF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="At PPRBD Cell">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPPRBD" runat="server" Text='<%# Eval("PPRBD_SE") %>' OnClick="lnkPPRBD_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnPPRBDF" runat="server" OnClick="lnPPRBDF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Variation Approved">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkVariationComplete" runat="server" Text='<%# Eval("Variation_Complete") %>' OnClick="lnkVariationComplete_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:LinkButton ID="lnVariationCompleteF" runat="server" OnClick="lnVariationCompleteF_Click" ForeColor="White"></asp:LinkButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
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
                                                                        <asp:BoundField DataField="Package_ExtraItem_Id" HeaderText="Package_ExtraItem_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Package_ExtraItem_ProjectWorkPkg_Id" HeaderText="Package_ExtraItem_ProjectWorkPkg_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PackageExtraItemApproval_Next_Designation_Id" HeaderText="PackageExtraItemApproval_Next_Designation_Id">
                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                            <ItemStyle CssClass="displayStyle" />
                                                                            <FooterStyle CssClass="displayStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PackageExtraItemApproval_Next_Organisation_Id" HeaderText="PackageExtraItemApproval_Next_Organisation_Id">
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
                                                                                <asp:ImageButton ID="btnOpenTimeline" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimeline_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Date" DataField="Package_ExtraItem_AddedOn" />
                                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                                        <asp:BoundField HeaderText="Current Tender Cost (In Lakhs)" DataField="ProjectWorkPkg_AgreementAmount" />
                                                                        <asp:BoundField HeaderText="Revised Tender Cost (In Lakhs)" DataField="Package_ExtraItem_NewTenderCost" />
                                                                        <asp:BoundField HeaderText="Comments" DataField="PackageExtraItemApproval_Comments" />
                                                                        <asp:BoundField HeaderText="Date Processed" DataField="PackageExtraItemApproval_Date" />

                                                                        <asp:TemplateField HeaderText="Approval Letter">
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href='<%# Eval("Package_ExtraItem_ApprovalFilePath") %>'>
                                                                                    <button class="btn btn-danger btn-sm" title="Download Approval Letter">
                                                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                                                    </button>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Excel File">
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href='<%# Eval("Package_ExtraItem_ExtraItemFilePath") %>'>
                                                                                    <button class="btn btn-danger btn-sm" title="Download Excel File For Extra Item">
                                                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                                                    </button>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Open">
                                                                            <ItemTemplate>
                                                                                <a href="MasterProjectWorkMIS_7.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Variation Approval</a>
                                                                                <br />
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
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                                <h3 class="header smaller red">Timeline Analysis 
                                </h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdTimeLine" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdTimeLine_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="PackageExtraItemApproval_Id" HeaderText="PackageExtraItemApproval_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Mode" HeaderText="Mode" />
                                                    <asp:BoundField DataField="PackageExtraItemApproval_Status_Text" HeaderText="Action Taken" />
                                                    <asp:BoundField DataField="PackageExtraItemApproval_Comments" HeaderText="Comments (If Any)" />
                                                    <asp:BoundField DataField="Designation_Current" HeaderText="Action By (Designation)" />
                                                    <asp:BoundField DataField="Person_Name" HeaderText="Action By (Name)" />
                                                    <asp:BoundField DataField="Designation_Next" HeaderText="Next Action (Designation)" />
                                                    <asp:BoundField DataField="PackageExtraItemApproval_AddedOn" HeaderText="Action Taken On" />
                                                    <asp:BoundField DataField="t1" HeaderText="Time Elapsed (Overall)" />
                                                    <asp:BoundField DataField="t2" HeaderText="Time Elapsed (Step Wise)" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnclose3" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
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



