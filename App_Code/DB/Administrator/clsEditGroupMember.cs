using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ORCS.DB.Administrator
{
    /// <summary>
    /// Summary description for clsEditGroupMember
    /// </summary>
    public class clsEditGroupMember
    {
        public clsEditGroupMember()
        {
        }

        /// <summary>
        /// 根據群組類別取得對應的群組資料表名稱
        /// </summary>
        /// <param name="GroupClassify"></param>
        /// <returns></returns>
        public static string GetTableNameByGroupClassify(string strGroupClassify, string strAuthority)
        {
            switch (strGroupClassify)
            {
                //系級
                case clsGroupNode.GroupClassification_SchoolGroup:
                    return clsGroup.TB_ORCS_MemberDepartment;
                //課程
                case clsGroupNode.GroupClassification_ClassGroup:
                    switch (strAuthority)
                    {
                        //管理者
                        case AllSystemUser.Authority_Administrator:
                            return clsGroup.TB_ORCS_MemberCourseTeacher;
                        //教師
                        case AllSystemUser.Authority_Teacher:
                            return clsGroup.TB_ORCS_MemberCourseTeacher;
                        //助教
                        case AllSystemUser.Authority_Assistant:
                            return clsGroup.TB_ORCS_MemberCourseAssistant;
                        //學生
                        case AllSystemUser.Authority_Student:
                            return clsGroup.TB_ORCS_MemberCourseStudent;
                    }
                    break;
                //小組
                case clsGroupNode.GroupClassification_TempGroup:
                    return clsGroup.TB_ORCS_MemberGroup;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 判斷是否組長
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static bool IsGroupLeader(object cUserID , object iGroupID, object cIdentityType)
        {
            DataTable dtMeetingIdentit = ORCS_MeetingIdentity_SELECT_by_GroupID_IdentityType(iGroupID,cIdentityType);
            if (dtMeetingIdentit.Rows.Count == 0)
            {
                return false;
            }
            return (cUserID.ToString().Equals(dtMeetingIdentit.Rows[0]["cUserID"].ToString()));
        }

        /// <summary>
        /// 取得小組組長ID
        /// </summary>
        /// <param name="iGroupID">小組ID</param>
        /// <returns></returns>
        public static string GetGroupLeaderID(object iGroupID)
        {
            DataTable dtMeetingIdentit = ORCS_MeetingIdentity_SELECT_by_GroupID_IdentityType(iGroupID, "ChairMan");
            if (dtMeetingIdentit.Rows.Count == 0)
            {
                return "";
            }
            return (dtMeetingIdentit.Rows[0]["cUserID"].ToString());
        }

        /// <summary>
        /// 根據課程ID和使用者ID取得小組ID
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static string GetGroupIDByUserIDClassID(object cUserID, object iClassID)
        {
            //取出使用者小組ID
            DataTable dtGroupMember = ORCS_GroupMember_SELECT_by_UserID(cUserID);
            //取出課程下所有小組ID
            DataTable dtTempGroup = clsEditGroup.ORCS_TempGroup_SELECT_by_iClassGroupID(iClassID);
            if (dtGroupMember.Rows.Count > 0 && dtTempGroup.Rows.Count > 0)
            {
                foreach (DataRow drGroupMember in dtGroupMember.Rows)
                {
                    foreach (DataRow drTempGroup in dtTempGroup.Rows)
                    {
                        if (drGroupMember["iGroupID"].ToString().Equals(drTempGroup["iTempGroupID"].ToString()))
                        {
                            return drGroupMember["iGroupID"].ToString();
                        }
                    }
                }
            }
            return "";
        }

        #region SELECT

        /// <summary>
        /// 根據iGroupID和cGroupClassify取出使用者ID
        /// </summary>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_GroupID_Classify(object iGroupID, object cGroupClassify, object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), cAuthority.ToString());
            string strSQL = "SELECT * FROM " + strTableName + " WHERE iGroupID LIKE '" + iGroupID + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        /// <summary>
        /// 根據iGroupID和cGroupClassify取出使用者ID(除了教師)
        /// </summary>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_GroupID_Classify_Except_Teachers(object iGroupID, object cGroupClassify)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(),"s");
            string strSQL = "SELECT OM.* FROM " + strTableName + " OM , ORCS_User OU  WHERE OM.cUserID=OU.cUserID AND OU.cAuthority !='t' AND OM.iGroupID LIKE '" + iGroupID + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        /// <summary>
        /// 根據iGroupID和cGroupClassify和cAuthority取出使用者ID
        /// </summary>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_GroupID_Classify_Authority(object iGroupID, object cGroupClassify ,object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), cAuthority.ToString());
            string strSQL = "SELECT OM.* FROM " + strTableName + " OM , ORCS_User OU  WHERE OM.cUserID=OU.cUserID AND OM.iGroupID LIKE '" + iGroupID + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }


        /// <summary>
        /// 根據iGroupID和cGroupClassify隨機取出使用者ID
        /// </summary>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_GroupID_Classify_Random(object iGroupID, object cGroupClassify, object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), cAuthority.ToString());
            string strSQL = "SELECT * FROM " + strTableName + " WHERE iGroupID LIKE '" + iGroupID + "' ORDER BY NEWID()";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        /// <summary>
        /// 根據iGroupID和cGroupClassify隨機排列取出使用者ID(不含教師)
        /// </summary>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_GroupID_Classify_Random_Except_Teachers(object iGroupID, object cGroupClassify, object cAuthority)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), cAuthority.ToString());
            string strSQL = "SELECT OM.* FROM " + strTableName + " OM , ORCS_User OU  WHERE OM.cUserID=OU.cUserID AND OU.cAuthority !='t' AND OM.iGroupID LIKE '" + iGroupID + "' ORDER BY NEWID()";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        /// <summary>
        /// 根據cUserID取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_UserID(object cUserID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strSQL = "SELECT * FROM " + clsGroup.TB_ORCS_MemberGroup + " WHERE cUserID LIKE '" + cUserID + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        /// <summary>
        /// 根據cUserID和cGroupClassify取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_UserID_Classify(object cUserID, object cGroupClassify)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strAthority = AllSystemUserUtil.GetAuthourityByUserID(cUserID.ToString());
            if (strAthority != null)
            {
                string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), strAthority);
                string strSQL = "SELECT * FROM " + strTableName + " WHERE cUserID LIKE '" + cUserID + "'";
                dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
                return dtGroupMember;
            }
            return null;
        }

        /// <summary>
        /// 根據cUserID、iGroupID和cGroupClassify取出使用者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_GroupMember_SELECT_by_UserID_GroupID_Classify(object cUserID, object iGroupID, object cGroupClassify)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strAthority = AllSystemUserUtil.GetAuthourityByUserID(cUserID.ToString());
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), strAthority);
            string strSQL = "SELECT * FROM " + strTableName + " WHERE cUserID LIKE '" + cUserID + "' AND iGroupID LIKE '" + iGroupID + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }
		
		
		/// <summary>
        /// 根據cUserID、iGroupID取出第幾小組
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <returns></returns>
		public static DataTable ORCS_TempGroup_SELECT_by_UserID_GroupID(object cUserID, object iGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strSQL = "SELECT * FROM ORCS_TempGroup WHERE iTempGroupID LIKE '" + cUserID + "' AND iClassGroupID LIKE '" + iGroupID+"'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }
        /// <summary>
        /// 根據iGroupID和cIdentityType取出會議主席/紀錄者資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static DataTable ORCS_MeetingIdentity_SELECT_by_GroupID_IdentityType(object iGroupID, object cIdentityType)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            DataTable dtGroupMember = new DataTable();
            string strSQL = "SELECT * FROM ORCS_MeetingIdentity WHERE iGroupID LIKE '" + iGroupID + "' AND cIdentityType LIKE '" + cIdentityType + "'";
            dtGroupMember = ORCSDB.GetDataSet(strSQL).Tables[0];
            return dtGroupMember;
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在群組資料表存入資料
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <returns></returns>
        public static int ORCS_GroupMember_INSERT(object cUserID, object iGroupID, object cGroupClassify)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strAthority = AllSystemUserUtil.GetAuthourityByUserID(cUserID.ToString());
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), strAthority);
            string strSQL = "INSERT INTO " + strTableName + "(cUserID, iGroupID) VALUES('" + cUserID + "','" + iGroupID + "')";
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
        /// 儲存某組別的主席和紀錄者
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static int ORCS_MeetingIdentity_INSERT(object iGroupID, object cUserID, object cIdentityType)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO ORCS_MeetingIdentity(iGroupID, cUserID, cIdentityType) VALUES('" + iGroupID + "','" + cUserID + "','" + cIdentityType + "')";
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
        /// 根據GroupID和GroupClassify刪除群組使用者
        /// </summary>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static int ORCS_GroupMember_DELETE_by_GroupID_Classify(object iGroupID, object cGroupClassify)
        {
            string strSQL = "";
            clsORCSDB ORCSDB = new clsORCSDB();
            switch(cGroupClassify.ToString())
            {
                //系級
                case clsGroupNode.GroupClassification_SchoolGroup:
                    strSQL = "DELETE " + clsGroup.TB_ORCS_MemberDepartment + " WHERE iGroupID LIKE '" + iGroupID + "'";
                    try
                    {
                        ORCSDB.ExecuteNonQuery(strSQL);
                    }
                    catch
                    {
                        return -1;
                    }
                    break;
                //課程
                case clsGroupNode.GroupClassification_ClassGroup:
                    strSQL = "DELETE " + clsGroup.TB_ORCS_MemberCourseTeacher + " WHERE iGroupID LIKE '" + iGroupID + "'";
                    try
                    {
                        ORCSDB.ExecuteNonQuery(strSQL);
                    }
                    catch
                    {
                        return -1;
                    }
                    strSQL = "DELETE " + clsGroup.TB_ORCS_MemberCourseAssistant + " WHERE iGroupID LIKE '" + iGroupID + "'";
                    try
                    {
                        ORCSDB.ExecuteNonQuery(strSQL);
                    }
                    catch
                    {
                        return -1;
                    }
                    strSQL = "DELETE " + clsGroup.TB_ORCS_MemberCourseStudent + " WHERE iGroupID LIKE '" + iGroupID + "'";
                    try
                    {
                        ORCSDB.ExecuteNonQuery(strSQL);
                    }
                    catch
                    {
                        return -1;
                    }
                    break;
                //小組
                case clsGroupNode.GroupClassification_TempGroup:
                    strSQL = "DELETE " + clsGroup.TB_ORCS_MemberGroup + " WHERE iGroupID LIKE '" + iGroupID + "'";
                    try
                    {
                        ORCSDB.ExecuteNonQuery(strSQL);
                    }
                    catch
                    {
                        return -1;
                    }
                    break;
            }
            return 0;
        }
        
        /// <summary>
        /// 根據UserID、GroupID和GroupClassify刪除群組使用者
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static int ORCS_GroupMember_DELETE_by_UserID_GroupID_Classify(object cUserID, object iGroupID, object cGroupClassify)
        {
            string strAthority = AllSystemUserUtil.GetAuthourityByUserID(cUserID.ToString());
            string strTableName = GetTableNameByGroupClassify(cGroupClassify.ToString(), strAthority);
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE " + strTableName + " WHERE cUserID LIKE '" + cUserID + "' AND iGroupID LIKE '" + iGroupID + "'";
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
        /// 根據GroupID刪除群組使用者
        /// </summary>
        /// <param name="cUserID"></param>
        /// <param name="iGroupID"></param>
        /// <param name="cGroupClassify"></param>
        /// <returns></returns>
        public static int ORCS_MeetingIdentity_DELETE_by_GroupID(object iGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE ORCS_MeetingIdentity WHERE iGroupID LIKE '" + iGroupID + "'";
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