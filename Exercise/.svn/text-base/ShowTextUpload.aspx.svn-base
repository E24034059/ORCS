<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowTextUpload.aspx.cs" Inherits="Exercise_ShowTextUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="dIsAnonymous" style="display:none;" runat="server">
        <asp:Label ID="lbIsAnonymous" runat="server" Font-Size="XX-Large" Text="作答者名稱是否匿名?" />&nbsp;&nbsp;
        <asp:RadioButtonList ID="rblIsAnonymous" runat="server" AutoPostBack="true"  Font-Size="XX-Large"  RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rblIsAnonymous_SelectedIndexChanged">
            <asp:ListItem Text="是      " Value="1" Selected="True"/>
            <asp:ListItem Text="否" Value="0" />
        </asp:RadioButtonList>
        </div>
        <asp:Label ID="lbUploadTextContent" runat="server" Font-Size="XX-Large" Text="上傳文字內容" /><br />
        <br />
        <div id="dContent" style="background-color:white;border-collapse:collapse;text-align:left;border:3px double #808080;height:600px;overflow-y:auto;" runat="server"></div>
        <br />
        <div style="text-align:center;">
            <asp:Button ID="btCancel" runat="server" Font-Size="X-Large" Text="關閉" OnClick="btCancel_Click" />
        </div>
    </div>
    </form>
</body>
</html>
