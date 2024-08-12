<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="FundRecievedInULB.aspx.cs" Inherits="FundRecievedInULB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="ULBFundId" runat="server" />

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
                                    <h4 class="mb-sm-0">ULB Fund</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan</li>
                                            <li class="breadcrumb-item active">ULB Fund</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">ULB Fund</h4>
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
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div3" runat="server">
                                                        <asp:Label ID="lblRemark" runat="server" Text="SFC Fund(in Cr)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="SFC" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div1" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="CFC Fund(in Cr)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="CFC" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Total Taxt Collection(in Cr)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="TotalTax" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsave_Click" />--%>
                                                        <asp:Button ID="BtnUpdate" Text="Update" OnClick="BtnUpdate_Click" runat="server" Visible="false" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnsave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfFormApproval_Id" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCancel" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="BtnUpdate" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlZone" />
                    
                </Triggers>
            </asp:UpdatePanel>
             <div class="container-fluid">
                 <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">ULB Fund</h4>
                                      
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                   <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                 <div style="overflow: auto">
                                                <asp:GridView runat="server" ID="grdPost" CssClass="display table table-bordered"  AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="State" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="ULBName" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                                                        <asp:BoundField HeaderText="SFC FUnd" DataField="SFCFund" />
                                                        <asp:BoundField HeaderText="CFC Fund" DataField="CFCFund" />
                                                        <asp:BoundField HeaderText="Total Taxt Collection" DataField="TotalTaxtCollection" />
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditULBFund" OnCommand="Edit_Command" CommandArgument='<%# Eval("ULBFundId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>

                                                             
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                           

                                                             <ItemTemplate>
                                                                <asp:Button ID="btnDelete" runat="server" Text='Delete' CommandName="DeleteULBFund" OnClientClick="return confirm('Are You Sure !!!')" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("ULBFundId") %>' CssClass="btn btn-danger drill_btn" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                         </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
             </div>
        </div>
    </div>
   
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

     <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.16.2/xlsx.full.min.js"></script>

  

    <script>
      
           

      

        function ExportToExcel(type, fn, dl) {
            debugger
            const currentDate = new Date();

            // Get the current date
            const year = currentDate.getFullYear();
            const month = currentDate.getMonth() + 1; // Months are zero-based
            const day = currentDate.getDate();

            // Format the date as desired (e.g., YYYY-MM-DD)
            const formattedDate = "ULB Fund Detail_" + `${year}-${month}-${day}`;

            var elt = document.getElementById('ctl00_ContentPlaceHolder1_GrdULBFund');

            // Clone the table to avoid modifying the original
            var clonedTable = elt.cloneNode(true);

            // Get all rows
            var rows = clonedTable.rows;

            // Remove the last two columns from each row
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].cells.length > 2) {
                    rows[i].deleteCell(-1); // Remove last cell
                    rows[i].deleteCell(-1); // Remove second to last cell
                }
            }

            var wb = XLSX.utils.table_to_book(clonedTable, { sheet: "sheet1" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || (formattedDate + "." + (type || 'xlsx')));
        }


    </script>

</asp:Content>
