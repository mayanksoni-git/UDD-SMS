<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApiDataPage.aspx.cs" Inherits="ApiDataPage" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API Data Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnFetchData" runat="server" Text="Fetch Data" OnClick="btnFetchData_Click" />
            <br /><br />
            <asp:GridView ID="gvApiData" runat="server" AutoGenerateColumns="true" />
        </div>
    </form>
</body>
</html>


