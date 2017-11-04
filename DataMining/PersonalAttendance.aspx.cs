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

public partial class DataMining_PersonalAttendance : ORCS.ORCS_Page
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
            //載入學生
            LoadStudent();
            //檢查下拉式選單是否有預設值
            if (Request.QueryString["ddlClass"] != null)
            {
                string strClass = Request.QueryString["ddlClass"].ToString();
                //檢查下拉式選單是否該項目，若有則選定其項目
                if (ddlClass.Items.FindByValue(strClass) != null)
                    ddlClass.SelectedValue = strClass;
            }
            if (Request.QueryString["ddlStudent"] != null)
            {
                string strStudent = Request.QueryString["ddlStudent"].ToString();
                //檢查下拉式選單是否該項目，若有則選定其項目
                if (ddlStudent.Items.FindByValue(strStudent) != null)
                    ddlStudent.SelectedValue = strStudent;
            }
            //載入總統計資料
            LoadTotalStatistics();
            //載入歷史訊息
            LoadHistory();
        }
    }
    //載入系所
    protected void LoadDepartment()
    {
        //先清空系所、課程和學生資料
        ddlDepartment.Items.Clear();
        ddlClass.Items.Clear();
        ddlStudent.Items.Clear();
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
            ddlStudent.Items.Clear();
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
    //載入學生
    protected void LoadStudent()
    {
        if (ddlClass.Items.Count > 0)
        {
            //先清空學生資料
            ddlStudent.Items.Clear();
            //根據課程抓取底下所有的學生
            DataTable dtStudent = clsEditGroupMember.ORCS_GroupMember_SELECT_by_GroupID_Classify(ddlClass.SelectedItem.Value, clsGroupNode.GroupClassification_ClassGroup,"s");
            if (dtStudent.Rows.Count > 0)
            {
                foreach (DataRow drStudent in dtStudent.Rows)
                {
                    //取得使用者資料
                    DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(drStudent["cUserID"].ToString());
                    //若為學生才加入DropDownList
                    if (dtUser.Rows[0]["cAuthority"].ToString() == "s")
                    {
                        //取得學生姓名
                        string strStudentName = dtUser.Rows[0]["cUserName"].ToString();
                        //加入DropDownList
                        ddlStudent.Items.Add(new ListItem(strStudentName, drStudent["cUserID"].ToString()));
                    }
                }
            }
        }
    }
    //載入總統計資料
    protected void LoadTotalStatistics()
    {
        //先清空table物件
        tbTotalStatistics.Controls.Clear();
        if (ddlStudent.Items.Count > 0)
        {
            #region 標題列
            //定義標題列
            HtmlTableRow tbrTotalStaTitle = new HtmlTableRow();
            HtmlTableCell tbcTotalStaTitle = new HtmlTableCell();
            tbcTotalStaTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold; CURSOR: hand;");
            tbcTotalStaTitle.Align = "center";
            //定義標題列物件
            Label lbTotalStaTitle = new Label(); // 定義點擊縮放的圖示
            lbTotalStaTitle.Text = "<IMG id='img_TotalStaTitle' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "總出席率";
            //將物件加入標題列
            tbcTotalStaTitle.Controls.Add(lbTotalStaTitle);
            tbrTotalStaTitle.Cells.Add(tbcTotalStaTitle);
            tbTotalStatistics.Rows.Add(tbrTotalStaTitle);
            #endregion

            #region 內容
            //讀取出席歷史資料
            DataTable dtAttendanceHis = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_UserID_GroupID(ddlStudent.SelectedItem.Value, ddlClass.SelectedItem.Value);
            if (dtAttendanceHis.Rows.Count > 0)
            {
                double dAbsenceAtt = 0; //定義未到天數
                double dActualAtt = 0;  //定義實到天數
                double dLateAtt = 0;    //定義遲到天數
                foreach (DataRow drAttendanceHis in dtAttendanceHis.Rows)
                {
                    if (drAttendanceHis["iAttendanceCond"].ToString() == "0")
                        dAbsenceAtt++;
                    else if (drAttendanceHis["iAttendanceCond"].ToString() == "1")
                        dActualAtt++;
                    else
                        dLateAtt++;
                }
                string strAbsenceAtt = "未到天數: " + dAbsenceAtt + "天";
                string strActualAtt = "實到天數: " + dActualAtt + "天";
                string strLateAtt = "遲到天數: " + dLateAtt + "天";
                //計算出席率
                double dAttRate = (int)(((dActualAtt + dLateAtt) / dtAttendanceHis.Rows.Count) * 100);
                lbTotalStaTitle.Text = "<IMG id='img_TotalStaTitle' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "總出席率:" + dAttRate + "%";
                //定義出席率列
                HtmlTableRow tbrAttRate = new HtmlTableRow();
                tbrAttRate.ID = "tbrAttRate_TotalStaTitle";
                HtmlTableCell tbcAttRate = new HtmlTableCell();
                tbcAttRate.Attributes.Add("Style", "border: 2px solid black;");
                tbcAttRate.Align = "center";
                tbcAttRate.ID = "tbcAttRate_TotalStaTitle";
                tbcAttRate.Height = "300";
                tbcTotalStaTitle.Attributes.Add("onclick", "ShowChart('" + tbrAttRate.ID + "' , 'img_TotalStaTitle')");
                //將物件加入Table
                tbrAttRate.Cells.Add(tbcAttRate);
                tbTotalStatistics.Rows.Add(tbrAttRate);
                //呼叫javacript畫圖餅圖函數
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>drawChart('" + strActualAtt + "','" + strLateAtt + "','" + strAbsenceAtt + "'," + dActualAtt + ", " + dLateAtt + "," + dAbsenceAtt + ",'" + tbcAttRate.ID + "')</script>");
            }
            #endregion
        }
    }
    //載入歷史訊息
    protected void LoadHistory()
    {
        //先清空table物件
        tbInfo.Controls.Clear();
        if (ddlStudent.Items.Count > 0)
        {
            #region 標題列
            //定義標題列
            HtmlTableRow tbrTitle = new HtmlTableRow();
            HtmlTableCell tbcTitleTime = new HtmlTableCell();
            tbcTitleTime.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleTime.Align = "center";
            HtmlTableCell tbcTitleState = new HtmlTableCell();
            tbcTitleState.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleState.Align = "center";
            HtmlTableCell tbcTitleClassStatistics = new HtmlTableCell();
            tbcTitleClassStatistics.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleClassStatistics.Align = "center";
            //定義標題列物件
            Label lbTitleTime = new Label();
            lbTitleTime.Text = "上課日期";
            Label lbTitleState = new Label();
            lbTitleState.Text = "狀態";
            Label lbTitleClassStatistics = new Label();
            lbTitleClassStatistics.Text = "全班統計";
            //將物件加入標題列
            tbcTitleTime.Controls.Add(lbTitleTime);
            tbcTitleState.Controls.Add(lbTitleState);
            tbcTitleClassStatistics.Controls.Add(lbTitleClassStatistics);
            tbrTitle.Cells.Add(tbcTitleTime);
            tbrTitle.Cells.Add(tbcTitleState);
            tbrTitle.Cells.Add(tbcTitleClassStatistics);
            tbInfo.Rows.Add(tbrTitle);
            #endregion

            #region 內容
            //讀取出席歷史資料
            DataTable dtAttendanceHis = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_UserID_GroupID(ddlStudent.SelectedItem.Value, ddlClass.SelectedItem.Value);
            int iCount = 0;//定義資料筆數
            if (dtAttendanceHis.Rows.Count > 0)
            {
                foreach (DataRow drAttendanceHis in dtAttendanceHis.Rows)
                {
                    //先累加資料筆數
                    iCount++;
                    //定義TableRow和TableCell
                    HtmlTableRow tbrAttendanceHisTitle = new HtmlTableRow(); // 定義子標題列(點擊縮放的地方)
                    HtmlTableRow tbrAttendanceHis = new HtmlTableRow(); // 定義歷史資料列
                    tbrAttendanceHis.ID = "tbrAttendanceHis_" + drAttendanceHis["cAttendanceID"].ToString();
                    HtmlTableCell tbcAttendanceHisTitle = new HtmlTableCell(); // 定義子標題行
                    tbcAttendanceHisTitle.Attributes.Add("Style", "background-color: #C6dfff; CURSOR: hand; border: 2px solid black;");
                    tbcAttendanceHisTitle.Attributes.Add("onclick", "ShowChart('" + tbrAttendanceHis.ID + "' , 'img_" + drAttendanceHis["cAttendanceID"].ToString() + "')");
                    tbcAttendanceHisTitle.ColSpan = 3;
                    HtmlTableCell tbcTime = new HtmlTableCell(); // 定義第一行(上課日期)
                    tbcTime.Attributes.Add("Style", "border: 2px solid black;");
                    tbcTime.Align = "center";
                    HtmlTableCell tbcState = new HtmlTableCell(); // 定義第二行(狀態)
                    tbcState.Attributes.Add("Style", "border: 2px solid black;");
                    tbcState.Align = "center";
                    HtmlTableCell tbcClassStatistics = new HtmlTableCell(); // 定義第三行(全班統計)
                    tbcClassStatistics.Attributes.Add("Style", "border: 2px solid black;");
                    tbcClassStatistics.Align = "center";
                    tbcClassStatistics.Height = "200";
                    tbcClassStatistics.Width = "50%";
                    tbcClassStatistics.ID = "tbcClassStatistics_" + drAttendanceHis["cAttendanceID"].ToString();
                    //定義出席歷史資料物件
                    Label lbAttendanceHisTitle = new Label(); // 定義點擊縮放的圖示
                    lbAttendanceHisTitle.Text = "<IMG id='img_" + drAttendanceHis["cAttendanceID"].ToString() + "' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "第" + iCount + "筆資料";
                    lbAttendanceHisTitle.Font.Size = 18;
                    Label lbTime = new Label(); // 定義上課日期
                    lbTime.Text = "<a href=GroupAttendance.aspx?ddlClass=" + ddlClass.SelectedItem.Value + "&ddlTime=" + drAttendanceHis["cAttendanceID"].ToString() + ">" + clsTimeConvert.DBTimeToDateTime(drAttendanceHis["cAttendanceID"].ToString()).ToShortDateString() + "</a>";
                    Label lbState = new Label(); // 定義狀態
                    if (drAttendanceHis["iAttendanceCond"].ToString() == "0")
                    {
                        lbState.Text = "未到";
                        lbState.ForeColor = Color.Red;
                    }
                    else if (drAttendanceHis["iAttendanceCond"].ToString() == "1")
                    {
                        lbState.Text = "已到";
                        lbState.ForeColor = Color.Blue;
                    }
                    else
                    {
                        if (drAttendanceHis["cAttendanceTime"].ToString() == "" || drAttendanceHis["cAttendanceTime"].ToString() == "NULL")
                            lbState.Text = "遲到";
                        else
                        {
                            //計算遲到時間
                            //將學生登入時間減掉遲到時間
                            TimeSpan tsTotalLateTime = clsTimeConvert.DBTimeToDateTime(drAttendanceHis["cAttendanceTime"].ToString()).Subtract(clsTimeConvert.DBTimeToDateTime(drAttendanceHis["cAttendanceID"].ToString()).AddMinutes(10));
                            //回傳遲到多少分鐘(若為0分鐘則顯示1分鐘)
                            string strTotalLateTime = tsTotalLateTime.TotalMinutes.ToString("0");
                            if (strTotalLateTime == "0")
                                strTotalLateTime = "1";
                            lbState.Text = "遲到<br />" + strTotalLateTime + "分鐘";
                        }
                        lbState.ForeColor = Color.Brown;
                    }
                    //將出席歷史資料物件加入Table
                    tbcAttendanceHisTitle.Controls.Add(lbAttendanceHisTitle);
                    tbcTime.Controls.Add(lbTime);
                    tbcState.Controls.Add(lbState);
                    tbrAttendanceHisTitle.Cells.Add(tbcAttendanceHisTitle);
                    tbrAttendanceHis.Cells.Add(tbcTime);
                    tbrAttendanceHis.Cells.Add(tbcState);
                    tbrAttendanceHis.Cells.Add(tbcClassStatistics);
                    tbInfo.Rows.Add(tbrAttendanceHisTitle);
                    tbInfo.Rows.Add(tbrAttendanceHis);
                    
                    //圓餅圖計算
                    DataTable dtAttClassHis = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_AttendanceID_GroupID(drAttendanceHis["cAttendanceID"].ToString(), ddlClass.SelectedItem.Value);
                    if (dtAttClassHis.Rows.Count > 0)
                    {
                        int iAbsenceAtt = 0; //定義未到人數
                        int iActualAtt = 0;  //定義實到人數
                        int iLateAtt = 0;    //定義遲到人數
                        foreach (DataRow drAttClassHis in dtAttClassHis.Rows)
                        {
                            if (drAttClassHis["iAttendanceCond"].ToString() == "0")
                                iAbsenceAtt++;
                            else if (drAttClassHis["iAttendanceCond"].ToString() == "1")
                                iActualAtt++;
                            else
                                iLateAtt++;
                        }
                        string strAbsenceAtt = "未到人數: " + iAbsenceAtt + "人";
                        string strActualAtt = "實到人數: " + iActualAtt + "人";
                        string strLateAtt = "遲到人數: " + iLateAtt + "人";
                        //呼叫javacript畫圖餅圖函數
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload" + drAttendanceHis["cAttendanceID"].ToString(), "<script>drawChart('" + strActualAtt + "','" + strLateAtt + "','" + strAbsenceAtt + "'," + iActualAtt + ", " + iLateAtt + "," + iAbsenceAtt + ",'" + tbcClassStatistics.ID + "')</script>");
                    }
                }
            }
            #endregion
        }
    }
    //選擇不同系所發生之事件
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入課程
        LoadClass();
        //載入學生
        LoadStudent();
        //載入總統計資料
        LoadTotalStatistics();
        //載入歷史訊息
        LoadHistory();
    }
    //選擇不同課程發生之事件
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入學生
        LoadStudent();
        //載入總統計資料
        LoadTotalStatistics();
        //載入歷史訊息
        LoadHistory();
    }
    //選擇不同學生發生之事件
    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入總統計資料
        LoadTotalStatistics();
        //載入歷史訊息
        LoadHistory();
    }
}