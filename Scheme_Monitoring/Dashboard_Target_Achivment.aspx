<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="Dashboard_Target_Achivment.aspx.cs" Inherits="Dashboard_Target_Achivment" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                        <div class="page-header">
                            <div class="col-md-12">
                                <div class="col-md-9">
                                    <h1>Achievement Dashboard							
                                    <small>
                                        <i class="ace-icon fa fa-angle-double-right"></i>
                                        Overview &amp; Stats
                                    </small>
                                        <div style="float: right">
                                            <span class="label label-warning arrowed arrowed-right">ALL FIGURES IN INR CRORE</span>
                                        </div>
                                    </h1>
                                </div>
                                <div class="col-md-3 pull-right">
                                    <div style="margin-right: 50px;">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="~/assets/images/mb/Filter.svg" Height="60px" Width="60px"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:ImageButton ID="btnPhysicalView" runat="server" Width="250px" Height="250px" ImageUrl="~/assets/images/Physical.png" OnClick="btnPhysicalView_Click" Style="border: 2px solid #555;"></asp:ImageButton>
                                        <h4 class="header smaller grey"><b>PHYSICAL - TARGET VS ACHIEVEMENT</b></h4>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                </div>--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:ImageButton ID="btnFinancialView" runat="server" Width="250px" Height="250px" ImageUrl="~/assets/images/Financial.png" OnClick="btnFinancialView_Click" Style="border: 2px solid #ff0000;"></asp:ImageButton>
                                        <h4 class="header smaller red"><b>FINANCIAL - TARGET VS ACHIEVEMENT</b></h4>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" runat="server" id="divPhysicalOptions" visible="false">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:ImageButton ID="btnWorkCompletion" runat="server" Width="100px" Height="100px" ImageUrl="~/assets/images/Work_Cpmpleted.jpg" OnClick="btnWorkCompletion_Click" Style="border: 2px solid orange;"></asp:ImageButton>
                                        <h4 class="header smaller orange"><b>Work Completion</b></h4>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:ImageButton ID="btnWorkPhysicalClosure" runat="server" Width="100px" Height="100px" ImageUrl="~/assets/images/Work_Physical_Completed.jpg" OnClick="btnWorkPhysicalClosure_Click" Style="border: 2px solid white;"></asp:ImageButton>
                                        <h4 class="header smaller grey"><b>Handingover</b></h4>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:ImageButton ID="btnFinancialHandover" runat="server" Width="100px" Height="100px" ImageUrl="~/assets/images/Work_Financial_Completed.png" OnClick="btnFinancialHandover_Click" Style="border: 2px solid green;"></asp:ImageButton>
                                        <h4 class="header smaller green"><b>Financial Closure</b></h4>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" runat="server" id="divFinancialOptions" visible="false">
                            <div class="col-md-12">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-2">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number">
                                                                <asp:LinkButton ID="lnkQ1" OnClick="lnkQ1_Click" runat="server" Font-Bold="true">Quarter 1</asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number">
                                                                <asp:LinkButton ID="lnkQ2" OnClick="lnkQ2_Click" runat="server" Font-Bold="true">Quarter 2</asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number">
                                                                <asp:LinkButton ID="lnkQ3" OnClick="lnkQ3_Click" runat="server" Font-Bold="true">Quarter 3</asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-blue">
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number">
                                                                <asp:LinkButton ID="lnkQ4" OnClick="lnkQ4_Click" runat="server" Font-Bold="true">Quarter 4</asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <ul class="list-unstyled spaced2">
                                                <li>
                                                    <div class="infobox infobox-red">
                                                        <div class="infobox-data">
                                                            <span class="infobox-data-number">
                                                                <asp:LinkButton ID="lnkOverAll" OnClick="lnkOverAll_Click" runat="server" Font-Bold="true">Over All</asp:LinkButton></span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-1">
                                </div>
                            </div>
                        </div>

                        <div class="row" runat="server" id="divFinancial" visible="false">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div class="table-header">
                                            <b><%=Session["Default_Division"].ToString() %> WISE TARGET & ACHIEVEMENT - FINANCIAL </b>
                                        </div>
                                        <!-- div.table-responsive -->
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="DivisionFinancialTarget_Id" HeaderText="DivisionFinancialTarget_Id">
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
                                                    <asp:BoundField DataField="Zone_Name" HeaderText="Zone" />
                                                    <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                                    <asp:BoundField DataField="Division_Name" HeaderText="Division" />

                                                    <asp:BoundField DataField="DivisionFinancialTarget_YearTarget" HeaderText="Financial Target For Financial Year" />
                                                    <asp:BoundField DataField="DivisionFinancialTarget_YearAchivment" HeaderText="Financial Achivment For Financial Year" />
                                                    <asp:BoundField DataField="YearAchivment_Per" HeaderText="Financial Achivment Percentage" />

                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q1Target" HeaderText="Financial Target For Q1" />
                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q1Achivment" HeaderText="Financial Achievement For Q1" />
                                                    <asp:BoundField DataField="Q1Achivment_Per" HeaderText="Q1 Achivment Percentage" />

                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q2Target" HeaderText="Financial Target For Q2" />
                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q2Achivment" HeaderText="Financial Achievement For Q2" />
                                                    <asp:BoundField DataField="Q2Achivment_Per" HeaderText="Q2 Achivment Percentage" />

                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q3Target" HeaderText="Financial Target For Q3" />
                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q3Achivment" HeaderText="Financial Achievement For Q3" />
                                                    <asp:BoundField DataField="Q3Achivment_Per" HeaderText="Q3 Achivment Percentage" />

                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q4Target" HeaderText="Financial Target For Q2" />
                                                    <asp:BoundField DataField="DivisionFinancialTarget_Q4Achivment" HeaderText="Financial Achievement For Q2" />
                                                    <asp:BoundField DataField="Q4Achivment_Per" HeaderText="Q4 Achivment Percentage" />
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
                        <br />

                        <div class="row" runat="server" id="divPhysical" visible="false">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="Div1" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div class="table-header">
                                            <b><%=Session["Default_Division"].ToString() %> WISE TARGET & ACHIEVEMENT - PHYSICAL </b>
                                        </div>
                                        <!-- div.table-responsive -->
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPhysical" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPhysical_PreRender" OnRowDataBound="grdPhysical_RowDataBound" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="DivisionPhysicalTarget_Id" HeaderText="DivisionPhysicalTarget_Id">
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
                                                    <asp:BoundField DataField="Zone_Name" HeaderText="Zone" />
                                                    <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                                    <asp:BoundField DataField="Division_Name" HeaderText="Division" />

                                                    <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalCompletionTarget" HeaderText="Work Completion Target" />
                                                    <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalCompletionAchivment" HeaderText="Work Completion Achievement" />
                                                    <asp:BoundField DataField="PhysicalCompletionAchivment_Per" HeaderText="Work Completion Achievement Precentage" />

                                                    <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalHandoverTarget" HeaderText="Handingover Target" />
                                                    <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalHandoverAchivment" HeaderText="Handingover Achievement" />
                                                    <asp:BoundField DataField="PhysicalHandoverAchivment_Per" HeaderText="Handingover Achievement Precentage" />

                                                    <asp:BoundField DataField="DivisionPhysicalTarget_FinancialHandoverTarget" HeaderText="Financial Closure Target" />
                                                    <asp:BoundField DataField="DivisionPhysicalTarget_FinancialHandoverAchivment" HeaderText="Financial Closure Achievement" />
                                                    <asp:BoundField DataField="FinancialHandoverAchivment_Per" HeaderText="Financial Closure Achievement Precentage" />
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

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">

                            <div class="space-6"></div>
                            <h3 class="header smaller red">Apply Filter
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4" id="divZone" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="divCircle" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="divDivision" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Year </label>
                                            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control datepicker" autocomplete="off" Width="80px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
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



