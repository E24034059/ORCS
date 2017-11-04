using MLAS.Util;
using System.Data;

namespace MLAS.DB.Activity
{
    /// <summary>
    /// clsMLASCourse 的摘要描述
    /// </summary>
    public class clsMLASActivityBasicInfo
    {
        public clsMLASActivityBasicInfo()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region SELECT
        public static DataTable MLAS_ActivityBasicInfo_Select(object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityBasicInfo WHERE cActivityID LIKE '" + cActivityID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static string MLAS_ActivityBasicInfo_Select_cActivityName_BycActivityID(object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cActivityName FROM MLAS_ActivityBasicInfo WHERE cActivityID LIKE '" + cActivityID + "'";
            try
            {
                return MLASDB.ExecuteScalar(strSQL).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static DataTable MLAS_ActivityBasicInfo_Select_Activity_HaveStartDate()
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityBasicInfo WHERE cActivityStartTime NOT LIKE ''";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable MLAS_ActivityBasicInfo_Select_Activity_HaveDueDate()
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityBasicInfo WHERE cActivityEndTime NOT LIKE ''";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable MLAS_ActivityBasicInfo_Select_Activity_ByStartTime(object cActivityStartTime)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityBasicInfo WHERE cActivityStartTime LIKE '" + cActivityStartTime + "%'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable MLAS_ActivityBasicInfo_Select_Activity_ByEndTime(object cActivityEndTime)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityBasicInfo WHERE cActivityEndTime LIKE '" + cActivityEndTime + "%'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static string MLAS_ActivityBasicInfo_Select_ByRequiredActivityID(object cRequiredActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cActivityID FROM MLAS_ActivityBasicInfo WHERE cRequiredActivityID LIKE'" + cRequiredActivityID + "'";
            try
            {
                return MLASDB.ExecuteScalar(strSQL).ToString();
            }
            catch
            {
                return "";
            }
        }
        public static string MLAS_ActivityBasicInfo_Select_ActivityTitle_ByActivityID(object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cActivityTitle FROM MLAS_ActivityBasicInfo WHERE cActivityID LIKE'" + cActivityID + "'";
            try
            {
                return MLASDB.ExecuteScalar(strSQL).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string MLAS_ActivityBasicInfo_Select_cActivityViewMode_BycActivityID(object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cActivityViewMode FROM MLAS_ActivityBasicInfo WHERE cActivityID LIKE '" + cActivityID + "'";
            try
            {
                return MLASDB.ExecuteScalar(strSQL).ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region INSERT
        public static int MLAS_ActivityBasicInfo_Insert(object cActivityID, object cActivityName, object cActivityState)
        {
            string strSQL = "INSERT INTO MLAS_ActivityBasicInfo(cActivityID,cActivityName,cActivityState) VALUES(@cActivityID,@cActivityName,@cActivityState)";
            object[] pList = { cActivityID, cActivityName, cActivityState };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        #endregion

        #region Delete
        public static int MLAS_ActivityBasicInfo_DELETE_cActivityID(object cActivityID)
        {
            string strSQL = "DELETE MLAS_ActivityBasicInfo WHERE cActivityID LIKE '" + cActivityID + "'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        #endregion

        #region UPDATE
        public static int MLAS_ActivityBasicInfo_UPDATE(object cActivityID, object cActivityTitle, object cActivityStartTime, object cActivityEndTime, object cRequiredActivityID, object cActivityStartCondition, object cActivityKeyword)
        {
            string strSQL = "UPDATE MLAS_ActivityBasicInfo SET cActivityTitle=@cActivityTitle, cActivityStartTime=@cActivityStartTime, cActivityEndTime=@cActivityEndTime, cRequiredActivityID=@cRequiredActivityID, cActivityStartCondition=@cActivityStartCondition, cActivityKeyword=@cActivityKeyword WHERE cActivityID LIKE @cActivityID";
            object[] pList = { cActivityTitle, cActivityStartTime, cActivityEndTime, cRequiredActivityID, cActivityStartCondition, cActivityKeyword, cActivityID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLAS_ActivityBasicInfo_UPDATE_ByTemplate(object cActivityID, object cActivityTitle, object cActivityStartTime, object cActivityEndTime, object cRequiredActivityID, object cActivityStartCondition, object cActivityKeyword, object cActivityNext)
        {
            string strSQL = "UPDATE MLAS_ActivityBasicInfo SET cActivityTitle=@cActivityTitle, cActivityStartTime=@cActivityStartTime, cActivityEndTime=@cActivityEndTime, cRequiredActivityID=@cRequiredActivityID, cActivityStartCondition=@cActivityStartCondition, cActivityKeyword=@cActivityKeyword , cActivityNext=@cActivityNext WHERE cActivityID LIKE @cActivityID";
            object[] pList = { cActivityTitle, cActivityStartTime, cActivityEndTime, cRequiredActivityID, cActivityStartCondition, cActivityKeyword, cActivityNext, cActivityID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLAS_ActivityBasicInfo_UPDATE_state(object cActivityID, object cActivityState)
        {
            string strSQL = "UPDATE MLAS_ActivityBasicInfo SET cActivityState=@cActivityState WHERE cActivityID LIKE @cActivityID";
            object[] pList = { cActivityState, cActivityID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLAS_ActivityBasicInfo_UPDATE_StartTime_EndTime(object cActivityID, object cActivityStartTime, object cActivityEndTime)
        {
            string strSQL = "UPDATE MLAS_ActivityBasicInfo SET cActivityStartTime=@cActivityStartTime, cActivityEndTime=@cActivityEndTime WHERE cActivityID LIKE @cActivityID";
            object[] pList = { cActivityStartTime, cActivityEndTime, cActivityID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLAS_ActivityBasicInfo_UPDATE_ActivityViewMode(object cActivityID, object cActivityViewMode)
        {
            string strSQL = "UPDATE MLAS_ActivityBasicInfo SET cActivityViewMode=@cActivityViewMode WHERE cActivityID LIKE @cActivityID";
            object[] pList = { cActivityViewMode, cActivityID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
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