using ORCS.DB.Administrator;
using ORCS.DB.Attendance;
using ORCS.DB.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DataMining_GroupAttendanceStatistics : System.Web.UI.Page
{
    //課程ID
    string strCourseID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GetParameters();
        loadAttStatistics();
    }

    private void GetParameters() 
    {
        //取得課程ID
        if (Request.QueryString["cCourseID"] != null)
        {
            strCourseID = Request.QueryString["cCourseID"].ToString();
        }
    }

    private void loadAttStatistics()
    {

        #region 標題列
        //定義標題列
        HtmlTableRow tbrTitle = new HtmlTableRow();
        HtmlTableCell tbcUserNameTitle = new HtmlTableCell();
        tbcUserNameTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcUserNameTitle.Width = "20%";
        tbcUserNameTitle.Align = "center";
        HtmlTableCell tbcActualAttTitle = new HtmlTableCell();
        tbcActualAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcActualAttTitle.Width = "20%";
        tbcActualAttTitle.Align = "center";
        HtmlTableCell tbcLateAttTitle = new HtmlTableCell();
        tbcLateAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcLateAttTitle.Width = "20%";
        tbcLateAttTitle.Align = "center";
        HtmlTableCell tbcAbsenceAttTitle = new HtmlTableCell();
        tbcAbsenceAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcAbsenceAttTitle.Width = "20%";
        tbcAbsenceAttTitle.Align = "center";
        HtmlTableCell tbcAttTitle = new HtmlTableCell();
        tbcAttTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcAttTitle.Width = "20%";
        tbcAttTitle.Align = "center";
        //HtmlTableCell tbcTitleClassStatistics = new HtmlTableCell();
        //tbcTitleClassStatistics.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        //tbcTitleClassStatistics.Align = "center";
        //定義標題列物件 
        Label lbUserNameTitle = new Label();
        lbUserNameTitle.Text = "使用者名稱";
        Label lbActualAttTitle = new Label();
        lbActualAttTitle.Text = "準時到課";
        Label lbLateAttTitle = new Label();
        lbLateAttTitle.Text = "遲到";
        Label lbAbsenceAttTitle = new Label();
        lbAbsenceAttTitle.Text = "未到";
        Label lbAttTitle = new Label();
        lbAttTitle.Text = "實到(%)";
        //Label lbTitleClassStatistics = new Label();
        //lbTitleClassStatistics.Text = "全班統計";
        //將物件加入標題列
        tbcUserNameTitle.Controls.Add(lbUserNameTitle);
        tbcActualAttTitle.Controls.Add(lbActualAttTitle);
        tbcLateAttTitle.Controls.Add(lbLateAttTitle);
        tbcAbsenceAttTitle.Controls.Add(lbAbsenceAttTitle);
        tbcAttTitle.Controls.Add(lbAttTitle);
        //tbcTitleClassStatistics.Controls.Add(lbTitleClassStatistics);
        tbrTitle.Cells.Add(tbcUserNameTitle);
        tbrTitle.Cells.Add(tbcActualAttTitle);
        tbrTitle.Cells.Add(tbcLateAttTitle);
        tbrTitle.Cells.Add(tbcAbsenceAttTitle);
        tbrTitle.Cells.Add(tbcAttTitle);
        //tbrTitle.Cells.Add(tbcTitleClassStatistics);
        tbClassAttStatistics.Rows.Add(tbrTitle);
        #endregion

        #region 內容
        //根據課程抓取底下所有的上課時段
        DataTable dtTime = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_DISTINCT_GroupID(strCourseID);
        //取得課程學生名單
        DataTable dtStudents = clsEditGroupMember.ORCS_GroupMember_SELECT_by_GroupID_Classify(strCourseID, clsGroupNode.GroupClassification_ClassGroup, AllSystemUser.Authority_Student);
        //讀取出席歷史資料
        DataTable dtAttendanceHis = clsAttendanceHistory.ORCS_StudentAttendanceHistory_SELECT_by_GroupID(strCourseID);
        //第幾位學生
        int iTimes = 0;
        foreach (DataRow drStudent in dtStudents.Rows)
        {
            iTimes++;

            HtmlTableRow tbrContent = new HtmlTableRow();
            HtmlTableCell tbcUserNameContent = new HtmlTableCell();
            if ((iTimes%2)==1)
            { }
            tbcUserNameContent.Attributes.Add("Style", "border: 2px solid black; font-weight: bold;");
            tbcUserNameContent.Align = "center";
            HtmlTableCell tbcActualAttContent = new HtmlTableCell();
            tbcActualAttContent.Attributes.Add("Style", "border: 2px solid black;");
            tbcActualAttContent.Align = "center";
            HtmlTableCell tbcLateAttContent = new HtmlTableCell();
            tbcLateAttContent.Attributes.Add("Style", "border: 2px solid black; font-weight: bold;");
            tbcLateAttContent.Align = "center";
            HtmlTableCell tbcAbsenceAttContent = new HtmlTableCell();
            tbcAbsenceAttContent.Attributes.Add("Style", "border: 2px solid black; font-weight: bold;");
            tbcAbsenceAttContent.Align = "center";
            HtmlTableCell tbcAttContent = new HtmlTableCell();
            tbcAttContent.Attributes.Add("Style", "border: 2px solid black; font-weight: bold;");
            tbcAttContent.Align = "center";

            if ((iTimes % 2) == 1)
            {
                tbcUserNameContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFDFAC;");
                tbcActualAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFDFAC;");
                tbcLateAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFDFAC;");
                tbcAbsenceAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFDFAC;");
                tbcAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFDFAC;");
            }
            else
            {
                tbcUserNameContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFCAB6;");
                tbcActualAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFCAB6;");
                tbcLateAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFCAB6;");
                tbcAbsenceAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFCAB6;");
                tbcAttContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFCAB6;");
            }


            //使用者名稱
            Label lbUserNameContent = new Label();
            string strStudentID = drStudent["cUserID"].ToString();
            //取得個人名稱
            string strStudentName = clsORCSUser.ORCS_User_SELECT_by_UserID(strStudentID).Rows[0]["cUserName"].ToString();
            lbUserNameContent.Text = strStudentName;

            //出席資料
            Label lbActualAttContent = new Label();//實到
            Label lbLateAttContent = new Label();//遲到
            Label lbAbsenceAttContent = new Label();//未到
            Label lbAttContent = new Label();//有到

            //實到次數
            int iActualAtt = 0;
            //遲到次數
            int iLateAtt = 0;
            //未到次數
            int iAbsenceAtt = 0;
            //全部次數
            int iAllAtt = 0;

            foreach (DataRow drTime in dtTime.Rows)
            {
                string strAttendanceID = drTime["cAttendanceID"].ToString();
                DataRow[] drAttendanceHis = dtAttendanceHis.Select("cAttendanceID = '" + strAttendanceID + "' AND cUserID = '" + strStudentID + "'");
                if (drAttendanceHis.Length > 0)
                {
                    iAllAtt++;

                    if (drAttendanceHis[0]["iAttendanceCond"].ToString() == "0") // 未到
                    {
                        iAbsenceAtt++;
                    }
                    else if (drAttendanceHis[0]["iAttendanceCond"].ToString() == "1") // 實到
                    {
                        iActualAtt++;
                    }
                    else // 遲到
                    {
                        iLateAtt++;
                    }
                }
            }
            lbActualAttContent.Text = iActualAtt.ToString();
            lbLateAttContent.Text = iLateAtt.ToString();
            lbAbsenceAttContent.Text = iAbsenceAtt.ToString();
            lbAttContent.Text = (iLateAtt + iActualAtt).ToString() + "(" + (((double)(iLateAtt + iActualAtt) / (double)iAllAtt) * 100).ToString("0.#") + "%)";

            tbcUserNameContent.Controls.Add(lbUserNameContent);
            tbcActualAttContent.Controls.Add(lbActualAttContent);
            tbcLateAttContent.Controls.Add(lbLateAttContent);
            tbcAbsenceAttContent.Controls.Add(lbAbsenceAttContent);
            tbcAttContent.Controls.Add(lbAttContent);

            tbrContent.Cells.Add(tbcUserNameContent);
            tbrContent.Cells.Add(tbcActualAttContent);
            tbrContent.Cells.Add(tbcLateAttContent);
            tbrContent.Cells.Add(tbcAbsenceAttContent);
            tbrContent.Cells.Add(tbcAttContent);

            tbClassAttStatistics.Rows.Add(tbrContent);
        }
        #endregion
    }
}