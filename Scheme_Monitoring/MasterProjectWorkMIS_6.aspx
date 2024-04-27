<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectWorkMIS_6.aspx.cs" Inherits="MasterProjectWorkMIS_6" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                            CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Upload Utilization Certificate  </h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdCallProductDtls_PreRender" OnRowDataBound="grdCallProductDtls_RowDataBound">
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
                                                                <asp:TemplateField HeaderText="UC Date">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUCDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectUC_SubmitionDate") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UC Number">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUC_Number" runat="server" CssClass="form-control" Text='<%# Eval("ProjectUC_Comments") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UC Filled %">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUCP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Text='<%# Eval("ProjectUC_Achivment") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Upload UC Document">
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="flUploadUC" runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                        <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Upload UC Document">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkUCDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectUC_Document") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAction" OnClick="btnAction_Click" runat="server" ToolTip="Click to edit" ImageUrl="~/assets/images/edit.png" Width="25px" Height="25px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDeleteUC" OnClick="btnDeleteUC_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="25px" Height="25px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
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


                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Project Related Specific Issues (If Any)</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                <div class="col-xxl-12 col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdIssue" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdIssue_PreRender" OnRowDataBound="grdIssue_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Id" HeaderText="ProjectWorkIssueDetails_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectWorkIssueDetails_Path" HeaderText="ProjectWorkIssueDetails_Path">
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
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblRowNumber" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Issuing Type">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueType_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sub Issue">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlDependency" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discription" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIssuingCategory" runat="server" CssClass="form-control" Text='<%# Eval("ProjectWorkIssueDetails_Category") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Issuing Effective Date">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIssuingDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkIssueDetails_Date") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Comments">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Eval("ProjectWorkIssueDetails_Comments") %>' />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnQuestionnaireU" OnClick="btnQuestionnaireU_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                        <asp:ImageButton ID="btnDeleteQuestionnaireU" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnDeleteQuestionnaireU_Click" Width="30px" Height="30px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Upload Issue Document">
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="flUploadIssue" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Download Document">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkIssueDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("ProjectWorkIssueDetails_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add Conversation Log">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkConLog" runat="server" Text="Add Conversation" OnClick="lnkConLog_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Resolved Date">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtResolvedDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy" Text='<%# Eval("ProjectWorkIssueDetails_DateResolved") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Resolved">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDeleteIssue" OnClick="btnDeleteIssue_Click" runat="server" ImageUrl="~/assets/images/resolved.jpg" Width="35px" Height="35px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
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




                        <div id="divLog" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Conversation Log Against Specific Issue</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdLog" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdLog_PreRender">
                                                                <Columns>
                                                                    <asp:BoundField DataField="PMIS_ProjectWorkIssueHistory_Id" HeaderText="PMIS_ProjectWorkIssueHistory_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PMIS_ProjectWorkIssueHistory_IssueDetails_Id" HeaderText="PMIS_ProjectWorkIssueHistory_IssueDetails_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PMIS_ProjectWorkIssueHistory_Path" HeaderText="PMIS_ProjectWorkIssueHistory_Path">
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
                                                                            <asp:TextBox ID="txtLogDate" runat="server" Text='<%# Eval("PMIS_ProjectWorkIssueHistory_Date") %>' CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Subject">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSubject" runat="server" Text='<%# Eval("PMIS_ProjectWorkIssueHistory_Subject") %>' CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Letter Written By Authority / Designation">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtFrom" Text='<%# Eval("PMIS_ProjectWorkIssueHistory_LetterWrittenBy") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Letter Written To Authority / Designation">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtTo" Text='<%# Eval("PMIS_ProjectWorkIssueHistory_LetterWrittenTo") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Comments">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtLogComments" runat="server" TextMode="MultiLine" Text='<%# Eval("PMIS_ProjectWorkIssueHistory_Comments") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="btnAddLog" OnClick="btnAddLog_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                            <asp:ImageButton ID="btnRemoveLog" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="btnRemoveLog_Click" Width="30px" Height="30px" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload Issued Letter Document">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="flUploadLog" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Download Document">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkIssueLogDoc" runat="server" Text="Download" GO_FilePath='<%#Eval("PMIS_ProjectWorkIssueHistory_Path") %>' OnClientClick="return downloadGO(this);"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnSaveLog" Text="Save All" OnClick="btnSaveLog_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                                                            <asp:HiddenField ID="hf_Issue_Indx" runat="server" Value="0"></asp:HiddenField>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDeleteLog" OnClick="btnDeleteLog_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="30px" Height="30px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
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
                        <div class="row">

                            <div class="col-xxl-12 col-md-12 text-center">
                                <div>
                                    <asp:Button ID="btnSave" Text="Update Details" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                </div>
                            </div>

                        </div>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 500px; margin-left: -32px" ScrollBars="Auto">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Action On UC</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label5" runat="server" Text="Action*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList CssClass="form-select" ID="ddlAction" runat="server">
                                                                <asp:ListItem Text="---Select---" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="Label19" runat="server" Text="Comments*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtCommentsUC" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <br />
                                                            <asp:Button ID="btnUpdateAction" Text="Update" OnClick="btnUpdateAction_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
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
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Scheme_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Sr_No" runat="server" Value="0" />
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
        <!-- /.main-content -->
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





