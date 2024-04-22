<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterInspectionConfiguration.aspx.cs" Inherits="MasterInspectionConfiguration" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="container-fluid">
                        <div id="divhide" runat="server" visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Scheme Wise Inspection Configuration
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="dgvQuestionnaire" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true" OnRowDataBound="dgvQuestionnaire_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectQuestionnaire_ProjectId" HeaderText="ProjectQuestionnaire_ProjectId">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectQuestionnaire_Id" HeaderText="ProjectQuestionnaire_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Questionnaire">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProjectQuestionnaire_Name" runat="server" CssClass="form-control" Text='<%# Eval("ProjectQuestionnaire_Name") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                        <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add Answers">
                                                    <ItemTemplate>
                                                        <asp:ImageButton src="assets/images/add-icon.png" Width="30px" Height="30px" runat="server" ID="imgAddAnswer" OnClick="imgAddAnswer_Click"></asp:ImageButton>
                                                        <asp:CheckBox ID="chkAnswerDesc" runat="server" Text="Select For Descriptive Answer" />
                                                        <asp:HiddenField ID="hdf_Answer" Value="" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">

                                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                            &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-warning"></asp:Button>&nbsp; &nbsp; &nbsp;
                                         <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn btn-light"></asp:Button>

                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-12">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Inspection Configuration Master</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Basic Masters</li>
                                                <li class="breadcrumb-item active">Inspection Configuration</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-xs-12">

                                        <!-- div.table-responsive -->
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
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

                                                    <asp:BoundField DataField="Project_Name" HeaderText="Scheme" />
                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                    <asp:BoundField DataField="Created_Date" HeaderText="Created Date" />
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
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
                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Inspection Configuration Answer
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div style="overflow: auto">
                                    <asp:GridView ID="gdvAnswer" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectAnswer_ProjectQuestionnaireId" HeaderText="ProjectAnswer_ProjectQuestionnaireId">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectAnswer_Id" HeaderText="ProjectAnswer_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answer">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtProjectAnswer_Name" runat="server" CssClass="form-control" Text='<%# Eval("ProjectAnswer_Name") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnAddItemAnswer" OnClick="btnAddItemAnswer_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                    <asp:ImageButton ID="btnMinusDate" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdeleteAnswer_Click" Width="30px" Height="30px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <br />
                                        <asp:Button ID="btnSaveAnswer" runat="server" CssClass="btn btn-info" Text="Save Answer" OnClick="btnSaveAnswer_Click" />
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btnAddYes_No" runat="server" CssClass="btn btn-dark" Text="Add Yes / No" OnClick="btnAddYes_No_Click" />
                                    </div>
                                    <div class="col-xs-6">
                                        <br />
                                        <asp:Button ID="btnclose" runat="server" CssClass="btn btn-warning" Text="Close" />
                                    </div>

                                </div>
                                <!-- PAGE CONTENT ENDS -->
                            </div>
                            <!-- /.col -->
                        </div>

                    </asp:Panel>
                    <asp:HiddenField ID="hf_Project_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hdf_AnswerGlobal" Value="" runat="server" />
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
    <!-- /.main-content -->

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



