<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Attendance_Attendance" Title="出席狀況" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" Runat="Server">
    <asp:UpdatePanel ID="upalAttendanceCond" runat="server">
        <ContentTemplate>
            <asp:Timer ID="timAttendanceCond" runat="server" ontick="timAttendanceCond_Tick" Interval="1000">
            </asp:Timer>
            <table ID="tbAttCond" runat="server" align="center" style="font-size: large">
                <tr>
                    <td align="center">
                        <asp:Label ID="lbExceptAtt" runat="server" Text="&nbsp;&nbsp;&nbsp;應到人數: 0 人"></asp:Label> |
                        <asp:Label ID="lbActualAtt" runat="server" Text="<img src='../App_Themes/general/images/online.png' width='25px' />實到人數: 0 人" ForeColor="Green"></asp:Label> |
                        <asp:Label ID="lbLateAtt" runat="server" Text="<img src='../App_Themes/general/images/late.png' width='20px' />遲到人數: 0 人" ForeColor="Purple"></asp:Label> |
                        <asp:Label ID="lbAbsenceAtt" runat="server" Text="<img src='../App_Themes/general/images/offline.png' width='20px' />未到人數: 0 人" ForeColor="Red"></asp:Label> | 
                        <asp:Label ID="lbLeaveEarlyAtt" runat="server" Text="<img src='../App_Themes/general/images/leaveEarly.png' width='20px' />早退人數: 0 人" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table align="center">
                <tr>
                    <td>
                         <asp:Button ID="btnReRollCall" runat="server" CssClass="ORCS_button_control" Text="重新點名" onclick="btnReRollCall_Click" /> 
                    </td>

                </tr>
            </table>
            <br />
            <table ID="tbAttDetialCond" runat="server" align="center" 
                style="border-collapse:collapse; font-size: large;" cellpadding="10">
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:Panel ID="palButtonField" runat="server" HorizontalAlign="Right">
        <asp:Button ID="btnBackHomepage" runat="server" CssClass="ORCS_button_control" Text="返回首頁" PostBackUrl="../Homepage/Homepage.aspx" />
    </asp:Panel>
</asp:Content>