<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="MasterProjectWorkMIS_5.aspx.cs" Inherits="MasterProjectWorkMIS_5" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="page-content">
                        <div>
                            <ul class="steps" style="margin-left: 0">
                                <li data-step="1" class="active">
                                    <span class="step">1</span>
                                    <span class="title">Basic Details</span>
                                </li>
                                <li data-step="2" class="active">
                                    <span class="step">2</span>
                                    <span class="title">GO Release Details</span>
                                </li>
                                <li data-step="3" class="active">
                                    <span class="step">3</span>
                                    <span class="title">Target & Achivments</span>
                                </li>
                                <li data-step="4" class="active">
                                    <span class="step">4</span>
                                    <span class="title">Physical Components</span>
                                </li>
                                <li data-step="5" class="active">
                                    <span class="step">5</span>
                                    <span class="title">Document Vault</span>
                                </li>
                                <li data-step="6">
                                    <span class="step">6</span>
                                    <span class="title">UC Details and Issues</span>
                                </li>
                                <li data-step="7">
                                    <span class="step">7</span>
                                    <span class="title">Variation Details</span>
                                </li>
                            </ul>
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
                                        <asp:GridView ID="grdPackageDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender" OnRowDataBound="grdPackageDetails_RowDataBound">
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
                                                        <asp:ImageButton ID="btnPackageEdit" Width="20px" Height="20px" OnClick="btnPackageEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
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
                                                <asp:BoundField HeaderText="Contract Bond" DataField="Agreement" />
                                                <asp:BoundField HeaderText="Bank Gurantee" DataField="BankGurantee" />
                                                <asp:BoundField HeaderText="Mobelization Advance" DataField="Mobelization" />
                                                <asp:BoundField HeaderText="Performance Security" DataField="PerformanceSecurity" />
                                                <asp:BoundField HeaderText="Liquidated Damages" DataField="LD" />
                                                <asp:BoundField HeaderText="Financial Closure" DataField="FC" />
                                                <asp:BoundField HeaderText="Time Extention" DataField="TE" />
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
                                                <asp:GridView ID="grdContractBond" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdContractBond_PreRender" OnRowDataBound="grdContractBond_RowDataBound">
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
                                                        <asp:BoundField DataField="ProjectWorkPkg_DocApplicable" HeaderText="ProjectWorkPkg_DocApplicable">
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
                                                        <asp:TemplateField HeaderText="Document Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCB_Date" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkPkgDoc_DocumentDate") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Refrence Number">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRef_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_DocumentNo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Document">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUploadCB" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadCB" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkPkgDoc_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Not Applicable">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkNA" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_Comments") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDeleteCB" OnClick="btnDeleteCB_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
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
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnUploadDocs1" Text="Upload Bond Documents" OnClick="btnUploadDocs1_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
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
                                                <asp:GridView ID="grdBG" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdBG_PreRender" OnRowDataBound="grdBG_RowDataBound" ShowFooter="true">
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
                                                        <asp:BoundField DataField="ProjectWorkPkg_DocApplicable" HeaderText="ProjectWorkPkg_DocApplicable">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Effective From Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBGFrom" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkPkgDoc_DocumentDate") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Effective Till Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBGTill" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkPkgDoc_DocumentDate2") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BG Number">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRef_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_DocumentNo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount (In Rupees)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_Amount") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Document">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUploadBG" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAddBG" OnClick="btnAddBG_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                <asp:ImageButton ID="btnRemoveBG" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnRemoveBG_Click" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadBG" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkPkgDoc_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Not Applicable">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkNA" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_Comments") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDeleteBG" OnClick="btnDeleteBG_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnUploadDocs2" Text="Upload Documents" OnClick="btnUploadDocs2_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Upload Liquidated Damages (LD) Documents
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdLD" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdLD_PreRender" OnRowDataBound="grdLD_RowDataBound" ShowFooter="true">
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
                                                        <asp:BoundField DataField="ProjectWorkPkg_DocApplicable" HeaderText="ProjectWorkPkg_DocApplicable">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_PathW" HeaderText="ProjectWorkPkgDoc_PathW">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkPkgDoc_Withdraw" HeaderText="ProjectWorkPkgDoc_Withdraw">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLDFrom" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkPkgDoc_DocumentDate") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Letter Number">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRef_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_DocumentNo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount  (In Rupees)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_Amount") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Document For LD Impose">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUploadLD" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAddLD" OnClick="btnAddLD_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LD Not Imposed">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkNA" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details / Clause For LD Imposition">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control " Text='<%# Eval("ProjectWorkPkgDoc_Comments") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LD Withdrawn">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkWithdrawn" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason For LD Withdrawal">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCommentsWithdrawal" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPkgDoc_CommentsW") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Document For LD Withdrawn">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUploadLDWithdraw" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnRemoveLD" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnRemoveLD_Click" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadLD" runat="server" Text="Download LD" GO_FilePath='<%#Eval("ProjectWorkPkgDoc_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                <br />
                                                                <asp:LinkButton ID="lnkDownloadLDWithdraw" runat="server" Text="Download LD Withdraw" GO_FilePath='<%#Eval("ProjectWorkPkgDoc_PathW") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDeleteLD" OnClick="btnDeleteLD_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnUploadDocs3" Text="Upload Documents (LD)" OnClick="btnUploadDocs3_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Text="Save and Next >>" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSkip" Text="Skip and Next >>" OnClick="btnSkip_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField ID="hf_ProjectWorkPkg_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUploadDocs1" />
                    <asp:PostBackTrigger ControlID="btnUploadDocs2" />
                    <asp:PostBackTrigger ControlID="btnUploadDocs3" />
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
</asp:Content>



