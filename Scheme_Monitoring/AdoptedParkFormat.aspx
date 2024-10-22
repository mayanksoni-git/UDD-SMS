<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="AdoptedParkFormat.aspx.cs" Inherits="AdoptedParkFormat" EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  
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
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">ADOPTED PARK</h4>
                                     <a href="AdoptedPark_Edit.aspx"  class="filter-btn" style="float:right;width:155px"><i class="icon-download"></i> Go To List</a>

                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                
                                              <%--  <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                 <asp:HiddenField ID="Id" runat="server" Value="0" />
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
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div1" runat="server">
                                                        <asp:Label ID="lblWard" runat="server" Text="Ward*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtWard" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="lblNoOfParkInULB" runat="server" Text="No of Park In ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtNoOfParkInULB" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="lblAdoptionInProcess" runat="server" Text="NO of park Adoption(Inprocess)*" CssClass="form-label"></asp:Label>
                                                    <asp:TextBox ID="txtAdoptionInProcess" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div10" runat="server">
                                                        <asp:Label ID="lblNoOfParkAdopted" runat="server" Text="No of Park Adopted*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtNoOfParkAdopted" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    </div>
                                               
                                            
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                              <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">ADOPTED PARK DETAILS</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <div class="col-xxl-12 col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="grdCallProductDtls" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true" OnPreRender="grdCallProductDtls_PreRender" OnRowDataBound="grdCallProductDtls_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Id" HeaderText="Id">
                                                                     <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                      </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name of Park Adopted*">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="AdoptedParkName" runat="server" CssClass="form-control" autocomplete="off" Text='<%# Eval("AdoptedParkName") %>' EnableViewState="true"  style="width: 300px;"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adopted Park Latitude*">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="ParkLatitude" runat="server" CssClass="form-control " Text='<%# Eval("ParkLatitude") %>' EnableViewState="true"  style="width: 100px;"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adopted Park Longitude*">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="ParkLongitude" runat="server" CssClass="form-control" Text='<%# Eval("ParkLongitude") %>' onkeyup="isNumericVal(this);" EnableViewState="true"  style="width: 100px;"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Year Of Adoption*">
                                                                            <ItemTemplate>
                                                                                      <asp:DropDownList ID="SessionId" runat="server" CssClass="form-select" EnableViewState="true"  style="width: 200px;">
                                                                                  
                                                                                </asp:DropDownList>
                                                                            <%--<asp:TextBox ID="SessionId" runat="server" CssClass="form-control" Text='<%# Eval("SessionId") %>' onkeyup="isNumericVal(this);"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Month*">
                                                                        <ItemTemplate>
                                                                             <asp:DropDownList ID="MonthId" runat="server" CssClass="form-select" EnableViewState="true"  style="width: 200px;">
                                                                                   
                                                                                </asp:DropDownList>
                                                                            <%--<asp:TextBox ID="MonthId" runat="server" CssClass="form-control" Text='<%# Eval("MonthId") %>' onkeyup="isNumericVal(this);"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                         <%--<FooterTemplate>
                                                                            <asp:ImageButton ID="btnDynamic" OnClick="btnDynamic_Click" runat="server" 
                                                                                ImageUrl="~/assets/images/add-icon.png" Width="30px" Height="30px" />
                                                                            <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" 
                                                                                ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                        </FooterTemplate>--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name of CSR/PPP/NGO/RWA others*">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="NameCSR_NGO" runat="server" CssClass="form-control" Text='<%# Eval("NameCSR_NGO") %>' EnableViewState="true"  style="width: 300px;"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Detail of the CSR/PPP/NGO/RWA/Others*">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="DetailCSR_NGO" runat="server" CssClass="form-control" Text='<%# Eval("DetailCSR_NGO") %>' EnableViewState="true"  style="width: 300px;"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Geotagged Photographs of Park*">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="GeotaggedPhotographs" runat="server" CssClass="form-control" EnableViewState="true" Text='<%# Eval("GeotaggedPhotographs") %>'/>
                                                                            <%--<asp:Label ID="lblGeotaggedPhotographs" runat="server" Text='<%# Eval("GeotaggedPhotographs") %>'></asp:Label>--%>
                                                                       <%--     <a href='<%# Eval("GeotaggedPhotographs").ToString().Replace("~", "") %>' target="_blank">View File </a>--%>
                                                                               <asp:LinkButton runat="server" id="linkViewFile" href='<%# Eval("GeotaggedPhotographs").ToString().Replace("~", "") %>' target="_blank" 
                                                                                  visible='<%# !string.IsNullOrEmpty(Eval("GeotaggedPhotographs") as string) %>'>View File</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                    </asp:TemplateField>
        
                                                                    <asp:TemplateField HeaderText="MOU Attached*">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="MOUAttached" runat="server" CssClass="form-control" EnableViewState="true"/>
                                                                            <%--<asp:Label ID="lblMOUAttached" runat="server" Text='<%# Eval("MOUAttached") %>'></asp:Label>--%>
                                                                            <%--<a href='<%# Eval("MOUAttached").ToString().Replace("~", "") %>' target="_blank">View File </a>--%>
                                                                               <asp:LinkButton runat="server" id="linkViewFile1" href='<%# Eval("MOUAttached").ToString().Replace("~", "") %>' target="_blank" 
                                                                                  visible='<%# !string.IsNullOrEmpty(Eval("MOUAttached") as string) %>'>View File</asp:LinkButton>
                                                                           </ItemTemplate>
                                                                       
                                                                    </asp:TemplateField> 
        
                                                                    <asp:TemplateField HeaderText="UploadKML">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="UploadKML" runat="server" CssClass="form-control" EnableViewState="true"/>
                                                                            <%--<asp:Label ID="lblUploadKML" runat="server" Text='<%# Eval("UploadKML") %>'></asp:Label>--%>
                                                                            <%--<a href='<%# Eval("UploadKML").ToString().Replace("~", "") %>' target="_blank">Download KML </a>--%>
                                                                           <asp:LinkButton runat="server" id="linkViewFile2" href='<%# Eval("UploadKML").ToString().Replace("~", "") %>' target="_blank" 
                                                                                  visible='<%# !string.IsNullOrEmpty(Eval("UploadKML") as string) %>'>View File</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                              
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <%--<asp:ImageButton ID="DeleteDetails" OnClick="DeleteDetails_Click" runat="server" ImageUrl="~/assets/images/delete.png" Width="25px" Height="25px" />--%>
                                                                         <asp:ImageButton ID="imgdeleteQuestionnaire" CssClass="pull-right" runat="server" 
                                                                                ImageUrl="~/assets/images/minus-icon.png" OnClick="imgdelete_Click" Width="30px" Height="30px" />
                                                                       
                                                                       </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <%--<FooterStyle Font-Bold="true" ForeColor="White" />--%>
                                                            </asp:GridView>
                                                            <div class="row">
                                                                <div class="col-sm-12" style="text-align:center">
                                                                    <asp:ImageButton ID="btnDynamic" OnClick="btnDynamic_Click" runat="server" 
                                                                                ImageUrl="~/assets/images/addmore_btn.png" Width="200px" Height="60px" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <div class="col-xxl-3 col-md-6">
                            <div>
                                <br />
                                <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"></asp:Button>
                            </div>
                        </div>
                            <!--end col-->
                              
                        </div>

                        
                    </div>
                </ContentTemplate>
                <Triggers>
     <asp:PostBackTrigger ControlID="btnSave" />
     <asp:PostBackTrigger ControlID="btnDynamic" />
                    <%--<asp:PostBackTrigger ControlID="btnDynamic" />--%>
                </Triggers>
            </asp:UpdatePanel>
         
        </div>
    </div>
  

</asp:Content>


