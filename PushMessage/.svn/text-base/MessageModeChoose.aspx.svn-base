<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageModeChoose.aspx.cs" Inherits="PushMessage_MessageModeChoose" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Choose Message Mode</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="99%" cellpadding="15" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="lbModeTitle" runat="server" Text="請選擇推播訊息模式:" Font-Size="XX-Large"></asp:Label>
                </td>
            </tr>
            <tr id="trModeChoose" runat="server">
                <td>
                    <asp:RadioButtonList ID="rblModeChoose" runat="server" Font-Size="X-Large" 
                        AutoPostBack="True" onselectedindexchanged="rblModeChoose_SelectedIndexChanged">
                        <asp:ListItem Value="CreateTempMessageGroup">建立推播訊息群組</asp:ListItem>
                        <asp:ListItem Value="EnterTempMessageGroup" Selected="True">加入推播訊息群組</asp:ListItem>
                        <asp:ListItem Value="SendMessage">傳送推播訊息</asp:ListItem>
                        <asp:ListItem Value="ViewMessage">觀看推播訊息</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnConfirm" runat="server" Text="下一步" Font-Size="Medium" 
                        onclick="btnConfirm_Click" CommandName="cnEnterTempMessageGroup" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="Medium" OnClientClick="window.close()"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

