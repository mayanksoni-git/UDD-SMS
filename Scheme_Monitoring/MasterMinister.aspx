<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="~/MasterMinister.aspx.cs" Inherits="MasterMinister" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Minister Master</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Jurisdiction Masters</li>
                                            <li class="breadcrumb-item active">Minister</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="col-xs-12">
                                    <div class="clearfix">
                                        <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Create New" CssClass="btn-filter mb-2"></asp:Button>
                                    </div>
                                </div>

                                <div id="divCreateNew" runat="server" visible="false" class="card">
                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-xs-12">

                                                <div class="table-header">
                                                    <h3 style="font-weight: bold; font-size: large">Create / Update Minister</h3>
                                                    <hr />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblName" CssClass="control-label no-padding-right" Text="Name" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblPartyH" runat="server" Text="Party" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlParties" runat="server" CssClass="form-select mb-2"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblMinistry" CssClass="control-label no-padding-right" Text="Ministry" runat="server"></asp:Label>
                                                    <asp:TextBox ID="TxtMinistry" runat="server" CssClass="form-control" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblMinisterType" runat="server" Text="Minister Type" CssClass="control-label no-padding-right"></asp:Label>
                                                    <asp:DropDownList ID="ddlMinisterType" runat="server" CssClass="form-select mb-2">
                                                        <asp:ListItem Value="0">-Select Minister Type-</asp:ListItem>
                                                        <asp:ListItem Value="1">CM</asp:ListItem>
                                                        <asp:ListItem Value="2">Deputy CM</asp:ListItem>
                                                        <asp:ListItem Value="3">Cabinet Minister</asp:ListItem>
                                                        <asp:ListItem Value="4">Minister of State</asp:ListItem>
                                                        <asp:ListItem Value="5">Minister of State(Independed Charge)</asp:ListItem>
                                                        <asp:ListItem Value="6">Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblFromDate" runat="server" Text="From date" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblToDate" runat="server" Text="To date" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblRemark" CssClass="control-label no-padding-right" Text="Ramarks" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblActiveMinister" CssClass="control-label no-padding-right" Text="Is Active Minister" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chkIsActiveMinister" runat="server" CssClass="form-control mb-2" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="d-block">&nbsp;</label>
                                                    <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn bg-danger text-white"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                  </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                            <Columns>

                                                <asp:BoundField DataField="UPMinisterID" HeaderText="Minister ID">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PartyId" HeaderText="PartyId">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MinisterTypeId" HeaderText="MinisterTypeId">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>

                                               <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkUpdate" runat="server" CommandName="UpdateRow" ImageUrl="~/assets/images/edit.png" OnClick="lnkUpdate_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UPMinisterName" HeaderText="Minister Name" />
                                                <asp:BoundField DataField="PartyName" HeaderText="Party Name" />
                                                <asp:BoundField DataField="MinistryName" HeaderText="Ministry Name" />
                                                <asp:BoundField DataField="FromDate" HeaderText="Appointed From" />
                                                <asp:BoundField DataField="ToDate" HeaderText="Appointed To" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                <asp:BoundField DataField="IsActive" HeaderText="IsActive">
                                                 <%--   <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />--%>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Delete">
                                                     <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" Width="20px" Height="20px" OnClick="btnDelete_Click" ImageUrl="~/assets/images/delete.png" runat="server" />
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

                    <asp:HiddenField ID="hf_MinisterId" runat="server" Value="0" />
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
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= txtFromDate.ClientID %>").datepicker({
                dateFormat: "yy-mm-dd",  // You can customize the format here
                changeMonth: true,
                changeYear: true
            });

            $("#<%= txtToDate.ClientID %>").datepicker({
                dateFormat: "yy-mm-dd",  // You can customize the format here
                changeMonth: true,
                changeYear: true
            });
        });
</script>
</asp:Content>



