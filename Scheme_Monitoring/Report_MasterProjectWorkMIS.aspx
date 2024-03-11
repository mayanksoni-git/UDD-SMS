<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_MasterProjectWorkMIS.aspx.cs" Inherits="Report_MasterProjectWorkMIS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <style type="text/css">
            body {
                font-family: Arial;
                font-size: 10pt;
            }

            .Repeater, .Repeater td, .Repeater td {
                border: 1px solid #ccc;
            }

                .Repeater td {
                    background-color: #eee !important;
                }

                .Repeater th {
                    background-color: #6C6C6C !important;
                    color: White;
                    font-size: 10pt;
                    line-height: 200%;
                }

                .Repeater span {
                    color: black;
                    font-size: 10pt;
                    line-height: 200%;
                }

            .page_enabled, .page_disabled {
                display: inline-block;
                height: 30px;
                min-width: 30px;
                line-height: 30px;
                text-align: center;
                text-decoration: none;
                border: 1px solid #ccc;
                width: 130px;
                margin: 1px 2px 2px 2px;
            }

            .page_enabled {
                background-color: #eee;
                color: #000;
                font-weight: bold;
            }

            .page_disabled {
                background-color: #6C6C6C;
                color: #fff !important;
                border: 2px solid #ff0000;
            }
        </style>
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mpDeduction" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup1"
                        CancelControlID="btnclose1" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup1" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <cc1:ModalPopupExtender ID="mpMisStepHistory" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mpLog" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Project Details
                                    <asp:ImageButton ID="btnMisHistory" Width="40px" Height="35px" OnClick="btnMisHistory_Click" Style="float: right" ImageUrl="~/assets/images/More_Than.png" CssClass="align-center" runat="server" />
                                    <h4 style="float: right">View MIS Update History   .</h4>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div style="overflow: auto">
                                    <asp:GridView ID="grdProMIS" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProMIS_PreRender" OnRowDataBound="grdProMIS_RowDataBound">
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
                                            <asp:BoundField HeaderText="Project Start Date" DataField="ProjectWork_StartDate" />
                                            <asp:BoundField HeaderText="Project Completion Date" DataField="ProjectWork_EndDate" />
                                            <asp:TemplateField HeaderText="Download_GO">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkGODoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWork_GO_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProjectWork_GO_Path" HeaderText="ProjectWork_GO_Path">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Funding Pattern Breakup
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-6">
                                <asp:GridView ID="grdFundingPattern" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFundingPattern_PreRender" ShowFooter="true" OnRowDataBound="grdFundingPattern_RowDataBound">
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
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" Text="Centage (In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Contegency (In Lakhs)</label>
                                                <asp:TextBox ID="txtContegencytext" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" Text="Total Cost (Including Centage)" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtWorkCost_Centage" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
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
                                        <asp:GridView ID="grdPackageDetails" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender" ShowFooter="true">
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
                                                <asp:BoundField HeaderText="Vendor / Contractor" DataField="Vendor_Name" />
                                                <asp:BoundField HeaderText="Vendor / Contractor (Mobile)" DataField="Vendor_Mobile">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Reporting Staff (JE / APE)" DataField="List_ReportingStaff_JEAPE_Name" />
                                                <asp:BoundField HeaderText="Reporting Staff (AE / PE)" DataField="List_ReportingStaff_AEPE_Name" />
                                                <asp:BoundField DataField="ProjectWorkPkg_Agreement_No" HeaderText="Agreement No" />
                                                <asp:BoundField HeaderText="Agreement Date " DataField="ProjectWorkPkg_Agreement_Date" />
                                                <asp:BoundField HeaderText="Due Date Of Completion " DataField="ProjectWorkPkg_Due_Date" />
                                                <asp:BoundField HeaderText="Agreement Amount (Tender Cost)" DataField="ProjectWorkPkg_AgreementAmount" />
                                                <asp:BoundField HeaderText="GST Type" DataField="ProjectWorkPkg_GST" />
                                                <asp:BoundField HeaderText="GST %" DataField="ProjectWorkPkg_Percent" />
                                                <asp:BoundField HeaderText="Due Date Of Completion Extend" DataField="ProjectWorkPkg_ExtendDate" />
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

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
                                            <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdCallProductDtls_PreRender" OnRowDataBound="grdCallProductDtls_RowDataBound">
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
                                            <asp:GridView ID="grdULBShare" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdULBShare_PreRender" OnRowDataBound="grdULBShare_RowDataBound">
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
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Invoice Details
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdProjectInvoiceDetails" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProjectInvoiceDetails_PreRender" OnRowDataBound="grdProjectInvoiceDetails_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                <asp:BoundField HeaderText="Project Name" DataField="ProjectWork_Name" />
                                                <asp:TemplateField HeaderText="View Previous Invoice">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblPrevInvoiceTotal" Text="0.00" runat="server" OnClick="lblPrevInvoiceTotal_Click" Font-Bold="true"></asp:LinkButton></td>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Financial Achivment Till May 2021 (Bills Raised)" DataField="Inv_Before">
                                                    <HeaderStyle BackColor="#FFB752" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Financial Achivment Till May 2021 (Bills Approved)" DataField="Inv_Before_A">
                                                    <HeaderStyle BackColor="#FFB752" />
                                                </asp:BoundField>

                                                <asp:BoundField HeaderText="Financial Achivment June 2021 (Bills Raised)" DataField="Inv_Current_Less">
                                                    <HeaderStyle BackColor="#6FB3E0" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Financial Achivment June 2021 (Bills Approved)" DataField="Inv_Current_Less_A">
                                                    <HeaderStyle BackColor="#6FB3E0" />
                                                </asp:BoundField>

                                                <asp:BoundField HeaderText="Financial Achivment July 2021 (Bills Raised)" DataField="Inv_Current">
                                                    <HeaderStyle BackColor="#87B87F" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Financial Achivment July 2021 (Bills Approved)" DataField="Inv_Current_A">
                                                    <HeaderStyle BackColor="#87B87F" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Physical Achivment Till Date" DataField="ProjectWorkPhysicalTarget_Target" />
                                                <asp:BoundField HeaderText="Financial Target Till Date (In Lakhs)" DataField="ProjectWorkFinancialTarget_TargetA" />
                                                <asp:BoundField HeaderText="Financial Achivment Till Date (%)" DataField="ProjectWorkFinancialTarget_Target" />
                                                <asp:TemplateField HeaderText="Target Month">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Enabled="false">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtTargetMonth" runat="server" CssClass="form-control datepicker" autocomplete="off" Width="80px" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Invoice Details">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Text="View Package Wise Details" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Change Log">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkViewLog" runat="server" Width="40px" Height="50px" ImageUrl="~/assets/images/log.png" OnClick="lnkViewLog_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divInvoiceDetails" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Package Wise Invoice Details
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover table-responsive" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true" ShowHeader="false" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="row alert-info">
                                                                    <div class="col-md-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead class="thin-border-bottom">
                                                                                <tr>
                                                                                    <th></th>
                                                                                    <th>Package Code
                                                                                    </th>
                                                                                    <th>Package Name
                                                                                    </th>
                                                                                    <th>Vendor / Contractor
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHInvTill" Text='Invoice Till May 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHInvPrev" Text='Invoice June 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHInvCurr" Text='Invoice July 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHADPTill" Text='Other Dept Till May 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHADPPrev" Text='Other Dept June 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHADPCurr" Text='Other Dept July 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHMATill" Text='MA Till May 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHMAPrev" Text='MA June 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHMACurr" Text='MA July 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHDRTill" Text='DR Till May 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHDRPrev" Text='DR June 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblHDRCurr" Text='DR July 2021' runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>

                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="">
                                                                                        <asp:ImageButton ID="imgShow" runat="server" OnClick="Show_Hide_ChildGrid" ImageUrl="assets/images/plus.png"
                                                                                            CommandArgument="Show" /></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblPackageCode" Text='<%#Eval("ProjectWorkPkg_Code") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblPackageName" Text='<%#Eval("ProjectWorkPkg_Name") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>

                                                                                    <td class="">
                                                                                        <asp:Label ID="lblVendor" Text='<%#Eval("Vendor_Name") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>

                                                                                    <td class="">
                                                                                        <asp:Label ID="lblInvTill" Text='<%#Eval("Inv_Before") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblInvPrev" Text='<%#Eval("Inv_Current_Less") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblInvCurr" Text='<%#Eval("Inv_Current") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblADPTill" Text='<%#Eval("ADP_Before") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblADPPrev" Text='<%#Eval("ADP_Current_Less") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblADPCurr" Text='<%#Eval("ADP_Current") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblMATill" Text='<%#Eval("MA_Before") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblMAPrev" Text='<%#Eval("MA_Current_Less") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblMACurr" Text='<%#Eval("MA_Current") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblDRTill" Text='<%#Eval("DR_Before") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblDRPrev" Text='<%#Eval("DR_Current_Less") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                    <td class="">
                                                                                        <asp:Label ID="lblDRCurr" Text='<%#Eval("DR_Current") %>' runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div class="row alert-warning" runat="server" visible="false" id="pnlOrdersDiv">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group">
                                                                                <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                                                                    <div class="row">
                                                                                        <div class="col-xs-12">
                                                                                            <div class="table-header">
                                                                                                Package Invoice Details
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div style="overflow: auto">
                                                                                                <asp:GridView ID="grdPostBeat" runat="server" CssClass="table table-striped table-bordered table-hover table-responsive" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPostBeat_PreRender">
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
                                                                                                        <asp:BoundField DataField="PackageInvoice_PackageEMBMaster_Id" HeaderText="PackageInvoice_PackageEMBMaster_Id">
                                                                                                            <HeaderStyle CssClass="displayStyle" />
                                                                                                            <ItemStyle CssClass="displayStyle" />
                                                                                                            <FooterStyle CssClass="displayStyle" />
                                                                                                        </asp:BoundField>
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
                                                                                                        <asp:BoundField DataField="List_EMBNo" HeaderText="EMB No" />
                                                                                                        <asp:BoundField DataField="PackageInvoice_Date" HeaderText="Invoice Date" />
                                                                                                        <asp:BoundField DataField="PackageInvoice_VoucherNo" HeaderText="Voucher No" />
                                                                                                        <asp:BoundField DataField="Total_Line_Items" HeaderText="Total Line Items" />
                                                                                                        <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                                                                        <asp:BoundField DataField="Total_Amount_D" HeaderText="Deduction" />
                                                                                                        <asp:BoundField DataField="Total_Amount_F" HeaderText="Total Amount" />
                                                                                                        <asp:BoundField DataField="FinancialTrans_TransAmount" HeaderText="Total Fund Transfred" />
                                                                                                        <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                                                                        <asp:BoundField DataField="OfficeBranch_Name" HeaderText="Pending at Organization" />
                                                                                                        <asp:BoundField DataField="Invoice_Status" HeaderText="Current Status" />
                                                                                                        <asp:BoundField DataField="InvoiceAdditionalStatus" HeaderText="Reason (If Any)" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-xs-12">
                                                                                            <div class="table-header">
                                                                                                Package Other Departmental Invoice Details
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
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
                                                                                                        <asp:BoundField DataField="Package_ADP_ADPTotalAmount" HeaderText="Other Departmental Amount" />
                                                                                                        <asp:BoundField DataField="Designation_Current" HeaderText="Forwarded From Designation" />
                                                                                                        <asp:BoundField DataField="Designation_DesignationName" HeaderText="Pending at Designation" />
                                                                                                        <asp:BoundField DataField="PackageADP_Status" HeaderText="Current Status" />
                                                                                                        <asp:BoundField DataField="InvoiceAdditionalStatus_Text" HeaderText="Reason (If Any)" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-xs-12">
                                                                                            <div class="table-header">
                                                                                                Package Moblization Advance Details
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
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
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-xs-12">
                                                                                            <div class="table-header">
                                                                                                Package Deduction Release Details
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
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
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divPreInvoiceDetails" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Previous Invoice Detailss
                               
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdInvoice" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:BoundField DataField="PackageInvoice_Id" HeaderText="PackageInvoice_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Deducton">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnViewDeduction" Width="40px" Height="20px" OnClick="btnViewDeduction_Click" ImageUrl="~/assets/images/deduction.png" CssClass="align-center" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PackageInvoice_Date" HeaderText="Invoice Date" />
                                                    <asp:BoundField DataField="PackageInvoice_VoucherNo" HeaderText="Voucher No" />
                                                    <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="GST" HeaderText="GST Amount" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Total Amount" />
                                                    <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Previous Invoice Details (Other Departmental Invoices)
   
       
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdInvoiceADP" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="Package_ADP_Id" HeaderText="Package_ADP_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Package_ADP_Date" HeaderText="Invoice Date" />
                                                    <asp:BoundField DataField="Package_ADP_RefNo" HeaderText="Bill / Refrence No" />
                                                    <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="GST" HeaderText="GST Amount" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Total Amount" />
                                                    <asp:BoundField DataField="Package_ADP_AddedOn" HeaderText="Added On" />
                                                </Columns>
                                                <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdDeductionHistory" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Deduction_Name" HeaderText="Deduction" />
                                                    <asp:BoundField DataField="PackageInvoiceAdditional_Deduction_Value_Final" HeaderText="Deduction Value" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Physical Progress Component
                                       
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPhysicalProgress" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdPhysicalProgress_RowDataBound">
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
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPostPhysicalProgress" runat="server" Enabled="false" />
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
                                <div class="table-header">
                                    Package Wise Document Upload
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdPackageDetailsDoc" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetailsDoc_PreRender" OnRowDataBound="grdPackageDetailsDoc_RowDataBound">
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
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnPackageDocEdit" Width="20px" Height="20px" OnClick="btnPackageDocEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Package Code" DataField="ProjectWorkPkg_Code" />
                                                <asp:BoundField HeaderText="Package Name" DataField="ProjectWorkPkg_Name" />
                                                <asp:BoundField HeaderText="Agreement Amount" DataField="ProjectWorkPkg_AgreementAmount" />
                                                <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                <asp:BoundField HeaderText="Agreement Date" DataField="ProjectWorkPkg_Agreement_Date" />
                                                <asp:BoundField HeaderText="Due Date Of Completion" DataField="ProjectWorkPkg_Due_Date" />
                                                <asp:BoundField HeaderText="Vendor / Contractor" DataField="Vendor_Name" />
                                                <asp:BoundField HeaderText="Vendor / Contractor (Mobile)" DataField="Vendor_Mobile" />
                                                <asp:BoundField HeaderText="Reporting Staff (JE / APE)" DataField="List_ReportingStaff_JEAPE_Name" />
                                                <asp:BoundField HeaderText="Reporting Staff (AE / PE)" DataField="List_ReportingStaff_AEPE_Name" />
                                                <asp:BoundField HeaderText="Total CB Document Uploaded" DataField="Agreement" />
                                                <asp:BoundField HeaderText="Total BG Document Uploaded" DataField="BankGurantee" />
                                                <asp:BoundField HeaderText="Total MA Document Uploaded" DataField="Mobelization" />
                                                <asp:BoundField HeaderText="Total PS Document Uploaded" DataField="PerformanceSecurity" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divUpload" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Upload Contract Bond
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdContractBond" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdContractBond_PreRender" OnRowDataBound="grdContractBond_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Id" HeaderText="ProjectWorkPkgDoc_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TradeDocument_Id" HeaderText="TradeDocument_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Path" HeaderText="ProjectWorkPkgDoc_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TradeDocument_Name" HeaderText="Document Type" />
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_DocumentDate" HeaderText="Document Date" />
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_DocumentNo" HeaderText="Refrence Number" />

                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadCB" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkPkgDoc_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Comments" HeaderText="Comments" />
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
                                        Upload Bank Gurantee / Performance Security / Moblization Advance and Other Documents
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdBG" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdBG_PreRender" OnRowDataBound="grdBG_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Id" HeaderText="ProjectWorkPkgDoc_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TradeDocument_Id" HeaderText="TradeDocument_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Path" HeaderText="ProjectWorkPkgDoc_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TradeDocument_Name" HeaderText="Document Type" />
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_DocumentDate" HeaderText="Effective From Date" />
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_DocumentDate2" HeaderText="Effective Till Date" />
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_DocumentNo" HeaderText="BG Number" />
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Amount" HeaderText="Amount" />
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadBG" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkPkgDoc_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Comments" HeaderText="Comments" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    UC Details                               
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdULBShareUC" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdULBShareUC_PreRender" OnRowDataBound="grdULBShareUC_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectUC_Id" HeaderText="ProjectUC_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectUC_Document" HeaderText="ProjectUC_Document">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="UC Date" DataField="ProjectUC_SubmitionDate" />
                                                    <asp:BoundField HeaderText="UC Number" DataField="ProjectUC_Comments" />
                                                    <asp:BoundField HeaderText="UC Filled %" DataField="ProjectUC_Achivment" />
                                                    <asp:TemplateField HeaderText="Upload UC Document">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUCDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectUC_Document") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
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
                                    Specific Issues (If Any)
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdIssue" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdIssue_PreRender" OnRowDataBound="grdIssue_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Id" HeaderText="ProjectWorkIssueDetails_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Dependency_Id" HeaderText="ProjectWorkIssueDetails_Dependency_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Path" HeaderText="ProjectWorkIssueDetails_Path">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issuing Type">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="form-control" Enabled="false">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dependency">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlDependency" runat="server" CssClass="form-control" Enabled="false">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Category" HeaderText="Discription" />
                                                    <asp:TemplateField HeaderText="Issuing Effective Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIssuingDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkIssueDetails_Date") %>' Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectWorkIssueDetails_Comments" HeaderText="Comments" />
                                                    <asp:TemplateField HeaderText="Download Document">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkIssueDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkIssueDetails_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Conversation Log">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkConLog" runat="server" Text="View Conversation" OnClick="lnkConLog_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divLog" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Conversation Log Against Specific Issue
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdLog" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdLog_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLogDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Letter Written By Authority / Designation">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Letter Written To Authority / Designation">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLogComments" runat="server" TextMode="MultiLine" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkIssueLogDoc" runat="server" Text="Download" OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; height: 500px; margin-left: -32px" ScrollBars="Auto">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdMisStepHistory" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:BoundField DataField="MIS_ProjectWork_StepCountHistory_Id" HeaderText="MIS_ProjectWork_StepCountHistory_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MIS_ProjectWork_StepCountHistory_AddedBy" HeaderText="MIS_ProjectWork_StepCountHistory_AddedBy">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Person_Name" HeaderText="Person Name" />
                                                    <asp:BoundField DataField="MIS_ProjectWork_StepCountHistory_AddedOn" HeaderText="Saved Date" />
                                                    <asp:BoundField DataField="MIS_ProjectWork_StepCountHistory_StepCount" HeaderText="Saved Steps" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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

                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdChangeLog" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectWorkFinancialTarget_Id" HeaderText="ProjectWorkFinancialTarget_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Target_Month_Year" HeaderText="Target Month / Year" />
                                                    <asp:BoundField DataField="ProjectWorkPhysicalTarget_Target" HeaderText="Physical Target" />
                                                    <asp:BoundField DataField="Person_Name" HeaderText="Added By" />
                                                    <asp:BoundField DataField="ProjectWorkFinancialTarget_AddedOn" HeaderText="Added Date" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="space"></div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdBOQ" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBOQ_PreRender" OnRowDataBound="grdBOQ_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="PackageBOQ_Id" HeaderText="PackageBOQ_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageBOQVariation_Id" HeaderText="PackageBOQVariation_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description Of Goods">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecification" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageBOQ_Specification") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Unit_Name" HeaderText="Unit"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Variation">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlVariation" runat="server" Enabled="false">
                                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity As Per CB">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyM" runat="server" Text='<%# Eval("PackageBOQ_Qty") %>' Width="90px "></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity As Per Actual Measurment">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyV" runat="server" Text='<%# Eval("BOQ_Qty") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estimated Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRE" runat="server" Text='<%# Eval("PackageBOQ_RateEstimated") %>' Width="90px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quoted Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRQ" runat="server" Text='<%# Eval("PackageBOQ_RateQuoted") %>' Width="90px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Up-To-Date Details">
                                                    <ItemTemplate>
                                                        <table class="table table-striped table-bordered table-hover">
                                                            <tbody>
                                                                <tr style="font-weight: bold">
                                                                    <td class="alert alert-info">
                                                                        <label class="control-label no-padding"></label>
                                                                    </td>
                                                                    <td class="alert alert-info">
                                                                        <label class="control-label no-padding">Quantity Paid</label>
                                                                    </td>
                                                                    <td class="alert alert-info">
                                                                        <label class="control-label no-padding">Percentage Value Paid</label>
                                                                    </td>
                                                                    <td class="alert alert-info">
                                                                        <label class="control-label no-padding">Amount Paid</label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="alert alert-info">
                                                                        <label class="control-label no-padding">BOQ Details</label>
                                                                    </td>
                                                                    <td class="">
                                                                        <asp:TextBox ID="txtBOQQ" runat="server" Text='<%# Eval("PackageBOQ_QtyPaidN") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td class="">
                                                                        <asp:TextBox ID="txtBOQP" runat="server" Text='<%# Eval("PackageBOQ_PercentageValuePaidTillDateN") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td class="">
                                                                        <asp:TextBox ID="txtBOQA" runat="server" Text='<%# Eval("PackageBOQ_AmountPaidN") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="alert alert-info">
                                                                        <label class="control-label no-padding">EMB Details</label>
                                                                    </td>
                                                                    <td class="">
                                                                        <asp:TextBox ID="txtUTDQ" runat="server" Text='<%# Eval("PackageBOQ_QtyPaidN") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td class="">
                                                                        <asp:TextBox ID="txtUTDP" runat="server" Text='<%# Eval("PackageBOQ_PercentageValuePaidTillDateN") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td class="">
                                                                        <asp:TextBox ID="txtUTDA" runat="server" Text='<%# Eval("PackageBOQ_AmountPaidN") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Comments" DataField="PackageBOQVariation_Comments" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdVariationDocuments" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdVariationDocuments_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="Package_ExtraItem_Id" HeaderText="Package_ExtraItem_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWorkPkg_Id" HeaderText="ProjectWorkPkg_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Package_ExtraItem_ExtraItemFilePath" HeaderText="Package_ExtraItem_ExtraItemFilePath">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Package_ExtraItem_ApprovalFilePath" HeaderText="Package_ExtraItem_ApprovalFilePath">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ProjectWorkPkg_Code" HeaderText="Package Code" />
                                                    <asp:BoundField DataField="Package_ExtraItem_AddedOn" HeaderText="Date" />
                                                    <asp:TemplateField HeaderText="Download Approval File">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkApprovalDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("Package_ExtraItem_ApprovalFilePath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Download BOQ Excel File">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBOQDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("Package_ExtraItem_ExtraItemFilePath") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hf_CS" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_SS" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_US" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_BJT" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Project_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_ProjectWorkPkg_Id" runat="server" Value="0" />
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
    <script>
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

