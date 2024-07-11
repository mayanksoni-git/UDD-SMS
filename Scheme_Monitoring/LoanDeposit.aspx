<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin_PMS.master" AutoEventWireup="true" CodeFile="LoanDeposit.aspx.cs" Inherits="LoanDeposit" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <style>
        .due {
            background-color: #ffcccc;
        }
    </style>
    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Loan Recovery</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">MIS</li>
                                            <li class="breadcrumb-item active">Loan Recovery</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Basic Detail</h4>
                                        <%--<a class="btn btn-primary" href="#">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-arrow-left-circle-fill" viewBox="0 0 16 16">
                                                    <path d="M8 0a8 8 0 1 0 0 16A8 8 0 0 0 8 0m3.5 7.5a.5.5 0 0 1 0 1H5.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L5.707 7.5z" />
                                                </svg>
                                                Back to Report</a>--%>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-lg-2 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-lg-2 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-lg-2 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                    <div class="col-lg-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-lg-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblProject" runat="server" Text="Project" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row gy-4">
                                                <div class="col-lg-12 col-md-12">
                                                    <ul class="totalloan-amount">
                                                        <li>
                                                            <asp:Label ID="lblTotalLoanAmount" runat="server" Text="" /></li>
                                                        <li>
                                                            <asp:Label ID="lblRemainingLoanAmount" runat="server" Text="" /></li>
                                                        <li>
                                                            <asp:Label ID="lblTotalDueAmount" runat="server" Text="" /></li>
                                                    </ul>
                                                </div>
                                            </div>

                                            
                                            <!--end row-->
                                           </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xxl-12 col-md-12">
                            <div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" visible="false" id="divData">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Loan EMI's Detail</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">

                                                    <asp:GridView ID="gvRecords" runat="server" CssClass="display table table-bordered" ShowFooter="true" AutoGenerateColumns="False" OnRowDataBound="gvEMIs_RowDataBound" EmptyDataText="No Records Found">
                                                        <Columns>
                                                            <asp:BoundField DataField="ProjectWorkGO_Id" HeaderText="Loan Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>

                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>


                                                            <asp:BoundField DataField="ProjectWork_Name" HeaderText="Project Name" />
                                                            <%--<asp:BoundField DataField="ProjectWorkGO_TotalRelease" HeaderText="Installment Amount" />--%>

                                                            <asp:TemplateField HeaderText="Released Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInstallmentAmount" runat="server" Text='<%# Bind("ProjectWorkGO_TotalRelease","{0:0.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalInstallmentAmount" runat="server" Style="font-weight: 700; font-size: 12px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="ProjectWorkGO_GO_Date" HeaderText="Release Date" DataFormatString="{0:yyyy-MM-dd}" />
                                                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />

                                                            <asp:TemplateField HeaderText="Paid Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PaidAmount","{0:0.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalPaidAmount" runat="server" Style="font-weight: 700; font-size: 12px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <%--<asp:BoundField DataField="RemainingAmount" HeaderText="Remaining Amount" />--%>
                                                            <asp:TemplateField HeaderText="RemainingAmount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemainingAmount" runat="server" Text='<%# Bind("RemainingAmount","{0:0.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalRemainingAmount" runat="server" Style="font-weight: 700; font-size: 12px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <b><%# Convert.ToBoolean(Eval("IsPaid")) ? "Paid" : "Unpaid" %></b>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="22" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <!--end row-->

                                            <div class="row gy-4">
                                                <div class="col-lg-2 col-md-6">
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="lblDepositAmount" runat="server" Text="Deposit Amount*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtDepositAmount"  runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblDepositDate" runat="server" Text="Deposit Date*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtDepositDate"  Placeholder="YYYY-MM-DD"  runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtDepositDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>--%>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4 col-md-6">
                                                    <div>
                                                        <label class="d-block"> &nbsp;</label>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfLoanRelease_Id" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>

                    <div runat="server" visible="false" id="divDeposit">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Loan Recovery Detail</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <!-- div.table-responsive -->
                                                <div class="clearfix" id="Div2" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                                <div style="overflow: auto">

                                                    <asp:GridView ID="gvDeposits" runat="server" ShowFooter="true" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                        <Columns>
                                                            <asp:BoundField DataField="DepositID" HeaderText="Deposit Id">
                                                                <HeaderStyle CssClass="displayStyle" />
                                                                <ItemStyle CssClass="displayStyle" />
                                                                <FooterStyle CssClass="displayStyle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Deposit Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDepositeAmount" runat="server" Text='<%# Bind("DepositAmount","{0:0.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbl_TotalDepositAmount" runat="server" Style="font-weight: 700; font-size: 12px; color: #000000"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DepositDate" HeaderText="Deposit Date" DataFormatString="{0:yyyy-MM-dd}" />
                                                            <%--<asp:BoundField DataField="AddedOn" HeaderText="Record Created Date" DataFormatString="{0:yyyy-MM-dd}" />--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <td colspan="22" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                </Triggers>
            </asp:UpdatePanel>
            <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                            <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                                <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
        </div>
    </div>
</asp:Content>
