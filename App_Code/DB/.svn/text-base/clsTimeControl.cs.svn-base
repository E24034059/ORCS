using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ORCS.DB
{
    /// <summary>
    /// Summary description for clsTimeControl
    /// </summary>
    public class clsTimeControl
    {
        public clsTimeControl()
        {
        }

        #region SELECT

        /// <summary>
        /// 從TimeControl資料表取得資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <returns></returns>
        public static DataTable ORCS_TimeControl_SELECT(object cTimeControlName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM ORCS_TimeControl WHERE cTimeControlName LIKE '" + cTimeControlName + "' ORDER BY iTimeControlID ASC ";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }



        /// <summary>
        /// 從TimeControl資料表取得資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TimeControl_SELECT_TimeConName_GroupID(object cTimeControlName, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM ORCS_TimeControl WHERE cTimeControlName LIKE '" + cTimeControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY iTimeControlID ASC ";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在TimeControl資料表存入資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <param name="cTime"></param>
        /// <returns></returns>
        public static int ORCS_TimeControl_INSERT(object cTimeControlName, object cTime)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_TimeControl(cTimeControlName, cTime) VALUES('" + cTimeControlName + "','" + cTime + "')";
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
        /// 在TimeControl資料表存入資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <param name="cTime"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TimeControl_INSERT_TimeName_Time_GroupID(object cTimeControlName, object cTime, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_TimeControl(cTimeControlName, cTime, iClassGroupID) VALUES('" + cTimeControlName + "','" + cTime + "','" + iClassGroupID + "')";
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
        /// 在TimeControl資料表更新資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <param name="cTime"></param>
        /// <returns></returns>
        public static int ORCS_TimeControl_UPDATE(object cTimeControlName, object cTime)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_TimeControl SET cTime = '" + cTime + "' WHERE cTimeControlName LIKE '" + cTimeControlName + "'";
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
        /// 在TimeControl資料表更新資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <param name="cTime"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TimeControl_UPDATE_TimeName_Time_GroupID(object cTimeControlName, object cTime, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_TimeControl SET cTime = '" + cTime + "' WHERE cTimeControlName LIKE '" + cTimeControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
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
        /// 在TimeControl資料表刪除資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <returns></returns>
        public static int ORCS_TimeControl_DELETE(object cTimeControlName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_TimeControl WHERE cTimeControlName LIKE '" + cTimeControlName + "'";
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
        /// 在TimeControl資料表刪除資料
        /// </summary>
        /// <param name="cTimeControlName"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TimeControl_DELETE_TimeConName_GroupID(object cTimeControlName, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_TimeControl WHERE cTimeControlName LIKE '" + cTimeControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
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