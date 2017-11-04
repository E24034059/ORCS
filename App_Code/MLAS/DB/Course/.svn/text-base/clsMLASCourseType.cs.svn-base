using System.Data;

namespace MLAS.DB.Course
{
    /// <summary>
    /// clsMLASCourseType 的摘要描述
    /// </summary>
    public class clsMLASCourseType
    {
        public clsMLASCourseType()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        #region SELECT
        /// <summary>
        /// Get data from MLAS_CourseInfo table
        /// </summary>
        /// <param name="cCourseID"></param>
        /// <returns></returns>
        public static DataTable MLAS_CourseType_SELECT_All()
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_CourseType";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion

        #region INSERT
        public static int MLAS_CourseType_INSERT(object cCourseTypeName, object cCourseTypeDescription)
        {
            string strSQL = "INSERT INTO MLAS_CourseType(cCourseTypeName,cCourseTypeDescription) VALUES(@cCourseTypeName,@cCourseTypeDescription)";
            object[] pList = { cCourseTypeName, cCourseTypeDescription };
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

        #region DELETE
        public static int MLAS_CourseType_DELETE(object cCourseTypeName)
        {
            string strSQL = "DELETE MLAS_CourseType WHERE cCourseTypeName LIKE '" + cCourseTypeName + "'";
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
    }
}