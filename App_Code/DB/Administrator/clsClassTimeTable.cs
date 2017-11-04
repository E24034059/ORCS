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

namespace ORCS.DB.Administrator
{
    /// <summary>
    /// Summary description for clsClassTimeTable
    /// </summary>
    public class clsClassTimeTable
    {
        public clsClassTimeTable()
        {
        }

        #region SELECT

        /// <summary>
        /// 從ClassTimeTable取出資料
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ClassTimeTable_SELECT_by_ClassGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtClassTimeTable = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ClassTimeTable WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
            dtClassTimeTable = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtClassTimeTable;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在ClassTimeTable資料表存入資料
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cStartTime"></param>
        /// <param name="cEndTime"></param>
        /// <returns></returns>
        public static int ORCS_ClassTimeTable_INSERT(object iClassGroupID, object cStartTime, object cEndTime)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_ClassTimeTable(iClassGroupID, cStartTime, cEndTime) VALUES('" + iClassGroupID + "','" + cStartTime + "','" + cEndTime + "')";
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
        #endregion

        #region DELETE

        /// <summary>
        /// 從ClassTimeTable刪除群組
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cStartTime"></param>
        /// <returns></returns>
        public static int ORCS_ClassTimeTable_DELETE_by_ClassGroupID_StartTime(object iClassGroupID, object cStartTime)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_ClassTimeTable WHERE iClassGroupID LIKE '" + iClassGroupID + "' AND cStartTime LIKE '" + cStartTime + "'";
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