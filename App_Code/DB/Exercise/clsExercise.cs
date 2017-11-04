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
    /// Summary description for clsExercise
    /// </summary>
    public class clsExercise
    {
        public clsExercise()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region SELECT

        /// <summary>
        /// 根據題目ID取出使用者資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_cExerciseID(object cExerciseID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE cExerciseID LIKE '" + cExerciseID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據iClassGroupID取出使用者資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_iClassGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據iClassGroupID取該堂課程所有題目ID
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_iClassGroupID_For_QuestionNumber(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT DISTINCT cExerciseID FROM ORCS_ExerciseCondition WHERE iClassGroupID = '" + iClassGroupID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據題目ID和GroupID取出使用者資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_cExerciseID_GroupID(object cExerciseID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE cExerciseID LIKE '" + cExerciseID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據iClassGroupID和iAnswerMode取出使用者資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_iClassGroupID_iAnswerMode(object iClassGroupID, object iAnswerMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE iClassGroupID LIKE '" + iClassGroupID + "' AND iAnswerMode = '" + iAnswerMode + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據題目ID和UserID取出使用者資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_cExerciseID_cUserID(object cExerciseID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據UserID和GroupID取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_cUserID_GroupID(object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }


        /// <summary>
        /// 根據題目ID、UserID和GroupID取出使用者資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ExerciseCondition_SELECT_by_cExerciseID_cUserID_GroupID(object cExerciseID, object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtExerciseCond = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ExerciseCondition WHERE cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
            dtExerciseCond = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtExerciseCond;
        }

        /// <summary>
        /// 根據UserID取出上傳資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_FileUploadData_SELECT_by_ClassGroupID_UserID(object iClassGroupID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtFileUploadData = new DataTable();
            string strSQL = "SELECT * FROM ORCS_FileUploadData WHERE iClassGroupID LIKE '" + iClassGroupID + "' AND cUserID LIKE '" + cUserID + "'";
            dtFileUploadData = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtFileUploadData;
        }

        /// <summary>
        /// 根據ExerciseCondID和UserID取出上傳資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_FileUploadData_SELECT_by_ExerciseCondID_UserID(object cExerciseCondID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtFileUploadData = new DataTable();
            string strSQL = "SELECT * FROM ORCS_FileUploadData WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cUserID LIKE '" + cUserID + "'";
            dtFileUploadData = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtFileUploadData;
        }

        /// <summary>
        /// 根據ExerciseCondID、ExerciseID和UserID取出上傳資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_FileUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(object cExerciseCondID, object cExerciseID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtFileUploadData = new DataTable();
            string strSQL = "SELECT * FROM ORCS_FileUploadData WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "'";
            dtFileUploadData = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtFileUploadData;
        }

        /// <summary>
        /// 根據ExerciseCondID、ExerciseID和UserID取出上傳資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TextUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(object cExerciseCondID, object cExerciseID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtFileUploadData = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TextUploadData WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "'";
            dtFileUploadData = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtFileUploadData;
        }

        /// <summary>
        /// 根據UserID取出上傳資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TextUploadData_SELECT_by_ClassGroupID_UserID(object iClassGroupID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtFileUploadData = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TextUploadData WHERE iClassGroupID LIKE '" + iClassGroupID + "' AND cUserID LIKE '" + cUserID + "'";
            dtFileUploadData = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtFileUploadData;
        }

        /// <summary>
        /// 根據ExerciseCondID和UserID取出上傳資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TextUploadData_SELECT_by_ExerciseCondID_UserID(object cExerciseCondID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtFileUploadData = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TextUploadData WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cUserID LIKE '" + cUserID + "'";
            dtFileUploadData = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtFileUploadData;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在ExerciseCondition資料表存入資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <returns></returns>
        public static int ORCS_ExerciseCondition_INSERT(object cExerciseID, object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_ExerciseCondition(cExerciseID, cUserID) VALUES('" + cExerciseID + "','" + cUserID + "')";
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
        /// 在ExerciseCondition資料表存入資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_ExerciseCondition_INSERT_ExerciseID_UserID_GroupID(object cExerciseID, object cUserID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_ExerciseCondition(cExerciseID, cUserID, iClassGroupID) VALUES('" + cExerciseID + "','" + cUserID + "','" + iClassGroupID + "')";
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
        /// 在ExerciseConditionHistory資料表存入資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="iExerciseState"></param>
        /// <param name="iClassGroupID"></param>
        /// <param name="cCaseID"></param>
        /// <param name="cPaperID"></param>
        /// <returns></returns>
        public static int ORCS_ExerciseConditionHistory_INSERT(object cExerciseCondID, object cExerciseID, object cUserID, object iExerciseState, object iClassGroupID, object cCaseID, object cPaperID, object iAnswerMode)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_ExerciseConditionHistory(cExerciseCondID, cExerciseID, cUserID, iExerciseState, iClassGroupID, cCaseID, cPaperID, iAnswerMode) VALUES('" + cExerciseCondID + "','" + cExerciseID + "','" + cUserID + "','" + iExerciseState + "','" + iClassGroupID + "','" + cCaseID + "','" + cPaperID + "','" + iAnswerMode + "')";
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
        /// 在ORCS_FileUploadData資料表存入資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="cFileName"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_FileUploadData_INSERT(object cExerciseCondID, object cExerciseID, object cUserID, object cFileName, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_FileUploadData(cExerciseCondID, cExerciseID, cUserID, cFileName, iClassGroupID) VALUES('" + cExerciseCondID + "','" + cExerciseID + "','" + cUserID + "','" + cFileName + "','" + iClassGroupID + "')";
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
        /// 在ORCS_TextUploadData資料表存入資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="cFileName"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TextUploadData_INSERT(object cExerciseCondID, object cExerciseID, object cUserID, object cTextContent, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_TextUploadData(cExerciseCondID, cExerciseID, cUserID, txtTextContent, iClassGroupID) VALUES('" + cExerciseCondID + "','" + cExerciseID + "','" + cUserID + "','" + cTextContent + "','" + iClassGroupID + "')";
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
        /// 更新學生作答狀態
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="iExerciseState"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_ExerciseCondition_UPDATE(object cExerciseID, object cUserID, object iExerciseState, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_ExerciseCondition SET iExerciseState = '" + iExerciseState + "' WHERE cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
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
        /// 更新學生上傳作品檔案資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="cFileName"></param>
        /// <returns></returns>
        public static int ORCS_FileUploadData_UPDATE(object cExerciseCondID, object cExerciseID, object cUserID, object cFileName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_FileUploadData SET cFileName = '" + cFileName + "' WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "'";
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
        /// 更新學生上傳作品檔案資料
        /// </summary>
        /// <param name="cExerciseCondID"></param>
        /// <param name="cExerciseID"></param>
        /// <param name="cUserID"></param>
        /// <param name="cFileName"></param>
        /// <returns></returns>
        public static int ORCS_TextUploadData_UPDATE(object cExerciseCondID, object cExerciseID, object cUserID, object cTextContent)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_TextUploadData SET txtTextContent = '" + cTextContent + "' WHERE cExerciseCondID LIKE '" + cExerciseCondID + "' AND cExerciseID LIKE '" + cExerciseID + "' AND cUserID LIKE '" + cUserID + "'";
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
        /// 根據ExerciseID刪除使用者出席狀態資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <returns></returns>
        public static int ORCS_ExerciseCondition_DELETE_by_cExerciseID(object cExerciseID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_ExerciseCondition WHERE cExerciseID LIKE '" + cExerciseID + "'";
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
        /// 根據ExerciseID和GroupID刪除使用者出席狀態資料
        /// </summary>
        /// <param name="cExerciseID"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_ExerciseCondition_DELETE_by_cExerciseID_GroupID(object cExerciseID, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_ExerciseCondition WHERE cExerciseID LIKE '" + cExerciseID + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
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