<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tax_Pivot.aspx.cs" Inherits="Tax_Pivot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dynamic Data Analysis | PIVOT</title>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.11/c3.min.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/d3/3.5.5/d3.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui-touch-punch/0.2.3/jquery.ui.touch-punch.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/PapaParse/4.1.2/papaparse.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.11/c3.min.js"></script>

    <!-- PivotTable.js libs from ../dist -->
    <link rel="stylesheet" type="text/css" href="../dist/pivot.css" />
    <script type="text/javascript" src="../dist/pivot.js"></script>
    <script type="text/javascript" src="../dist/d3_renderers.js"></script>
    <script type="text/javascript" src="../dist/c3_renderers.js"></script>
    <script type="text/javascript" src="../dist/export_renderers.js"></script>

    <%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
    <script src="../assets/js/jquery.table2excel.js"></script>

    <%--<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.4.1/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/2.3.5/jspdf.plugin.autotable.min.js"></script>
    <script src="../assets/js/tableHTMLExport.js"></script>--%>

    <style>
        html {
            height: 100%;
        }

        body {
            font-family: Verdana;
            min-height: 95%;
            border: 5px dotted;
        }

        .whiteborder {
            border-color: white;
        }

        .greyborder {
            border-color: lightgrey;
        }

        #filechooser {
            color: #555;
            text-decoration: underline;
            cursor: pointer; /* "hand" cursor */
        }

        .node {
            border: solid 1px white;
            font: 10px sans-serif;
            line-height: 12px;
            overflow: hidden;
            position: absolute;
            text-indent: 2px;
        }

        .c3-line, .c3-focused {
            stroke-width: 3px !important;
        }

        .c3-bar {
            stroke: white !important;
            stroke-width: 1;
        }

        .c3 text {
            font-size: 12px;
            color: grey;
        }

        .tick line {
            stroke: white;
        }

        .c3-axis path {
            stroke: grey;
        }

        .c3-circle {
            opacity: 1 !important;
        }

        .c3-xgrid-focus {
            visibility: hidden !important;
        }

        .hideDisplay {
            display: none;
        }

        .topnav {
            position: relative;
            z-index: 2;
            font-size: 17px;
            background-color: #5f5f5f;
            color: #f1f1f1;
            width: 100%;
            padding: 0;
            letter-spacing: 1px;
            font-family: "Segoe UI",Arial,sans-serif;
            height: 35px;
            width: 100%;
        }

        .topnav1 {
            position: relative;
            z-index: 2;
            font-size: 17px;
            background-color: #ffffcc;
            color: #f1f1f1;
            width: 100%;
            padding: 0;
            letter-spacing: 1px;
            font-family: "Segoe UI",Arial,sans-serif;
            height: 35px;
            width: 100%;
        }

        .w3-card, .w3-card-2 {
            box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);
        }
    </style>
