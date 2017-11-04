using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ORCS.Util;
using ORCS.DB;
using ORCS.Base;

public partial class Exercise_ModifyExerciseTime : ORCS.ORCS_Page
{
    string strExerciseID = ""; //ExerciseID
    string strEndTime = ""; //EndTime
    string strGroupID = ""; //GroupID
    //載入頁面事件
    protected void Page_Load(object sender, EventArgs e)
    {
        //抓取URL參數
        if (Request.QueryString["cExerciseID"] != null)
        {
            strExerciseID = Request.QueryString["cExerciseID"].ToString();  //ExerciseID
        }
        if (Request.QueryString["cEndTime"] != null)
        {
            strEndTime = Request.QueryString["cEndTime"].ToString();        //EndTime
        }
        if (Request.QueryString["cGroupID"] != null)
        {
            strGroupID = Request.QueryString["cGroupID"].ToString();        //GroupID
        }
        //作答剩餘時間
        TimeRemnant();
    }
    //目前剩餘時間Timer更新事件
    protected void timRemnantTime_Tick(object sender, EventArgs e)
    {
        //作答剩餘時間
        TimeRemnant();
    }
    //作答剩餘時間
    protected void TimeRemnant()
    {
        if (strEndTime != "")
        {
            TimeSpan tsTimeRemnant = clsTimeConvert.DBTimeToDateTime(strEndTime) - DateTime.Now;
            int intTestDuration = Convert.ToInt32(tsTimeRemnant.TotalSeconds);
            //檢查是否到達作答時間
            if (intTestDuration >= 0)
            {
                if (intTestDuration > 60)
                {
                    int intHour = Convert.ToInt32(intTestDuration / 3600);
                    int intTempMinut = Convert.ToInt32(intTestDuration % 3600);
                    int intMinut = Convert.ToInt32(intTempMinut / 60);
                    int intSecond = Convert.ToInt32(intTempMinut % 60);
                    string strHour = "00";
                    string strMinute = "00";
                    string strSecond = "00";
                    if (intHour < 10)
                    {
                        strHour = "0" + intHour.ToString();
                    }
                    else
                    {
                        strHour = intHour.ToString();
                    }
                    if (intMinut < 10)
                    {
                        strMinute = "0" + intMinut.ToString();
                    }
                    else
                    {
                        strMinute = intMinut.ToString();
                    }
                    if (intSecond < 10)
                    {
                        strSecond = "0" + intSecond.ToString();
                    }
                    else
                    {
                        strSecond = intSecond.ToString();
                    }
                    string strDuration = strHour + "'" + strMinute + "'" + strSecond;
                    lbTimeRemnant.Text = strDuration;
                }
                else
                {
                    string strDuration = "00'00'" + intTestDuration.ToString();
                    lbTimeRemnant.Text = strDuration;
                }
            }
        }
    }
    //「確定」按鈕事件
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //取出作答時間
        DataTable dtExerciseQues = clsSystemControl.ORCS_SystemControl_SELECT_SysName_GroupID(strExerciseID, strGroupID);
        //增加作答時間
        if (dtExerciseQues.Rows.Count > 0)
        {
            if (dtExerciseQues.Rows[0]["cEndTime"].ToString() != "")
            {   
                DateTime dtEndtime = clsTimeConvert.DBTimeToDateTime(dtExerciseQues.Rows[0]["cEndTime"].ToString());
                if(dtEndtime.CompareTo(DateTime.Now) >= 0)
                {
                    dtEndtime = clsTimeConvert.DBTimeToDateTime(dtExerciseQues.Rows[0]["cEndTime"].ToString()).AddMinutes(Convert.ToDouble(ddlExerciseTime_Min.SelectedItem.Value)).AddSeconds(Convert.ToDouble(ddlExerciseTime_Sec.SelectedItem.Value));
                }else
                {
                    dtEndtime = DateTime.Now.AddMinutes(Convert.ToDouble(ddlExerciseTime_Min.SelectedItem.Value)).AddSeconds(Convert.ToDouble(ddlExerciseTime_Sec.SelectedItem.Value));
                }

                string strNewEndTime = dtEndtime.ToString("yyyyMMddHHmmss");
                //更新作答時間
                clsSystemControl.ORCS_SystemControl_UPDATE_SysName_EndTime_GroupID(strExerciseID, strNewEndTime, strGroupID);

                //更新作答題目參數 改成開放作答
                clsSystemControl.ORCS_SystemControl_UPDATE_SysName_Param_GroupID(Session["strExerciseID"].ToString(), "1", strGroupID);

            }
        }
        Response.Write("<script>window.close()</script>");
    }
}