<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateTable.aspx.cs" Inherits="GenerateTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="border-color:black; border-style: solid; width: 100%;">
                <tr>
                    <th rowspan="4" style="background-color: #CFE2F3">Edit</th>
                    <th style="background-color: #76A5AF">A</th>
                    <th colspan="6" style="background-color: #F1C232">B</th>
                    <th colspan="5" style="background-color: #F1C232">C</th>
                    <th colspan="2" style="background-color: #76A5AF">C2</th>
                    <th colspan="3" style="background-color: #E69138">D1</th>
                    <th colspan="2" style="background-color: #E69138">D3</th>
                    <th colspan="2" style="background-color: #76A5AF">E</th>
                </tr>
                <tr>
                    <th rowspan="3" style="background-color: #CFE2F3">Sr.No.</th>
                    <th colspan="6" style="background-color: #C9DAF8">City Profile</th>
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
                    <th rowspan="2" style="background-color: #D9D2E9">Conventional</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Improvised Wood</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Gas</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Electric </th>
                    <th rowspan="2" style="background-color: #D9D2E9">Existing &#39;mortal remains&#39; handling capacity**</th>
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
                    <th style="background-color: #A4C2F4">Funds required in lacs (only includes pyres and not other facilities)</th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
