<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="DataMiningEntry.aspx.cs" Inherits="DataMining_DataMiningEntry" Title="歷史紀錄" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" runat="Server">
    <table id="tbDataMining" width="100%" runat="server" cellpadding="5">
        <tr>
            <td width="33%" />
            <td>
                <asp:TreeView ID="tvDataMining" runat="server" ImageSet="Contacts"
                    NodeIndent="30">
                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                    <Nodes>
                        <asp:TreeNode Text="資料探勘" SelectAction="None">
                            <asp:TreeNode Text="出席狀況紀錄" SelectAction="None">
                                <asp:TreeNode Text="個人" NavigateUrl="javascript:void window.open('PersonalAttendance.aspx', '', 'menubar=no, resizable=yes');"></asp:TreeNode>
                                <asp:TreeNode Text="全班" NavigateUrl="javascript:void window.open('GroupAttendance.aspx', '', 'menubar=no, resizable=yes');"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="課堂練習紀錄" SelectAction="None">
                                <asp:TreeNode Text="個人" NavigateUrl="javascript:void window.open('PersonalExercise.aspx', '', 'menubar=no, resizable=yes');"></asp:TreeNode>
                                <asp:TreeNode Text="全班" NavigateUrl="javascript:void window.open('GroupExercise.aspx', '', 'menubar=no, resizable=yes');"></asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                    <NodeStyle Font-Names="Verdana" Font-Size="XX-Large" ForeColor="Black"
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                    <LeafNodeStyle Font-Underline="True" Font-Bold="True" ForeColor="#5555DD" />
                </asp:TreeView>
            </td>
        </tr>
    </table>
    <table align="right">
        <tr>
            <td>
                <asp:Button ID="btnBackHomepage" runat="server" CssClass="ORCS_button_control" Text="返回首頁" PostBackUrl="../Homepage/Homepage.aspx" />
            </td>
        </tr>
    </table>
</asp:Content>
