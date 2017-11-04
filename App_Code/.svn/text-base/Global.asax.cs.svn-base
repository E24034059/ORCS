using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Timers;

using ORCS.Base;
using ORCS.DB;
using ORCS.DB.Administrator;
using ORCS.DB.Attendance;
using ORCS.DB.Exercise;
using ORCS.DB.User;
using Microsoft.Owin;
using Owin;
using System.Web.Routing;

namespace ORCS
{
    /// <summary>
    /// Summary description for Global
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        System.Timers.Timer timerForLate;
        System.Timers.Timer timerForInitSys;
        System.Timers.Timer timerForOpenClass;

        public Global()
        {
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //定義遲到Timer
            timerForLate = new System.Timers.Timer(30000); //30秒更新一次
            timerForLate.Elapsed += new ElapsedEventHandler(ExeConsole_timerForLate);
            timerForLate.Start();

            //定義系統初始化Timer(若教師忘記按「下課」則系統自動更新)
            timerForInitSys = new System.Timers.Timer(21600000); //6小時更新一次
            timerForInitSys.Elapsed += new ElapsedEventHandler(ExeConsole_timerForInitSys);
            timerForInitSys.Start();

            //定義上課時間Timer
            timerForOpenClass = new System.Timers.Timer(180000); //3分鐘更新一次
            timerForOpenClass.Elapsed += new ElapsedEventHandler(ExeConsole_timerForOpenClass);
            timerForOpenClass.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            timerForLate.Stop();
            timerForInitSys.Stop();
            timerForOpenClass.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
        //遲到Timer事件
        protected void ExeConsole_timerForLate(object sender, System.Timers.ElapsedEventArgs e)
        {
            //若現在時間超過遲到時間則將 SystemControl 改為2(遲到)
            DataTable dtTimeControl = clsTimeControl.ORCS_TimeControl_SELECT("LateTime");
            if (dtTimeControl.Rows.Count > 0)
            {
                foreach (DataRow drTimeControl in dtTimeControl.Rows)
                {
                    if (DateTime.Compare(clsTimeConvert.DBTimeToDateTime(drTimeControl["cTime"].ToString()), DateTime.Now) < 0)
                    {
                        clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID("SystemControl", "2", drTimeControl["iClassGroupID"].ToString());
                    }
                }
            }
        }
        //系統初始化Timer事件
        protected void ExeConsole_timerForInitSys(object sender, System.Timers.ElapsedEventArgs e)
        {
            //若系統在每日23:59:59尚未下課則系統自動下課
            DataTable dtSysCont = clsSystemControl.ORCS_SystemControl_SELECT("SystemControl");
            if (dtSysCont.Rows.Count > 0)
                foreach (DataRow drSysCont in dtSysCont.Rows)
                {
                    if (drSysCont["iSysControlParam"].ToString() != "0")
                    {
                        DataTable dtTimeControl = clsTimeControl.ORCS_TimeControl_SELECT_TimeConName_GroupID("InitialTime", drSysCont["iClassGroupID"].ToString());
                        //若沒有InitialTime則新增
                        if (dtTimeControl.Rows.Count <= 0)
                        {
                            string strAddDateTime = DateTime.Now.ToString("yyyyMMdd") + "235959";
                            clsTimeControl.ORCS_TimeControl_INSERT_TimeName_Time_GroupID("InitialTime", strAddDateTime, drSysCont["iClassGroupID"].ToString());
                            dtTimeControl = clsTimeControl.ORCS_TimeControl_SELECT_TimeConName_GroupID("InitialTime", drSysCont["iClassGroupID"].ToString());
                        }
                        //取得InitialTime
                        DateTime timDateTime = clsTimeConvert.DBTimeToDateTime(dtTimeControl.Rows[0]["cTime"].ToString());
                        if (DateTime.Compare(timDateTime, DateTime.Now) < 0)
                        {
                            //將InitialTime加1天
                            timDateTime = timDateTime.AddDays(1);
                            clsTimeControl.ORCS_TimeControl_UPDATE_TimeName_Time_GroupID("InitialTime", timDateTime.ToString("yyyyMMddHHmmss"), drSysCont["iClassGroupID"].ToString());
                            //儲存學生出席狀況和作答狀況
                            SaveStudentAttendanceAndExerciseCond(drSysCont["iClassGroupID"].ToString(), drSysCont["cStartTime"].ToString());
                            //下課時刪除學生出席名單
                            clsAttendance.ORCS_StudentAttendance_DELETE_by_UserID_GroupID("%", drSysCont["iClassGroupID"].ToString());
                            //下課時刪除學生題目作答名單
                            clsExercise.ORCS_ExerciseCondition_DELETE_by_cExerciseID_GroupID("%ExerciseControl%", drSysCont["iClassGroupID"].ToString());
                            //刪除遲到時間
                            clsTimeControl.ORCS_TimeControl_DELETE_TimeConName_GroupID("LateTime", drSysCont["iClassGroupID"].ToString());
                            //將系統改為下課(0)
                            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID("SystemControl", "0", "", "", drSysCont["iClassGroupID"].ToString());
                            //將作答狀態改為未開放(0)
                            clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID("%ExerciseControl%", "0", "", "", drSysCont["iClassGroupID"].ToString());
                        }
                    }
                    else
                    {
                        DataTable dtTimeControl = clsTimeControl.ORCS_TimeControl_SELECT_TimeConName_GroupID("InitialTime", drSysCont["iClassGroupID"].ToString());
                        //若沒有InitialTime則新增
                        if (dtTimeControl.Rows.Count <= 0)
                        {
                            string strAddDateTime = DateTime.Now.ToString("yyyyMMdd") + "235959";
                            clsTimeControl.ORCS_TimeControl_INSERT_TimeName_Time_GroupID("InitialTime", strAddDateTime, drSysCont["iClassGroupID"].ToString());
                            dtTimeControl = clsTimeControl.ORCS_TimeControl_SELECT_TimeConName_GroupID("InitialTime", drSysCont["iClassGroupID"].ToString());
                        }
                        //取得InitialTime
                        DateTime timDateTime = clsTimeConvert.DBTimeToDateTime(dtTimeControl.Rows[0]["cTime"].ToString());
                        if (DateTime.Compare(timDateTime, DateTime.Now) < 0)
                        {
                            //將InitialTime加1天
                            timDateTime = timDateTime.AddDays(1);
                            clsTimeControl.ORCS_TimeControl_UPDATE_TimeName_Time_GroupID("InitialTime", timDateTime.ToString("yyyyMMddHHmmss"), drSysCont["iClassGroupID"].ToString());
                        }
                    }
                }
        }
        //儲存學生出席狀況和作答狀況
        protected void SaveStudentAttendanceAndExerciseCond(string strClassGroupID, string strStartTime)
        {
            //取得上課時間當ID(Date Time格式:20101023125323，Convert.ToDateTime("2010-10-23 12:53:23"))
            string strDateTime = strStartTime;
            //儲存出席狀態
            DataTable dtAttendance = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID("%", strClassGroupID);
            if (dtAttendance.Rows.Count > 0)
            {
                foreach (DataRow drAttendance in dtAttendance.Rows)
                    clsAttendance.ORCS_StudentAttendanceHistory_INSERT(strDateTime, drAttendance["cUserID"].ToString(), drAttendance["iAttendanceCond"].ToString(), drAttendance["cAttendanceTime"].ToString(), drAttendance["cIPAddress"].ToString(), drAttendance["iClassGroupID"].ToString(), drAttendance["iGroupMode"].ToString());
            }
            //儲存作答狀態
            DataTable dtExercise = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID("%", strClassGroupID);
            if (dtExercise.Rows.Count > 0)
            {
                foreach (DataRow drExercise in dtExercise.Rows)
                    clsExercise.ORCS_ExerciseConditionHistory_INSERT(strDateTime, drExercise["cExerciseID"].ToString(), drExercise["cUserID"].ToString(), drExercise["iExerciseState"].ToString(), drExercise["iClassGroupID"].ToString(), drExercise["cCaseID"].ToString(), drExercise["cPaperID"].ToString(), drExercise["iAnswerMode"].ToString());
            }
        }
        //定義上課時間Timer
        protected void ExeConsole_timerForOpenClass(object sender, System.Timers.ElapsedEventArgs e)
        {
            //取得課程ID
            DataTable dtClassGroup = clsEditGroup.ORCS_ClassGroup_SELECT_by_ClassGroupID("%");
            //判斷課程上下課時間是否應該開啟
            if (dtClassGroup.Rows.Count > 0)
            {
                foreach (DataRow drClassGroup in dtClassGroup.Rows)
                {
                    //取得課程上下課時間
                    DataTable dtClassTimeTable = clsClassTimeTable.ORCS_ClassTimeTable_SELECT_by_ClassGroupID(drClassGroup["iClassGroupID"].ToString());
                    if (dtClassTimeTable.Rows.Count > 0)
                    {
                        foreach (DataRow drClassTimeTable in dtClassTimeTable.Rows)
                        {
                            //取得課程上下課時間
                            DateTime datStartTime = clsTimeConvert.DBTimeToDateTime(drClassTimeTable["cStartTime"].ToString());
                            DateTime datEndtTime = clsTimeConvert.DBTimeToDateTime(drClassTimeTable["cEndTime"].ToString());
                            //取得課程狀態
                            DataTable dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", drClassTimeTable["iClassGroupID"].ToString());
                            if (dtSystemControl.Rows[0]["iSysControlParam"].ToString() == "0" && System.DateTime.Now >= datStartTime && System.DateTime.Now <= datEndtTime)
                            {
                                //預設10分鐘後遲到，將時間加入TimeControl資料表
                                clsTimeControl.ORCS_TimeControl_INSERT_TimeName_Time_GroupID("LateTime", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"), drClassTimeTable["iClassGroupID"].ToString());
                                //將系統改為上課(1)
                                clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID("SystemControl", "1", DateTime.Now.ToString("yyyyMMddHHmmss"), "", drClassTimeTable["iClassGroupID"].ToString());
                                //載入學生出席狀況名單(個人)
                                DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_Authority("s");
                                if (dtUserInfo.Rows.Count > 0)
                                {
                                    foreach (DataRow drUserInfo in dtUserInfo.Rows)
                                    {
                                        //檢查使用者是否屬於上課的班級，若是則存入出席名單裡
                                        DataTable dtGroupMember = clsEditGroupMember.ORCS_GroupMember_SELECT_by_UserID_GroupID_Classify(drUserInfo["cUserID"].ToString(), drClassTimeTable["iClassGroupID"].ToString(), clsGroupNode.GroupClassification_ClassGroup);
                                        //插入ORCS_StudentAttendance資料表，出席狀況預設為0
                                        if (dtGroupMember.Rows.Count > 0)
                                            clsAttendance.ORCS_StudentAttendance_INSERT_UserID_AttCond_GroupID_iGroupMode(drUserInfo["cUserID"].ToString(), "0", drClassTimeTable["iClassGroupID"].ToString(),"0");
                                    }
                                }
                                //載入學生出席狀況名單(小組)
                                DataTable dtGroupInfo = clsEditGroup.ORCS_TempGroup_SELECT_by_iClassGroupID(drClassTimeTable["iClassGroupID"].ToString());
                                if (dtGroupInfo.Rows.Count > 0)
                                {
                                    foreach (DataRow drGroupInfo in dtGroupInfo.Rows)
                                    {
                                        //插入ORCS_StudentAttendance資料表，出席狀況預設為0
                                        clsAttendance.ORCS_StudentAttendance_INSERT_UserID_AttCond_GroupID_iGroupMode(drGroupInfo["iTempGroupID"].ToString(), "0", drClassTimeTable["iClassGroupID"].ToString(), "1");
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}