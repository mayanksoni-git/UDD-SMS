<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="NPSPassbookEntry.aspx.cs" Inherits="NPSPassbookEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <style>
                        .control-label {
                            text-transform: uppercase;
                            color: #383535;
                            font-family: sans-serif;
                            transition: 0.6s;
                        }

                            .control-label:hover {
                                color: red;
                                letter-spacing: 0.5px;
                            }
                    </style>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="HRMSEmployee_Id" HeaderText="HRMSEmployee Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Name" DataField="HRMSEmployee_Name" />
                                                    <asp:BoundField HeaderText="Spouse Name" DataField="HRMSEmployee_SpouseName" />
                                                    <asp:BoundField HeaderText="Father Name" DataField="HRMSEmployee_FatherName" />
                                                    <asp:BoundField HeaderText="Gender" DataField="HRMSEmployee_Gender" />
                                                    <asp:BoundField HeaderText="Joining Date" DataField="HRMSEmployee_JoinDateInService" />
                                                    <asp:BoundField HeaderText="Full Address" DataField="HRMSEmployee_FullAddress" />
                                                    <asp:TemplateField HeaderText="Basic Salary">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtEmployeeBasicSalary" runat="server" CssClass="form-control" Text='<%# Eval("HRMSEmployeeSalaryInfo_BasicSalary") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apply">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" CssClass="btn btn-xs btn-pink" Text="Apply"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-header">NPS Passbook Details</div>
                        <br />
                        <div class="row">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" Placeholder="YEAR" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdNPSPassbook" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered"
                                    EmptyDataText="No Records Found" OnPreRender="grdNPSPassbook_PreRender" OnRowDataBound="grdNPSPassbook_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Month_Id" HeaderText="Month_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Month_FinancialOrder" HeaderText="Month_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="S No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Month_MonthName" HeaderText="Month Name" />
                                        <asp:TemplateField HeaderText="Employee Contribution">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmployeeContribution" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("NPSPassbookDetails_EmployeeContribution") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employer Contribution">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmployerContribution" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("NPSPassbookDetails_EmployerContribution") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttotalamount" runat="server" CssClass="form-control" Enabled="false" Text='<%#Eval("NPSPassbookDetails_TotalAmount") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-4"></div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span style="margin-left: 50px">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save Passbook Details" OnClick="btnSave_Click" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_Person_Id" runat="server" Value="0" />
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

    <script type="text/javascript">

        function setTabPageActive(mainMenuId, subMenuId, contentPageId, totalCount) {
            debugger;
            for (var i = 0; i < totalCount; i++) {
                $("#w_" + (i + 1)).removeClass('active');
                $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1)).removeClass('active in');
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
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                debugger;
                if (sessionStorage["_activeMainTabMenu"] == "" || sessionStorage["_activeSubTabMenu"] == undefined || sessionStorage["_activetotalCount"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#w_" + (i + 1)).removeClass('active');
                        $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1)).removeClass('active in');
                    }
                    $("#" + sessionStorage["_activeMainTabMenu"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu"] + "").attr('aria-expanded', 'true');
                }
            });
        });

    </script>
</asp:Content>

