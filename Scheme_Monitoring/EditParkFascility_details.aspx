<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="EditParkFascility_details.aspx.cs" Inherits="EditParkFascility_details" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">

                         <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Edit Park Fascilities</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">COSTRUCTION OF PARK</li>
                                            <li class="breadcrumb-item active">Edit Park Fascilities</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                          <div class="row">
                            <div class="col-lg-12">
                                <div class="card" id="sectionFilter" runat="server">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Edit Park Fascilities</h4>

                                    </div>
                                    <!-- end card header -->
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
                                                            <asp:FileUpload ID="Geotagged_Photos" runat="server" CssClass="form-control" />
                                                            <%--<asp:Label ID="lblGeotaggedPhotosview" runat="server"  CssClass="form-label"></asp:Label>--%>
                                                            <asp:LinkButton runat="server" id="linkViewFile" href='<%# Eval("Geotagged_Photos").ToString().Replace("~", "") %>' target="_blank">View File</asp:LinkButton>
                                                                            </ItemTemplate>

                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <asp:Label ID="lblEventsOrganised" runat="server" Text="Events Organised in Parks*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtEventsOrganised" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xxl-3 col-md-6 mt-4 ">
                                                <asp:Button ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                

                            </div>
                        </div>


                       
                    </ContentTemplate>
                  <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <%--<asp:PostBackTrigger ControlID="exportToExcel" />--%>
                </Triggers>

                </asp:UpdatePanel>
          

            </div>
        </div>
    
    </asp:Content>
