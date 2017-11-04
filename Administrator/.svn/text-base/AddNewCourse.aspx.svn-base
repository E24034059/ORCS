<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="AddNewCourse.aspx.cs" Inherits="Administrator_AddNewCourse" Title="新增課程" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
    <script type="text/javascript">
        //開啟選擇單位頁面
        function OpenSelectGroup() {
            var serverIP = document.getElementById("ctl00_cphContentArea_hfServerIP").value;
            window.open('http://' + serverIP + '/ORCS/Administrator/SelectGroup.aspx?modle=EditMLASCourse', '_blank', 'height=600,width=450,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
            //window.open('../User/SelectGroup.aspx?modle=EditMLASCourse', '_blank', 'height=500,width=450,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
        }
    </script>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="MLAS_div_click">
            <div class="MLAS_div_setup_title">
                <asp:Label ID="lbEditCourse" runat="server" Text="" />
            </div>
            <div class="MLAS_div_setup_item">
                <div class="MLAS_div_click">
                    <table>
                        <tr>
                            <td style="width:200px">
                                <div class="MLAS_div_setup_item_title">
                                    <asp:Label ID="lblMembershipGroups" runat="server" Text="所屬單位" />
                                </div>
                            </td>
                            <td style="width:100%">
                                <div class="MLAS_div_setup_item_content">
                                    <input type="text" id="tbSelectGroupPath" value="" style="width: 95%; color: #000000;" runat="server" readonly="readonly" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="MLAS_div_click">
                    <table>
                        <tr>
                            <td style="width:200px">
                                <div class="MLAS_div_setup_item_title">
                                    <asp:Label ID="lbCourseName" runat="server" Text="" />
                                </div>
                            </td>
                            <td style="width:100%">
                                <div class="MLAS_div_setup_item_content">
                                    <asp:TextBox ID="tbCourseName" runat="server" Width="95%" Text="" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="MLAS_div_click">
                    <table>
                        <tr>
                            <td style="width:200px">
                                <div class="MLAS_div_setup_item_title">
                                    <asp:Label ID="lbCourseDescription" runat="server" Text="" />
                                </div>
                            </td>
                            <td style="width:100%">
                                <div class="MLAS_div_setup_item_content" >
                                    <asp:TextBox ID="tbCourseDescription" runat="server" Width="95%" Rows="3" TextMode="multiline" Text="" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="MLAS_div_click">
                    <table>
                        <tr>
                            <td style="width:200px">
                                <div class="MLAS_div_setup_item_title">
                                    <asp:Label ID="lblCourseType" runat="server" Text="系列教學活動模組" />
                                </div>
                            </td>
                            <td style="width:100%">
                                <div class="MLAS_div_setup_item_content">
                                    <asp:DropDownList ID="ddlCourseType" runat="server" Width="200px" OnInit="ddlCourseType_Init"></asp:DropDownList><br>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="MLAS_div_click">
                    <table>
                        <tr>
                            <td style="width:200px">
                                <div class="MLAS_div_setup_item_title">
                                    <asp:Label ID="lbCourseView" runat="server" Text="" />
                                </div>
                            </td>
                            <td style="width:100%">
                                <div class="MLAS_div_setup_item_content">
                                    <asp:RadioButton ID="rbViewCourse" runat="server" GroupName="CourseView" Text="" Checked="true" /><br>
                                    <asp:RadioButton ID="rbHindCourse" runat="server" GroupName="CourseView" Text="" /><br>
                                    <asp:RadioButton ID="rbPublicCourse" runat="server" GroupName="CourseView" Text="" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <br>
    <asp:Panel ID="pFunctionButtons" runat="server" Width="98%" style="text-align:right;">
        <asp:Button ID="btBack" runat="server" Text="" CssClass="MLAS_button_control" OnClick="btBack_Click" />
        <asp:Button ID="btFinish" runat="server" Text="" CssClass="MLAS_button_control" OnClick="btFinish_Click" />
    </asp:Panel>
    <input type="hidden" id="hfServerIP" value="" runat="server" />
    <input type="hidden" id="hfBelongUnitID" value="" runat="server" />
    <input type="hidden" id="hfDepartmentID" value="" runat="server" />
</asp:Content>