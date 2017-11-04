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
    /// Summary description for clsEditGroup
    /// </summary>
    public class clsEditGroup
    {
        public clsEditGroup()
        {
        }

        /// <summary>
        /// 根據課程ID取得課程名稱和課程所屬部門名稱
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable GetClassGroupNameAndSchoolGroupNameByClassGroupID(object iClassID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strSQL = "SELECT OC.cClassGroupName,OS.cSchoolGroupName FROM ORCS_ClassGroup OC INNER JOIN " +
                " ORCS_SchoolGroup OS ON OC.iSchoolGroupID=OS.iSchoolGroupID WHERE OC.iClassGroupID = '" + iClassID + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        #region SELECT

        /// <summary>
        /// 取出所有課程
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_ClassGroup_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtSchoolGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ClassGroup";
            dtSchoolGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtSchoolGroup;
        }

        /// <summary>
        /// 取出所有小組
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtSchoolGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TempGroup";
            dtSchoolGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtSchoolGroup;
        }

        /// <summary>
        /// 根據iSchoolGroupID取出群組
        /// </summary>
        /// <param name="iSchoolGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_SchoolGroup_SELECT_by_SchoolGroupID(object iSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtSchoolGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_SchoolGroup WHERE iSchoolGroupID LIKE '" + iSchoolGroupID + "'";
            dtSchoolGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtSchoolGroup;
        }

        /// <summary>
        /// 根據ORCS_SchoolGroup的iParentNodeID(iSchoolGroupID)取出群組
        /// </summary>
        /// <param name="iParentNodeID"></param>
        /// <returns></returns>
        public static DataTable ORCS_SchoolGroup_SELECT__ChildeNode_by_ParentGroupID(object iParentNodeID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtTempGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_SchoolGroup WHERE iParentNodeID LIKE '" + iParentNodeID + "'";
            dtTempGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtTempGroup;
        }

        /// <summary>
        /// 根據iClassGroupID取出群組
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ClassGroup_SELECT_by_ClassGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtClassGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ClassGroup WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
            dtClassGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtClassGroup;
        }

        /// <summary>
        /// 根據iClassGroupID的ParentGroupID(iSchoolGroupID)取出群組
        /// </summary>
        /// <param name="iSchoolGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ClassGroup_SELECT_by_ParentGroupID(object iSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtClassGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ClassGroup WHERE iSchoolGroupID LIKE '" + iSchoolGroupID + "'";
            dtClassGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtClassGroup;
        }

        /// <summary>
        /// 根據iClassGroupID取出群組
        /// </summary>
        /// <param name="iTempGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_by_iClassGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtTempGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TempGroup WHERE iClassGroupID LIKE '" + iClassGroupID + "'ORDER BY iTempGroupID ASC";
            dtTempGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtTempGroup;
        }

        /// <summary>
        /// 根據iTempGroupID取出群組
        /// </summary>
        /// <param name="iTempGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_by_TempGroupID(object iTempGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtTempGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TempGroup WHERE iTempGroupID LIKE '" + iTempGroupID + "'";
            dtTempGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtTempGroup;
        }

        /// <summary>
        /// 根據iTempGroupID的ParentGroupID(iClassGroupID)取出群組
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_by_ParentGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtTempGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TempGroup WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
            dtTempGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtTempGroup;
        }

        /// <summary>
        /// 根據ParentGroupID(iSchoolGroupID)和cClassGroupName取出課程ID
        /// </summary>
        /// <param name="iSchoolGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_ClassGroup_SELECT_by_ParentGroupID_ClassName(object iSchoolGroupID, object cClassGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtClassGroup = new DataTable();
            string strSQL = "SELECT * FROM ORCS_ClassGroup WHERE iSchoolGroupID LIKE '" + iSchoolGroupID + "'AND cClassGroupName LIKE '" + cClassGroupName + "'";
            dtClassGroup = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtClassGroup;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在SchoolGroup資料表存入資料
        /// </summary>
        /// <param name="cSchoolGroupName"></param>
        /// <returns></returns>
        public static int ORCS_SchoolGroup_INSERT(object cSchoolGroupName, object cSchoolGroupParentNodeID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_SchoolGroup(cSchoolGroupName,iParentNodeID) VALUES('" + cSchoolGroupName + "','" + cSchoolGroupParentNodeID + "')";
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
        /// 在ClassGroup資料表存入資料
        /// </summary>
        /// <param name="cClassGroupName"></param>
        /// <param name="iSchoolGroupID"></param>
        /// <returns></returns>
        public static int ORCS_ClassGroup_INSERT(object cClassGroupName, object iSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_ClassGroup(cClassGroupName, iSchoolGroupID) VALUES('" + cClassGroupName + "','" + iSchoolGroupID + "')";
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
        /// 在TempGroup資料表存入資料
        /// </summary>
        /// <param name="cTempGroupName"></param>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TempGroup_INSERT(object cTempGroupName, object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_TempGroup(cTempGroupName, iClassGroupID) VALUES('" + cTempGroupName + "','" + iClassGroupID + "')";
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
        /// 更新SchoolGroup資料表
        /// </summary>
        /// <param name="iSchoolGroupID"></param>
        /// <param name="cSchoolGroupName"></param>
        /// <returns></returns>
        public static int ORCS_SchoolGroup_UPDATE(object iSchoolGroupID, object cSchoolGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_SchoolGroup SET cSchoolGroupName = '" + cSchoolGroupName + "' WHERE iSchoolGroupID LIKE '" + iSchoolGroupID + "'";
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
        /// 更新ClassGroup資料表
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <param name="cClassGroupName"></param>
        /// <returns></returns>
        public static int ORCS_ClassGroup_UPDATE(object iClassGroupID, object cClassGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_ClassGroup SET cClassGroupName = '" + cClassGroupName + "' WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
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
        /// 更新TempGroup資料表
        /// </summary>
        /// <param name="iTempGroupID"></param>
        /// <param name="cTempGroupName"></param>
        /// <returns></returns>
        public static int ORCS_TempGroup_UPDATE(object iTempGroupID, object cTempGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE ORCS_TempGroup SET cTempGroupName = '" + cTempGroupName + "' WHERE iTempGroupID LIKE '" + iTempGroupID + "'";
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
        /// 根據SchoolGroup刪除群組
        /// </summary>
        /// <param name="iSchoolGroupID"></param>
        /// <returns></returns>
        public static int ORCS_SchoolGroup_DELETE_by_SchoolGroupID(object iSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_SchoolGroup WHERE iSchoolGroupID LIKE '" + iSchoolGroupID + "'";
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
        /// 根據ClassGroup刪除群組
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_ClassGroup_DELETE_by_ClassGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_ClassGroup WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
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
        /// 根據ClassGroup的ParentID(iSchoolGroupID)刪除群組
        /// </summary>
        /// <param name="iSchoolGroupID"></param>
        /// <returns></returns>
        public static int ORCS_ClassGroup_DELETE_by_ParentGroupID(object iSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_ClassGroup WHERE iSchoolGroupID LIKE '" + iSchoolGroupID + "'";
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
        /// 根據TempGroup刪除群組
        /// </summary>
        /// <param name="iTempGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TempGroup_DELETE_by_TempGroupID(object iTempGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_TempGroup WHERE iTempGroupID LIKE '" + iTempGroupID + "'";
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
        /// 根據TempGroup的ParentID(iClassGroupID)刪除群組
        /// </summary>
        /// <param name="iClassGroupID"></param>
        /// <returns></returns>
        public static int ORCS_TempGroup_DELETE_by_ParentGroupID(object iClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_TempGroup WHERE iClassGroupID LIKE '" + iClassGroupID + "'";
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