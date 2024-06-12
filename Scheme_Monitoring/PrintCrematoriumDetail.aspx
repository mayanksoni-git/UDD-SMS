<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCrematoriumDetail.aspx.cs" Inherits="PrintCrematoriumDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Crematorium Detail</title>
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
                <h3>Crematorium Detail</h3>
            </div>
            <%--Same grid is used on Page RptCrematoriumDetail.aspx ExportToExcelCrematoriumDetail.aspx page also, please change there as well if needed--%>
            <asp:GridView ID="gvCrematoriumDetail" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Zone" DataField="ZoneName" />--%>
                    <%--<asp:BoundField HeaderText="District" DataField="CircleName" />--%>
                    <asp:BoundField HeaderText="ULB" DataField="DivisionName" />
                    <%--<asp:BoundField HeaderText="Year" DataField="Year" />
                    <asp:BoundField HeaderText="Month" DataField="MonthName" />--%>
                    <asp:BoundField HeaderText="Name of Crematorium" DataField="NameCMTR" />

                    <asp:BoundField HeaderText="Location of Crematorium" DataField="LocationCMTR" />
                    <%--<asp:BoundField HeaderText="No of Pyres in Crematorium" DataField="NoOfPyres" />--%>
                    <asp:BoundField HeaderText="Conventional + Improvised Wood + Gas + Electric =Total No of Pyres in Crematorium" DataField="PyresDetail" />

                    <asp:TemplateField HeaderText="Drinking Water Facility">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("DrinkingWater")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is the facility connected to Electricity Grid">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("ElecticityGrid")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Parking Space">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("Parking")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Shed for pyres">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("Shed")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hearse">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("Hearse")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hand Pump">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("HandPump")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Boundary Wall">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("BoundaryWall")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Entry Gate">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("EntryGate")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Waiting/Prayer Hall">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("PrayerHall")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Office/Care Taker Room">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("CareTakerRoom")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ash Storage Rack">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("AshStorage")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bathroom for Bathing">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("Bathrooms")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is Washroom facility available ?">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("Washroom")) ? "Yes" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Does Registration Happens ?">
                        <ItemTemplate>
                            <%# 
                                Eval("Registration").ToString() == "1" ? "Yes" : 
                                Eval("Registration").ToString() == "2" ? "Other" : 
                                Eval("Registration").ToString() == "0" ? "No" : "Unknown"
                            %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name of Filler" DataField="FillerName" />
                    <asp:BoundField HeaderText="Contact No of Filler" DataField="FillerContact" />
                    <asp:BoundField HeaderText="Total no. of Cremations done from 1/Jan/2023 - 31/Dec/2023" DataField="TotalCMTRDone" />
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

