<%@ Page Language="C#" CodeFile="TextUpload.aspx.cs" Inherits="Exercise_TextUpload" ValidateRequest="false" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../ckeditor/ckeditor.js"></script>
    <script>
        function checkFinish(){
            if (confirm('請問是否完成作答?確認完成後，只能觀看不能再修改答案。')) {
                document.getElementById('hfIsComplete').value = '1';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbUploadTextContent" runat="server" Font-Size="XX-Large" Text="上傳文字內容" />
        <br />
        <asp:TextBox ID="tbContent" runat="server" Height="100%" TextMode="MultiLine" class="ckeditor"></asp:TextBox>
        <br />
        <div style="text-align:center;">
            <asp:Button ID="btFininsh" runat="server" Font-Size="XX-Large" Text="上傳" OnClick="btFininsh_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btCancel" runat="server" Font-Size="XX-Large" Text="取消" OnClick="btCancel_Click" />
        </div>
    </div>
        <asp:HiddenField ID="hfIsComplete" Value="0" runat="server" />
    </form>
</body>
</html>
