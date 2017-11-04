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
using ORCS.DB.Exercise;
using ORCS.DB.User;
using System.Drawing;

public partial class DataMining_GroupExercise : ORCS.ORCS_Page
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
            //載入全班作答歷史訊息
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
            DataTable dtTime = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_DISTINCT_GroupID(ddlClass.SelectedItem.Value);
            if (dtTime.Rows.Count > 0)
            {
                foreach (DataRow drTime in dtTime.Rows)
                {
                    string strEcerciseCondID = drTime["cExerciseCondID"].ToString();
                    //將上課時段加入DropDownList
                    ddlTime.Items.Add(new ListItem(clsTimeConvert.DBTimeToDateTime(strEcerciseCondID).ToShortDateString(), strEcerciseCondID));
                }
            }
        }
    }
    //載入全班作答歷史訊息
    protected void LoadClassAtt()
    {
        //先清空table物件
        tbClassEx.Controls.Clear();
        if (ddlTime.Items.Count > 0)
        {
            #region 標題列
            //定義標題列
            HtmlTableRow tbrTitle = new HtmlTableRow();
            HtmlTableCell tbcTitleQues = new HtmlTableCell();
            tbcTitleQues.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleQues.Align = "center";
            HtmlTableCell tbcTitleFinAns = new HtmlTableCell();
            tbcTitleFinAns.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleFinAns.Align = "center";
            HtmlTableCell tbcTitleNoAns = new HtmlTableCell();
            tbcTitleNoAns.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleNoAns.Align = "center";
            HtmlTableCell tbcTitleClassStatistics = new HtmlTableCell();
            tbcTitleClassStatistics.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleClassStatistics.Align = "center";
            tbcTitleClassStatistics.Width = "55%";
            //定義標題列物件
            Label lbTitleQues = new Label();
            lbTitleQues.Text = "習題";
            Label lbTitleFinAns = new Label();
            lbTitleFinAns.Text = "已作答";
            Label lbTitleNoAns = new Label();
            lbTitleNoAns.Text = "未作答";
            Label lbTitleClassStatistics = new Label();
            lbTitleClassStatistics.Text = "全班統計";
            //將物件加入標題列
            tbcTitleQues.Controls.Add(lbTitleQues);
            tbcTitleFinAns.Controls.Add(lbTitleFinAns);
            tbcTitleNoAns.Controls.Add(lbTitleNoAns);
            tbcTitleClassStatistics.Controls.Add(lbTitleClassStatistics);
            tbrTitle.Cells.Add(tbcTitleQues);
            tbrTitle.Cells.Add(tbcTitleFinAns);
            tbrTitle.Cells.Add(tbcTitleNoAns);
            tbrTitle.Cells.Add(tbcTitleClassStatistics);
            tbClassEx.Rows.Add(tbrTitle);
            #endregion

            #region 內容
            //讀取作答歷史資料
            DataTable dtExerciseHis = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_DISTINCT_ExerciseCondID_GroupID(ddlTime.SelectedItem.Value, ddlClass.SelectedItem.Value);
            int iCountNum = 0;//定義題數
            if (dtExerciseHis.Rows.Count > 0)
            {
                foreach (DataRow drExerciseHis in dtExerciseHis.Rows)
                {
                    //先累加題數
                    iCountNum++;
                    //定義TableRow和TableCell
                    HtmlTableRow tbrExerciseHisTitle = new HtmlTableRow(); // 定義子標題列(點擊縮放的地方)
                    HtmlTableRow tbrExerciseHis = new HtmlTableRow(); // 定義歷史資料列
                    tbrExerciseHis.ID = "tbrExerciseHis_" + drExerciseHis["cExerciseID"].ToString();
                    HtmlTableCell tbcExerciseHisTitle = new HtmlTableCell(); // 定義子標題行
                    tbcExerciseHisTitle.Attributes.Add("Style", "background-color: #C6dfff; CURSOR: hand; border: 2px solid black;");
                    tbcExerciseHisTitle.Attributes.Add("onclick", "ShowChart('" + tbrExerciseHis.ID + "' , 'img_" + drExerciseHis["cExerciseID"].ToString() + "')");
                    tbcExerciseHisTitle.ColSpan = 4;
                    HtmlTableCell tbcQues = new HtmlTableCell(); // 定義第一行(習題)
                    tbcQues.Attributes.Add("Style", "border: 2px solid black;");
                    tbcQues.Align = "center";
                    HtmlTableCell tbcFinAns = new HtmlTableCell(); // 定義第二行(已作答)
                    tbcFinAns.Attributes.Add("Style", "border: 2px solid black;");
                    tbcFinAns.Align = "center";
                    HtmlTableCell tbcNoAns = new HtmlTableCell(); // 定義第三行(未作答)
                    tbcNoAns.Attributes.Add("Style", "border: 2px solid black;");
                    tbcNoAns.Align = "center";
                    HtmlTableCell tbcClassStatistics = new HtmlTableCell(); // 定義第四行(全班統計)
                    tbcClassStatistics.Attributes.Add("Style", "border: 2px solid black;");
                    tbcClassStatistics.Align = "center";
                    tbcClassStatistics.ID = "tbcClassStatistics_" + drExerciseHis["cExerciseID"].ToString();
                    tbcClassStatistics.Height = "200";
                    //定義作答歷史資料物件
                    Label lbExerciseHisTitle = new Label(); // 定義點擊縮放的圖示
                    lbExerciseHisTitle.Text = "<IMG id='img_" + drExerciseHis["cExerciseID"].ToString() + "' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "第" + iCountNum + "份";
                    lbExerciseHisTitle.Font.Size = 18;
                    Label lbQuesNum = new Label(); // 定義習題題號
                    lbQuesNum.Text = "第" + iCountNum + "份";
                    //將作答歷史資料物件加入Table
                    tbcExerciseHisTitle.Controls.Add(lbExerciseHisTitle);
                    tbcQues.Controls.Add(lbQuesNum);
                    tbrExerciseHisTitle.Cells.Add(tbcExerciseHisTitle);
                    tbrExerciseHis.Cells.Add(tbcQues);
                    tbrExerciseHis.Cells.Add(tbcFinAns);
                    tbrExerciseHis.Cells.Add(tbcNoAns);
                    tbrExerciseHis.Cells.Add(tbcClassStatistics);
                    tbClassEx.Rows.Add(tbrExerciseHisTitle);
                    tbClassEx.Rows.Add(tbrExerciseHis);

                    //抓取每個習題詳細資料(已作答人員、未作答人員和全班統計)
                    DataTable dtExerciseDetailHis = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_ExerciseCondID_ExerciseID_GroupID(ddlTime.SelectedItem.Value, drExerciseHis["cExerciseID"].ToString(), ddlClass.SelectedItem.Value);
                    if (dtExerciseDetailHis.Rows.Count > 0)
                    {
                        int iNoAns = 0; //定義未作答人數
                        int iFinAns = 0;  //定義已作答人數
                        string strNoAns = "";
                        string strFinAns = "";
                        foreach (DataRow drExerciseDetailHis in dtExerciseDetailHis.Rows)
                        {
                            //全班的話只統計個人作答模式
                            if (drExerciseDetailHis["iAnswerMode"].ToString().Equals("1"))
                            {
                                //取得使用者資料
                                DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(drExerciseDetailHis["cUserID"].ToString());
                                //定義歷史出席資料物件
                                Label lbStudentName = new Label();
                                lbStudentName.Text = "<a href=PersonalExercise.aspx?ddlClass=" + ddlClass.SelectedItem.Value + "&ddlStudent=" + drExerciseDetailHis["cUserID"].ToString() + ">" + dtUser.Rows[0]["cUserName"].ToString() + "</a><br />";
                                if (drExerciseDetailHis["iExerciseState"].ToString() == "0")
                                {
                                    tbcNoAns.Controls.Add(lbStudentName);
                                    iNoAns++;
                                }
                                else
                                {
                                    tbcFinAns.Controls.Add(lbStudentName);
                                    iFinAns++;
                                }
                                strNoAns = "未作答人數: " + iNoAns + "人";
                                strFinAns = "已作答人數: " + iFinAns + "人";
                            }else if(drExerciseDetailHis["iAnswerMode"].ToString().Equals("2"))//小組
                            {
                                //取得使用者資料
                                //取得小組名單 且要求求顯示小組成員名稱
                                string strTeamName = clsGroup.ORCS_TempGroup_SELECT_By_cTempGroupID(drExerciseDetailHis["cUserID"].ToString()).Rows[0]["cTempGroupName"].ToString();
                                //定義歷史出席資料物件
                                Label lbStudentName = new Label();
                                lbStudentName.Text = "<a href=TeamExercise.aspx?ddlClass=" + ddlClass.SelectedItem.Value + "&ddlTeamID=" + drExerciseDetailHis["cUserID"].ToString() + ">" + strTeamName + "</a><br />";
                                if (drExerciseDetailHis["iExerciseState"].ToString() == "0")
                                {
                                    tbcNoAns.Controls.Add(lbStudentName);
                                    iNoAns++;
                                }
                                else
                                {
                                    tbcFinAns.Controls.Add(lbStudentName);
                                    iFinAns++;
                                }
                                strNoAns = "未作答組數: " + iNoAns + "組";
                                strFinAns = "已作答組數: " + iFinAns + "組";
                            }
                        }
                        //呼叫javacript畫圖餅圖函數
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload" + drExerciseHis["cExerciseID"].ToString(), "<script>drawChart('" + strFinAns + "','" + strNoAns + "'," + iFinAns + ", " + iNoAns + ",'" + tbcClassStatistics.ID + "')</script>");
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
        //載入上課時段
        LoadTime();
        //載入全班作答歷史訊息
        LoadClassAtt();
    }
    //選擇不同課程發生之事件
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入上課時段
        LoadTime();
        //載入全班作答歷史訊息
        LoadClassAtt();
    }
    //選擇不同上課時段發生之事件
    protected void ddlTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入全班作答歷史訊息
        LoadClassAtt();
    }
}