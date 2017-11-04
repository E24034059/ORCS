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

using ORCS.Base;
using ORCS.DB;
using ORCS.DB.Attendance;
using ORCS.DB.User;
using ORCS.Util;
using System.Drawing;
using ORCS.DB.Administrator;

public partial class Attendance_Attendance : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //頁面初始化
        base.ORCS_Init();
        if (!IsPostBack)
        {
            //載入出缺席名單
            LoadAttendanceList();
            //檢查重新點名按鈕
            CheckReRollCallBtn();
        }
    }
    //Timer更新出缺席狀況事件
    protected void timAttendanceCond_Tick(object sender, EventArgs e)
    {
        //檢查重新點名後學生出席狀態
        CheckReRollCallStudentAtt();
        //載入出缺席名單
        LoadAttendanceList();
        //檢查重新點名按鈕
        CheckReRollCallBtn();
    }
    //載入出缺席名單
    protected void LoadAttendanceList()
    {
        //取得學生出席名單
        DataTable dtStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID("%", ORCSSession.GroupID);
        if (dtStudentAtt.Rows.Count > 0)
        {
            int iAllStudent = 0;     //應到總數
            int iActualStudent = 0;  //實到人數
            int iLateStudent = 0;    //遲到人數
            int iAbsenceStudent = 0; //未到人數
            int iLeaveEarlyStudent = 0; //早退人數
            //定義TableRow
            HtmlTableRow tbrAttList = new HtmlTableRow();
            foreach (DataRow drStudentAtt in dtStudentAtt.Rows)
            {
                if (drStudentAtt["iGroupMode"].ToString() == "0")//只需顯示個人
                {
                    //一列10個學生
                    if ((iAllStudent % 10) == 0)
                        tbrAttList = new HtmlTableRow();
                    //定義TableCell
                    HtmlTableCell tbcAttList = new HtmlTableCell();
                    tbcAttList.Attributes.Add("Style", "border: 2px solid black");
                    tbcAttList.Align = "center";
                    //定義學生姓名Label
                    Label lbAttStudName = new Label();
                    lbAttStudName.Text = clsORCSUser.ORCS_User_SELECT_by_UserID(drStudentAtt["cUserID"].ToString()).Rows[0]["cUserName"].ToString() + "<br />";
                    //定義學生出席狀況Label
                    Label lbAttStudState = new Label();
                    if (drStudentAtt["iAttendanceCond"].ToString() == "0") // 未出席學生
                    {
                        lbAttStudState.ForeColor = Color.Red;
                        lbAttStudState.Text = "<img src='../App_Themes/general/images/offline.png' width='45px' />";
                        iAbsenceStudent++; // 累加未到人數

                    }
                    else if (drStudentAtt["iAttendanceCond"].ToString() == "1") // 已出席學生(準時)
                    {
                        lbAttStudState.ForeColor = Color.Green;
                        lbAttStudState.Text = "<img src='../App_Themes/general/images/online.png' width='45px' />";
                        iActualStudent++; // 累加實到人數
                    }
                    else if (drStudentAtt["iAttendanceCond"].ToString() == "2")// 已出席學生(遲到)
                    {
                        lbAttStudState.ForeColor = Color.Purple;
                        lbAttStudState.Text = "<img src='../App_Themes/general/images/late.png' width='30px' /><br />" + clsTimeConvert.TotalLateTime(drStudentAtt["cUserID"].ToString(), drStudentAtt["iClassGroupID"].ToString()) + "分鐘";
                        iActualStudent++; // 累加實到人數
                        iLateStudent++; // 累加遲到人數
                    }
                    else if (drStudentAtt["iAttendanceCond"].ToString() == "3")// 未出席學生(早退)
                    {
                        lbAttStudState.ForeColor = Color.Purple;
                        lbAttStudState.Text = "<img src='../App_Themes/general/images/leaveEarly.png' width='45px' />";
                        iLeaveEarlyStudent++; // 累加早退人數
                    }

                    //放入Table
                    tbcAttList.Controls.Add(lbAttStudName);
                    tbcAttList.Controls.Add(lbAttStudState);
                    tbrAttList.Cells.Add(tbcAttList);
                    tbAttDetialCond.Rows.Add(tbrAttList);

                    //累加應到人數
                    iAllStudent++;
                }
            }
            //顯示應到、已到、未到、早退人數
            lbExceptAtt.Text = "&nbsp;&nbsp;&nbsp;應到人數: " + iAllStudent + " 人";
            lbActualAtt.Text = "<img src='../App_Themes/general/images/online.png' width='25px' />實到人數: " + iActualStudent + " 人";
            lbLateAtt.Text = "<img src='../App_Themes/general/images/late.png' width='25px' />遲到人數: " + iLateStudent + " 人";
            lbAbsenceAtt.Text = "<img src='../App_Themes/general/images/offline.png' width='25px' />未到人數: " + iAbsenceStudent + " 人";
            lbLeaveEarlyAtt.Text = "<img src='../App_Themes/general/images/leaveEarly.png' width='25px' />早退人數: " + iLeaveEarlyStudent + " 人";
        }
    }
    //「重新點名」按鈕是否顯示出來
    protected void CheckReRollCallBtn()
    {
        //若為學生，則將重新"點名"改為重新"簽到"
        if (ORCSSession.Authority == "s")
            btnReRollCall.Text = "重新簽到";
        //取得學生出席名單
        DataTable dtStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID("%");
        if (dtStudentAtt.Rows.Count <= 0)
        {
            btnReRollCall.Visible = false;
        }
        else
        {
            //若學生已經點完名則將「重新點名」按鈕隱藏
            dtStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID(ORCSSession.UserID);
            if (dtStudentAtt.Rows.Count > 0)
            {
                if (dtStudentAtt.Rows[0]["iAttendanceCond"].ToString() == "0")
                    btnReRollCall.Visible = true;
                else
                    btnReRollCall.Visible = false;
            }
        }
    }
    //重新點名控制事項
    protected void ReRollCall()
    {
        switch (ORCSSession.Authority)
        {
            case "t": // 教師事件
                //將學生出席狀態改為未到(個人)
                clsAttendance.ORCS_StudentAttendance_UPDATE("%", 0, "", "", ORCSSession.GroupID,"0");
                //將學生出席狀態改為未到(小組)
                clsAttendance.ORCS_StudentAttendance_UPDATE("%", 0, "", "", ORCSSession.GroupID,"1");
                //將系統狀態改為1
                clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID("SystemControl", "1", ORCSSession.GroupID);
                //將遲到時間延後12分鐘
                clsTimeControl.ORCS_TimeControl_UPDATE_TimeName_Time_GroupID("LateTime", DateTime.Now.AddMinutes(12).ToString("yyyyMMddHHmmss"), ORCSSession.GroupID);
                break;
            case "s": // 學生事件
                //取得系統狀態
                DataTable dtORCS_SystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", ORCSSession.GroupID);
                //strSystemState:0:未上課 1:學生準時 2:學生遲到
                string strSystemState = dtORCS_SystemControl.Rows[0]["iSysControlParam"].ToString();
                if (strSystemState == "1") // 準時
                {
                    //更新學生出席狀況(個人)
                    clsAttendance.ORCS_StudentAttendance_UPDATE(ORCSSession.UserID, "1", DateTime.Now.ToString("yyyyMMddHHmmss"), Request.ServerVariables["REMOTE_ADDR"].ToString(), ORCSSession.GroupID,"0");
                    //記錄上課出席狀況(個人)
                    clsAttendanceRecord.addStudentAttendanceRecord(ORCSSession.GroupID, ORCSSession.UserID, DateTime.Now.ToString("yyyyMMddHHmmss"), "1","0");
                    //更新學生出席狀況(小組)
                    clsAttendance.ORCS_StudentAttendance_UPDATE(ORCSSession.TempID, "1", DateTime.Now.ToString("yyyyMMddHHmmss"), Request.ServerVariables["REMOTE_ADDR"].ToString(), ORCSSession.GroupID, "1");
                    //記錄上課出席狀況(小組)
                    clsAttendanceRecord.addStudentAttendanceRecord(ORCSSession.GroupID, ORCSSession.TempID, DateTime.Now.ToString("yyyyMMddHHmmss"), "1", "1");
                }
                break;
        }
    }
    //「重新點名」事件
    protected void btnReRollCall_Click(object sender, EventArgs e)
    {
        //重新點名控制事項
        ReRollCall();
        //載入出席名單
        LoadAttendanceList();
        //檢查重新點名按鈕
        CheckReRollCallBtn();
        //記錄重新點名時間
        clsClassTimeRecord.updateReRollCallTime(ORCSSession.GroupID);
    }

    //檢查重新點名後學生出席狀態
    protected void CheckReRollCallStudentAtt()
    {
        if (ORCSSession.GroupID == null)
        {
            return;
        }
        //取得該堂課點名資訊
        DataTable dtClassTimeRecord = clsClassTimeRecord.getLastClassTimeRecord(ORCSSession.GroupID);
        //判斷是否正在重新點名中
        if (dtClassTimeRecord.Rows[0]["bIsReRollCall"].ToString().Equals("True"))
        {
            //取得最後點名時間
            DateTime reRollCallTime = clsTimeConvert.DBTimeToDateTime(dtClassTimeRecord.Rows[0]["cReRollCallTime"].ToString());
            //判斷是否再重新點名時間外如果是就要檢查學生是否早退
            if (DateTime.Compare(reRollCallTime.AddMinutes(12),DateTime.Now)<0)
            {
                //取得學生出席名單
                DataTable dtStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID("%", ORCSSession.GroupID);
                //檢查全部學生出席狀況
                foreach (DataRow drStudentAtt in dtStudentAtt.Rows)
                {
                    //如果非準時上課 要檢查學生之前在該堂課的出席記錄
                    //如果有出席記錄就記錄該學生出席狀態為早退
                    //如果沒有記錄則為未出席,不需改變狀態
                    if (drStudentAtt["iAttendanceCond"].ToString() != "1")
                    {
                        if (clsAttendanceRecord.hasAttendanceRecordInClass(ORCSSession.GroupID, drStudentAtt["cUserID"].ToString(), dtClassTimeRecord.Rows[0]["cStartTime"].ToString()))
                        {
                            clsAttendance.ORCS_StudentAttendance_WithoutIPAddress_UPDATE(drStudentAtt["cUserID"].ToString(), "3", DateTime.Now.ToString("yyyyMMddHHmmss"),drStudentAtt["iClassGroupID"].ToString());
                        }
                    }
                }
                //檢查完畢後將重新點名狀態改為False
                clsClassTimeRecord.updateReRollCallTimeState(ORCSSession.GroupID,"False");
            }
        }
    }
}