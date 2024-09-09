<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApiDataPage3.aspx.cs" Inherits="ApiDataPage3" Async="true" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store JSON Data</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <asp:FileUpload ID="fileUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload and Store Data" OnClick="btnUpload_Click" />
        </div>
    </form>
</body>
</html>


