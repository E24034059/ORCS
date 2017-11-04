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
using ORCS.Base;


namespace ORCS.DB.Administrator
{
    /// <summary>
    /// Summary description for clsClassTimeTable
    /// </summary>
    public class clsClassTimeRecord
    {
        public clsClassTimeRecord()
        {
        //
		// TODO: Add constructor logic here
		//
        }

        #region SELECT

        /// <summary>
        /// 取得該堂課最後一筆課堂記錄
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable getLastClassTimeRecord(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtClassTimeRecord = new DataTable();
            string strCheckClassSQL = "SELECT TOP 1 * FROM ORCS_ClassTimeRecord WHERE  iClassGroupID = " + iClassGroupID + " ORDER BY iclassTime DESC";
            dtClassTimeRecord = ORCSDB.GetDataSet(strCheckClassSQL).Tables[0];
            return dtClassTimeRecord;
        }

        /// <summary>
        /// 取得最後重新點名時間
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DateTime getReRollCallTime(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strCheckClassSQL = "SELECT TOP 1 * FROM ORCS_ClassTimeRecord WHERE  iClassGroupID = " + iClassGroupID + " ORDER BY iclassTime DESC";
            //最後點名時間
            DateTime reRollCallTime = clsTimeConvert.DBTimeToDateTime(ORCSDB.GetDataSet(strCheckClassSQL).Tables[0].Rows[0]["cReRollCallTime"].ToString());
            return reRollCallTime;
        }

        #endregion

        #region INSERT
        /// <summary>
        /// 增加上課初始時間記錄
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cStartTime"></param>
        /// <returns></returns>
        public static int addInintialClassTimeRecord(object iClassGroupID, object cStartTime)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            //取得該堂課最新一筆記錄
            DataTable dtClassTimeRecord = getLastClassTimeRecord(iClassGroupID);
            //如果沒有上課記錄代表第一次上課
            if (dtClassTimeRecord.Rows.Count == 0)
            {
                string strSQL = "INSERT INTO ORCS_ClassTimeRecord(iClassGroupID,cStartTime,iClassTime,bIsReRollCall) VALUES('" + iClassGroupID + "','" + cStartTime + "','" + "1" + "','" +"0"+ "')";
                try
                {
                    ORCSDB.ExecuteNonQuery(strSQL);
                }
                catch
                {
                    return -1;
                }
            }
            else //以最後一堂課的堂次再加1做為新的堂次
            {
                string strSQL = "INSERT INTO ORCS_ClassTimeRecord(iClassGroupID,cStartTime,iClassTime,bIsReRollCall) VALUES('" + iClassGroupID + "','" + cStartTime + "','" + (int.Parse(dtClassTimeRecord.Rows[0]["iClassTime"].ToString()) + 1) + "','" + "0" + "')";
                try
                {
                    ORCSDB.ExecuteNonQuery(strSQL);
                }
                catch
                {
                    return -1;
                }
            }
            return 0;
        }
        #endregion

        #region UPDATE


        /// <summary>
        /// 修改該堂課重新點名記錄並且將重新點名狀態改為TRUE
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int updateReRollCallTime(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            //取得該堂課最新一筆記錄
            DataTable dtClassTimeRecord = getLastClassTimeRecord(iClassGroupID);
            string strCheckClassSQL = "UPDATE ORCS_ClassTimeRecord SET cReRollCallTime = '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "',bIsReRollCall= '1' WHERE iClassGroupID= '" + iClassGroupID + "' AND iClassTime = '" + dtClassTimeRecord.Rows[0]["iClassTime"].ToString() + "'";
            //如果沒有上課記錄代表第一次上課
            try
            {
                ORCSDB.ExecuteNonQuery(strCheckClassSQL);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 修改該堂課重新點名記錄並且將重新點名狀態改為TRUE
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int updateReRollCallTimeState(object iClassGroupID, object bIsReRollCall)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            //取得該堂課最新一筆記錄
            DataTable dtClassTimeRecord = getLastClassTimeRecord(iClassGroupID);
            string strCheckClassSQL = "UPDATE ORCS_ClassTimeRecord SET bIsReRollCall= '" + bIsReRollCall + "' WHERE iClassGroupID= '" + iClassGroupID + "' AND iClassTime = '" + dtClassTimeRecord.Rows[0]["iClassTime"].ToString() + "'";
            //如果沒有上課記錄代表第一次上課
            try
            {
                ORCSDB.ExecuteNonQuery(strCheckClassSQL);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 修改下課結束時間記錄
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cStartTime"></param>
        /// <returns></returns>
        public static int updateEndClassTimeRecord(object iClassGroupID, object cEndTime)
        {
            
            clsORCSDB ORCSDB = new clsORCSDB();
            //取得該堂課最新一筆記錄
            DataTable dtClassTimeRecord = getLastClassTimeRecord(iClassGroupID);
            string strCheckClassSQL = "UPDATE ORCS_ClassTimeRecord SET cEndTime = '" + cEndTime + "' WHERE iClassGroupID= '" + iClassGroupID + "' AND iClassTime = '" + dtClassTimeRecord.Rows[0]["iClassTime"].ToString() + "'";
            //如果沒有上課記錄代表第一次上課
            try
            {
                ORCSDB.ExecuteNonQuery(strCheckClassSQL);
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
        /// 從ClassTimeTable刪除群組
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cStartTime"></param>
        /// <returns></returns>
        public static int ORCS_ClassTimeTable_DELETE_by_ClassGroupID_StartTime(object iClassGroupID, object cStartTime)
        {/*
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_ClassTimeTable WHERE iClassGroupID LIKE '" + iClassGroupID + "' AND cStartTime LIKE '" + cStartTime + "'";
            try
            {
                ORCSDB.ExecuteNonQuery(strSQL);
            }
            catch
            {
                return -1;
            }*/
            return 0;
          
        }

        #endregion
    }
}