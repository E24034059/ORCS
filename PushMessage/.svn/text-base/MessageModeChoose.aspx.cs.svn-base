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
using ORCS.DB;
using ORCS;

public partial class PushMessage_MessageModeChoose: ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    
    //檢查ORCS是否為上課狀態 2014/1/21 蕭凱
    protected string CheckClassState()
    {
        
        string strClassID = ""; //定義正在上課的課程ID
        /*
        //抓取使用者的課程ID
        clsORCSDB myDb = new clsORCSDB();
        string strSQL = "SELECT * FROM ORCS_GroupMember WHERE cUserID = '" + ORCSSession.UserID + "' AND cGroupClassify = 'ClassGroup'";
        DataTable dtGroupMember = new DataTable();
        dtGroupMember = myDb.GetDataSet(strSQL).Tables[0];
        if (dtGroupMember.Rows.Count > 0)
        {
            //抓取正在上課的課程ID
            foreach (DataRow drGroupMember in dtGroupMember.Rows)
            {
                strSQL = "SELECT * FROM ORCS_SystemControl WHERE iClassGroupID = '" + drGroupMember["iGroupID"].ToString() + "' AND cSysControlName = 'SystemControl'";
                DataTable dtSystemControl = new DataTable();
                dtSystemControl = myDb.GetDataSet(strSQL).Tables[0];
                if (dtSystemControl.Rows.Count > 0)
                    if (dtSystemControl.Rows[0]["iSysControlParam"].ToString() != "0") //判斷該課程是否上課("0":非上課,"1":上課,"2":上課遲到)
                        strClassID = dtSystemControl.Rows[0]["iClassGroupID"].ToString();
            }
        }
         */
        return strClassID;//回傳課程ID
    }
    //檢查是否有自己的臨時推播訊息群組
    protected bool CheckTempGroup()
    {
        /*
        //抓取臨時推播訊息群組ID
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL = "SELECT * FROM PushMessage_TempGroupMember WHERE cUserID = '" + ORCSSession.UserID + "'";
        DataTable dtTempGroupMember = new DataTable();
        dtTempGroupMember = HintsDB.getDataSet(strSQL).Tables[0];
        if (dtTempGroupMember.Rows.Count > 0)
            return true;
        else*/
            return false;
         
    }
    //訊息模式選擇事件
    protected void rblModeChoose_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rblModeChoose.SelectedValue)
        {
            case "EnterTempMessageGroup": //------------------------進入臨時推播訊息群組
                btnConfirm.CommandName = "cnEnterTempMessageGroup";
                break;
            case "CreateTempMessageGroup": //-----------------------建立臨時推播訊息群組
                btnConfirm.CommandName = "cnCreateTempMessageGroup";
                break;
            case "SendMessage": //----------------------------------傳送推播訊息
                btnConfirm.CommandName = "cnSendMessage";
                break;
            case "ViewMessage": //----------------------------------觀看推播訊息回應
                btnConfirm.CommandName = "cnViewMessage";
                break;

        }
    }
    //「下一步」按鈕事件
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        /*
        switch (btnConfirm.CommandName)
        {
            case "cnEnterTempMessageGroup": //----------------------------進入臨時推播訊息群組
                Page.RegisterClientScriptBlock("WindowOpen", "<script>window.open('SelectTempMsgGroup.aspx','SelectTempMsgGroup', 'width=720, height=640, scrollbars=yes');window.close();</script>");
                break;
            case "cnCreateTempMessageGroup": //---------------------------建立臨時推播訊息群組
                Page.RegisterClientScriptBlock("WindowOpen", "<script>window.open('CreateTempMsgGroup.aspx','CreateTempMessageGroup', 'width=500, height=380, scrollbars=yes');window.close();</script>");
                break;
            case "cnSendMessage": //--------------------------------------傳送推播訊息
                string strClassID = CheckClassState(); //取得課程ID
                bool bCheckTempGroup = CheckTempGroup();//檢查是否有自己的臨時推播訊息群組
                //先檢查是否有上課的課程，在檢查是否有臨時推播訊息群組
                if (strClassID != "" || bCheckTempGroup == true)
                {
                    //產生訊息考卷所需的CaseID、PaperID和考卷名稱，要給HINTS的Paper_CaseDivisionSection資料表用
                    string strCaseID = usi.UserID + "Case" + DateTime.Now.ToString("yyyyMMddHHmmss");   //CaseID
                    string strSectionName = "推播訊息";    //考卷名稱(Paper_CaseDivisionSection資料表欄位名稱為cSectionName)
                    string strPaperID = usi.UserID + "PushMessage" + DateTime.Now.ToString("yyyyMMddHHmmss");   //PaperID
                    //取得前人所寫的資料表函數
                    SQLString mySQL = new SQLString();
                    //將取得的CaseID、PaperID和考卷名稱插入Paper_CaseDivisionSection資料表
                    mySQL.saveToPaper_CaseDivisionSection(strPaperID, strCaseID, "1", strSectionName);
                    //將取得的PaperID和考卷名稱插入Paper_Header資料表
                    mySQL.saveToPaper_Header(strPaperID, strSectionName, strSectionName, "General", "Author", "Edit", "All", "0", "10");
                    //開啟編輯考卷頁面
                    Page.RegisterClientScriptBlock("WindowOpen", "<script>window.open('PushMessageEdit/PushMessage_MainPage.aspx?cCaseID=" + strCaseID + "&cSectionName=" + strSectionName + "&cPaperID=" + strPaperID + "','PushMessage_MainPage', 'fullscreen=yes, resizable=yes, scrollbars=yes');window.close();</script>");
                }
                else
                    Page.RegisterClientScriptBlock("alert", "<script>alert('無傳送的課程或群組')</script>");
                break;
            case "cnViewMessage": //--------------------------------------觀看推播訊息
                Page.RegisterClientScriptBlock("WindowOpen", "<script>window.open('MessageList.aspx','MessageList', 'width=920, height=720, scrollbars=yes, resizable=yes');window.close();</script>");
                break;
        }
         */
    }
     
}