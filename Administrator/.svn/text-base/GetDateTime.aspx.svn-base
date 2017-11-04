<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetDateTime.aspx.cs" Inherits="Administrator_GetDateTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>取得日期</title>
    <base target="_self" />
    <script type="text/javascript">
    function ReturnDateTime()
    {
        window.returnValue = document.getElementById('hfDateTime').value;
        window.close();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <asp:Calendar ID="calDateTime" runat="server" BackColor="White" BorderColor="White"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="16pt" 
            ForeColor="Black" Height="190px"
            NextPrevFormat="FullMonth" Width="100%" 
            onselectionchanged="calDateTime_SelectionChanged" FirstDayOfWeek="Monday">
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True"
                Font-Size="12pt" ForeColor="#333399" />
        </asp:Calendar>
        <asp:HiddenField ID="hfDateTime" runat="server" />
    </table>
    <table align="center" cellpadding="10">
        <tr>
            <td>
                <asp:Button ID="btnConfirm" runat="server" Text="確定" Font-Size="Large" OnClientClick="ReturnDateTime()" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="Large" OnClientClick="window.close()" />
            </td>
        </tr>
    <table>
    </form>
</body>
</html>
