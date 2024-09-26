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
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">CONSTRUCTION OF PARKS</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">MIS</li>
                                            <li class="breadcrumb-item active">CONSTRUCTION OF PARKS</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                              <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">PARK DETAIL</h4>
                                       <%-- <a class="btn btn-primary" href="CrematoriumDetail.aspx">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-database-fill-add" viewBox="0 0 16 16">
                                                <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0M8 1c-1.573 0-3.022.289-4.096.777C2.875 2.245 2 2.993 2 4s.875 1.755 1.904 2.223C4.978 6.711 6.427 7 8 7s3.022-.289 4.096-.777C13.125 5.755 14 5.007 14 4s-.875-1.755-1.904-2.223C11.022 1.289 9.573 1 8 1" />
                                                <path d="M2 7v-.839c.457.432 1.004.751 1.49.972C4.722 7.693 6.318 8 8 8s3.278-.307 4.51-.867c.486-.22 1.033-.54 1.49-.972V7c0 .424-.155.802-.411 1.133a4.51 4.51 0 0 0-4.815 1.843A12 12 0 0 1 8 10c-1.573 0-3.022-.289-4.096-.777C2.875 8.755 2 8.007 2 7m6.257 3.998L8 11c-1.682 0-3.278-.307-4.51-.867-.486-.22-1.033-.54-1.49-.972V10c0 1.007.875 1.755 1.904 2.223C4.978 12.711 6.427 13 8 13h.027a4.55 4.55 0 0 1 .23-2.002m-.002 3L8 14c-1.682 0-3.278-.307-4.51-.867-.486-.22-1.033-.54-1.49-.972V13c0 1.007.875 1.755 1.904 2.223C4.978 15.711 6.427 16 8 16c.536 0 1.058-.034 1.555-.097a4.5 4.5 0 0 1-1.3-1.905" />
                                            </svg> Add New</a>--%>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                
                                             
                                               <div class="col-xxl-3 col-md-6">
                                                    <div id="divMandal" runat="server">
                                                        <asp:Label ID="lblMandal" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="divCircle" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                               

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div12" runat="server">
                                                        <asp:Label ID="lblPlantedtreeName" runat="server" Text="Name of Trees Planted*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtPlantedtreeName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div13" runat="server">
                                                        <asp:Label ID="lblSpeciesOftree" runat="server" Text="Species of Planted Trees*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtSpeciesOftree" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div14" runat="server">
                                                        <asp:Label ID="lblFascility" runat="server" Text="Fascility Avaliable*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtFascility" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div15" runat="server">
                                                        <asp:Label ID="lblFascilityAdded" runat="server" Text="Fascility Added*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtFascilityAdded" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div16" runat="server">
                                                        <asp:Label ID="lblNoofGardener" runat="server" Text="No. of Gardener*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtNoofGardener" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                              
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="div17" runat="server">
                                                        <asp:Label ID="lblFrequencyMaintenance" runat="server" Text="Frequency of Maintenance*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtFrequencyMaintenance" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                               
                                                   <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblMonth" runat="server" Text="Month*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblGeotaggedPhotos" runat="server" Text="Geotagged Photos*" CssClass="form-label"></asp:Label>
                                                    <asp:FileUpload ID="fileUploadGeotaggedPhotos" runat="server" CssClass="form-control" />
                                                   
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblEventsOrganised" runat="server" Text="Events Organised in Parks*" CssClass="form-label"></asp:Label>
                                                      <asp:TextBox ID="txtEventsOrganised" runat="server" CssClass="form-control"></asp:TextBox>
                                                   
                                                </div>
                                         

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
         
        </div>
    </div>
    
</asp:Content>



