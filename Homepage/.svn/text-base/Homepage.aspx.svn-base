<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage_Homepage" Title="首頁"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="upalGetMessage" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Timer ID="timGetMessage" runat="server" Interval="100000"  ontick="timGetMessage_Tick"/>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timGetMessage" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" Runat="Server">
    <br/><br/><br/><br/>
    <table style="width: 100%;" cellpadding="10">
        <tr>
            <td align="center">
                <asp:Button ID="btnOpenClass" runat="server" CssClass="ORCS_menu_button" 
                    Text="上課" onclick="btnOpenClass_Click" 
                    onmouseover="style.fontWeight='bold';style.backgroundColor='lightgreen'" onmouseout="style.fontWeight='';style.backgroundColor='lightblue'" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnAttendance" runat="server" CssClass="ORCS_menu_button" 
                    Text="出席狀況" onclick="btnAttendance_Click" 
                    onmouseover="style.fontWeight='bold';style.backgroundColor='lightgreen'" onmouseout="style.fontWeight='';style.backgroundColor='lightblue'" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnExercise" runat="server" CssClass="ORCS_menu_button" 
                    Text="課堂練習" onclick="btnExercise_Click" 
                    onmouseover="style.fontWeight='bold';style.backgroundColor='lightgreen'" onmouseout="style.fontWeight='';style.backgroundColor='lightblue'" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnCloseClass" runat="server" CssClass="ORCS_menu_button" 
                    Text="下課" onclick="btnCloseClass_Click" 
                    onmouseover="style.fontWeight='bold';style.backgroundColor='lightgreen'" onmouseout="style.fontWeight='';style.backgroundColor='lightblue'" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnEditGroup" runat="server" CssClass="ORCS_menu_button" 
                    Text="群組管理" onclick="btnEditGroup_Click" 
                    onmouseover="style.fontWeight='bold';style.backgroundColor='lightgreen'" onmouseout="style.fontWeight='';style.backgroundColor='lightblue'" />
            </td>
        </tr>
    </table>
    <!--班級選擇區-->
    <div id="divChoiceClass" runat="server" style="border: medium solid #000000; background-color: #CCCCCC; position: absolute;
        height: 250px; width: 350px; top: 85px; left: 360px;" visible="false">
        <asp:Label ID="lbChoiceClass" runat="server" Text="請選擇上課班級:" Font-Size="X-Large" Font-Bold="True" />
        <br />
        <table cellpadding="10">
            <tr>
                <td>
                    <asp:Label ID="lbDepartment" runat="server" Text="系所:" Font-Size="X-Large"></asp:Label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" Font-Size="Large" 
                        AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbClass" runat="server" Text="班級:" Font-Size="X-Large"></asp:Label>
                    <asp:DropDownList ID="ddlClass" runat="server" Font-Size="Large">
                    </asp:DropDownList>
                <td>    
            </tr>
        </table>
        <table align="center" cellpadding="20">
            <tr>
                <td><asp:Button ID="btnConfirm" runat="server" Text="確定" Font-Size="Large" onclick="btnConfirm_Click" /></td>
                <td><asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="Large" onclick="btnCancel_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>