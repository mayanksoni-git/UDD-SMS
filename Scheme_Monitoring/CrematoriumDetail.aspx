<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="CrematoriumDetail.aspx.cs" Inherits="CrematoriumDetail" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <style>
    /* The Modal (background) */
    .modal {
        display: none;
        position: fixed;
        z-index: 1000;
        padding-top: 60px;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgb(0,0,0);
        background-color: rgba(0,0,0,0.9);
    }

    /* Modal Content (image) */
    .modal-content {
        margin: auto;
        display: block;
        width: 80%;
        max-width: 700px;
        transform: scale(1);
        transition: transform 0.25s ease;
    }

    /* Caption of Modal Image (Image Text) - Same Width as the Image */
    #caption {
        margin: auto;
        display: block;
        width: 80%;
        max-width: 700px;
        text-align: center;
        color: #ccc;
        padding: 10px 0;
        height: 150px;
    }

    /* Add Animation - Zoom in the Modal */
    .modal-content, #caption {  
        -webkit-animation-name: zoom;
        -webkit-animation-duration: 0.4s;
        animation-name: zoom;
        animation-duration: 0.4s;
    }

    @-webkit-keyframes zoom {
        from {transform:scale(0)} 
        to {transform:scale(1)}
    }

    @keyframes zoom {
        from {transform:scale(0.1)} 
        to {transform:scale(1)}
    }

    /* The Close Button */
    .close {
        position: absolute;
        top: 15px;
        right: 35px;
        color: #f1f1f1;
        font-size: 40px;
        font-weight: bold;
        transition: 0.3s;
    }

    .close:hover,
    .close:focus {
        color: #bbb;
        text-decoration: none;
        cursor: pointer;
    }

    /* Zoom Controls */
    .zoom-controls {
        text-align: center;
        margin-top: 10px;
    }

    .zoom-controls button {
        font-size: 18px;
        padding: 10px;
        margin: 0 5px;
        cursor: pointer;
    }
