<%@ Page Language="C#" MasterPageFile="~/BasicForm/BasicForm.master" AutoEventWireup="true" CodeFile="EditGroupMember.aspx.cs" Inherits="Administrator_EditGroupMember" Title="管理群組" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdvancedProcessArea" runat="Server">
    <asp:ScriptManagerProxy ID="_proxy" runat="server">
    </asp:ScriptManagerProxy>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentArea" Runat="Server">
    <table align="center" width="85%">
        <!--上層控制選項-->
        <tr>
            <td align="left">
                <asp:Button ID="btnEditGroup" runat="server" CssClass="ORCS_Exercise_button" Width="180px" Text="編輯單位及課程" PostBackUrl="../Administrator/EditGroup.aspx" />
                &nbsp;&nbsp;<asp:Button ID="btnAutoGroup" runat="server"  CssClass="ORCS_Exercise_button" Width="180px" Text="自動分組" onclick="btnAutoGroup_Click"/><br /><br />
                <asp:Button ID="btnShowQueryMemberBelongCourse" runat="server" CssClass="ORCS_Exercise_button" Width="180px" Text="查詢成員所屬課程" onClick="btnShowQueryMemberBelongCourse_Click" />
                &nbsp;&nbsp;<asp:Button ID="btnQueryGroupInformation" runat="server" CssClass="ORCS_Exercise_button" Width="180px" Text="查詢分組資訊" Visible="false" onClick="btnQueryGroupInformation_Click" />
                &nbsp;&nbsp;<asp:Button ID="btnExceltoDB" runat="server" style="display:none;" CssClass="ORCS_Exercise_button" Text="匯入成員" onClick="btnExceltoDB_Click"/>
            </td>
            <td align="center">
                <asp:Label ID="lbEditMode" runat="server" Text="編輯模式" Font-Size="Large"></asp:Label>
                <asp:DropDownList ID="ddlEditMode" runat="server" Font-Size="Medium" AutoPostBack="True" onselectedindexchanged="ddlEditMode_SelectedIndexChanged">
                    <asp:ListItem Value="EditMember" Selected="True">編輯人員</asp:ListItem>
                    <asp:ListItem Value="EditIdentity">設定主席</asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;
                 <asp:Label ID="lbAuthority" runat="server" Text="身分" Font-Size="Large"></asp:Label>
                <asp:DropDownList ID="ddlAuthority" runat="server" Font-Size="Medium" 
                    AutoPostBack="True" onselectedindexchanged="ddlAuthority_SelectedIndexChanged">
                    <asp:ListItem Value="t">教師</asp:ListItem>
                    <asp:ListItem Value="s" Selected="True">學生</asp:ListItem>
                    <asp:ListItem Value="a">助教</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <!--主要群組架構-->
        <tr>
            <!--群組TreeView-->
            <td valign="top">
                <asp:TreeView ID="tvGroup" runat="server" ImageSet="Arrows"
                    onselectednodechanged="tvGroup_SelectedNodeChanged">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="Red" 
                        HorizontalPadding="0px" VerticalPadding="0px" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="24pt" ForeColor="Black" 
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                </asp:TreeView>
            </td>
            <!--群組人員-->
            <td align="center" valign="top">
                <div id="divGroupMember" runat="server" style="border-style: inset; vertical-align: top; background-color: #FFFFFF; overflow: auto; width: 480px; height: 680px;">
                    <asp:GridView ID="gvGroupMember" runat="server" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="100%">
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="全選" OnCheckedChanged="chkSelectAll_OnCheckedChanged" AutoPostBack="True" />
                                    全選
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkUserSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
                <div id="divEditIdentify" runat="server" visible = "false">
                    <table style="border-color: #000000; border-style: double;" align="right" width="480px">
                        <tr>
                            <td>
                                <asp:ListBox ID="MemberList" runat="server" Height="400px" Width="300px" Font-Size="Larger"></asp:ListBox>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                             <table style="border-color: #000000; border-style: double;">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbChairman" runat="server" Text="選擇主席" Font-Size="Larger"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddChairMan" runat="server" Text=">>" Width="100px"  onclick="btnAddChairMan_Click" /> 
                                                    </td>
                                                    <td>
                                                        <asp:ListBox ID="ChairmanList" runat="server" Height="55px" Width="200px" Font-Size="Larger"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <table style="border-color: #000000; border-style: double;">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbRecordMan" runat="server" Text="選擇紀錄者" Font-Size="Larger"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddRecordMan" runat="server" Text=">>" Width="100px" onclick="btnAddRecordMan_Click"/> 
                                                    </td>
                                                    <td>
                                                        <asp:ListBox ID="RecordmanList" runat="server" Height="55px" Width="200px" Font-Size="Larger"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td>
                                            <table> 
                                                <tr align="right">
                                                    <td>
                                                         <asp:Button ID="btnSaveChairMan" runat="server" Text="儲存" Width="100px" Font-Size="Larger"/> 
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table style="width:100%;text-align:right;">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" CssClass="ORCS_button_control" Text="儲存" OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" CssClass="ORCS_button_control" Text="返回首頁" PostBackUrl="../Homepage/Homepage.aspx" />
                <asp:Button ID="btnClose" runat="server" CssClass="ORCS_button_control" Text="完成" Visible="false" OnClientClick="window.close();return false;" />
            </td>
        </tr>
    </table>
    <!--自動分組-->
    <div id="divGroupNum" runat="server" style="border: medium solid #000000; background-color: #CCCCCC; position: absolute;
        height: 150px; width: 600px; top: 85px; left: 360px;" visible="false">
        <table style="height:150px">
            <tr>
                <td>
                    <asp:Label ID="lbGroupNum" runat="server" Text="此課程人數" Font-Size="X-Large"></asp:Label>
                    <asp:DropDownList ID="ddlGroupNum" runat="server" Width="350px" Font-Size="X-Large"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="padding-left:25em">
                    <table style="width:200px">
                        <tr>
                            <td>
                                <asp:Button ID="btnConfirm" runat="server" Text="確定" Font-Size="Large" OnClick="btnConfirm_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="取消" Font-Size="Large" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- 查詢成員所屬課程-->
    <div id="divQueryMemberBelongCourse" runat="server" style="border: medium solid #000000; background-color: #CCCCCC; position: absolute;
         width: 680px; top: 85px; left: 300px;" visible="false">
        <table cellpadding="10">
            <tr>
                <td>
                    <asp:Label ID="lbQueryMemberBelongCourse" runat="server" Text="查詢成員所屬課程" Font-Size="X-Large"></asp:Label>
                <td/>    
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbQueryID" runat="server" Text="請輸入帳號：" Font-Size="X-Large"></asp:Label>
                    <asp:textbox ID="txtQueryID" Width="260px" runat="server"  Font-Size="X-Large"></asp:textbox>
                    &nbsp;&nbsp;<asp:Button ID="btnQueryMemberBelongCourse" runat="server" Text="查詢" Font-Size="Large" onClick="btnQueryMemberBelongCourse_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btnCancelQueryMemberBelongCourse" runat="server" Text="取消" Font-Size="Large" onClick="btnCloseQueryMemberBelongCourse_Click"/>
                <td/>    
            </tr>
        </table>
        <div id="divTableMemberBelongCourse" style="display:none;" runat="server">
        <table align="center" cellpadding="20">
            <tr align="center">
                <td>
                    <asp:GridView ID="gvQueryMemberBelongCourse" runat="server" BackColor="#CCCCCC" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                        CellSpacing="2" ForeColor="Black" Visible = "false" Width = "650px">
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
         </table>
         <table align="Right" cellpadding="20">
            <tr align ="Right">
                <td>
                    <asp:Button ID="btnCloseQueryMemberBelongCourse" runat="server" Text="關閉" Font-Size="Large" OnClick="btnCloseQueryMemberBelongCourse_Click" visible="false"/>
                </td>
            </tr>
        </table>
        </div>
    </div>
    <!--匯入班級成員-->
    <div id="divExceltoDB" runat="server" style="border: medium solid #000000; background-color: #CCCCCC; position: absolute;
        height: 150px; width: 600px; top: 85px; left: 360px;" visible="false">
        <table cellpadding="10">
            <tr>
                <td>
                    <asp:Label ID="lbExcelUpload" runat="server" Text="請選擇Excel檔案上傳" Font-Size="X-Large"></asp:Label>
                <td/>    
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="FileUploadExcel" runat="server"  Width="500px" Font-Size= "Larger" />
                <td/>    
            </tr>
        </table>
        <table align="Right" cellpadding="20">
            <tr>
                <td>
                    <asp:Button ID="btnExcelSave" runat="server" Text="上傳" Font-Size="Large" onclick="btnExcelSave_Click"/>
                    <asp:Button ID="btnExcelCancel" runat="server" Text="取消" Font-Size="Large"  onclick="btnExcelCancel_Click"/>
                 </td>
            </tr>
        </table>
        <br /><br /><br />
        <table>
             <tr align="left">
                <td>
                    <asp:Label ID="lbRemind" runat="server" Text="*確認無誤後，請按送出按鈕" Font-Size="Large" Visible ="false"></asp:Label>
                </td>
             </tr>
        </table>
        <table align="center" cellpadding="20">
            <tr align="center">
                <td>
                    <asp:GridView ID="gvExcelMemberData" runat="server" BackColor="#CCCCCC" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                        CellSpacing="2" ForeColor="Black" Visible = "false" Width = "500px">
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
         </table>
         <table align="Right" cellpadding="20">
            <tr align ="Right">
                <td>
                    <asp:Button ID="btnAddMemberByExcel" runat="server" Text="送出" Font-Size="Large" onclick="btnAddMemberByExcel_Click" Visible = "false"/>
                </td>
            </tr>
        </table>
    </div>
     <!--尚未分組-->
    <div id="divNonGroup" runat="server" visible=false style="border: medium solid #000000; background-color: #CCCCCC;
        height: 150px; width: 500px;">
            <asp:Label ID="Label1" runat="server" Text="尚未分組名單" Font-Size="Large"></asp:Label>
          <asp:GridView ID="gvNonGroup" runat="server" BackColor="White" 
              BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="450px" AutoGenerateColumns = False>
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
          <Columns>  
                <asp:BoundField DataField="SchoolName" HeaderText="系所" ReadOnly="True" />
	            <asp:BoundField DataField="ClasslName" HeaderText="課程" ReadOnly="True" />
                <asp:BoundField DataField="UserID" HeaderText="帳號" ReadOnly="True" />
                <asp:BoundField DataField="UserName" HeaderText="姓名" ReadOnly="True" />
          </Columns>
          </asp:GridView>
          <table width = "100%">
            <tr align ="right">
                <td>
                     <asp:Button ID="btnAutoGroupForManual" runat="server" Text="自動分組" onclick="btnAutoGroupForManual_Click" visible="false" Width="100px" Font-Size="Larger"/>
                </td>
            </tr>
          </table>
    </div>
    <asp:HiddenField ID="hfNodeValue" runat="server" Value="" />
</asp:Content>
