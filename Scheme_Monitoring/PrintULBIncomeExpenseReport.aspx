<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintULBIncomeExpenseReport.aspx.cs" Inherits="PrintULBIncomeExpenseReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print ULB Income Expense Report</title>
    <style>
        @media print {
            body, table {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
                width: 100%;
                height: 100%;
            }
            table {
                page-break-inside: auto;
            }
            tr {
                page-break-inside: avoid;
                page-break-after: auto;
            }
            th{
                background:#000;
                text-align:left;
            }
            @page {
                size: A4 landscape;
                margin: 10mm;
            }
        }

        .print-container {
            width: 100%;
            margin: auto;
            text-align: center;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-view th, .grid-view td {
            border: 1px solid #000;
            padding: 8px;
            text-align: left;
        }

        .btn-print {
            margin-top: 20px;
        }
        .form_center{
            display:flex;
            justify-content:center;
            align-items:center;
        }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div class="p-2 mt-1 border">        <div class="print-container">
            <div class="header">
                <img src="assets/images/logo-dark.png" alt="Organization Logo" />
                <h1>Schemes Monitoring System</h1>
                <h2>Urban Development Department, Government of Uttar Pradesh</h2>
                <h3>ULB Income Expenditure</h3>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <strong>Search Criteria :</strong>
                </div>
                <div class="col-xxl-6 col-sm-6">
                    <strong>State :</strong><span id="State" runat="server"></span>
                </div>
                <div class="col-xxl-6 col-sm-6">
                    <strong>District :</strong><span id="dist" runat="server"></span>
                </div>
                <div class="col-xxl-6 col-sm-6">
                    <strong>ULB Name :</strong><span id="uname" runat="server"></span>
                </div>
                <div class="col-xxl-6 col-sm-6">
                    <strong>ULBType :</strong><span id="utype" runat="server"></span>
                </div>
                <div class="col-xxl-6 col-sm-6">
                    <strong>Financial year :</strong><span id="fyr" runat="server"></span>
                </div>
            </div>
            <%--Same grid is used on Page RptPyresTracker.aspx and ExportToExcel.aspx page also, please change there as well if needed--%>
            <div class="form_center">
          <asp:Repeater ID="rptSearchResult" runat="server" >
                                                        <HeaderTemplate>
                                                         
                                                            <table id="sample-table-2" class="mt-5 table table-striped table-bordered table-hover">

                                                                <thead style="background:#f28a2b;">
                                                                    <tr class="table-primary">
                                                                       <th style="text-align:left"  rowspan="2">Sr</th>
                                                                        <th style="text-align:left"  rowspan="2">
                                                                           District
                                                                        </th>
                                                                        <th style="text-align:left"  rowspan="2">
                                                                            ULB Type
                                                                        </th>
                                                                        <th style="text-align:left" rowspan="2">
                                                                            ULB Name
                                                                        </th>
                                                                        <th style="text-align:right"  rowspan="2">
                                                                           Income (in Lacs)
                                                                        </th>
                                                                        <th align="center" colspan="3">
                                                                            Expenditure  
                                                                              
                                                                        </th>
                                                                        <th style="text-align:right"  rowspan="2">
                                                                           Balance Amount(in Lacs)
                                                                        </th>

                                                                    </tr>
                                                            <tr class=" table-primary">
                                                                <th style="text-align:right" >New Expense(in Lacs)</th>
                                                                <th style="text-align:right" >Maintenance Expense(in Lacs)</th>
                                                                 <th style="text-align:right" >Total Expense(in Lacs)</th>
                                                            </tr>
                                                                </thead>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                              <td>
    <span>1</span> 
</td>
                                                                <td align="left">
                                                                    <%--.HindiName,d.EnglishName as distEng,d.HindiName as distHindi--%>
                                                                    <%#DataBinder.Eval(Container,"DataItem.Circle_Name")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#DataBinder.Eval(Container,"DataItem.Division_Type")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#DataBinder.Eval(Container,"DataItem.ULBNAme")%>
                                                                </td>
                                                                <td style="text-align:right" >
                                                                    <%#DataBinder.Eval(Container,"DataItem.TotalIncome")%>
                                                                </td>
                                                                <td style="text-align:right" >
                                                                    <%#DataBinder.Eval(Container,"DataItem.NewExpense")%>
                                                                </td>
                                                                <td style="text-align:right" >
                                                                    <%#DataBinder.Eval(Container,"DataItem.MaintenanceExpense")%>
                                                                </td>
                                                                <td style="text-align:right" >
                                                                    <%#DataBinder.Eval(Container,"DataItem.TotalExpense")%>
                                                                </td>
                                                                <td style="text-align:right" >
                                                                    <%#DataBinder.Eval(Container,"DataItem.BalanceAmn")%>
                                                                </td>




                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
     <%--       <asp:Button ID="Button1" runat="server" Text="Print" CssClass="btn-print" OnClientClick="printPage()" />--%>
                </div>
        </div>
          </div>
    </form>
    <script>
        function printPage() {
            window.print();
            return false;
        }
    </script>
</body>
</html>

