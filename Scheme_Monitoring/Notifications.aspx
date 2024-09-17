<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="Notifications.aspx.cs" Inherits="Notifications" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     
    <asp:HiddenField ID="Notification_Id" runat="server" />
   
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
        <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

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
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Create Notification</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">CMVNY Scheme</li>
                                            <li class="breadcrumb-item active">Notification</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Create Notification</h4>
                                         <a href="NotificationList.aspx"  class="filter-btn" style="float:right;width:155px"><i class="icon-download"></i> Notification List</a>

                                    </div>
                                    <!-- end card header -->
                                     <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                              

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="lblULBType" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select" RepeatDirection="Horizontal"  AutoPostBack="true" OnSelectedIndexChanged="ddlULBType_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Nigam" Value="NN"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Panchayat" Value="NP"></asp:ListItem>
                                                            <asp:ListItem Text="Nagar Palika Parishad" Value="NPP"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                        <div id="divDivisionNames" runat="server">
                                                            <asp:Label ID="lblDivisionNames" runat="server" Text="Division Names" CssClass="form-label"></asp:Label>
                                                            <asp:ListBox ID="lstDivisionNames" runat="server" CssClass="form-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstDivisionNames_SelectedIndexChanged" class="chosen-select form-control multiselect" data-placeholder="Select Division">
                                                                 <asp:ListItem Text="All Divisions" Value="0"></asp:ListItem>
                                                            </asp:ListBox>
                                                        </div>
                                                </div>

                                             <div class="col-xxl-3 col-md-6 " id="" <%--style="display:none"--%>>
                                                    <div id="div1" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="Notification Heading*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtNotificationHeading" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 " id="" <%--style="display:none"--%>>
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="lblpop" runat="server" Text="Notification Details*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtNotificationDetail" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                    <div class="col-xxl-3 col-md-6 " id="" <%--style="display:none"--%>>
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Notification Date*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtNotificationDate" type="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                    <div class="col-xxl-3 col-md-6 " id="" <%--style="display:none"--%>>
                                                    <div id="div3" runat="server">
                                                        <asp:Label ID="Label3" runat="server" Text="Notification From Date*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtFromDate" type="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                    <div class="col-xxl-3 col-md-6 " id="" <%--style="display:none"--%>>
                                                    <div id="div4" runat="server">
                                                        <asp:Label ID="Label4" runat="server" Text="Notification To Date*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtToDate" type="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                                    <div class="col-xxl-3 col-md-6 " id="" <%--style="display:none"--%>>
                                                    <div id="div5" runat="server">
                                                        <asp:Label ID="Label5" runat="server" Text="Notification Doc*" CssClass="form-label"></asp:Label>
                                                         <asp:FileUpload ID="fileNotificationDocument" runat="server"   CssClass="form-control" />
                                           
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <br />
                                                          <asp:Button ID="BtnSave" Text="Submit" OnClick="BtnSave_Click" runat="server" Style="float: right" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="BtnUpdate" Text="Update" Visible="false" OnClick="BtnUpdate_Click" runat="server" Style="float: right" CssClass="btn bg-success text-white"></asp:Button>

                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                       
                                                    </div>
                                                </div>
                                              
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>

                                

                            </div>
                        </div>


                       
                    </ContentTemplate>
                  <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSave" />
                    <%--<asp:PostBackTrigger ControlID="exportToExcel" />--%>
                </Triggers>

                </asp:UpdatePanel>
          

            </div>
        </div>
    
        <style>
            tbale thead tr th{
                text-align:center;
            }
        .filter-btn {
    display: flex;
    padding: 8px 10px;
    border-radius: 5px;
    font-size: 14px;
    font-weight: 500;
    color: #ffffff;
    background-color: #f85420;
    width: 137px;
    border: 0;
    background: -webkit-gradient(linear, left top, right top, from(#FF5722), to(#D84315));
    background: linear-gradient(88deg, #FF5722 0%, #D84315);
    text-align: center;
    align-items: center;
    justify-content: center;
    height: 39px;
    line-height: 22px;
    transition: all .35s ease-Out;
}
        .smallsBtn{
                float: right;
    height: 36px;
    padding: 8px;
    border: 1px solid #d6d6d2;
    background: white;
    margin: 3px;
        }
    </style>
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.16.2/xlsx.full.min.js"></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.3.1/jspdf.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.5.21/jspdf.plugin.autotable.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />

<script type="text/javascript">
    $(document).ready(function () {
        // Initialize Select2 on the ListBox
        initializeSelect2();

        // Reinitialize Select2 after UpdatePanel partial updates
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initializeSelect2();
        });
    });

    function initializeSelect2() {
        // Ensure Select2 is applied to the ListBox
        $('#<%= lstDivisionNames.ClientID %>').select2({
            placeholder: "Select Division",
            allowClear: true
        });
    }
</script>

    <script>

        function exportTableToPDF() {
            const { jsPDF } = window.jspdf;
            const doc = new jsPDF('landscape', 'pt', 'a3');

            //doc.addFileToVFS('NotoSansDevanagari-Regular.ttf', "");
            //doc.addFont('NotoSansDevanagari-Regular.ttf', 'NotoSansDevanagari', 'normal');
            //doc.setFont('NotoSansDevanagari');

            doc.autoTable({
                html: '#sample-table-2',
                startY: 20,
                margin: { horizontal: 10 },
                styles: {
                    fontSize: 6,
                    cellPadding: 1,
                    overflow: 'linebreak',
                    minCellHeight: 10,
                    font: 'NotoSansDevanagari',
                },
                headStyles: {
                    fillColor: [40, 40, 40],
                    textColor: [255, 255, 255],
                },
                bodyStyles: {
                    cellWidth: 'wrap',
                },
                columnStyles: {
                    0: { cellWidth: 'auto' },
                    1: { cellWidth: 'auto' },
                },
                rowPageBreak: 'auto',
            });

            doc.save('VisionPlan.pdf');
        }

        function printTable() {
            debugger
            var tableHtml = document.getElementById('sample-table-2') ? document.getElementById('sample-table-2').outerHTML : '';
            if (tableHtml) {
                var printWindow = window.open();
                printWindow.document.write('<html><head><title>Print Table</title></head><body >');
                printWindow.document.write(tableHtml);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
            } else {
                console.error('Table not found for printing');
            }
        }

        $(document).ready(function () {
            $("#sample-table-2").DataTable({
                "lengthMenu": [
                    [25, 50, 100, -1], // Page length options
                    [25, 50, 100, "All"] // Labels for each option
                ]
            });
        });
  function ExportToExcel(type, fn, dl) {
    // Get the current date
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = currentDate.getMonth() + 1; // Months are zero-based
    const day = currentDate.getDate();

    var h2Element = document.querySelector('.tblheader h3');
    var h2Value = h2Element ? h2Element.innerText : 'DefaultHeader';

    // Format the date as desired (e.g., YYYY-MM-DD)
    const formattedDate = "Vision Plan Detail_" + `${year}-${month}-${day}`;

    var elt = document.getElementById('sample-table-2');

    // Remove the last column from the table before export
    removeLastColumn(elt);

    // Convert the table to an Excel workbook
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });

    // Set the width of each column
    var ws = wb.Sheets["sheet1"];
    
    // Define column widths (e.g., 20 characters wide for each column)
    var columnWidths = [
        { wch: 10 }, // Width of the first column
        { wch: 20 }, // Width of the second column
        { wch: 20 }, // Continue for each column as needed
        { wch: 10 }, // Continue for each column as needed
        { wch: 50 }, // Continue for each column as needed
        { wch: 18 }, // Continue for each column as needed
        { wch: 30 }, // Continue for each column as needed
        { wch: 20 }, // Continue for each column as needed
        { wch: 10 }, // Continue for each column as needed
        { wch: 20 }, // Continue for each column as needed
        { wch: 20 }, // Continue for each column as needed
        { wch: 20 }, // Continue for each column as needed
        { wch: 20 }, // Continue for each column as needed
        // You can add more columns depending on the number of columns in your table
    ];
    
    ws['!cols'] = columnWidths;

    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || (formattedDate + "." + (type || 'xlsx')));
}

// Function to remove the last column from the table
function removeLastColumn(table) {
    const rows = table.rows;
    for (let i = 0; i < rows.length; i++) {
        const lastCell = rows[i].cells.length - 1;
        if (lastCell >= 0) {
            rows[i].deleteCell(lastCell);
        }
    }
}

    </script>
    </asp:Content>