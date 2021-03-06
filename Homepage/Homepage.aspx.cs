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
using Hints.DB;
using ORCS.Base;

public partial class Homepage_Homepage : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //頁面初始化
        base.ORCS_Init();
        if (!IsPostBack)
        {
            //若身分為學生則
            if (ORCSSession.Authority == "s") // 學生
            {
                btnOpenClass.Visible = false;
                btnCloseClass.Visible = false;
                btnEditGroup.Visible = false;
            }
            else if (ORCSSession.Authority == "t") // 教師
            {
                btnEditGroup.Visible = false;
                btnExercise.Visible = true;
            }
            else if (ORCSSession.Authority == "a") // 助教
            {
                btnOpenClass.Visible = false;
                btnCloseClass.Visible = false;
                btnEditGroup.Visible = false;
            }
            else // 管理員(x)
            {
                btnOpenClass.Visible = false;
                btnCloseClass.Visible = false;
                btnAttendance.Visible = false;
                btnExercise.Visible = false;
            }
        }
        //系統狀態
        SystemState();
        //初始按鈕事件
        btnCloseClass.Attributes.Add("onclick", "return confirm('下課後學生即無法進入系統，確定要繼續嗎?');");
    }
    //「上課」按鈕事件
    protected void btnOpenClass_Click(object sender, EventArgs e)
    {
        //載入班級選擇
        LoadClass();
        //顯示班級選擇視窗
        divChoiceClass.Visible = true;
    }
    //「下課」按鈕事件
    protected void btnCloseClass_Click(object sender, EventArgs e)
    {
        //儲存學生出席狀況和作答狀況
        SaveStudentAttendanceAndExerciseCond();
        //下課時記錄該堂課下課時間
        clsClassTimeRecord.updateEndClassTimeRecord(ORCSSession.GroupID, System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        //下課時刪除學生出席名單
        clsAttendance.ORCS_StudentAttendance_DELETE_by_UserID_GroupID("%", ORCSSession.GroupID);
        //下課時刪除學生題目作答名單
        clsExercise.ORCS_ExerciseCondition_DELETE_by_cExerciseID_GroupID("%ExerciseControl%", ORCSSession.GroupID);
        //刪除遲到時間
        clsTimeControl.ORCS_TimeControl_DELETE_TimeConName_GroupID("LateTime", ORCSSession.GroupID);
        //將系統改為下課(0)
        clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID("SystemControl", "0", "", "", ORCSSession.GroupID);
        //將作答狀態改為未開放(0)並且作答模式改成尚未設定(0)
        clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID_AnswerMode("%ExerciseControl%", "0", "", "", ORCSSession.GroupID,"0");
        //判斷目前系統設定為上課(1)或下課(0)
        SystemState();
        //將上課中GroupID的Session設為NULL
        ORCSSession.GroupID = null;
    }
    //「出席狀況」按鈕事件
    protected void btnAttendance_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Attendance/Attendance.aspx");
    }
    //「課堂練習」按鈕事件
    protected void btnExercise_Click(object sender, EventArgs e)
    {
        Session["GroupMod"] = "Individual";
        Response.Redirect("../Exercise/Exercise.aspx");
    }
    //「群組管理」按鈕事件
    protected void btnEditGroup_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Administrator/EditGroupMember.aspx");
    }
    //選擇「確定」事件
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ddlClass.SelectedItem == null)
            Response.Write("<script>alert('無所屬的班級，無法上課')</script>");
        else
        {
            //初始上課時間記錄到ORCS_ClassTimeRecord
            clsClassTimeRecord.addInintialClassTimeRecord(ddlClass.SelectedItem.Value, DateTime.Now.ToString("yyyyMMddHHmmss"));
            //預設12分鐘後遲到，將時間加入TimeControl資料表
            clsTimeControl.ORCS_TimeControl_INSERT_TimeName_Time_GroupID("LateTime", DateTime.Now.AddMinutes(12).ToString("yyyyMMddHHmmss"), ddlClass.SelectedItem.Value);
            //將系統改為上課(1)
            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID("SystemControl", "1", DateTime.Now.ToString("yyyyMMddHHmmss"), "", ddlClass.SelectedItem.Value);

            //將目前上課GroupID存入Session裡
            ORCSSession.GroupID = ddlClass.SelectedItem.Value;
            //系統狀態
            SystemState();
            //載入學生出席狀況名單
            LoadStudentAttendance();
            //隱藏班級選擇視窗
            divChoiceClass.Visible = false;
        }
    }
    //選擇「取消」事件
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //隱藏班級選擇視窗
        divChoiceClass.Visible = false;
    }
    //載入班級選擇
    protected void LoadClass()
    {
        //先清空系所和班級
        ddlDepartment.Items.Clear();
        ddlClass.Items.Clear();
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
        //載入班級
        if (ddlDepartment.Items.Count > 0)
        {
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
    //選擇不同系所發生之事件
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //先清空班級
        ddlClass.Items.Clear();
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
    //判斷目前系統設定為上課(1)或下課(0)
    protected void SystemState()
    {
        DataTable dtSystemControl;
        dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
        string strSystemState = "0";
        if (dtSystemControl.Rows.Count > 0)
            strSystemState = dtSystemControl.Rows[0]["iSysControlParam"].ToString();
        //設定上課和下課按鈕狀態
        //下課時
        if (strSystemState == "0")
        {
            btnOpenClass.Enabled = true;
            btnCloseClass.Enabled = false;
            btnAttendance.Enabled = false;
            btnExercise.Enabled = false;

            //Remove hand cursor on disabled button in C# Asp,and recover after the button is enabled.
            btnOpenClass.Style.Remove(HtmlTextWriterStyle.Cursor);
            btnCloseClass.Style.Add(HtmlTextWriterStyle.Cursor, "default");
            btnAttendance.Style.Add(HtmlTextWriterStyle.Cursor, "default");
            btnExercise.Style.Add(HtmlTextWriterStyle.Cursor, "default");
        }
        //上課時
        else
        {
            btnOpenClass.Enabled = false;
            btnCloseClass.Enabled = true;
            btnAttendance.Enabled = true;
            btnExercise.Enabled = true;

            //Remove hand cursor on disabled button in C# Asp,and recover after the button is enabled.
            btnOpenClass.Style.Add(HtmlTextWriterStyle.Cursor, "default");
            btnCloseClass.Style.Remove(HtmlTextWriterStyle.Cursor);
            btnAttendance.Style.Remove(HtmlTextWriterStyle.Cursor);
            btnExercise.Style.Remove(HtmlTextWriterStyle.Cursor);
        }
    }
    //載入學生出席狀況名單
    protected void LoadStudentAttendance()
    {
        //記錄個人出席狀態
        //檢查使用者是否屬於上課的班級，若是則存入出席名單裡
        DataTable dtGroupMember = clsEditGroupMember.ORCS_GroupMember_SELECT_by_GroupID_Classify_Authority(ORCSSession.GroupID, clsGroupNode.GroupClassification_ClassGroup,AllSystemUser.Authority_Student);
        //插入ORCS_StudentAttendance資料表，出席狀況預設為0
        if (dtGroupMember.Rows.Count > 0)
            foreach (DataRow drGroupMember in dtGroupMember.Rows)
                clsAttendance.ORCS_StudentAttendance_INSERT_UserID_AttCond_GroupID_iGroupMode(drGroupMember["cUserID"].ToString(), "0", ORCSSession.GroupID, "0");

        //記錄小組出席狀態
        DataTable dtGroupInfo = clsEditGroup.ORCS_TempGroup_SELECT_by_iClassGroupID(ORCSSession.GroupID);
        if (dtGroupInfo.Rows.Count > 0)
        {
            foreach (DataRow drGroupInfo in dtGroupInfo.Rows)
            {
                //插入ORCS_StudentAttendance資料表，出席狀況預設為0
                clsAttendance.ORCS_StudentAttendance_INSERT_UserID_AttCond_GroupID_iGroupMode(drGroupInfo["iTempGroupID"].ToString(), "0", ORCSSession.GroupID,"1");
            }
        }
    }
    //儲存學生出席狀況和作答狀況
    protected void SaveStudentAttendanceAndExerciseCond()
    {
        //取得上課時間當ID(Date Time格式:20101023125323，Convert.ToDateTime("2010-10-23 12:53:23"))
        DataTable dtSysControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
        string strDateTime = dtSysControl.Rows[0]["cStartTime"].ToString();
        //儲存出席狀態
        DataTable dtAttendance = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID("%", ORCSSession.GroupID);
        if (dtAttendance.Rows.Count > 0)
        {
            foreach (DataRow drAttendance in dtAttendance.Rows)
                clsAttendance.ORCS_StudentAttendanceHistory_INSERT(strDateTime, drAttendance["cUserID"].ToString(), drAttendance["iAttendanceCond"].ToString(), drAttendance["cAttendanceTime"].ToString(), drAttendance["cIPAddress"].ToString(), drAttendance["iClassGroupID"].ToString(), drAttendance["iGroupMode"].ToString());
        }
        //儲存作答狀態
        DataTable dtExercise = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID("%", ORCSSession.GroupID);
        if (dtExercise.Rows.Count > 0)
        {
            foreach (DataRow drExercise in dtExercise.Rows)
                clsExercise.ORCS_ExerciseConditionHistory_INSERT(strDateTime, drExercise["cExerciseID"].ToString(), drExercise["cUserID"].ToString(), drExercise["iExerciseState"].ToString(), drExercise["iClassGroupID"].ToString(), drExercise["cCaseID"].ToString(), drExercise["cPaperID"].ToString(), drExercise["iAnswerMode"].ToString());
        }
    }

    /// <summary>
    /// 接收推播訊息事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void timGetMessage_Tick(object sender, EventArgs e)
    {
        //取得訊息資料
        clsHintsDB myDb = new clsHintsDB();
        string strSQL = "SELECT * FROM PushMessage_MessageMember WHERE cUserID = '" + ORCSSession.UserID + "'";
        DataTable dtMessageMember = new DataTable();
        dtMessageMember = myDb.getDataSet(strSQL).Tables[0];
        if (dtMessageMember.Rows.Count > 0)
        {
            foreach (DataRow drMessageMember in dtMessageMember.Rows)
            {
                //3秒內有新訊息才出現訊息視窗
                if (clsTimeConvert.SetDateTime(drMessageMember["cCreateTime"].ToString()).AddSeconds(3) > DateTime.Now)
                {
                    string strCaseID = drMessageMember["cCaseID"].ToString();  //CaseID
                    string strPaperID = drMessageMember["cPaperID"].ToString(); //PaperID
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "newpage", "window.open('/HINTS/Learning/Exercise/Paper_DisplayForORCS.aspx?cPushMessagePage=PushMessagePage&cCaseID=" + strCaseID + "&cPaperID=" + strPaperID + "','MessageOpen', 'fullscreen=yes, scrollbars=yes');", true);
                }
            }
        }
        dtMessageMember.Dispose();
    }
}
