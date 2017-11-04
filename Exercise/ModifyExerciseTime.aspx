<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyExerciseTime.aspx.cs" Inherits="Exercise_ModifyExerciseTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <base target="_self" />
    <title>修改作答時間</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <table width="99%" cellpadding="10" cellspacing="5">
        <tr align="left">
            &nbsp;&nbsp;
            <asp:Label ID="lbModifyExerciseTime" runat="server" Text="修改作答時間:" Font-Size="XX-Large"></asp:Label>
        </tr>
    </table>
    <table width="99%" cellpadding="10" cellspacing="5">
        <tr align="left">
            <asp:UpdatePanel ID="upaltimRemnantTime" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="timRemnantTime" runat="server" ontick="timRemnantTime_Tick" Interval="1000" />
                     <br />&nbsp;&nbsp;
                    <asp:Label ID="lbTimeRemnantTitle" runat="server" Text="目前剩餘時間:" Font-Size="X-Large"></asp:Label>
                    <asp:Label ID="lbTimeRemnant" runat="server" Text="00'00'00" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </tr>
    </table>
    <table width="99%" cellpadding="10" cellspacing="5">
        <tr align="left">
            <br />&nbsp;&nbsp;
            <asp:Label ID="lbAddExerciseTime" runat="server" Text="增加作答時間:" Font-Size="X-Large"></asp:Label>
            <asp:DropDownList ID="ddlExerciseTime_Min" runat="server" Font-Size="Large" Width="60px">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="40">40</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
                <asp:ListItem Value="60">60</asp:ListItem>
                <asp:ListItem Value="100">100</asp:ListItem>
                <asp:ListItem Value="120">120</asp:ListItem>
                <asp:ListItem Value="200">200</asp:ListItem>
                <asp:ListItem Value="240">240</asp:ListItem>
                <asp:ListItem Value="300">300</asp:ListItem>
                <asp:ListItem Value="1000">1000</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lbMinute" runat="server" Text="分" Font-Size="X-Large"></asp:Label>
            <asp:DropDownList ID="ddlExerciseTime_Sec" runat="server" Font-Size="Large" Width="60px">
                <asp:ListItem Value="0">0</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="40">40</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lbSecond" runat="server" Text="秒" Font-Size="X-Large"></asp:Label>
        </tr>
        <tr align="center">
            <td>
                <asp:Button ID="btnConfirm" runat="server" Text="確定" Font-Size="Large" OnClick="btnConfirm_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="Large" OnClientClick="window.close()"/>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
