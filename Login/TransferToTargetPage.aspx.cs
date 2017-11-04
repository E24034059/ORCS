using ORCS.DB.Administrator;
using ORCS.DB.User;
using ORCS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_TransferToTargetPage : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Login();
    }

    //由HINTS登入事件
    protected void Login()
    {
        string HINTSUserID = "";       //使用者帳號
        string strTagetPage = "";  //轉移目標網址
        //使用者帳號
        if (Request.QueryString["UserID"] != null)
            HINTSUserID = Request.QueryString["UserID"];
        //轉移目標網址
        if (Request.QueryString["TargetPage"] != null)
            strTagetPage = Request.QueryString["TargetPage"];
        DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        //登入條件檢查
        DataTable dtUserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(HINTSUserID);
        ORCSSession = new ORCSSessionManager(this);
        ORCSSession.ResetSession();
        ORCSSession.IsLogin = true;
        ORCSSession.UserID = dtUserInfo.Rows[0]["cUserID"].ToString();
        ORCSSession.Authority = dtUserInfo.Rows[0]["cAuthority"].ToString();
        ORCSSession.TempID = clsEditGroupMember.GetGroupIDByUserIDClassID(ORCSSession.UserID, ORCSSession.GroupID);
        Response.Redirect(GeTagetPageURL(strTagetPage));
    }

    private string GeTagetPageURL(string strTagetPage)
    {
        switch(strTagetPage)
        {
            //編輯單位群組功能頁面
            case "EditGroup":
                return "../Administrator/EditGroup.aspx?TransferFrom=Hints_HelpMenu";
            case "EditMemberGroup":
                return "../Administrator/EditGroupMember.aspx?TransferFrom=Hints_HelpMenu";
        }

        return "";
    }
}