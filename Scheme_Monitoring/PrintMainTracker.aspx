<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMainTracker.aspx.cs" Inherits="PrintMainTracker" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Crematorium Main Trackery</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="print-container">
            <div class="header">
                <img src="assets/images/logo-dark.png" alt="Organization Logo" />
                <h1>Schemes Monitoring System</h1>
                <h2>Urban Development Department, Government of Uttar Pradesh</h2>
                <h3>Crematorium Main Tracker</h3>
            </div>
            <%--Same grid is used on Page RptPyresTracker.aspx and ExportToExcel.aspx page also, please change there as well if needed--%>
            <asp:GridView ID="ExportGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <%--<asp:BoundField DataField="PyresTracker_Id" HeaderText="Pyres Tracker Id">
                <HeaderStyle CssClass="displayStyle" />
                <ItemStyle CssClass="displayStyle" />
                <FooterStyle CssClass="displayStyle" />
            </asp:BoundField>--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <tr>
                                <th rowspan="4"></th>
                                <th style="background-color: #76A5AF">A</th>
                                <th colspan="7" style="background-color: #F1C232">B</th>
                                <th colspan="5" style="background-color: #F1C232">C</th>
                                <th colspan="2" style="background-color: #76A5AF">C2</th>
                                <th colspan="3" style="background-color: #E69138">D1</th>
                                <th colspan="2" style="background-color: #E69138">D3</th>
                                <th colspan="2" style="background-color: #76A5AF">E</th>
                            </tr>
                            <tr>
                                <th rowspan="3" style="background-color: #CFE2F3">Sr.No.</th>
                                <th colspan="7" style="background-color: #C9DAF8">City Profile</th>
                                <th colspan="5" style="background-color: #D9D2E9">No. of existing pyres (excluding under construction) as per city administration</th>
                                <th colspan="2" style="background-color: #C9DAF8">Decision for next step</th>
                                <th colspan="3" style="background-color: #D9D2E9">Upgradation of existing conventional pyres</th>
                                <th colspan="2" style="background-color: #D9D2E9">Current status for installation of EFPs</th>
                                <th colspan="2" style="background-color: #00FFFF">Final output</th>
                            </tr>
                            <tr>
                                <th rowspan="2" style="background-color: #C9DAF8">District</th>
                                <th rowspan="2" style="background-color: #C9DAF8">ULB</th>
                                <th rowspan="2" style="background-color: #C9DAF8">Total urban population</th>
                                <th rowspan="2" style="background-color: #C9DAF8">Population likely to be cremated (80%)</th>
                                <th rowspan="2" style="background-color: #C9DAF8">Death rate per 1000 per year</th>
                                <th rowspan="2" style="background-color: #C9DAF8">Estimated no. of deaths per day (incl 10% buffer)</th>
                                <th rowspan="2" style="background-color: #C9DAF8">No of Existing Crematorium</th>
                                <th rowspan="2" style="background-color: #D9D2E9">Conventional</th>
                                <th rowspan="2" style="background-color: #D9D2E9">Improvised Wood</th>
                                <th rowspan="2" style="background-color: #D9D2E9">Gas</th>
                                <th rowspan="2" style="background-color: #D9D2E9">Electric </th>
                                <th rowspan="2" style="background-color: #D9D2E9">Existing &#39;mortal remains&#39; handling capacity</th>
                                <th rowspan="2" style="background-color: #C9DAF8">Decision: Upgrade existing or build new dependent on existing capacity</th>
                                <th rowspan="2" style="background-color: #C9DAF8">Total estimated deaths per day - Existing &#39;mortal remains&#39; handling capacity of EFCs = Remaining &#39;mortal remains&#39; to be handled</th>
                                <th style="background-color: #D9D2E9">Improvised Wood </th>
                                <th style="background-color: #D9D2E9">Gas </th>
                                <th style="background-color: #D9D2E9">Electric </th>
                                <th rowspan="2" style="background-color: #C9DAF8">Remaining capacity (ideally should be negative)</th>
                                <th rowspan="2" style="background-color: #D9D2E9">Comment on capacity</th>
                                <th colspan="2" style="background-color: #A4C2F4">Upgrade existing ones</th>
                            </tr>
                            <tr>
                                <th style="background-color: #D9D2E9">2</th>
                                <th style="background-color: #D9D2E9">4</th>
                                <th style="background-color: #D9D2E9">4</th>
                                <th style="background-color: #A4C2F4">Number of conventional pyres to be revamped</th>
                                <th style="background-color: #A4C2F4">Funds required in Lakhs (only includes pyres and not other facilities)</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td><%# Container.DataItemIndex + 1 %></td>
                            <td><%# Eval("CircleName")%></td>
                            <td><%# Eval("DivisionName")%></td>
                            <td><%# Eval("UrbanPopulation")%></td>
                            <td><%# Eval("PopulationCreamtion80")%></td>
                            <td><%# Eval("DeathPer1000")%></td>
                            <td><%# Eval("EstDeath10Buffer")%></td>
                            <td><%# Eval("ExistCMTR")%></td>
                            <td><%# Eval("Conventional")%></td>
                            <td><%# Eval("ImprovisedWood")%></td>
                            <td><%# Eval("Gas")%></td>
                            <td><%# Eval("Electric")%></td>
                            <td><%# Eval("ExistCapacity")%></td>
                            <td><%# Eval("UpgradeExisting")%></td>
                            <td><%# Eval("RemainingToBeHandled")%></td>
                            <td><%# Eval("UpgradeImprovisedWood")%></td>
                            <td><%# Eval("UpgradeGas")%></td>
                            <td><%# Eval("UpgradeElectric")%></td>
                            <td><%# Eval("RemainingCapacity")%></td>
                            <td><%# Eval("CommentOnCapacity")%></td>
                            <td><%# Eval("PyresToBeRevamped")%></td>
                            <td><%# Eval("FundsRequired")%></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="Button1" runat="server" Text="Print" CssClass="btn-print" OnClientClick="printPage()" />
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

