<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_3.aspx.cs" Inherits="MasterProjectWorkMIS_3" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <style type='text/css'>
            input[type=checkbox] {
                height: 18px;
                width: 18px;
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

                    <cc1:ModalPopupExtender ID="mpLog" runat="server" PopupControlID="Panel2" TargetControlID="btnShowPopup2"
                        CancelControlID="btnclose2" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup2" Text="Show" runat="server" Style="display: none;"></asp:Button>

                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel3" TargetControlID="btnShowPopup3"
                        CancelControlID="btnclose3" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup3" Text="Show" runat="server" Style="display: none;"></asp:Button>

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

                                <li data-step="4">
                                    <span class="step">4</span>
                                    <span class="title">Physical Components</span>
                                </li>

                                <li data-step="5">
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
                                    Invoice Details
                               
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdProjectInvoiceDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdProjectInvoiceDetails_PreRender" OnRowDataBound="grdProjectInvoiceDetails_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2"><%# Eval("ProjectWork_ProjectCode") %></td>
                                                                </tr>

                                                                <tr>
                                                                    <td colspan="2"><%# Eval("ProjectWork_Name") %></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Previous Invoice</td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lblPrevInvoiceTotal" Text="0.00" runat="server" OnClick="lblPrevInvoiceTotal_Click" Font-Bold="true"></asp:LinkButton></td>
                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" ControlStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInv" Text='Pkg invoices' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADP" Text='Other Dept' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMA" Text='Advance' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDR" Text='Ded Release' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Financial Achivment Till May 2021 (Bills Raised)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInvBefore" Text='<%# Eval("Inv_Before") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADPBefore" Text='<%# Eval("ADP_Before") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMABefore" Text='<%# Eval("MA_Before") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDRBefore" Text='<%# Eval("DR_Before") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#FFB752" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Financial Achivment Till May 2021 (Bills Approved)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInvBeforeA" Text='<%# Eval("Inv_Before_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADPBeforeA" Text='<%# Eval("ADP_Before_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMABeforeA" Text='<%# Eval("MA_Before_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDRBeforeA" Text='<%# Eval("DR_Before_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#FFB752" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Financial Achivment June 2021 (Bills Raised)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInv_Current_Less" Text='<%# Eval("Inv_Current_Less") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADP_Current_Less" Text='<%# Eval("ADP_Current_Less") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMA_Current_Less" Text='<%# Eval("MA_Current_Less") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDR_Current_Less" Text='<%# Eval("DR_Current_Less") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#6FB3E0" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Financial Achivment June 2021 (Bills Approved)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInv_Current_Less_A" Text='<%# Eval("Inv_Current_Less_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADP_Current_Less_A" Text='<%# Eval("ADP_Current_Less_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMA_Current_Less_A" Text='<%# Eval("MA_Current_Less_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDR_Current_Less_A" Text='<%# Eval("DR_Current_Less_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#6FB3E0" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Financial Achivment July 2021 (Bills Raised)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInv_Current" Text='<%# Eval("Inv_Current") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADP_Current" Text='<%# Eval("ADP_Current") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMA_Current" Text='<%# Eval("MA_Current") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDR_Current" Text='<%# Eval("DR_Current") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#87B87F" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Financial Achivment July 2021 (Bills Approved)">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblInv_Current_A" Text='<%# Eval("Inv_Current_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblADP_Current_A" Text='<%# Eval("ADP_Current_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMA_Current_A" Text='<%# Eval("MA_Current_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDR_Current_A" Text='<%# Eval("DR_Current_A") %>' runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#87B87F" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Achivment Till Date (%)" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>Physical</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhysicalTarget" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkPhysicalTarget_Target") %>' onkeyup="isNumericVal(this);"></asp:TextBox></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Financial</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFinancialTarget" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkFinancialTarget_Target") %>' onkeyup="isNumericVal(this);" Enabled="false"></asp:TextBox></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>SNA Limit Needed (In Lakhs)</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFinancialTargetA" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkFinancialTarget_TargetA") %>' onkeyup="isNumericVal(this);"></asp:TextBox></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target Month For Project Completion">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
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
                                                        <asp:TextBox ID="txtTargetMonth" runat="server" CssClass="form-control datepicker" autocomplete="off" Width="80px"></asp:TextBox>
                                                        <hr />
                                                        <span><b>Trial Run Till:</b></span>
                                                         <asp:TextBox ID="txtTrialRunDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkFinancialTarget_TrialMonth") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <table class="display table table-bordered no-margin-bottom no-border-top">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Text="View Package Wise Details" /></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="lnkViewLog" runat="server" Width="40px" Height="50px" ImageUrl="~/assets/images/log.png" OnClick="lnkViewLog_Click" /></td>
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
                        <div id="divCentage" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Centage Send To HQ Jal Nigam
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdCentageDetails" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdCentageDetails_PreRender" OnRowDataBound="grdCentageDetails_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectCentageReceived_Id" HeaderText="ProjectCentageReceived_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectCentageReceived_Path" HeaderText="ProjectCentageReceived_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectCentageReceived_ProjectWork_Id" HeaderText="ProjectCentageReceived_ProjectWork_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCentageDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectCentageReceived_Date") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Letter Number">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRef_Number" runat="server" CssClass="form-control " Text='<%# Eval("ProjectCentageReceived_LetterNo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount  (In Rupees)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control " Text='<%# Eval("ProjectCentageReceived_Amount") %>' onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Document">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUpload" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAdd" OnClick="btnAdd_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                <asp:ImageButton ID="btnRemove" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnRemove_Click" Width="30px" Height="30px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Document">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectCentageReceived_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
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
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkPhysicalCompleted" Text="Physical Completed With Reduced Scope. (Variation Document is Mandatory to Upload On Step 7)" Font-Bold="true" runat="server"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkFinancialCompletedAll" Text="Financial Progress Completed No Further Payment is Required. (Project Will be marked as Completed and EMB and Invoicing will be disabled)" Font-Bold="true" runat="server"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkFinancialCompletedPartial" Text="Financial Progress Completed No Further Payment is Required. (Deduction Release is Pending To Be Raised)" Font-Bold="true" runat="server"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Physical Closure Details
                                           
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label ID="lbl1" Text="Physical Closure Appliable" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="rbtPhysicalClosureApplicable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPhysicalClosureApplicable_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text=" Yes " Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text=" No " Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="divHandoverNote" runat="server" visible="false">
                                        <div class="col-xs-12">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" Text="Handover Note Send To Local Body" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="rbtHandoverNote" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtHandoverNote_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text=" Yes " Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text=" No " Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divHandoverNoteYes" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" Text="Letter No" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandoverNoteYesRef_Number" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" Text="Letter Date" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandoverNoteYesDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label4" Text="Upload Letter" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <button class="btn btn-danger btn-sm" id="btnDownloadLetterHandover" runat="server" onclick="return DownloadLetterHandover(this);">
                                                            <i class="ace-icon fa fa-download icon-only"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:FileUpload ID="flHandoverNoteDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divHandoverNoteNo" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label6" Text="Tentitive Date" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandoverTentitiveDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" Text="Remarks" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandoverNoRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="divHandOverDone" runat="server" visible="false">
                                        <div class="col-xs-12">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label8" Text="Handover Done" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="rbtHandOverDone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtHandOverDone_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text=" Yes " Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text=" No " Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divHandOverDoneYes" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label9" Text="Letter No" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandOverDoneLetterNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label10" Text="Letter Date" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandOverDoneDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label11" Text="Upload Letter" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <button class="btn btn-danger btn-sm" id="btnHandOverDoneDoc" runat="server" onclick="return DownloadHandOverDoneDoc(this);">
                                                            <i class="ace-icon fa fa-download icon-only"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:FileUpload ID="flHandOverDoneDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" Text="Defect Liability Period Upto" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtDefectLiabilityDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divHandOverDoneNo" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label12" Text="Tentitive Date" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandOverDoneTentitiveDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label13" Text="Remarks" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHandOverDoneRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Financial Closure Details
                                           
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label14" Text="Financial Closure Appliable" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="rbtFinancialClosureApplicable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtFinancialClosureApplicable_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text=" Yes " Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text=" No " Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divFinancialClosureYes" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label15" Text="Letter No" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtFinancialClosureLetterNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label16" Text="Letter Date" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtFinancialClosureLetterDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label17" Text="Upload Letter" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <button class="btn btn-danger btn-sm" id="btnFinancialClosureDoc" runat="server" onclick="return DownloadFinancialClosureDoc(this);">
                                                            <i class="ace-icon fa fa-download icon-only"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:FileUpload ID="flFinancialClosureDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divFinancialClosureNo" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label19" Text="Tentitive Date" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtFinancialClosureTentitiveDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                            data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label20" Text="Remarks" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtFinancialClosureRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnUploadDocs1" Text="Update Physical and Financial Closure Details" OnClick="btnUploadDocs1_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divFinancialDoc" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Upload Financial Document Demanded By Directorate AMRUT
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12" style="overflow-x: auto">
                                            <div>
                                                <asp:GridView ID="grdFinancialDoc" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdFinancialDoc_PreRender" OnRowDataBound="grdFinancialDoc_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkFinancialDoc_Id" HeaderText="ProjectWorkFinancialDoc_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkFinancialDoc_PathLetter" HeaderText="ProjectWorkFinancialDoc_PathLetter">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectWorkFinancialDoc_PathExcel" HeaderText="ProjectWorkFinancialDoc_PathExcel">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Report Compiled Till Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCompiledDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkFinancialDoc_Date") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Signed Letter">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUploadLetter" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Excel Document">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUploadExcel" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCommentsFinancial" runat="server" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("ProjectWorkFinancialDoc_Comments") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadLetter" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkFinancialDoc_PathLetter") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download Excel File">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadExcel" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkFinancialDoc_PathExcel") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
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
                                        <div class="col-xs-12" style="overflow-x: auto">

                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered table-responsive" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true" ShowHeader="false" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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
                                                                    <table class="display table table-bordered">
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
                                                                                            <asp:GridView ID="grdPostBeat" runat="server" CssClass="display table table-bordered table-responsive" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPostBeat_PreRender">
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
                                                                                                    <asp:TemplateField HeaderText="View Docs">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkOtherDocsI" runat="server" Text="View" OnClick="lnkOtherDocsI_Click" Font-Bold="true"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
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
                                                                                            <asp:GridView ID="grdADP" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdADP_PreRender" OnRowDataBound="grdADP_RowDataBound">
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
                                                                                                    <asp:TemplateField HeaderText="View Docs">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkOtherDocsA" runat="server" Text="View" OnClick="lnkOtherDocsA_Click" Font-Bold="true"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
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
                                                                                            <asp:GridView ID="grdMA" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdMA_PreRender" OnRowDataBound="grdMA_RowDataBound">
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
                                                                                                    <asp:TemplateField HeaderText="View Docs">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkOtherDocsM" runat="server" Text="View" OnClick="lnkOtherDocsM_Click" Font-Bold="true"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
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
                                                                                            <asp:GridView ID="grdDeductionRelease" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdDeductionRelease_PreRender" OnRowDataBound="grdDeductionRelease_RowDataBound">
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
                                                                                                    <asp:TemplateField HeaderText="View Docs">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkOtherDocsD" runat="server" Text="View" OnClick="lnkOtherDocsD_Click" Font-Bold="true"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
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
                        <div id="divPreInvoiceDetails" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Previous Invoice Details (Package RA Invoices)                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdInvoice" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true">
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
                                                    <asp:BoundField DataField="ProjectWorkPkg_Code" HeaderText="Pkg Code" />
                                                    <asp:BoundField DataField="PackageInvoice_Date" HeaderText="Invoice Date" />
                                                    <asp:BoundField DataField="PackageInvoice_VoucherNo" HeaderText="Voucher No" />
                                                    <asp:BoundField DataField="Total_Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="GST" HeaderText="GST Amount" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Total Amount" />
                                                    <asp:BoundField DataField="PackageInvoice_AddedOn" HeaderText="Added On" />
                                                </Columns>
                                                <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
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
                                            <asp:GridView ID="grdInvoiceADP" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true">
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
                        <div class="row">
                            <div class="col-sm-12">
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
                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDeductionHistory" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
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

                    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup1" Style="display: none; width: 930px; margin-left: -32px">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdChangeLog" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
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
                                        <asp:Button ID="btnclose2" Text="Close" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Other Related Documents Attached                                 
                               
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdMultipleFiles" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdMultipleFiles_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="PackageInvoiceDocs_FileName" HeaderText="PackageInvoiceDocs_FileName">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="File Name" DataField="TradeDocument_Name" />
                                                    <asp:BoundField HeaderText="Order No" DataField="PackageInvoiceDocs_OrderNo" />
                                                    <asp:BoundField HeaderText="Comments" DataField="PackageInvoiceDocs_Comments" />
                                                    <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("PackageInvoiceDocs_FileName") %>' OnClientClick="return downloadFile(this);"></asp:LinkButton>
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
                                        <button id="btnclose3" runat="server" text="Close" class="btn btn-warning"></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:HiddenField ID="hf_PrePhysicalProgress" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_DownloadLetterHandover" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_HandOverDoneDoc" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_FinancialClosureDoc" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUploadDocs1" />
                    <asp:PostBackTrigger ControlID="btnSave" />
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
        <!-- /.main-content -->
    </div>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[src*=minus]").each(function () {
                $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                $(this).next().remove()
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>

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

        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }

        function DownloadLetterHandover(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_DownloadLetterHandover').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function DownloadHandOverDoneDoc(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_HandOverDoneDoc').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function DownloadFinancialClosureDoc(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_FinancialClosureDoc').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }
    </script>
</asp:Content>





