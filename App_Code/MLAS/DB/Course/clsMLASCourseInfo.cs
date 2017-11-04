using System.Data;

namespace MLAS.DB.Course
{
    /// <summary>
    /// clsMLASCourse 的摘要描述
    /// </summary>
    public class clsMLASCourseInfo
    {
        public clsMLASCourseInfo()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 根據MLASCourseID取得ORCSCourseID
        /// </summary>
        /// <param name="MLASCourseID"></param>
        /// <returns></returns>
        public static string GetORCSCourseIDByMLASCourseID(string MLASCourseID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cCourseBelongUnitID FROM MLAS_CourseInfo WHERE cCourseID LIKE '" + MLASCourseID + "'";
            DataTable dtMLASCourseInfo = MLASDB.GetDataSet(strSQL).Tables[0];
            if (dtMLASCourseInfo.Rows.Count > 0)
            {
                //所屬ORCS單位路徑
                string[] strORCSUnits = dtMLASCourseInfo.Rows[0]["cCourseBelongUnitID"].ToString().Split('/');
                //回傳ORCS課程ID
                return strORCSUnits[strORCSUnits.Length - 1];
            }
            else
                return "";
        }

        #region SELECT
        /// <summary>
        /// Get data from MLAS_CourseInfo table
        /// </summary>
        /// <param name="cCourseID"></param>
        /// <returns></returns>
        public static DataTable MLASCourse_SELECT(object cCourseID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_CourseInfo WHERE cCourseID LIKE '" + cCourseID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable MLASCourse_SELECT_ALL()
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_CourseInfo";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable MLASCourse_SELECT_byViewMode(object cCourseViewMode)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_CourseInfo WHERE cCourseViewMode LIKE '" + cCourseViewMode + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion

        public static int MLASCourse_INSERT(object cCourseID, object cCourseName, object cCourseViewMode, object cCourseDescription, object cCourseDivision, object cCourseType, object cCourseSubDivision, object cCourseBelongUnitID, object cCourseBelongUnitName)
        {
            string strSQL = "INSERT INTO MLAS_CourseInfo(cCourseID,cCourseName,cCourseViewMode,cCourseDescription,cCourseDivision,cCourseType, cCourseSubDivision, cCourseBelongUnitID, cCourseBelongUnitName) VALUES(@cCourseID,@cCourseName,@cCourseViewMode,@cCourseDescription,@cCourseDivision,@cCourseType,@cCourseSubDivision,@cCourseBelongUnitID,@cCourseBelongUnitName)";
            object[] pList = { cCourseID, cCourseName, cCourseViewMode, cCourseDescription, cCourseDivision, cCourseType, cCourseSubDivision, cCourseBelongUnitID, cCourseBelongUnitName };
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

        public static int MLASCourse_DELETE(object cCourseID)
        {
            string strSQL = "DELETE MLAS_CourseInfo WHERE cCourseID LIKE '" + cCourseID + "'";
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

        public static int MLAS_CourseInfo_UPDATE_cCourseViewMode(object cCourseID, object cCourseViewMode)
        {
            string strSQL = "UPDATE MLAS_CourseInfo SET cCourseViewMode=@cCourseViewMode WHERE cCourseID LIKE @cCourseID";
            object[] pList = { cCourseViewMode, cCourseID };
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
    }
}