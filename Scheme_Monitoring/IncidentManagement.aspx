<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="IncidentManagement.aspx.cs" Inherits="IncidentManagement" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .incident-form {
            background-color: #f8f9fa;
            border-radius: 5px;
            padding: 20px;
            margin-bottom: 20px;
        }
        .party-table {
            margin-top: 20px;
        }
        .hidden {
            display: none;
        }
    </style>

    <div class="main-content">
        <div class="page-content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 mb-3">
                        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                            <h4 class="mb-sm-0">Incident Management</h4>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="upIncident" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Incident Details</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="incident-form">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">District *</label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select" 
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">ULB *</label>
                                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Name of Chairman *</label>
                                                        <asp:TextBox ID="txtChairmanName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Remind in Days *</label>
                                                        <asp:DropDownList ID="ddlRemindDays" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="3 Days" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="7 Days" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="15 Days" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="30 Days" Value="30"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-12">
                                                    <h5>Add Parties to Incident</h5>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Designation *</label>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="Chairman" Value="Chairman"></asp:ListItem>
                                                            <asp:ListItem Text="EO" Value="EO"></asp:ListItem>
                                                            <asp:ListItem Text="AR" Value="AR"></asp:ListItem>
                                                            <asp:ListItem Text="JE" Value="JE"></asp:ListItem>
                                                            <asp:ListItem Text="Clerk" Value="Clerk"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">Name of Party *</label>
                                                        <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">&nbsp;</label>
                                                        <asp:Button ID="btnAddParty" runat="server" Text="Add Party" 
                                                            CssClass="btn btn-primary" OnClick="btnAddParty_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-12">
                                                    <div class="table-responsive party-table">
                                                        <asp:GridView ID="gvParties" runat="server" CssClass="table table-bordered" 
                                                            AutoGenerateColumns="false" EmptyDataText="No parties added yet"
                                                            OnRowCommand="gvParties_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                <asp:BoundField DataField="PartyName" HeaderText="Name" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnRemoveParty" runat="server" CommandName="RemoveParty" 
                                                                            CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-danger">
                                                                            Remove
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-12 text-center">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success me-2" 
                                                        OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary me-2" 
                                                        Visible="false" OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" 
                                                        OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mt-4">
                                            <div class="col-12">
                                                <div class="card">
                                                    <div class="card-header align-items-center d-flex">
                                                        <h4 class="card-title mb-0 flex-grow-1">Incident List</h4>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvIncidents" runat="server" CssClass="table table-bordered" 
                                                                AutoGenerateColumns="false" EmptyDataText="No incidents found"
                                                                OnRowCommand="gvIncidents_RowCommand" DataKeyNames="Incident_Id">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                                                    <asp:BoundField DataField="ULBName" HeaderText="ULB" />
                                                                    <asp:BoundField DataField="ChairmanName" HeaderText="Chairman" />
                                                                    <asp:BoundField DataField="RemindInDays" HeaderText="Remind In (Days)" />
                                                                    <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditIncident" 
                                                                                CommandArgument='<%# Eval("Incident_Id") %>' CssClass="btn btn-sm btn-primary">
                                                                                Edit
                                                                            </asp:LinkButton>
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
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:HiddenField ID="hfIncidentId" runat="server" Value="0" />
            </div>
        </div>
    </div>
</asp:Content>