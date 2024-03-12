<%@  Language="C#" MasterPageFile="~/TemplatePopup.master" AutoEventWireup="true" CodeFile="One_Pager_Detail.aspx.cs" Inherits="One_Pager_Detail" %>

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
                                <div class="clearfix">
                                    <div class="pull-right tableTools-container"></div>
                                </div>
                                <div style="font-weight: bold;" class="table-header" runat="server" id="tdProjectWorkName">
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12">
                                <table class="display table table-bordered no-margin-bottom no-border-top" style="font-weight: bold;">
                                    <tbody>
                                        <tr>
                                            <td>Programe:
                                            </td>
                                            <td runat="server" id="tdScheme"></td>
                                            <td>District</td>
                                            <td runat="server" id="tdDistrict"></td>

                                        </tr>

                                        <tr>
                                            <td>Engineer In Charge:</td>
                                            <td runat="server" id="tdContactPerson"></td>
                                            <td>Project Type</td>
                                            <td runat="server" id="tdProjectType"></td>
                                        </tr>

                                        <tr>
                                            <td>Zone
                                            </td>
                                            <td runat="server" id="tdZone"></td>
                                            <td>Circle</td>
                                            <td runat="server" id="tdCircle"></td>
                                        </tr>

                                        <tr>
                                            <td>Division
                                            </td>
                                            <td runat="server" id="tdDivision"></td>
                                            <td>GO No</td>
                                            <td runat="server" id="tdGONo"></td>
                                        </tr>

                                        <tr>
                                            <td>Sanctioned Cost</td>
                                            <td runat="server" id="tdSanctionCost"></td>
                                            <td>GO Date
                                            </td>
                                            <td runat="server" id="tdGODate"></td>
                                        </tr>

                                        <tr>
                                            <td>Work Cost
                                            </td>
                                            <td runat="server" id="tdWorkCost"></td>
                                            <td>Centage</td>
                                            <td runat="server" id="tdCentage"></td>
                                        </tr>
                                        <tr>
                                            <td>Tender Cost (Without GST)
                                            </td>
                                            <td runat="server" id="tdTenderCost"></td>
                                            <td>Tender Cost (With GST)</td>
                                            <td runat="server" id="tdTenderCostGST"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <h4 class="header smaller red"><b>Funding Pattern and GO Released Details (Installment)</b>
                        </h4>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    <asp:GridView ID="grdFundingPattern" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFundingPattern_PreRender" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField DataField="FundingPattern_Id" HeaderText="FundingPattern_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FundingPattern_Name" HeaderText="Funding Pattern Name" />
                                            <asp:BoundField DataField="ProjectWorkFundingPattern_Value" HeaderText="Amount (In Lakhs)" />
                                        </Columns>
                                        <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                    </asp:GridView>
                                </div>

                                <div class="col-xs-6">
                                    <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdCallProductDtls_PreRender">
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
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Project Package Details
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdPackageDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWorkPkg_Work_Id" HeaderText="ProjectWorkPkg_Work_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                <asp:BoundField HeaderText="Contractor" DataField="Vendor_Name" />
                                                <asp:BoundField HeaderText="Contractor (Mobile)" DataField="Vendor_Mobile" />
                                                <asp:BoundField DataField="ProjectWorkPkg_Agreement_No" HeaderText="Agreement No" />
                                                <asp:BoundField HeaderText="Agreement Date" DataField="ProjectWorkPkg_Agreement_Date" />
                                                <asp:BoundField HeaderText="Completion Date As Per Agreement" DataField="ProjectWorkPkg_Due_Date" />
                                                <asp:BoundField HeaderText="Completion Date As Per Extend" DataField="ProjectWorkPkg_ExtendDate" />
                                                <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Physical Component Progress
               
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPhysicalProgress" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdPhysicalProgress_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="PhysicalProgressComponent_Id" HeaderText="PhysicalProgressComponent_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectPkg_PhysicalProgress_Id" HeaderText="ProjectPkg_PhysicalProgress_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PhysicalProgressComponent_Component" HeaderText="Component" />
                                                    <asp:BoundField DataField="Unit_Name" HeaderText="Unit" />
                                                    <asp:BoundField DataField="ProjectPkg_PhysicalProgress_MasterValue" HeaderText="Proposed (Number)" />
                                                    <asp:BoundField DataField="ProjectUC_PhysicalProgress_PhysicalProgress" HeaderText="Completed (Number)" />
                                                    <asp:BoundField DataField="ProjectUC_PhysicalProgress_PhysicalFunctional" HeaderText="Functional (Number)" />
                                                    <asp:BoundField DataField="ProjectUC_PhysicalProgress_PhysicalNonFunctional" HeaderText="Non-Functional (Number)" />
                                                    <asp:BoundField DataField="ProjectUC_PhysicalProgress_Remarks" HeaderText="Remarks" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <table class="display table table-bordered no-margin-bottom no-border-top" style="font-weight: bold;">
                                    <tbody>
                                        <tr>
                                            <td>Over All Physical Progress (%):
                                            </td>
                                            <td runat="server" id="tdPhysicalProgress"></td>
                                            <td>Financial Progress (%):</td>
                                            <td runat="server" id="tdFinancialProgress"></td>
                                        </tr>
                                        <tr>
                                            <td>Physical Handover Done:
                                            </td>
                                            <td runat="server" id="tdPhysicalHandover"></td>
                                            <td>Financial Closure Done:</td>
                                            <td runat="server" id="tdFinancialClosure"></td>
                                        </tr>
                                        <tr>
                                            <td>Total Expenditure (According To PMIS):
                                            </td>
                                            <td runat="server" id="tdTotalExpenditure"></td>
                                            <td>Total Expenditure (According To AMRUT Directorate)</td>
                                            <td runat="server" id="tdTotalExpenditureAMRUT"></td>
                                        </tr>

                                        <tr>
                                            <td>Total SNA Limit Assigned
                                            </td>
                                            <td runat="server" id="tdSNALimitAssigned"></td>
                                            <td>Total SNA Limit Utilized</td>
                                            <td runat="server" id="tdSNALimitUtilized"></td>
                                        </tr>

                                        <tr>
                                            <td>Total Available Limit (As Per PMIS)
                                            </td>
                                            <td runat="server" id="tdSNALimitAvailablePMIS"></td>
                                            <td>Total Available Limit (As Per PNB)</td>
                                            <td runat="server" id="tdSNALimitAvailablePNB"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Issue Details (If Any)
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdIssueDetails" runat="server" CssClass="display table table-bordered"
                                            AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdIssueDetails_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_WorkId" HeaderText="ProjectWorkIssueDetails_WorkId">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Id" HeaderText="ProjectWorkIssueDetails_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Date" HeaderText="Date Reported On" />
                                                <asp:BoundField DataField="ProjectIssue_Name" HeaderText="Issue" />
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Comments" HeaderText="Comments" />
                                                <asp:BoundField DataField="ProjectWorkIssueDetails_DateResolved" HeaderText="Resolved On" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
    </div>
</asp:Content>

