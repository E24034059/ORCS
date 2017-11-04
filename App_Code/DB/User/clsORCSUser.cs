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

namespace ORCS.DB.User
{
    /// <summary>
    /// Summary description for clsORCSUser
    /// </summary>
    public class clsORCSUser
    {
        protected clsORCSDB ORCSDB = new clsORCSDB();
        protected string strSQL = "";

        public clsORCSUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 根據UserID取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static string GetAuthorityByUserID(object cUserID)
        {
            DataTable dtUserInfo = ORCS_User_SELECT_by_UserID(cUserID);
            if (dtUserInfo.Rows.Count > 0)
                return dtUserInfo.Rows[0]["cAuthority"].ToString();
            return null;
        }


        #region SELECT

        /// <summary>
        /// 根據UserID取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_User_SELECT_by_UserID(object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtUserInfo = new DataTable();
            string strSQL = "SELECT * FROM ORCS_User WHERE cUserID LIKE '" + cUserID + "'";
            dtUserInfo = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtUserInfo;
        }

        /// <summary>
        /// 根據使用者身分取出使用者資料
        /// </summary>
        /// <param name="cAuthority"></param>
        /// <returns></returns>
        public static DataTable ORCS_User_SELECT_by_Authority(object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtUserInfo = new DataTable();
            string strSQL = "SELECT * FROM ORCS_User WHERE cAuthority LIKE '" + cAuthority + "'";
            dtUserInfo = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtUserInfo;
        }

        /// <summary>
        /// 根據UserID取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_User_SELECT_ID_Name_by_UserID(object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtUserInfo = new DataTable();
            string strSQL = "SELECT cUserID, cUserName FROM ORCS_User WHERE cUserID LIKE '" + cUserID + "'";
            dtUserInfo = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtUserInfo;
        }

        /// <summary>
        /// 根據UserID和Authority取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="cAuthority"></param>
        /// <returns></returns>
        public static DataTable ORCS_User_SELECT_ID_Name_by_UserID_Authority(object cUserID, object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtUserInfo = new DataTable();
            string strSQL = "SELECT cUserID, cUserName FROM ORCS_User WHERE cUserID LIKE '" + cUserID + "' AND cAuthority LIKE '" + cAuthority + "'";
            dtUserInfo = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtUserInfo;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在User資料表存入資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="cPassword"></param>
        /// <param name="cUserName"></param>
        /// <param name="cAuthority"></param>
        /// <returns></returns>
        public static int ORCS_User_INSERT(object cUserID, object cPassword, object cUserName, object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_User(cUserID, cPassword, cUserName, cAuthority) VALUES('" + cUserID + "','" + cPassword + "','" + cUserName + "','" + cAuthority + "')";
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
        /// <param name="cPassword"></param>
        /// <returns></returns>
        public static int ORCS_User_UPDATE_UserID_Password(object cUserID, object cPassword)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_ORCS SET cPassword = '" + cPassword + "' WHERE cUserID LIKE '" + cUserID + "'";
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
        #endregion

        #region CHECK

        public static bool ORCS_User_CHECK_Login(object cUserID, object cPassword)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM ORCS_User WHERE cUserID = '" + cUserID + "' AND cPassword = '" + cPassword + "'";
            if (ORCSDB.ExecuteDataTable(strSQL).Rows.Count > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}