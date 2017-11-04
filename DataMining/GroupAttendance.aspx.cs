using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ORCS.Util;
using ORCS.Base;
using ORCS.DB;
using ORCS.DB.Administrator;
using ORCS.DB.Attendance;
using ORCS.DB.User;
using System.Drawing;

public partial class DataMining_GroupAttendance : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //頁面初始化
        base.ORCS_Init();
        if (!IsPostBack)
        {
            //隱藏Master Page的上下框架(供HINTS的Datamining使用)
            ((HtmlTable)Master.FindControl("header_table")).Visible = false;
            (Master.FindControl("divFooter")).Visible = false;
            //載入系所
            LoadDepartment();
            //載入課程
            LoadClass();
            //載入上課時段
            LoadTime();
            //檢查下拉式選單是否有預設值
            if (Request.QueryString["ddlClass"] != null)
            {
                string strClass = Request.QueryString["ddlClass"].ToString();
                //檢查下拉式選單是否該項目，若有則選定其項目
                if (ddlClass.Items.FindByValue(strClass) != null)
                    ddlClass.SelectedValue = strClass;
            }
            if (Request.QueryString["ddlTime"] != null)
            {
                string strTime = Request.QueryString["ddlTime"].ToString();
                //檢查下拉式選單是否該項目，若有則選定其項目
                if (ddlTime.Items.FindByValue(strTime) != null)
                    ddlTime.SelectedValue = strTime;
            }
            //載入全班出席歷史訊息
            LoadClassAtt();
        }
    }
    //載入系所
    protected void LoadDepartment()
    {
        //先清空系所、課程和上課時段資料
        ddlDepartment.Items.Clear();
        ddlClass.Items.Clear();
        ddlTime.Items.Clear();
        //載入系所
        DataTable dtDepartment = clsEditGroupMember.ORCS_GroupMember_SELECT_by_UserID_Classify(ORCSSession.UserID, clsGroupNode.GroupClassification_SchoolGroup);
        if (dtDepartment.Rows.Count > 0)
        {
            foreach (DataRow drDepartment in dtDepartment.Rows)
            {
                //取得系所名稱
                string strDepartmentName = clsEditGroup.ORCS_SchoolGroup_SELECT_by_SchoolGroupID(drDepartment["iGroupID"].ToString()).Rows[0]["cSchoolGroupName"].ToString();
                //加入DropDownList
                ddlDepartment.Items.Add(new ListItem(strDepartmentName, drDepartment["iGroupID"].ToString()));
            }
        }
    }
    //載入課程
    protected void LoadClass()
    {
        if (ddlDepartment.Items.Count > 0)
        {
            //先清空班級和學生資料
            ddlClass.Items.Clear();
            ddlTime.Items.Clear();
            //根據系所抓取底下所有的班級
            DataTable dtAllClass = clsEditGroup.ORCS_ClassGroup_SELECT_by_ParentGroupID(ddlDepartment.SelectedItem.Value);
            //抓取使用者所屬的班級
            DataTable dtClass = new DataTable();
            if (dtAllClass.Rows.Count > 0)
            {
                foreach (DataRow drAllClass in dtAllClass.Rows)
                {
                    //取得班級
                    dtClass.Merge(clsEditGroupMember.ORCS_GroupMember_SELECT_by_UserID_GroupID_Classify(ORCSSession.UserID, drAllClass["iClassGroupID"].ToString(), clsGroupNode.GroupClassification_ClassGroup));
                }
            }
            if (dtClass.Rows.Count > 0)
            {
                foreach (DataRow drClass in dtClass.Rows)
                {
                    //取得班級名稱
                    string strClassName = clsEditGroup.ORCS_ClassGroup_SELECT_by_ClassGroupID(drClass["iGroupID"].ToString()).Rows[0]["cClassGroupName"].ToString();
                    //加入DropDownList
                    ddlClass.Items.Add(new ListItem(strClassName, drClass["iGroupID"].ToString()));
                }
            }
        }
    }
    //載入上課時段
    protected void LoadTime()
    {
        if (ddlClass.Items.Count > 0)
        {
            //先清空上課時段資料
            ddlTime.Items.Clear();
            //根據課程抓取底下所有的上課時段
            DataTable dtTime = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_DISTINCT_GroupID(ddlClass.SelectedItem.Value);
            if (dtTime.Rows.Count > 0)
            {
                foreach (DataRow drTime in dtTime.Rows)
                {
                    string strAttendanceID = drTime["cAttendanceID"].ToString();
                    //將上課時段加入DropDownList
                    ddlTime.Items.Add(new ListItem(clsTimeConvert.DBTimeToDateTime(strAttendanceID).ToShortDateString(), strAttendanceID));
                }
            }
        }
    }
    //載入全班出席歷史訊息
    protected void LoadClassAtt()
    {
        //先清空table物件
        tbClassAtt.Controls.Clear();
        if (ddlTime.Items.Count > 0)
        {
            #region 標題列
            //定義標題列
            HtmlTableRow tbrTitle = new HtmlTableRow();
            HtmlTableCell tbcActualAttTitle = new HtmlTableCell();
            tbcActualAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcActualAttTitle.Align = "center";
            HtmlTableCell tbcLateAttTitle = new HtmlTableCell();
            tbcLateAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcLateAttTitle.Align = "center";
            HtmlTableCell tbcAbsenceAttTitle = new HtmlTableCell();
            tbcAbsenceAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcAbsenceAttTitle.Align = "center";
            HtmlTableCell tbcTitleClassStatistics = new HtmlTableCell();
            tbcTitleClassStatistics.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleClassStatistics.Align = "center";
            //定義標題列物件
            Label lbActualAttTitle = new Label();
            lbActualAttTitle.Text = "實到";
            Label lbLateAttTitle = new Label();
            lbLateAttTitle.Text = "遲到";
            Label lbAbsenceAttTitle = new Label();
            lbAbsenceAttTitle.Text = "未到";
            Label lbTitleClassStatistics = new Label();
            lbTitleClassStatistics.Text = "全班統計";
            //將物件加入標題列
            tbcActualAttTitle.Controls.Add(lbActualAttTitle);
            tbcLateAttTitle.Controls.Add(lbLateAttTitle);
            tbcAbsenceAttTitle.Controls.Add(lbAbsenceAttTitle);
            tbcTitleClassStatistics.Controls.Add(lbTitleClassStatistics);
            tbrTitle.Cells.Add(tbcActualAttTitle);
            tbrTitle.Cells.Add(tbcLateAttTitle);
            tbrTitle.Cells.Add(tbcAbsenceAttTitle);
            tbrTitle.Cells.Add(tbcTitleClassStatistics);
            tbClassAtt.Rows.Add(tbrTitle);
            #endregion

            #region 內容
            //讀取出席歷史資料
            DataTable dtAttendanceHis = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_AttendanceID_GroupID(ddlTime.SelectedItem.Value, ddlClass.SelectedItem.Value);
            if (dtAttendanceHis.Rows.Count > 0)
            {
                //定義TableRow和TableCell
                HtmlTableRow tbrAttendanceHis = new HtmlTableRow();
                HtmlTableCell tbcActualAtt = new HtmlTableCell(); // 定義第一行(實到)
                tbcActualAtt.Attributes.Add("Style", "border: 2px solid black;");
                tbcActualAtt.Align = "center";
                tbcActualAtt.VAlign = "top";
                HtmlTableCell tbcLateAtt = new HtmlTableCell(); // 定義第二行(遲到)
                tbcLateAtt.Attributes.Add("Style", "border: 2px solid black;");
                tbcLateAtt.Align = "center";
                tbcLateAtt.VAlign = "top";
                HtmlTableCell tbcAbsenceAtt = new HtmlTableCell(); // 定義第三行(未到)
                tbcAbsenceAtt.Attributes.Add("Style", "border: 2px solid black;");
                tbcAbsenceAtt.Align = "center";
                tbcAbsenceAtt.VAlign = "top";
                HtmlTableCell tbcClassStatistics = new HtmlTableCell(); // 定義第四行(全班統計)
                tbcClassStatistics.Attributes.Add("Style", "border: 2px solid black;");
                tbcClassStatistics.Align = "center";
                tbcClassStatistics.Height = "380";
                tbcClassStatistics.Width = "55%";
                tbcClassStatistics.ID = "tbcClassStatistics";
                //定義圓餅圖統計
                int iAbsenceAtt = 0; //定義未到人數
                int iActualAtt = 0;  //定義實到人數
                int iLateAtt = 0;    //定義遲到人數
                //將人員分類(實到、遲到和未到)
                foreach (DataRow drAttendanceHis in dtAttendanceHis.Rows)
                {
                    //全班的話只統計個人
                    if (drAttendanceHis["iGroupMode"].ToString().Equals("0"))
                    {
                        //取得使用者資料
                        DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(drAttendanceHis["cUserID"].ToString());
                        //定義歷史出席資料物件
                        Label lbStudentName = new Label();
                        lbStudentName.Text = "<a href=PersonalAttendance.aspx?ddlClass=" + ddlClass.SelectedItem.Value + "&ddlStudent=" + drAttendanceHis["cUserID"].ToString() + ">" + dtUser.Rows[0]["cUserName"].ToString() + "</a><br />";
                        if (drAttendanceHis["iAttendanceCond"].ToString() == "0") // 未到
                        {
                            tbcAbsenceAtt.Controls.Add(lbStudentName);
                            iAbsenceAtt++;
                        }
                        else if (drAttendanceHis["iAttendanceCond"].ToString() == "1") // 實到
                        {
                            tbcActualAtt.Controls.Add(lbStudentName);
                            iActualAtt++;
                        }
                        else // 遲到
                        {
                            //計算遲到時間
                            if (drAttendanceHis["cAttendanceTime"].ToString() != "" && drAttendanceHis["cAttendanceTime"].ToString() != "NULL")
                            {
                                //將學生登入時間減掉遲到時間
                                TimeSpan tsTotalLateTime = clsTimeConvert.DBTimeToDateTime(drAttendanceHis["cAttendanceTime"].ToString()).Subtract(clsTimeConvert.DBTimeToDateTime(drAttendanceHis["cAttendanceID"].ToString()).AddMinutes(10));
                                //回傳遲到多少分鐘(若為0分鐘則顯示1分鐘)
                                string strTotalLateTime = tsTotalLateTime.TotalMinutes.ToString("0");
                                if (strTotalLateTime == "0")
                                    strTotalLateTime = "1";
                                lbStudentName.Text = "<a href=PersonalAttendance.aspx?ddlClass=" + ddlClass.SelectedItem.Value + "&ddlStudent=" + drAttendanceHis["cUserID"].ToString() + ">" + dtUser.Rows[0]["cUserName"].ToString() + "(" + strTotalLateTime + "分鐘)</a><br />";
                            }
                            tbcLateAtt.Controls.Add(lbStudentName);
                            iLateAtt++;
                        }
                    }
                }
                string strAbsenceAtt = "未到人數: " + iAbsenceAtt + "人";
                string strActualAtt = "實到人數: " + iActualAtt + "人";
                string strLateAtt = "遲到人數: " + iLateAtt + "人";
                //呼叫javacript畫圖餅圖函數
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>drawChart('" + strActualAtt + "','" + strLateAtt + "','" + strAbsenceAtt + "'," + iActualAtt + ", " + iLateAtt + "," + iAbsenceAtt + ",'" + tbcClassStatistics.ID + "')</script>");
                //將出席歷史資料物件加入Table
                tbrAttendanceHis.Cells.Add(tbcActualAtt);
                tbrAttendanceHis.Cells.Add(tbcLateAtt);
                tbrAttendanceHis.Cells.Add(tbcAbsenceAtt);
                tbrAttendanceHis.Cells.Add(tbcClassStatistics);
                tbClassAtt.Rows.Add(tbrAttendanceHis);
            }
            #endregion
        }
    }
    //選擇不同系所發生之事件
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入課程
        LoadClass();
        //載入上課時段
        LoadTime();
        //載入全班出席歷史訊息
        LoadClassAtt();
    }
    //選擇不同課程發生之事件
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入上課時段
        LoadTime();
        //載入全班出席歷史訊息
        LoadClassAtt();
    }
    //選擇不同上課時段發生之事件
    protected void ddlTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入全班出席歷史訊息
        LoadClassAtt();
    }

    //「出席狀況統計」
    protected void btnOpenAttStatistic_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "open_paper", "window.open('GroupAttendanceStatistics.aspx?cCourseID=" + ddlClass.SelectedValue + "','ViewPaper', 'width=800px,height=800px , scrollbars=yes, resizable=yes');", true);
        //載入全班出席歷史訊息
        LoadClassAtt();
    }
}