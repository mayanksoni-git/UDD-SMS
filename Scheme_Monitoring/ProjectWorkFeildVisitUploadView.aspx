<%@ Page Language="C#" MasterPageFile="~/TemplatePopup.master" AutoEventWireup="true" CodeFile="ProjectWorkFeildVisitUploadView.aspx.cs" Inherits="ProjectWorkFeildVisitUploadView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <div class="page-content">
                            <div class="row">
                                <div style="overflow: auto">
                                    <asp:GridView ID="grdPost1" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost1_PreRender" OnRowDataBound="grdPost1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectWork_ProjectType_Id" HeaderText="ProjectWork_ProjectType_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnView" Width="20px" Height="20px" OnClick="btnView_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                            <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                            <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                            <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                            <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                            <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                            <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                            <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                            <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                            <asp:BoundField HeaderText="Physical %" DataField="Physical_Progress" />
                                            <asp:BoundField HeaderText="Financial %" DataField="Financial_Progress" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Component Wise Breakup 
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="overflow: auto">
                                    <asp:GridView ID="grdPhysicalProgress" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPhysicalProgress_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="Component_Id" HeaderText="Component_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Component_Unit" HeaderText="Component" />
                                            <asp:BoundField DataField="Proposed" HeaderText="Proposed" />
                                            <asp:BoundField DataField="PhysicalProgress" HeaderText="Completed" />
                                            <asp:BoundField DataField="Functional" HeaderText="Functional" />
                                            <asp:BoundField DataField="NonFunctional" HeaderText="Non Functional" />
                                            <asp:BoundField DataField="Percentage_Cpmpleted" HeaderText="Completed Percentage" />
                                            <asp:BoundField DataField="Percentage_Cpmpleted_Functional" HeaderText="Completed Percentage (Functional)" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:DropDownList ID="ddlVisitsMade" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnGetVisitData" Text="Get Visit Details" OnClick="btnGetVisitData_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:ImageButton ID="btnAddComments" runat="server" ImageUrl="~/assets/images/comments.png" Width="80px" Height="80px" OnClick="btnAddComments_Click" Visible="false" ToolTip="Click To Add Comments.............">
                                            </asp:ImageButton>
                                        </div>
                                    </div>

                                    <div class="col-md-3" id="divMap" runat="server" visible="false">
                                        <div class="form-group" id="divMap1" runat="server">
                                            <br />
                                            <a onclick="return openPopup(this);" role="button" class="bigger bg-primary white" data-toggle="modal">&nbsp;Map 
                                            </a>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdVisitDetails" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdVisitDetails_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectUC_Concent_Id" HeaderText="ProjectUC_Concent_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectUC_Concent_Type" HeaderText="Component" />
                                                    <asp:BoundField DataField="ProjectUC_Concent_Question" HeaderText="Question" />
                                                    <asp:BoundField DataField="ProjectUC_Concent_Answer" HeaderText="Response" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div id="divVisitPics" class="row" runat="server" visible="false">
                                <div class="row">
                                    <table class="display table table-bordered no-margin-bottom no-border-top">
                                        <thead>
                                            <tr>
                                                <th>Description</th>
                                                <th>Response</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td runat="server" id="Q30">Upload Photo 1:</td>
                                                <td>
                                                    <asp:FileUpload ID="flAnnexture1" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="Q31">Upload Photo 2:</td>
                                                <td>
                                                    <asp:FileUpload ID="flAnnexture2" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="Q32">Upload Photo 3:</td>
                                                <td>
                                                    <asp:FileUpload ID="flAnnexture3" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="Q33">Upload High Quality Video Clip (Max 500 MB):</td>
                                                <td>
                                                    <asp:FileUpload ID="flVideo" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="Q34">Upload Inspection Report (In PDF File):</td>
                                                <td>
                                                    <asp:FileUpload ID="flInspectionReport" runat="server" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnUpload" Text="Upload Documents" OnClick="btnUpload_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdSitePics" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdSitePics_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectPkgSitePics_Id" HeaderText="ProjectPkgSitePics_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path1" HeaderText="ProjectPkgSitePics_SitePic_Path1">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path2" HeaderText="ProjectPkgSitePics_SitePic_Path2">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path3" HeaderText="ProjectPkgSitePics_SitePic_Path3">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path4" HeaderText="ProjectPkgSitePics_SitePic_Path4">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <br />
                                                            <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" runat="server" ImageUrl="~/assets/images/edit.png" OnClick="btnEdit_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectPkgSitePics_ComponentName" HeaderText="Component" />
                                                    <asp:TemplateField HeaderText="Site Pic 1">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img1" Width="200px" Height="200px" runat="server" ImageUrl='<%# Eval("ProjectPkgSitePics_SitePic_Path1") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site Pic 2">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img2" Width="200px" Height="200px" runat="server" ImageUrl='<%# Eval("ProjectPkgSitePics_SitePic_Path2") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site Pic 3">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img3" Width="200px" Height="200px" runat="server" ImageUrl='<%# Eval("ProjectPkgSitePics_SitePic_Path3") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Video">
                                                        <ItemTemplate>
                                                            <video width="200" height="200" controls>
                                                                <source src='<%# Eval("ProjectPkgSitePics_SitePic_Path4") %>' type="video/mp4">
                                                            </video>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inspection Report">
                                                        <ItemTemplate>
                                                            <a href="<%# Eval("ProjectPkgSitePics_SitePic_Path5") %>" target="_blank">View Report</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="700px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Previous Comments and Reply..............
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-12">
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdComments" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdComments_PreRender">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectVisitComment_Id" HeaderText="ProjectVisitComment_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="FeildVisitL1_Name" HeaderText="Category L1" />
                                                    <asp:BoundField DataField="FeildVisitL2_Name" HeaderText="Category L2" />
                                                    <asp:BoundField DataField="ProjectVisitComment_Comments" HeaderText="Comment / Reply" />
                                                    <asp:BoundField DataField="Designation_DesignationName" HeaderText="Designation" />
                                                    <asp:BoundField DataField="ProjectVisitComment_AddedOn" HeaderText="Added On" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Add New Comment or Reply..............
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Field Visit L1* </label>
                                            <asp:DropDownList ID="ddlFeildVisitL1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFeildVisitL1_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Field Visit L1* </label>
                                            <asp:DropDownList ID="ddlFeildVisitL2" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <br />
                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSaveComments" Text="Add Comments" OnClick="btnSaveComments_Click" runat="server" CssClass="btn btn-inverse"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div id="UpdatePopup" class="modal fade" tabindex="-1" style="padding-left: 0px;">
                            <div class="modal-dialog" style="margin-top: 0px; margin-left: 150px; width: 1000px;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h3 class="smaller lighter blue no-margin">View Site Location</h3>
                                    </div>

                                    <div class="modal-body" style="height: 600px;">
                                        <div id="map" style="width: 100%; height: 500px"></div>
                                        <br />
                                        <div id="mapaddress"></div>
                                    </div>

                                    <div class="modal-footer">
                                        <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                                            <i class="ace-icon fa fa-times"></i>
                                            Close
                                       
                                        </button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_ProjectVisit_Id" Value="0" runat="server" />
                        <asp:HiddenField ID="hf_VType" Value="0" runat="server" />
                        <asp:HiddenField ID="hf_ProjectWork_Id" Value="0" runat="server" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
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


    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAtesZxiIJNhGcfDmtkQRs_OuSmCPUH9f4&sensor=false">  
    </script>

    <script src="Scripts/jquery-1.11.2.js"></script>
    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD1KxtLqZtakEaBbSMouMIPl5tDKUz0IqM"></script>--%>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

    <script type="text/javascript">  
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            //alert("There is Some Problem on your current browser to get Geo Location!");
        }

        function success(position) {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
            $('#ctl00_ContentPlaceHolder1_hf_Location').val(lat + "|" + long);
            $('#ctl00_ContentPlaceHolder1_lblLat').val(lat);
            $('#ctl00_ContentPlaceHolder1_lblLong').val(long);
            //var geocoder = new google.maps.Geocoder;
            //geocodeLatLng(geocoder, lat, long);

            var latlng = new google.maps.LatLng(lat, long);
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        $('#ctl00_ContentPlaceHolder1_lblAddress').val(results[1].formatted_address);
                        $('#ctl00_ContentPlaceHolder1_hf_Address').val(results[1].formatted_address);
                    }
                }
            });
        }

        function geocodeLatLng(geocoder, lat, lng) {
            var latlng = { lat: parseFloat(lat), lng: parseFloat(lng) };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        $('#ctl00_ContentPlaceHolder1_lblAddress').val(results[0].formatted_address);
                        $('#ctl00_ContentPlaceHolder1_hf_Address').val(results[0].formatted_address);
                    } else {
                        window.alert('No results found');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }
            });
        }
    </script>

    <script type="text/javascript">
        var _lst = [];
        var _lsttemp = [];
        var _lat;
        var _lng;
        var _address;
        function openPopup(obj) {
            getMapData(obj);
            obj.href = "#UpdatePopup";
            return false;
        }

        function getMapData(obj) {
            _lst = [];
            try {
                _lat = obj.attributes.lat.nodeValue;
                _lng = obj.attributes.long.nodeValue;
                _address = 'Site Location';
                document.getElementById('mapaddress').innerHTML = _address;
                var _obj = {};
                _obj.lat = parseFloat(_lat);
                _obj.lng = parseFloat(_lng);
                _obj.Created_date = "";
                _lst.push(_obj);
                if (_lst.length > 0)
                    initMap();
            }
            catch
            {

            }
        }

        var marker;
        getMapData();

        function initMap() {
            // debugger;
            var a = 0;
            var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
            var labelIndex = 0;

            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 18,
                center: { lat: parseFloat(_lat), lng: parseFloat(_lng) },
                mapTypeId: 'terrain'
            });

            var lineSymbol = {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 1,
                strokeColor: '#393'
            };

            var endPoint = {
                path: 'M17.070 2.93c-3.906-3.906-10.234-3.906-14.141 0-3.906 3.904-3.906 10.238 0 14.14 0.001 0 7.071 6.93 7.071 14.93 0-8 7.070-14.93 7.070-14.93 3.907-3.902 3.907-10.236 0-14.14zM10 14c-2.211 0-4-1.789-4-4s1.789-4 4-4 4 1.789 4 4-1.789 4-4 4z',
                fillOpacity: 1,
                scale: 1.5,
                strokeColor: '#008000',
                //strokeWeight: 8
            };

            var startPoint = {
                path: 'M17.070 2.93c-3.906-3.906-10.234-3.906-14.141 0-3.906 3.904-3.906 10.238 0 14.14 0.001 0 7.071 6.93 7.071 14.93 0-8 7.070-14.93 7.070-14.93 3.907-3.902 3.907-10.236 0-14.14zM10 14c-2.211 0-4-1.789-4-4s1.789-4 4-4 4 1.789 4 4-1.789 4-4 4z',
                fillOpacity: 0.8,
                scale: 1.5,
                // strokeWeight: 10
                strokeColor: '#1e90ff'
            };

            var flightPlanCoordinates = new Array();
            for (var i = 0; i < _lst.length; i++) {
                flightPlanCoordinates.push('lat: ' + _lst[i].lat + ', lng: ' + _lst[i].lng + '');

                if (i == 0) {
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        icon: startPoint,
                        //label: labels[labelIndex++ % labels.length],
                        animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,

                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Time Stamp: ' + _lst[i].Created_date
                    });
                    marker.addListener('click', toggleBounce);

                }
                else if (i == _lst.length - 1) {
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        icon: endPoint,
                        animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,
                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Time Stamp: ' + _lst[i].Created_date
                    });
                    marker.addListener('click', toggleBounce);

                }
                else {
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        label: labels[labelIndex++ % labels.length],
                        //animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,
                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Time Stamp: ' + _lst[i].Created_date
                    });
                    marker.addListener('click', toggleBounce);
                }
            }

            flightPlanCoordinates = _lst;
            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: '#393',
                strokeOpacity: 1.0,
                strokeWeight: 1
            });
            flightPath.setMap(map);
            marker = new google.maps.Marker({
                map: map,
                draggable: true,
                animation: google.maps.Animation.DROP,
            });
            marker.addListener('click', toggleBounce);

            var lineSymbol = {
                path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW,
                scale: 4,
                strokeColor: '#393'
            };
            var line = new google.maps.Polyline({
                path: flightPlanCoordinates,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
                map: map
            });

            animateCircle(line);

            function animateCircle(line) {
                var count = 0;
                window.setInterval(function () {
                    count = (count + 1) % 200;

                    var icons = line.get('icons');
                    icons[0].offset = (count / 2) + '%';
                    line.set('icons', icons);
                }, 400);
            }

            function toggleBounce() { };
        };

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

