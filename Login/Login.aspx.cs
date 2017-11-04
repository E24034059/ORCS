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
using System.Net.NetworkInformation;

using ORCS.Util;
using ORCS.Base;
using ORCS.DB;
using ORCS.DB.Administrator;
using ORCS.DB.Attendance;
using ORCS.DB.User;

public partial class Login_Login : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //登入畫面初始設定
        ((Label)Master.FindControl("lTitle")).Text = Page.Title;
        ((Control)Master.FindControl("spanToolbar")).Visible = false;
        lErrorMsg.Text = "";
        //由MLAS登入
        if (Request.QueryString["MLASLogin"] != null)
            MLASLogin(); // 由MLAS登入事件
        //由HINTS登入
        if (Request.QueryString["Link"] != null)
            HINTSLogin(); // 由HINTS登入事件
        //由HINTS的Datamining進入ORCS系統
        if (Request.QueryString["HINTSDataminingLink"] != null)
            HINTSDataminingLink();// 由HINTS的Datamining進入ORCS系統事件
    }
    //頁面載入前初始化Session
    protected override void OnPreInit(EventArgs e)
    {
        ORCSSession = new ORCSSessionManager(this);
        ORCSSession.IsLogin = false;
        ORCSSession.ProcessPage = "ORCS_Login";

        base.OnPreInit(e);
         
    }
    //回傳系統狀態(下課:0或上課:1或遲到:2)
    protected string CheckSystemState()
    {
        //找出自己所屬的課程
        DataTable dtGroupMember = clsEditGroupMember.ORCS_GroupMember_SELECT_by_UserID_Classify(ORCSSession.UserID, clsGroupNode.GroupClassification_ClassGroup);
        //若不屬於任何課程則回傳0
        if (dtGroupMember.Rows.Count <= 0)
            return "0";
        else
        {
            //找出所屬課程系統狀態
            DataTable dtSystemControl = new DataTable();
            foreach (DataRow drGroupMember in dtGroupMember.Rows)
            {
                dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", drGroupMember["iGroupID"].ToString());
                //若系統沒有SystemControl控制項則新增此控制項
                if (dtSystemControl.Rows.Count <= 0)
                {
                    clsSystemControl.ORCS_SystemControl_INSERT_SysName_GroupID("SystemControl", drGroupMember["iGroupID"].ToString());
                    dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID("SystemControl", drGroupMember["iGroupID"].ToString());
                }
                if (dtSystemControl.Rows[0]["iSysControlParam"].ToString() != "0")
                {
                    //將目前上課的課程GroupID存入Session裡
                    ORCSSession.GroupID = drGroupMember["iGroupID"].ToString();
                    return dtSystemControl.Rows[0]["iSysControlParam"].ToString();
                }
            }
            return "0";
        }
    }
    //登入按鈕事件
    protected void btLogin_Click(object sender, EventArgs e)
    {
        //判斷帳密是否輸入正確
        //登入成功
        if (clsORCSUser.ORCS_User_CHECK_Login(tbUserID.Text, tbPasswd.Text))
        {
            DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(tbUserID.Text);
            ORCSSession = new ORCSSessionManager(this);
            ORCSSession.ResetSession();
            ORCSSession.IsLogin = true;
            ORCSSession.UserID = dtUserInfo.Rows[0]["cUserID"].ToString();
            ORCSSession.Authority = dtUserInfo.Rows[0]["cAuthority"].ToString();
            //檢查系統狀態，若未上課並且身分是學生則無法進入系統首頁
            if (CheckSystemState() == "0" && ORCSSession.Authority == "s")//CheckSystemState() == "0" 未上課
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('尚未開始上課，無法登入系統')", true);
                ORCSSession.ClearSession();
            }
            else
            {
                //檢查IP位址
                if (bCheckClientIP() == false && ORCSSession.Authority == "s")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('無法從上課地點以外的地方登入')", true);
                    ORCSSession.ClearSession();
                }
                //成大教室虛擬IP都相同暫時註解
                else if (bUserClientIP() == false)//檢查IP是否為自己的IP
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('此電腦已有其他使用者登入，無法再登入')", true);
                    ORCSSession.ClearSession();
                }
                //
                else
                {
                    //記錄學生小組ID
                    ORCSSession.TempID = clsEditGroupMember.GetGroupIDByUserIDClassID(ORCSSession.UserID, ORCSSession.GroupID);
                    //學生登入成功更新出席狀態(教師不會更新，因為教師沒出現在出席狀態資料表)
                    //個人
                    UpdateStudentAttendance(ORCSSession.UserID, "0");
                    //小組
                    UpdateStudentAttendance(ORCSSession.TempID, "1");
                    //登入成功後進入ORCS首頁
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../Homepage/Homepage.aspx'", true);
                }
            }
        }
        //登入失敗
        else
            lErrorMsg.Text = "登入失敗，請檢查你的帳號與密碼";
    }
    //檢查登入的網域是否為內部網路
    protected Boolean bCheckClientIP()
    {
        //因為電腦都為虛擬IP 且IP相同 暫時先不檢查
        return true;

        string strClientIP = "";   // 使用者IP
        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) // 有代理伺服器IP
            strClientIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        else // 無代理伺服器IP
            strClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        /*//取得子網路遮罩(目前只能取得伺服器而已)
        foreach (NetworkInterface nifAddr in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nifAddr.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                string strSubNetMask = nifAddr.GetIPProperties().UnicastAddresses[3].IPv4Mask.ToString();
        }*/
        //取得IP前兩個位址(由於Debug是Localhost，所以IP索引會超過0出現錯誤，所以要加try&catch)
        try
        {
            //IP與子網路遮罩作AND
            strClientIP = strClientIP.Split('.')[0].ToString() + "." + strClientIP.Split('.')[1].ToString();
            // 檢查網域
            if (strClientIP == "140.116")
                return true;
            else
                return false;
        }
        catch 
        { 
            return false;
        }
        
    }
    //檢查IP是否為自己的IP
    protected Boolean bUserClientIP()
    {
        //因為電腦都為虛擬IP 且IP相同 暫時先不檢查
        return true;

        DataTable dtAttendance = clsAttendance.ORCS_StudentAttendance_SELECT_by_IPAddress(Request.ServerVariables["REMOTE_ADDR"].ToString());
        //檢查IP是否存在，若存在在檢查是否為當事人的IP，回傳true表示可使用
        if (dtAttendance.Rows.Count > 0 && ORCSSession.Authority == "s")
            if (dtAttendance.Rows[0]["cUserID"].ToString() == ORCSSession.UserID)
                return true;
            else
                return false;//IP已存在並且不是本人的則回傳false
        else
            return true;
    }
    //由HINTS登入事件
    protected void HINTSLogin()
    {
        string HINTSUserID = "";       //使用者帳號
        string HINTSUserPassword = ""; //使用者密碼
        string HINTSUserName = "";     //使用者姓名
        string HINTSUserAuthority = "";//使用者身分
        //使用者帳號
        if (Request.QueryString["ID"] != null)
            HINTSUserID = Request.QueryString["ID"];
        //使用者密碼
        if (Request.QueryString["Password"] != null)
            HINTSUserPassword = Request.QueryString["Password"];
        //使用者姓名
        if (Request.QueryString["Name"] != null)
            HINTSUserName = Request.QueryString["Name"];
        //使用者身分
        if (Request.QueryString["Authority"] != null)
            HINTSUserAuthority = Request.QueryString["Authority"];
        DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        //若無此使用者則新增
        if (dtUser.Rows.Count <= 0)
        {
            clsORCSUser.ORCS_User_INSERT(HINTSUserID, HINTSUserPassword, HINTSUserName, HINTSUserAuthority);
        }
        else
        {
            if (dtUser.Rows[0]["cPassword"].ToString() != HINTSUserPassword)
                clsORCSUser.ORCS_User_UPDATE_UserID_Password(HINTSUserID, HINTSUserPassword);
        }
        //登入條件檢查
        DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        ORCSSession = new ORCSSessionManager(this);
        ORCSSession.ResetSession();
        ORCSSession.IsLogin = true;
        ORCSSession.UserID = dtUserInfo.Rows[0]["cUserID"].ToString();
        ORCSSession.Authority = dtUserInfo.Rows[0]["cAuthority"].ToString();
        //檢查系統狀態，若未上課並且身分是學生則無法進入系統首頁
        if (CheckSystemState() == "0" && ORCSSession.Authority == "s")
        {
            ORCSSession.ClearSession();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('尚未開始上課，無法登入系統');window.close();", true);
        }
        else
        {
            //檢查IP位址
            if (bCheckClientIP() == false && ORCSSession.Authority == "s")
            {
                ORCSSession.ClearSession();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('無法從上課地點以外的地方登入');window.close();", true);
            }
            else if (bUserClientIP() == false)//檢查IP是否為自己的IP
            {
                ORCSSession.ClearSession();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('此電腦已有其他使用者登入，無法再登入');window.close();", true);
            }
            else
            {
                //記錄學生小組ID
                ORCSSession.TempID = clsEditGroupMember.GetGroupIDByUserIDClassID(ORCSSession.UserID, ORCSSession.GroupID);
                //學生登入成功更新出席狀態(教師不會更新，因為教師沒出現在出席狀態資料表)
                //個人
                UpdateStudentAttendance(ORCSSession.UserID, "0");
                //小組
                UpdateStudentAttendance(ORCSSession.TempID, "1");
                //登入成功後進入ORCS首頁
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../Homepage/Homepage.aspx'", true);
            }
        }
    }
    //由HINTS的Datamining進入ORCS系統事件
    protected void HINTSDataminingLink()
    {
        string HINTSUserID = "";       //使用者帳號
        string HINTSUserPassword = ""; //使用者密碼
        string HINTSUserName = "";     //使用者姓名
        string HINTSUserAuthority = "";//使用者身分
        string ddlClass = "";          //使用者要觀看的Class
        string DMType = "";            //從HINTS過來的Datamining Type
        //使用者帳號
        if (Request.QueryString["ID"] != null)
            HINTSUserID = Request.QueryString["ID"];
        //使用者密碼
        if (Request.QueryString["Password"] != null)
            HINTSUserPassword = Request.QueryString["Password"];
        //使用者姓名
        if (Request.QueryString["Name"] != null)
            HINTSUserName = Request.QueryString["Name"];
        //使用者身分
        if (Request.QueryString["Authority"] != null)
            HINTSUserAuthority = Request.QueryString["Authority"];
        //使用者要觀看的Class
        if (Request.QueryString["ddlClass"] != null)
            ddlClass = Request.QueryString["ddlClass"];
        //從HINTS過來的Datamining Type
        if (Request.QueryString["DMType"] != null)
            DMType = Request.QueryString["DMType"];
        //取得使用者資料
        DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        //若無此使用者則新增
        if (dtUser.Rows.Count <= 0)
        {
            clsORCSUser.ORCS_User_INSERT(HINTSUserID, HINTSUserPassword, HINTSUserName, HINTSUserAuthority);
        }
        else
        {
            if (dtUser.Rows[0]["cPassword"].ToString() != HINTSUserPassword)
                clsORCSUser.ORCS_User_UPDATE_UserID_Password(HINTSUserID, HINTSUserPassword);
        }
        //登入條件檢查
        DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        ORCSSession = new ORCSSessionManager(this);
        ORCSSession.ResetSession();
        ORCSSession.IsLogin = true;
        ORCSSession.UserID = dtUserInfo.Rows[0]["cUserID"].ToString();
        ORCSSession.Authority = dtUserInfo.Rows[0]["cAuthority"].ToString();
        //記錄學生小組ID
        ORCSSession.TempID = clsEditGroupMember.GetGroupIDByUserIDClassID(ORCSSession.UserID, ORCSSession.GroupID);
        //登入成功後進入ORCS的Datamining
        switch (DMType)
        {
            case "IndividualAtt"://個人出席狀況
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../DataMining/PersonalAttendance.aspx'", true);
                break;
            case "ClassAtt"://全班出席狀況
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../DataMining/GroupAttendance.aspx?ddlClass=" + ddlClass + "'", true);
                break;
            case "IndividualEx"://個人課堂練習狀況
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../DataMining/PersonalExercise.aspx'", true);
                break;
            case "GroupEx"://小組課堂練習狀況
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../DataMining/TeamExercise.aspx'", true);
                break;
            case "ClassEx"://全班課堂練習狀況
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../DataMining/GroupExercise.aspx?ddlClass=" + ddlClass + "'", true);
                break;
        }
    }

    //由HINTS的Datamining進入ORCS系統事件
    protected void MLASLink()
    {
        string HINTSUserID = "";       //使用者帳號
        string HINTSUserPassword = ""; //使用者密碼
        string HINTSUserName = "";     //使用者姓名
        string HINTSUserAuthority = "";//使用者身分
        //使用者帳號
        if (Request.QueryString["ID"] != null)
            HINTSUserID = Request.QueryString["ID"];
        //使用者密碼
        if (Request.QueryString["Password"] != null)
            HINTSUserPassword = Request.QueryString["Password"];
        //使用者姓名
        if (Request.QueryString["Name"] != null)
            HINTSUserName = Request.QueryString["Name"];
        //使用者身分
        if (Request.QueryString["Authority"] != null)
            HINTSUserAuthority = Request.QueryString["Authority"];
        DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        //若無此使用者則新增
        if (dtUser.Rows.Count <= 0)
        {
            clsORCSUser.ORCS_User_INSERT(HINTSUserID, HINTSUserPassword, HINTSUserName, HINTSUserAuthority);
        }
        else
        {
            if (dtUser.Rows[0]["cPassword"].ToString() != HINTSUserPassword)
                clsORCSUser.ORCS_User_UPDATE_UserID_Password(HINTSUserID, HINTSUserPassword);
        }
        //登入條件檢查
        DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        ORCSSession = new ORCSSessionManager(this);
        ORCSSession.ResetSession();
        ORCSSession.IsLogin = true;
        ORCSSession.UserID = dtUserInfo.Rows[0]["cUserID"].ToString();
        ORCSSession.Authority = dtUserInfo.Rows[0]["cAuthority"].ToString();
        //檢查系統狀態，若未上課並且身分是學生則無法進入系統首頁
        if (CheckSystemState() == "0" && ORCSSession.Authority == "s")
        {
            ORCSSession.ClearSession();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('尚未開始上課，無法登入系統');window.close();", true);
        }
        else
        {
            //檢查IP位址
            if (bCheckClientIP() == false && ORCSSession.Authority == "s")
            {
                ORCSSession.ClearSession();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('無法從上課地點以外的地方登入');window.close();", true);
            }
            else if (bUserClientIP() == false)//檢查IP是否為自己的IP
            {
                ORCSSession.ClearSession();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('此電腦已有其他使用者登入，無法再登入');window.close();", true);
            }
            else
            {
                //學生登入成功更新出席狀態(教師不會更新，因為教師沒出現在出席狀態資料表)
                //個人
                UpdateStudentAttendance(ORCSSession.UserID, "0");
                //小組
                UpdateStudentAttendance(ORCSSession.TempID, "1");
                //登入成功後進入ORCS首頁
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../Homepage/Homepage.aspx'", true);
            }
        }
    }
            //回傳系統狀態(下課:0或上課:1或遲到:2)
    protected void MLASLogin()
    {
        string MLASUserID = "";       //使用者帳號
        string MLASUserPassword = ""; //使用者密碼
        string MLASUserName = "";     //使用者姓名
        string MLASUserAuthority = "";//使用者身分
        //使用者帳號
        if (Request.QueryString["UserID"] != null)
            MLASUserID = Request.QueryString["UserID"];
        //使用者密碼
        if (Request.QueryString["Password"] != null)
            MLASUserPassword = Request.QueryString["Password"];
        //使用者姓名
        if (Request.QueryString["Name"] != null)
            MLASUserName = Request.QueryString["Name"];
        //使用者身分
        if (Request.QueryString["Authority"] != null)
            MLASUserAuthority = Request.QueryString["Authority"];
        DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(MLASUserID);
        //若無此使用者則新增
        if (dtUser.Rows.Count <= 0)
        {
            clsORCSUser.ORCS_User_INSERT(MLASUserID, MLASUserPassword, MLASUserName, MLASUserAuthority);
        }
        else
        {
            if (dtUser.Rows[0]["cPassword"].ToString() != MLASUserPassword)
                clsORCSUser.ORCS_User_UPDATE_UserID_Password(MLASUserID, MLASUserPassword);
        }
        //登入條件檢查
        DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(MLASUserID);
        ORCSSession = new ORCSSessionManager(this);
        ORCSSession.ResetSession();
        ORCSSession.IsLogin = true;
        ORCSSession.UserID = dtUserInfo.Rows[0]["cUserID"].ToString();
        ORCSSession.Authority = dtUserInfo.Rows[0]["cAuthority"].ToString();
        //檢查系統狀態，若未上課並且身分是學生則無法進入系統首頁
        if (CheckSystemState() == "0" && ORCSSession.Authority == "s")
        {
            ORCSSession.ClearSession();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('尚未開始上課，無法登入系統');window.close();", true);
        }
        else
        {
            //檢查IP位址
            if (bCheckClientIP() == false && ORCSSession.Authority == "s")
            {
                ORCSSession.ClearSession();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('無法從上課地點以外的地方登入');window.close();", true);
            }
            else if (bUserClientIP() == false)//檢查IP是否為自己的IP
            {
                ORCSSession.ClearSession();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('此電腦已有其他使用者登入，無法再登入');window.close();", true);
            }
            else
            {
                //記錄學生小組ID
                ORCSSession.TempID = clsEditGroupMember.GetGroupIDByUserIDClassID(ORCSSession.UserID, ORCSSession.GroupID);
                //學生登入成功更新出席狀態(教師不會更新，因為教師沒出現在出席狀態資料表)
                //個人
                UpdateStudentAttendance(ORCSSession.UserID, "0");
                //小組
                UpdateStudentAttendance(ORCSSession.TempID, "1");
                //登入成功後進入ORCS首頁
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "location.href='../Homepage/Homepage.aspx'", true);
            }
        }
    }

    /// <summary>
    /// 學生登入成功更新出席狀態(教師不會更新，因為教師沒出現在出席狀態資料表)(個人/小組)
    /// </summary>
    /// <param name="strUserID">個人ID/小組ID</param>
    /// <param name="strGroupMode">個人(0)/小組(1)</param>
    private void UpdateStudentAttendance(string strUserID ,string strGroupMode)
    {
        DataTable dtAttendance = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_iGroupMode(strUserID, strGroupMode);
        if (dtAttendance.Rows.Count > 0) // 判斷學生只能更新一次登入時間和狀態(個人/小組)
        {
            if (dtAttendance.Rows[0]["iAttendanceCond"].ToString() == "0" && CheckSystemState() == "1") //準時
            {
                //更新學生出席記錄(個人/小組)
                clsAttendance.ORCS_StudentAttendance_UPDATE(strUserID, "1", System.DateTime.Now.ToString("yyyyMMddHHmmss"), Request.ServerVariables["REMOTE_ADDR"].ToString(), ORCSSession.GroupID, strGroupMode);
                //記錄學生登入記錄(個人/小組)
                clsAttendanceRecord.addStudentAttendanceRecord(ORCSSession.GroupID, strUserID, System.DateTime.Now.ToString("yyyyMMddHHmmss"), "1", strGroupMode);
            }
            else if (dtAttendance.Rows[0]["iAttendanceCond"].ToString() == "0" && CheckSystemState() == "2") //遲到
            {
                //更新學生出席記錄(個人/小組)
                clsAttendance.ORCS_StudentAttendance_UPDATE(strUserID, "2", System.DateTime.Now.ToString("yyyyMMddHHmmss"), Request.ServerVariables["REMOTE_ADDR"].ToString(), ORCSSession.GroupID, strGroupMode);
                //記錄學生登入記錄(個人/小組)
                clsAttendanceRecord.addStudentAttendanceRecord(ORCSSession.GroupID, strUserID, System.DateTime.Now.ToString("yyyyMMddHHmmss"), "1", strGroupMode);
                if (strGroupMode == "0")//個人才需提醒遲到
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('遲到" + clsTimeConvert.TotalLateTime(ORCSSession.UserID, ORCSSession.GroupID) + "分鐘囉!下次請早點來上課，謝謝');location.href='../Homepage/Homepage.aspx';", true);
                }
            }
        }

    }
}