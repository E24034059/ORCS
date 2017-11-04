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
namespace MLAS.DB.SeriesActivity
{
    /// <summary>
    /// clsMLASSeriesActivityUser 的摘要描述
    /// </summary>
    public class clsMLASSeriesActivityUser
    {
        public clsMLASSeriesActivityUser()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region SELECT
        public static DataTable MLAS_SeriesActivityUser_Select(object cSeriesActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static string MLAS_SeriesActivityUser_Select_GroupName(object cSeriesActivityID, object cSeriesActivityUserGroupID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cSeriesActivityUserGroupName FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cSeriesActivityUserGroupID LIKE '" + cSeriesActivityUserGroupID + "'";
            DataTable dt = MLASDB.GetDataSet(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["cSeriesActivityUserGroupName"].ToString();
            else
                return "";
        }
        public static DataTable MLAS_SeriesActivityUser_Select_group_DISTINCT(object cSeriesActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT DISTINCT cSeriesActivityUserGroupID FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' ORDER BY cSeriesActivityUserGroupID ASC ";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_SeriesActivityUser_Select_group_DISTINCT_cSeriesActivityUserGroup(object cSeriesActivityID, object cSeriesActivityUserGroupID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT DISTINCT cSeriesActivityUserGroupID FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'AND cSeriesActivityUserGroupID NOT LIKE '" + cSeriesActivityUserGroupID + "' ORDER BY cSeriesActivityUserGroupID ASC ";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_SeriesActivityUser_Select_group_specific(object cSeriesActivityID, object cSeriesActivityUserGroupID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cSeriesActivityUserGroupID LIKE '" + cSeriesActivityUserGroupID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_SeriesActivityUser_Select_user(object cSeriesActivityID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cUserID LIKE '" + cUserID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_SeriesActivityUser_Select_by_cSeriesActivityID_cSeriesActivityUserGroupID(object cSeriesActivityID, object cSeriesActivityUserGroupID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cSeriesActivityUserGroupID LIKE '" + cSeriesActivityUserGroupID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static string MLAS_SeriesActivityUser_Select_SeriesActivityUserGroupID(object cSeriesActivityID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT cSeriesActivityUserGroupID FROM MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cUserID LIKE '" + cUserID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0].Rows[0]["cSeriesActivityUserGroupID"].ToString();
        }
        public static DataTable MLAS_SeriesActivityUserJOINMLAS_SeriesActivityUser(object cSeriesActivityID, object cActivityID, object cSeriesActivityUserGroupID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT MLAS_SeriesActivityUser.cSeriesActivityID, MLAS_ActivityUserRole.cActivityID, MLAS_ActivityUserRole.cUserID, " +
                " MLAS_SeriesActivityUser.cSeriesActivityUserGroupID, MLAS_SeriesActivityUser.cSeriesActivityUserGroupName " +
                " FROM MLAS_ActivityUserRole INNER JOIN MLAS_SeriesActivityUser ON MLAS_ActivityUserRole.cUserID = MLAS_SeriesActivityUser.cUserID " +
                " WHERE (MLAS_SeriesActivityUser.cSeriesActivityID = '" + cSeriesActivityID + "') AND " +
                " (MLAS_ActivityUserRole.cActivityID = '" + cActivityID + "') AND " +
                " (MLAS_SeriesActivityUser.cSeriesActivityUserGroupID = '" + cSeriesActivityUserGroupID + "')";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 取得分組的組別資料
        /// </summary>
        /// <param name="cSeriesActivityID"></param>
        /// <param name="cActivityID"></param>
        /// <returns></returns>
        public static DataTable MLAS_SeriesActivityUserJOINMLAS_SeriesActivityUser_DISTINCT_Group(object cSeriesActivityID, object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT DISTINCT dbo.MLAS_SeriesActivityUser.cSeriesActivityUserGroupID " +
                " FROM dbo.MLAS_ActivityUserRole INNER JOIN dbo.MLAS_SeriesActivityUser ON dbo.MLAS_ActivityUserRole.cUserID = dbo.MLAS_SeriesActivityUser.cUserID " +
                " WHERE (dbo.MLAS_SeriesActivityUser.cSeriesActivityID = '" + cSeriesActivityID + "') AND (dbo.MLAS_ActivityUserRole.cActivityID = '" + cActivityID + "') ";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        /// <summary>
        /// 根據使用者ID取得組別的ID
        /// </summary>
        /// <param name="cSeriesActivityID"></param>
        /// <param name="cActivityID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable MLAS_SeriesActivityUserJOINMLAS_SeriesActivityUser_SELECT_GroupID(object cSeriesActivityID, object cActivityID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT DISTINCT dbo.MLAS_SeriesActivityUser.cSeriesActivityUserGroupID " +
                " FROM dbo.MLAS_ActivityUserRole INNER JOIN dbo.MLAS_SeriesActivityUser ON dbo.MLAS_ActivityUserRole.cUserID = dbo.MLAS_SeriesActivityUser.cUserID " +
                " WHERE (dbo.MLAS_SeriesActivityUser.cSeriesActivityID = '" + cSeriesActivityID + "') AND (dbo.MLAS_ActivityUserRole.cActivityID = '" + cActivityID + "') AND (dbo.MLAS_SeriesActivityUser.cUserID = '" + cUserID + "')";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        /// <summary>
        /// 取得分組的組別ID與名稱
        /// </summary>
        /// <param name="cSeriesActivityID"></param>
        /// <param name="cActivityID"></param>
        /// <returns></returns>
        public static DataTable MLAS_SeriesActivityUserJOINMLAS_SeriesActivityUser_SELECT_DISTINCT_Group(object cSeriesActivityID, object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT DISTINCT MLAS_SeriesActivityUser.cSeriesActivityUserGroupID, MLAS_SeriesActivityUser.cSeriesActivityUserGroupName " +
                " FROM MLAS_ActivityUserRole INNER JOIN MLAS_SeriesActivityUser ON MLAS_ActivityUserRole.cUserID = MLAS_SeriesActivityUser.cUserID " +
                " WHERE (MLAS_SeriesActivityUser.cSeriesActivityID = '" + cSeriesActivityID + "') AND " +
                " (MLAS_ActivityUserRole.cActivityID = '" + cActivityID + "') " +
                " ORDER BY MLAS_SeriesActivityUser.cSeriesActivityUserGroupID ASC";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion

        #region INSERT
        public static int MLAS_SeriesActivityUser_INSERT(object cSeriesActivityID, object cUserID, object cSeriesActivityUserGroupID, object cSeriesActivityUserGroupName)
        {
            string strSQL = "INSERT INTO MLAS_SeriesActivityUser(cSeriesActivityID,cUserID,cSeriesActivityUserGroupID,cSeriesActivityUserGroupName)" +
            "VALUES(@cSeriesActivityID,@cUserID,@cSeriesActivityUserGroupID,@cSeriesActivityUserGroupName)";
            object[] pList = { cSeriesActivityID, cUserID, cSeriesActivityUserGroupID, cSeriesActivityUserGroupName };
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

        #region UPDATE
        public static int MLAS_SeriesActivityUser_UPDATE_group(object cSeriesActivityUserGroupID, object cSeriesActivityUserGroupName, object cSeriesActivityID, object cUserID)
        {
            string strSQL = "UPDATE MLAS_SeriesActivityUser SET cSeriesActivityUserGroupID=@cSeriesActivityUserGroupID,cSeriesActivityUserGroupName=@cSeriesActivityUserGroupName WHERE cSeriesActivityID LIKE @cSeriesActivityID AND cUserID LIKE @cUserID";
            object[] pList = { cSeriesActivityUserGroupID, cSeriesActivityUserGroupName, cSeriesActivityID, cUserID };
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
        public static int MLAS_SeriesActivityUser_DELETE_cSeriesActivityID(object cSeriesActivityID)
        {
            string strSQL = "DELETE MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'";
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
        public static int MLAS_SeriesActivityUser_DELETE_cSeriesActivityID_cUserID(object cSeriesActivityID, object cUserID)
        {
            string strSQL = "DELETE MLAS_SeriesActivityUser WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'AND cUserID LIKE '" + cUserID + "'";
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