<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkSNALimitULB.aspx.cs" Inherits="MasterProjectWorkSNALimitULB" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Project List To Assign Monthly Limit
                                            <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </h3>

                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="table-header">
                                                    SNA Account Status                               
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-xs-6 col-sm-3 pricing-box">
                                                <div class="widget-box widget-color-dark">
                                                    <div class="widget-header">
                                                        <h5 class="widget-title bigger lighter">Total Balance (In Lakhs)</h5>
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
                                                                                <asp:LinkButton ID="lnkTotalBalance" runat="server" Font-Bold="true" >0</asp:LinkButton></span>
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
                                                        <h5 class="widget-title bigger lighter"><b>Assigned Limit (In Lakhs)</b></h5>
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
                                                                                <asp:LinkButton ID="lnkAssigned" runat="server" Font-Bold="true">0</asp:LinkButton></span>
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
                                                        <h5 class="widget-title bigger lighter">Limit Utilized (In Lakhs)</h5>
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
                                                        <h5 class="widget-title bigger lighter">Available Balance (In Lakhs)</h5>
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
                                                                                <asp:LinkButton ID="lnkAvailable" runat="server">0</asp:LinkButton></span>
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
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">District* </label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-warning" OnClick="btnSearch_Click" Text="Search" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="grdAssignedLimitSummery" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdAssignedLimitSummery_PreRender" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkType" runat="server" Text='<%# Eval("Text") %>' OnClick="lnkType_Click" Font-Bold="true"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lnkTypeF" runat="server" Text="Total Projects" OnClick="lnkTypeF_Click" ForeColor="White"></asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Total_ULB" HeaderText="Total ULB" />
                                                        <asp:BoundField DataField="Total_Projects" HeaderText="Total Projects" />
                                                        <asp:BoundField DataField="AssignedLimit" HeaderText="Assigned Limit (In Lakhs)" />
                                                        <asp:BoundField DataField="UsedLimit" HeaderText="Used Limit (In Lakhs)" />
                                                        <asp:BoundField DataField="AvailableLimit" HeaderText="Available Limit (In Lakhs)" />
                                                    </Columns>
                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="row" runat="server" id="divData" visible="false">
                                            <!-- div.table-responsive -->
                                            <div id="dtOptions" runat="server" class="clearfix">
                                                <div class="pull-right tableTools-container">
                                                </div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWork_ULB_Id" HeaderText="ProjectWork_ULB_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <br />
                                                                <b><a href='Report_AccountStatement.aspx?ProjectWork_Id=<%# Eval("ProjectWork_ULB_Id") %>' target="_blank"><span style="color: red">Statement</span></a></b>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Jurisdiction">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Jurisdiction</th>
                                                                            <th>Name</th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <tr>
                                                                            <td>District</td>
                                                                            <td><%# Eval("Jurisdiction_Name_Eng") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>ULB</td>
                                                                            <td><%# Eval("ULB_Name") %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Funding Pattern">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Funding Pattern</th>
                                                                            <th>In Lakhs</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Central Share</td>
                                                                            <td><%# Eval("Central_Share") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>State Share</td>
                                                                            <td><%# Eval("State_Share") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>ULB Share</td>
                                                                            <td><%# Eval("ULB_Share") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Total Share</td>
                                                                            <td><%# Eval("Total_Share") %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Cost</th>
                                                                            <th>In Lakhs</th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Work Cost</td>
                                                                            <td><%# Eval("Work_Cost") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Centage</td>
                                                                            <td><%# Eval("Centage") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Total Cost</td>
                                                                            <td><%# Eval("Total_Cost") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Tender Cost</td>
                                                                            <td><%# Eval("Tender_Cost") %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total GO Issued">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkGOCount" runat="server" Font-Bold="true" Text='<%# Eval("Total_GO") %>' OnClick="lnkGOCount_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Share Received via GO">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Share Received via GO</th>
                                                                            <th>In Lakhs</th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Central Share</td>
                                                                            <td><%# Eval("GO_CentralShare") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>State Share</td>
                                                                            <td><%# Eval("GO_StateShare") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>ULB Share</td>
                                                                            <td><%# Eval("GO_ULBShare") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Centage</td>
                                                                            <td><%# Eval("ProjectWorkGO_Centage") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Total Share</td>
                                                                            <td><%# Eval("Total_GO_Share") %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Financial Progress">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Invoice</th>
                                                                            <th>In Lakhs</th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Previous Entry</td>
                                                                            <td><%# Eval("PrevInvoiceAmount") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>On Portal</td>
                                                                            <td><%# Eval("Total_Invoice_Value") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Defered</td>
                                                                            <td><%# Eval("Deffered_Value") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Total</td>
                                                                            <td><%# Eval("Total_Amount") %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField HeaderText="Physical Progress (%)" DataField="Physical_Progress" />
                                                        <asp:BoundField HeaderText="Financial Progress (%)" DataField="Financial_Progress" />
                                                        <asp:TemplateField HeaderText="Limit Assigned">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Limit</th>
                                                                            <th>In Lakhs</th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <tr>
                                                                            <td>Total Assigned</td>
                                                                            <td><%# Eval("SNAAccountLimit_AssignedLimit") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Total Utilized</td>
                                                                            <td><%# Eval("SNAAccountLimitUsed_UsedLimit") %></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Total Available</td>
                                                                            <td><%# Eval("SNAAccountAvailableLimit") %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add Limit (In Lakhs)">
                                                            <ItemTemplate>
                                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLimit" runat="server" CssClass="form-control" Width="120px"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:FileUpload ID="flUploadDoc" runat="server" /></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnUpdateLimit" Text="Update Limit" OnClick="btnUpdateLimit_Click" runat="server" CssClass="btn btn-warning" Visible="false"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Fund Release Installment Details  For Central & State Share                                  
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdCallProductDtls_PreRender" OnRowDataBound="grdCallProductDtls_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkGO_Id" HeaderText="ProjectWorkGO_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkGO_Document_Path" HeaderText="ProjectWorkGO_Document_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectWorkGO_GO_Date" HeaderText="GO Date" />
                                                        <asp:BoundField DataField="ProjectWorkGO_GO_Number" HeaderText="GO Number" />
                                                        <asp:BoundField DataField="ProjectWorkGO_CentralShare" HeaderText="Central Share (In Lakhs)" />
                                                        <asp:BoundField DataField="ProjectWorkGO_StateShare" HeaderText="State Share (In Lakhs)" />
                                                        <asp:BoundField DataField="ProjectWorkGO_Centage" HeaderText="Centage (In Lakhs)" />
                                                        <asp:TemplateField HeaderText="Download GO Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSCGO" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkGO_Document_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
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
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Fund Release Details For ULB Share                                  
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdULBShare" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdULBShare_PreRender" OnRowDataBound="grdULBShare_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkGO_Id" HeaderText="ProjectWorkGO_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkGO_Document_Path" HeaderText="ProjectWorkGO_Document_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectWorkGO_GO_Date" HeaderText="Date" />
                                                        <asp:BoundField DataField="ProjectWorkGO_GO_Number" HeaderText="Refrence Number" />
                                                        <asp:BoundField DataField="ProjectWorkGO_IssuingAuthority" HeaderText="Fund Source" />
                                                        <asp:BoundField DataField="ULB_Name" HeaderText="ULB Name" />
                                                        <%--Change to name--%>
                                                        <asp:BoundField DataField="ProjectWorkGO_ULBShare" HeaderText="ULB Share Released (In Lakhs)" />
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkULBGO" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkGO_Document_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
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
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpdateLimit" />
                </Triggers>
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
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPost')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: true,
                                    fixedHeader: {
                                        header: false,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null
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

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });

        function openStatement(obj) {
            window.open(location.origin + "/Report_AccountStatementSNA.aspx", "_blank", "", false);
            return false;
        }

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
    </script>
</asp:Content>
