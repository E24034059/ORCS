using System.Data;

namespace MLAS.DB.Course
{
    /// <summary>
    /// clsMLASCourseUser 的摘要描述
    /// </summary>
    public class clsMLASCourseUser
    {
        public clsMLASCourseUser()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region SELECT
        public static DataTable MLAS_CourseUser_SELECT(object cCourseID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_CourseUser_SELECT_User(object cCourseID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "' AND cUserID LIKE '" + cUserID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion

        public static int MLAS_CourseUser_INSERT(object cCourseID, object cUserID)
        {
            string strSQL = "INSERT INTO MLAS_CourseUser(cCourseID,cUserID) VALUES(@cCourseID,@cUserID)";
            object[] pList = { cCourseID, cUserID };
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

        public static int MLAS_CourseUser_DELETE(object cCourseID)
        {
            string strSQL = "DELETE MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "'";
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

        public static int MLAS_CourseUser_DELETE_UserID(object cCourseID, object cUserID)
        {
            string strSQL = "DELETE MLAS_CourseUser WHERE cCourseID LIKE '" + cCourseID + "' AND cUserID LIKE '" + cUserID + "'";
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
    }
}