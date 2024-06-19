<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="ExportToExcel.aspx.cs" Inherits="ExportToExcel" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
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
                    <td><%# Eval("CircleName")%> <%# Eval("MonthName")%> <%# Eval("Year")%></td>
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
</asp:Content>
