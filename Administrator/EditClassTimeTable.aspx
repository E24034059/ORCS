<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="EditClassTimeTable.aspx.cs" Inherits="Administrator_EditClassTimeTable" Title="編輯課表"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" Runat="Server">
    <!--行事曆-->
    <table width="100%" runat="server" cellpadding="5">
        <tr>
            <td align="center">
                <asp:ImageButton ID="ibtnColl" runat="server" 
                    ImageUrl="~/App_Themes/general/images/collapse.gif" Height="15px" 
                    Width="20px" onclick="ibtnColl_Click" />
                <asp:ImageButton ID="ibtnExp" runat="server" 
                    ImageUrl="~/App_Themes/general/images/expand.gif" Height="15px" 
                    Width="20px" Visible="false" onclick="ibtnExp_Click" />
                <asp:Label ID="lbCalendar" runat="server" Text="行事曆" Font-Size="Large"></asp:Label>    
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Calendar ID="calClassTimeTable" runat="server" BorderColor="Black" 
                    FirstDayOfWeek="Monday" onselectionchanged="calClassTimeTable_SelectionChanged">
                    <TodayDayStyle ForeColor="White" BackColor="#498aff" />
                    <OtherMonthDayStyle ForeColor="#CCCCCC" />
                </asp:Calendar>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnAddNewClassTime" runat="server" 
                    CssClass="ORCS_button_control" Text="新增課程" OnClientClick="window.showModalDialog('../Administrator/AddNewClassTime.aspx',window,'dialogWidth:620px;dialogHeight:680px;')" />
            </td>
        </tr>
    </table>
    <!--課程時間表-->
    <table ID="tbClassTimeTable" runat="server" align="center" 
        style="border-collapse:collapse; font-size: large;" cellpadding="10">
        <tr align="center">
            <td id="tdNone" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
            </td>
            <td id="tdMonday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbMonday" runat="server" Text="星期一"></asp:Label>
            </td>
            <td id="tdTuesday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbTuesday" runat="server" Text="星期二"></asp:Label>
            </td>
            <td id="tdWednesday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbWednesday" runat="server" Text="星期三"></asp:Label>
            </td>
            <td id="tdThursday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbThursday" runat="server" Text="星期四"></asp:Label>
            </td>
            <td id="tdFriday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbFriday" runat="server" Text="星期五"></asp:Label>
            </td>
            <td id="tdSaturday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbSaturday" runat="server" Text="星期六"></asp:Label>
            </td>
            <td id="tdSunday" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbSunday" runat="server" Text="星期日"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td id="tdOne" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbOne" runat="server" Text="第一節<br/>8:10~9:00"></asp:Label>
            </td>
            <td id="td_Monday_One" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_One" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_One" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_One" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_One" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_One" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_One" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdTwo" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbTwo" runat="server" Text="第二節<br/>9:10~10:00"></asp:Label>
            </td>
            <td id="td_Monday_Two" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Two" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Two" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Two" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Two" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Two" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Two" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdThree" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbThree" runat="server" Text="第三節<br/>10:10~11:00"></asp:Label>
            </td>
            <td id="td_Monday_Three" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Three" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Three" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Three" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Three" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Three" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Three" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdFour" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbFour" runat="server" Text="第四節<br/>11:10~12:00"></asp:Label>
            </td>
            <td id="td_Monday_Four" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Four" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Four" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Four" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Four" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Four" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Four" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdNoom" runat="server" style="border: 2px solid black" bgcolor="White" colspan="8">
                <asp:Label ID="lbNoom" runat="server" Text="午休"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td id="tdFive" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbFive" runat="server" Text="第五節<br/>14:10~15:00"></asp:Label>
            </td>
            <td id="td_Monday_Five" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Five" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Five" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Five" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Five" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Five" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Five" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdSix" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbSix" runat="server" Text="第六節<br/>15:10~16:00"></asp:Label>
            </td>
            <td id="td_Monday_Six" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Six" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Six" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Six" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Six" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Six" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Six" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdSeven" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbSeven" runat="server" Text="第七節<br/>16:10~17:00"></asp:Label>
            </td>
            <td id="td_Monday_Seven" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Seven" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Seven" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Seven" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Seven" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Seven" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Seven" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdEight" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbEight" runat="server" Text="第八節<br/>17:10~18:00"></asp:Label>
            </td>
            <td id="td_Monday_Eight" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Eight" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Eight" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Eight" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Eight" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Eight" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Eight" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdNine" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbNine" runat="server" Text="第九節<br/>18:10~19:00"></asp:Label>
            </td>
            <td id="td_Monday_Nine" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_Nine" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_Nine" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_Nine" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_Nine" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_Nine" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_Nine" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdA" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbA" runat="server" Text="第A節<br/>19:10~20:00"></asp:Label>
            </td>
            <td id="td_Monday_A" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_A" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_A" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_A" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_A" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_A" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_A" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdB" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbB" runat="server" Text="第B節<br/>20:10~21:00"></asp:Label>
            </td>
            <td id="td_Monday_B" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_B" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_B" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_B" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_B" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_B" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_B" runat="server" style="border: 2px solid black">
            </td>
        </tr>
        <tr align="center">
            <td id="tdC" runat="server" style="border: 2px solid black" bgcolor="#CCCCCC">
                <asp:Label ID="lbC" runat="server" Text="第C節<br/>21:10~22:00"></asp:Label>
            </td>
            <td id="td_Monday_C" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Tuesday_C" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Wednesday_C" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Thursday_C" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Friday_C" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Saturday_C" runat="server" style="border: 2px solid black">
            </td>
            <td id="td_Sunday_C" runat="server" style="border: 2px solid black">
            </td>
        </tr>
    </table>
    <table align="right">
        <asp:Button ID="btnBackHomepage" runat="server" CssClass="ORCS_button_control" Text="返回首頁" PostBackUrl="../Homepage/Homepage.aspx" />
    </table>
</asp:Content>