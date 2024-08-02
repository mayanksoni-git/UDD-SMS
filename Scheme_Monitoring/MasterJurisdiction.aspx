<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterJurisdiction.aspx.cs" Inherits="MasterJurisdiction" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" enablepartialrendering="true" enablepagemethods="true" asyncpostbacktimeout="6000">
            </cc1:toolkitscriptmanager>
            <asp:UpdatePanel ID="up" runat="server">
                <contenttemplate>
                    <cc1:modalpopupextender id="mp1" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShowPopup"
                        cancelcontrolid="btnclose" backgroundcssclass="modalBackground1">
                    </cc1:modalpopupextender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12 ">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">
                                        <asp:Label ID="lblMainHeader" runat="server" Text="Master Jurisdiction"></asp:Label>
                                    </h4>
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
                                                    <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
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


                                                        <div style="overflow: auto" class="mt-3">
                                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                                <columns>
                                                                    <asp:BoundField DataField="M_Jurisdiction_Id" HeaderText="M_Jurisdiction_Id">
                                                                        <headerstyle cssclass="displayStyle" />
                                                                        <itemstyle cssclass="displayStyle" />
                                                                        <footerstyle cssclass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Parent_Jurisdiction_Id" HeaderText="Parent_Jurisdiction_Id">
                                                                        <headerstyle cssclass="displayStyle" />
                                                                        <itemstyle cssclass="displayStyle" />
                                                                        <footerstyle cssclass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <itemtemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </itemtemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="Parent_Jurisdiction_Name_Eng" HeaderText="Parent Jurisdiction" />
                                                                    <asp:BoundField DataField="Jurisdiction_Name_Eng" HeaderText="Jurisdiction Name" />

                                                                    <asp:BoundField DataField="Jurisdiction_Code" HeaderText="Jurisdiction Code" />
                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                                    <asp:BoundField DataField="Created_Date" HeaderText="Created Date" />
                                                                    <asp:BoundField DataField="ModifyBy" HeaderText="Modified By" />
                                                                    <asp:BoundField DataField="M_Jurisdiction_ModifiedOn" HeaderText="Modified Date" />

                                                                    <asp:TemplateField HeaderText="View">
                                                                        <itemtemplate>
                                                                            <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClientClick="validateSave(this);" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                                        </itemtemplate>
                                                                    </asp:TemplateField>
                                                                </columns>
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
                                        <asp:Label ID="lblHeader" runat="server" Text="Create / Update Jurisdiction" CssClass="control-label no-padding-right" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                        <hr />
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4" id="dvPJ" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblParentJurisdiction" runat="server" Text="Parent Jurisdiction*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlParentJurisdiction" runat="server" class="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlParentJurisdiction_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblJurisdictionName" runat="server" Text="Jurisdiction Name*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtJurisdictionName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblJurisdictionCode" runat="server" Text="Jurisdiction Code" CssClass="control-label no-padding-right "></asp:Label>
                                        <asp:TextBox ID="txtJurisdictionCode" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group mt-2">
                                        <asp:Button ID="btnSave" Text="Save" OnClientClick="return ValidateJurisdiction(this);" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"></asp:Button>
                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-danger"></asp:Button>
                                        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn bg-info"></asp:Button>
                                        <button id="btnclose" runat="server" text="Close" cssclass="btn bg-warning" style="display: none"></button>
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
                <progresstemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </progresstemplate>
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



