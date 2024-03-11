<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="PackageDeductionReleaseApproval.aspx.cs" Inherits="PackageDeductionReleaseApproval" %>

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

                                <div class="table-header">
                                    Select A Work Package TO Approval Deduction Release
                                    <div class="form-group" style="float: right; padding-right: 10px">
                                   <div class="row">
                                       <div class="col-sm-12">
                                           <div class="col-md-3">
                                               <img src="assets/images/rupee.png" width="20px" height="25px" />
                                           </div>
                                           <div class="col-md-3">
                                               <div class="price" style="font-weight: bold" id="divBalance" runat="server">
                                                   0.00														
                                               </div>
                                           </div>
                                           <div class="col-md-3">
                                               <small><b>INR</b></small>
                                           </div>
                                       </div>
                                       <small id="divAccountNo" runat="server"><b>Account No: 0294***110***2*7</b></small>
                                   </div>
                               </div>
                               
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label no-padding-right">Scheme </label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divCircle" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divDivision" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h3 class="header smaller lighter blue">Project Package List</h3>

                                            <!-- div.table-responsive -->
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Package_DeductionRelease_RefNo" HeaderText="Ref No" />
                                                        <asp:BoundField DataField="Package_DeductionRelease_Date" HeaderText="Ref Date" />
                                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Project" DataField="Project_Name" />
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
                                </div>
                            </div>
                        </div>
                        <div id="divEntry" runat="server" visible="false">
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Add Additional Department Payment Item Approval
                                       
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdDeductions" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                            <Columns>
                                                <asp:BoundField DataField="Package_DeductionRelease_Item_Id" HeaderText="Package_DeductionRelease_Item_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Package_DeductionRelease_Item_Deduction_Id" HeaderText="Package_DeductionRelease_Item_Deduction_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Category" DataField="Deduction_Category" />
                                                <asp:BoundField HeaderText="Deduction" DataField="Deduction_Name" />
                                                <asp:TemplateField HeaderText="Deduction Value">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeductionValue" Text='<%# Eval("Deduction_Value") %>' onkeyup="isNumericVal(this);" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduction Release">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeductionRelease" Text='<%# Eval("DeductionReleaseAmount") %>' onkeyup="isNumericVal(this);" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDeductionRelease_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Date</label>
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Ref No</label>
                                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Status</label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Total Amount</label>
                                            <br />
                                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="label label-xlg label-inverse arrowed arrowed-right"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="false" id="divAmountTransfered">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Total Amount Transfered</label>
                                            <asp:TextBox ID="txtFudTransfered" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="false" id="divPPA">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">PPA No Generated On PFMS:</label>
                                            <asp:TextBox ID="txtPPANo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPPANo_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="tabbable">
                                        <ul class="nav nav-tabs" id="myTab2">
                                            <li class="active">
                                                <a data-toggle="tab" href="#doc1" aria-expanded="true">
                                                    <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                    Upload Document (If Any)
                                                </a>
                                            </li>

                                            <li class="">
                                                <a data-toggle="tab" href="#doc2" aria-expanded="false">
                                                    <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                    Download / View Previous Uploaded Document
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="form-group form-check" style="float: right; padding-right: 10px">
                                            <asp:CheckBox ID="chkUpdateExisting" runat="server" Text="Update Existing Uploaded Document" />
                                        </div>
                                        <div class="tab-content">
                                            <div id="doc1" class="tab-pane fade active in">
                                                <asp:GridView ID="grdDocumentMaster" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Documents Configured To Upload" OnPreRender="grdDocumentMaster_PreRender" OnRowDataBound="grdDocumentMaster_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="TradeDocument_Id" HeaderText="TradeDocument_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProcessConfigDocumentLinking_DocumentMaster_Id" HeaderText="ProcessConfigDocumentLinking_DocumentMaster_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Document To Upload" DataField="TradeDocument_Name" />
                                                        <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDocumentOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select PDF File To Attach">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flUpload" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDocumentComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div id="doc2" class="tab-pane fade">
                                                <div style="overflow: auto">
                                                    <asp:GridView ID="grdMultipleFiles" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="grdMultipleFiles_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="Package_DeductionRelease_Docs_FileName" HeaderText="Package_DeductionRelease_Docs_FileName">
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
                                                            <asp:BoundField HeaderText="Order No" DataField="Package_DeductionRelease_Docs_OrderNo" />
                                                            <asp:BoundField HeaderText="Comments" DataField="Package_DeductionRelease_Docs_Comments" />
                                                            <asp:TemplateField HeaderText="Download">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" PersonFiles_FilePath='<%#Eval("Package_DeductionRelease_Docs_FileName") %>' OnClientClick="return downloadFile(this);"></asp:LinkButton>
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

                            <div class="row" id="divPPAMessage" runat="server">
                                <div class="col-md-12">
                                    <asp:Label ID="lblPPAMessage" runat="server" CssClass="label label-danger arrowed" Text="Please Input A Valid PPA No and Amount Of The Generated PPA through PFMA will be same as Invoice Amount Excluding Deduction."></asp:Label>
                                </div>
                            </div>
                            <div class="row" id="divPPAVerification" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdPPAVerification" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Documents Configured To Upload" OnPreRender="grdPPAVerification_PreRender">
                                        <Columns>
                                            <asp:BoundField HeaderText="PFMS Batch Number" DataField="PFMS_Batch_Number" />
                                            <asp:BoundField HeaderText="Amount" DataField="Batch_Amount" />
                                            <asp:BoundField HeaderText="Debit AC No" DataField="Debit_Account_Number" />
                                            <asp:BoundField HeaderText="PPA Received On" DataField="File_Received_Date" />
                                            <asp:BoundField HeaderText="PPA Expiry Date" DataField="Expiry_Date" />
                                            <asp:BoundField HeaderText="Batch Status" DataField="Batch_Status" />
                                            <asp:BoundField HeaderText="Failure Reason Code" DataField="Failure_Reason_Code" />
                                            <asp:BoundField HeaderText="Failure Reason Description" DataField="Failure_Reason_Description" />
                                            <asp:BoundField HeaderText="Response Status" DataField="Response_Status" />
                                            <asp:BoundField HeaderText="CBSTxnId" DataField="CBSTxnId" />
                                            <asp:BoundField HeaderText="CBSTranDate" DataField="CBSTranDate" />                                       
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <hr />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6" id="divAdditionalReason" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Status</label>
                                            <asp:DropDownList ID="ddlAdditionalReason" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="---Select---" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Funds Not Available In This Project"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="UC Not Received"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="ULB Share Not Received"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Mismatch Of Account"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Document Not Proper"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Mismatch Of Cover Letter Details"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="Others"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Reason / Comments</label>
                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnApprove" Text="Approve" OnClick="btnApprove_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnRevert" Text="Revert To Division" OnClick="btnRevert_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnRecommend" Text="Recommend TO SMD Sir" OnClick="btnRecommend_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Package_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Loop" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_Package_DeductionRelease_Id" runat="server" Value="0" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnApprove" />
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
                                    colReorder: false,
                                    fixedHeader: {
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
    </script>
    <script>
        function downloadFile(obj) {
            var PersonFiles_FilePath;
            PersonFiles_FilePath = obj.attributes.PersonFiles_FilePath.nodeValue;
            window.open(window.location.origin + PersonFiles_FilePath, "_blank", "", false);
            //location.href = window.location.origin + PersonFiles_FilePath;
            return false;
        }

        function openDoc(obj, fileType) {
            var path = '';
            debugger;
            if (fileType == "CB") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_Agreement').value;
            }
            else if (fileType == "PS") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_PerformanceSecurity').value;
            }
            else if (fileType == "BG") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_BG').value;
            }
            else if (fileType == "MA") {
                path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Doc_Mobelization').value;
            }
            else {
                path = "";
            }
            if (path != '') {
                obj.href = window.location.origin + path;
                window.open(window.location.origin + path, "_blank", "", false);
            }
            else {
                obj.href = "#";
                alert("No Document Found");
            }
            return false;
        }
    </script>
</asp:Content>

