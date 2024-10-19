<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="AdoptedParkDetail.aspx.cs" Inherits="AdoptedParkDetail" EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">ADD PARK DETAIL</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div id="fieldsContainer">
                                                <div class="fieldGroup">
                                                    <div class="row gy-4">
                                                        <asp:HiddenField ID="Id" runat="server"/>
                                                         <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="Label1" runat="server" Text="Park Name" CssClass="form-label"></asp:Label>
                                                          <asp:TextBox ID="txtadoptedparkname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                      <%--  <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblMandal" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="control-label no-padding-right"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>--%>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblPlantedtreeName" runat="server" Text="Name of Trees Planted*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtPlantedtreeName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblSpeciesOftree" runat="server" Text="Species of Planted Trees*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtSpeciesOftree" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblFascility" runat="server" Text="Facility Available*" CssClass="form-label"></asp:Label>
                                                           <%-- <asp:TextBox ID="txFascility" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="txFascility" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="Select Facility" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Tracks" Value="Tracks"></asp:ListItem>
                                                            <asp:ListItem Text="Benches" Value="Benches"></asp:ListItem>
                                                            <asp:ListItem Text="Electricity" Value="Electricity"></asp:ListItem>
                                                            <asp:ListItem Text="Water Facility" Value="Water Facility"></asp:ListItem>
                                                            <asp:ListItem Text="Sinages" Value="Sinages"></asp:ListItem>
                                                            <asp:ListItem Text="Lights" Value="Lights"></asp:ListItem>
                                                            <asp:ListItem Text="Open Gym" Value="Open Gym"></asp:ListItem>
                                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblFascilityAdded" runat="server" Text="Facility Added*" CssClass="form-label"></asp:Label>
                                                            <%--<asp:TextBox ID="txtFascilityAdded" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                             <asp:DropDownList ID="txtFascilityAdded" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="Select Facility" Value=""></asp:ListItem>
                                                             <asp:ListItem Text="Tracks" Value="Tracks"></asp:ListItem>
                                                            <asp:ListItem Text="Benches" Value="Benches"></asp:ListItem>
                                                            <asp:ListItem Text="Electricity" Value="Electricity"></asp:ListItem>
                                                            <asp:ListItem Text="Water Facility" Value="Water Facility"></asp:ListItem>
                                                            <asp:ListItem Text="Sinages" Value="Sinages"></asp:ListItem>
                                                            <asp:ListItem Text="Lights" Value="Lights"></asp:ListItem>
                                                            <asp:ListItem Text="Open Gym" Value="Open Gym"></asp:ListItem>
                                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblNoofGardener" runat="server" Text="No. of Gardener*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtNoofGardener" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblFrequencyMaintenance" runat="server" Text="Frequency of Maintenance*" CssClass="form-label"></asp:Label>
                                                            <%--<asp:TextBox ID="txtFrequencyMaintenance" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="txtFrequencyMaintenance" runat="server" CssClass="form-select">
                                                            <asp:ListItem Text="Select Maintenance" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Cleaning" Value="Cleaning"></asp:ListItem>
                                                            <asp:ListItem Text="Grass Cutting" Value="Grass Cutting"></asp:ListItem>
                                                            <asp:ListItem Text="Pruning of Plants" Value="Pruning of Plants"></asp:ListItem>
                                                            <asp:ListItem Text="Watering" Value="Watering"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblGeotaggedPhotos" runat="server" Text="Geotagged Photos*" CssClass="form-label"></asp:Label>
                                                            <asp:FileUpload ID="fileUploadGeotaggedPhotos" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblEventsOrganised" runat="server" Text="Events Organised in Parks*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtEventsOrganised" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-xxl-6 col-md-6">
                                                <asp:Button ID="btnAddMore" runat="server" Text="Add More" CssClass="btn btn-primary" OnClientClick="addFieldGroup(); return false;" />
                                            </div>
                                            <div class="col-xxl-3 col-md-6">
                                                <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

  <script type="text/javascript">
      var i = 1;
      function addFieldGroup() {
          i++;
          var container = document.getElementById('fieldsContainer');
          var newFieldGroup = document.createElement('div');
          newFieldGroup.className = 'fieldGroup';
          newFieldGroup.innerHTML = `
            <table style='border:5px solid gray'>
                <tr>
                    <h3>Park Details ${i}</h3>
                    <div class="row gy-4">
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Name of Trees Planted*</label>
                            <input type="text" class="form-control" name="txtPlantedtreeName[]" />
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Species of Planted Trees*</label>
                            <input type="text" class="form-control" name="txtSpeciesOftree[]" />
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Facility Available*</label>
                             <select name="txFascility[]" Class="form-select" Id="txFascility[]">
                            <option Value="">Select Facility</option>
                            <option Value="Tracks">Tracks</option>
                            <option Value="Benches">Benches</option>
                            <option Value="Electricity">Electricity</option>
                            <option Value="Water Facility">Water Facility</option>
                            <option Value="Sinages">Sinages</option>
                            <option Value="Lights">Lights</option>
                            <option Value="Open Gym">Open Gym</option>
                            <option Value="Other">Other</option>
                        </select>
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Facility Added*</label>
                            
                        <select name="txtFascilityAdded[]" Id="txtFascilityAdded[]" Class="form-select">
                                    <option Value="">Select Added Facility</option>
                            <option Value="Tracks">Tracks</option>
                            <option Value="Benches">Benches</option>
                            <option Value="Electricity">Electricity</option>
                            <option Value="Water Facility">Water Facility</option>
                            <option Value="Sinages">Sinages</option>
                            <option Value="Lights">Lights</option>
                            <option Value="Open Gym">Open Gym</option>
                            <option Value="Other">Other</option>
                        </select>
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">No. of Gardener*</label>
                            <input type="text" class="form-control" name="txtNoofGardener[]" />
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Frequency of Maintenance*</label>
                        <select name="txtFrequencyMaintenance[]" Id="txtFrequencyMaintenance[]"  Class="form-select">
                            <option  Value="">Select Maintenance</option>
                            <option Value="Cleaning">Cleaning</option>
                            <option Value="Grass Cutting">Grass Cutting</option>
                            <option Value="Pruning of Plants">Pruning of Plants</option>
                            <option Value="Watering">Watering</option>
                        </select>
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Geotagged Photos*</label>
                            <input type="file" class="form-control" name="fileUploadGeotaggedPhotos[]" />
                        </div>
                        <div class="col-xxl-3 col-md-6">
                            <label class="form-label">Events Organised in Parks*</label>
                            <input type="text" class="form-control" name="txtEventsOrganised" />
                        </div>
                    </div>
                    <button type="button" class="btn btn-danger" onclick="removeFieldGroup(this)">Remove</button>
                </tr>
            </table>
        `;
          container.appendChild(newFieldGroup);
      }

      function removeFieldGroup(button) {
          var fieldGroup = button.closest('.fieldGroup'); // Navigate to the parent div of the button
          fieldGroup.remove(); // Remove the field group
      }
  </script>

</asp:Content>
