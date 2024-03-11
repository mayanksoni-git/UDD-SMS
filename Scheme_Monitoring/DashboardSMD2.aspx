<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="DashboardSMD2.aspx.cs" Inherits="DashboardSMD2" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpViewSummery" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpViewSummeryExpert" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpTimeLine" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpMISStatus" runat="server" PopupControlID="Panel4" TargetControlID="btnShowPopup4"
                        CancelControlID="btnclose4" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup4" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="ProViewSummery" runat="server" PopupControlID="Panel5"
                        TargetControlID="btnShowPopup5"
                        CancelControlID="btnclose5" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup5" Text="Show" runat="server" Style="display: none;"></asp:Button>

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
                                        <div style="float: right" runat="server" id="divMIS">
                                            <asp:Button ID="btnMIS" Text="PMIS Report" runat="server" CssClass="btn btn-inverse" OnClick="btnMIS_Click"></asp:Button>
                                        </div>
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
                                    <asp:Label ID="lblDivisionH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">Search By</label>
                                    <asp:RadioButtonList ID="rbtSearchBy" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Till Date</asp:ListItem>
                                        <asp:ListItem Value="2">Date Range</asp:ListItem>
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

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs" id="myTab22">
                                        <li class="active" id="inv_1" onclick="setTabPageActive('inv_1', 't_1', 'doc11', 4)">
                                            <a data-toggle="tab" href="#doc11" aria-expanded="true" id="t_1">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Invoice Status & TAT Summery
                                            </a>
                                        </li>

                                        <li class="" id="inv_2" onclick="setTabPageActive('inv_2', 't_2', 'doc22', 4)">
                                            <a data-toggle="tab" href="#doc22" aria-expanded="false" id="t_2">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Other Departmental Invoices & TAT Status
                                            </a>
                                        </li>

                                        <li class="" id="inv_3" onclick="setTabPageActive('inv_3', 't_3', 'doc33', 4)">
                                            <a data-toggle="tab" href="#doc33" aria-expanded="false" id="t_3">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Mobilization Advance / Design and Drawing Invoice & TAT Status
                                            </a>
                                        </li>

                                        <li class="" id="inv_4" onclick="setTabPageActive('inv_4', 't_4', 'doc44', 4)">
                                            <a data-toggle="tab" href="#doc44" aria-expanded="false" id="t_4">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Deduction Release & TAT Status
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="doc11" class="tab-pane fade active in">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdSummery" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdSummery_PreRender" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab" HeaderText="TAT Slab" />
                                                        <asp:TemplateField HeaderText="Total Invoices">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoice" runat="server" OnClick="lnkTotalInvoice_Click" Font-Bold="true" Text='<%# Eval("Total_Invoice") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending For Marking">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePending" runat="server" OnClick="lnkTotalInvoicePending_Click" Font-Bold="true" Text='<%# Eval("Total_Pending") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingF" runat="server" OnClick="lnkTotalInvoicePendingF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Marked To Experts">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarked" runat="server" OnClick="lnkTotalInvoiceMarked_Click" Font-Bold="true" Text='<%# Eval("Total_Marked") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExp" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExp_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedF" runat="server" OnClick="lnkTotalInvoiceMarkedF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="De Escelated To UPJN">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceDeEscelatedHO" runat="server" OnClick="lnkTotalInvoiceDeEscelatedHO_Click" Font-Bold="true" Text='<%# Eval("Total_DeEscelated_To_HO") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceDeEscelatedHOF" runat="server" OnClick="lnkTotalInvoiceDeEscelatedHOF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Approved For Payment">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePayment" runat="server" OnClick="lnkTotalInvoicePayment_Click" Font-Bold="true" Text='<%# Eval("Total_Approved") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpA" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpA_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentF" runat="server" OnClick="lnkTotalInvoicePaymentF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpAF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpAF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending At Expert">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingE" runat="server" OnClick="lnkTotalInvoicePendingE_Click" Font-Bold="true" Text='<%# Eval("Total_Pending_E") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpP" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpP_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingEF" runat="server" OnClick="lnkTotalInvoicePendingEF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpPF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpPF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deferred With Comments">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejected" runat="server" OnClick="lnkTotalInvoiceRejected_Click" Font-Bold="true" Text='<%# Eval("Total_Deferred") %>' />

                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <div class="inline dropdown-hover">
                                                                        <button class="btn btn-minier btn-primary">
                                                                            See Details
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
                                                                        </button>

                                                                        <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
                                                                            <li>
                                                                                <asp:LinkButton ID="lnkInfoExpR" runat="server" OnClick="lnkInfoExpR_Click" Font-Bold="true" Text="View Expert Wise" />
                                                                            </li>

                                                                            <li>
                                                                                <asp:LinkButton ID="lnkRejectionWise" runat="server" OnClick="lnkRejectionWise_Click" Font-Bold="true" Text="View Deferred Type Wise" />
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedF" runat="server" OnClick="lnkTotalInvoiceRejectedF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />

                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <div class="inline dropdown-hover">
                                                                        <button class="btn btn-minier btn-primary">
                                                                            See Details
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
                                                                        </button>

                                                                        <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
                                                                            <li>
                                                                                <asp:LinkButton ID="lnkInfoExpRF" runat="server" OnClick="lnkInfoExpRF_Click" Font-Bold="true" Text="View Expert Wise" />
                                                                            </li>

                                                                            <li>
                                                                                <asp:LinkButton ID="lnkRejectionWiseF" runat="server" OnClick="lnkRejectionWiseF_Click" Font-Bold="true" Text="View Deferred Type Wise" />
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="De Escelated">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalDeEscelated" runat="server" OnClick="lnkTotalDeEscelated_Click" Font-Bold="true" Text='<%# Eval("Total_DeEscelated") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpD" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpD_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalDeEscelatedF" runat="server" OnClick="lnkTotalDeEscelatedF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpDF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpDF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab_Order" HeaderText="Date_Diff_Slab_Order">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                                </asp:GridView>
                                            </div>

                                        </div>

                                        <div id="doc22" class="tab-pane fade">
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdSummeryADP" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdSummeryADP_PreRender" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab" HeaderText="TAT Slab" />
                                                        <asp:TemplateField HeaderText="Total Invoices">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceADP" runat="server" OnClick="lnkTotalInvoiceADP_Click" Font-Bold="true" Text='<%# Eval("Total_Invoice") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending For Marking">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingADP" runat="server" OnClick="lnkTotalInvoicePendingADP_Click" Font-Bold="true" Text='<%# Eval("Total_Pending") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingADPF" runat="server" OnClick="lnkTotalInvoicePendingADPF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Marked To Experts">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedADP" runat="server" OnClick="lnkTotalInvoiceMarkedADP_Click" Font-Bold="true" Text='<%# Eval("Total_Marked") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpADP" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpADP_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedADPF" runat="server" OnClick="lnkTotalInvoiceMarkedADPF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpADPF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpADPF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Approved For Payment">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentADP" runat="server" OnClick="lnkTotalInvoicePaymentADP_Click" Font-Bold="true" Text='<%# Eval("Total_Approved") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpAADP" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpAADP_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentADPF" runat="server" OnClick="lnkTotalInvoicePaymentADPF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpAADPF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpAADPF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending At Expert">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingADPE" runat="server" OnClick="lnkTotalInvoicePendingADPE_Click" Font-Bold="true" Text='<%# Eval("Total_Pending_E") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpADPE" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpADPE_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingADPEF" runat="server" OnClick="lnkTotalInvoicePendingADPEF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpADPEF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpADPEF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deferred With Comments">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedADP" runat="server" OnClick="lnkTotalInvoiceRejectedADP_Click" Font-Bold="true" Text='<%# Eval("Total_Deferred") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <div class="inline dropdown-hover">
                                                                        <button class="btn btn-minier btn-primary">
                                                                            See Details
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
                                                                        </button>

                                                                        <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
                                                                            <li>
                                                                                <asp:LinkButton ID="lnkInfoExpADPR" runat="server" OnClick="lnkInfoExpADPR_Click" Font-Bold="true" Text="View Expert Wise" />
                                                                            </li>

                                                                            <li>
                                                                                <asp:LinkButton ID="lnkRejectionWiseADP" runat="server" OnClick="lnkRejectionWiseADP_Click" Font-Bold="true" Text="View Deferred Type Wise" />
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedADPF" runat="server" OnClick="lnkTotalInvoiceRejectedADPF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <div class="inline dropdown-hover">
                                                                        <button class="btn btn-minier btn-primary">
                                                                            See Details
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
                                                                        </button>

                                                                        <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
                                                                            <li>
                                                                                <asp:LinkButton ID="lnkInfoExpRADPF" runat="server" OnClick="lnkInfoExpRADPF_Click" Font-Bold="true" Text="View Expert Wise" />
                                                                            </li>

                                                                            <li>
                                                                                <asp:LinkButton ID="lnkRejectionWiseADPF" runat="server" OnClick="lnkRejectionWiseADPF_Click" Font-Bold="true" Text="View Deferred Type Wise" />
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab_Order" HeaderText="Date_Diff_Slab_Order">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="doc33" class="tab-pane fade">
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdSummeryMA" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdSummeryMA_PreRender" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab" HeaderText="TAT Slab" />
                                                        <asp:TemplateField HeaderText="Total Invoices">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMA" runat="server" OnClick="lnkTotalInvoiceMA_Click" Font-Bold="true" Text='<%# Eval("Total_Invoice") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending For Marking">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingMA" runat="server" OnClick="lnkTotalInvoicePendingMA_Click" Font-Bold="true" Text='<%# Eval("Total_Pending") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingMAF" runat="server" OnClick="lnkTotalInvoicePendingMAF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Marked To Experts">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedMA" runat="server" OnClick="lnkTotalInvoiceMarkedMA_Click" Font-Bold="true" Text='<%# Eval("Total_Marked") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpMA" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpMA_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedMAF" runat="server" OnClick="lnkTotalInvoiceMarkedMAF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpMAF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpMAF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Approved For Payment">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentMA" runat="server" OnClick="lnkTotalInvoicePaymentMA_Click" Font-Bold="true" Text='<%# Eval("Total_Approved") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpAMA" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpAMA_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentMAF" runat="server" OnClick="lnkTotalInvoicePaymentMAF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpAMAF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpAMAF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending At Expert">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingMAE" runat="server" OnClick="lnkTotalInvoicePendingMAE_Click" Font-Bold="true" Text='<%# Eval("Total_Pending_E") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpMAE" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpMAE_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingMAEF" runat="server" OnClick="lnkTotalInvoicePendingMAEF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpMAEF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpMAEF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deferred With Comments">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedMA" runat="server" OnClick="lnkTotalInvoiceRejectedMA_Click" Font-Bold="true" Text='<%# Eval("Total_Deferred") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpRMA" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpRMA_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedMAF" runat="server" OnClick="lnkTotalInvoiceRejectedMAF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpRMAF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpRMAF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab_Order" HeaderText="Date_Diff_Slab_Order">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="doc44" class="tab-pane fade">
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdSummeryDR" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdSummeryDR_PreRender" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab" HeaderText="TAT Slab" />
                                                        <asp:TemplateField HeaderText="Total Invoices">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceDR" runat="server" OnClick="lnkTotalInvoiceDR_Click" Font-Bold="true" Text='<%# Eval("Total_Invoice") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending For Marking">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingDR" runat="server" OnClick="lnkTotalInvoicePendingDR_Click" Font-Bold="true" Text='<%# Eval("Total_Pending") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingDRF" runat="server" OnClick="lnkTotalInvoicePendingDRF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Marked To Experts">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedDR" runat="server" OnClick="lnkTotalInvoiceMarkedDR_Click" Font-Bold="true" Text='<%# Eval("Total_Marked") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpDR" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpDR_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceMarkedDRF" runat="server" OnClick="lnkTotalInvoiceMarkedDRF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpDRF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpDRF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Approved For Payment">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentDR" runat="server" OnClick="lnkTotalInvoicePaymentDR_Click" Font-Bold="true" Text='<%# Eval("Total_Approved") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpADR" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpADR_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePaymentDRF" runat="server" OnClick="lnkTotalInvoicePaymentDRF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpADRF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpADRF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pending At Expert">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingDRE" runat="server" OnClick="lnkTotalInvoicePendingDRE_Click" Font-Bold="true" Text='<%# Eval("Total_Pending_E") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpDRE" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpDRE_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoicePendingDREF" runat="server" OnClick="lnkTotalInvoicePendingDREF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpDREF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpDREF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deferred With Comments">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedDR" runat="server" OnClick="lnkTotalInvoiceRejectedDR_Click" Font-Bold="true" Text='<%# Eval("Total_Deferred") %>' />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpRDR" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpRDR_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTotalInvoiceRejectedDRF" runat="server" OnClick="lnkTotalInvoiceRejectedDRF_Click" Font-Bold="true" ForeColor="White" Font-Underline="true" />
                                                                <div class="form-group" style="float: right; padding-right: 10px">
                                                                    <asp:ImageButton ID="btnInfoExpRDRF" runat="server" Height="20px" ImageUrl="~/assets/images/info.png" OnClick="btnInfoExpRDRF_Click" Width="20px" ToolTip="View Expert Wise Details" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Date_Diff_Slab_Order" HeaderText="Date_Diff_Slab_Order">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="space-6"></div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix">
                                        <div class="pull-right grdInvoiceDashVtableTools-container"></div>
                                    </div>

                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdInvoiceDashV" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" OnPreRender="grdInvoiceDashV_PreRender" EmptyDataText="No Records Found" OnRowDataBound="grdInvoiceDashV_RowDataBound">
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
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnOpenInvoiceV" runat="server" Height="20px" ImageUrl="~/assets/images/edit.png" OnClick="btnOpenInvoiceV_Click" Width="20px" />
                                                        <asp:ImageButton ID="btnOpenTimeline" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimeline_Click" Width="20px" ToolTip="Click To Show Timeline" />
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

                                                <asp:BoundField DataField="Total_Line_Items" HeaderText="Total Line Items" />
                                                <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                <asp:BoundField DataField="Total_Amount_D" HeaderText="Deduction" />
                                                <asp:BoundField DataField="Total_Amount_F" HeaderText="Total Amount" />
                                                <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                <asp:BoundField DataField="PackageInvoice_ProcessedOn" HeaderText="Processed On" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                <asp:BoundField DataField="Invoice_Status" HeaderText="Current Status" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus" HeaderText="Reason (If Any)" />
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
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix">
                                        <div class="pull-right grdADPVtableTools-container"></div>
                                    </div>

                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdADPV" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPreRender="grdADPV_PreRender" EmptyDataText="No Records Found" OnRowDataBound="grdADPV_RowDataBound">
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
                                                <asp:BoundField DataField="Package_ADP_Loop" HeaderText="Package_ADP_Loop">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:ImageButton ID="btnOpenTimelineADP" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimelineADP_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                    </ItemTemplate>
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
                                                <asp:BoundField DataField="Package_ADP_ADPTotalAmount" HeaderText="ADP Amount" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                <asp:BoundField DataField="PackageADP_Status" HeaderText="Current Status" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Reason (If Any)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix">
                                        <div class="pull-right grdMAVtableTools-container"></div>
                                    </div>

                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdMAV" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPreRender="grdMAV_PreRender" EmptyDataText="No Records Found" OnRowDataBound="grdMAV_RowDataBound">
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
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:ImageButton ID="btnOpenTimelineMA" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimelineMA_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                    </ItemTemplate>
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
                                                <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                <asp:BoundField DataField="Package_MobilizationAdvanceApproval_AddedOn" HeaderText="Processed On" />
                                                <asp:BoundField DataField="Package_MobilizationAdvance_TotalAmount" HeaderText="Amount" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                <asp:BoundField DataField="Package_MobilizationAdvance_Status" HeaderText="Current Status" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Reason (If Any)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix">
                                        <div class="pull-right grdDRVtableTools-container"></div>
                                    </div>

                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDRV" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPreRender="grdDRV_PreRender" EmptyDataText="No Records Found" OnRowDataBound="grdDRV_RowDataBound">
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
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:ImageButton ID="btnOpenTimelineDR" runat="server" Height="20px" ImageUrl="~/assets/images/timeline.png" OnClick="btnOpenTimelineDR_Click" Width="20px" ToolTip="Click To Show Timeline" />
                                                    </ItemTemplate>
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
                                                <asp:BoundField DataField="Package_DeductionRelease_TotalReleaseAmount" HeaderText="Deduction Release Amount" />
                                                <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                <asp:BoundField DataField="Package_DeductionRelease_Status" HeaderText="Current Status" />
                                                <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Reason (If Any)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Pending Action(s)</h3>

                        <div class="space-6"></div>
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
                                                <asp:GridView ID="grdInvoice" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdInvoice_PreRender" OnRowDataBound="grdInvoice_RowDataBound">
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
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkMarkH" Text="Bulk Action" AutoPostBack="true" OnCheckedChanged="chkMarkH_CheckedChanged" runat="server" />
                                                            </HeaderTemplate>
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

                                                        <asp:BoundField DataField="Total_Line_Items" HeaderText="Total Line Items" />
                                                        <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                        <asp:BoundField DataField="Total_Amount_D" HeaderText="Deduction" />
                                                        <asp:BoundField DataField="Total_Amount_F" HeaderText="Total Amount" />
                                                        <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                        <asp:BoundField DataField="PackageInvoice_ProcessedOn" HeaderText="JNUP To SMD Date" />
                                                        <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                        <asp:BoundField DataField="Invoice_Status" HeaderText="Current Status" />
                                                        <asp:BoundField DataField="InvoiceAdditionalStatus" HeaderText="Reason (If Any)" />
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

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnRevert" Text="Revert" OnClick="btnRevert_Click" runat="server" CssClass="btn btn-purple"></asp:Button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc2" class="tab-pane fade">
                                            <div class="clearfix">
                                                <div class="pull-right grdADPtableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdADP" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdADP_PreRender" OnRowDataBound="grdADP_RowDataBound">
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
                                                        <asp:BoundField HeaderText="Budget" DataField="ProjectWork_Budget" />
                                                        <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                        <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                        <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                        <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                        <asp:BoundField DataField="PackageADPApproval_AddedOn" HeaderText="JNUP To SMD Date" />
                                                        <asp:BoundField DataField="Package_ADP_ADPTotalAmount" HeaderText="ADP Amount" />
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
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnRevertADP" Text="Revert" OnClick="btnRevertADP_Click" runat="server" CssClass="btn btn-purple"></asp:Button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div id="doc3" class="tab-pane fade">
                                            <div class="clearfix">
                                                <div class="pull-right grdMAtableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdMA" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdMA_PreRender" OnRowDataBound="grdMA_RowDataBound">
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
                                                        <asp:BoundField DataField="Package_MobilizationAdvanceApproval_AddedOn" HeaderText="JNUP To SMD Date" />
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

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnRevertMA" Text="Revert" OnClick="btnRevertMA_Click" runat="server" CssClass="btn btn-purple"></asp:Button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc4" class="tab-pane fade">
                                            <div class="clearfix">
                                                <div class="pull-right grdDeductionReleasetableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdDeductionRelease" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductionRelease_PreRender" OnRowDataBound="grdDeductionRelease_RowDataBound">
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
                                                        <asp:BoundField DataField="Package_DeductionReleaseApproval_AddedOn" HeaderText="JNUP To SMD Date" />
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

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnRevertDR" Text="Revert" OnClick="btnRevertDR_Click" runat="server" CssClass="btn btn-purple"></asp:Button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <h3 class="header smaller red">Water and Sewerage Report Annexture Upload Status Summery: 
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdAnnextureDtls" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdAnnextureDtls_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectType_Id" HeaderText="ProjectType_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>                                            
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>         
                                            <asp:TemplateField HeaderText="Project Type">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkProjectType" Text='<%# Eval("ProjectType_Name") %>' OnClick="lnkProjectType_Click" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                            <asp:BoundField HeaderText="Total Projects" DataField="Total_Projects" />
                                            <asp:BoundField HeaderText="Total Projects for Sewerage" DataField="Total_Projects_Sew" />
                                            <asp:BoundField HeaderText="Total Projects for Water Supply" DataField="Total_Projects_Water" />
                                             <asp:BoundField HeaderText="Total Projects for Sewerage (More Than 90)" DataField="Total_Projects_Sew_90" />
                                            <asp:BoundField HeaderText="Total Projects for Water Supply (More Than 90)" DataField="Total_Projects_Water_90" />
                                            <asp:TemplateField HeaderText="Report Uploaded">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkReport" Text='<%# Eval("Report") %>' OnClick="lnkReport_Click" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annexture 1 Uploaded">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAnnexture1" Text='<%# Eval("Annexture_1") %>' OnClick="lnkAnnexture1_Click" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annexture 2 Uploaded">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAnnexture2" Text='<%# Eval("Annexture_2") %>' OnClick="lnkAnnexture2_Click" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annexture 3 Uploaded">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAnnexture3" Text='<%# Eval("Annexture_3") %>' OnClick="lnkAnnexture3_Click" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="space-6"></div>

                        <h3 class="header smaller red">Data Updation Status Report
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="clearfix">
                                        <div class="pull-right grdDataUpdationStatusReporttableTools-container"></div>
                                    </div>
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDataUpdationStatusReport" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdDataUpdationStatusReport_PreRender" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="Zone_Id" HeaderText="Zone_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:RadioButton ID="rbtChoose" runat="server" GroupName="a" AutoPostBack="true"
                                                            OnCheckedChanged="rbtChoose_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkZone" runat="server" OnClick="lnkZone_Click" Font-Bold="true"
                                                            Text='<%# Eval("Zone_Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTotal_P" runat="server" OnClick="lnkTotal_Project_Click" Font-Bold="true"
                                                            Text='<%# Eval("Total_Project") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkTotal_P_F" runat="server" OnClick="lnkTotal_Project_F_Click"
                                                            BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Total_Package" HeaderText="Total Packages" />
                                                <asp:TemplateField HeaderText="BOQ Not Available">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBOQ_NA" runat="server" OnClick="lnkBOQ_NA_Click" Font-Bold="true"
                                                            Text='<%# Eval("Total_BOQ_Not_Available") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkBOQ_NA_F" runat="server" OnClick="lnkBOQ_NA_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Total Freezed Package" DataField="Total_Freezed" />
                                                <asp:TemplateField HeaderText="Total Not Freezed Package">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUnFreezed" runat="server" OnClick="lnkUnFreezed_Click" Font-Bold="true"
                                                            Text='<%# Eval("Total_Not_Freezed") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkUnFreezed_F" runat="server" OnClick="lnkUnFreezed_F_Click"
                                                            BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RA_Status" HeaderText="Total RA Bills Entry Status" />
                                                
                                                <asp:TemplateField HeaderText="Total Projects at Step 0">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_0" runat="server" OnClick="lnkStep_0_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_0") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_0_F" runat="server" OnClick="lnkStep_0_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 1">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_1" runat="server" OnClick="lnkStep_1_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_1") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_1_F" runat="server" OnClick="lnkStep_1_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 2">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_2" runat="server" OnClick="lnkStep_2_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_2") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_2_F" runat="server" OnClick="lnkStep_2_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 3">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_3" runat="server" OnClick="lnkStep_3_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_3") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_3_F" runat="server" OnClick="lnkStep_3_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 4">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_4" runat="server" OnClick="lnkStep_4_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_4") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_4_F" runat="server" OnClick="lnkStep_4_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 5">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_5" runat="server" OnClick="lnkStep_5_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_5") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_5_F" runat="server" OnClick="lnkStep_5_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 6">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_6" runat="server" OnClick="lnkStep_6_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_6") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_6_F" runat="server" OnClick="lnkStep_6_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects at Step 7">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStep_7" runat="server" OnClick="lnkStep_7_Click" Font-Bold="true"
                                                            Text='<%# Eval("At_Step_7") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkStep_7_F" runat="server" OnClick="lnkStep_7_F_Click" BackColor="Gray"
                                                            ForeColor="White" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="true" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:ImageButton ID="btnDownload" OnClick="btnDownload_Click" runat="server" ImageUrl="~/assets/images/excel_import.png"
                                            Width="60px" Height="50px"></asp:ImageButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px" ScrollBars="Auto">
                            <h3 class="header smaller red">List Of Packages for Zone: 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="clearfix">
                                            <div class="pull-right grdPkgViewtableTools-container"></div>
                                        </div>

                                        <asp:GridView ID="grdPkgView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPkgView_PreRender" OnRowDataBound="grdPkgView_RowDataBound">
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
                                                <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
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
                                                <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
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
                                                <asp:BoundField HeaderText="Due Date Of Completion" DataField="ProjectWorkPkg_Due_Date" />
                                                <asp:BoundField HeaderText="Vendor / Contractor" DataField="Vendor_Name" />
                                                <asp:BoundField HeaderText="Vendor / Contractor (Mobile)" DataField="Vendor_Mobile" />
                                                <asp:BoundField HeaderText="Reporting Staff (JE / APE)" DataField="List_ReportingStaff_JEAPE_Name" />
                                                <asp:BoundField HeaderText="Reporting Staff (AE / PE)" DataField="List_ReportingStaff_AEPE_Name" />
                                                <asp:BoundField HeaderText="BOQ Count (Approved / Total)" DataField="Total_BOQ" />
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose1" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 400px; height: 550px; margin-left: -32px">
                            <h3 class="header smaller red" runat="server" id="sp_HeaderText">Expert Wise Invoice Pending List 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdExpertWiseDtls" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdExpertWiseDtls_PreRender" OnRowDataBound="grdExpertWiseDtls_RowDataBound" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="PersonAdditionalCharge_PersonId" HeaderText="PersonAdditionalCharge_PersonId">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Expert Name" DataField="Person_Name" />
                                                <asp:TemplateField HeaderText="Total Invoices">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTotalInvoiceCount" runat="server" OnClick="lnkTotalInvoiceCount_Click" Font-Bold="true" Text='<%# Eval("Total_Invoices") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Amount" DataField="Total_Amount" />
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose2" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
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
                                        <asp:GridView ID="grdTimeLine" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdTimeLine_PreRender">
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

                        <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px"
                            ScrollBars="Auto">
                            <h3 class="header smaller red">List Of Projects for Zone: 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="clearfix">
                                            <div class="pull-right grdProjectstableTools-container"></div>
                                        </div>
                                        <asp:GridView ID="grdProjects" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProjects_PreRender" OnRowDataBound="grdProjects_RowDataBound">
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
                                                <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                <asp:BoundField HeaderText="Project" DataField="Project_Name" />
                                                <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                <asp:BoundField HeaderText="Description" DataField="ProjectWork_Description" />
                                                <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                                <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                                <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose4" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 100%; margin-left: -32px"
                            ScrollBars="Auto">
                            <h3 class="header smaller red">List Of Projects for Zone: 
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="clearfix">
                                            <div class="pull-right grdPkgViewtableTools-container"></div>
                                        </div>
                                        <asp:GridView ID="GridproView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                            EmptyDataText="No Records Found" OnPreRender="grdProView_PreRender" OnRowDataBound="GridproView_RowDataBound">
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
                                                <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                <asp:BoundField HeaderText="Project" DataField="Project_Name" />
                                                <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                <asp:BoundField HeaderText="Description" DataField="ProjectWork_Description" />
                                                <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                                <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                                <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnclose5" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField ID="hf_TAT_Range" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ClickMode" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDownload" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                                    "iDisplayLength": 50,
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdInvoiceDashV').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdInvoiceDashV')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdInvoiceDashV')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdInvoiceDashVtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdInvoiceDashVtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdInvoiceDashVtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdInvoiceDashV .dropdown-toggle', function (e) {
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPkgView').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPkgView')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPkgView')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdPkgViewtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdPkgViewtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdPkgViewtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPkgView .dropdown-toggle', function (e) {
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdADP').length;
                if (DataTableLength > 0) {
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
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

                                    "iDisplayLength": 50,
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
                        myTable.buttons().container().appendTo($('.grdADPtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdADPtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdADPtableTools-container')).find('a.dt-button').each(function () {
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
            })
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdMA').length;
                if (DataTableLength > 0) {
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
                        myTable.buttons().container().appendTo($('.grdMAtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdMAtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdMAtableTools-container')).find('a.dt-button').each(function () {
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
            })
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdDeductionRelease').length;
                if (DataTableLength > 0) {
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
                        myTable.buttons().container().appendTo($('.grdDeductionReleasetableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdDeductionReleasetableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdDeductionReleasetableTools-container')).find('a.dt-button').each(function () {
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
            })
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdADPV').length;
                if (DataTableLength > 0) {
                    try {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdADPV')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdADPV')
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
                                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                            myTable.buttons().container().appendTo($('.grdADPVtableTools-container'));

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
                                $('.dt-button-collection').appendTo('.grdADPVtableTools-container .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.grdADPVtableTools-container')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdADPV .dropdown-toggle', function (e) {
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdDRV').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdDRV')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdDRV')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdDRVtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdDRVtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdDRVtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdDRV .dropdown-toggle', function (e) {
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdMAV').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdMAV')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdMAV')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdMAVtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdMAVtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.tableTools-container-MA')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdMAV .dropdown-toggle', function (e) {
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_GridproView').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_GridproView')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_GridproView')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                        myTable.buttons().container().appendTo($('.grdPkgViewtableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdPkgViewtableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.grdPkgViewtableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_GridproView .dropdown-toggle', function (e) {
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdProjects').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdProjects')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdProjects')
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
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
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
                        myTable.buttons().container().appendTo($('.grdProjectstableTools-container'));

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
                            $('.dt-button-collection').appendTo('.grdProjectstableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.tableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdProjects .dropdown-toggle', function (e) {
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

    <script type="text/javascript">

        function setTabPageActive(mainMenuId, subMenuId, contentPageId, totalCount) {
            debugger;
            for (var i = 0; i < totalCount; i++) {
                $("#inv_" + (i + 1)).removeClass('active');
                $("#t_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1) + (i + 1)).removeClass('active in');
            }
            //$('#nav nav-tabs').find('li[class^="active"]').removeClass('active');
            //$('#nav nav-tabs').find('a').removeAttr('aria-expanded');

            $("#" + mainMenuId + "").addClass('active');
            $("#" + subMenuId + "").attr('aria-expanded', 'true');
            $("#" + contentPageId + "").addClass('active in');

            sessionStorage["_activeMainTabMenu"] = mainMenuId;
            sessionStorage["_activeSubTabMenu"] = subMenuId;
            sessionStorage["_activecontentPageId"] = contentPageId;
            sessionStorage["_activetotalCount"] = totalCount;
        }

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

