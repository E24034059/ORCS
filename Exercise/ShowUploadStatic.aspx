<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowUploadStatic.aspx.cs" Inherits="Exercise_ShowUploadStatic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbExFileList" runat="server" Text="作品檔案列表" Font-Size="XX-Large" ForeColor="Blue" />
        <br /><br />
        <table id="tbExFileList" align="center" runat="server" style="border-collapse: collapse" width="90%" cellpadding="5"/>
    </div>
    </form>
</body>
</html>
