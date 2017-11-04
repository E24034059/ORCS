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

using ORCS.Util;

namespace ORCS.DB.Attendance
{
    /// <summary>
    /// Summary description for clsAttendanceHistory
    /// </summary>
    public class clsAttendanceHistory
    {
        public clsAttendanceHistory()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region SELECT

        /// <summary>
        /// 根據UserID和ClassGroupID取出歷史出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendanceHistory_SELECT_by_UserID_GroupID(object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAttHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendanceHistory WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cAttendanceID ASC ";
            dtStudentAttHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAttHis;
        }


        /// <summary>
        /// 根據ClassGroupID取出歷史出席狀態資料
        /// </summary>
        /// <param name="cAttendanceID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendanceHistory_SELECT_by_GroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAttHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendanceHistory WHERE iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cAttendanceID ASC ";
            dtStudentAttHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAttHis;
        }

        /// <summary>
        /// 根據AttendanceID和ClassGroupID取出歷史出席狀態資料
        /// </summary>
        /// <param name="cAttendanceID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendanceHistory_SELECT_by_AttendanceID_GroupID(object cAttendanceID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAttHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendanceHistory WHERE cAttendanceID LIKE '" + cAttendanceID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cAttendanceID ASC ";
            dtStudentAttHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAttHis;
        }

        /// <summary>
        /// 根據ClassGroupID取出單一歷史出席狀態資料
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendanceHistory_SELECT_by_DISTINCT_GroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAttHis = new DataTable();
            string strSQL = "SELECT DISTINCT cAttendanceID FROM ORCS_StudentAttendanceHistory WHERE iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cAttendanceID ASC ";
            dtStudentAttHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAttHis;
        }

        #endregion

    }
}