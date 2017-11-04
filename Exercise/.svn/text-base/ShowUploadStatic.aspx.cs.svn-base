using ORCS.DB.Administrator;
using ORCS.DB.Attendance;
using ORCS.DB.Exercise;
using ORCS.DB.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Exercise_ShowUploadStatic : System.Web.UI.Page
{

    string strExerciseCondID = "";  //ExerciseCondID
    string strExerciseID = "";      //ExerciseID
    string strUserID = "";          //UserID
    string strGroupID = "";         //GroupID
    string strUploadMode = "";      //UploadMode
    string strAuthority = "";       //Authority

    protected void Page_Load(object sender, EventArgs e)
    {
        //抓取URL參數
        if (Request.QueryString["cExerciseCondID"] != null)
        {
            strExerciseCondID = Request.QueryString["cExerciseCondID"].ToString();  //ExerciseCondID
        }
        if (Request.QueryString["cExerciseID"] != null)
        {
            strExerciseID = Request.QueryString["cExerciseID"].ToString();          //ExerciseID
        }
        if (Request.QueryString["cUserID"] != null)
        {
            strUserID = Request.QueryString["cUserID"].ToString();                  //UserID
        }
        if (Request.QueryString["cGroupID"] != null)
        {
            strGroupID = Request.QueryString["cGroupID"].ToString();                //GroupID
        }
        if (Request.QueryString["cUploadMode"] != null)
        {
            strUploadMode = Request.QueryString["cUploadMode"].ToString();          //UploadMode
        }
        if (Request.QueryString["cAuthority"] != null)
        {
            strAuthority = Request.QueryString["cAuthority"].ToString();           //Authority
        }

        //定義檔案列表標題
        lbExFileList.Text = "第 " + strExerciseID.Split('_')[1] + " 份作品檔案統計";
        LoadExFileList();
    }

    //載入作品檔案列表
    protected void LoadExFileList()
    {
        //--------------------------------作品檔案列表標題-----------------------------------------
        //定義作品檔案列表標題
        HtmlTableRow tbrTitle = new HtmlTableRow();
        tbrTitle.Attributes.Add("Style", "Font-size: 20px; background-color: lightgray");
        tbrTitle.Align = "center";
        HtmlTableCell tbcCompleteTitle = new HtmlTableCell();
        tbcCompleteTitle.Width = "33%";
        tbcCompleteTitle.InnerText = "已完成上傳";
        tbcCompleteTitle.Attributes.Add("Style", "border: 2px solid; border-color: black");
        HtmlTableCell tbcIncompleteTitle = new HtmlTableCell();
        tbcIncompleteTitle.Width = "33%";
        tbcIncompleteTitle.InnerText = "未完成上傳";
        tbcIncompleteTitle.Attributes.Add("Style", "border: 2px solid; border-color: black");
        HtmlTableCell tbcYetReachedTitle = new HtmlTableCell();
        tbcYetReachedTitle.Width = "33%";
        tbcYetReachedTitle.Attributes.Add("Style", "border: 2px solid; border-color: black");
        tbcYetReachedTitle.InnerText = "未到";

        //加入Table
        tbrTitle.Cells.Add(tbcCompleteTitle);
        tbrTitle.Cells.Add(tbcIncompleteTitle);
        tbrTitle.Cells.Add(tbcYetReachedTitle);
        tbExFileList.Rows.Add(tbrTitle);


        //--------------------------------作品檔案列表內容-----------------------------------------
        //取得作品檔案列表
        DataTable dtExerciseCond = clsExercise.ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID(strExerciseID, strGroupID);
        //已完成人數
        int iCompleteNum = 0;
        //未完成人數
        int iInCompleteNum = 0;
        //未到人數
        int iYetReachedNum = 0;

        if (dtExerciseCond.Rows.Count > 0)
        {
            if (dtExerciseCond.Rows[0]["iAnswerMode"].ToString().Equals("1"))//個人
            {
                //取得個人出席名單
                DataTable dtIndivStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID_GroupMode("%", strGroupID, "0");
                foreach(DataRow drExerciseCond in dtExerciseCond.Rows)
                {
                    if (drExerciseCond["iExerciseState"].ToString().Equals("1"))
                    {
                        iCompleteNum++;
                    }
                    else
                    {
                        DataRow[] drIndivStudentAtt = dtIndivStudentAtt.Select("cUserID ='" + drExerciseCond["cUserID"].ToString() + "'");
                        if (drIndivStudentAtt[0]["iAttendanceCond"].ToString().Equals("0"))
                        {
                            iYetReachedNum++;
                        }
                        else
                        {
                            iInCompleteNum++;
                        }
                    }
                }
            }
            else if (dtExerciseCond.Rows[0]["iAnswerMode"].ToString().Equals("2"))//小組
            {
                //取得小組出席名單
                DataTable dtTeamStudentAtt = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID_GroupID_GroupMode("%", strGroupID, "1");
                foreach (DataRow drExerciseCond in dtExerciseCond.Rows)
                {
                    if (drExerciseCond["iExerciseState"].ToString().Equals("1"))
                    {
                        iCompleteNum++;
                    }
                    else
                    {
                        DataRow[] drTeamStudentAtt = dtTeamStudentAtt.Select("cUserID ='" + drExerciseCond["cUserID"].ToString() + "'");
                        if (drTeamStudentAtt[0]["iAttendanceCond"].ToString().Equals("0"))
                        {
                            iYetReachedNum++;
                        }
                        else
                        {
                            iInCompleteNum++;
                        }
                    }
                }
            }
         
        }

        //定義作品檔案列表
        HtmlTableRow tbr = new HtmlTableRow();
        tbr.Attributes.Add("Style", "Font-size: 20px; background-color: lightgray");
        tbr.Align = "center";
        HtmlTableCell tbcComplete = new HtmlTableCell();
        tbcComplete.Width = "33%";
        tbcComplete.InnerText = iCompleteNum.ToString();
        tbcComplete.Attributes.Add("Style", "border: 2px solid; border-color: black");
        HtmlTableCell tbcIncomplete = new HtmlTableCell();
        tbcIncomplete.Width = "33%";
        tbcIncomplete.InnerText = iInCompleteNum.ToString(); ;
        tbcIncomplete.Attributes.Add("Style", "border: 2px solid; border-color: black");
        HtmlTableCell tbcYetReached = new HtmlTableCell();
        tbcYetReached.Width = "33%";
        tbcYetReached.Attributes.Add("Style", "border: 2px solid; border-color: black");
        tbcYetReached.InnerText = iYetReachedNum.ToString();


        //加入Table
        tbr.Cells.Add(tbcComplete);
        tbr.Cells.Add(tbcIncomplete);
        tbr.Cells.Add(tbcYetReached);
        tbExFileList.Rows.Add(tbr);
        /*
         * 
            foreach (DataRow drExerciseCond in dtExerciseCond.Rows)
            {
                //定義作品檔案列表
                HtmlTableRow tbrExFileList = new HtmlTableRow();
                tbrExFileList.Attributes.Add("Style", "Font-size: 20px");
                tbrExFileList.Align = "center";
                HtmlTableCell tbcUserName = new HtmlTableCell();
                tbcUserName.Attributes.Add("Style", "border: 2px solid; border-color: black");
                HtmlTableCell tbcExFileListFinishState = new HtmlTableCell();
                tbcExFileListFinishState.Attributes.Add("Style", "border: 2px solid; border-color: black");
                HtmlTableCell tbcExFileListbtnOpenHtml = new HtmlTableCell();
                tbcExFileListbtnOpenHtml.Attributes.Add("Style", "border: 2px solid; border-color: black");
                HtmlTableCell tbcExFileListbtnDownload = new HtmlTableCell();
                tbcExFileListbtnDownload.Attributes.Add("Style", "border: 2px solid; border-color: black");
                HtmlTableCell tbcExFileListUploadNumber = new HtmlTableCell();
                tbcExFileListUploadNumber.Attributes.Add("Style", "border: 2px solid; border-color: black");

                //定義作品檔案列表內容
                //姓名
                Label lbUserName = new Label();
                if (drExerciseCond["iAnswerMode"].ToString() == "1")//取得個人名稱
                    lbUserName.Text = clsORCSUser.ORCS_User_SELECT_by_UserID(drExerciseCond["cUserID"].ToString()).Rows[0]["cUserName"].ToString();
                else if (drExerciseCond["iAnswerMode"].ToString() == "2")//取得小組名稱
                    lbUserName.Text = clsGroup.ORCS_TempGroup_SELECT_By_cTempGroupID(drExerciseCond["cUserID"].ToString()).Rows[0]["cTempGroupName"].ToString();
                //上傳狀態(檢查ORCS_FileUploadData資料表是否有資料)
                Label lbExFileListFinishState = new Label();

                //「網頁瀏覽」按鈕
                Button btnOpenHtml = new Button();
                //「下載」按鈕
                Button btnDownload = new Button();
                //上傳作品檔案數/總題目數
                Label lbUploadNumber = new Label();
                //上傳模式是上傳檔案模式 iUploadMode 上傳檔案:0 上傳文字:1
                if (drExerciseCond["iUploadMode"].ToString().Equals("0"))
                {
                    DataTable dtFileUploadData = clsExercise.ORCS_FileUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, drExerciseCond["cUserID"].ToString());
                    if (dtFileUploadData.Rows.Count > 0) //有檔案
                        lbExFileListFinishState.Text = "上傳完成";
                    else //無檔案
                    {
                        //判斷學生有無出席
                        DataRow[] dr = dtStudentAtt.Select("cUserID='" + drExerciseCond["cUserID"].ToString() + "'");
                        //如果學生出席狀態是準時和遲到設定為尚未上傳 0:未到 1:準時 2:遲到 3:早退
                        if (dr[0]["iAttendanceCond"].ToString().Equals("1") || dr[0]["iAttendanceCond"].ToString().Equals("2"))
                        {
                            lbExFileListFinishState.ForeColor = System.Drawing.Color.Red;
                            lbExFileListFinishState.Text = "尚未上傳";
                        }
                        else//如果是缺席或早退設定為未出席
                        {
                            lbExFileListFinishState.ForeColor = System.Drawing.Color.Gray;
                            lbExFileListFinishState.Text = "未出席";
                        }
                    }

                    //「網頁瀏覽」按鈕
                    btnOpenHtml.Font.Size = 14;
                    btnOpenHtml.Text = "網頁瀏覽";
                    if (dtFileUploadData.Rows.Count > 0) //有檔案才加ID
                    {
                        //取得檔案名稱
                        string strFileName = dtFileUploadData.Rows[0]["cFileName"].ToString();
                        //取得檔案副檔名(變大寫)
                        string strSubFileName = strFileName.Split('.')[1].ToUpper();
                        btnOpenHtml.ID = "btnOpenHtml-" + strExerciseCondID + "-" + strExerciseID + "-" + drExerciseCond["cUserID"].ToString() + "-" + strFileName;//btnOpenHtml-ExerciseCondID-ExerciseID-UserID-FileName
                        //若檔案不符合以下檔案格式網頁瀏覽按鈕設成不可使用
                        if (!(strSubFileName == "PDF" || strSubFileName == "JPG" || strSubFileName == "JPEG"
                            || strSubFileName == "GIF" || strSubFileName == "PNG" || strSubFileName == "BMP" || strSubFileName == "TXT" || strSubFileName == "DOC" || strSubFileName == "DOCX" || strSubFileName == "PPT" || strSubFileName == "PTTX"
                            || strSubFileName == "XLS" || strSubFileName == "XLSX"))
                        {
                            btnOpenHtml.Enabled = false;
                        }
                    }
                    else //無檔案隱藏按鈕
                        btnOpenHtml.Visible = false;


                    //「下載」按鈕
                    btnDownload.Font.Size = 14;
                    btnDownload.Text = "下載";

                }
                else//上傳文字模式
                {
                    DataTable dtTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, drExerciseCond["cUserID"].ToString());
                    if (dtTextUploadData.Rows.Count > 0) //有檔案
                        lbExFileListFinishState.Text = "上傳完成";
                    else //無檔案
                    {
                        //判斷學生有無出席
                        DataRow[] dr = dtStudentAtt.Select("cUserID='" + drExerciseCond["cUserID"].ToString() + "'");
                        //如果學生出席狀態是準時和遲到設定為尚未上傳 0:未到 1:準時 2:遲到 3:早退
                        if (dr[0]["iAttendanceCond"].ToString().Equals("1") || dr[0]["iAttendanceCond"].ToString().Equals("2"))
                        {
                            lbExFileListFinishState.ForeColor = System.Drawing.Color.Red;
                            lbExFileListFinishState.Text = "尚未上傳";
                        }
                        else//如果是缺席或早退設定為未出席
                        {
                            lbExFileListFinishState.ForeColor = System.Drawing.Color.Gray;
                            lbExFileListFinishState.Text = "未出席";
                        }
                    }

                    //「網頁瀏覽」按鈕
                    btnOpenHtml.Font.Size = 14;
                    btnOpenHtml.Text = "網頁瀏覽";
                    if (dtTextUploadData.Rows.Count > 0) //有檔案才加ID
                    {
                        btnOpenHtml.ID = "btnOpenByShowTextUpload-" + strExerciseCondID + "-" + strExerciseID + "-" + drExerciseCond["cUserID"].ToString() + "-" + drExerciseCond["iClassGroupID"].ToString() + "-" + drExerciseCond["iAnswerMode"].ToString() + "-" + strAuthority;
                    }
                    else //無檔案隱藏按鈕
                        btnOpenHtml.Visible = false;


                    //「下載」按鈕
                    btnDownload.Visible = false;

                }

                //上傳作品檔案數/總題目數
                DataTable dtAllFileUploadData = clsExercise.ORCS_FileUploadData_SELECT_by_ExerciseCondID_UserID(strExerciseCondID, drExerciseCond["cUserID"].ToString());
                DataTable dtAllTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ExerciseCondID_UserID(strExerciseCondID, drExerciseCond["cUserID"].ToString());

                //加入Table
                tbcUserName.Controls.Add(lbUserName);
                tbcExFileListFinishState.Controls.Add(lbExFileListFinishState);
                tbcExFileListbtnOpenHtml.Controls.Add(btnOpenHtml);
                tbcExFileListbtnDownload.Controls.Add(btnDownload);
                tbcExFileListUploadNumber.Controls.Add(lbUploadNumber);
                tbrExFileList.Cells.Add(tbcUserName);
                tbrExFileList.Cells.Add(tbcExFileListFinishState);
                tbrExFileList.Cells.Add(tbcExFileListbtnOpenHtml);
                tbrExFileList.Cells.Add(tbcExFileListbtnDownload);
                tbrExFileList.Cells.Add(tbcExFileListUploadNumber);
                tbExFileList.Rows.Add(tbrExFileList);
            }
         */
    }
}