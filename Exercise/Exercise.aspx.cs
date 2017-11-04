﻿using System;
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
using ORCS.DB;
using ORCS.DB.Administrator;
using ORCS.DB.Attendance;
using ORCS.DB.Exercise;
using ORCS.DB.User;
using System.Drawing;
using ORCS.Base;
using System.Net;

public partial class Exercise_Exercise : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //頁面初始化
        base.ORCS_Init();
        if (!IsPostBack)
        {
            Session.Remove("strExerciseID");
            //預設學生作答狀態的Table為隱藏
            tbExCond.Visible = false;
            tbExCondGroup.Visible = false;
            tbExDetialCond.Visible = false;
            btnShowExerciseCond.Visible = false;
            btnShowExerciseAns.Visible = false;
            //學生無法修改作答時間
            if (ORCSSession.Authority == "s")
                btnModifyTime.Visible = false;
            //若上課的GroupID為NULL表示尚未上課
            if (ORCSSession.GroupID != null)
            {
                //檢查題目是否存在(預設14題)
                CheckExerciseQues(14);
                //載入作答狀態表單
                LoadExerciseStateList();
                //載入學生作答狀態名單
                LoadExerciseCond();
                //作答剩餘時間
                TimeRemnant();
            }
            //設定頁面小組ID
            HTempGroupID.Value = (ORCSSession.TempID!="")?ORCSSession.TempID:"NoGroup";
            //設定頁面使用者ID
            HUserID.Value = ORCSSession.UserID;

            //取得ServerIP
            string ServerIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .First(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .ToString();
            //取得ORCS sendMessageBySignalR.aspx頁面(http://localhost/ORCS/sendMessageBySignalR.aspx)
            string strSendMessageURL = "http://" + ServerIP + "/ORCS/sendMessageBySignalR.aspx?UserID=" + ORCSSession.UserID + "&GroupID=" + ORCSSession.TempID;
            ifSendMessage.Attributes["src"] = strSendMessageURL;
        }
        //載入動態元件
        if (IsPostBack)
        {
            if (this.ViewState["ObjectAdded"] != null && (bool)this.ViewState["ObjectAdded"] == true)
            {
                LoadExerciseStateList();
            }
            if (this.ViewState["ObjectAdded2"] != null && (bool)this.ViewState["ObjectAdded2"] == true)
            {
                LoadExerciseCond();
            }
        }
        if (Session["strExerciseID"] != null)
        {
            tbExCondALL.Attributes.Add("style", "text-align:center;border: solid 2px #000000;width:900px;");
        }
    }
    //作答狀態Timer更新事件
    protected void timExerciseState_Tick(object sender, EventArgs e)
    {
        //檢查是否有推播考卷
        CheckPushPaper();
        //作答剩餘時間
        TimeRemnant();
    }
    //答題情況Timer更新事件(傳送推播Paper事件)
    protected void timExerciseCond_Tick(object sender, EventArgs e)
    {
        //檢查是否有推播考卷
        CheckPushPaper();
        //作答剩餘時間
        TimeRemnant();
    }
    //檢查是否有推播考卷
    protected void CheckPushPaper()
    {
        //檢查是否有推播考卷
        if (ORCSSession.Authority == "s")//學生才會出現考卷
        {
            //取得考卷資料
            DataTable dtExerciseCondition = clsExercise.ORCS_ExerciseCondition_SELECT_by_cUserID_GroupID(ORCSSession.UserID, ORCSSession.GroupID);
            if (dtExerciseCondition.Rows.Count > 0)
            {
                foreach (DataRow drExerciseCondition in dtExerciseCondition.Rows)
                {
                    //3秒內有新考卷才出現考卷視窗
                    if (drExerciseCondition["cCreateTime"].ToString() != "")
                    {
                        if (clsTimeConvert.DBTimeToDateTime(drExerciseCondition["cCreateTime"].ToString()).AddSeconds(3) > DateTime.Now)
                        {
                            string strCaseID = drExerciseCondition["cCaseID"].ToString();  //CaseID
                            string strPaperID = drExerciseCondition["cPaperID"].ToString(); //PaperID
                            string strExerciseID = drExerciseCondition["cExerciseID"].ToString(); //ExerciseID
                            //開啟考卷視窗
                            if (strCaseID == "NULL" || strCaseID == "")//無考卷
                                break;
                            else //有考卷
                                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('/HINTS/Learning/Exercise/Paper_DisplayForORCS.aspx?cCaseID=" + strCaseID + "&cPaperID=" + strPaperID + "&cExerciseIDGroupID=" + strExerciseID + "-" + ORCSSession.GroupID + "&cStartTime=" + drExerciseCondition["cCreateTime"].ToString() + "','ViewPaper', 'fullscreen=yes, scrollbars=yes, resizable=yes')", true);
                        }
                    }
                }
            }
        }
    }
    //作答剩餘時間
    protected void TimeRemnant()
    {
        //取出題目
        DataTable dtExerciseQues = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("%ExerciseControl%", ORCSSession.GroupID);
        if (dtExerciseQues.Rows.Count > 0)
        {
            
            foreach (DataRow drExerciseQues in dtExerciseQues.Rows)
            {
                //正在作答以及有設作答時間則顯示作答剩餘時間
                if (drExerciseQues["iSysControlParam"].ToString() == "1" && drExerciseQues["cEndTime"].ToString() != "")
                {
                    TimeSpan tsTimeRemnant = clsTimeConvert.DBTimeToDateTime(drExerciseQues["cEndTime"].ToString()) - DateTime.Now;
                    int intTestDuration = Convert.ToInt32(tsTimeRemnant.TotalSeconds);
                    //檢查是否到達作答時間
                    if (intTestDuration >= 0)
                    {
                        //通知教師與學生作答時間只剩2分鐘
                        if (intTestDuration >= 119 && intTestDuration < 120)
                        {
                            string strExerciseNum = drExerciseQues["cSysControlName"].ToString().Split('_')[1];
                            if (ORCSSession.Authority == "t")
                            {
                                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "TimeConfirm", "TimeConfirm('" + drExerciseQues["cSysControlName"].ToString() + "','" + drExerciseQues["cEndTime"].ToString() + "','" + ORCSSession.GroupID + "','" + strExerciseNum + "')", true);
                            }
                            else
                                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "TimeAlert", "alert('第" + strExerciseNum + "份題目作答時間只剩2分鐘!')", true);
                        }
                        //檢查是否為被選取的題目
                        if (Session["strExerciseID"] != null)
                        {
                            if (drExerciseQues["cSysControlName"].ToString() == Session["strExerciseID"].ToString())
                            {
                                Session["cEndTime"] = drExerciseQues["cEndTime"].ToString();
                                if (intTestDuration > 60)
                                {
                                    int intHour = Convert.ToInt32(intTestDuration / 3600);
                                    int intTempMinut = Convert.ToInt32(intTestDuration % 3600);
                                    int intMinut = Convert.ToInt32(intTempMinut / 60);
                                    int intSecond = Convert.ToInt32(intTempMinut % 60);
                                    string strHour = "00";
                                    string strMinute = "00";
                                    string strSecond = "00";
                                    if (intHour < 10)
                                    {
                                        strHour = "0" + intHour.ToString();
                                    }
                                    else
                                    {
                                        strHour = intHour.ToString();
                                    }
                                    if (intMinut < 10)
                                    {
                                        strMinute = "0" + intMinut.ToString();
                                    }
                                    else
                                    {
                                        strMinute = intMinut.ToString();
                                    }
                                    if (intSecond < 10)
                                    {
                                        strSecond = "0" + intSecond.ToString();
                                    }
                                    else
                                    {
                                        strSecond = intSecond.ToString();
                                    }
                                    string strDuration = strHour + "'" + strMinute + "'" + strSecond;
                                    lbTimeRemnant.Text = strDuration;
                                }
                                else
                                {
                                    string strDuration = "00'00'" + intTestDuration.ToString();
                                    lbTimeRemnant.Text = strDuration;
                                }
                            }
                        }
                    }
                    else //作答時間到
                    {
                        //更新作答題目參數
                        clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(drExerciseQues["cSysControlName"].ToString(), "2", ORCSSession.GroupID);
                        lbTimeRemnant.Text = "00'00'00";
                        //重新載入作答狀態表單
                        LoadExerciseStateList();
                        //重新載入答題情況表單
                        LoadExerciseCond();
                    }
                }
                else
                {
                    //若沒設作答時間或停止作答則不顯示剩餘時間
                    if (Session["strExerciseID"] != null)
                    {
                        if (drExerciseQues["cSysControlName"].ToString() == Session["strExerciseID"].ToString())
                        {
                            tbTimeRemnant.Visible = true;
                            lbTimeRemnant.Text = "00'00'00";
                            Session["cEndTime"] = drExerciseQues["cEndTime"].ToString();
                        }
                    }
                }
            }
        }
    }
    //載入作答狀態表單
    protected void LoadExerciseStateList()
    {
        //首先清空作答狀態表單Table
        tbExDetialState.Controls.Clear();
        //取出題目
        DataTable dtExerciseQues = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("ExerciseControl%", ORCSSession.GroupID);
        if (dtExerciseQues.Rows.Count > 0)
        {
            int iAllQues = 0; // 題目總數
            //定義TableRow
            HtmlTableRow tbrQueaList = new HtmlTableRow();
            foreach (DataRow drExerciseQues in dtExerciseQues.Rows)
            {
                //定義題目狀態
                string strExerciseState = drExerciseQues["iSysControlParam"].ToString();
                //定義題目名稱
                string strExerciseName = drExerciseQues["cSysControlName"].ToString();
                //一列7個題目
                if ((iAllQues % 7) == 0)
                    tbrQueaList = new HtmlTableRow();
                //定義TableCell
                HtmlTableCell tbcQueaList = new HtmlTableCell();
                tbcQueaList.Attributes.Add("Style", "border: 2px solid black");
                tbcQueaList.Align = "center";
                tbcQueaList.VAlign = "top";
                //定義題目標題Label
                Label lbQuesTitle = new Label();
                if (drExerciseQues["iAnswerMode"].ToString().Equals("1"))//個人作答
                    lbQuesTitle.Text = "第 " + strExerciseName.Split('_')[1] + " 份(個人)<br />";
                else if (drExerciseQues["iAnswerMode"].ToString().Equals("2"))//小組作答
                    lbQuesTitle.Text = "第 " + strExerciseName.Split('_')[1] + " 份(小組)<br />";
                else 
                    lbQuesTitle.Text = "第 " + strExerciseName.Split('_')[1] + " 份<br />";
                //定義題目狀態Label
                Label lbQuesState = new Label();
                /*定義教師作答按鈕*/
                //「答題情況」
                Button btnExerciseCond = new Button();
                btnExerciseCond.ID = "btnExerciseCond." + strExerciseName;
                btnExerciseCond.CssClass = "ORCS_Exercise_button";
                btnExerciseCond.Text = "答題情況";
                btnExerciseCond.Click += new EventHandler(btnExerciseCond_Click);
                //「開放作答」
                Button btnOpenExercise = new Button();
                btnOpenExercise.ID = "btnOpenExercise." + strExerciseName;
                btnOpenExercise.CssClass = "ORCS_Exercise_button";
                btnOpenExercise.Text = "開放作答";
                btnOpenExercise.Click += new EventHandler(btnOpenExercise_Click);
                //「停止作答」
                Button btnCloseExercise = new Button();
                btnCloseExercise.ID = "btnCloseExercise." + strExerciseName;
                btnCloseExercise.CssClass = "ORCS_Exercise_button";
                btnCloseExercise.Text = "停止作答";
                btnCloseExercise.Click += new EventHandler(btnCloseExercise_Click);
                btnCloseExercise.Attributes.Add("onclick", "return confirm('確定停止學生作答?');");
                //「觀看考卷」
                Button btnViewPaper = new Button();
                btnViewPaper.CssClass = "ORCS_Exercise_button";
                btnViewPaper.Text = "觀看考卷";
                btnViewPaper.Click += new EventHandler(btnViewPaper_Click);
                //定義「觀看考卷」按鈕ID
                DataTable dtExercisePaperCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID(strExerciseName, ORCSSession.GroupID);
                if (ORCSSession.Authority == "s")//學生
                {
                    if (dtExercisePaperCond.Rows.Count > 0 && !dtExercisePaperCond.Rows[0]["cPaperID"].ToString().Equals(""))
                    {
                        btnViewPaper.Enabled = true;
                        btnViewPaper.ID = "btnViewPaper." + strExerciseName + "." + dtExercisePaperCond.Rows[0]["cCaseID"].ToString() + "." + dtExercisePaperCond.Rows[0]["cPaperID"].ToString() + "." + strExerciseState + "." + dtExercisePaperCond.Rows[0]["cCreateTime"].ToString() + "." + dtExercisePaperCond.Rows[0]["iAnswerMode"].ToString();
                    }
                    else//口說題目顯示呈灰色
                    {
                        btnViewPaper.CssClass = "ORCS_Exercise_button_CanNotClick";
                        btnViewPaper.Enabled = false;
                    }
                }
                else//教師
                {
                    if (dtExercisePaperCond.Rows.Count > 0 && !dtExercisePaperCond.Rows[0]["cPaperID"].ToString().Equals(""))
                    {
                        btnViewPaper.Enabled = true;
                        btnViewPaper.ID = "btnViewPaper." + strExerciseName + "." + dtExercisePaperCond.Rows[0]["cCaseID"].ToString() + "." + dtExercisePaperCond.Rows[0]["cPaperID"].ToString() + "." + strExerciseState + "." + dtExercisePaperCond.Rows[0]["cCreateTime"].ToString() + "." + dtExercisePaperCond.Rows[0]["iAnswerMode"].ToString();
                    }
                    else//口說題目顯示呈灰色
                    {
                        btnViewPaper.CssClass = "ORCS_Exercise_button_CanNotClick";
                        btnViewPaper.Enabled = false;
                    }
                }
                //「瀏覽作品檔案」
                Button btnViewExFile = new Button();
                btnViewExFile.ID = "btnViewExFile." + strExerciseName + "." + drExerciseQues["iUploadMode"].ToString();
                btnViewExFile.CssClass = "ORCS_Exercise_button";
                btnViewExFile.Text = "瀏覽作品檔案";
                btnViewExFile.Click += new EventHandler(btnViewExFile_Click);
                if (dtExercisePaperCond.Rows.Count > 0 && !dtExercisePaperCond.Rows[0]["cPaperID"].ToString().Equals(""))//不是口說題目
                {
                    btnViewExFile.CssClass = "ORCS_Exercise_button_CanNotClick";
                    btnViewExFile.Enabled = false;
                }
                else
                {
                    btnViewExFile.Enabled = true;
                }
                //「作答結果統計」
                Button btnViewExResult = new Button();
                btnViewExResult.CssClass = "ORCS_Exercise_button";
                btnViewExResult.Text = "作答結果統計";
                btnViewExResult.Click += new EventHandler(btnViewExResult_Click);
                DataTable dtExercisePaperCond2 = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID(strExerciseName, ORCSSession.GroupID);
                if (dtExercisePaperCond2.Rows.Count > 0 && !dtExercisePaperCond.Rows[0]["cPaperID"].ToString().Equals(""))//代表出考卷
                    btnViewExResult.ID = "btnViewExResult." + strExerciseName + "." + dtExercisePaperCond2.Rows[0]["cCaseID"].ToString() + "." + dtExercisePaperCond2.Rows[0]["cPaperID"].ToString() + "." + strExerciseState + "." + dtExercisePaperCond2.Rows[0]["iAnswerMode"].ToString() + "." + dtExercisePaperCond2.Rows[0]["cCreateTime"].ToString() + ".";
                else if (dtExercisePaperCond2.Rows.Count > 0)//代表口說題目
                {
                    btnViewExResult.ID = "btnViewExResult." + strExerciseName + "." + dtExercisePaperCond2.Rows[0]["cCaseID"].ToString() + "." + dtExercisePaperCond2.Rows[0]["cPaperID"].ToString() + "." + strExerciseState + "." + dtExercisePaperCond2.Rows[0]["iAnswerMode"].ToString() + "." + dtExercisePaperCond2.Rows[0]["cCreateTime"].ToString() + ".True." + dtExercisePaperCond2.Rows[0]["iUploadMode"].ToString();
                }
                /*定義學生作答按鈕*/
                //「作答完畢」
                //學生(個人/小組)
                DataTable dtExerciseCond = new DataTable();
                if (drExerciseQues["iAnswerMode"].ToString().Equals("1"))//個人作答
                    dtExerciseCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_cUserID(strExerciseName, ORCSSession.UserID);
                else if (drExerciseQues["iAnswerMode"].ToString().Equals("2"))//小組作答
                    dtExerciseCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_cUserID(strExerciseName, ORCSSession.TempID);
                Button btnFinishExercise = new Button();
                btnFinishExercise.ID = "btnFinishExercise." + strExerciseName + "." + drExerciseQues["iAnswerMode"].ToString();
                btnFinishExercise.Text = "作答完畢";
                btnFinishExercise.Enabled = false;
                btnFinishExercise.CssClass = "ORCS_Exercise_button_CanNotClick";
                btnFinishExercise.Click += new EventHandler(btnFinishExercise_Click);
                btnFinishExercise.Attributes.Add("onclick", "return confirm('確定作答完畢?');");
                if (dtExerciseCond.Rows.Count > 0 && dtExerciseCond.Rows[0]["iExerciseState"].ToString() == "1")
                {
                    btnFinishExercise.Text = "尚未作答";
                }
                //「作品檔案上傳」
                Button btnUploadExFile = new Button();
                btnUploadExFile.ID = "btnUploadExFile." + strExerciseName + "." + drExerciseQues["iAnswerMode"].ToString() + "." + drExerciseQues["iUploadMode"].ToString();
                btnUploadExFile.CssClass = "ORCS_Exercise_button";
                btnUploadExFile.Text = "作品檔案上傳";
                btnUploadExFile.Click += new EventHandler(btnUploadExFile_Click);
                if (dtExercisePaperCond.Rows.Count > 0 && !dtExercisePaperCond.Rows[0]["cPaperID"].ToString().Equals(""))//不是口說題目
                {
                    btnUploadExFile.CssClass = "ORCS_Exercise_button_CanNotClick";
                    btnUploadExFile.Enabled = false;
                }
                else
                {
                    btnUploadExFile.Enabled = true;
                }
                /*判斷題目狀態*/
                switch (strExerciseState)
                {
                    case "0": // 尚未開放作答
                        //Label控制
                        lbQuesState.Text = "尚未開放<br />";
                        lbQuesState.ForeColor = Color.Red;
                        //Button控制
                        //教師
                        btnExerciseCond.Visible = false;
                        btnCloseExercise.Visible = false;
                        btnViewExResult.Visible = false;
                        btnViewPaper.Visible = false;
                        btnViewExFile.Visible = false;
                        //若尚未上課則無法點選「開放作答」按鈕
                        DataTable dtSysControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
                        if (dtSysControl.Rows.Count <= 0)
                        {
                            btnOpenExercise.CssClass = "ORCS_Exercise_button_CanNotClick";
                            btnOpenExercise.Enabled = false;
                        }
                        else if (dtSysControl.Rows[0]["iSysControlParam"].ToString() == "0")
                        {
                            btnOpenExercise.CssClass = "ORCS_Exercise_button_CanNotClick";
                            btnOpenExercise.Enabled = false;
                        }
                        //學生
                        btnFinishExercise.Visible = false;
                        btnUploadExFile.Visible = false;
                        break;
                    case "1": // 開放作答
                        //Label控制
                        lbQuesState.Text = "開放作答<br />";
                        lbQuesState.ForeColor = Color.Blue;
                        //Button控制
                        //教師
                        btnOpenExercise.Visible = false;
                        btnViewPaper.Visible = true;
                        btnViewExFile.Visible = true;
                        btnViewExResult.Visible = true;
                        if (ORCSSession.Authority == "s" )
                        {
                            btnViewExResult.Visible = false;
                        }
                        break;
                    case "2": // 停止作答
                        //Label控制
                        lbQuesState.Text = "停止作答<br />";
                        //Button控制
                        //教師
                        btnOpenExercise.Visible = false;
                        btnViewExFile.Visible = true;
                        btnCloseExercise.Visible = true;
                        btnCloseExercise.CssClass = "ORCS_Exercise_button_CanNotClick";
                        btnCloseExercise.Enabled = false;
                        //學生
                        btnUploadExFile.CssClass = "ORCS_Exercise_button_CanNotClick";
                        btnUploadExFile.Enabled = false;
                        break;
                }

                //放入Table
                tbcQueaList.Controls.Add(lbQuesTitle);
                tbcQueaList.Controls.Add(lbQuesState);
                tbrQueaList.Cells.Add(tbcQueaList);
                tbExDetialState.Rows.Add(tbrQueaList);
                if (ORCSSession.Authority == "s") // 學生
                {
                    //加入換行語法(版面設定)
                    Label lbNxline = new Label();
                    lbNxline.Text = "<br />";
                    Label lbNxline2 = new Label();
                    lbNxline2.Text = "<br />";
                    Label lbNxline3 = new Label();
                    lbNxline3.Text = "<br />";
                    Label lbNxline4 = new Label();
                    lbNxline4.Text = "<br />";
                    tbcQueaList.Controls.Add(btnViewPaper);
                    tbcQueaList.Controls.Add(lbNxline);
                    tbcQueaList.Controls.Add(btnExerciseCond);
                    tbcQueaList.Controls.Add(lbNxline2);
                    //當結束作答時學生可以觀看其他人作答情況
                    if(strExerciseState.Equals("2"))
                        tbcQueaList.Controls.Add(btnViewExFile);
                    else
                        tbcQueaList.Controls.Add(btnUploadExFile);
                    tbcQueaList.Controls.Add(lbNxline3);
                    tbcQueaList.Controls.Add(btnFinishExercise);
                    tbcQueaList.Controls.Add(lbNxline4);
                    tbcQueaList.Controls.Add(btnViewExResult);
                }
                else // 教師
                {
                    //加入換行語法(版面設定)
                    Label lbNxline = new Label();
                    lbNxline.Text = "<br />";
                    Label lbNxline2 = new Label();
                    lbNxline2.Text = "<br />";
                    Label lbNxline3 = new Label();
                    lbNxline3.Text = "<br />";
                    Label lbNxline4 = new Label();
                    lbNxline4.Text = "<br />";
                    tbcQueaList.Controls.Add(btnViewPaper);
                    tbcQueaList.Controls.Add(lbNxline);
                    tbcQueaList.Controls.Add(btnExerciseCond);
                    tbcQueaList.Controls.Add(lbNxline2);
                    tbcQueaList.Controls.Add(btnViewExFile);
                    tbcQueaList.Controls.Add(lbNxline3);
                    tbcQueaList.Controls.Add(btnViewExResult);
                    tbcQueaList.Controls.Add(lbNxline4);
                    tbcQueaList.Controls.Add(btnOpenExercise);
                    tbcQueaList.Controls.Add(btnCloseExercise);
                }

                //累加應到人數
                iAllQues++;
            }
        }
        this.ViewState["ObjectAdded"] = true;
    }
    //檢查題目是否存在
    protected void CheckExerciseQues(int iQuesNum)
    {
        //檢查題目
        for (int i = 1; i <= iQuesNum; i++)
        {
            //取出題目資料
            DataTable dtExerciseQues = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("ExerciseControl_" + i, ORCSSession.GroupID);
            //若沒有題目則新增
            if (dtExerciseQues.Rows.Count <= 0)
            {
                clsSystemControl.ORCS_SystemControl_INSERT_SysName_GroupID("ExerciseControl_" + i, ORCSSession.GroupID);
            }
        }
    }
    //載入學生答題情況名單
    protected void LoadExerciseCond()
    {
        //首先清空答題情況表單Table
        tbExDetialCond.Controls.Clear();
        //取得學生答題情況名單
        DataTable dtExerciseCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID(Session["strExerciseID"], ORCSSession.GroupID);
        if (dtExerciseCond.Rows.Count > 0)
        {
            //個人/小組作答模式狀態欄
            SetExerciseCond(dtExerciseCond, dtExerciseCond.Rows[0]["iAnswerMode"].ToString());
            //顯示學生作答狀態的Table
            tbExDetialCond.Visible = true;
            //教師端顯示「允許學生觀看答題情況」按鈕
            if (ORCSSession.Authority != "s")
            {
                btnShowExerciseCond.Visible = true;
            }
        }
        else//若尚未開放任何題目則隱藏答題情況
        {
            tbExCond.Visible = false;
            btnShowExerciseCond.Visible = false;
            btnShowExerciseAns.Visible = false;
        }
        this.ViewState["ObjectAdded2"] = true;
    }

    /// <summary>
    /// 設定個人/小組答題情況欄
    /// </summary>
    /// <param name="dtExerciseCond">個人/小組答題情況資訊</param>
    /// <param name="strAnswerMode">個人(1)/小組(2)</param>
    private void SetExerciseCond(DataTable dtExerciseCond ,string strAnswerMode)
    {
        int iAllAns = 0;         //所有作答人數/小組數
        int iAllMustAns = 0;     //應作答人數/小組數
        int iActualAns = 0;      //已作答人數/小組數
        int iNotAns = 0;         //未作答人數/小組數
        int iAbsenceStudent = 0; //未到人數/小組數
        //定義TableRow
        HtmlTableRow tbrExList = new HtmlTableRow();
        foreach (DataRow drExerciseCond in dtExerciseCond.Rows)
        {
            //取得學生出席狀態(判斷應作答人員和未到人員)
            DataTable dtStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID(drExerciseCond["cUserID"].ToString(), ORCSSession.GroupID);
            string strAttState = "";
            if (dtStudentAtt.Rows.Count > 0)
                strAttState = dtStudentAtt.Rows[0]["iAttendanceCond"].ToString();
            else
                strAttState = "未到";
            //定義TableRow
            //一列10個學生
            if ((iAllAns % 10) == 0)
                tbrExList = new HtmlTableRow();
            //定義TableCell
            HtmlTableCell tbcExList = new HtmlTableCell();
            tbcExList.Attributes.Add("Style", "border: 2px solid black");
            tbcExList.Align = "center";
            //定義學生姓名Label
            Label lbStudName = new Label();
            if (strAnswerMode == "1")
            {
                lbStudName.Text = clsORCSUser.ORCS_User_SELECT_by_UserID(drExerciseCond["cUserID"].ToString()).Rows[0]["cUserName"].ToString() + "<br />";
            }
            else if (strAnswerMode == "2")
            {
                string strShowText = "";
                //取得小組名單 且要求求顯示小組成員名稱
                string strTeamName = clsGroup.ORCS_TempGroup_SELECT_By_cTempGroupID(drExerciseCond["cUserID"].ToString()).Rows[0]["cTempGroupName"].ToString();
                strShowText += strTeamName + "<br />";
                //取得小組組長ID
                string strGroupLeaderID = clsEditGroupMember.GetGroupLeaderID(drExerciseCond["cUserID"].ToString());
                //取得小組內成員
                clsORCSDB ORCSDB = new clsORCSDB();
                string strSQL = "SELECT * FROM ORCS_MemberGroup WHERE iGroupID ='" + drExerciseCond["cUserID"].ToString() + "'";
                DataTable dtTeamMenber = ORCSDB.GetDataSet(strSQL).Tables[0];
                foreach (DataRow drTeamMenber in dtTeamMenber.Rows)
                {
                    //取得組員出席狀況(0:未到,1:準時,2:遲到,3:早退)
                    string strGroupMembersCond = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID(drTeamMenber["cUserID"], ORCSSession.GroupID).Rows[0]["iAttendanceCond"].ToString();
                    if(drTeamMenber["cUserID"].Equals(strGroupLeaderID))
                    {
                        if (strGroupMembersCond.Equals("0") || strGroupMembersCond.Equals("3"))
                            strShowText += "<font style='color: Red'>" + clsORCSUser.ORCS_User_SELECT_by_UserID(drTeamMenber["cUserID"].ToString()).Rows[0]["cUserName"].ToString() + "(組長)</font><br />";
                        else
                            strShowText += clsORCSUser.ORCS_User_SELECT_by_UserID(drTeamMenber["cUserID"].ToString()).Rows[0]["cUserName"].ToString() + "(組長)<br />";
                    }
                    else
                    {
                        if (strGroupMembersCond.Equals("0") || strGroupMembersCond.Equals("3"))
                            strShowText += "<font style='color: Red'>" + clsORCSUser.ORCS_User_SELECT_by_UserID(drTeamMenber["cUserID"].ToString()).Rows[0]["cUserName"].ToString() + "(組員)</font><br />";
                        else
                            strShowText += clsORCSUser.ORCS_User_SELECT_by_UserID(drTeamMenber["cUserID"].ToString()).Rows[0]["cUserName"].ToString() + "(組員)<br />";
                    }
                }
                lbStudName.Text = strShowText;
            }
            //定義學生答題情況Label
            Label lbExState = new Label();
            if (drExerciseCond["iExerciseState"].ToString() == "0" && strAttState != "0") // 未作答學生
            {
                lbExState.ForeColor = Color.Red;
                lbExState.Text = "<img src='../App_Themes/general/images/not_ans.png' width='45px' />";
                iNotAns++;    // 累加未作答人數/小組數
                iAllMustAns++;// 累加應作答人數/小組數

            }
            else if (drExerciseCond["iExerciseState"].ToString() == "1" && strAttState != "0") // 已作答學生
            {
                lbExState.ForeColor = Color.Blue;
                lbExState.Text = "<img src='../App_Themes/general/images/actual_ans.png' width='45px' />";
                iActualAns++; // 累加已作答人數/小組數
                iAllMustAns++;// 累加應作答人數/小組數
            }
            else // 未到學生
            {
                lbExState.ForeColor = Color.Brown;
                lbExState.Text = "<img src='../App_Themes/general/images/offline.png' width='45px' />";
                iAbsenceStudent++; // 累加未到人數/小組數
                iAllMustAns++;// 累加應作答人數/小組數
            }
            /*定義作答內容按鈕*/
            //「作答內容」
            Button btnAnsCond = new Button();
            btnAnsCond.CssClass = "ORCS_Exercise_button";
            btnAnsCond.Text = "作答內容";
            btnAnsCond.Width = 80;
            btnAnsCond.Click += new EventHandler(btnAnsCond_Click);
            //定義「作答內容」按鈕ID(btnAnsCond.UserID.PaperID.iAnswerMode)
            btnAnsCond.ID = "btnAnsCond." + drExerciseCond["cUserID"].ToString() + "." + drExerciseCond["cPaperID"].ToString() + "." + drExerciseCond["cCreateTime"].ToString() + "." + drExerciseCond["iUploadMode"].ToString() + "." + drExerciseCond["cUserID"].ToString() + "." + drExerciseCond["cExerciseID"].ToString() + "." + drExerciseCond["iClassGroupID"].ToString()+ "." + drExerciseCond["iAnswerMode"].ToString();
            //教師直接顯示作答內容，學生要教師允許學生互相觀看答題答案才顯示作答內容
            if ((ShowExerciseAns(Session["strExerciseID"].ToString()) == true || ORCSSession.Authority != "s") && strAttState != "0")
                btnAnsCond.Visible = true;
            else
                btnAnsCond.Visible = false;

            //放入Table
            //加入換行語法(版面設定)
            Label lbNxline = new Label();
            lbNxline.Text = "<br />";
            tbcExList.Controls.Add(lbStudName);
            tbcExList.Controls.Add(lbExState);
            tbcExList.Controls.Add(lbNxline);
            tbcExList.Controls.Add(btnAnsCond);
            tbrExList.Cells.Add(tbcExList);
            //判斷是否顯示學生詳細答題情況
            if (ShowExerciseCond(Session["strExerciseID"].ToString()) == true || ORCSSession.Authority != "s")
                tbExDetialCond.Rows.Add(tbrExList);
            //若已經允許學生觀看答題情況則出現「允許學生互相觀看答題答案」按鈕
            if (ShowExerciseCond(Session["strExerciseID"].ToString()) == true && ORCSSession.Authority != "s")
                btnShowExerciseAns.Visible = true;
            else
                btnShowExerciseAns.Visible = false;

            //累加所有作答人數
            iAllAns++;
        }
        //計算作答人數百分比
        string strAllMustAnsPercent = (((double)iAllMustAns / (double)iAllAns) * 100).ToString("0");
        string strActualAnsPercent = (((double)iActualAns / (double)iAllAns) * 100).ToString("0");
        string strNotAnsPercent = (((double)iNotAns / (double)iAllAns) * 100).ToString("0");
        string strAbsenceAttPercent = (((double)iAbsenceStudent / (double)iAllAns) * 100).ToString("0");
        if (strAnswerMode == "1")//個人作答模式
        {
            //題目標題
            lbExerciseTitle.Text = "第" + Session["strExerciseID"].ToString().Split('_')[1] + "份(個人)作答狀況";
            //顯示應作答、未作答、未到人數/小組數
            lbExceptAns.Text = "&nbsp;&nbsp;&nbsp;應作答人數: " + iAllMustAns + " 人(" + strAllMustAnsPercent + "%)";
            lbActualAns.Text = "<img src='../App_Themes/general/images/actual_ans.png' width='25px' />已作答人數: " + iActualAns + " 人(" + strActualAnsPercent + "%)";
            lbNotAns.Text = "<img src='../App_Themes/general/images/not_ans.png' width='25px' />未作答人數: " + iNotAns + " 人(" + strNotAnsPercent + "%)";
            lbAbsenceAtt.Text = "<img src='../App_Themes/general/images/offline.png' width='25px' />未到人數: " + iAbsenceStudent + " 人(" + strAbsenceAttPercent + "%)";
        }
        else if (strAnswerMode == "2") //小組作答模式
        {
            //題目標題
            lbExerciseTitle.Text = "第" + Session["strExerciseID"].ToString().Split('_')[1] + "份(小組)作答狀況";
            //顯示應作答、未作答、未到人數/小組數
            lbExceptAns.Text = "&nbsp;&nbsp;&nbsp;應作答小組數: " + iAllMustAns + " 組(" + strAllMustAnsPercent + "%)";
            lbActualAns.Text = "<img src='../App_Themes/general/images/actual_ans.png' width='25px' />已作答小組數: " + iActualAns + " 組(" + strActualAnsPercent + "%)";
            lbNotAns.Text = "<img src='../App_Themes/general/images/not_ans.png' width='25px' />未作答小組數: " + iNotAns + " 組(" + strNotAnsPercent + "%)";
            lbAbsenceAtt.Text = "<img src='../App_Themes/general/images/offline.png' width='25px' />未到小組數: " + iAbsenceStudent + " 組(" + strAbsenceAttPercent + "%)";
        }
        //顯示學生(個人/小組)作答狀態的Table
        tbExCond.Visible = true;
    }

    //是否允許學生觀看詳細答題情況
    protected Boolean ShowExerciseCond(string strExerciseID)
    {
        //取得題目題數
        string strExerciseNo = strExerciseID.Split('_')[1];
        //定義要顯示給學生看的答題情況題目
        string strShowExerciseCond = "ShowExerciseCond_" + strExerciseNo;
        //取出ShowExerciseCond狀態，若沒有則建立
        DataTable dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID(strShowExerciseCond, ORCSSession.GroupID);
        if (dtSystemControl.Rows.Count <= 0)
        {
            clsSystemControl.ORCS_SystemControl_INSERT_SysName_GroupID(strShowExerciseCond, ORCSSession.GroupID);
            dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID(strShowExerciseCond, ORCSSession.GroupID);
        }
        //若不開放則回傳false
        if (dtSystemControl.Rows[0]["iSysControlParam"].ToString() == "0")
        {
            btnShowExerciseCond.Text = "目前不允許學生觀看答題情況";
            return false;
        }
        else
        {
            btnShowExerciseCond.Text = "目前允許學生觀看答題情況";
            return true;
        }
    }
    //是否允許允許學生互相觀看答題答案
    protected Boolean ShowExerciseAns(string strExerciseID)
    {
        //取得題目題數
        string strExerciseNo = strExerciseID.Split('_')[1];
        //定義要顯示給學生看的答題情況題目
        string strShowExerciseAns = "ShowExerciseAns_" + strExerciseNo;
        //取出ShowExerciseAns狀態，若沒有則建立
        DataTable dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID(strShowExerciseAns, ORCSSession.GroupID);
        if (dtSystemControl.Rows.Count <= 0)
        {
            clsSystemControl.ORCS_SystemControl_INSERT_SysName_GroupID(strShowExerciseAns, ORCSSession.GroupID);
            dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID(strShowExerciseAns, ORCSSession.GroupID);
        }
        //若不開放則回傳false
        if (dtSystemControl.Rows[0]["iSysControlParam"].ToString() == "0")
        {
            btnShowExerciseAns.Text = "目前不允許學生互相觀看答案";
            return false;
        }
        else
        {
            btnShowExerciseAns.Text = "目前允許學生互相觀看答案";
            return true;
        }
    }
    //「開放作答」按鈕事件
    protected void btnOpenExercise_Click(object sender, EventArgs e)
    {
        //現在時間
        string strNowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        string strNowTimeName = DateTime.Now.ToString("yyyy/MM/dd_HH:mm");
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //產生考卷所需的CaseID、PaperID和考卷名稱，要給HINTS的Paper_CaseDivisionSection資料表用
        string strCaseID = ORCSSession.UserID + "Case" + strNowTime;   //CaseID
        string strSectionName = "測驗" + (sender as Button).ID.ToString().Split('_')[1] + "(" + strNowTimeName + ")";//考卷名稱(Paper_CaseDivisionSection資料表欄位名稱為cSectionName)
        string strPaperID = ORCSSession.UserID + strNowTime;           //PaperID
        //開啟選擇題目出處
        ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "click", "window.open('/HINTS/Learning/Exercise/SelectPaperMode.aspx?cCaseID=" + strCaseID + "&cSectionName=" + strSectionName + "&cPaperID=" + strPaperID + "&cExerciseIDcGroupID=" + strExerciseID + "-" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.UserID + "','WindowOpen', 'width=500, height=680')", true);
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //重新載入作答狀態表單
        LoadExerciseStateList();
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //作答剩餘時間
        TimeRemnant();
        //顯示「允許學生觀看答題情況」按鈕
        btnShowExerciseCond.Visible = true;
        //重新載入答題情況表單
        LoadExerciseCond();
    }
    //「停止作答」按鈕事件
    protected void btnCloseExercise_Click(object sender, EventArgs e)
    {
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //更新作答題目參數
        clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(strExerciseID, "2", ORCSSession.GroupID);
        //作答剩餘時間
        TimeRemnant();
        //重新載入作答狀態表單
        LoadExerciseStateList();
        //重新載入答題情況表單
        LoadExerciseCond();
    }
    //「作答完畢」按鈕事件
    protected void btnFinishExercise_Click(object sender, EventArgs e)
    {
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //取得題目作答模式
        string strAnswerMode = (sender as Button).ID.ToString().Split('.')[2];
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //更新作答題目參數
        if (strAnswerMode == "1")
            clsExercise.ORCS_ExerciseCondition_UPDATE(strExerciseID, ORCSSession.UserID, "1", ORCSSession.GroupID);
        else if (strAnswerMode == "2")
            clsExercise.ORCS_ExerciseCondition_UPDATE(strExerciseID, ORCSSession.TempID, "1", ORCSSession.GroupID);
        //作答剩餘時間
        TimeRemnant();
        //重新載入作答狀態表單
        LoadExerciseStateList();
        //重新載入答題情況表單
        LoadExerciseCond();
    }
    //「答題情況」按鈕事件
    protected void btnExerciseCond_Click(object sender, EventArgs e)
    {
        if (ORCSSession.Authority.Equals(AllSystemUser.Authority_Teacher))
            tbTimeRemnant.Visible = true;
        tbExCondALL.Attributes.Add("style", "text-align:center;border: solid 2px #000000;width:900px;");
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //作答剩餘時間
        TimeRemnant();
        //重新載入答題情況表單
        LoadExerciseCond();
    }
    //「觀看考卷」按鈕事件
    protected void btnViewPaper_Click(object sender, EventArgs e)
    {
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //取得CaseID
        string strCaseID = (sender as Button).ID.ToString().Split('.')[2];
        //取得PaperID
        string strPaperID = (sender as Button).ID.ToString().Split('.')[3];
        //取得考卷作答狀態
        string strExerciseState = (sender as Button).ID.ToString().Split('.')[4];
        //取得考卷出題時間
        string strStartTime = (sender as Button).ID.ToString().Split('.')[5];
        //取得考卷作答模式
        string strAnswerMode = (sender as Button).ID.ToString().Split('.')[6];
        //取得個人考卷作答狀態
        string strPersonalExerciseState = "";
        //取得考卷資訊並且當作答模式為小組時,判斷使用者是否為小組組長
        bool bIsLeader = true;
        DataTable dtExerciseCond = new DataTable();
        if (strAnswerMode.Equals("1"))//個人作答 作答者用使用者ID查詢
           dtExerciseCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_cUserID(strExerciseID, ORCSSession.UserID);
        else if (strAnswerMode.Equals("2"))//小組作答 作答者用小組ID查詢
        {
            dtExerciseCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_cUserID(strExerciseID, ORCSSession.TempID);
            //取得使用者是否為小組組長
            bIsLeader = clsEditGroupMember.IsGroupLeader(ORCSSession.UserID, ORCSSession.TempID, "ChairMan");
        }
        if (dtExerciseCond.Rows.Count > 0)
            strPersonalExerciseState = dtExerciseCond.Rows[0]["iExerciseState"].ToString();
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //作答剩餘時間
        TimeRemnant();
        //重新載入答題情況表單
        LoadExerciseCond();
        //開啟考卷視窗
        if (strCaseID == "NULL" || strCaseID == "")//無考卷
            ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "alert", "alert('無考卷')", true);
        else if ((strExerciseState == "1" || ORCSSession.Authority != "s") && strPersonalExerciseState != "1")//有考卷並且尚未停止作答或身份不為學生並且尚未作答完畢
            ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('/HINTS/Learning/Exercise/Paper_DisplayForORCS.aspx?cCaseID=" + strCaseID + "&cPaperID=" + strPaperID + "&cExerciseIDGroupID=" + strExerciseID + "-" + ORCSSession.GroupID + "&cCourseID=" + ORCSSession.GroupID + "&cStartTime=" + strStartTime + "&cAnswerMode=" + strAnswerMode + "&cIsLeader=" + bIsLeader + "&cTempGroupID=" + ORCSSession.TempID + "&cUserID=" + ORCSSession.UserID + "','ViewPaper', 'fullscreen=yes, scrollbars=yes, resizable=yes')", true);
        else//有考卷並且停止作答或作答完畢
            ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('/HINTS/Learning/Exercise/Paper_InfoForORCS.aspx?cPaperID=" + strPaperID + "&cStartTime=" + strStartTime + "&cAnswerMode=" + strAnswerMode + "&cTempGroupID=" + ORCSSession.TempID + "','ViewPaper', 'fullscreen=yes, scrollbars=yes, resizable=yes')", true);
    }
    //「瀏覽作品檔案」按鈕事件
    protected void btnViewExFile_Click(object sender, EventArgs e)
    {
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //取得上傳模式
        string strUploadMode = (sender as Button).ID.ToString().Split('.')[2];
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //作答剩餘時間
        TimeRemnant();
        //重新載入答題情況表單
        LoadExerciseCond();
        //取得上課時間當ExerciseCondID
        DataTable dtSysControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
        string strDateTime = dtSysControl.Rows[0]["cStartTime"].ToString();
        //開啟作品檔案列表頁面
        ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('ViewExFile.aspx?cExerciseCondID=" + strDateTime + "&cExerciseID=" + strExerciseID + "&cGroupID=" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.UserID + "&cUploadMode=" + strUploadMode + "&cAuthority=" + ORCSSession.Authority + "','OpenFileUpload', 'width=920px, height=860px, scrollbars=yes, resizable=yes')", true);
    }
    //「作答結果統計」按鈕事件
    protected void btnViewExResult_Click(object sender, EventArgs e)
    {
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //取得CaseID
        string strCaseID = (sender as Button).ID.ToString().Split('.')[2];
        //取得PaperID
        string strPaperID = (sender as Button).ID.ToString().Split('.')[3];
        //取得考卷作答狀態
        string strExerciseState = (sender as Button).ID.ToString().Split('.')[4];
        //取得考卷作答模式(個人:1 小組:2)
        string strAnswerMode = (sender as Button).ID.ToString().Split('.')[5];
        //取得考卷開始時間
        string strStartTime = (sender as Button).ID.ToString().Split('.')[6];
        //取得是否為上傳
        string strIsUpload = (sender as Button).ID.ToString().Split('.')[7];
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //作答剩餘時間
        TimeRemnant();
        //重新載入答題情況表單
        LoadExerciseCond();
        if (strIsUpload.Equals("True"))//口說題目
        {
            //取得上傳模式
            string strUploadMode = (sender as Button).ID.ToString().Split('.')[8];
            //取得上課時間當ExerciseCondID
            DataTable dtSysControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
            string strDateTime = dtSysControl.Rows[0]["cStartTime"].ToString();

            ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('ShowUploadStatic.aspx?cExerciseCondID=" + strDateTime + "&cExerciseID=" + strExerciseID + "&cGroupID=" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.UserID + "&cUploadMode=" + strUploadMode + "&cAuthority=" + ORCSSession.Authority + "','OpenFileUpload', 'width=600px, height=200px, scrollbars=yes, resizable=yes')", true);
            //ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_result", "window.open('.aspx?cExerciseID=" + strExerciseID + "&cPaperID=" + strPaperID + "&cUserID=" + ORCSSession.UserID + "&cAuthority=" + ORCSSession.Authority + "&cAnswerMode=" + strAnswerMode + "&cCourseID=" + ORCSSession.GroupID + "&cStartTime=" + strStartTime + "','ViewExResult', 'fullscreen=yes, scrollbars=yes, resizable=yes')", true);
        }
        else//考卷
        {
            //開啟考卷視窗
            if (strCaseID == "NULL" || strCaseID == "")//無考卷
                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "alert", "alert('無考卷，無法瀏覽作答結果')", true);
            else//有考卷並且停止作答
                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_result", "window.open('/HINTS/Learning/Exercise/Paper_ORCSExResult.aspx?cExerciseID=" + strExerciseID + "&cPaperID=" + strPaperID + "&cUserID=" + ORCSSession.UserID + "&cAuthority=" + ORCSSession.Authority + "&cAnswerMode=" + strAnswerMode + "&cCourseID=" + ORCSSession.GroupID + "&cStartTime=" + strStartTime + "','ViewExResult', 'fullscreen=yes, scrollbars=yes, resizable=yes')", true);
        }

    }
    //「作品檔案上傳」按鈕事件
    protected void btnUploadExFile_Click(object sender, EventArgs e)
    {
        //取得題目ID
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[1];
        //取得作答模式(0:尚未作答,1:個人作答,2:小組作答)
        string strAnswerMode = (sender as Button).ID.ToString().Split('.')[2];
        //取得上傳模式(0:上傳檔案,1:上傳文字)
        string strUploadMode = (sender as Button).ID.ToString().Split('.')[3];
        //題目標題
        lbExerciseTitle.Text = "第" + strExerciseID.Split('_')[1] + "份";
        //紀錄題目ID
        Session["strExerciseID"] = strExerciseID;
        //作答剩餘時間
        TimeRemnant();
        //重新載入答題情況表單
        LoadExerciseCond();
        //取得上課時間當ExerciseCondID
        DataTable dtSysControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
        string strDateTime = dtSysControl.Rows[0]["cStartTime"].ToString();
        //開啟上傳檔案頁面
        if (strAnswerMode == "1")       //個人作答模式 作答者ID記錄個人ID
        {
            if (strUploadMode.Equals("0"))
                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('FileUpload.aspx?cExerciseCondID=" + strDateTime + "&cExerciseID=" + strExerciseID + "&cGroupID=" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.UserID + "','FileUpload', 'width=630px, height=270px')", true);
            else if(strUploadMode.Equals("1"))
                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('TextUpload.aspx?cExerciseCondID=" + strDateTime + "&cExerciseID=" + strExerciseID + "&cGroupID=" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.UserID + "','FileUpload', 'width=700px, height=850px, scrollbars=yes, resizable=yes')", true);
        }
        else if (strAnswerMode == "2")   //小組作答模式 作答者ID記錄小組ID
        {
            //取得小組組長ID
            string strGroupLeaderID = clsEditGroupMember.GetGroupLeaderID(ORCSSession.TempID);
            //取得組長出席狀況(0:未到,1:準時,2:遲到,3:早退)
            string strGroupLeaderCond = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID(strGroupLeaderID, ORCSSession.GroupID).Rows[0]["iAttendanceCond"].ToString();

            //如果組長遲到或早退將由組員代答
            if (strGroupLeaderID.Equals(ORCSSession.UserID) || (strGroupLeaderCond.Equals("0") || strGroupLeaderCond.Equals("3")))
            {
                //不是組長時會提示小組成員組長不在
                if (!strGroupLeaderID.Equals(ORCSSession.UserID))
                {
                    ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_alert", "alert('本題為小組作答模式，但由於組長不在可由小組成員回答');", true);
                }
                if (strUploadMode.Equals("0"))
                    ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('FileUpload.aspx?cExerciseCondID=" + strDateTime + "&cExerciseID=" + strExerciseID + "&cGroupID=" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.TempID + "','FileUpload', 'width=630px, height=270px')", true);
                if (strUploadMode.Equals("1"))
                    ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('TextUpload.aspx?cExerciseCondID=" + strDateTime + "&cExerciseID=" + strExerciseID + "&cGroupID=" + ORCSSession.GroupID + "&cUserID=" + ORCSSession.TempID + "','FileUpload', 'width=700px, height=850px, scrollbars=yes, resizable=yes')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_alert", "alert('本題為小組作答模式，請由組長作答!');", true);
            }
        }
    }
    //「作答內容」按鈕事件
    protected void btnAnsCond_Click(object sender, EventArgs e)
    {
        //取得作答者ID(個人ID/小組ID)
        string strUserID = (sender as Button).ID.ToString().Split('.')[1];
        //取得PaperID
        string strPaperID = (sender as Button).ID.ToString().Split('.')[2];
        //取得考試時間
        string strStartTime = (sender as Button).ID.ToString().Split('.')[3];
        //上傳模式(0:上傳檔案,1:上傳文字)
        string strUploadMode = (sender as Button).ID.ToString().Split('.')[4];
        //作答者ID
        string strStudentID = (sender as Button).ID.ToString().Split('.')[5];
        //開始時間
        string strExerciseID = (sender as Button).ID.ToString().Split('.')[6];
        //課程ID
        string strCourseID = (sender as Button).ID.ToString().Split('.')[7];
        //作答模式
        string strAnswerMode = (sender as Button).ID.ToString().Split('.')[8];
        //取得上課時間當ExerciseCondID
        DataTable dtSysControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", strCourseID);
        string strExerciseCondID = dtSysControl.Rows[0]["cStartTime"].ToString();

        //開啟學生作答內容視窗
        if (strUploadMode.Equals("0"))//上傳檔案
        {
            DataTable dtFileUploadData = clsExercise.ORCS_FileUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, strStudentID);
            if (dtFileUploadData.Rows.Count > 0)
            {
                //取得檔案名稱
                string strFileName = dtFileUploadData.Rows[0]["cFileName"].ToString();
                //取得檔案副檔名(變大寫)
                string strSubFileName = strFileName.Split('.')[1].ToUpper();
                //檔案路徑
                string strFilePath = "../Exercise/ExFileUpload/" + strExerciseCondID + "/" + strExerciseID + "/" + strStudentID + "/" + strFileName;
                //若檔案符合以下檔案則可直接從網頁瀏覽:PDF,JPG,JPEG,GIF,PNG,BMP
                if (strSubFileName == "PDF" || strSubFileName == "JPG" || strSubFileName == "JPEG"
                    || strSubFileName == "GIF" || strSubFileName == "PNG" || strSubFileName == "BMP" || strSubFileName == "TXT")
                    ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "click", "window.open('" + strFilePath + "','OpenHtml', 'resizable=yes, width=700px, height=620px ,scrollbars=yes')", true);
                else if (strSubFileName == "DOC" || strSubFileName == "DOCX" || strSubFileName == "PPT" || strSubFileName == "PTTX"
                            || strSubFileName == "XLS" || strSubFileName == "XLSX")
                    ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "click", "window.open('https://docs.google.com/viewer?url=http://140.116.72.28/ORCS/Exercise/" + HttpUtility.UrlEncode(strFilePath) + "','OpenHtml', 'resizable=yes, width=700px, height=620px,scrollbars=yes')", true);
                else
                    ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "alert", "alert('此檔案無法直接在網頁瀏覽，請由瀏覽作品檔案頁面下載此檔案')", true);
                return;
            }
        }
        else if (strUploadMode.Equals("1"))//上傳文字
        {
            DataTable dtTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, strStudentID);
            if (dtTextUploadData.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('ShowTextUpload.aspx?cExerciseCondID=" + strExerciseCondID + "&cExerciseID=" + strExerciseID + "&cGroupID=" + strCourseID + "&cUserID=" + strStudentID + "&cAnswerMode=" + strAnswerMode + "&cAuthority=" + ORCSSession.Authority + "','FileUpload', 'width=700px, height=800px')", true);
                return;
            }
        }
        if (strPaperID != "NULL" && strPaperID != "")
        {

            ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paperInfo", "window.open('/HINTS/Learning/Exercise/Paper_InfoForAnsCond.aspx?cUserID=" + strUserID + "&cPaperID=" + strPaperID + "&cTempGroupID=" + strUserID + "&cStartTime=" + strStartTime + "','WindowOpen', 'fullscreen=yes, scrollbars=yes, resizable=yes')", true);
        }
        else
        {
            //無考卷
            ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "alert", "alert('尚未作答')", true);
        }
    }
    //「允許學生觀看答題情況」按鈕事件
    protected void btnShowExerciseCond_Click(object sender, EventArgs e)
    {
        //取得題目題數
        string strExerciseNo = Session["strExerciseID"].ToString().Split('_')[1];
        //定義要顯示給學生看的答題情況題目
        string strShowExerciseCond = "ShowExerciseCond_" + strExerciseNo;
        //更新ShowExerciseCond狀態
        if (btnShowExerciseCond.Text == "目前不允許學生觀看答題情況") // 允許
        {
            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(strShowExerciseCond, "1", ORCSSession.GroupID);
            btnShowExerciseCond.Text = "目前允許學生觀看答題情況";
            btnShowExerciseAns.Visible = true;//顯示「允許學生互相觀看答題答案」按鈕
        }
        else // 不允許
        {
            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(strShowExerciseCond, "0", ORCSSession.GroupID);
            btnShowExerciseCond.Text = "目前不允許學生觀看答題情況";
            btnShowExerciseAns.Visible = false;//隱藏「允許學生互相觀看答題答案」按鈕
        }
        //重新載入答題情況表單
        LoadExerciseCond();
    }
    //「允許學生互相觀看答題答案」按鈕事件
    protected void btnShowExerciseAns_Click(object sender, EventArgs e)
    {
        //取得題目題數
        string strExerciseNo = Session["strExerciseID"].ToString().Split('_')[1];
        //定義要顯示給學生看的答題情況題目
        string strShowExerciseAns = "ShowExerciseAns_" + strExerciseNo;
        //更新ShowExerciseCond狀態
        if (btnShowExerciseAns.Text == "目前不允許學生互相觀看答案") // 允許
        {
            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(strShowExerciseAns, "1", ORCSSession.GroupID);
            btnShowExerciseAns.Text = "目前允許學生互相觀看答案";
            //取得ORCS判斷匿名資料(ShowStaticticsName_題號)
            clsORCSDB ORCSDB = new clsORCSDB();     //呼叫ORCS資料庫
            string strShowExerciseStaticticsName = "ShowStaticticsName_" + strExerciseNo;
            string strSQL = "SELECT * FROM ORCS_SystemControl WHERE cSysControlName = '" + strShowExerciseStaticticsName + "' AND iClassGroupID = '" + ORCSSession.GroupID + "'";
            DataTable dtORCSSysControl = ORCSDB.GetDataSet(strSQL).Tables[0];
            //若沒資料則直接新增
            if (dtORCSSysControl.Rows.Count <= 0)
            {
                strSQL = "INSERT INTO ORCS_SystemControl(cSysControlName, iSysControlParam, iClassGroupID) VALUES('" + strShowExerciseStaticticsName + "','1','" + ORCSSession.GroupID + "')";
                ORCSDB.ExecuteNonQuery(strSQL);
            }
            else
            {
                strSQL = "UPDATE ORCS_SystemControl SET iSysControlParam = '1' WHERE cSysControlName = '" + strShowExerciseStaticticsName + "' AND iClassGroupID = '" + ORCSSession.GroupID + "'";
                ORCSDB.ExecuteNonQuery(strSQL);
            }

        }
        else // 不允許
        {
            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(strShowExerciseAns, "0", ORCSSession.GroupID);
            btnShowExerciseAns.Text = "目前不允許學生互相觀看答案";
            //取得ORCS判斷匿名資料(ShowStaticticsName_題號)
            clsORCSDB ORCSDB = new clsORCSDB();     //呼叫ORCS資料庫
            string strShowExerciseStaticticsName = "ShowStaticticsName_" + strExerciseNo;
            string strSQL = "SELECT * FROM ORCS_SystemControl WHERE cSysControlName = '" + strShowExerciseStaticticsName + "' AND iClassGroupID = '" + ORCSSession.GroupID + "'";
            DataTable dtORCSSysControl = ORCSDB.GetDataSet(strSQL).Tables[0];
            //若沒資料則直接新增
            if (dtORCSSysControl.Rows.Count <= 0)
            {
                strSQL = "INSERT INTO ORCS_SystemControl(cSysControlName, iSysControlParam, iClassGroupID) VALUES('" + strShowExerciseStaticticsName + "','0','" + ORCSSession.GroupID + "')";
                ORCSDB.ExecuteNonQuery(strSQL);
            }
            else
            {
                strSQL = "UPDATE ORCS_SystemControl SET iSysControlParam = '0' WHERE cSysControlName = '" + strShowExerciseStaticticsName + "' AND iClassGroupID = '" + ORCSSession.GroupID + "'";
                ORCSDB.ExecuteNonQuery(strSQL);
            }
        }
        //重新載入答題情況表單
        LoadExerciseCond();
    }
    //「修改作答時間」按鈕事件
    protected void btnModifyTime_Click(object sender, EventArgs e)
    {
        //開啟修改作答時間視窗
        ScriptManager.RegisterClientScriptBlock(upalExerciseState, this.GetType(), "open_paper", "window.open('ModifyExerciseTime.aspx?cExerciseID=" + Session["strExerciseID"].ToString() + "&cEndTime=" + Session["cEndTime"] + "&cGroupID=" + ORCSSession.GroupID + "','OpenModifyExerciseTime', 'width=380px, height=300px')", true);
    }
}