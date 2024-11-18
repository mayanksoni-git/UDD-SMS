<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProjectType.aspx.cs" Inherits="MasterProjectType" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Project Type Master</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Basic Masters</li>
                                                <li class="breadcrumb-item active">Project Type</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-xs-12">
                                            <div class="clearfix">
                                                <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Create New" CssClass="btn-filter mb-2"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="card" id="divAddNew" runat="server" visible="false">
                                <div class="card-body">
                                    
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                <h3>Project Type</h3>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label no-padding-right">Scheme </label>
                                                <asp:DropDownList ID="ddlSearchScheme" runat="server" CssClass="form-select mb-2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblProjectType" runat="server" Text="Project Type*" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtProjectType" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <asp:Label ID="lblDescription" runat="server" Text="Project Type Description" CssClass="control-label no-padding-right"></asp:Label>
                                                <%--<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control mb-2"></asp:TextBox>--%>
                                                <obout:Editor runat="server" ID="txtDescription" Height="400px" Width="100%">
                                                    <EditPanel FullHtml="true" />
                                                </obout:Editor>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group mt-3">
                                                <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn bg-danger text-white"></asp:Button>
                                                <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn bg-danger text-white"></asp:Button>
                                            </div>
                                        </div>

                                    </div>

                                 </div>
                                  </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                    <!-- div.table-responsive -->
                                                    <!-- div.dataTables_borderWrap -->
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ProjectType_Id" HeaderText="ProjectType_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectType_ProjectId" HeaderText="ProjectType_ProjectId">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectType_Description" HeaderText="ProjectType_Description">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ProjectType_Name" HeaderText="ProjectType" />
                                                                <asp:TemplateField HeaderText="Project Type Description">
                                                                    <ItemTemplate>
                                                                        <div id="htmlDesc" runat="server">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Project_Name" HeaderText="Scheme" />
                                                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                                <asp:BoundField DataField="Created_Date" HeaderText="Created Date" />
                                                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" />
                                                                <asp:BoundField DataField="Modified_Date" HeaderText="Modified Date" />
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
                                            <!-- PAGE CONTENT ENDS -->
                                        </div>
                                        <!-- /.col -->
                                    </div>

                                </div>

                            </div>
                            
                             </div>

                        <asp:HiddenField ID="hf_ProjectType_Id" runat="server" Value="0" />
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



