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

using ORCS.Util;

namespace ORCS.DB.Exercise
{
    /// <summary>
    /// Summary description for clsExerciseHistory
    /// </summary>
    public class clsExerciseHistory
    {
        public clsExerciseHistory()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region SELECT

        /// <summary>
        /// 根據UserID和ClassGroupID取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_UserID_GroupID(object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseConditionHistory WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseCondID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        /// <summary>
        /// 根據ClassGroupID取出唯一作答上課時段資料
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_DISTINCT_GroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT DISTINCT cExerciseCondID FROM ORCS_ExerciseConditionHistory WHERE iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseCondID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        /// <summary>
        /// 根據UserID和ClassGroupID取出唯一作答上課時段資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_DISTINCT_UserID_GroupID(object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT DISTINCT cExerciseCondID FROM ORCS_ExerciseConditionHistory WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseCondID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        /// <summary>
        /// 根據ExerciseCondID和ClassGroupID取出使用者資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_ExerciseCondID_GroupID(object cExerciseCondID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseConditionHistory WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseCondID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        /// <summary>
        /// 根據ExerciseCondID和ClassGroupID取出唯一習題資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_DISTINCT_ExerciseCondID_GroupID(object cExerciseCondID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT DISTINCT cExerciseID FROM ORCS_ExerciseConditionHistory WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        /// <summary>
        /// 根據ExerciseCondID、ExerciseID和ClassGroupID取出使用者資料
        /// </summary>
        /// <param name="ExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_ExerciseCondID_ExerciseID_GroupID(object cExerciseCondID, object cExerciseID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseConditionHistory WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cExerciseID LIKE '" + cExerciseID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseCondID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        /// <summary>
        /// 根據ExerciseCondID、UserID和ClassGroupID取出使用者資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseConditionHistory_SELECT_by_ExerciseCondID_cUserID_GroupID(object cExerciseCondID, object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCondHis = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseConditionHistory WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY cExerciseID ASC ";
            dtExerciseCondHis = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCondHis;
        }

        #endregion

    }
}