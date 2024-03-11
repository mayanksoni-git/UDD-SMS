<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UP_MAP.aspx.cs" Inherits="UP_MAP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP MAP View</title>


    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.min.js"></script>
    <link href="Scripts/select2.css" rel="stylesheet" />
    <script src="Scripts/select2.js"></script>
    <link href="Scripts/sweetalert.css" rel="stylesheet" />
    <script src="Scripts/sweetalert.min.js"></script>


    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />


    <script src="Scripts/JS1.js"></script>
    <script src="Scripts/JS2.js"></script>


    <style type="text/css">
        .st0 {
            fill: lightgrey;
            stroke: #FFFFFF;
            stroke-width: 1.5;
            stroke-linecap: round;
            stroke-linejoin: round;
            stroke-miterlimit: 3;
        }

        .st1 {
            fill: #ffd793;
            stroke: white;
            stroke-width: 1.5;
            stroke-miterlimit: 3;
        }

        .st2 {
            fill: lightyellow;
            stroke: green;
            stroke-width: 1.5;
            stroke-miterlimit: 3;
        }

        .st3 {
            fill: lightgreen;
            stroke: #FFFFFF;
            stroke-width: 1.5;
            stroke-miterlimit: 3;
        }

        .st4 {
            fill: lightcyan;
            stroke: #FFFFFF;
            stroke-width: 1.5;
            stroke-miterlimit: 3;
        }

        .st5 {
            fill: white;
            stroke: black;
            stroke-width: 1.5;
            stroke-miterlimit: 3;
        }

        .st6 {
            fill: none;
            stroke: #FFFFFF;
            stroke-width: 1.5;
            stroke-linejoin: round;
            stroke-miterlimit: 3;
        }

        .st8 {
            font-size: 27.0945px;
        }

        .st9 {
            font-size: 23.7558px;
        }

        .st1:hover {
            opacity: 0.7 !important;
        }

        .st2:hover {
            opacity: 0.7 !important;
        }

        .st3:hover {
            opacity: 0.7 !important;
        }

        .st4:hover {
            opacity: 0.7 !important;
        }

        .legend {
            position: absolute;
            top: 50px;
            right: 0;
            max-width: 400px;
            width: 100%;
        }

        .tbl {
            position: absolute;
            top: 146px;
            right: 0;
            max-width: 512px;
            width: 100%;
        }

        .heading {
            position: relative;
            margin-bottom: 0px;
            padding-top: 5px;
            padding-left: 7px;
            padding-bottom: 5px;
            background-color: rgb(11 77 79);
            border-radius: 10px;
            color: white;
            font-family: fangsong;
            text-align: center;
        }

        tbody tr:nth-child(odd) {
            background-color: lightyellow;
            color: black;
        }

        .st0_Legend {
            background-color: lightgrey !important;
        }

        .st1_Legend {
            background-color: #ffd793 !important;
        }

        .st2_Legend {
            background-color: lightyellow !important;
        }

        .st3_Legend {
            background-color: lightgreen !important;
        }

        .st4_Legend {
            background-color: lightcyan !important;
        }

        .st5_Legend {
            background-color: white !important;
        }

        .st6_Legend {
            background-color: none !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("tbody tr:odd").css({
                "background-color": "ffd793",
                "color": "black"
            });
        });
    </script>
    <script>
        function printDiv() {
            window.print()
            // var divToPrint = document.getElementById('DivIdToPrint');

            // var newWin = window.open('', 'Print-Window');

            //// newWin.document.open();

            // newWin.document.write(window.print());

            //// newWin.document.close();

            // setTimeout(function () { newWin.close(); }, 10);

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%;">
            <svg xmlns:sodipodi="http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:cc="http://creativecommons.org/ns#" xmlns:svg="http://www.w3.org/2000/svg" xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" id="stsv" inkscape:output_extension="org.inkscape.output.svg.inkscape" sodipodi:docname="UP COVID-19 map.svg" sodipodi:version="0.32" x="0px" y="0px" viewBox="0 0 1802.4 1870.6" style="enable-background: new 0 0 1802.4 1870.6;" xml:space="preserve" class="undefined  replaced-svg" data-url="new_jjam.svg">
                <sodipodi:namedview bordercolor="#666666" borderopacity="1.0" fit-margin-bottom="0" fit-margin-left="0" fit-margin-right="0" fit-margin-top="0" gridtolerance="10.0" guidetolerance="10.0" id="base" inkscape:current-layer="svg4211" inkscape:cx="1201.6269" inkscape:cy="1254.3358" inkscape:pagecheckerboard="true" inkscape:pageopacity="0.0" inkscape:pageshadow="2" inkscape:window-height="713" inkscape:window-maximized="0" inkscape:window-width="683" inkscape:window-x="683" inkscape:window-y="27" inkscape:zoom="0.2151813" objecttolerance="10.0" pagecolor="#ffffff" showgrid="false" style="block-size: auto; height: auto; inline-size: auto; overflow-x: visible; overflow-y: visible; perspective-origin: 50% 50%; transform-origin: 0px 0px; vertical-align: baseline; width: auto;" units="cm"></sodipodi:namedview>
                <defs>
                    <inkscape:perspective id="perspective109" inkscape:persp3d-origin="1189.6945 : 822.10929 : 1" inkscape:vp_x="0 : 1233.1639 : 1" inkscape:vp_y="0 : 1000 : 0" inkscape:vp_z="2379.3888 : 1233.1639 : 1" sodipodi:type="inkscape:persp3d" style="block-size: auto; height: auto; inline-size: auto; overflow-x: visible; overflow-y: visible; perspective-origin: 50% 50%; transform-origin: 0px 0px; vertical-align: baseline; width: auto;"></inkscape:perspective>
                </defs>
                <g id="svg4211" transform="translate(10.344776,6.632)">
                    <path id="<% =obj_Map_View_Li[0].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[0].ClassName %>" d="<% =obj_Map_View_Li[0].D %>"></path>
                    <path id="<% =obj_Map_View_Li[1].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[1].ClassName %>" d="<% =obj_Map_View_Li[1].D %>"></path>
                    <path id="<% =obj_Map_View_Li[2].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[2].ClassName %>" d="<% =obj_Map_View_Li[2].D %>"></path>
                    <path id="<% =obj_Map_View_Li[3].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[3].ClassName %>" d="<% =obj_Map_View_Li[3].D %>"></path>
                    <path id="<% =obj_Map_View_Li[4].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[4].ClassName %>" d="<% =obj_Map_View_Li[4].D %>"></path>
                    <path id="<% =obj_Map_View_Li[5].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[5].ClassName %>" d="<% =obj_Map_View_Li[5].D %>"></path>
                    <path id="<% =obj_Map_View_Li[6].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[6].ClassName %>" d="<% =obj_Map_View_Li[6].D %>"></path>
                    <path id="<% =obj_Map_View_Li[7].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[7].ClassName %>" d="<% =obj_Map_View_Li[7].D %>"></path>
                    <path id="<% =obj_Map_View_Li[8].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[8].ClassName %>" d="<% =obj_Map_View_Li[8].D %>"></path>
                    <path id="<% =obj_Map_View_Li[9].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[9].ClassName %>" d="<% =obj_Map_View_Li[9].D %>"></path>
                    <path id="<% =obj_Map_View_Li[10].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[10].ClassName %>" d="<% =obj_Map_View_Li[10].D %>"></path>
                    <path id="<% =obj_Map_View_Li[11].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[11].ClassName %>" d="<% =obj_Map_View_Li[11].D %>"></path>
                    <path id="<% =obj_Map_View_Li[12].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[12].ClassName %>" d="<% =obj_Map_View_Li[12].D %>"></path>
                    <path id="<% =obj_Map_View_Li[13].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[13].ClassName %>" d="<% =obj_Map_View_Li[13].D %>"></path>
                    <path id="<% =obj_Map_View_Li[14].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[14].ClassName %>" d="<% =obj_Map_View_Li[14].D %>"></path>
                    <path id="<% =obj_Map_View_Li[15].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[15].ClassName %>" d="<% =obj_Map_View_Li[15].D %>"></path>
                    <path id="<% =obj_Map_View_Li[16].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[16].ClassName %>" d="<% =obj_Map_View_Li[16].D %>"></path>
                    <path id="<% =obj_Map_View_Li[17].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[17].ClassName %>" d="<% =obj_Map_View_Li[17].D %>"></path>
                    <path id="<% =obj_Map_View_Li[18].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[18].ClassName %>" d="<% =obj_Map_View_Li[18].D %>"></path>
                    <path id="<% =obj_Map_View_Li[19].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[19].ClassName %>" d="<% =obj_Map_View_Li[19].D %>"></path>
                    <path id="<% =obj_Map_View_Li[20].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[20].ClassName %>" d="<% =obj_Map_View_Li[20].D %>"></path>
                    <path id="<% =obj_Map_View_Li[21].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[21].ClassName %>" d="<% =obj_Map_View_Li[21].D %>"></path>
                    <path id="<% =obj_Map_View_Li[22].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[22].ClassName %>" d="<% =obj_Map_View_Li[22].D %>"></path>
                    <path id="<% =obj_Map_View_Li[23].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[23].ClassName %>" d="<% =obj_Map_View_Li[23].D %>"></path>
                    <path id="<% =obj_Map_View_Li[24].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[24].ClassName %>" d="<% =obj_Map_View_Li[24].D %>"></path>
                    <path id="<% =obj_Map_View_Li[25].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[25].ClassName %>" d="<% =obj_Map_View_Li[25].D %>"></path>
                    <path id="<% =obj_Map_View_Li[26].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[26].ClassName %>" d="<% =obj_Map_View_Li[26].D %>"></path>
                    <path id="<% =obj_Map_View_Li[27].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[27].ClassName %>" d="<% =obj_Map_View_Li[27].D %>"></path>
                    <path id="<% =obj_Map_View_Li[28].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[28].ClassName %>" d="<% =obj_Map_View_Li[28].D %>"></path>
                    <path id="<% =obj_Map_View_Li[29].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[29].ClassName %>" d="<% =obj_Map_View_Li[29].D %>"></path>
                    <path id="<% =obj_Map_View_Li[30].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[30].ClassName %>" d="<% =obj_Map_View_Li[30].D %>"></path>
                    <path id="<% =obj_Map_View_Li[31].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[31].ClassName %>" d="<% =obj_Map_View_Li[31].D %>"></path>
                    <path id="<% =obj_Map_View_Li[32].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[32].ClassName %>" d="<% =obj_Map_View_Li[32].D %>"></path>
                    <path id="<% =obj_Map_View_Li[33].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[33].ClassName %>" d="<% =obj_Map_View_Li[33].D %>"></path>
                    <path id="<% =obj_Map_View_Li[34].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[34].ClassName %>" d="<% =obj_Map_View_Li[34].D %>"></path>
                    <path id="<% =obj_Map_View_Li[35].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[35].ClassName %>" d="<% =obj_Map_View_Li[35].D %>"></path>
                    <path id="<% =obj_Map_View_Li[36].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[36].ClassName %>" d="<% =obj_Map_View_Li[36].D %>"></path>
                    <path id="<% =obj_Map_View_Li[37].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[37].ClassName %>" d="<% =obj_Map_View_Li[37].D %>"></path>
                    <path id="<% =obj_Map_View_Li[38].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[38].ClassName %>" d="<% =obj_Map_View_Li[38].D %>"></path>
                    <path id="<% =obj_Map_View_Li[39].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[39].ClassName %>" d="<% =obj_Map_View_Li[39].D %>"></path>
                    <path id="<% =obj_Map_View_Li[40].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[40].ClassName %>" d="<% =obj_Map_View_Li[40].D %>"></path>
                    <path id="<% =obj_Map_View_Li[41].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[41].ClassName %>" d="<% =obj_Map_View_Li[41].D %>"></path>
                    <path id="<% =obj_Map_View_Li[42].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[42].ClassName %>" d="<% =obj_Map_View_Li[42].D %>"></path>
                    <path id="<% =obj_Map_View_Li[43].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[43].ClassName %>" d="<% =obj_Map_View_Li[43].D %>"></path>
                    <path id="<% =obj_Map_View_Li[44].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[44].ClassName %>" d="<% =obj_Map_View_Li[44].D %>"></path>
                    <path id="<% =obj_Map_View_Li[45].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[45].ClassName %>" d="<% =obj_Map_View_Li[45].D %>"></path>
                    <path id="<% =obj_Map_View_Li[46].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[46].ClassName %>" d="<% =obj_Map_View_Li[46].D %>"></path>
                    <path id="<% =obj_Map_View_Li[47].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[47].ClassName %>" d="<% =obj_Map_View_Li[47].D %>"></path>
                    <path id="<% =obj_Map_View_Li[48].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[48].ClassName %>" d="<% =obj_Map_View_Li[48].D %>"></path>
                    <path id="<% =obj_Map_View_Li[49].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[49].ClassName %>" d="<% =obj_Map_View_Li[49].D %>"></path>
                    <path id="<% =obj_Map_View_Li[50].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[50].ClassName %>" d="<% =obj_Map_View_Li[50].D %>"></path>
                    <path id="<% =obj_Map_View_Li[51].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[51].ClassName %>" d="<% =obj_Map_View_Li[51].D %>"></path>
                    <path id="<% =obj_Map_View_Li[52].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[52].ClassName %>" d="<% =obj_Map_View_Li[52].D %>"></path>
                    <path id="<% =obj_Map_View_Li[53].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[53].ClassName %>" d="<% =obj_Map_View_Li[53].D %>"></path>
                    <path id="<% =obj_Map_View_Li[54].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[54].ClassName %>" d="<% =obj_Map_View_Li[54].D %>"></path>
                    <path id="<% =obj_Map_View_Li[55].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[55].ClassName %>" d="<% =obj_Map_View_Li[55].D %>"></path>
                    <path id="<% =obj_Map_View_Li[56].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[56].ClassName %>" d="<% =obj_Map_View_Li[56].D %>"></path>
                    <path id="<% =obj_Map_View_Li[57].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[57].ClassName %>" d="<% =obj_Map_View_Li[57].D %>"></path>
                    <path id="<% =obj_Map_View_Li[58].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[58].ClassName %>" d="<% =obj_Map_View_Li[58].D %>"></path>
                    <path id="<% =obj_Map_View_Li[59].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[59].ClassName %>" d="<% =obj_Map_View_Li[59].D %>"></path>
                    <path id="<% =obj_Map_View_Li[60].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[60].ClassName %>" d="<% =obj_Map_View_Li[60].D %>"></path>
                    <path id="<% =obj_Map_View_Li[61].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[61].ClassName %>" d="<% =obj_Map_View_Li[61].D %>"></path>
                    <path id="<% =obj_Map_View_Li[62].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[62].ClassName %>" d="<% =obj_Map_View_Li[62].D %>"></path>
                    <path id="<% =obj_Map_View_Li[63].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[63].ClassName %>" d="<% =obj_Map_View_Li[63].D %>"></path>
                    <path id="path5460" inkscape:connector-curvature="0" class="st5" d="M100.9,539.6c-0.9,1.6-3.3,1.3-4.2,2.7   c1.4-1.4,4.7-0.9,5.2-3.2c0.3-1.5-0.4-2.7-1.2-3.8C101.2,536.8,101.6,538.3,100.9,539.6z"></path>
                    <path id="path5464" inkscape:connector-curvature="0" class="st5" d="M98.1,531.2c0,0.1,0.1,0.3,0.2,0.4c-0.1-0.4-0.1-0.8,0-1.1   c0.2-1.4,1-2.7,0.8-4c-0.4,0.7-0.8,1.3-1.1,2C97.9,529.4,97.8,530.4,98.1,531.2L98.1,531.2z"></path>
                    <path id="path5468" inkscape:connector-curvature="0" class="st5" d="M96.3,549.1l-0.4-1.3C95.9,548.3,96.1,548.7,96.3,549.1z"></path>
                    <path id="path5480" inkscape:connector-curvature="0" class="st5" d="M100.3,533.6c0,0.4,0.2,0.8,0.3,1.1c0.1,0.1,0.2,0.3,0.2,0.4   L100.3,533.6z"></path>
                    <path id="uttar_pradesh_2_" inkscape:connector-curvature="0" class="st6" d="M116.1,719.3c11,12.3,6.7,15-2.3,24.2   c-23.6,24.1,8.8,66.2,9.3,91c0.4,17.2,18.6,16.1,30.4,21c4.7,2,11.2,2.2,14.7,6.6c6.7,8.4-10.9,8.5-10.9,9.5   c-1,15.9,26.3,1.5,18.7,24.1c-6.5,1.5-8.3,6.4-7,13.1c-16.8-4.2-16.4,9.3-15.4,21.8c14.4,1.3,37.8-1.9,41.2,16.5   c-10.1-0.5-26.3-2.2-27,10.4c-9.3,0.4-18.9-0.4-28.2,0c-0.3,1.2-0.6,2.4-0.8,3.7c-22,1.5-32.4,40.9-12.1,39.6   c-2.1-32.5,32.9-19.9,56.1-20.7c-0.4-5.4,1.5-11.7,1.1-17c14.7-0.6,21.4-2.1,31.2,9.6c6.8,8,23-10.3,32.5-7.9   c1.6,8.9,0.8,20.2,12.9,17.4c0.2-6.5,2.4-13.4,5.7-19.1c2.5,0.1,5.7-0.7,8.2-0.5c0.8-6.8,3.1-8.9,9.1-10.2   c3.3,9.8,8.6,18.6,18,20.1c-8.3,3.3-7.7,15.4,1,17.3c10.3,2.3,20.5-3.6,30.8-2c11.4,1.7,21.2,8.5,32.5,10.2   c11.7,1.7,23.6,0.1,35.2-0.3c-0.1-1.6,0.7-3.8,0.6-5.4c18-0.7,23.2,6.1,36.2,15.8c-1.6,9.7,9.6,11.6,16.5,13.4   c12.6,3.3,12.2,10.2,11.6,22.2c-0.7,14.9,9.7,22.8,15.1,35.4c6.3,14.9-0.8,32.3-0.3,47.7c-18,0.2-9.9,18.7-20.7,19.1   c-3.2,6.3-1.9,14.5-2.6,21.6c-11.9,6.3-10,29.6-22.6,35.3c-2.6,10.8-11.7,21.1-13.2,31.7c-11.4-10.4-11.5,8.8-10.7,16   c-1.4,0.1-2.7,0.2-4,0.3c-0.4,8.8,14.5,18.2,8.5,26.8c-4.3,6.1-10.4,5.1-16.8,4.5c-8.5-0.8-11.1,4.8-17.5,8.6   c-12.4,7.4-30.2-0.6-43.7,0.9c-15.6,1.8-10.4,18.1-11.3,29c-6.6,2.6-17.1-5.7-21.3,2.8c-2.9,5.8-1,13.6-1.4,19.8   c-0.7,10.6-3.2,34,13.8,32.2c1.5,7.1,6.7,12.9,5.4,20.4c-14.3-3-10,15.2-22.1,13.2c-1.3,14.6,0.7,26-12,36.2   c-6.1,4.9-17.6,15.3-26.4,13c-1.5,14,42,77.4,11.1,84.5c-1.7,14.1,6.5,13,16.9,15.8c21.7,5.7,8.4,33.3,9.6,47.9   c5.9,0.3,11.3,3,17.3,1.9c1.2-13.4,2.7-21.2,14-28.8c2.5,13.4,17.2,7.4,25.3,13.5c11,8.3,3.1,32.3,22.5,34.7   c-0.5-2.9,1.1-6.9,0.7-9.9c9-2.7,22.8-0.5,21.9,12c12.6-3.6,22-16.2,35.6-19.8c0.6-9.6,1.5-19.9,11.1-24.5c-6.6-8-10-16.1-21.3-20   c-1.3-6,2.8-9.4,8.6-8.6c0.5-5.4,2.7-12.2,0.8-17.6c-2.1-5.9-9.2-7.5-8.8-14.7c-10.3-1.5-21.4,7.1-33.7,4.6   c-0.7-17.9,10.1-40.2,5.7-57.2c-3.4-13-16.2-4.4-21.8-15.4c-5.5-10.8,7.7-56.2-13.2-52.3c-5.5-12.2,2.8-31.7-10.9-40.4   c-6-3.8-25,0.6-21.9-10.2c13,0,18.9-10.7,24.3-21.5c17.2-7.7,24-4.2,24.8,13.4c17.3,1.5,19.5,1.5,21.1-15.3   c3.6,0.4,7.8-0.6,11.4-0.2c0.5-5.2,0.4-10.6,0.8-15.8c11.7,0.3,16.7-11.1,28-15.6c4.7,7.7-2,11.9-5.1,18.2   c-4.7,9.8,6.7,19.1,7,28.7c-14.4,0.6-29.2,2-26.8,21.4c3.2,2,7.2,4.6,9.7,6.2c1.1-4.7,2.3-9.2,2.3-14.3c4.2-1.5,8.5-2.1,12.9-1.2   c0.3,10.8-0.1,18.4-10.5,23.4c-0.8,2.5-1,7.3-0.3,9.9c5.7,1.1,10-2.7,10.6-8.1c4.9-0.8,13.9-7.4,18.8-3.8c6.8,4.9-4,15.6,7,17.9   c-0.4,2,0.6,4.9,0.3,6.9c8.5,0.4,13.5-6.6,22.4-4.1c10.2,2.9,16.2,7.3,27.5,3.9c-0.3-3.6,1-7.8,0.8-11.3c11.5-1.1-3.5-30-4.6-37.4   c9.4,0.5,12.9,9.8,22.7,8.3c-1.3,17.2,9.9,12.7,23.4,13.9c1.7,9.9-49.7,48.2-14.8,53.8c1.3-6.8-6.3-14.6,2.2-19   c7.2-3.7,17.4,0.8,25,0.4c-2.2-18.8,12.9-12.8,16.8,0.4c16.5,2.2,37.1,0.6,54.3-0.1c0.4-5.2-2-40.7,12.9-30.7   c11.3,7.6,37.1-10,28.9-23.8c7.4-1.2,17.9,0.8,24.8,3.1c0.1-7.5,23.6-14.3,30-13.6c0.4,9.2,0.3,17.5,8.1,23.5   c6.8,5.3,15.6,17.3,23.3,20.2c3.4,19.8-23.4,17.2-19.7,32.9c1.6,0.5,2.7,1.2,4.1,1.7c-0.3,1.6-2.2,5.9-3.3,7.2c-4.6,1.8-3,1-6,4.3   c-5.1,0.3-9.3,3.7-10.2,8.5c6.5,0.6,12.9,0.8,18.9-0.9c-1.5-8.8,7.1-15.3,15.3-12.7c9.4,3,5.4,15,5.5,22.1   c17.4,5.4,13.2-19.8,9.6-28.8c5.2-3.9,13.6-5.2,16.4,1.5c1.1-2.5,3.1-3.9,3.1-8.4c9.5-2.4,8.5,3.5,11.9,9.6   c3.9,6.9,17.8,3.6,24.3,4.2c-0.1-0.8,0-3.1,0.1-4c-3.7-1.5-5.8-4.8-6.3-9c6.3-2.7,14.5-1.5,21.4-1c-0.4,3.1,1.4,7.2,1.5,8.9   c1.5-2.8,2.3-7.6,2-11.8c5.2-0.2,10.7-0.1,15.9,0.4c-0.8,3.1-2.7,5.4-5.7,6.9c-5.9,4.1-4.7,11.6-5.1,17.7   c-0.8,11.4-2.7,22.3-9.2,32c13,3.8,17.9-11.6,30.6-10.6c0.4,10.1,46.5,5.9,57.3,5.5c-8.8-19.6,21.3-28.2,7.5-46.8   c3.3-3.5,8.5-3.4,13.4-2.4c-0.8,3.7,0.8,7.9,1.5,10.3c3.9-6.9,9.6-16,20.8-12.7c0.1,2.9,2.1,5.4,1.8,8.4c5,5.2,8.1,10.3,17.1,9.3   c-1.8-10,7.6-14.7,4-26c6.9-0.3,19.2-3.3,25,1.8c10.4,9.2-1.8,23.1-0.7,33.8c9.5,0.8,25.6-5.9,28.7,6.6   c4.6,18.1,27.5,8.9,39.9,11.9c0.4,10.7-4.9,25.2,2,34.8c7.8,11,30.3,3.8,41.5,4.7c0,4.2,0.1,8.7,0.1,12.9   c8.6,0.8,17.6,0.4,26.2,1.1c0.3,8.1-0.5,16.5-0.2,24.7c4.7,0.5,9.6,0.4,14.3,0.8c0.2,4.7-0.1,9.6,0.1,14.3   c11.6-2,9.2,11.5,18.6,9.3c1-7.7-1.7-14.4-0.5-21.7c16.1-0.6,50.1,7.3,59.4-5.8c6.7-1.2,15.8,0.8,20.3,6.1   c0.7,3.4,1.1,6.7,1.4,10.1c2.9,3.6,8.6,0.9,11.5,4.2c14.8,16.2-5.3,25.1-19.5,18.3c-0.7,6.6-0.8,13.7-0.6,20.3c2-0.4,5,0.9,6.9,0.6   c0.3,6.6-0.3,13.6-0.1,20.2c15.2-0.4,13.2,11.6,9.6,22.2c-5.1,14.9-11.5,20-13,36.8c-2.9-0.7-7,0.8-9.9,0.2   c-0.8,2.1-0.8,6.3-0.2,8.4c7.2,2.5,12.8,6.7,15.7,13.9c2.4,5.8-1.7,15.8,8.2,15.2c-2.2,15.2,17.4,12.2,27.7,15.5   c1,21,48.3,21.1,62.6,13.8c15.5-7.9,2.7-27.5,12.3-38.5c4.2-4.8,9.4,3.2,10-6.5c0.5-8.4,10.9-6.2,16.9-6.6   c-1.5-10.7-14.5-16.5-3.5-26.8c6.2-5.8,19.2-8,10.9-19.2c-7.9-10.7-2.1-18.4,4.7-26.9c5.4-6.8-2.4-9.2-3.8-15.1   c-1.3-5.7,0.2-11.9,0.8-17.6c14.7-1.4,26.7-7.6,27.6-23.8c1.1-19.2-8-27.8-22.4-37.9c-15.9-11.2-10.7-28.1-17.2-43.7   c-7.5-18-6.7-32.8-0.5-51.1c4.5-13.4,10.5-26.2,25.5-29.6c19.1-4.2,26.8-14,43.2-22.6c11.7-6.1,28.3-6.6,36.3-18.4   c8.5-12.4,12-30,29.2-33.1c4.1-6.5,13.8-6.1,19.9-9.4c7-3.7,7.3-11.5,8.8-18.3c3.1-14.9,31.3-11,28.7,5.5   c11.8,1.4,12.9-5.9,21.1-11.5c13.4-9.2,29.7,3.8,43.7,5c29.2,2.6,28.1-27.6,4.2-34c-23.9-6.4-39-21.7-61-32.5   c-5.7-2.8-11.5-5.7-16.5-9.6c-7.1-5.4-4-12.8-7.9-19.6c-8.4-14.3-24.3-19-30.4-36c11.5,0.9,28-3.7,37.2-10.9   c6.2-4.8,0.6-17.6-1-23.3c-2.4-9.1-9.5-7.5-17.6-8.3c-11.1-1.1-20.3-8.6-32.1-10c-5.6-12.9-4.2-25.3,12.3-26   c16.5-0.7,9.5-10,23.7-14.1c17.8-5.1,37,2.6,55.1-0.1c8.1-1.2,15.3-6.4,8.2-14.6c-4.8-5.6-14.6-3.1-20.9-3.6   c0.2-5.5-2.9-9.5-8.6-9.1c-3.4-10.4-2.4-20.6-2.5-32.1c-12.8-2.2-28.6,3.3-39.7-5.7c-12.8-10.4-3.7-27.5-6.2-41   c-3-15.8-23.8-7.3-19.4-24.1c4-15.4-13.8-25.3-11.9-40.5c-16.8,0.6-19.4-17.5-20.6-30.8"></path>
                    <path id="haryana_1_" inkscape:connector-curvature="0" class="st6" d="M76.5,487.9c-2.2-9.5-6.1-19.1-6.7-29.2"></path>
                    <path id="path5922" inkscape:connector-curvature="0" class="st6" d="M-2.6-1.7"></path>
                    <path id="path5924" inkscape:connector-curvature="0" class="st6" d="M-2.6-1.7"></path>
                    <path id="<% =obj_Map_View_Li[64].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[64].ClassName %>" d="<% =obj_Map_View_Li[64].D %>"></path>
                    <path id="<% =obj_Map_View_Li[65].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[65].ClassName %>" d="<% =obj_Map_View_Li[65].D %>"></path>
                    <path id="<% =obj_Map_View_Li[66].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[66].ClassName %>" d="<% =obj_Map_View_Li[66].D %>"></path>
                    <path id="<% =obj_Map_View_Li[67].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[67].ClassName %>" d="<% =obj_Map_View_Li[67].D %>"></path>
                    <path id="<% =obj_Map_View_Li[68].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[68].ClassName %>" d="<% =obj_Map_View_Li[68].D %>"></path>
                    <path id="<% =obj_Map_View_Li[69].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[69].ClassName %>" d="<% =obj_Map_View_Li[69].D %>"></path>
                    <path id="<% =obj_Map_View_Li[70].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[70].ClassName %>" d="<% =obj_Map_View_Li[70].D %>"></path>
                    <path id="<% =obj_Map_View_Li[71].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[71].ClassName %>" d="<% =obj_Map_View_Li[71].D %>"></path>
                    <path id="<% =obj_Map_View_Li[72].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[72].ClassName %>" d="<% =obj_Map_View_Li[72].D %>"></path>
                    <path id="haryana" style="cursor: pointer" inkscape:connector-curvature="0" class="st6" d="M69.8,458.6c-1.5-28-0.2-51.7-8.5-79.2   c-7.9-25.8,1.2-60.3,9.9-85.2c1.3-0.1,2.6-0.2,4-0.4c4.6-7.8,4.3-16.5,10.8-23.5c5.7-6.2,9.6-11.4,10.2-20.1   c0.8-13-2.5-32.7,16.5-32.2c2.3-6.5,7.9-6.9,8.9-14.4c1.4-10.4,12.4-8.2,19.9-8.5c2.5-27.5,30.6-42.2,42.5-65   c2.3-4.3,3.9-8.8,5-13.2"></path>
                    <path id="uttaranchal_2_" inkscape:connector-curvature="0" class="st6" d="M185.1,101.9c-1.2,13.5,17.5,23.9,26.2,31.8   c15,13.6,36,14.5,53,24.5c-7.5-1.4-13.8,3.5-20.5,6c-4.7,14.9-6.9,32.8-17.2,45.2c-8.8,10.7-28.4,22.7-18.7,38.1   c5.9,9.4,1.7,30.4,10.3,35.7c11.9,7.3,30.1-1.4,42.4-3.5c-1.4,6.9,3.8,16.1-2.6,23.1c5.5,2.8,17.9,20.5,24.8,14.7   c12.2-10.1,4.3-19.1,21.1-23.5c11.7-3.1,9.5-19.1,21.3-18.2c14.6,1,22.8-12.1,35.7-3.2c15.2,10.4,16.3,25,26.9,38.5   c12.5,15.8,33.3,26.4,48.4,39.7c5.7,5,31.5,14.9,32.1,22.3c0.8,10.1-24.1,10-30.2,11.9c-3.7,4.8-6.2,10.6-4.9,17.1   c12.8,4.6,14.4,21.2,28.5,23.3c0.3,23.1,19.1,11.9,34,12.5c21.9,0.9,21,11.4,21.7,29.8c15.7-0.9,29.4,15.2,44.3,19   c14.6,3.8,19.1,6.9,19.9,21c13.2,1.2,26.8-0.6,39.7,2.8c17.6,4.7,33.3-5.4,50.2-1.5c0.3,7-0.4,14.4-0.1,21.4c4.2-1,8.6-1,12.9-0.4   c0.4,12.3,18.4-0.3,16.2,19.2c6.1,0.5,12.6,0.7,18.8,0.5c6.9-8,6.5-18,17.3-22.6c1.9-0.8,3.8-1.6,5.7-2.4"></path>
                    <path id="<% =obj_Map_View_Li[73].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[73].ClassName %>" d="<% =obj_Map_View_Li[73].D %>"></path>
                    <path id="<% =obj_Map_View_Li[74].SVG_District_Name %>" style="cursor: pointer" inkscape:connector-curvature="0" class="<% =obj_Map_View_Li[74].ClassName %>" d="<% =obj_Map_View_Li[74].D %>"></path>
                </g>
                <g id="Ebene_2" transform="translate(10.344776,6.632)"></g>
                <g id="Distriktnamen">
                    <text transform="matrix(0.9905 0 0 1 229.369 943.1168)" class="st7 st8">Agra</text>
                    <text transform="matrix(0.9905 0 0 1 247.261 746.6832)" class="st7 st8">Aligarh</text>
                    <text transform="matrix(0.9905 0 0 1 1045.7329 1412.566)" class="st7 st8">Prayagraj</text>
                    <text transform="matrix(0.9905 0 0 1 1208.1576 1102.0333)" class="st7 st8">Ambedkar </text>
                    <text transform="matrix(0.9905 0 0 1 1218.266 1126.7345)" class="st7 st8">Nagar</text>
                    <text transform="matrix(0.9905 0 0 1 296.4622 516.3795)" class="st7 st8">Amroha</text>
                    <text transform="matrix(0.9905 0 0 1 544.0125 1031.9791)" class="st7 st8">Auraiya</text>
                    <text transform="matrix(0.9905 0 0 1 1307.3297 1225.6947)" class="st7 st8">Azamgarh</text>
                    <text transform="matrix(0.9905 0 0 1 430.7174 715.8896)" class="st7 st8">Badaun</text>
                    <text transform="matrix(0.9905 0 0 1 30.1087 470.8673)" class="st7 st8">Baghpat</text>
                    <text transform="matrix(0.9905 0 0 1 941.6691 688.9498)" class="st7 st8">Bahraich</text>
                    <text transform="matrix(0.9905 0 0 1 1545.5571 1245.0936)" class="st7 st8">Ballia</text>
                    <text transform="matrix(0.9905 0 0 1 1148.5573 843.5712)" class="st7 st8">Balrampur</text>
                    <text transform="matrix(0.9905 0 0 1 767.0276 1314.9684)" class="st7 st8">Banda</text>
                    <text transform="matrix(0.9905 0 0 1 933.0162 997.2862)" class="st7 st8">Barabanki</text>
                    <text transform="matrix(0.9905 0 0 1 508.4251 627.9083)" class="st7 st8">Bareilly</text>
                    <text transform="matrix(0.9905 0 0 1 1234.1024 1022.2733)" class="st7 st8">Basti</text>
                    <text transform="matrix(0.9905 0 0 1 337.5035 390.5197)" class="st7 st8">Bijnor</text>
                    <text transform="matrix(0.9905 0 0 1 180.225 646.9937)" class="st7 st8">Bulandshahr</text>
                    <text transform="matrix(0.9905 0 0 1 1387.6993 1411.7947)" class="st7 st8">Chandauli</text>
                    <text transform="matrix(0.9905 0 0 1 854.5228 1383.6735)" class="st7 st8">Chitrakoot</text>
                    <text transform="matrix(0.9905 0 0 1 1467.0033 1099.0358)" class="st7 st8">Deoria</text>
                    <text transform="matrix(0.9905 0 0 1 403.428 843.8419)" class="st7 st8">Etah</text>
                    <text transform="matrix(0.9905 0 0 1 448.1483 1004.8102)" class="st7 st8">Etawah</text>
                    <text transform="matrix(0.9905 0 0 1 1067.8324 1051.7936)" class="st7 st8">Ayodhya</text>
                    <text transform="matrix(0.9905 0 0 1 533.7029 865.4347)" class="st7 st8">Farrukhabad</text>
                    <text transform="matrix(0.9905 0 0 1 815.6365 1228.5953)" class="st7 st8">Fatehpur</text>
                    <text transform="matrix(0.9905 0 0 1 308.3924 916.5568)" class="st7 st8">Firozabad</text>
                    <text transform="matrix(0.9905 0 0 1 65.1092 612.2037)" class="st7 st8">Gautam</text>
                    <text transform="matrix(0.9905 0 0 1 65.1092 639.299)" class="st7 st8">Buddha </text>
                    <text transform="matrix(0.9905 0 0 1 65.1092 666.3932)" class="st7 st8">Nagar</text>
                    <text transform="matrix(0.9905 0 0 1 59.9974 541.6853)" class="st7 st8">Ghaziabad</text>
                    <text transform="matrix(0.9905 0 0 1 1414.2816 1321.4037)" class="st7 st8">Ghazipur</text>
                    <text transform="matrix(0.9905 0 0 1 1080.0726 970.2173)" class="st7 st8">Gonda</text>
                    <text transform="matrix(0.9905 0 0 1 1375.8656 1051.7936)" class="st7 st8">Gorakhpur</text>
                    <text transform="matrix(0.9905 0 0 1 607.6761 1271.3326)" class="st7 st8">Hamirpur</text>
                    <text transform="matrix(0.9905 0 0 1 675.2493 899.5919)" class="st7 st8">Hardoi</text>
                    <text transform="matrix(0.9905 0 0 1 259.5457 820.2863)" class="st7 st8">Hathras</text>
                    <text transform="matrix(0.9905 0 0 1 508.4251 1189.3336)" class="st7 st8">Jalaun</text>
                    <text transform="matrix(0.9905 0 0 1 1199.7799 1301.814)" class="st7 st8">Jaunpur</text>
                    <text transform="matrix(0.9905 0 0 1 313.9876 1509.3102)" class="st7 st8">Lalitpur</text>
                    <text transform="matrix(0.9905 0 0 1 385.93 775.1368)" class="st7 st8">Kasganj</text>
                    <text transform="matrix(0.9905 0 0 1 446.2054 1281.5963)" class="st7 st8">Jhansi</text>
                    <text transform="matrix(0.9905 0 0 1 827.3021 1014.6266)" class="st7 st8">Lucknow</text>
                    <text transform="matrix(0.9905 0 0 1 953.2103 1343.9401)" class="st7 st8">Kaushambi</text>
                    <text transform="matrix(0.9905 0 0 1 562.869 955.7369)" class="st7 st8">Kannauj</text>
                    <text transform="matrix(0.9905 0 0 1 619.3651 1088.192)" class="st7 st8">Kanpur</text>
                    <text transform="matrix(0.9905 0 0 1 619.3651 1115.2848)" class="st7 st8">Dehat</text>
                    <text transform="matrix(0.9905 0 0 1 1482.1466 1007.4677)" class="st7 st8">Kushinagar</text>
                    <text transform="matrix(0.9905 0 0 1 770.9163 688.7623)" class="st7 st8">Lakhimpur </text>
                    <text transform="matrix(0.9905 0 0 1 794.3831 708.0318)" class="st7 st8">Kheri</text>
                    <text transform="matrix(0.9905 0 0 1 580.3675 1352.2672)" class="st7 st8">Mahoba</text>
                    <text transform="matrix(0.9905 0 0 1 446.9811 935.2995)" class="st7 st8">Mainpuri</text>
                    <text transform="matrix(0.9905 0 0 1 1445.556 1214.5343)" class="st7 st8">Mau</text>
                    <text transform="matrix(0.9905 0 0 1 130.1522 820.2843)" class="st7 st8">Mathura</text>
                    <text transform="matrix(0.9905 0 0 1 172.0482 469.7887)" class="st7 st8">Meerut</text>
                    <text transform="matrix(0.9905 0 0 1 1387.6993 936.3375)" class="st7 st8">Maharajganj</text>
                    <text transform="matrix(0.9905 0 0 1 724.2913 1121.3424)" class="st7 st8">Kanpur</text>
                    <text transform="matrix(0.9905 0 0 1 827.3021 851.6881)" class="st7 st8">Sitapur</text>
                    <text transform="matrix(0.9905 0 0 1 1315.4242 1591.1266)" class="st7 st8">Sonbhadra</text>
                    <text transform="matrix(0.9905 0 0 1 1160.0825 1173.8561)" class="st7 st8">Sultanpur</text>
                    <text transform="matrix(0.9905 0 0 1 1252.6266 902.4947)" class="st7 st8">Siddharthnagar</text>
                    <text transform="matrix(0.9905 0 0 1 1038.5179 784.9313)" class="st7 st8">Shravasti</text>
                    <text transform="matrix(0.9905 0 0 1 644.5316 588.6471)" class="st7 st8">Pilibhit</text>
                    <text transform="matrix(0.9905 0 0 1 1282.1838 1367.782)" class="st7 st8">Varanasi</text>
                    <text transform="matrix(0.9905 0 0 1 771.6927 1059.7701)" class="st7 st8">Unnao</text>
                    <text transform="matrix(0.9905 0 0 1 576.1326 742.3126)" class="st7 st8">Shahjahanpur</text>
                    <text transform="matrix(0.9905 0 0 1 1223.1802 1462.3573)" class="st7 st8">Mirzapur</text>
                    <text transform="matrix(0.8435 0 0 1 160.9342 394.176)" class="st7 st9">Muzaffarnagar</text>
                    <text transform="matrix(0.9905 0 0 1 879.7991 1153.9996)" class="st7 st8">Raebareli</text>
                    <text transform="matrix(0.9905 0 0 1 1030.7739 1250.0667)" class="st7 st8">Pratapgarh</text>
                    <text transform="matrix(0.9905 0 0 1 474.0904 506.6168)" class="st7 st8">Rampur</text>
                    <text transform="matrix(0.9905 0 0 1 95.3063 253.6969)" class="st7 st8">Saharanpur</text>
                    <text transform="matrix(0.9905 0 0 1 1328.7925 1012.5801)" class="st7 st8">Sant</text>
                    <text transform="matrix(0.9905 0 0 1 1309.3284 1041.4786)" class="st7 st8">Kabir </text>
                    <text transform="matrix(0.9905 0 0 1 1307.2891 1074.131)" class="st7 st8">Nagar</text>
                    <text transform="matrix(0.9905 0 0 1 1176.2207 1379.4896)" class="st7 st8">Bhadohi </text>
                    <text transform="matrix(0.9905 0 0 1 1159.7924 1326.3561)" class="st7 st8"></text>
                    <text transform="matrix(0.9905 0 0 1 378.5909 545.4454)" class="st7 st8">Moradabad</text>
                    <text transform="matrix(0.9905 0 0 1 336.2693 608.84)" class="st7 st8">Sambhal</text>
                    <text transform="matrix(1.0717 0 0 1 76.6375 368.4308)" class="st7 st8">Shamli</text>
                    <text transform="matrix(0.9905 0 0 1 212.3719 547.5406)" class="st7 st8">Hapur</text>
                    <text transform="matrix(0.9905 0 0 1 1011.318 1145.651)" class="st7 st8">Amethi</text>
                    <path id="path8540" d="M160.9,565.5c-0.2,0.2-0.4,0.2-0.7,0.2C160.5,565.7,160.7,565.7,160.9,565.5z"></path>
                </g>
            </svg>
            <div style="margin-bottom: 2px">
                <% 
                    for (int i = 0; i < obj_Map_View_Legend_Li.Count; i++)
                    {
                %>
                <div class="row">
                    <div class="col-md-1 <% =obj_Map_View_Legend_Li[i].ClassName %>"></div>
                    <div class="col-md-5" style="font-size: 14px !important;"><% =obj_Map_View_Legend_Li[i].Legend_Text %></div>
                </div>
                <%
                    }
                %>
            </div>
        </div>

        <script>

            jQuery(function ($) {
                $("#stsv").svgInject(function () {
                    // Injection complete
                    $("polyline").hover(function () {
                        $("#tooltip-span").attr("style", "display:block;");
                        var manid = $(this).attr("id").toUpperCase(); + "ss";
                        $("#tooltipload").html($("#" + manid).html());
                    });
                    $("path").click(function () {
                        var manid = $(this).attr("id").toLowerCase();
                        //window.location = "../ProjectDetails/AgencyTypeWise?District=" + manid;
                        window.open('Dashboard_PMIS_CNDS.aspx', '_blank');
                    });
                    //"Invalid web service call, missing value for parameter: 'obj_DistrictClass'."
                    $("path").hover(function () {
                        $("#tooltip-span").attr("style", "display:block;");
                        var manid = $(this).attr("id").toLowerCase();
                        $("#tooltipload").html(manid.toUpperCase());
                        var district = manid;
                        var param = { District_Name: district };
                        $.ajax({
                            url: "UP_MAP.aspx/getDistrictWiseData",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                row = "";
                                row += '<tr>'
                                row += '<td style="text-align:left;">District</td>'
                                row += '<td style="text-align:left;">' + data.d.District_Name + '</td>'
                                row += '</tr>'

                                row += '<tr>'
                                row += '<td style="text-align:left;">Data 1</td>'
                                row += '<td style="text-align:left;">' + data.d.Data1 + '</td>'
                                row += '</tr>'

                                row += '<tr>'
                                row += '<td style="text-align:left;">Data 2</td>'
                                row += '<td style="text-align:left;">' + data.d.Data2 + '</td>'
                                row += '</tr>'

                                $("#tab2").html(row);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    });
                    $("div").hover(function () {
                        $("#tooltip-span").attr("style", "display:none;");
                    });
                });
            })
        </script>
        <style>
            #tooltip-span {
                display: none;
                position: fixed;
                /*overflow: scroll;*/
            }

            .tootltip_table th, .tootltip_table td {
                font-size: 11px;
                color: black;
                padding: 4px;
            }
        </style>
        <div id="tooltip-span" style="display: none; top: 0; left: 0;">
            <div id="tooltipload" style="padding: 4px !important; width: auto !important; background-color: #fdfaf3; color: black !important;"></div>
            <div style="padding: 2px !important; max-width: 400px !important; width: 100%; background-color: #ffd793; color: black !important;">
                <div class="tootltip_table">
                    <table class="table table-bordered" style="color: white" id="example123">
                        <thead>
                            <tr>
                                <th>Discription</th>
                                <th>Data</th>
                            </tr>
                        </thead>
                        <tbody id="tab2" name="tblbody">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <script>
            var tooltipSpan = document.getElementById('tooltip-span');
            window.onmousemove = function (e) {
                var x = e.clientX,
                    y = e.clientY;
                tooltipSpan.style.top = (y + 2) + 'px';
                tooltipSpan.style.left = (x + 2) + 'px';
            };
        </script>
    </form>
</body>
</html>
