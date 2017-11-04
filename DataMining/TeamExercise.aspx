<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="TeamExercise.aspx.cs" Inherits="DataMining_TeamExercise" Title="小組課堂練習紀錄" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" Runat="Server">
    <!--超連結樣式-->
    <style type="text/css">
        A:link {COLOR: #3300ff; text-decoration: underline}   
        A:hover {COLOR: #0000FF; text-decoration: underline} 
        A:active {COLOR: #ff0033; text-decoration: underline}  
    </style>
    <!--javacript-->
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        //畫圓餅圖函數
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart(strFinAns, strNoAns, iFinAns, iNoAns, strTableCellID) {
            if (document.getElementById('ctl00_cphContentArea_' + strTableCellID) != null) {
                //定義內容
                var data = google.visualization.arrayToDataTable([
                    ['State', 'People'],
                    [strFinAns, iFinAns],
                    [strNoAns, iNoAns]
                ]);
                //定義樣式
                var options = {
                    is3D: true,
                    fontSize: 20,
                    chartArea: { height: "100%", width: "100%" }
                };
                //抓取要放入位置的ID並顯示出來
                var chart = new google.visualization.PieChart(document.getElementById('ctl00_cphContentArea_' + strTableCellID));
                chart.draw(data, options);
            }
        }
        //隱藏TableRow函數
        function ShowChart(strSelectionTRID, strImgID) {
            if (document.getElementById("ctl00_cphContentArea_" + strSelectionTRID) != null) {
                if (document.getElementById("ctl00_cphContentArea_" + strSelectionTRID).style.display == "none") {
                    document.getElementById("ctl00_cphContentArea_" + strSelectionTRID).style.display = "";
                    document.getElementById(strImgID).src = "../App_Themes/general/images/collapse.gif";
                }
                else {
                    document.getElementById("ctl00_cphContentArea_" + strSelectionTRID).style.display = "none";
                    document.getElementById(strImgID).src = "../App_Themes/general/images/expand.gif";
                }
            }
        }
        //網頁瀏覽按鈕觸發開啟事件
        function btnWebBrowsingClick(obj) {
            var pathParams = obj.id.split("-");
            ////btnWebBrowsing-subFileName-ExerciseCondID-ExerciseID-UserID-FileName
            var strSubFileName = pathParams[1];
            var strExerciseCondID = pathParams[2];
            var strExerciseID = pathParams[3];
            var strUserID = pathParams[4];
            var strFileName = pathParams[5];
            var strFilePath = "ExFileUpload/" + strExerciseCondID + "/" + strExerciseID + "/" + strUserID + "/" + strFileName;

            //若檔案符合以下檔案則可直接從網頁瀏覽:PDF,JPG,JPEG,GIF,PNG,BMP
            if (strSubFileName == "PDF" || strSubFileName == "JPG" || strSubFileName == "JPEG"
                || strSubFileName == "GIF" || strSubFileName == "PNG" || strSubFileName == "BMP" || strSubFileName == "TXT")
                window.open("http://140.116.72.28/MLAS/Exercise/" + strFilePath, 'OpenHtml', 'resizable=yes, width=700px, height=620px');
            else if (strSubFileName == "DOC" || strSubFileName == "DOCX" || strSubFileName == "PPT" || strSubFileName == "PTTX"
                     || strSubFileName == "XLS" || strSubFileName == "XLSX")
                window.open("https://docs.google.com/viewer?url=http://140.116.72.28/MLAS/Exercise/" + encodeURIComponent(strFilePath), 'OpenHtml', 'resizable=yes, width=700px, height=620px');
            else
                alert("此檔案無法在網頁瀏覽");
        }


        //網頁瀏覽(文字)按鈕觸發開啟事件
        function btnWebTextBrowsingClick(obj) {

            var pathParams = obj.id.split("-");
            //strExerciseCondID
            var strExerciseCondID = pathParams[1];
            //strExerciseID
            var strExerciseID = pathParams[2];
            //取得作答者ID
            var strStudentID = pathParams[3];
            //取得課程ID
            var strCourseID = pathParams[4];
            //取得作答模式
            var strAnswerMode = pathParams[5];
            //取得使用者權限
            var strAuthority = pathParams[6];

            window.open('http://140.116.72.28/MLAS/Activity_ClassExercise/ShowTextUpload.aspx?cExerciseCondID=' + strExerciseCondID + '&cExerciseID=' + strExerciseID + '&cGroupID=' + strCourseID + '&cUserID=' + strStudentID + '&cAnswerMode=' + strAnswerMode + '&cAuthority=' + strAuthority + '', 'FileUpload', 'width=700px, height=800px');
        }

        //下載檔案按鈕觸發下載事件
        function btnDownloadClick(obj) {
            var urlPath = "http://140.116.72.28/MLAS/Activity_ClassExercise/FileDownload.aspx?exerciseParams=" + encodeURIComponent(obj.id);
            window.open(urlPath, 'OpenHtml', '');
        }
    </script>
    <!--內容框架-->
    <table align="center" cellpadding="5">
        <tr>
            <td>
                <asp:Label ID="lbDepartment" runat="server" Text="系所:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlDepartment" runat="server" Font-Size="X-Large" 
                    Width="230" AutoPostBack="True" 
                    onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lbClass" runat="server" Text="課程:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlClass" runat="server" Font-Size="X-Large" Width="230" 
                    AutoPostBack="True" onselectedindexchanged="ddlClass_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lbStudent" runat="server" Text="小組:" Font-Size="XX-Large"></asp:Label>
                <asp:DropDownList ID="ddlStudent" runat="server" Font-Size="X-Large" 
                    Width="230" AutoPostBack="True" 
                    onselectedindexchanged="ddlStudent_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table id="tbTotalStatistics" runat="server" align="center" 
        style="border-collapse: collapse; font-size: x-large;" width="80%" cellpadding="5" />
    <br />
    <table id="tbInfo" runat="server" align="center" 
        style="border-collapse: collapse; font-size: x-large;" width="90%" />
    <br />
    <table align="right">
        <asp:Button ID="btnCloseWindow" runat="server" CssClass="ORCS_button_control" Text="關閉" OnClientClick="window.close()" />
    </table>
</asp:Content>