<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectGroup.aspx.cs" Inherits="Administrator_SelectGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="text-align:left">
                    <asp:TreeView ID="tvGroup" runat="server" ImageSet="Arrows"
                        OnSelectedNodeChanged="tvGroup_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="Red"
                            HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="24pt" ForeColor="Black"
                            HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
        <div style="text-align:right"><input type="button" value="取消" style="font-size: x-large;" onclick="window.close();" runat="server" /></div>
    </form>
</body>
</html>
