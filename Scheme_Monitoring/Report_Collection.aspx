<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="Report_Collection.aspx.cs" Inherits="Report_Collection" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                                        <h4 class="card-title mb-0 flex-grow-1">Analysis Report Scheme Wise</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="clearfix" id="dtOptions" runat="server">
                                                    <div class="pull-right tableTools-container"></div>
                                                </div>
                                                <div class="col-xxl-12 col-md-12">
                                                    <div style="overflow: auto">
                                                        <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" ShowFooter="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="योजना / कार्यक्रम का नाम">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Project_Name") %>' OnClick="lnkScheme_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="lnkSchemeF" runat="server" Text="All Schemes" OnClick="lnkSchemeF_Click" Font-Bold="true" ForeColor="White" BackColor="#666666"></asp:LinkButton>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Total_Count" HeaderText="कुल परियोजनाओं की संख्या" />
                                                                <asp:BoundField DataField="Completed_Count" HeaderText="भौतिक रूप से पूर्ण परियोजनाओं की संख्या" />
                                                                <asp:BoundField DataField="OnGoing_Count" HeaderText="चल रहे परियोजनाओं की संख्या" />

                                                                <asp:BoundField DataField="Total_Sanction" HeaderText="कुल स्वीकृत धनराशी (लाख में)" />
                                                                <asp:BoundField DataField="Completed_Sanction" HeaderText="भौतिक रूप से पूर्ण परियोजनाओं की स्वीकृत धनराशी (लाख में)" />
                                                                <asp:BoundField DataField="OnGoing_Sanction" HeaderText="चल रहे परियोजनाओं की स्वीकृत धनराशी (लाख में)" />

                                                                <asp:BoundField DataField="Total_Release" HeaderText="कुल अवमुक्त धनराशी (लाख में)" />
                                                                <asp:BoundField DataField="Completed_Release" HeaderText="भौतिक रूप से पूर्ण परियोजनाओं की अवमुक्त धनराशी (लाख में)" />
                                                                <asp:BoundField DataField="OnGoing_Release" HeaderText="चल रहे परियोजनाओं की अवमुक्त धनराशी (लाख में)" />

                                                                <asp:BoundField DataField="Total_Remaining_Amount" HeaderText="कुल अवशेष धनराशी (लाख में)" />
                                                                <asp:BoundField DataField="Completed_Remaining_Amount" HeaderText="भौतिक रूप से पूर्ण परियोजनाओं में अवशेष धनराशी (लाख में)" />
                                                                <asp:BoundField DataField="OnGoing_Remaining_Amount" HeaderText="चल रहे परियोजनाओं में अवशेष धनराशी (लाख में)" />

                                                                <asp:BoundField DataField="Total_Expenditure" HeaderText="कुल व्यय (लाख में)" />
                                                                <asp:BoundField DataField="Completed_Expenditure" HeaderText="भौतिक रूप से पूर्ण परियोजनाओं में व्यय (लाख में)" />
                                                                <asp:BoundField DataField="OnGoing_Expenditure" HeaderText="चल रहे परियोजनाओं में व्यय (लाख में)" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#666666" ForeColor="White" Font-Bold="true" />
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

