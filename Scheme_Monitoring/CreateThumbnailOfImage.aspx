<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateThumbnailOfImage.aspx.cs" Inherits="CreateThumbnailOfImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Upload and Create Thumbnail" OnClick="Button1_Click" />
            <asp:Image ID="ThumbnailImage" runat="server" />
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
