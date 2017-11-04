using System.Data;

namespace MLAS.DB.Course
{
    /// <summary>
    /// clsMLASCourse 的摘要描述
    /// </summary>
    public class clsMLASVIEWCourseUser
    {
        public clsMLASVIEWCourseUser()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        #region SELECT
        /// <summary>
        /// Get data from VIEWCourseUser table
        /// </summary>
        /// <param name="cCourseID"></param>
        /// <returns></returns>
        public static DataTable VIEWCourseUser_SELECT(object cCourseID, object cAuthority)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "' AND cAuthority LIKE'" + cAuthority + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseUser_SELECT_SingleS(object cCourseID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "' AND cUserID LIKE'" + cUserID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseUser_SELECT(object cCourseID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion

    }
}