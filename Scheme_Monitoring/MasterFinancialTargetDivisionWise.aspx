<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterFinancialTargetDivisionWise.aspx.cs" Inherits="MasterFinancialTargetDivisionWise" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdate" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div class="table-header">
                                            <b> <%=Session["Default_Division"].ToString() %> WISE TARGET & ACHIEVEMENT - FINANCIAL </b>
                                        </div>
                                        <!-- div.table-responsive -->
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound" ShowFooter="true">
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
                                                    <asp:TemplateField HeaderText="Financial Target For Financial Year">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialTarget" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_YearTarget") %>' onkeyup="isNumericVal(this);" AutoPostBack="true" OnTextChanged="txtFinancialTarget_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#f15a25" ForeColor="White" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Target For Q1">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialTargetQ1" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q1Target") %>' onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#39beb9" ForeColor="White" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Achievement For Q1">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialAchivmentQ1" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q1Achivment") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#39beb9" ForeColor="White" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Financial Target For Q2">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialTargetQ2" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q2Target") %>' onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#f2ad22" ForeColor="White" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Achievement For Q2">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialAchivmentQ2" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q2Achivment") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#f2ad22" ForeColor="White" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Financial Target For Q3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialTargetQ3" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q3Target") %>' onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#bcd63d" ForeColor="White" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Achievement For Q3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialAchivmentQ3" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q3Achivment") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#bcd63d" ForeColor="White" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Financial Target For Q4">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialTargetQ4" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q4Target") %>' onkeyup="isNumericVal(this);" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#5360cf" ForeColor="White"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Achievement For Q4">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFinancialAchivmentQ4" runat="server" CssClass="form-control" Text='<%# Eval("DivisionFinancialTarget_Q4Achivment") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#5360cf" ForeColor="White"/>
                                                    </asp:TemplateField>
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



