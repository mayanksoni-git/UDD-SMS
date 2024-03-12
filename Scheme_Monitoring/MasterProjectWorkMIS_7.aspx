<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_7.aspx.cs" Inherits="MasterProjectWorkMIS_7" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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

                                <li data-step="6" class="active">
                                    <span class="step">6</span>
                                    <span class="title">UC Details and Issues</span>
                                </li>

                                <li data-step="7" class="active">
                                    <span class="step">7</span>
                                    <span class="title">Variation Details</span>
                                </li>
                            </ul>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Please Select A Package To Open BOQ for Variation Details                                   
                               
                                </div>
                            </div>
                        </div>
                        <div class="space"></div>
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

                        <div class="row" id="divBOQUploadTool" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:Button ID="btnDownloadVariationBOQ" Text="Download Excel For Varation Entry" OnClick="btnDownloadVariationBOQ_Click" runat="server" CssClass="btn btn-inverse"></asp:Button>
                                </div>
                                <div class="col-md-1">
                                    <button class="btn btn-danger btn-sm" onclick="return downloadGOE(this);" title="Download Excel For Extra Item Entry" runat="server" id="btnDownloadExtraItem">
                                        <i class="ace-icon fa fa-download icon-only"></i>
                                    </button>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label no-padding-right">Upload Excel For Varation Entry:</label>
                                    <asp:FileUpload ID="flUploadVariationEntry" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label no-padding-right">Upload Excel For Extra Item Entry:</label>
                                    <asp:FileUpload ID="dlUploadExtraItemEntry" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnUploadData" Text="Upload Excel Files" OnClick="btnUploadData_Click" runat="server" CssClass="btn btn-dark"></asp:Button>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdBOQ" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdBOQ_PreRender" OnRowDataBound="grdBOQ_RowDataBound">
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
                                                        <asp:DropDownList ID="ddlVariation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVariation_SelectedIndexChanged">
                                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity As Per CB">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyM" runat="server" Text='<%# Eval("PackageBOQ_Qty") %>' Width="90px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity As Per Actual Measurment">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyV" runat="server" Text='<%# Eval("BOQ_Qty") %>' Width="90px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estimated Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRE" runat="server" Text='<%# Eval("PackageBOQ_RateEstimated") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quoted Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRQ" runat="server" Text='<%# Eval("PackageBOQ_RateQuoted") %>' Width="90px" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comments">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Eval("PackageBOQVariation_Comments") %>'>
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PackageBOQVariationTemp_Id" HeaderText="PackageBOQVariationTemp_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divActionBOQ1" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">No Variation Available</label>
                                            <br />
                                            <asp:CheckBox ID="chkNoVariation" runat="server" AutoPostBack="true" OnCheckedChanged="chkNoVariation_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Add Extra Item</label>
                                            <br />
                                            <asp:CheckBox ID="chkAddExtraItem" runat="server" AutoPostBack="true" OnCheckedChanged="chkAddExtraItem_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Approval Report (SLTC):</label>
                                            <asp:FileUpload ID="flUploadPDF" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <button class="btn btn-danger btn-sm" onclick="return downloadGO(this);" title="Download Approval Report (SLTC)" runat="server" id="aGO">
                                                <i class="ace-icon fa fa-download icon-only"></i>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload BOQ Excel:</label>
                                            <asp:FileUpload ID="flUploadExcel" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <button class="btn btn-danger btn-sm" onclick="return downloadGO1(this);" title="Download BOQ Excel" runat="server" id="aGO2">
                                                <i class="ace-icon fa fa-download icon-only"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Upload Appraisal Note Document</label>
                                            <asp:FileUpload ID="flAppraisalNote" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:GridView ID="grdMultipleFiles" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdMultipleFiles_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ExtraItemDocs_FileName" HeaderText="ExtraItemDocs_FileName">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="File Name" DataField="ExtraItemDocs_FileName" />
                                                    <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("ExtraItemDocs_FileName") %>' OnClientClick="return downloadFile(this);"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divPartialSaving" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:CheckBox ID="chkEnablePartialSaving" runat="server" Text="Enable Partial Saving." />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:CheckBox ID="chkLoadFromPartialSavedData" runat="server" Text="Load From Partial Saved Data." AutoPostBack="true" OnCheckedChanged="chkLoadFromPartialSavedData_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnDeletePartialSavedData" Text="Delete Partial Saved Data" OnClick="btnDeletePartialSavedData_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divActionBOQExtraItem" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdExtraItem" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdExtraItem_PreRender" ShowFooter="true" OnRowDataBound="grdExtraItem_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="PackageBOQ_Package_Id" HeaderText="PackageBOQ_Package_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageBOQ_Unit_Id" HeaderText="PackageBOQ_Unit_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageBOQVariation_Id" HeaderText="PackageBOQVariation_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageBOQVariation_GSTType" HeaderText="PackageBOQVariation_GSTType">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PackageBOQVariation_GST" HeaderText="PackageBOQVariation_GST">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification / BOQ Extra Item">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSpecification" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_Specification") %>' TextMode="MultiLine"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="btnAdd" Width="20px" Height="20px" OnClick="btnAdd_Click" ImageUrl="~/assets/images/add-icon.png" runat="server" />
                                                        &nbsp;
                                                               
                                                        <asp:ImageButton ID="btnMinus" Width="20px" Height="20px" OnClick="btnMinus_Click" ImageUrl="~/assets/images/minus-icon.png" runat="server" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_Qty") %>' MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estimated Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRateEstimate" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_RateEstimated") %>' MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quoted Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRateQuoted" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_RateQuoted") %>' MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GST Type">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rbtGSTType" runat="server" RepeatDirection="Vertical">
                                                            <asp:ListItem Text="Exclude GST" Value="Exclude GST" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Include GST" Value="Include GST"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GST %">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlGSTPercentage" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="5" Value="5.00"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12.00"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18.00" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28.00"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty Paid Till Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyPaid" runat="server" CssClass="form-control" Text='<%# Eval("PackageBOQ_QtyPaid") %>' MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divActionBOQ2" runat="server" visible="false">
                            <div class="col-sm-12">
                                <div class="col-md-3" runat="server" id="divTenderCostRevised">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Revised Tender Cost Excluding GST (In Lakhs):</label>
                                        <asp:TextBox ID="txtTenderCostRevised" runat="server" Text="0" CssClass="form-control" MaxLength="10" onkeyup="isNumericVal(this);" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSave" Text="Save Package Variation" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:CheckBox ID="chkFinalAtZonal" runat="server" Text="End Variation Approval here As It has been Approved At Zonal Chief Level Only." />
                                        <asp:Button ID="btnUpdate" Text="Update Variation In Origional BOQ" OnClick="btnUpdate_Click" runat="server" CssClass="btn btn-btn-purple"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnSaveFinal" Text="Save and Complete" OnClick="btnSave_Click_Final" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnSkip" Text="Skip and Close >>" OnClick="btnSkip_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Package_ExtraItem_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWorkPkg_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_IsFirst" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Path" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Path1" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Loop" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnDownloadVariationBOQ" />
                    <asp:PostBackTrigger ControlID="btnDownloadExtraItem" />
                    <asp:PostBackTrigger ControlID="btnUploadData" />
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
    <script type="text/javascript">
        function downloadGO(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Path').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }

        function downloadGO1(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Path1').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }

        function downloadGOE(obj) {
            var path = "/Downloads/Sample_ExtraItem.xls";
            window.open(location.origin + path, "_blank", "", false);
            return false;
        }

        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }
    </script>

</asp:Content>





