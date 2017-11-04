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
using ORCS.DB.Administrator;

namespace ORCS.DB.Attendance
{
    /// <summary>
    /// Summary description for clsAttendanceRecord
    /// </summary>
    public class clsAttendanceRecord
    {
        public clsAttendanceRecord()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region SELECT

        /// <summary>
        /// 根據ClassGroupID和UserID取得學生參與課程最後一次上課記錄
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable getLastAttendanceRecord(object iClassGroupID,object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAttRecord = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendanceRecord WHERE iClassGroupID = '" + iClassGroupID + "' AND cUserID ='" + cUserID + "' ORDER BY cAttendanceTime DESC";
            dtStudentAttRecord = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAttRecord;
        }


        /// <summary>
        /// 在該堂課有無上課點名記錄
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static bool hasAttendanceRecordInClass(object iClassGroupID, object cUserID, object cStartTime)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAttRecord = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendanceRecord WHERE iClassGroupID = '" + iClassGroupID + "' AND cUserID ='" + cUserID + "' AND cAttendanceTime >= '" + cStartTime + "' ORDER BY cAttendanceTime DESC";
            if (ORCSDB.GetDataSet(strSQL).Tables[0].Rows.Count > 0)
                return true;
            return false;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 當登入或重新簽到會增加學生出席記錄
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cUserID"></param>
        /// <param name="cAttendanceTime"></param>
        /// <param name="iAttendanceCond"></param>
        /// <returns></returns>
        public static int addStudentAttendanceRecord(object iClassGroupID, object cUserID, object cAttendanceTime, object iAttendanceCond, object iGroupMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            //取得該堂課程最後堂次
            DataTable dtClassTimeRecord = clsClassTimeRecord.getLastClassTimeRecord(iClassGroupID);
            string strSQL = "INSERT INTO ORCS_StudentAttendanceRecord(iClassGroupID, cUserID,cAttendanceTime,iAttendanceCond,iClassTime,iGroupMode) VALUES('" + iClassGroupID + "','" + cUserID + "','" + cAttendanceTime + "','" + iAttendanceCond + "','" + dtClassTimeRecord.Rows[0]["iClassTime"].ToString() + "','" + iGroupMode + "')";
            try
            {
                ORCSDB.ExecuteNonQuery(strSQL);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        #endregion
    }
}
