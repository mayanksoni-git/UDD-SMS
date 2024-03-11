<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterCheckListNOC.aspx.cs" Inherits="MasterCheckListNOC" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    Configuration Of NOC Check List
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div style="overflow: auto">
                                    <asp:GridView ID="dgvQuestionnaire" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" ShowFooter="true" OnRowDataBound="dgvQuestionnaire_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="NOCQuestionnaire_Id" HeaderText="NOCQuestionnaire_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOCQuestionnaire_QuestionType" HeaderText="NOCQuestionnaire_QuestionType">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNOCQuestionnaire_Sr" runat="server" CssClass="form-control" Text='<%# Eval("NOCQuestionnaire_Sr") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Questionnaire">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNOCQuestionnaire_Name" runat="server" CssClass="form-control" Text='<%# Eval("NOCQuestionnaire_Name") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnQuestionnaire" OnClick="btnQuestionnaire_Click" runat="server" ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                    <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question Type">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Selected="True" Text="Subjective" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Yes / No" Value="Y"></asp:ListItem>
                                                    </asp:DropDownList>
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
    <!-- /.main-content -->

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
    <script src="assets/js/dataTables.colReorder.min.js"></script>

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



