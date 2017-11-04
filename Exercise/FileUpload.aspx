<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="Exercise_FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>作品檔案上傳</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="left" cellpadding="10px">
            <tr align="left">
                <td>
                    <asp:Label ID="lbExFile" runat="server" Text="請選擇要上傳的作品檔案:" Font-Size="XX-Large" ForeColor="Blue" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbUploadYet" runat="server" Text="已經上傳過檔案，若重新上傳將覆蓋原始檔案" Font-Size="Large" ForeColor="Red" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="fuExFile" runat="server" Width="580px" Font-Size="X-Large" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="上傳" Font-Size="X-Large" OnClick="btnUpload_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="X-Large" OnClientClick="window.close()"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
