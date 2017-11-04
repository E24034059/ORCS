using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using MLAS.Util;
namespace MLAS.DB.Course
{
    /// <summary>
    /// clsMLASCourse 的摘要描述
    /// </summary>
    public class clsMLASVIEWCourseToActivity
    {
        public clsMLASVIEWCourseToActivity()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region SELECT
        public static DataTable VIEWCourseToActivity_SELECT_ActivityStartDate(object cCourseID, object cActivityStartTime)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseToActivity WHERE cCourseID LIKE '" + cCourseID + "' AND cActivityStartTime LIKE '" + cActivityStartTime + "%'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseToActivity_SELECT_Course(object cCourseID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseToActivity WHERE cCourseID LIKE '" + cCourseID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseToActivity_SELECT_SeriesActivity(object cSeriesActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseToActivity WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseToActivity_SELECT_Activity(object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseToActivity WHERE cActivityID LIKE '" + cActivityID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseToActivity_SELECT_ActivityID(object cSeriesActivityID, object cActivityName)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseToActivity WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cActivityName LIKE '" + cActivityName + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable VIEWCourseToActivity_SELECT_SpecialActivity(object cActivityID, object cActivityName)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM VIEW_MLAS_CourseToActivity WHERE cActivityID LIKE '" + cActivityID + "' AND cActivityName LIKE '%" + cActivityName + "%'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        #endregion

    }
}