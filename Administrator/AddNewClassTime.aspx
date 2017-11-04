<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewClassTime.aspx.cs" Inherits="Administrator_AddNewClassTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增課程</title>
    <base target="_self" />
    <script type="text/javascript">
    //取得課程開始日期
    function SelectClassStartDate()
    {
        var DateTime;
        DateTime = window.showModalDialog('../Administrator/GetDateTime.aspx',window,'dialogWidth:500px;dialogHeight:300px;');
        if (DateTime != undefined)
            document.getElementById('tbClassStartDate').value = DateTime;
    }
    //取得課程結束日期
    function SelectClassEndDate()
    {
        var DateTime;
        DateTime = window.showModalDialog('../Administrator/GetDateTime.aspx',window,'dialogWidth:500px;dialogHeight:300px;');
        if (DateTime != undefined)
            document.getElementById('tbClassEndDate').value = DateTime;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br/>
    <table width="150">
        <asp:Label ID="lbAddClass" runat="server" Text="新增課程:" Font-Size="XX-Large" Font-Bold="True"></asp:Label>
    </table>
    <table cellpadding="18">
        <tr>
            <td align="left">
                <asp:Label ID="lbDepartment" runat="server" Text="系所:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlDepartment" runat="server" Font-Size="XX-Large" 
                    AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged" Width="230">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbClass" runat="server" Text="課程:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlClass" runat="server" Font-Size="XX-Large" Width="230">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbClassStartDate" runat="server" Text="上課日期:" Font-Size="XX-Large"></asp:Label>
                <asp:TextBox ID="tbClassStartDate" runat="server" Font-Size="X-Large" Width="150px"></asp:TextBox>
                <asp:Label ID="lbDateTimeForm" runat="server" Text="年/月/日" Font-Size="X-Large" />
                <asp:Button ID="btnClassStartDate" runat="server" Text="選擇日期" 
                    Font-Size="X-Large" OnClientClick="SelectClassStartDate()" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbClassEndDate" runat="server" Text="上課期限:" Font-Size="XX-Large"></asp:Label>
                <asp:TextBox ID="tbClassEndDate" runat="server" Font-Size="X-Large" Width="150px"></asp:TextBox>
                <asp:Label ID="lbDateTimeForm2" runat="server" Text="年/月/日" Font-Size="X-Large" />
                <asp:Button ID="btnClassEndDate" runat="server" Text="選擇日期" 
                    Font-Size="X-Large" OnClientClick="SelectClassEndDate()" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbRepeat" runat="server" Text="重複:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlRepeat" runat="server" Font-Size="XX-Large" Width="120">
                    <asp:ListItem Value="None" Selected="True">無</asp:ListItem>
                    <asp:ListItem Value="Day">每天</asp:ListItem>
                    <asp:ListItem Value="Week">每周</asp:ListItem>
                    <asp:ListItem Value="Month">每月</asp:ListItem>
                    <asp:ListItem Value="Year">每年</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbClassStartTime" runat="server" Text="起始時間:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlClassStartTime" runat="server" Font-Size="XX-Large" Width="330">
                <asp:ListItem Value="One" Selected="True">第一節(8:10~9:00)</asp:ListItem>
                <asp:ListItem Value="Two">第二節(9:10~10:00)</asp:ListItem>
                <asp:ListItem Value="Three">第三節(10:10~11:00)</asp:ListItem>
                <asp:ListItem Value="Four">第四節(11:10~12:00)</asp:ListItem>
                <asp:ListItem Value="Five">第五節(14:10~15:00)</asp:ListItem>
                <asp:ListItem Value="Six">第六節(15:10~16:00)</asp:ListItem>
                <asp:ListItem Value="Seven">第七節(16:10~17:00)</asp:ListItem>
                <asp:ListItem Value="Eight">第八節(17:10~18:00)</asp:ListItem>
                <asp:ListItem Value="Nine">第九節(18:10~19:00)</asp:ListItem>
                <asp:ListItem Value="A">第A節(19:10~20:00)</asp:ListItem>
                <asp:ListItem Value="B">第B節(20:10~21:00)</asp:ListItem>
                <asp:ListItem Value="C">第C節(21:10~22:00)</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbClassEndTime" runat="server" Text="結束時間:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlClassEndTime" runat="server" Font-Size="XX-Large" Width="330">
                <asp:ListItem Value="One" Selected="True">第一節(8:10~9:00)</asp:ListItem>
                <asp:ListItem Value="Two">第二節(9:10~10:00)</asp:ListItem>
                <asp:ListItem Value="Three">第三節(10:10~11:00)</asp:ListItem>
                <asp:ListItem Value="Four">第四節(11:10~12:00)</asp:ListItem>
                <asp:ListItem Value="Five">第五節(14:10~15:00)</asp:ListItem>
                <asp:ListItem Value="Six">第六節(15:10~16:00)</asp:ListItem>
                <asp:ListItem Value="Seven">第七節(16:10~17:00)</asp:ListItem>
                <asp:ListItem Value="Eight">第八節(17:10~18:00)</asp:ListItem>
                <asp:ListItem Value="Nine">第九節(18:10~19:00)</asp:ListItem>
                <asp:ListItem Value="A">第A節(19:10~20:00)</asp:ListItem>
                <asp:ListItem Value="B">第B節(20:10~21:00)</asp:ListItem>
                <asp:ListItem Value="C">第C節(21:10~22:00)</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table align="center" cellpadding="15">
        <tr>
            <td>
                <asp:Button ID="btnConfirm" runat="server" Text="確定" Font-Size="X-Large" 
                    onclick="btnConfirm_Click" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="X-Large" OnClientClick="window.close()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
