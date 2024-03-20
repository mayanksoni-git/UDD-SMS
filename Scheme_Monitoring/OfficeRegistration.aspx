<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="OfficeRegistration.aspx.cs" Inherits="OfficeRegistration" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <%-- code here --%>
                        <div class="row gy-4">
                            <div class="card-body">
                                <div class="live-preview">
                                    <div class="row gy-4">
                                        <div class="col-xxl-4 col-md-6">
                                            <asp:Label ID="lblCenterName" runat="server" Text="Organization Name" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtOfficeBranchName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblRegNo" runat="server" Text="Organization Type" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:RadioButtonList ID="rbtOrgType" runat="server" RepeatColumns="2">
                                                    <asp:ListItem Selected="True" Text="Goverment" Value="Goverment"></asp:ListItem>
                                                    <asp:ListItem Text="Directorate" Value="Directorate"></asp:ListItem>
                                                    <asp:ListItem Text="Board" Value="Board"></asp:ListItem>
                                                    <asp:ListItem Text="Nigam" Value="Nigam"></asp:ListItem>
                                                    <asp:ListItem Text="Mission" Value="Mission"></asp:ListItem>
                                                    <asp:ListItem Text="Society" Value="Society"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblLandLineNo" runat="server" Text="Organization Land Line No" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtLandLineNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblMobileNo" runat="server" Text="Organization Mobile No" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblAddress" runat="server" Text="Organization Address" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblEmailId" runat="server" Text="Organization E-mail ID" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblGSTN" runat="server" Text="GSTIN No" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:TextBox ID="txtGSTN" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblState" runat="server" Text="State" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-xxl-4 col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblDistrict" runat="server" Text="District" CssClass="control-label no-padding-right"></asp:Label>
                                                <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..."></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Organisation Other Details</h4>

                            </div>


                            <div class="row gy-4">

                                <div class="col-xxl-4 col-md-6">

                                    <asp:FileUpload ID="flLogo" runat="server" />
                                </div>
                                <div class="col-xxl-4 col-md-6">
                                    <ul class="list-unstyled mb-0" id="dropzone-preview">
                                        <asp:Image ID="imgPreview" Width="50px" Height="50px" runat="server" />
                                    </ul>
                                </div>
                                <!-- end dropzon-preview -->
                            </div>

                            <div class="text-center">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-primary"></asp:Button>
                                    &nbsp; &nbsp; &nbsp;
                                          <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn btn-danger"></asp:Button>
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
                                                <h4 class="card-title mb-0 flex-grow-1">Organisation Registration</h4>

                                            </div>
                                            <!-- div.table-responsive -->
                                            <!-- div.dataTables_borderWrap -->
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-primary" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:BoundField DataField="OfficeBranch_Id" HeaderText="OfficeBranch_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="State_Id" HeaderText="State_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OfficeBranch_JurisdictionId" HeaderText="OfficeBranch_JurisdictionId">
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
                                                                <asp:ImageButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" ImageUrl="~/assets/images/edit.png" Width="20px" Height="20px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField HeaderText="State" DataField="State_Name" />
                                                        <asp:BoundField HeaderText="District" DataField="District" />
                                                        <asp:BoundField HeaderText="Organization Type" DataField="OfficeBranch_RegistrationNo" />
                                                        <asp:BoundField HeaderText="Organization Name" DataField="OfficeBranch_Name" />
                                                        <asp:BoundField HeaderText="Land Line" DataField="OfficeBranch_LL" />
                                                        <asp:BoundField HeaderText="Mobile" DataField="OfficeBranch_Mobile" />
                                                        <asp:BoundField HeaderText="FullAddress" DataField="OfficeBranch_FullAddress" />
                                                        <asp:BoundField HeaderText="GSTIN" DataField="OfficeBranch_GSTIN" />
                                                        <asp:BoundField HeaderText="EmailId" DataField="OfficeBranch_EmailId" />
                                                        <asp:BoundField HeaderText="WebSite" DataField="OfficeBranch_WebSite">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PANNumber" DataField="OfficeBranch_PANNumber">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="OfficeBranch_ARV_Formula" DataField="OfficeBranch_ARV_Formula">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="OfficeBranch_Logo" DataField="OfficeBranch_Logo">
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
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_OfficeBranch_Id" runat="server" Value="0" />
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
</asp:Content>
