<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="ProjectWorkRoadGalleryView.aspx.cs" Inherits="ProjectWorkRoadGalleryView" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>

                    <div class="page-content">

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    View Road Reinstatment Photos Uploaded
                               
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-12" id="divGallery" runat="server">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="clearfix">
                                    <div class="pull-right tableTools-container"></div>
                                </div>
                                <div class="table-header">
                                    Map View
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div id="map" style="width: 100%; height: 500px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_Map_Data" runat="server" />
                    </div>
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
        <!-- /.main-content -->
    </div>

    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <%--<script src="assets/js/bootstrap.min.js"></script>--%>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <script src="assets/js/dataTables.colReorder.min.js"></script>
    <script src="assets/js/jquery.colorbox.min.js"></script>


    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var $overflow = '';
                var colorbox_params = {
                    rel: 'colorbox',
                    reposition: true,
                    scalePhotos: true,
                    scrolling: false,
                    previous: '<i class="ace-icon fa fa-arrow-left"></i>',
                    next: '<i class="ace-icon fa fa-arrow-right"></i>',
                    close: '&times;',
                    current: '{current} of {total}',
                    maxWidth: '100%',
                    maxHeight: '100%',
                    onOpen: function () {
                        $overflow = document.body.style.overflow;
                        document.body.style.overflow = 'hidden';
                    },
                    onClosed: function () {
                        document.body.style.overflow = $overflow;
                    },
                    onComplete: function () {
                        $.colorbox.resize();
                    }
                };

                $('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);
                $("#cboxLoadingGraphic").html("<i class='ace-icon fa fa-spinner orange fa-spin'></i>");//let's add a custom loading icon


                $(document).one('ajaxloadstart.page', function (e) {
                    $('#colorbox, #cboxOverlay').remove();
                });
            })
        });

        function Poppuplose() {
            $find('details').hide();
        }
    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD1KxtLqZtakEaBbSMouMIPl5tDKUz0IqM"></script>
    <script src="Scripts/jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var _lst = [];
            var _lsttemp = [];
            //var latCenter;
            //var lngCenter;
            function getMapData() {
                _lsttemp = eval($("#ctl00_ContentPlaceHolder1_hf_Map_Data").val());
                if (_lsttemp != 0) {
                    for (var i = 0; i < _lsttemp.length; i++) {
                        var _obj = {};
                        _obj.lat = parseFloat(_lsttemp[i].lat);
                        _obj.lng = parseFloat(_lsttemp[i].lng);
                        _obj.description = _lsttemp[i].description;
                        //if (i == 0) {
                        //    latCenter = parseFloat(_lsttemp[i].lat);
                        //    lngCenter = parseFloat(_lsttemp[i].leg);
                        //}
                        _lst.push(_obj);
                    }
                    if (_lst.length > 0)
                        initMap();
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
                    center: { lat: 26.85806, lng: 80.9441639 },
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
                    strokeColor: '#1e90ff '
                };
                var flightPlanCoordinates = new Array();
                for (var i = 0; i < _lst.length; i++) {
                    flightPlanCoordinates.push('lat: ' + _lst[i].lat + ', lng: ' + _lst[i].lng + '');
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        label: labels[labelIndex++ % labels.length],
                        //animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,
                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Action: ' + _lst[i].description
                    });
                    marker.addListener('click', toggleBounce);
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
                    //position: { lat: 59.327, lng: 18.067 }
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
        })
    </script>
    <style type="text/css">
        .MyColr-tooltip + .tooltip > .tooltip-inner {
            width: 200px;
            max-width: 300px !important;
            background-color: blue;
            color: white;
            text-align: center;
            font-weight: 600;
            text-transform: capitalize;
            opacity: 1;
            filter: alpha(opacity=100);
            -moz-box-shadow: 0 0 5px 2px black;
            -webkit-box-shadow: 0 0 5px 2px black;
            box-shadow: 0 0 5px 2px black;
        }
    </style>
</asp:Content>


