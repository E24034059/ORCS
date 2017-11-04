<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="EditGroup.aspx.cs" Inherits="Administrator_EditGroup" Title="編輯單位及課程" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                <table style="width:95%;" align="center"  cellpadding="10">
                    <tr>
                        <!--群組TreeView-->
                        <td style="vertical-align:top;width:30%;">
                            <p style="font-size:36px;height:30px">請選擇編輯單位：</p>
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
                        <!--群組修改-->
                        <td style="vertical-align:top;">
                            <table style="border-color: #000000; border-style: double; width:100%;border-collapse:collapse;" align="center">
                                <tr>
                                    <td colspan="3" style="height:60px;border-bottom:#000000 5px double;text-align:center;background-color:#75c1fb">
                                        <asp:Label ID="lbUnitNodeData" runat="server" Text="單位資料" Font-Size="24pt"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height:55px;width:250px;border-bottom:#000000 2px solid;">
                                        <asp:Label ID="lbUnitName" runat="server" Text="單位名稱" Font-Size="20pt"></asp:Label>
                                    </td>
                                    <td style="min-width:380px;border-bottom:#000000 2px solid;">
                                        <asp:TextBox ID="tbUnitName" runat="server" Font-Size="20pt" Height="40px" Width="400px"></asp:TextBox>
                                        <asp:Button ID="btnEditName" runat="server" Text="修改名稱" Height="40px" Width="140px" Font-Size="20pt" OnClick="btnEditName_Click" />
                                    </td>
                                </tr>
                                <tr id="trChildUnit" runat="server">
                                    <td style="height:55px;width:250px;border-bottom:#000000 2px solid;">
                                        <asp:Label ID="lbChildUnitName" runat="server" Text="新增隸屬單位名稱" Font-Size="20pt"></asp:Label>
                                    </td>
                                    <td style="min-width:380px;border-bottom:#000000 2px solid;">
                                        <asp:TextBox ID="tbChildUnitName" runat="server" Font-Size="20pt" Height="40px" Width="400px"></asp:TextBox>
                                        <asp:Button ID="btnAddChildUnit" runat="server" Text="新增單位" Height="40px" Width="140px" Font-Size="20pt" OnClick="btnAddChildUnit_Click" />
                                    </td>
                                </tr>
                                <tr id="trChildUnitIsDepartment" runat="server">
                                    <td style="height:55px;width:250px;border-bottom:#000000 2px solid;">
                                        <asp:Label ID="lbIsNodeDepartment" runat="server" Text="新增單位是否為系級(若選是新增單位為系級，系級子單位則為課程)" Font-Size="20pt"></asp:Label>
                                    </td>
                                    <td colspan="2" style="border-bottom:#000000 2px solid;">
                                        <asp:RadioButtonList ID="rblIsNodeDepartment" runat="server" Font-Size="20pt" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="是" Value="Y" />
                                            <asp:ListItem Text="否" Value="N" Selected="True" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr id="trDeleteUnit" runat="server">
                                    <td colspan="3" style="height:55px;text-align:center;">
                                        <asp:Button ID="btnDeleteNode" runat="server" Text="刪除單位" Font-Size="20pt" Height="40px" OnClick="btnDeleteNode_Click"/>
                                    </td>
                                </tr>
                            </table>
                            <br /><br />
                            <table style="width:100%;text-align: right;">
                                <tr>
                                    <td>
                                        <input type="button" id="btnClose" style="font-size:20pt;height:40px;display:none;" value="完成" onclick="window.close();" runat="server" />
                                        <asp:Button ID="btnBack" runat="server" Text="返回群組管理" Font-Size="20pt" PostBackUrl="../Administrator/EditGroupMember.aspx" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfNodeValue" runat="server" />
</asp:Content>