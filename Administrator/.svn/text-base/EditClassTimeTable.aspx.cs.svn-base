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
using ORCS.DB.Administrator;
using ORCS.DB.User;

public partial class Administrator_EditClassTimeTable : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //頁面初始化
        base.ORCS_Init();

        if (!IsPostBack)
        {
            //初始行事曆日期為現在
            calClassTimeTable.SelectedDate = DateTime.Now;
            //若為學生則將「新增課程」按鈕隱藏
            if (ORCSSession.Authority == "s")
                btnAddNewClassTime.Visible = false;
        }
        //載入課程行程表
            LoadClassTimeTable();
    }
    //載入課程行程表
    protected void LoadClassTimeTable()
    {
        //先清空TableCell物件
        for (int i = 0; i < 14; i++)
        {
            for (int j = 1; j < 8; j++)
            {
                if (i != 0 && i != 5)
                {
                    tbClassTimeTable.Rows[i].Cells[j].Controls.Clear();//清空物件
                    tbClassTimeTable.Rows[i].Cells[j].BgColor = "";//將背景顏色還原
                }
                else if (i == 0)
                    tbClassTimeTable.Rows[i].Cells[j].BgColor = "#CCCCCC";//將背景顏色還原
            }
        }
        //變換選擇到星期幾的顏色
        SelectDayOfColor();
        //找出使用者所屬課程
        DataTable dtClass = clsEditGroupMember.ORCS_GroupMember_SELECT_by_UserID_Classify(ORCSSession.UserID, clsGroupNode.GroupClassification_ClassGroup);
        if (dtClass.Rows.Count > 0)
        {
            //抓取各個課程的時間
            DataTable dtClassTimeTable = new DataTable();
            foreach (DataRow drClass in dtClass.Rows)
            {
                //取得時間表
                dtClassTimeTable.Merge(clsClassTimeTable.ORCS_ClassTimeTable_SELECT_by_ClassGroupID(drClass["iGroupID"].ToString()));
            }
            if (dtClassTimeTable.Rows.Count > 0)
            {
                //取得當週行事曆起始日期和結束日期
                string strStartEndDate = clsTimeConvert.DayOfWeekStartEnd(calClassTimeTable.SelectedDate);
                string strStartDate = strStartEndDate.Split('_')[0].ToString();            //起始日期
                string strEndDate = strStartEndDate.Split('_')[1].ToString() + " 23:59:59";//結束日期
                //判斷抓取的課程是否介於起始日期和結束日期之間，若是則顯示在課表上
                foreach (DataRow drClassTimeTable in dtClassTimeTable.Rows)
                {
                    DateTime datStartTime = clsTimeConvert.DBTimeToDateTime(drClassTimeTable["cStartTime"].ToString());
                    if (datStartTime >= Convert.ToDateTime(strStartDate) && datStartTime <= Convert.ToDateTime(strEndDate))
                        ShowClassTimeTable(drClassTimeTable["iClassGroupID"].ToString(), datStartTime);
                }
            }
        }
    }
    //顯示課表物件
    protected void ShowClassTimeTable(string strClassGroupID, DateTime datStartTime)
    {
        //取得當天為星期幾
        string strDay = datStartTime.DayOfWeek.ToString();
        //取得第幾節
        string strPart = "";
        switch (datStartTime.ToString("HH"))
        {
            case "08":
                strPart = "One";
                break;
            case "09":
                strPart = "Two";
                break;
            case "10":
                strPart = "Three";
                break;
            case "11":
                strPart = "Four";
                break;
            case "14":
                strPart = "Five";
                break;
            case "15":
                strPart = "Six";
                break;
            case "16":
                strPart = "Seven";
                break;
            case "17":
                strPart = "Eight";
                break;
            case "18":
                strPart = "Nine";
                break;
            case "19":
                strPart = "A";
                break;
            case "20":
                strPart = "B";
                break;
            case "21":
                strPart = "C";
                break;
        }
        //定義課程名稱Label
        Label lbClassName = new Label();
        lbClassName.Text = clsEditGroup.ORCS_ClassGroup_SELECT_by_ClassGroupID(strClassGroupID).Rows[0]["cClassGroupName"].ToString() + "<br/>";
        //定義課程刪除按鈕
        Button btnDeleteClass = new Button();
        btnDeleteClass.ID = strClassGroupID + "_" + datStartTime.ToString("yyyyMMddHHmmss");
        btnDeleteClass.Text = "刪除";
        btnDeleteClass.CssClass = "ORCS_Exercise_button";
        btnDeleteClass.Width = 80;
        btnDeleteClass.Click += new EventHandler(btnDeleteClass_Click);
        btnDeleteClass.Attributes.Add("onclick", "return confirm('          確定刪除?');");
        //定義下一行符號Lable(避免多個班級擠在一起)
        Label lbNextLine = new Label();
        lbNextLine.Text = "<br/>";
        //將課程名稱Label放進對應的TableCell裡
        ((HtmlTableCell)Master.FindControl("cphContentArea").FindControl("td_" + strDay + "_" + strPart)).Controls.Add(lbClassName);
        //將課程刪除Button放進對應的TableCell裡(學生除外)
        if (ORCSSession.Authority != "s")
        {
            ((HtmlTableCell)Master.FindControl("cphContentArea").FindControl("td_" + strDay + "_" + strPart)).Controls.Add(btnDeleteClass);
            ((HtmlTableCell)Master.FindControl("cphContentArea").FindControl("td_" + strDay + "_" + strPart)).Controls.Add(lbNextLine);
        }
        //定義TableCell顏色
        ((HtmlTableCell)Master.FindControl("cphContentArea").FindControl("td_" + strDay + "_" + strPart)).BgColor = "96dfff";
    }
    //變換選擇到星期幾的顏色
    protected void SelectDayOfColor()
    {
        switch (calClassTimeTable.SelectedDate.DayOfWeek)
        {
            case DayOfWeek.Monday:
                tdMonday.BgColor = "Lightgreen";
                break;
            case DayOfWeek.Tuesday:
                tdTuesday.BgColor = "Lightgreen";
                break;
            case DayOfWeek.Wednesday:
                tdWednesday.BgColor = "Lightgreen";
                break;
            case DayOfWeek.Thursday:
                tdThursday.BgColor = "Lightgreen";
                break;
            case DayOfWeek.Friday:
                tdFriday.BgColor = "Lightgreen";
                break;
            case DayOfWeek.Saturday:
                tdSaturday.BgColor = "Lightgreen";
                break;
            case DayOfWeek.Sunday:
                tdSunday.BgColor = "Lightgreen";
                break;
        }
    }
    //行事曆「縮小」按鈕事件
    protected void ibtnColl_Click(object sender, ImageClickEventArgs e)
    {
        //隱藏「縮小」按鈕
        ibtnColl.Visible = false;
        //顯示「展開」按鈕
        ibtnExp.Visible = true;
        //隱藏行事曆
        calClassTimeTable.Visible = false;
        //載入課程行程表
        LoadClassTimeTable();
    }
    //行事曆「展開」按鈕事件
    protected void ibtnExp_Click(object sender, ImageClickEventArgs e)
    {
        //隱藏「展開」按鈕
        ibtnExp.Visible = false;
        //顯示「縮小」按鈕
        ibtnColl.Visible = true;
        //顯示行事曆
        calClassTimeTable.Visible = true;
        //載入課程行程表
        LoadClassTimeTable();
    }
    //「行事曆」選擇不同日期事件
    protected void calClassTimeTable_SelectionChanged(object sender, EventArgs e)
    {
        //載入課程行程表
        LoadClassTimeTable();
    }
    //「刪除」按鈕事件
    protected void btnDeleteClass_Click(object sender, EventArgs e)
    {
        //取得刪除課程按鈕
        Button btnDeleteClass = (Button)sender;
        //定義刪除的課程ID
        string strDeleteClassID = btnDeleteClass.ID.Split('_')[0].ToString();
        //定義刪除的課程起始時間
        string strDeleteClassStartTime = btnDeleteClass.ID.Split('_')[1].ToString();
        //刪除課程
        clsClassTimeTable.ORCS_ClassTimeTable_DELETE_by_ClassGroupID_StartTime(strDeleteClassID, strDeleteClassStartTime);
        //載入課程行程表
        LoadClassTimeTable();
    }
}
