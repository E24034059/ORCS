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
using ORCS.DB;
using ORCS.DB.Administrator;
using ORCS.DB.User;

public partial class Administrator_AddNewClassTime : System.Web.UI.Page
{
    ORCSSessionManager ORCSSession;//定義Session
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session初始化
        ORCSSession = new ORCSSessionManager(this);
        //檢查是否已登入，若未登入則直接關閉頁面
        if (!ORCSSession.IsLogin)
        {
            Response.Redirect("<script>window.close();</script>");
        }
        if (!IsPostBack)
        {
            //載入課程
            LoadClass();
        }
    }
    //載入課程
    protected void LoadClass()
    {
        //先清空系所和課程資料
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
    //抓取起始時間或結束時間
    protected string GetStartAndEndTime(string strMode)
    {
        string strTime = "";
        if (strMode == "StartTime")
            strTime = ddlClassStartTime.SelectedItem.Value;
        else
            strTime = ddlClassEndTime.SelectedItem.Value;
        //抓取時間
        switch (strTime)
        {
            case "One":
                strTime = "08:00:00";
                break;
            case "Two":
                strTime = "09:00:00";
                break;
            case "Three":
                strTime = "10:00:00";
                break;
            case "Four":
                strTime = "11:00:00";
                break;
            case "Five":
                strTime = "14:00:00";
                break;
            case "Six":
                strTime = "15:00:00";
                break;
            case "Seven":
                strTime = "16:00:00";
                break;
            case "Eight":
                strTime = "17:00:00";
                break;
            case "Nine":
                strTime = "18:00:00";
                break;
            case "A":
                strTime = "19:00:00";
                break;
            case "B":
                strTime = "20:00:00";
                break;
            case "C":
                strTime = "21:00:00";
                break;
        }
        return strTime;
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
    //「確定」按鈕事件
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //判斷上課日期是否有填寫
        if (tbClassStartDate.Text != "")
        {
            //定義起始時間
            string strSartTime = GetStartAndEndTime("StartTime");
            //定義結束時間
            string strEndTime = GetStartAndEndTime("EndTime");
            //將上課起始日期時間轉成DateTime
            DateTime datStartTime = Convert.ToDateTime(tbClassStartDate.Text + " " + strSartTime);
            //將上課結束日期時間轉成DateTime
            DateTime datEndTime = Convert.ToDateTime(tbClassStartDate.Text + " " + strEndTime);
            //將上課時間存入ClassTimeTable資料表
            //先判斷是否有重複方式(每天、每周、每月、每年)
            if (tbClassEndDate.Text != "")
            {
                //將上課期限日期轉成DateTime
                DateTime datDueTime = Convert.ToDateTime(tbClassEndDate.Text + " 23:59:59");
                //定義DueDate起始時間
                DateTime datStartDate = datStartTime;
                //不同重複方式(每天、每周、每月、每年)存入資料表
                while (datStartDate < datDueTime)
                {
                    DateTime dat = datStartDate;
                    while (dat <= datEndTime)
                    {
                        //存入資料表
                        clsClassTimeTable.ORCS_ClassTimeTable_INSERT(ddlClass.SelectedItem.Value, dat.ToString("yyyyMMddHHmmss"), dat.AddHours(1).ToString("yyyyMMddHHmmss"));
                        //將條件時間加1小時(若遇到中午則加3小時)
                        if (dat.ToString("HHmmss") == "110000")
                            dat = dat.AddHours(3);
                        else
                            dat = dat.AddHours(1);
                    }
                    //判斷重複方式增加日期
                    switch (ddlRepeat.SelectedItem.Value)
                    {
                        case "None": //不重複
                            datStartDate = datDueTime;
                            break;
                        case "Day":  //每天
                            datStartDate = datStartDate.AddDays(1);
                            datEndTime = datEndTime.AddDays(1);
                            break;
                        case "Week": //每周
                            datStartDate = datStartDate.AddDays(7);
                            datEndTime = datEndTime.AddDays(7);
                            break;
                        case "Month"://每月
                            datStartDate = datStartDate.AddMonths(1);
                            datEndTime = datEndTime.AddMonths(1);
                            break;
                        case "Year": //每年
                            datStartDate = datStartDate.AddYears(1);
                            datEndTime = datEndTime.AddYears(1);
                            break;
                    }
                }
            }
            else
            {
                DateTime dat = datStartTime;
                while (dat <= datEndTime)
                {
                    //存入資料表
                    clsClassTimeTable.ORCS_ClassTimeTable_INSERT(ddlClass.SelectedItem.Value, dat.ToString("yyyyMMddHHmmss"), dat.AddHours(1).ToString("yyyyMMddHHmmss"));
                    //將條件時間加1小時(若遇到中午則加3小時)
                    if (dat.ToString("HHmmss") == "110000")
                        dat = dat.AddHours(3);
                    else
                        dat = dat.AddHours(1);
                }
            }
            //關閉視窗
            Response.Write("<script>window.close()</script>");
        }
        else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('請輸入上課日期');</script>");
    }
}
