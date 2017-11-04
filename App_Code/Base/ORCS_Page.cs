using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ORCS.Util;
using ORCS.DB;
using ORCS.DB.User;

namespace ORCS
{
    /// <summary>
    /// Summary description for ORCS_Page
    /// </summary>
    public class ORCS_Page : System.Web.UI.Page
    {
        protected ORCSSessionManager ORCSSession;

        public ORCS_Page()
        {

        }

        protected virtual void ORCS_Init()
        {
            // set sesstion
            ORCSSession = new ORCSSessionManager(this);

            //取得使用者ID(由MLAS活動導向過來需取得參數)
            if (Request.QueryString["MLASSystem"] == "Y")
            {
                string strUserID = "afeng";//Request.QueryString["UserID"].ToString();
                ORCSSession.UserID = strUserID;
                ORCSSession.IsLogin = true;
                DataTable UserInfo = clsORCSUser.ORCS_User_SELECT_by_UserID(strUserID);
                ORCSSession.Authority = UserInfo.Rows[0]["cAuthority"].ToString();
            }

            //標題名稱
            ((Label)Master.FindControl("lTitle")).Text = Page.Title;

            //若不為教師身分則將工具列的資料探勘(歷史紀錄)和群組管理隱藏
            if (ORCSSession.Authority != "t")
            {
                ((HyperLink)Master.FindControl("hlDataMining")).Visible = false;
                ((Label)Master.FindControl("lbDataMining")).Visible = false;
                ((HyperLink)Master.FindControl("hlEditGroup")).Visible = false;
                ((Label)Master.FindControl("lbEditGroup")).Visible = false;
            }

            //檢查是否已登入，若未登入則直接關閉頁面
            if (!ORCSSession.IsLogin)
            {
                Response.Redirect("<script>window.close();</script>");
            }

            /* 新版不使用以下驗證
            //檢查系統為上課(1)或下課(0),若為下課則將學生的Session清空
            DataTable dtSystemControl;
            dtSystemControl = clsSystemControl.ORCS_SystemControl_SELECT("SystemControl");
            string strSystemState = dtSystemControl.Rows[0]["iSysControlParam"].ToString();
            if (strSystemState == "0" && ORCSSession.Authority == "s")
                ORCSSession.ClearSession();
            */
        }
    }
}