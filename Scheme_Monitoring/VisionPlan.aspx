<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="VisionPlan.aspx.cs" Inherits="VisionPlan" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <asp:HiddenField ID="ULBFundId" runat="server" />
    <asp:HiddenField ID="ULBID" runat="server" />
    <asp:HiddenField ID="FYID" runat="server" />
   
   

    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
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
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Vision Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">CMVNY Scheme</li>
                                            <li class="breadcrumb-item active">Vision Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Filter :</h4>
                                         <a href="CreateVisionPlan.aspx"  class="filter-btn" style="float:right;width:155px"><i class="icon-download"></i> Create Vision Plan</a>

                                    </div>
                                    <!-- end card header -->
                                     <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                        <%--<asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6" id="sectionpriority"  runat="server" >
                                                    <div id="div7" runat="server">
                                                        <asp:Label ID="Label10" runat="server" Text="Self Priority" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="DdlPriority" runat="server" CssClass="form-select" >
                                                            <asp:ListItem value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                               <%-- <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="NN">Nagar Nigam</asp:ListItem>
                                                            <asp:ListItem Value="NPP">Nagar Palika Parishad</asp:ListItem>
                                                            <asp:ListItem Value="NP">Nagar Panchayat</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>

                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server" style="float:right"  CssClass="btn bg-success text-white"></asp:Button>

                                                       
                                                    </div>
                                                </div>
                                              
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>

                                <div class="card"  runat="server">
                                     <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Vision Plan</h4>
                                      <%--<asp:Button ID="ExportExcel" runat="server" Text="Export to Excel"  CommandName="Export Data" OnClick="ExportToExcel_Click" CssClass="btn btn-success" />--%>
                                        <a href="#" id="exportToExcel" runat="server" onclick="ExportToExcel('xlsx')" class="filter-btn" style="float:right"><i class="icon-download"></i> Export To Excel</a>

                                    </div>
                                <div class="card-body"  runat="server" style="">
                                     <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                            <table id="sample-table-2" class="table table-striped table-bordered table-hover table-responsive">


                                                <thead>
                                                    <tr class="table-primary">

                                                        <th style="text-align:center;font-size:15px" >Sr. No.</th>
                                                       <th  style="text-align:center;font-size:15px" colspan="3">ULB Details</th>
                                                        <th  style="text-align:center;font-size:15px">Project Name</th>
                                                       <th  style="text-align:center;font-size:15px" colspan="2">Existing</th>
                                                        <th  style="text-align:center;font-size:15px" ">Condition</th>
                                                       
                                                        <th  style="text-align:center;font-size:15px">User Charge</th>
                                                        <th  style="text-align:center;font-size:15px" >Ownership</th>
                                                      
                                                        <th  style="text-align:center;font-size:15px" colspan="2">Location</th>
                                                        <th  style="text-align:center;font-size:15px" >Priority</th>
                                                         <th style="text-align:center;font-size:15px">Action</th>
                                                    </tr>
                                                     <tr class="table-primary">

                                                       <th>#</th>
                                                        <th  style="text-align:center">District</th>
                                                        <th  style="text-align:center">ULB Name</th>
                                                        <th  style="text-align:center">Population</th>
                                                        <th  style="text-align:center">Project Name</th>
                                                        <th  style="text-align:center">Is Constructed?</th>
                                                      <%--  <th  style="text-align:center">Under Construction(Y/N)</th>
                                                        <th  style="text-align:center">Under Sanction (Y/N)</th>--%>
                                                        <th  style="text-align:center">Year of Construction for Constructed Building</th>
                                                       <%-- <th  style='text-align:center">Good </th>
                                                        <th  style="text-align:center">Need Renovaton</th>
                                                        <th  style="text-align:center">Need Redevelopement</th>--%>
                                                        <th  style="text-align:center">Condition</th>
                                                        <th  style="text-align:center">User Charge</th>
                                                        <%--<th  style="text-align:center">Nagar Nigam</th>--%>
                                                        <th  style="text-align:center">Ownership</th>
                                                        <th  style="text-align:center">No Of Similar Project</th>
                                                        <th  style="text-align:center">Ward Name (Ward No.)</th>
                                                        <th  style="text-align:center">(on a scale of 1 to 5, 5 being the highest)</th>
                                                         <th></th>
                                                    </tr>
                                                </thead>
                                       
                                     <asp:Repeater ID="grdPost" runat="server" OnItemCommand="grdPost_ItemCommand" >
                                        <ItemTemplate>
                                            <tr>
                                               
                                                <td><%# Container.ItemIndex + 1 %></td>
                                              
                                             

                                              
                                                <td><%# DataBinder.Eval(Container.DataItem, "Circle_Name") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Division_Name") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "population") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "ProjectName") %></td>
                                               <%-- <td><%# DataBinder.Eval(Container.DataItem, "Constructed") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "UnderConstruction") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Sanction") %></td>--%>
                                               

                                                <td><%# DataBinder.Eval(Container.DataItem, "Construction") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "constructedYear") %></td>
                                               <%-- <td><%# DataBinder.Eval(Container.DataItem, "GoodCondition") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "NeedRenovation") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "needredevelopement") %></td>--%>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Condition") %></td>

                                               
                                                <td><%# DataBinder.Eval(Container.DataItem, "Usercharge") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "OwnerShips") %></td>
                                                <%--<td><%# DataBinder.Eval(Container.DataItem, "OtherOwner") %></td>--%>
                                                <td><%# DataBinder.Eval(Container.DataItem, "NoOfSameProjInCity") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Loactions") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "selfPriority") %></td>


                                                <td>
                                                    <%--  <asp:LinkButton ID="btnEdit"  Text="Edit" CssClass="btn bg-warning" runat="server" ToolTip="Click to Edit Record" CommandName="edit"
                                                class="icon-pencil bigger-130 green" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VisionPlanID") + "|" + DataBinder.Eval(Container.DataItem, "PlanName") %>' />--%>
                                                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CssClass="btn bg-warning icon-pencil bigger-130 green" ToolTip="Click to Edit Record" CommandName="edit"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VisionPlanID") + "|" + DataBinder.Eval(Container.DataItem, "distId")+ "|" + DataBinder.Eval(Container.DataItem, "ULBID")+ "|" + DataBinder.Eval(Container.DataItem, "FYID") %>' />
                                                    <asp:LinkButton ID="btnDelete" CssClass="btn bg-danger" Text="Delete" runat="server" ToolTip="Click to Delete Record" CommandName="delete" class="icon-trash bigger-130 red" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VisionPlanID") %>'
                                                        OnClientClick="return confirm('Are you sure !');" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           
                                        </FooterTemplate>
                                        
                                    </asp:Repeater>
                                                <asp:Panel ID="NoRecordsPanel" runat="server" Visible="False">
                                                    <p style="color:red;">Record Not Found</p>
                                                </asp:Panel>
                                   </table>
                                </div>
                                </div>

                            </div>
                        </div>


                       
                    </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <%--<asp:PostBackTrigger ControlID="btnDelete" />--%>
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
    </style>
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.16.2/xlsx.full.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#sample-table-2").DataTable();
        })
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