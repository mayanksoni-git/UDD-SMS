<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="RptCrematoriumDetail.aspx.cs" Inherits="RptCrematoriumDetail" EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        -webkit-animation-duration: 0.6s;
        animation-name: zoom;
        animation-duration: 0.6s;
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
                                    <h4 class="mb-sm-0">Crematorium Detail</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">MIS</li>
                                            <li class="breadcrumb-item active">Crematorium Detail</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Crematorium Detail</h4>
                                        <a class="btn btn-primary" href="CrematoriumDetail.aspx">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-database-fill-add" viewBox="0 0 16 16">
                                                <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0M8 1c-1.573 0-3.022.289-4.096.777C2.875 2.245 2 2.993 2 4s.875 1.755 1.904 2.223C4.978 6.711 6.427 7 8 7s3.022-.289 4.096-.777C13.125 5.755 14 5.007 14 4s-.875-1.755-1.904-2.223C11.022 1.289 9.573 1 8 1" />
                                                <path d="M2 7v-.839c.457.432 1.004.751 1.49.972C4.722 7.693 6.318 8 8 8s3.278-.307 4.51-.867c.486-.22 1.033-.54 1.49-.972V7c0 .424-.155.802-.411 1.133a4.51 4.51 0 0 0-4.815 1.843A12 12 0 0 1 8 10c-1.573 0-3.022-.289-4.096-.777C2.875 8.755 2 8.007 2 7m6.257 3.998L8 11c-1.682 0-3.278-.307-4.51-.867-.486-.22-1.033-.54-1.49-.972V10c0 1.007.875 1.755 1.904 2.223C4.978 12.711 6.427 13 8 13h.027a4.55 4.55 0 0 1 .23-2.002m-.002 3L8 14c-1.682 0-3.278-.307-4.51-.867-.486-.22-1.033-.54-1.49-.972V13c0 1.007.875 1.755 1.904 2.223C4.978 15.711 6.427 16 8 16c.536 0 1.058-.034 1.555-.097a4.5 4.5 0 0 1-1.3-1.905" />
                                            </svg> Add New</a>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divCircle" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblYear" runat="server" Text="Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select">
                                                            <asp:ListItem Value="0">-select year-</asp:ListItem>
                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                            <asp:ListItem Value="2026">2026</asp:ListItem>
                                                            <asp:ListItem Value="2027">2027</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblMonth" runat="server" Text="Month*" CssClass="form-label"></asp:Label>p
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Crematorium Detail</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <!-- div.table-responsive -->
                                                    <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                    <!-- div.dataTables_borderWrap -->
                                                    <div style="overflow: auto">
                                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnPrintA4" runat="server" Text="Export in A4"  CssClass="btn btn-primary" OnClientClick="PrintCrematoriumDetail(); return false;" />
                                                        <%--Same grid is used on Page ExportToExcelCrematoriumDetail.aspx and PrintCrematoriumDetail.aspx page also, please change there as well if needed--%>
                                                        <asp:GridView ID="gvCrematoriumDetail" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                            <Columns> 
                                                                <asp:BoundField DataField="CrematoriumDetail_Id" HeaderText="Crematorium Detail Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                
                                                                <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Zone" DataField="ZoneName" />
                                                                <asp:BoundField HeaderText="District" DataField="CircleName" />
                                                                <asp:BoundField HeaderText="ULB" DataField="DivisionName" />
                                                                <asp:BoundField HeaderText="Year" DataField="Year" />
                                                                <asp:BoundField HeaderText="Month" DataField="MonthName" />
                                                                <asp:BoundField HeaderText="Name of Crematorium" DataField="NameCMTR" />
                                                                
                                                                <asp:TemplateField HeaderText="Crematorium Image">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgCMTR" Style="width: 85px; height: 61px;" ImageUrl='<%# Eval("CMTRImage") %>' runat="server" 
                                                                            OnClientClick='<%# "showImageModal(\"" + Eval("CMTRImage") + "\"); return false;" %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField HeaderText="Location of Crematorium" DataField="LocationCMTR" />
                                                                <asp:BoundField HeaderText="Conventional + Improvised Wood + Gas + Electric = Total No of Pyres in Crematorium" DataField="PyresDetail" />

                                                                <asp:TemplateField HeaderText="Drinking Water Facility">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("DrinkingWater")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Is the facility connected to Electricity Grid">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("ElecticityGrid")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Parking Space">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("Parking")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Shed for pyres">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("Shed")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Hearse">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("Hearse")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Hand Pump">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("HandPump")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Boundary Wall">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("BoundaryWall")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Waiting/Prayer Hall">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("PrayerHall")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Office/Care Taker Room">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("CareTakerRoom")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Ash Storage Rack">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("AshStorage")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Bathroom for Bathing">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("Bathrooms")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Is Washroom facility available ?">
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToBoolean(Eval("Washroom")) ? "Yes" : "No" %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Does Registration Happens ?">
                                                                    <ItemTemplate>
                                                                        <%# 
                                                                            Eval("Registration").ToString() == "1" ? "Yes" : 
                                                                            Eval("Registration").ToString() == "2" ? "Other" : 
                                                                            Eval("Registration").ToString() == "0" ? "No" : "Unknown"
                                                                        %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Name of Filler" DataField="FillerName" />
                                                                <asp:BoundField HeaderText="Contact No of Filler" DataField="FillerContact" />
                                                                <asp:BoundField HeaderText="Total no. of Cremations done in from 1/Jan/2023 - 31/Dec/2023 " DataField="TotalCMTRDone" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
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
        function PrintCrematoriumDetail() {
            window.open('PrintCrematoriumDetail.aspx', '_blank');
        }
    </script>
    
    <script type="text/javascript">
        function showImageModal(imageUrl) {
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
</asp:Content>



