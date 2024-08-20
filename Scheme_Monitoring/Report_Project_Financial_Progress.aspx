<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="Report_Project_Financial_Progress.aspx.cs" Inherits="Report_Project_Financial_Progress" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project Wise Financial Progress</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <%--<label class="control-label no-padding-right">Scheme </label>--%>
                                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchScheme" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6" id="divZone" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6" id="divCircle" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="control-label no-padding-right">Search By</label>
                                                        <asp:RadioButtonList ID="rbtSearchBy" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtSearchBy_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Value="1">Till Date</asp:ListItem>
                                                            <asp:ListItem Value="2">Date Range</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6" id="divFromDate" runat="server" visible="false">
                                                    <div>
                                                        <label class="control-label no-padding-right">Date From</label>
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" data-provider="flatpickr" autocomplete="off" data-date-format="d/m/Y" style="display:none;"></asp:TextBox>
                                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" data-provider="flatpickr" autocomplete="off" data-date-format="d/m/Y"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6" id="divTillDate" runat="server" visible="false">
                                                    <div>
                                                        <label class="control-label no-padding-right">Date Till</label>
                                                        <asp:TextBox ID="txtDateTill" runat="server" CssClass="form-control" data-provider="flatpickr" autocomplete="off" data-date-format="d/m/Y"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>
                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <!-- div.table-responsive -->
                                                        <div class="clearfix" id="dtOptions" runat="server">
                                                            <div class="pull-right tableTools-container"></div>
                                                        </div>
                                                        <!-- div.dataTables_borderWrap -->
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" ShowFooter="True" OnRowDataBound="grdPost_RowDataBound">
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
                                                                    <asp:BoundField HeaderText="Project" DataField="ProjectWork_Name" />
                                                                    <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />

                                                                    <asp:BoundField HeaderText="Moblization Advance" DataField="MA_Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Previous Invoices (Data Entry)" DataField="PrevInvoice_Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Previous Other Dept Payments (Data Entry)" DataField="PrevADP_Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Total EMB (Pending For Invoicing)" DataField="EMB_Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Total Invoices" DataField="Invoice_Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Total Other Dept Payments" DataField="ADP_Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Total Achivment" DataField="Total_Achivment">
                                                                        <HeaderStyle BackColor="Cornsilk" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
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
        <!-- /.main-content -->
    </div>

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





