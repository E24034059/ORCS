<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ViewExFile.aspx.cs" Inherits="Exercise_ViewExFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>瀏覽作品檔案</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManagerViewExFile" runat="server" />
    <div>
        <table width="99%" cellpadding="10px">
            <tr align="left">
                <td width="80%">
                    <asp:Label ID="lbExFileList" runat="server" Text="作品檔案列表" Font-Size="XX-Large" ForeColor="Blue" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlExList" runat="server" Width="160px" Height="30px" 
                        Font-Size="Large" onselectedindexchanged="ddlExList_SelectedIndexChanged" />  
                </td>
            </tr>
        </table>
        <div style="width:98%;height:700px;overflow-y:auto;border:3px double #808080;" >
        <asp:UpdatePanel ID="updatePanelViewExFile" runat="server">
            <ContentTemplate>
                <br />
                <asp:Timer ID="timerExFileList" runat="server" Interval="2000" ontick="timerExFileList_Tick" />
                <table id="tbExFileList" runat="server" style="border-collapse: collapse" width="90%" cellpadding="5" align="center" />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        <br />
        <table width="99%" cellpadding="10">
            <tr>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="關閉" Font-Size="X-Large" OnClientClick="window.close()"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
