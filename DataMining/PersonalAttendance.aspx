<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="PersonalAttendance.aspx.cs" Inherits="DataMining_PersonalAttendance" Title="個人出席紀錄" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" runat="Server">
    <!--超連結樣式-->
    <style type="text/css">
        A:link {
            COLOR: #3300ff;
            text-decoration: underline;
        }

        A:hover {
            COLOR: #0000FF;
            text-decoration: underline;
        }

        A:active {
            COLOR: #ff0033;
            text-decoration: underline;
        }
    </style>
    <!--javacript-->
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        //畫圓餅圖函數
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart(strActualAtt, strLateAtt, strAbsenceAtt, iActualAtt, iLateAtt, iAbsenceAtt, strTableCellID) {
            if (document.getElementById('ctl00_cphContentArea_' + strTableCellID) != null) {
                //定義內容
                var data = google.visualization.arrayToDataTable([
                    ['State', 'People'],
                    [strActualAtt, iActualAtt],
                    [strLateAtt, iLateAtt],
                    [strAbsenceAtt, iAbsenceAtt]
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
    </script>
    <!--內容框架-->
        <table align="center" cellpadding="5">
            <tr>
                <td>
                    <asp:Label ID="lbDepartment" runat="server" Text="系所:" Font-Size="XX-Large"></asp:Label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" Font-Size="X-Large"
                        Width="230" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lbClass" runat="server" Text="課程:" Font-Size="XX-Large"></asp:Label>
                    <asp:DropDownList ID="ddlClass" runat="server" Font-Size="X-Large" Width="230"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lbStudent" runat="server" Text="學生:" Font-Size="XX-Large"></asp:Label>
                    <asp:DropDownList ID="ddlStudent" runat="server" Font-Size="X-Large"
                        Width="230" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table id="tbTotalStatistics" runat="server" align="center"
            style="border-collapse: collapse; font-size: x-large;" width="80%" cellpadding="5" />
        <br />
        <table id="tbInfo" runat="server" align="center"
            style="border-collapse: collapse; font-size: x-large;" width="90%" cellpadding="5" />
        <br />
        <table align="right">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="ORCS_button_control" Text="關閉" OnClientClick="window.close()" />
                </td>
            </tr>
        </table>
</asp:Content>
