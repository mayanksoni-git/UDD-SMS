<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApiDataPage2.aspx.cs" Inherits="ApiDataPage2"  Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API Data to Table</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnGetData" runat="server" Text="Get API Data" OnClick="btnGetData_Click" />
            <br /><br />
            <asp:GridView ID="gvApiData" runat="server" AutoGenerateColumns="true"></asp:GridView>
        </div>
    </form>
</body>
</html>