</head>
<body class="whiteborder">
    <form id="form1" runat="server">
        <script type="text/javascript">
            var isRestore = 0;
            var RestoreConfig = '';
            $(function () {
                var renderers = $.extend(
                    $.pivotUtilities.renderers,
                    $.pivotUtilities.c3_renderers,
                    $.pivotUtilities.d3_renderers,
                    $.pivotUtilities.export_renderers
                );

                var parseAndPivot = function (f) {
                    $("#output").html("<p align='center' style='color:grey;'>(processing...)</p>")
                    Papa.parse(f, {
                        skipEmptyLines: true,
                        error: function (e) { alert(e) },
                        complete: function (parsed) {
                            if (isRestore == 0)
                                $("#output").pivotUI(parsed.data, { renderers: renderers }, true);
                            else {
                                $("#output").pivotUI(parsed.data, RestoreConfig, true);
                                isRestore = 0;
                                RestoreConfig = '';
                            }
                        }
                    });
                };

                $("#csv").bind("change", function (event) {
                    parseAndPivot(event.target.files[0]);
                });

                $("#textarea").bind("input change", function () {
                    parseAndPivot($("#textarea").val());
                });

                var dragging = function (evt) {
                    evt.stopPropagation();
                    evt.preventDefault();
                    evt.originalEvent.dataTransfer.dropEffect = 'copy';
                    $("body").removeClass("whiteborder").addClass("greyborder");
                };

                var endDrag = function (evt) {
                    evt.stopPropagation();
                    evt.preventDefault();
                    evt.originalEvent.dataTransfer.dropEffect = 'copy';
                    $("body").removeClass("greyborder").addClass("whiteborder");
                };

                var dropped = function (evt) {
                    evt.stopPropagation();
                    evt.preventDefault();
                    $("body").removeClass("greyborder").addClass("whiteborder");
                    parseAndPivot(evt.originalEvent.dataTransfer.files[0]);
                };
                $("html")
                    .on("dragover", dragging)
                    .on("dragend", endDrag)
                    .on("dragexit", endDrag)
                    .on("dragleave", endDrag)
                    .on("drop", dropped);

                $(document).ready(function () {
                    $("#textarea").val($("#hdf_Data").val());
                    parseAndPivot($("#textarea").val());
                });

                $("#save").on("click", function () {
                    var config = $("#output").data("pivotUIOptions");
                    var config_copy = JSON.parse(JSON.stringify(config));
                    //delete some values which will not serialize to JSON
                    delete config_copy["aggregators"];
                    delete config_copy["renderers"];
                    //$.cookie("pivotConfig", JSON.stringify(config_copy));
                    var report_name = $("#report_name").val();
                    if (report_name == "") {
                        alert('Report Name Can Not Be Blank!!');
                        return;
                    }
                    save(JSON.stringify(config_copy), report_name);
                });

                $("#restore").on("click", function () {
                    isRestore = 1;
                    parseAndPivot($("#textarea").val());
                    //$("#output").pivotUI(parsed.data, JSON.parse($.cookie("pivotConfig")), true);
                });

                $("#ddlReportLayouts").change(function () {
                    var item = $(this);
                    if (item.val() == "0") {
                        alert('Please Select A Report Layout..!');
                        return;
                    }
                    else {
                        getConfig(item.val());
                    }
                });

                $("#export_excel").click(function () {
                    $(".pvtTable").table2excel({
                        name: "Pivot_Data_Export_WB",
                        filename: "Pivot_Data_Export",
                        fileext: ".xls",
                        exclude_img: true,
                        exclude_links: true,
                        exclude_inputs: true
                    });
                });

                $("#print").click(function () {
                    var divContents = $(".pvtRendererArea").html();
                    var newWin = window.open('', 'Print-Window');
                    newWin.document.open();
                    newWin.document.write('<html><head><title></title>');
                    newWin.document.write('<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.11/c3.min.css" />');
                    newWin.document.write('<link rel="stylesheet" type="text/css" href="../dist/pivot.css" />');
                    newWin.document.write('</head><body >');
                    newWin.document.write(divContents);
                    newWin.document.write('</body></html>');
                    newWin.document.print();
                    newWin.document.close();
                });

                //$('#export_pdf').on('click', function () {
                //    $(".pvtTable").tableHTMLExport({ type: 'pdf', filename: 'Pivot_Data_Export.pdf' });
                //})

                $("#delete_report").on("click", function () {
                    if (confirm("Do You Want To Delete This Report Layout....??")) {
                        PerformAction("DeleteReport|" + $("#ddlReportLayouts").val());
                    }
                    else {

                    }
                });

                $("#home").on("click", function () {
                    location.href = '../Index.aspx';
                });

                function save(config, report_name) {
                    if (confirm("Do You Want To Save This Report Layout....??")) {
                        PerformAction("SaveReport|" + config + '|' + report_name);
                    }
                    else {

                    }
                }

                function getConfig(report_id) {
                    PerformAction("GetConfig|" + report_id);
                }

                window.ReceiveData = function (rVal) {
                    var receiveData = rVal.split('|');
                    if (receiveData[0] == "SaveReport") {
                        if (receiveData[1] == "true") {
                            alert("Report Layout Saved Successfully for Later Use...!!")
                            return;
                        }
                        else {
                            alert("Error In Saving Report Layout...!!");
                            return;
                        }
                    }
                    else if (receiveData[0] == "GetConfig") {
                        if (receiveData[1] == "true") {
                            isRestore = 1;
                            RestoreConfig = receiveData[2];
                            //RestoreConfig = JSON.parse($.cookie("pivotConfig"));
                            RestoreConfig = JSON.parse(receiveData[2]);
                            parseAndPivot($("#textarea").val());
                        }
                        else {
                            alert("Error In Loading Report Layout...!!");
                            return;
                        }
                    }
                    else if (receiveData[0] == "DeleteReport") {
                        if (receiveData[1] == "true") {
                            alert("Report Layout Deleted Successfully...!!");
                            return;
                        }
                        else {
                            alert("Error In Deleting Report Layout...!!");
                            return;
                        }
                    }
                    else { }
                };
            });
        </script>

        <div class="w3-card-2 topnav" id="topnav" style="position: relative;">
            <div style="float: left">
                <img src="../assets/images/mb/icon.png" width="25px" height="25px" id="home" style="cursor: pointer;" />
                <label style="color: white; font-weight: bold;">Choose Data:</label>
                <asp:RadioButton GroupName="a" ID="rbtOnSite" Checked="true" Text="PMIS Data" runat="server" />
                <asp:RadioButton GroupName="a" ID="rbtOffSite" Text="ePayment Data" runat="server" Enabled="false" />
                <asp:Button ID="btnLoad" runat="server" Text="Load Analysis Tool" OnClick="btnLoad_Click" />
            </div>

            <div style="float: right; margin-right: 10px;">
                <input type="button" value="Export (Excel)" id="export_excel" style="font-size: 12px;" />
                <input type="button" value="Print" id="print" style="font-size: 12px;" />
            </div>
        </div>
        <div class="w3-card-2 topnav1" id="topnav1" style="position: relative;">
            <div style="float: left">
            </div>

            <div style="float: right; margin-right: 10px;">
                <label style="color: black;">Report Layout Name:</label>
                <input type="text" id="report_name" style="font-size: 12px;" />
                <input type="button" value="Save Report Layout" id="save" style="font-size: 12px;" />

                <asp:DropDownList ID="ddlReportLayouts" runat="server"></asp:DropDownList>
                <input type="button" value="Delete Report" id="delete_report" style="font-size: 12px;" />
            </div>
        </div>

        <asp:HiddenField ID="hdf_Data" runat="server" Value="" />

        <textarea style="display: none;" id="textarea"></textarea>

        <div id="output" style="margin: 10px;"></div>
    </form>
</body>

</html>
