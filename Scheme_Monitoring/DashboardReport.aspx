<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="DashboardReport.aspx.cs" Inherits="DashboardReport" MaintainScrollPositionOnPostback="true" %>

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

                    <cc1:ModalPopupExtender ID="mpTimeLine" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <!-- /.ace-settings-container -->
                        <div class="page-header">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <h1>Dashboard							
                                        <small>
                                            <i class="ace-icon fa fa-angle-double-right"></i>
                                            Overview &amp; Stats
                                        </small>
                                        
                                    </h1>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtMappingWith" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtMappingWith_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Project For Division" Value="D"></asp:ListItem>
                                            <asp:ListItem Text="Project For ULB" Value="U"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.page-header -->
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Scheme</label>
                                    <asp:ListBox ID="ddlScheme" runat="server" SelectionMode="Multiple" class="chosen-select form-control" data-placeholder="Choose a Scheme..."></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3" id="divZone" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Organization</label>
                                    <asp:DropDownList ID="ddlBranchOffice" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBranchOffice_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Designation</label>
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Search By</label>
                                    <asp:RadioButtonList ID="rbtSearchBy" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Till Date</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">Date Range</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-3" id="divFromDate" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Date From</label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" id="divTillDate" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Date Till</label>
                                    <asp:TextBox ID="txtDateTill" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div id="divBalance1" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        SNA Account Status                               
                                    </div>
                                </div>
                            </div>
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
                                                                    <img src="assets/images/bank_account.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkProject" runat="server" Font-Bold="true" Text="0" OnClick="lnkProject_Click"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Total Limit Assigned</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/balance_total.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkTotalAssigned" runat="server" Font-Bold="true"></asp:LinkButton></span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-6 col-sm-3 pricing-box">
                                    <div class="widget-box widget-color-orange">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter"><b>Limit Utilized</b></h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <div class="infobox infobox-blue">
                                                            <div class="infobox-icon">
                                                                <i>
                                                                    <img src="assets/images/assigned_limit.png" width="60px" height="60px" />
                                                                </i>
                                                            </div>
                                                            <div class="infobox-data">
                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                    <asp:LinkButton ID="lnkUtilized" runat="server" Font-Bold="true"></asp:LinkButton></span>
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
                                            <h5 class="widget-title bigger lighter">Available Balance</h5>
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
                                                                    <asp:LinkButton ID="lnkAvailable" runat="server"></asp:LinkButton></span>
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



                        <h3 class="header smaller red">Pending Action(s) 
                        </h3>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs" id="myTab2">
                                        <li class="active" id="w_1" onclick="setTabPageActive1('w_1', 'wt_1', 'doc1', 4)">
                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Package Invoices
                                                <span class="badge badge-danger" runat="server" id="sp_Invoice">4</span>
                                            </a>
                                        </li>

                                        <li class="" id="w_2" onclick="setTabPageActive1('w_2', 'wt_2', 'doc2', 4)">
                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Other Departmental Payments Invoices
                                                <span class="badge badge-danger" runat="server" id="sp_OtherDept">4</span>
                                            </a>
                                        </li>

                                        <li class="" id="w_3" onclick="setTabPageActive1('w_3', 'wt_3', 'doc3', 4)">
                                            <a data-toggle="tab" href="#doc3" aria-expanded="false" id="wt_3">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Mobilization Advance / Designe and Drawing Payments Invoices
                                                <span class="badge badge-danger" runat="server" id="sp_OtherPayment">4</span>
                                            </a>
                                        </li>

                                        <li class="" id="w_4" onclick="setTabPageActive1('w_4', 'wt_4', 'doc4', 4)">
                                            <a data-toggle="tab" href="#doc4" aria-expanded="false" id="wt_4">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Deduction Release Payments Invoices
                                                <span class="badge badge-danger" runat="server" id="sp_DeductionRelease">4</span>
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="doc1" class="tab-pane fade active in">
                                            <div class="clearfix">
                                                <div class="pull-right grdInvoicetableTools-container"></div>
                                            </div>
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdInvoice" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdInvoice_PreRender" OnRowDataBound="grdInvoice_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="PackageInvoice_Id" HeaderText="PackageInvoice_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PackageInvoice_Package_Id" HeaderText="PackageInvoice_Package_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PackageInvoice_ProcessedBy" HeaderText="PackageInvoice_ProcessedBy">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PackageInvoiceApproval_Next_Organisation_Id" HeaderText="PackageInvoiceApproval_Next_Organisation_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PackageInvoiceApproval_Next_Designation_Id" HeaderText="PackageInvoiceApproval_Next_Designation_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PackageInvoiceCover_Id" HeaderText="PackageInvoiceCover_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:ImageButton ID="btnOpenTimeline1" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimeline1_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnOpenInvoice" runat="server" Height="20px" ImageUrl="~/assets/images/edit.png" OnClick="btnOpenInvoice_Click" Width="20px" />
                                                                <br />
                                                                <asp:ImageButton ID="btnCover" runat="server" Height="60px" ImageUrl="~/assets/images/cover.png" OnClick="btnCover_Click" Width="50px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bulk Action">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkMark" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="List_EMBNo" HeaderText="EMB No" />
                                                        <asp:BoundField DataField="PackageInvoice_Date" HeaderText="Invoice Date" />
                                                        <asp:BoundField DataField="PackageInvoice_VoucherNo" HeaderText="Voucher No" />
                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                        <asp:BoundField DataField="Total_Line_Items" HeaderText="Total Items" />
                                                        <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                        <asp:BoundField DataField="Total_Amount_D" HeaderText="Deduction" />
                                                        <asp:BoundField DataField="Total_Amount_F" HeaderText="Total Amount" />
                                                        <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                        <asp:BoundField DataField="PackageInvoice_ProcessedOn" HeaderText="Processed On" />
                                                        <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                        <asp:BoundField DataField="Invoice_Status" HeaderText="Current Status" />
                                                        <asp:BoundField DataField="InvoiceAdditionalStatus" HeaderText="Reason (If Any)" />
                                                        <asp:BoundField DataField="PackageInvoice_IsPrinted" HeaderText="PackageInvoice_IsPrinted">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PackageInvoice_Type" HeaderText="PackageInvoice_Type">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Invoice">
                                                            <ItemTemplate>
                                                                <a href='MasterGenerateInvoice_View.aspx?Package_Id=0&Invoice_Id=<%# Eval("PackageInvoice_Id") %>' target="_blank">View Invoice</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <div class="space-6"></div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnMark" Text="Mark" OnClick="btnMark_Click" runat="server" CssClass="btn btn-warning"></asp:Button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc2" class="tab-pane fade">
                                            <div class="clearfix" id="Div2" runat="server">
                                                <div class="pull-right tableTools-container-ADP"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdADP" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdADP_PreRender" OnRowDataBound="grdADP_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
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
                                                        <asp:BoundField DataField="Package_ADP_Id" HeaderText="Package_ADP_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="PackageADPApproval_Next_Organisation_Id" HeaderText="PackageADPApproval_Next_Organisation_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="PackageADPApproval_Next_Designation_Id" HeaderText="PackageADPApproval_Next_Designation_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="Package_ADP_Loop" HeaderText="Package_ADP_Loop">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:ImageButton ID="btnOpenTimelineADP1" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimelineADP1_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEditADP" Width="20px" Height="20px" OnClick="btnEditADP_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bulk Action">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkMark" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkMarkAH" Text="Bulk Action" AutoPostBack="true" OnCheckedChanged="chkMarkAH_CheckedChanged" runat="server" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Package_ADP_RefNo" HeaderText="Ref No" />
                                                        <asp:BoundField DataField="Package_ADP_Date" HeaderText="Ref Date" />
                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                        <asp:BoundField HeaderText="Department" DataField="ADP_Category_Name" />
                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                        <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                        <asp:BoundField HeaderText="Specification" DataField="List_Specification" />
                                                        <asp:BoundField DataField="PackageADPApproval_AddedOn" HeaderText="Processed On" />
                                                        <asp:BoundField DataField="Package_ADP_ADPTotalAmount" HeaderText="Other Departmental Amount" />
                                                        <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                        <asp:BoundField DataField="PackageADP_Status" HeaderText="Current Status" />
                                                        <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Reason (If Any)" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <div class="space-6"></div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnMarkADP" Text="Mark" OnClick="btnMarkADP_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc3" class="tab-pane fade">
                                            <div class="clearfix" id="Div3" runat="server">
                                                <div class="pull-right tableTools-container-MA"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdMA" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdMA_PreRender" OnRowDataBound="grdMA_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
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
                                                        <asp:BoundField DataField="Package_MobilizationAdvance_Id" HeaderText="Package_MobilizationAdvance_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Package_MobilizationAdvance_Loop" HeaderText="Package_MobilizationAdvance_Loop">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:ImageButton ID="btnOpenTimelineMA1" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimelineMA1_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEditMA" Width="20px" Height="20px" OnClick="btnEditMA_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bulk Action">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkMark" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkMarkMA" Text="Bulk Action" AutoPostBack="true" OnCheckedChanged="chkMarkMA_CheckedChanged" runat="server" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Package_MobilizationAdvance_RefNo" HeaderText="Ref No" />
                                                        <asp:BoundField DataField="Package_MobilizationAdvance_Date" HeaderText="Ref Date" />
                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                        <asp:BoundField HeaderText="Budget" DataField="ProjectWork_Budget" />
                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                        <asp:BoundField HeaderText="Agreement Amount" DataField="Package_MobilizationAdvance_AgreementAmount" />
                                                        <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                        <asp:BoundField HeaderText="Advance Type" DataField="Package_MobilizationAdvance_Type_Text" />
                                                        <asp:BoundField HeaderText="Per(%)" DataField="Package_MobilizationAdvance_Per" />
                                                        <asp:BoundField HeaderText="Total Amount" DataField="Package_MobilizationAdvance_TotalAmount" />
                                                        <asp:BoundField DataField="Package_MobilizationAdvanceApproval_AddedOn" HeaderText="Processed On" />
                                                        <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <div class="space-6"></div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnMarkMA" Text="Mark" OnClick="btnMarkMA_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc4" class="tab-pane fade">
                                            <div class="clearfix" id="Div4" runat="server">
                                                <div class="pull-right tableTools-container-DeductionRelease"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdDeductionRelease" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductionRelease_PreRender" OnRowDataBound="grdDeductionRelease_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
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
                                                        <asp:BoundField DataField="Package_DeductionRelease_Id" HeaderText="Package_DeductionRelease_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Package_DeductionRelease_Loop" HeaderText="Package_DeductionRelease_Loop">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:ImageButton ID="btnOpenTimelineDR1" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimelineDR1_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEditDR" Width="20px" Height="20px" OnClick="btnEditDR_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bulk Action">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkMark" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkMarkDR" Text="Bulk Action" AutoPostBack="true" OnCheckedChanged="chkMarkDR_CheckedChanged" runat="server" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Package_DeductionRelease_RefNo" HeaderText="Ref No" />
                                                        <asp:BoundField DataField="Package_DeductionRelease_Date" HeaderText="Ref Date" />
                                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Work Code" DataField="ProjectWork_ProjectCode" />
                                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                        <asp:BoundField HeaderText="Budget" DataField="ProjectWork_Budget" />
                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                        <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                        <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                        <asp:BoundField DataField="Package_DeductionReleaseApproval_AddedOn" HeaderText="Processed On" />
                                                        <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                        <asp:BoundField DataField="Organisation_Current" HeaderText="Forwarded From Organization">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                        <asp:BoundField DataField="OfficeBranch_Name" HeaderText="Pending at Organization">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Package_DeductionRelease_TotalDeductionAmount" HeaderText="TotalDeductionAmount" />
                                                        <asp:BoundField DataField="Package_DeductionRelease_TotalReleaseAmount" HeaderText="TotalReleaseAmount" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <div class="space-6"></div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnMarkDR" Text="Mark" OnClick="btnMarkDR_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; margin-left: -32px" ScrollBars="Auto">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        SNA Account Status                                
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdSNAAccountDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdSNAAccountDetails_PreRender">
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
                                                        <asp:BoundField DataField="Jurisdiction_Name_Eng" HeaderText="District" />
                                                        <asp:BoundField DataField="Project_Name" HeaderText="Scheme" />
                                                        <asp:BoundField DataField="ProjectWork_ProjectCode" HeaderText="Project Code" />
                                                        <asp:BoundField DataField="ProjectWork_Name" HeaderText="Work" />
                                                        <asp:BoundField HeaderText="ACCOUNT NUMBER" DataField="SNAAccountMaster_ACCT_NO" />
                                                        <asp:BoundField HeaderText="Total Limit Assigned" DataField="SNAAccountLimit_AssignedLimit" />
                                                        <asp:BoundField HeaderText="Total Limit Used" DataField="SNAAccountLimitUsed_UsedLimit" />
                                                        <asp:BoundField HeaderText="Total Available Limit" DataField="SNAAccountAvailableLimit" />
                                                        <asp:TemplateField HeaderText="Raise Issue">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnRaiseIssue" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClientClick="btnRaiseIssueClick()" Width="20px" />
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
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="700px">

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Document
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
                                            <button id="btnclose2" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">Timeline Analysis 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdTimeLine" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdTimeLine_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="PackageEMBApproval_Id" HeaderText="PackageEMBApproval_Id">
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
                                                <asp:BoundField DataField="PackageEMBApproval_Status_Text" HeaderText="Action Taken" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Action Reason" />
                                                <asp:BoundField DataField="PackageEMBApproval_Comments" HeaderText="Comments (If Any)" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Action By (Designation)" />
                                                <asp:BoundField DataField="Person_Name" HeaderText="Action By (Name)" />
                                                <asp:BoundField DataField="Designation_Next" HeaderText="Next Action (Designation)" />
                                                <asp:BoundField DataField="PackageEMBApproval_AddedOn" HeaderText="Action Taken On" />
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
        function btnRaiseIssueClick() {
            if (confirm("Wanted to Raise Isuue on this project  !!") == true) {

            }
            else {
            }
        }

    </script>


    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdInvoice').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdInvoice')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdInvoice')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: false,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdInvoicetableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdInvoicetableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdInvoicetableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdInvoice .dropdown-toggle', function (e) {
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

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdEMB').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdEMB')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdEMB')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdEMBtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdEMBtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdEMBtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdEMB .dropdown-toggle', function (e) {
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

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdDeductionRelease').length;
                if (DataTableLength > 0) {
                    try {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdDeductionRelease')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdDeductionRelease')
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
                                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                            myTable.buttons().container().appendTo($('.tableTools-container-DeductionRelease'));

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
                                $('.dt-button-collection').appendTo('.tableTools-container-DeductionRelease .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.tableTools-container-MA')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdDeductionRelease .dropdown-toggle', function (e) {
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
                    catch
                    { }
                }
            })
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdADP').length;
                if (DataTableLength > 0) {
                    try {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdADP')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdADP')
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
                                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                                        "iDisplayLength": 100,
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
                            myTable.buttons().container().appendTo($('.tableTools-container-ADP'));

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
                                $('.dt-button-collection').appendTo('.tableTools-container-ADP .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.tableTools-container-ADP')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdADP .dropdown-toggle', function (e) {
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
                    catch
                    { }
                }
            })
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdMA').length;
                if (DataTableLength > 0) {
                    try {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdMA')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdMA')
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
                                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                                        "iDisplayLength": 100,
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
                            myTable.buttons().container().appendTo($('.tableTools-container-MA'));

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
                                $('.dt-button-collection').appendTo('.tableTools-container-MA .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.tableTools-container-MA')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdMA .dropdown-toggle', function (e) {
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
                    catch
                    { }
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

        function setTabPageActive1(mainMenuId, subMenuId, contentPageId, totalCount) {
            debugger;
            for (var i = 0; i < totalCount; i++) {
                $("#w_" + (i + 1)).removeClass('active');
                $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1)).removeClass('active in');
            }

            $("#" + mainMenuId + "").addClass('active');
            $("#" + subMenuId + "").attr('aria-expanded', 'true');
            $("#" + contentPageId + "").addClass('active in');

            sessionStorage["_activeMainTabMenu1"] = mainMenuId;
            sessionStorage["_activeSubTabMenu1"] = subMenuId;
            sessionStorage["_activecontentPageId1"] = contentPageId;
            sessionStorage["_activetotalCount1"] = totalCount;
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                debugger;
                if (sessionStorage["_activeMainTabMenu"] == "" || sessionStorage["_activeSubTabMenu"] == undefined || sessionStorage["_activetotalCount"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#inv_" + (i + 1)).removeClass('active');
                        $("#t_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1) + (i + 1)).removeClass('active in');
                    }

                    $("#" + sessionStorage["_activeMainTabMenu"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu"] + "").attr('aria-expanded', 'true');
                }

                if (sessionStorage["_activeMainTabMenu1"] == "" || sessionStorage["_activeSubTabMenu1"] == undefined || sessionStorage["_activetotalCount1"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount1"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#w_" + (i + 1)).removeClass('active');
                        $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1)).removeClass('active in');
                    }

                    $("#" + sessionStorage["_activeMainTabMenu1"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId1"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu1"] + "").attr('aria-expanded', 'true');
                }
            });
        });
    </script>
</asp:Content>

