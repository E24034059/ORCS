<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicFormForLoginPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login" Title="登入" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" Runat="Server">
    <asp:ScriptManagerProxy id="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" Runat="Server">
    <div align="center" style="text-align: center; padding-top: 50px;">
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 40%; text-align: center;" border="1" cellpadding="5" cellspacing="5">
                    <tr>
                        <td style="width: 40%;">帳號</td>
                        <td><asp:TextBox ID="tbUserID" runat="server" Width="99%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>密碼</td>
                        <td><asp:TextBox ID="tbPasswd" runat="server" TextMode="Password" Width="99%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Button ID="btLogin" runat="server" CssClass="ORCS_button_control" Text="登入" onclick="btLogin_Click" /></td>
                    </tr>
                </table>
                <div>
                    <asp:Label ID="lErrorMsg" runat="server" Text="" CssClass="ORCS_span_reminder"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="tbUserID" ErrorMessage="請輸入帳號!" Display="None"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvPasswd" runat="server" ControlToValidate="tbPasswd" ErrorMessage="請輸入密碼!" Display="None"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" runat="server" />
                </div> 
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align: center; width: 100%; font-size: 100%; padding-top: 70px;">
            本系統的建議解析度為1024*768或更高<br /><br />
            請您檢查您的瀏覽器設定，若您有使用阻擋彈出式視窗，可能會影響到本系統的呈現
        </div>
    </div>
</asp:Content>