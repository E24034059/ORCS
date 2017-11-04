using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ORCS.Util;

namespace ORCS.DB.Attendance
{
    /// <summary>
    /// Summary description for clsAttendance
    /// </summary>
    public class clsAttendance
    {
        public clsAttendance()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region SELECT

        /// <summary>
        /// 根據UserID取出使用者出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendance_SELECT_by_UserID(object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAtt = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendance WHERE cUserID LIKE '" + cUserID + "'";
            dtStudentAtt = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAtt;
        }

        /// <summary>
        /// 根據UserID取出使用者出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendance_SELECT_by_UserID_iGroupMode(object cUserID, object iGroupMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAtt = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendance WHERE cUserID LIKE '" + cUserID + "' AND iGroupMode LIKE '" + iGroupMode + "'";
            dtStudentAtt = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAtt;
        }

        /// <summary>
        /// 根據IP Address取出使用者出席狀態資料
        /// </summary>
        /// <param name="cIPAddress"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendance_SELECT_by_IPAddress(object cIPAddress)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAtt = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendance WHERE cIPAddress LIKE '" + cIPAddress + "'";
            dtStudentAtt = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAtt;
        }

        /// <summary>
        /// 根據UserID和GroupID取出使用者出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendance_SELECT_by_UserID_GroupID(object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAtt = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendance WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
            dtStudentAtt = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAtt;
        }

        /// <summary>
        /// 根據UserID和GroupID取出使用者出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_StudentAttendance_SELECT_by_UserID_GroupID_GroupMode(object cUserID, object iClassGroupID, object iGroupMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtStudentAtt = new DataTable();
            string strSQL = "SELECT * FROM ORCS_StudentAttendance WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' AND iGroupMode = '" + iGroupMode + "'";
            dtStudentAtt = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtStudentAtt;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在StudentAttendance資料表存入資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iAttendanceCond"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendance_INSERT(object cUserID, object iAttendanceCond)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_StudentAttendance(cUserID, iAttendanceCond) VALUES('" + cUserID + "','" + iAttendanceCond + "')";
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

        /// <summary>
        /// 在StudentAttendance資料表存入資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iAttendanceCond"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendance_INSERT_UserID_AttCond_GroupID_iGroupMode(object cUserID, object iAttendanceCond, object iClassGroupID , object iGroupMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_StudentAttendance(cUserID, iAttendanceCond, iClassGroupID , iGroupMode) VALUES('" + cUserID + "','" + iAttendanceCond + "','" + iClassGroupID + "','" + iGroupMode + "')";
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

        /// <summary>
        /// 在StudentAttendanceHistory資料表存入資料
        /// </summary>
        /// <param name="cAttendanceID"></param>
        /// <param name="cUserID"></param>
        /// <param name="iAttendanceCond"></param>
        /// <param name="cAttendanceTime"></param>
        /// <param name="cIPAddress"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendanceHistory_INSERT(object cAttendanceID, object cUserID, object iAttendanceCond, object cAttendanceTime, object cIPAddress, object iClassGroupID, object iGroupMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_StudentAttendanceHistory(cAttendanceID, cUserID, iAttendanceCond, cAttendanceTime, cIPAddress, iClassGroupID, iGroupMode) VALUES('" + cAttendanceID + "','" + cUserID + "','" + iAttendanceCond + "','" + cAttendanceTime + "','" + cIPAddress + "','" + iClassGroupID + "','" + iGroupMode + "')";
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

        #region UPDATE

        /// <summary>
        /// 更新學生出席狀態
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iAttendanceCond"></param>
        /// <param name="cAttendanceTime"></param>
        /// <param name="cIPAddress"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendance_UPDATE(object cUserID, object iAttendanceCond, object cAttendanceTime, object cIPAddress, object iClassGroupID, object iGroupMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_StudentAttendance SET iAttendanceCond = '" + iAttendanceCond + "', cAttendanceTime = '" + cAttendanceTime + "', cIPAddress = '" + cIPAddress + "' WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' AND iGroupMode LIKE '" + iGroupMode + "'";
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

        /// <summary>
        /// 更新學生出席狀態,由於是自動檢查學生狀態無法得知該學生IP位址
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iAttendanceCond"></param>
        /// <param name="cAttendanceTime"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendance_WithoutIPAddress_UPDATE(object cUserID, object iAttendanceCond, object cAttendanceTime, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_StudentAttendance SET iAttendanceCond = '" + iAttendanceCond + "', cAttendanceTime = '" + cAttendanceTime + "' WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
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

        #region DELETE

        /// <summary>
        /// 根據UserID刪除使用者出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendance_DELETE_by_UserID(object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_StudentAttendance WHERE cUserID LIKE '" + cUserID + "'";
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

        /// <summary>
        /// 根據UserID和GroupID刪除使用者出席狀態資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_StudentAttendance_DELETE_by_UserID_GroupID(object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_StudentAttendance WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
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