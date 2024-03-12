<%@ Page Language="C#" MasterPageFile="~/TemplatePopup.master" AutoEventWireup="true" CodeFile="GIS_Assessment_Report.aspx.cs" Inherits="GIS_Assessment_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    View Property On Google Map
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-4" style="display:none">
                                    <div class="form-group">
                                        <asp:Label ID="lblDistrict" runat="server" Text="District" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none">
                                    <div class="form-group">
                                        <asp:Label ID="lblULB" runat="server" Text="ULB" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblPhase" runat="server" Text="Phases" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:RadioButtonList ID="chkPhases" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Phase 1" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Phase 2" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-8" style="display:none">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Building Type" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:CheckBoxList ID="chkBuilding_Type" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Kalyan Mandap" Value="Kalyan Mandap" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Office Building" Value="Office Building" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Office Building and Kalyan Mandap" Value="Office Building and Kalyan Mandap" Selected="True"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-info"></asp:Button>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="display:none">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Details and Geo Location (Map View)
                                </div>
                            </div>
                        </div>
                        <div class="row" style="display:none">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div id="map" style="width: 100%; height: 500px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Map Of UP
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <iframe id="frameUP" width="100%" height="600px" src="UP_MAP.aspx" runat="server"></iframe>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_Map_Data" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD1KxtLqZtakEaBbSMouMIPl5tDKUz0IqM"></script>
    <%--<script src="Scripts/jquery-1.11.2.js"></script>--%>

    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                var _lst = [];
                var _lsttemp = [];
                var latCenter;
                var lngCenter;

                var latCenterF;
                var lngCenterF;
                var location_In;

                var _lstinfowindow = [];
                var infowindow_In;


                function getMapData() {
                    debugger;
                    _lsttemp = eval($("#ctl00_ContentPlaceHolder1_hf_Map_Data").val());
                    if (_lsttemp != undefined && _lsttemp != "undefined" && _lsttemp != "") {
                        for (var i = 0; i < _lsttemp.length; i++) {
                            var _obj = {};
                            _obj.lat = parseFloat(_lsttemp[i].Lat);
                            _obj.lng = parseFloat(_lsttemp[i].Long);

                            if (i == 0) {
                                latCenter = parseFloat(_lsttemp[i].Lat);
                                lngCenter = parseFloat(_lsttemp[i].Long);
                            }
                            _lst.push(_obj);
                        }
                        if (_lst.length > 0)
                            initMap();
                    }
                }

                var marker;
                getMapData();

                function initMap() {
                    debugger;
                    var a = 0;
                    var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
                    var labelIndex = 0;

                    var map = new google.maps.Map(document.getElementById('map'), {
                        zoom: 10,
                        center: { lat: latCenter, lng: lngCenter },
                        mapTypeId: 'terrain'
                    });

                    var lineSymbol = {
                        path: google.maps.SymbolPath.CIRCLE,
                        scale: 1,
                        strokeColor: '#393'
                    };

                    var flightPlanCoordinates = new Array();
                    for (var i = 0; i < _lst.length; i++) {
                        flightPlanCoordinates.push('lat: ' + _lst[i].Lat + ', lng: ' + _lst[i].Long + '');
                        var marker;
                        var info_In;

                        latCenterF = parseFloat(_lsttemp[i].Lat);
                        lngCenterF = parseFloat(_lsttemp[i].Long);

                        location_In = { lat: latCenterF, lng: lngCenterF }
                        debugger;

                        info_In = '<div class="row">' +
                            '<div class="col-lg-12">' +
                            '<div class="col-lg-6">' +
                            '<div class="form-group">' +
                            '<h3>District : ' + _lsttemp[i].District + '</br></h3>' +
                            '<p><b>ULB : </b>' + _lsttemp[i].ULB + ' </br>' +
                            '<p>ULB Type :' + _lsttemp[i].Type + '</br>' +
                            '<p><b>Building Type : </b>' + _lsttemp[i].Building_Type + '</br>' +
                            '<p><b>Phase : </b>' + _lsttemp[i].Phase + '</br>' +

                            '</div>' +
                            '</div>' +
                            '<div class="col-lg-6">' +
                            '<div class="form-group">' +
                            '</div ></div ></div >' +
                            '</div >';
                        _lstinfowindow.push(info_In);
                        infowindow_In = new google.maps.InfoWindow({
                            content: _lstinfowindow[i]
                        });
                        var _icon = '';
                        _icon = _lsttemp[i].PropertyType_Icon;
                        if (_icon == '') {
                            _icon = '/icon/google.png';
                        }
                        marker = new google.maps.Marker({
                            position: _lst[i],
                            map: map,
                            title: 'Property Details',
                            icon: _icon,
                            information: _lstinfowindow[i]
                        });
                        marker.addListener('click', function () {
                            infowindow_In.setContent(this.information);
                            infowindow_In.open(map, this);
                        });
                    }

                    flightPlanCoordinates = _lst;

                    marker.addListener('click', toggleBounce);

                    function toggleBounce() { };
                };
            })
        });

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

