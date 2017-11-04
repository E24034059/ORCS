using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ORCS.DB;
using ORCS.DB.Attendance;

namespace ORCS.Base
{
    /// <summary>
    /// Summary description for clsTimeConvert
    /// </summary>
    public class clsTimeConvert
    {
        public clsTimeConvert()
        {
        }
        //將資料庫的時間轉成DateTime
        public static DateTime DBTimeToDateTime(string strDBTime)
        {
            string strYear = strDBTime.Substring(0, 4);    //年
            string strMonth = strDBTime.Substring(4, 2);   //月
            string strDay = strDBTime.Substring(6, 2);     //日
            string strHour = strDBTime.Substring(8, 2);    //時
            string strMinute = strDBTime.Substring(10, 2); //分
            string strSecond = strDBTime.Substring(12, 2); //秒
            //取得DateTime格式
            string strDateTimeFormat = strYear + "-" + strMonth + "-" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond;
            //回傳DateTime
            return Convert.ToDateTime(strDateTimeFormat);
        }
        //回傳遲到多少時間(分鐘)
        public static string TotalLateTime(string cUserID,string cCourseID)
        {
            DataTable dtAttendance = clsAttendance.ORCS_StudentAttendance_SELECT_by_UserID(cUserID);
            DataTable dtTimeControl = clsTimeControl.ORCS_TimeControl_SELECT_TimeConName_GroupID("LateTime", cCourseID);
            //將學生登入時間減掉遲到時間
            TimeSpan tsTotalLateTime = DBTimeToDateTime(dtAttendance.Rows[0]["cAttendanceTime"].ToString()).Subtract(DBTimeToDateTime(dtTimeControl.Rows[0]["cTime"].ToString()));
            //回傳遲到多少分鐘(若為0分鐘則顯示1分鐘)
            string strTotalLateTime = tsTotalLateTime.TotalMinutes.ToString("0");
            if (strTotalLateTime == "0")
                return "1";
            else
                return strTotalLateTime;
        }
        //判斷某天的日期是屬於當年的第幾周，並回傳當周的起始日期和結束日期(目前定義星期一為起始日)
        public static string DayOfWeekStartEnd(DateTime datDateTime)
        {
            //定義年
            int dYear = Convert.ToInt32(datDateTime.Year.ToString());
            //計算當天日期是屬於當年的第幾周
            double dWeeks = System.Math.Floor((double)((datDateTime.DayOfYear) + (Convert.ToInt32(Convert.ToDateTime(datDateTime.Year.ToString() + "-1-1").DayOfWeek)) - 2) / 7) + 1;
            //定義當年的第一天
            DateTime datFirstDay = new DateTime(dYear, 1, 1);
            //定義偏移值
            //計算當年起始日為星期幾
            double dAdd = 0;
            switch (datFirstDay.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dAdd = 0;
                    break;
                case DayOfWeek.Tuesday:
                    dAdd = -1;
                    break;
                case DayOfWeek.Wednesday:
                    dAdd = -2;
                    break;
                case DayOfWeek.Thursday:
                    dAdd = -3;
                    break;
                case DayOfWeek.Friday:
                    dAdd = -4;
                    break;
                case DayOfWeek.Saturday:
                    dAdd = -5;
                    break;
                case DayOfWeek.Sunday:
                    dAdd = -6;
                    break;
            }
            //取得起始日期
            DateTime datStartDate = new DateTime(dYear, 1, 1).AddDays((dWeeks - 1) * 7).AddDays(dAdd);
            //取得結束日期
            DateTime datEndDate = new DateTime(dYear, 1, 1).AddDays((dWeeks * 7) - 1).AddDays(dAdd);
            //回傳起始和結束日期
            return datStartDate.ToShortDateString() + "_" + datEndDate.ToShortDateString();
        }

        /// <summary>
        /// 此函式的目的是將傳入的資料庫時間字串轉換成DateTime的DataType的時間隔式
        /// </summary>
        /// <param name="PreTime">欲轉換成DateTime資料型態的時間字串</param>
        /// <returns></returns>
        public static DateTime SetDateTime(string PreTime)
        {
            DateTime ReturnTime = Convert.ToDateTime(clsTimeConvert.SetDateTimeString(PreTime));
            return ReturnTime;
        }

        /// <summary>
        /// 此函式的目的是將傳入的資料庫時間字串轉換成轉換成DateTime所能接受的時間字串
        /// </summary>
        /// <param name="PreTime">欲轉換成DateTime所能接受的時間字串</param>
        /// <returns></returns>
        public static string SetDateTimeString(string PreTime)
        {
            PreTime = PreTime.Replace("/", "").Replace(":", "").Replace(" ", "");
            while (PreTime.Length < 14)
            {
                PreTime = PreTime + "0";
            }
            char[] temp;
            temp = PreTime.ToCharArray();
            string strDateTime, strYear, strMonth, strDate, strHour, strMin, strSec = "";
            strYear = temp[0].ToString() + temp[1].ToString() + temp[2].ToString() + temp[3].ToString();
            strMonth = temp[4].ToString() + temp[5].ToString();
            strDate = temp[6].ToString() + temp[7].ToString();
            strHour = temp[8].ToString() + temp[9].ToString();
            if (strHour.Trim() == "")
            {
                strHour = "00";
            }
            strMin = temp[10].ToString() + temp[11].ToString();
            if (strMin.Trim() == "")
            {
                strMin = "00";
            }
            strSec = temp[12].ToString() + temp[13].ToString();
            if (strSec.Trim() == "")
            {
                strSec = "00";
            }
            if (strHour == "24")
            {
                strHour = "23";
                strMin = "59";
                strSec = "59";
            }
            strDateTime = strYear + "/" + strMonth + "/" + strDate + " " + strHour + ":" + strMin + ":" + strSec;
            return strDateTime;
        }
    }
}