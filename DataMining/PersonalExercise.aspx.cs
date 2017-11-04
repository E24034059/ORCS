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

public partial class DataMining_PersonalExercise : ORCS.ORCS_Page
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
            //載入學生
            LoadStudent();
            //檢查下拉式選單是否有預設值
            if (Request.QueryString["ddlClass"] != null)
            {
                string strClass = Request.QueryString["ddlClass"].ToString();
                //檢查下拉式選單是否該項目，若有則選定其項目
                if (ddlClass.Items.FindByValue(strClass) != null)
                    ddlClass.SelectedValue = strClass;
            }
            if (Request.QueryString["ddlStudent"] != null)
            {
                string strStudent = Request.QueryString["ddlStudent"].ToString();
                //檢查下拉式選單是否該項目，若有則選定其項目
                if (ddlStudent.Items.FindByValue(strStudent) != null)
                    ddlStudent.SelectedValue = strStudent;
            }
            //載入總統計資料
            LoadTotalStatistics();
            //載入歷史訊息
            LoadHistory();
        }
    }
    //載入系所
    protected void LoadDepartment()
    {
        //先清空系所、課程和學生資料
        ddlDepartment.Items.Clear();
        ddlClass.Items.Clear();
        ddlStudent.Items.Clear();
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
            ddlStudent.Items.Clear();
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
    //載入學生
    protected void LoadStudent()
    {
        if (ddlClass.Items.Count > 0)
        {
            //先清空學生資料
            ddlStudent.Items.Clear();
            //根據課程抓取底下所有的學生
            DataTable dtStudent = clsEditGroupMember.ORCS_GroupMember_SELECT_by_GroupID_Classify(ddlClass.SelectedItem.Value, clsGroupNode.GroupClassification_ClassGroup,"s");
            if (dtStudent.Rows.Count > 0)
            {
                foreach (DataRow drStudent in dtStudent.Rows)
                {
                    //取得使用者資料
                    DataTable dtUser = clsORCSUser.ORCS_User_SELECT_by_UserID(drStudent["cUserID"].ToString());
                    //若為學生才加入DropDownList
                    if (dtUser.Rows[0]["cAuthority"].ToString() == "s")
                    {
                        //取得學生姓名
                        string strStudentName = dtUser.Rows[0]["cUserName"].ToString();
                        //加入DropDownList
                        ddlStudent.Items.Add(new ListItem(strStudentName, drStudent["cUserID"].ToString()));
                    }
                }
            }
        }
    }
    //載入總統計資料
    protected void LoadTotalStatistics()
    {
        //先清空table物件
        tbTotalStatistics.Controls.Clear();
        if (ddlStudent.Items.Count > 0)
        {
            #region 標題列
            //定義標題列
            HtmlTableRow tbrTotalStaTitle = new HtmlTableRow();
            HtmlTableCell tbcTotalStaTitle = new HtmlTableCell();
            tbcTotalStaTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold; CURSOR: hand;");
            tbcTotalStaTitle.Align = "center";
            //定義標題列物件
            Label lbTotalStaTitle = new Label(); // 定義點擊縮放的圖示
            lbTotalStaTitle.Text = "<IMG id='img_TotalStaTitle' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "總作答率";
            //將物件加入標題列
            tbcTotalStaTitle.Controls.Add(lbTotalStaTitle);
            tbrTotalStaTitle.Cells.Add(tbcTotalStaTitle);
            tbTotalStatistics.Rows.Add(tbrTotalStaTitle);
            #endregion

            #region 內容
            //讀取作答歷史資料
            DataTable dtExerciseHis = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_UserID_GroupID(ddlStudent.SelectedItem.Value, ddlClass.SelectedItem.Value);
            if (dtExerciseHis.Rows.Count > 0)
            {
                double dNoAns = 0; //定義未作答題數
                double dFinAns = 0;  //定義已作答題數
                foreach (DataRow drExerciseHis in dtExerciseHis.Rows)
                {
                    if (drExerciseHis["iExerciseState"].ToString() == "0")
                        dNoAns++;
                    else
                        dFinAns++;
                }
                string strNoAns = "未作答份數: " + dNoAns + "份";
                string strFinAns = "已作答份數: " + dFinAns + "份";
                //計算作答率
                double dExRate = (int)((dFinAns / dtExerciseHis.Rows.Count) * 100);
                lbTotalStaTitle.Text = "<IMG id='img_TotalStaTitle' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "總作答率:" + dExRate + "%";
                //定義作答率列
                HtmlTableRow tbrExRate = new HtmlTableRow();
                tbrExRate.ID = "tbrExRate_TotalStaTitle";
                HtmlTableCell tbcExRate = new HtmlTableCell();
                tbcExRate.Attributes.Add("Style", "border: 2px solid black;");
                tbcExRate.Align = "center";
                tbcExRate.ID = "tbcExRate_TotalStaTitle";
                tbcExRate.Height = "300";
                tbcTotalStaTitle.Attributes.Add("onclick", "ShowChart('" + tbcExRate.ID + "' , 'img_TotalStaTitle')");
                //將物件加入Table
                tbrExRate.Cells.Add(tbcExRate);
                tbTotalStatistics.Rows.Add(tbrExRate);
                //呼叫javacript畫圖餅圖函數
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>drawChart('" + strFinAns + "','" + strNoAns + "'," + dFinAns + ", " + dNoAns + ",'" + tbcExRate.ID + "')</script>");
            }
            #endregion
        }
    }
    //載入歷史訊息
    protected void LoadHistory()
    {
        //先清空table物件
        tbInfo.Controls.Clear();
        if (ddlStudent.Items.Count > 0)
        {
            #region 標題列
            //定義標題列
            HtmlTableRow tbrTitle = new HtmlTableRow();
            HtmlTableCell tbcTitleTime = new HtmlTableCell();
            tbcTitleTime.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleTime.Align = "center";
            tbcTitleTime.Width = "20%";
            HtmlTableCell tbcTitleQues = new HtmlTableCell();
            tbcTitleQues.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleQues.Align = "center";
            tbcTitleQues.Width = "15%";
            HtmlTableCell tbcTitleState = new HtmlTableCell();
            tbcTitleState.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleState.Align = "center";
            tbcTitleState.Width = "15%";
            HtmlTableCell tbcTitleFileDownload = new HtmlTableCell();
            tbcTitleFileDownload.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleFileDownload.Align = "center";
            tbcTitleFileDownload.Width = "15%";
            HtmlTableCell tbcTitleClassStatistics = new HtmlTableCell();
            tbcTitleClassStatistics.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
            tbcTitleClassStatistics.Align = "center";
            //定義標題列物件
            Label lbTitleTime = new Label();
            lbTitleTime.Text = "上課日期";
            Label lbTitleQues = new Label();
            lbTitleQues.Text = "習題";
            Label lbTitleState = new Label();
            lbTitleState.Text = "狀態";
            Label lbTitleFileDownload = new Label();
            lbTitleFileDownload.Text = "作品檔案";
            Label lbTitleClassStatistics = new Label();
            lbTitleClassStatistics.Text = "全班統計";
            //將物件加入標題列
            tbcTitleTime.Controls.Add(lbTitleTime);
            tbcTitleQues.Controls.Add(lbTitleQues);
            tbcTitleState.Controls.Add(lbTitleState);
            tbcTitleFileDownload.Controls.Add(lbTitleFileDownload);
            tbcTitleClassStatistics.Controls.Add(lbTitleClassStatistics);
            tbrTitle.Cells.Add(tbcTitleTime);
            tbrTitle.Cells.Add(tbcTitleQues);
            tbrTitle.Cells.Add(tbcTitleState);
            tbrTitle.Cells.Add(tbcTitleFileDownload);
            tbrTitle.Cells.Add(tbcTitleClassStatistics);
            tbInfo.Rows.Add(tbrTitle);
            #endregion

            #region 內容
            //讀取出席歷史資料
            DataTable dtExerciseHis = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_DISTINCT_UserID_GroupID(ddlStudent.SelectedItem.Value, ddlClass.SelectedItem.Value);
            //讀取學生上傳作品檔案資料(上傳一般檔案)
            DataTable dtFileUploadData = clsExercise.ORCS_FileUploadData_SELECT_by_ClassGroupID_UserID(ddlClass.SelectedItem.Value, ddlStudent.SelectedItem.Value);
            //讀取學生上傳作品檔案資料(上傳文字檔案)
            DataTable dtTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ClassGroupID_UserID(ddlClass.SelectedItem.Value, ddlStudent.SelectedItem.Value);
            
            int iCount = 0;//定義資料筆數
            if (dtExerciseHis.Rows.Count > 0)
            {
                foreach (DataRow drExerciseHis in dtExerciseHis.Rows)
                {
                    //先累加資料筆數
                    iCount++;
                    //定義TableRow和TableCell
                    HtmlTableRow tbrExerciseHisTitle = new HtmlTableRow(); // 定義子標題列(點擊縮放的地方)
                    HtmlTableRow tbrExerciseHis = new HtmlTableRow(); // 定義歷史資料列
                    tbrExerciseHis.ID = "tbrExerciseHis_" + drExerciseHis["cExerciseCondID"].ToString();
                    HtmlTableCell tbcExerciseHisTitle = new HtmlTableCell(); // 定義子標題行
                    tbcExerciseHisTitle.Attributes.Add("Style", "background-color: #C6dfff; CURSOR: hand; border: 2px solid black;");
                    tbcExerciseHisTitle.Attributes.Add("onclick", "ShowChart('" + tbrExerciseHis.ID + "' , 'img_" + drExerciseHis["cExerciseCondID"].ToString() + "')");
                    tbcExerciseHisTitle.ColSpan = 5;
                    HtmlTableCell tbcTime = new HtmlTableCell(); // 定義第一行(上課日期)
                    tbcTime.Attributes.Add("Style", "border: 2px solid black;");
                    tbcTime.Align = "center";
                    HtmlTableCell tbcDetail = new HtmlTableCell(); // 定義第二行(習題、狀態和全班統計)
                    tbcDetail.Attributes.Add("Style", "border: 2px solid black;");
                    tbcDetail.Align = "center";
                    tbcDetail.ColSpan = 4; // 將習題、狀態、作品下載和全班統計合併
                    HtmlTable tbDetail = new HtmlTable();// 定義Table(將習題、狀態、作品檔案和全班統計合併放在TableCell裡)
                    tbDetail.Width = "100%";
                    tbDetail.Attributes.Add("Style", "border-collapse: collapse; font-size: x-large;");
                    //定義作答歷史資料物件
                    Label lbExerciseHisTitle = new Label(); // 定義點擊縮放的圖示
                    lbExerciseHisTitle.Text = "<IMG id='img_" + drExerciseHis["cExerciseCondID"].ToString() + "' src='../App_Themes/general/images/collapse.gif' height='15px' width='20px'>" + "第" + iCount + "筆資料";
                    lbExerciseHisTitle.Font.Size = 18;
                    Label lbTime = new Label(); // 定義上課日期
                    lbTime.Text = "<a href=GroupExercise.aspx?ddlClass=" + ddlClass.SelectedItem.Value + "&ddlTime=" + drExerciseHis["cExerciseCondID"].ToString() + ">" + clsTimeConvert.DBTimeToDateTime(drExerciseHis["cExerciseCondID"].ToString()).ToShortDateString() + "</a>";
                    //將作答歷史資料物件加入Table
                    tbcExerciseHisTitle.Controls.Add(lbExerciseHisTitle);
                    tbcTime.Controls.Add(lbTime);
                    tbcDetail.Controls.Add(tbDetail);
                    tbrExerciseHisTitle.Cells.Add(tbcExerciseHisTitle);
                    tbrExerciseHis.Cells.Add(tbcTime);
                    tbrExerciseHis.Cells.Add(tbcDetail);
                    tbInfo.Rows.Add(tbrExerciseHisTitle);
                    tbInfo.Rows.Add(tbrExerciseHis);

                    //抓取每個上課時段習題詳細資料
                    DataTable dtExerciseQuesHis = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_ExerciseCondID_cUserID_GroupID(drExerciseHis["cExerciseCondID"].ToString(), ddlStudent.SelectedItem.Value, ddlClass.SelectedItem.Value);
                    int iCountNum = 0;//定義題數
                    if (dtExerciseQuesHis.Rows.Count > 0)
                    {
                        foreach (DataRow drExerciseQuesHis in dtExerciseQuesHis.Rows)
                        {

                            //先累加題數
                            iCountNum++;
                            //作品檔案
                            HtmlTable tbFile = new HtmlTable();// 定義Table(將網頁瀏覽和檔案下載合併放在TableCell(作品檔案)裡)
                            tbFile.Width = "100%";
                            tbFile.Height = "100%";
                            tbFile.Attributes.Add("Style", "border-collapse: collapse; font-size: x-large;");
                            //定義TableRow和TableCell
                            HtmlTableRow tbrExerciseQuesHis = new HtmlTableRow(); // 定義習題資料列
                            HtmlTableRow tbrExerciseFileWebBrowsing = new HtmlTableRow(); // 定義作品檔案之網頁瀏覽列
                            HtmlTableRow tbrExerciseFileDownload = new HtmlTableRow(); // 定義作品檔案之檔案下載列
                            HtmlTableCell tbcQues = new HtmlTableCell(); // 定義第二行(習題)
                            tbcQues.Attributes.Add("Style", "border: 2px solid black;");
                            tbcQues.Align = "center";
                            tbcQues.Width = "18.5%";
                            HtmlTableCell tbcState = new HtmlTableCell(); // 定義第三行(狀態)
                            tbcState.Attributes.Add("Style", "border: 2px solid black;");
                            tbcState.Align = "center";
                            tbcState.Width = "19.5%";
                            HtmlTableCell tbcFile = new HtmlTableCell(); // 定義第四行(作品檔案)
                            tbcFile.Attributes.Add("Style", "border: 2px solid black;");
                            tbcFile.Align = "center";
                            tbcFile.Width = "19.5%";
                            HtmlTableCell tbcFileWebBrowsing = new HtmlTableCell(); // 定義第四行(作品檔案之網頁瀏覽)
                            tbcFileWebBrowsing.Attributes.Add("Style", "border: 2px solid black;");
                            tbcFileWebBrowsing.Align = "center";
                            tbcFileWebBrowsing.Width = "18.5%";
                            tbcFileWebBrowsing.Height = "50%";
                            HtmlTableCell tbcFileDownload = new HtmlTableCell(); // 定義第四行(作品檔案之下載檔案)
                            tbcFileDownload.Attributes.Add("Style", "border: 2px solid black;");
                            tbcFileDownload.Align = "center";
                            tbcFileDownload.Width = "18.5%";
                            tbcFileDownload.Height = "50%";
                            HtmlTableCell tbcClassStatistics = new HtmlTableCell(); // 定義第五行(全班統計)
                            tbcClassStatistics.Attributes.Add("Style", "border: 2px solid black;");
                            tbcClassStatistics.Align = "center";
                            tbcClassStatistics.ID = "tbcClassStatistics_" + drExerciseQuesHis["cExerciseCondID"].ToString() + "_" + drExerciseQuesHis["cExerciseID"].ToString();
                            tbcClassStatistics.Height = "200";
                            //定義出席歷史資料物件
                            Label lbQues = new Label(); // 定義題目
                            if (drExerciseQuesHis["cPaperID"].ToString() == "NULL" || drExerciseQuesHis["cPaperID"].ToString() == "")//無考卷則無超連結
                                lbQues.Text = "第" + iCountNum + "份";
                            else//有考卷則可觀看答題情形
                                lbQues.Text = "<a href=\"javascript:void window.open('/HINTS/Learning/Exercise/Paper_InfoForAnsCond.aspx?cUserID=" + drExerciseQuesHis["cUserID"].ToString() + "&cPaperID=" + drExerciseQuesHis["cPaperID"].ToString() + "', '', 'menubar=no,resizable=yes')\">第" + iCountNum + "份</a>";
                            Label lbState = new Label(); // 定義狀態
                            if (drExerciseQuesHis["iExerciseState"].ToString() == "0")
                            {
                                lbState.Text = "未作答";
                                lbState.ForeColor = Color.Red;
                            }
                            else
                            {
                                lbState.Text = "已作答";
                                lbState.ForeColor = Color.Blue;
                            }

                            //取得作品檔案資訊(上傳檔案)
                            DataRow[] drFileUploadData = dtFileUploadData.Select("cExerciseCondID='" + drExerciseQuesHis["cExerciseCondID"] + "' and cExerciseID='" + drExerciseQuesHis["cExerciseID"] + "'");
                            //取得作品檔案資訊(上傳文字)
                            DataRow[] drTextUploadData = dtTextUploadData.Select("cExerciseCondID='" + drExerciseQuesHis["cExerciseCondID"] + "' and cExerciseID='" + drExerciseQuesHis["cExerciseID"] + "'");
                            //作品「網頁瀏覽」按鈕
                            Button btnFileWebBrowsing = new Button();
                            btnFileWebBrowsing.Font.Size = 14;
                            btnFileWebBrowsing.Text = "網頁瀏覽";
                            if (drFileUploadData.Count() > 0) //有檔案才加ID
                            {

                                //取得檔案名稱
                                string strFileName = drFileUploadData[0]["cFileName"].ToString();
                                //取得檔案副檔名(變大寫)
                                string strSubFileName = strFileName.Split('.')[1].ToUpper();
                                //btnDownload-subFileName-ExerciseCondID-ExerciseID-UserID-FileName
                                btnFileWebBrowsing.ID = "btnFileWebBrowsing-" + strSubFileName + "-" + drExerciseQuesHis["cExerciseCondID"] + "-" + drExerciseQuesHis["cExerciseID"] + "-" + drExerciseQuesHis["cUserID"].ToString() + "-" + drFileUploadData[0]["cFileName"].ToString();
                                //若檔案不符合以下檔案格式網頁瀏覽按鈕設成不可使用
                                if (!(strSubFileName == "PDF" || strSubFileName == "JPG" || strSubFileName == "JPEG"
                                    || strSubFileName == "GIF" || strSubFileName == "PNG" || strSubFileName == "BMP" || strSubFileName == "TXT" || strSubFileName == "DOC" || strSubFileName == "DOCX" || strSubFileName == "PPT" || strSubFileName == "PTTX"
                                    || strSubFileName == "XLS" || strSubFileName == "XLSX"))
                                {
                                    btnFileWebBrowsing.Enabled = false;
                                }
                                btnFileWebBrowsing.Attributes["onclick"] = "return false;";
                                btnFileWebBrowsing.OnClientClick = "btnWebBrowsingClick(this);";
                            }
                            else if (drTextUploadData.Count() > 0)
                            {
                                btnFileWebBrowsing.ID = "btnOpenByShowTextUpload-" + drExerciseQuesHis["cExerciseCondID"] + "-" + drExerciseQuesHis["cExerciseID"] + "-" + drExerciseQuesHis["cUserID"].ToString() + "-" + ddlClass.SelectedItem.Value + "-" + drExerciseQuesHis["iAnswerMode"].ToString() + "-" + AllSystemUser.Authority_Teacher;
                                btnFileWebBrowsing.Attributes["onclick"] = "return false;";
                                btnFileWebBrowsing.OnClientClick = "btnWebTextBrowsingClick(this);";
                            } else //無檔案隱藏按鈕
                                btnFileWebBrowsing.Visible = false;

                            //作品「檔案下載」按鈕
                            Button btnFileDownload = new Button();
                            btnFileDownload.Font.Size = 14;
                            btnFileDownload.Text = "檔案下載";
                            if (drFileUploadData.Count() > 0) //有檔案才加ID
                            {
                                btnFileDownload.ID = "btnDownload-" + drExerciseQuesHis["cExerciseCondID"] + "-" + drExerciseQuesHis["cExerciseID"] + "-" + drExerciseQuesHis["cUserID"].ToString() + "-" + drFileUploadData[0]["cFileName"].ToString();//btnDownload-ExerciseCondID-ExerciseID-UserID-FileName
                                btnFileDownload.Attributes["onclick"] = "return false;";
                                btnFileDownload.OnClientClick = "btnDownloadClick(this);";
                            }
                            else //無檔案隱藏按鈕
                                btnFileDownload.Visible = false;

                            //作品檔案
                            tbcFileWebBrowsing.Controls.Add(btnFileWebBrowsing);
                            tbcFileDownload.Controls.Add(btnFileDownload);
                            tbrExerciseFileWebBrowsing.Cells.Add(tbcFileWebBrowsing);
                            tbrExerciseFileDownload.Cells.Add(tbcFileDownload);
                            tbFile.Controls.Add(tbrExerciseFileWebBrowsing);
                            tbFile.Controls.Add(tbrExerciseFileDownload);
                            //將作答歷史資料物件加入TableCell
                            tbcQues.Controls.Add(lbQues);
                            tbcState.Controls.Add(lbState);
                            tbcFile.Controls.Add(tbFile);
                            tbrExerciseQuesHis.Cells.Add(tbcQues);
                            tbrExerciseQuesHis.Cells.Add(tbcState);
                            tbrExerciseQuesHis.Cells.Add(tbcFile);
                            tbrExerciseQuesHis.Cells.Add(tbcClassStatistics);
                            tbDetail.Controls.Add(tbrExerciseQuesHis);

                            //圓餅圖計算
                            DataTable dtExerClassHis = clsExerciseHistory.ORCS_ExerciseConditionHistory_SELECT_by_ExerciseCondID_ExerciseID_GroupID(drExerciseHis["cExerciseCondID"].ToString(), drExerciseQuesHis["cExerciseID"].ToString(), ddlClass.SelectedItem.Value);
                            if (dtExerClassHis.Rows.Count > 0)
                            {
                                int iNoAns = 0; //定義未作答人數
                                int iFinAns = 0;  //定義已作答人數
                                foreach (DataRow drExerClassHis in dtExerClassHis.Rows)
                                {
                                    if (drExerClassHis["iExerciseState"].ToString() == "0")
                                        iNoAns++;
                                    else
                                        iFinAns++;
                                }
                                string strNoAns = "未作答人數: " + iNoAns + "人";
                                string strFinAns = "已作答人數: " + iFinAns + "人";
                                //呼叫javacript畫圖餅圖函數
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload" + drExerciseQuesHis["cExerciseCondID"].ToString() + drExerciseQuesHis["cExerciseID"].ToString(), "<script>drawChart('" + strFinAns + "','" + strNoAns + "'," + iFinAns + ", " + iNoAns + ",'" + tbcClassStatistics.ID + "')</script>");
                            }
                        }
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
        //載入學生
        LoadStudent();
        //載入總統計資料
        LoadTotalStatistics();
        //載入歷史訊息
        LoadHistory();
    }
    //選擇不同課程發生之事件
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入學生
        LoadStudent();
        //載入總統計資料
        LoadTotalStatistics();
        //載入歷史訊息
        LoadHistory();
    }
    //選擇不同學生發生之事件
    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        //載入總統計資料
        LoadTotalStatistics();
        //載入歷史訊息
        LoadHistory();
    }
}