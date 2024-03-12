<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_1_Green.aspx.cs" Inherits="MasterProjectWorkMIS_1" MaintainScrollPositionOnPostback="true"
    EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
                EnablePageMethods="true" AsyncPostBackTimeout="6000">
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

                                <li data-step="2">
                                    <span class="step">2</span>
                                    <span class="title">GO Release Details</span>
                                </li>

                                <li data-step="3">
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
                                    Create Project
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Scheme* </label>
                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">District* </label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control"
                                            data-placeholder="Choose a District..." AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">ULB* </label>
                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Project Code* </label>
                                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Project Name* </label>
                                        <asp:TextBox ID="txtProjectWorkName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" Text="Program*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="GO No*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtGONo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="GO Date*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtGODate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                            data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" Text="Upload Budget Sanctioned GO" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:FileUpload ID="flUploadGO" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" Text="Project Start Date *" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control date-picker"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="Project Completion Date" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCompleteDate" runat="server" CssClass="form-control date-picker"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <a id="aGO" href="#" target="_blank" runat="server" onclick="return downloadGO(this);"><b>View GO</b></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Sanctioned Amount [Without Centage] (In Lakhs)*"
                                            CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblCentage" runat="server" Text="Centage (In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtCentage" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="Total Cost (Including Centage)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtWorkCost_Centage" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Already Created Package Details
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdPackageDetails" runat="server" CssClass="display table table-bordered"
                                            AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPackageDetails_PreRender"
                                            ShowFooter="true" OnRowDataBound="grdPackageDetails_RowDataBound">
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
                                                <asp:BoundField DataField="ProjectWorkPkg_GST" HeaderText="ProjectWorkPkg_GST">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWorkPkg_Percent" HeaderText="ProjectWorkPkg_Percent">
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

                                                <asp:BoundField HeaderText="Agreement No" DataField="ProjectWorkPkg_Agreement_No" />
                                                <asp:BoundField HeaderText="Agreement Date" DataField="ProjectWorkPkg_Agreement_Date" />
                                                <asp:BoundField HeaderText="Actual Date Of Start" DataField="ProjectWorkPkg_Start_Date" />
                                                <asp:BoundField HeaderText="Due Date Of Completion" DataField="ProjectWorkPkg_Due_Date" />
                                                <asp:BoundField HeaderText="Tender Cost (In Lakhs)" DataField="ProjectWorkPkg_AgreementAmount" />

                                                <asp:TemplateField HeaderText="GST Type">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rbtGSTType" runat="server">
                                                            <asp:ListItem Text="Exclude GST" Value="Exclude GST" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Include GST" Value="Include GST"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GST Percent(%)">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlGSTPercentage" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="5" Value="5.00"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12.00" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18.00"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28.00"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Due Date Of Completion Extend">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtextenddate" runat="server" CssClass="form-control date-picker"
                                                            autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%#Eval("ProjectWorkPkg_ExtendDate") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pkg Physically Completed">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkPhysicalCompleted" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Text="Save and Next >>" OnClick="btnSave_Click" runat="server"
                                            CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSkip" Text="Skip and Next >>" OnClick="btnSkip_Click" runat="server"
                                            CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_GO_Path" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
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
    </div>
    <script type="text/javascript">
        function downloadGO(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_GO_Path').value;
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
    </script>
</asp:Content>



