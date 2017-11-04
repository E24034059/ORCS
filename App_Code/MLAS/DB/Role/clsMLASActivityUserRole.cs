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
namespace MLAS.DB.Role
{
    /// <summary>
    /// clsMLASActivityUserRole 的摘要描述
    /// </summary>
    public class clsMLASActivityUserRole
    {
        public clsMLASActivityUserRole()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        #region SELECT
        public static DataTable MLAS_ActivityUserRole_SELECT(object cActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityUserRole WHERE cActivityID='" + cActivityID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_ActivityUserRole_SELECT_cRoleID(object cRoleID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityUserRole WHERE cRoleID='" + cRoleID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_ActivityUserRole_SELECT_cActivityID_cRoleID(object cActivityID, object cRoleID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityUserRole WHERE cActivityID='" + cActivityID + "'AND cRoleID='" + cRoleID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_ActivityUserRole_SELECT_cActivityID_cRoleID_UserID(object cActivityID, object cRoleID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityUserRole WHERE cActivityID='" + cActivityID + "'AND cRoleID='" + cRoleID + "'AND cUserID='" + cUserID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_ActivityUserRole_SELECT_cActivityID_UserID(object cActivityID, object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityUserRole WHERE cActivityID='" + cActivityID + "'AND cUserID='" + cUserID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion

        #region INSERT
        public static int MLAS_ActivityUserRole_Insert(object cActivityID, object cRoleID, object cUserID)
        {
            string strSQL = "INSERT INTO MLAS_ActivityUserRole(cActivityID, cRoleID, cUserID) VALUES(@cActivityID, @cRoleID, @cUserID)";
            object[] pList = { cActivityID, cRoleID, cUserID };
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
        public static int MLAS_ActivityUserRole_DELETE(object cActivityID)
        {
            string strSQL = "DELETE MLAS_ActivityUserRole WHERE cActivityID LIKE '" + cActivityID + "'";
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
        public static int MLAS_ActivityUserRole_DELETE_cActivityID_cRoleID(object cActivityID, object cRoleID)
        {
            string strSQL = "DELETE MLAS_ActivityUserRole WHERE cActivityID LIKE '" + cActivityID + "'AND cRoleID='" + cRoleID + "'";
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
