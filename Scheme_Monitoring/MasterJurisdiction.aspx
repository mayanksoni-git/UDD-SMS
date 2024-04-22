<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterJurisdiction.aspx.cs" Inherits="MasterJurisdiction" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                        <div class="row">
                            <div class="col-12 ">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0"><asp:Label ID="lblMainHeader" runat="server" Text="Master Jurisdiction"></asp:Label></h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Jurisdiction Masters</li>
                                                <li class="breadcrumb-item active">Mandal</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div class="clearfix">
                                                        <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Create New" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="display: none;">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="lblLevel" runat="server" Text="Level" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
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
                                                        <div class="card-header align-items-center d-flex">
                                                            <h4 class="card-title mb-0 flex-grow-1">
                                                                <div class="table-header">
                                                                    
                                                                </div>
                                                            </h4>
                                                        </div>

                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="M_Jurisdiction_Id" HeaderText="M_Jurisdiction_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Parent_Jurisdiction_Id" HeaderText="Parent_Jurisdiction_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="Parent_Jurisdiction_Name_Eng" HeaderText="Parent Jurisdiction" />
                                                                    <asp:BoundField DataField="Jurisdiction_Name_Eng" HeaderText="Jurisdiction Name" />

                                                                    <asp:BoundField DataField="Jurisdiction_Code" HeaderText="Jurisdiction Code" />
                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                                    <asp:BoundField DataField="Created_Date" HeaderText="Created Date" />
                                                                    <asp:BoundField DataField="ModifyBy" HeaderText="Modified By" />
                                                                    <asp:BoundField DataField="M_Jurisdiction_ModifiedOn" HeaderText="Modified Date" />

                                                                    <asp:TemplateField HeaderText="View">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClientClick="validateSave(this);" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.col -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 800px; margin-left: -32px">
                            <div class="row">
                                <div class="col-xs-12">

                                    <div class="table-header">
                                        <asp:Label ID="lblHeader" runat="server" Text="Create / Update Jurisdiction" CssClass="control-label no-padding-right"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="dvPJ" runat="server">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblParentJurisdiction" runat="server" Text="Parent Jurisdiction*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:DropDownList ID="ddlParentJurisdiction" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlParentJurisdiction_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lblJurisdictionName" runat="server" Text="Jurisdiction Name*" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtJurisdictionName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lblJurisdictionCode" runat="server" Text="Jurisdiction Code" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtJurisdictionCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" Text="Save" OnClientClick="return ValidateJurisdiction(this);" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                            &nbsp; &nbsp; &nbsp;
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-warning"></asp:Button>&nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn"></asp:Button>
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="hf_M_Jurisdiction_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_dt_Options_Dynamic" runat="server" Value="" />
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