</style>
    <asp:HiddenField ID="hfCrematoriumDetail_Id" runat="server" />
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
                                        <h4 class="mb-sm-0">Create / Update Crematorium Detail</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">MIS</li>
                                                <li class="breadcrumb-item active">Crematorium Detail</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Basic Detail</h4>
                                            <a class="btn btn-primary" href="RptCrematoriumDetail.aspx">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-arrow-left-circle-fill" viewBox="0 0 16 16">
                                                    <path d="M8 0a8 8 0 1 0 0 16A8 8 0 0 0 8 0m3.5 7.5a.5.5 0 0 1 0 1H5.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L5.707 7.5z" />
                                                </svg>
                                                Back to Report</a>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-1 col-md-6">
                                                        <div id="div1" runat="server">
                                                            <asp:Label ID="lblYear" runat="server" Text="Year*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select">
                                                                <asp:ListItem Value="0">-Year-</asp:ListItem>
                                                                <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                <asp:ListItem Value="2027">2027</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-1 col-md-6">
                                                        <div id="div2" runat="server">
                                                            <asp:Label ID="lblMonth" runat="server" Text="Month*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="divZone" runat="server">
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="divCircle" runat="server">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="divDivision" runat="server">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div13" runat="server">
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="div18" runat="server">
                                                            <asp:Label ID="lblNameCMTR" runat="server" Text="Name of the Crematorium*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtNameCMTR" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div19" runat="server">
                                                            <asp:Label ID="lblLocationCMTR" runat="server" Text="Location of the Crematorium*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtLocationCMTR" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-6 col-md-6">
                                                        <div id="div12" runat="server">
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="div8" runat="server">
                                                            <asp:Label ID="lblConventional" runat="server" Text="Conventional Pyres*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtConventional" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateNoOfPyres" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="div9" runat="server">
                                                            <asp:Label ID="lblImprovisedWood" runat="server" Text="Improvised Wood Pyres*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtImprovisedWood" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateNoOfPyres" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="div10" runat="server">
                                                            <asp:Label ID="lblGas" runat="server" Text="Gas Pyres*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtGas" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateNoOfPyres" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="div11" runat="server">
                                                            <asp:Label ID="lblElectric" runat="server" Text="Electric Pyres*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtElectric" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateNoOfPyres" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-2 col-md-6">
                                                        <div id="div20" runat="server">
                                                            <asp:Label ID="lblNoOfPyres" runat="server" Text="Total No. of Pyres*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtNoOfPyres" runat="server" Enabled="False" Font-Bold="True" CssClass="form-control" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Details about the amenities</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblDrinkingWater" runat="server" Text="Drinking Water Facility*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblDrinkingWater" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblElecticityGrid" runat="server" Text="Is the facility connected to Electricity Grid*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblElecticityGrid" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblParking" runat="server" Text="Parking Space*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblParking" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblShed" runat="server" Text="Shed for Pyres*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblShed" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblHearse" runat="server" Text="Hearse" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblHearse" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblHandPump" runat="server" Text="Hand Pump*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblHandPump" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblBoundaryWall" runat="server" Text="Boundary Wall*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblBoundaryWall" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblEntryGate" runat="server" Text="Entry Gate*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblEntryGate" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblPrayerHall" runat="server" Text="Waiting Hall/Prayer Hall" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblPrayerHall" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblCareTakerRoom" runat="server" Text="Office/Care Taker Room*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblCareTakerRoom" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblAshStorage" runat="server" Text="Ash Storage Rack*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblAshStorage" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblBathrooms" runat="server" Text="Bathrooms for Bathing*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblBathrooms" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblWashroom" runat="server" Text="Is Washroom Facility Available?*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblWashroom" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Other Details</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <asp:Label ID="lblRegistration" runat="server" Text="Does Registration happens ?*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButtonList ID="rblRegistration" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                            <%--<asp:ListItem Value="2">Other</asp:ListItem>--%>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <asp:Label ID="lblCMTRImage" for="flCMTRImage" runat="server" Text="Add Image of Crematorium" CssClass="form-label"></asp:Label>
                                                            <asp:FileUpload ID="fuCMTRImage" CssClass="form-control" runat="server" onchange="showimagepreview(this)" />
                                                            <asp:HiddenField id="hfImageUrl" runat="server"/>
                                                        </div>
                                                    </div>

                                                    

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div4" runat="server">
                                                            <asp:Label ID="lblFillerName" runat="server" Text="Name of the person filling this form*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtFillerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div5" runat="server">
                                                            <asp:Label ID="lblFillerContact" runat="server" Text="Contact Number of the person filling this form*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtFillerContact" runat="server" CssClass="form-control" TextMode="Phone" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div3" runat="server">
                                                            <asp:Label ID="lblTotalCMTRDone" runat="server" Text="Total no. of Cremations done from 1/Jan/2023 - 31/Dec/2023 *" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtTotalCMTRDone" runat="server" CssClass="form-control"  onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div6" runat="server">
                                                            <asp:Label ID="lblCMTRImageDisplay" runat="server" Text="Uploaded Crematorium Image" CssClass="form-label" Visible="false"></asp:Label>
                                                            <asp:ImageButton ID="imgCMTRImageDisplay" Style="width: 100px; height: auto;" Visible="false" runat="server" OnClientClick="showImageModal(); return false;" />
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div7" runat="server">
                                                            <asp:Label ID="lblNewCMTRImage" runat="server" Text="Current Preview" CssClass="form-label"></asp:Label>
                                                            <asp:ImageButton ID="imgNewCMTRImage" ImageUrl="Images/CrematoriumImages/CrematoriumSample.jpg" Style="width: 100px;  height: auto;" OnClientClick="showNewImageModal(); return false;"  runat="server"  />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                            <div class="row">
                                <div class="col-xxl-12 col-md-12">
                                    <div>
                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" OnClick="btnUpdate_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                            <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                                <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            
        </div>
    </div>

    <div class="row">
        <div class="offset-lg-4 col-lg-4 mt-120">
            <div id="imageModal" class="modal" style="width: 100%; height: 100vh; z-index: 1500; display: none; margin: auto;">
                <span class="close" onclick="closeModal()">&times;</span>
                <img class="modal-content" id="modalImage" />
                <div id="caption"></div>
                <%--<div class="zoom-controls">
                    <button onclick="zoomIn()">+</button>
                    <button onclick="zoomOut()">-</button>
                </div>--%>
            </div>
        </div>
        
    </div>

    <script type="text/javascript">
        
        function showImageModal(imageUrl) {

            var hiddenField = document.getElementById('<%= hfImageUrl.ClientID %>');
            var imageUrl = hiddenField.value;
            var modal = document.getElementById("imageModal");
            var modalImg = document.getElementById("modalImage");
            modal.style.display = "block";
            modalImg.src = imageUrl.slice(2);
            modalImg.style.transform = "scale(1)";
        }

        function closeModal() {
            var modal = document.getElementById("imageModal");
            modal.style.display = "none";
        }

        function zoomIn() {
            var img = document.getElementById("modalImage");
            var modal = document.getElementById("imageModal");
            var currScale = getScale(img);
            img.style.transform = "scale(" + (currScale + 0.1) + ")";
            modal.style.display = "block";
        }

        function zoomOut() {
            var img = document.getElementById("modalImage");
            var modal = document.getElementById("imageModal");
            var currScale = getScale(img);
            if (currScale > 0.1) {
                img.style.transform = "scale(" + (currScale - 0.1) + ")";
                modal.style.display = "block";
            }
        }

        function getScale(element) {
            var st = window.getComputedStyle(element, null);
            var tr = st.getPropertyValue("transform");
            if (tr !== "none") {
                var values = tr.split('(')[1].split(')')[0].split(',');
                var scale = values[0];
                return parseFloat(scale);
            } else {
                return 1;
            }
        }
    </script>

    <script type="text/javascript">
        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    var imgControl = document.getElementById('<%= imgNewCMTRImage.ClientID %>');
                    imgControl.src = e.target.result;
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

        function showNewImageModal() {
            var imgControl = document.getElementById('<%= imgNewCMTRImage.ClientID %>');
            var imageUrl = imgControl.src;
            var modal = document.getElementById("imageModal");
            var modalImg = document.getElementById("modalImage");
            modal.style.display = "block";
            modalImg.src = imageUrl;
            modalImg.style.transform = "scale(1)";
        }
    </script>

    <script type="text/javascript">
        function downloadGO(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_GO_Path').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }
    </script>

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