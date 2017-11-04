using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



namespace ORCS.DB.Administrator
{
    /// <summary>
    /// clsGroup 的摘要描述
    /// </summary>
    public class clsGroup
    {
        //Data table(TB)
        public const string TB_ORCS_GroupClassification = "ORCS_GroupClassification";
        //Data table field(TF)
        public const string TF_GroupClassification_iGroupNodeClassificationID = "iGroupNodeClassificationID";
        public const string TF_GroupClassification_cGroupNodeClassificationName = "cGroupNodeClassificationName";
        //Data table(TB)
        public const string TB_ORCS_Group = "ORCS_Group";
        //Data table field(TF)
        public const string TF_Group_NodeID = "iNodeID";
        public const string TF_Group_NodeName = "cNodeName";
        public const string TF_Group_ParentNodeID = "iParentNodeID";
        public const string TF_Group_IsNextNodeDepartment = "bIsNextNodeDepartment";
        public const string TF_Group_NodeClassificationID = "iNodeClassificationID";
        //Data table(TB)
        public const string TB_ORCS_SchoolGroup = "ORCS_SchoolGroup";
        //Data table field(TF)
        public const string TF_ORCS_SchoolGroup_SchoolGroupID = "iSchoolGroupID";
        public const string TF_ORCS_SchoolGroup_SchoolGroupName = "cSchoolGroupName";
        public const string TF_ORCS_SchoolGroup_ParentNodeID = "iParentNodeID";
        //Data table(TB)
        public const string TB_ORCS_ClassGroup = "ORCS_ClassGroup";
        //Data table field(TF)
        public const string TF_ORCS_ClassGroup_ClassGroupID = "iClassGroupID";
        public const string TF_ORCS_ClassGroup_ClassGroupName = "cClassGroupName";
        public const string TF_ORCS_ClassGroup_SchoolGroupID = "iSchoolGroupID";
        //Data table(TB)
        public const string TB_ORCS_TempGroup = "ORCS_TempGroup";
        //Data table field(TF)
        public const string TF_ORCS_TempGroup_TempGroupID = "iTempGroupID";
        public const string TF_ORCS_TempGroup_TempGroupName = "cTempGroupName";
        public const string TF_ORCS_TempGroup_ClassGroupID = "iClassGroupID";
        //Data table(TB)
        public const string TB_ORCS_MemberDepartment = "ORCS_MemberDepartment";
        //Data table field(TF)
        public const string TF_ORCS_MemberDepartment_cUserID = "cUserID";
        public const string TF_ORCS_MemberDepartment_iGroupID = "iGroupID";

        //Data table(TB)
        public const string TB_ORCS_MemberCourseTeacher = "ORCS_MemberCourseTeacher";
        //Data table field(TF)
        public const string TF_ORCS_MemberCourseTeacher_cUserID = "cUserID";
        public const string TF_ORCS_MemberCourseTeacher_iGroupID = "iGroupID";

        //Data table(TB)
        public const string TB_ORCS_MemberCourseAssistant = "ORCS_MemberCourseAssistant";
        //Data table field(TF)
        public const string TF_ORCS_MemberCourseAssistant_cUserID = "cUserID";
        public const string TF_ORCS_MemberCourseAssistant_iGroupID = "iGroupID";

        //Data table(TB)
        public const string TB_ORCS_MemberCourseStudent = "ORCS_MemberCourseStudent";
        //Data table field(TF)
        public const string TF_ORCS_MemberCourseStudent_cUserID = "cUserID";
        public const string TF_ORCS_MemberCourseStudent_iGroupID = "iGroupID";

        //Data table(TB)
        public const string TB_ORCS_MemberGroup = "ORCS_MemberGroup";
        //Data table field(TF)
        public const string TF_ORCS_MemberGroup_cUserID = "cUserID";
        public const string TF_ORCS_MemberGroup_iGroupID = "iGroupID";

        //Dictionary<GroupClassificationID, GroupClassificationName>
        public static Dictionary<string, string> GroupClassifications;

        //Dictionary<DepartmentID, DepartmentName>
        public static List<clsGroupNode> Groups;

        //Dictionary<DepartmentID, DepartmentName>
        public static List<clsGroupNode> GroupDepartments;

        //Dictionary<CourseID, CourseName>
        public static List<clsGroupNode> GroupCourses;

        //Dictionary<TeamID, TeamName>
        public static List<clsGroupNode> GroupTeams;

        public clsGroup()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void InitUnitGroups(bool isUpdate)
        {
            if (Groups == null || isUpdate)
            {
                SetGroups();
            }

            if (GroupDepartments == null || isUpdate)
            {
                SetGroupDepartments();
            }

            if (GroupCourses == null || isUpdate)
            {
                SetGroupCourses();
            }

            if (GroupTeams == null || isUpdate)
            {
                SetGroupTeams();
            }
        }

        #region GET

        /// <summary>
        /// 根據單位ID和單位類別 取得單位資料
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="groupClassifyName"></param>
        /// <returns></returns>
        public static clsGroupNode GetUnitDataByGroupIDGroupClassifyName(string groupID, string groupClassifyName)
        {
            InitUnitGroups(false);

            switch (groupClassifyName)
            {
                case clsGroupNode.GroupClassification_SchoolGroup:
                    foreach (clsGroupNode GroupDepartment in GroupDepartments)
                    {
                        if (GroupDepartment.nodeID == groupID)
                        {
                            return GroupDepartment;
                        }
                    }
                    break;
                case clsGroupNode.GroupClassification_ClassGroup:
                    foreach (clsGroupNode GroupCourse in GroupCourses)
                    {
                        if (GroupCourse.nodeID == groupID)
                        {
                            return GroupCourse;
                        }
                    }
                    break;
                case clsGroupNode.GroupClassification_TempGroup:
                    foreach (clsGroupNode GroupTeam in GroupTeams)
                    {
                        if (GroupTeam.nodeID == groupID)
                        {
                            return GroupTeam;
                        }
                    }
                    break;
                default:
                    foreach (clsGroupNode Group in Groups)
                    {
                        if (Group.nodeID == groupID)
                        {
                            return Group;
                        }
                    }
                    break;
            }
            return null;
        }

        /// <summary>
        /// 根據單位編號 和單位類別 取得單位名稱
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="groupClassifyName"></param>
        /// <returns></returns>
        public static string GetGroupNameByGroupIDAndGroupClassifyName(string groupID, string groupClassifyName)
        {
            InitUnitGroups(false);

            switch (groupClassifyName)
            {
                case clsGroupNode.GroupClassification_SchoolGroup:
                    foreach (clsGroupNode GroupDepartment in GroupDepartments)
                    {
                        if (GroupDepartment.nodeID == groupID)
                        {
                            return GroupDepartment.nodeName;
                        }
                    }
                    break;
                case clsGroupNode.GroupClassification_ClassGroup:
                    foreach (clsGroupNode GroupCourse in GroupCourses)
                    {
                        if (GroupCourse.nodeID == groupID)
                        {
                            return GroupCourse.nodeName;
                        }
                    }
                    break;
                case clsGroupNode.GroupClassification_TempGroup:
                    foreach (clsGroupNode GroupTeam in GroupTeams)
                    {
                        if (GroupTeam.nodeID == groupID)
                        {
                            return GroupTeam.nodeName;
                        }
                    }
                    break;
                default:
                    foreach (clsGroupNode Group in Groups)
                    {
                        if (Group.nodeID == groupID)
                        {
                            return Group.nodeName;
                        }
                    }
                    break;
            }
            return null;
        }

        /// <summary>
        /// 根據CourseID取得到樹根路徑
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetUnitPathIDsByCourseID(string CourseID, string Type)
        {
            InitUnitGroups(false);
            string strUnitPathID = "";
            string strUnitPathName = "";
            string strTempID = "";
            //取得部門ID
            foreach (clsGroupNode GroupCourse in GroupCourses)
            {
                if (GroupCourse.nodeID == CourseID)
                {
                    strTempID = GroupCourse.preNodeID;
                    strUnitPathID = strTempID + "/" + CourseID;
                    strUnitPathName = GroupCourse.nodeName;
                }
            }
            //取得部門ID
            foreach (clsGroupNode GroupDepartment in GroupDepartments)
            {
                if (GroupDepartment.nodeID == strTempID)
                {
                    strTempID = GroupDepartment.preNodeID;
                    strUnitPathID = strTempID + "/" + strUnitPathID;
                    strUnitPathName = GroupDepartment.nodeName + "/" + strUnitPathName;
                }
            }
            //取得樹根到部門間的路徑
            bool isRepeat = true;
            while (isRepeat)
            {
                isRepeat = false;
                foreach (clsGroupNode Group in Groups)
                {
                    if (Group.nodeID == strTempID)
                    {
                        strTempID = Group.preNodeID;
                        if (strTempID != "0")//0為樹根的父節點編號 若為0代表ID無需再增加
                        {
                            strUnitPathID = strTempID + "/" + strUnitPathID;
                        }
                        strUnitPathName = Group.nodeName + "/" + strUnitPathName;
                        isRepeat = true;
                    }
                }
            }
            if (Type.Equals("Name"))
                return strUnitPathName;
            else
                return strUnitPathID;
        }

        /// <summary>
        /// 根據DepartmentID取得到Unit Root路徑
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetUnitPathIDsByDepartmentID(string DepartmentID, string Type)
        {
            InitUnitGroups(false);
            string strUnitPathID = "";
            string strUnitPathName = "";
            string strTempID = "";
            //取得部門上屬的UnitID
            foreach (clsGroupNode GroupDepartment in GroupDepartments)
            {
                if (GroupDepartment.nodeID == DepartmentID)
                {
                    strTempID = GroupDepartment.preNodeID;
                    strUnitPathID = strTempID + "/" + DepartmentID;
                    strUnitPathName = GroupDepartment.nodeName;
                }
            }
            //取得樹根到部門間的路徑
            bool isRepeat = true;
            while (isRepeat)
            {
                isRepeat = false;
                foreach (clsGroupNode Group in Groups)
                {
                    if (Group.nodeID == strTempID)
                    {
                        strTempID = Group.preNodeID;
                        if (strTempID != "0")//0為樹根的父節點編號 若為0代表ID無需再增加
                        {
                            strUnitPathID = strTempID + "/" + strUnitPathID;
                        }
                        strUnitPathName = Group.nodeName + "/" + strUnitPathName;
                        isRepeat = true;
                    }
                }
            }
            if (Type.Equals("Name"))
                return strUnitPathName;
            else
                return strUnitPathID;
        }

        /// <summary>
        /// 根據NodeID取得子節點的所有ID
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static List<string> GetChildNodeIDs(string NodeID)
        {
            List<string> childNodeIDs = new List<string>();
            DataTable childNodeDatas = ORCS_Group_SELECT_All_By_ParentNodeID(NodeID);
            if (childNodeDatas.Rows.Count > 0)
            {
                foreach (DataRow childNodeData in childNodeDatas.Rows)
                {
                    childNodeIDs.Add(childNodeData[TF_Group_NodeID].ToString());
                }
                return childNodeIDs;
            }
            return null;
        }

        /// <summary>
        /// 取得子節點
        /// </summary>
        /// <param name="levelNodeID"></param>
        /// <returns></returns>
        public static List<clsGroupNode> GetChildNodes(string NodeID)
        {
            DataTable childNodeDatas = ORCS_Group_SELECT_All_By_ParentNodeID(NodeID);
            if (childNodeDatas.Rows.Count > 0)
                return clsGroup.GetNodeDatasBy_TB_Group(childNodeDatas);
            return null;
        }


        /// <summary>
        /// 取得群組類別名稱
        /// </summary>
        /// <param name="groupClassificationID">群組類別ID</param>
        /// <returns></returns>
        public static string GetGroupClassificationNameByID(string groupClassificationID)
        {
            if (GroupClassifications == null)
                SetGroupClassificationDatas();
            return GroupClassifications[groupClassificationID];
        }


        /// <summary>
        /// Get TreeNode RootID
        /// </summary>
        /// <returns>RootID</returns>
        public static string GetTreeNodeRootID()
        {
            DataTable DTRootData = ORCS_GroupClassification_SELECT_By_GroupNodeClassificationName(clsGroupNode.GroupClassification_Root);
            return DTRootData.Rows[0][TF_GroupClassification_iGroupNodeClassificationID].ToString();
        }

        /// <summary>
        /// Get TreeNode Other GroupClassificationID
        /// </summary>
        /// <returns>RootID</returns>
        public static string GetTreeNodeOtherID()
        {
            DataTable DTRootData = ORCS_GroupClassification_SELECT_By_GroupNodeClassificationName(clsGroupNode.GroupClassification_Other);
            return DTRootData.Rows[0][TF_GroupClassification_iGroupNodeClassificationID].ToString();
        }

        /// <summary>
        /// 根據TB_Group組成節點資料串列
        /// </summary>
        /// <returns></returns>
        public static List<clsGroupNode> GetNodeDatasBy_TB_Group(DataTable dtNodeDatas)
        {
            //Get LevelGroup node data list
            List<clsGroupNode> NodeDatas = new List<clsGroupNode>();
            foreach (DataRow dtNodeData in dtNodeDatas.Rows)
            {
                clsGroupNode nodeData = new clsGroupNode(
                    dtNodeData[clsGroup.TF_Group_NodeID].ToString(),
                    dtNodeData[clsGroup.TF_Group_NodeName].ToString(),
                    dtNodeData[clsGroup.TF_Group_ParentNodeID].ToString(),
                    dtNodeData[clsGroup.TF_Group_IsNextNodeDepartment].ToString(),
                    dtNodeData[clsGroup.TF_Group_NodeClassificationID].ToString()
                    );
                NodeDatas.Add(nodeData);
            }
            return NodeDatas;
        }


        /// <summary>
        /// 根據TB_SchoolOrClassOrTempGroup組成節點資料串列
        /// </summary>
        /// <returns></returns>
        public static List<clsGroupNode> GetNodeDatasBy_TB_SchoolOrClassOrTempGroup(DataTable dtNodeDatas, string GroupClassifacationName)
        {
            List<clsGroupNode> NodeDatas = new List<clsGroupNode>();
            switch (GroupClassifacationName)
            {
                case clsGroupNode.GroupClassification_SchoolGroup:
                    foreach (DataRow dtNodeData in dtNodeDatas.Rows)
                    {
                        clsGroupNode nodeData = new clsGroupNode(
                            dtNodeData[TF_ORCS_SchoolGroup_SchoolGroupID].ToString(),
                            dtNodeData[TF_ORCS_SchoolGroup_SchoolGroupName].ToString(),
                            dtNodeData[TF_ORCS_SchoolGroup_ParentNodeID].ToString(),
                            "0",
                            clsGroupNode.GroupClassification_SchoolGroup
                            );
                        NodeDatas.Add(nodeData);
                    }
                    break;
                case clsGroupNode.GroupClassification_ClassGroup:
                    foreach (DataRow dtNodeData in dtNodeDatas.Rows)
                    {
                        clsGroupNode nodeData = new clsGroupNode(
                            dtNodeData[TF_ORCS_ClassGroup_ClassGroupID].ToString(),
                            dtNodeData[TF_ORCS_ClassGroup_ClassGroupName].ToString(),
                            dtNodeData[TF_ORCS_ClassGroup_SchoolGroupID].ToString(),
                            "0",
                            clsGroupNode.GroupClassification_ClassGroup
                            );
                        NodeDatas.Add(nodeData);
                    }
                    break;
                case clsGroupNode.GroupClassification_TempGroup:
                    foreach (DataRow dtNodeData in dtNodeDatas.Rows)
                    {
                        clsGroupNode nodeData = new clsGroupNode(
                            dtNodeData[TF_ORCS_TempGroup_TempGroupID].ToString(),
                            dtNodeData[TF_ORCS_TempGroup_TempGroupName].ToString(),
                            dtNodeData[TF_ORCS_TempGroup_ClassGroupID].ToString(),
                            "0",
                            clsGroupNode.GroupClassification_TempGroup
                            );
                        NodeDatas.Add(nodeData);
                    }
                    break;
                default:
                    break;
            }
            return NodeDatas;
        }

        /// <summary>
        /// 根據單位名稱取得單位編號
        /// </summary>
        /// <param name="strGroupName"></param>
        /// <param name="strGroupClassification"></param>
        /// <returns>If have UnitGroupData,return UnitGroupID ,else return null</returns>
        public static string GetGroupIDByGroupName_GroupClassification(string strUnitName, string strGroupClassification)
        {
            DataTable dtUnitGroupData;
            switch (strGroupClassification)
            {
                case clsGroupNode.GroupClassification_SchoolGroup:
                    dtUnitGroupData = ORCS_SchoolGroup_SELECT_By_cSchoolGroupName(strUnitName);
                    if (dtUnitGroupData.Rows.Count > 0)
                        return dtUnitGroupData.Rows[0][TF_ORCS_SchoolGroup_SchoolGroupID].ToString();
                    break;
                case clsGroupNode.GroupClassification_ClassGroup:
                    dtUnitGroupData = ORCS_ClassGroup_SELECT_By_cClassGroupName(strUnitName);
                    if (dtUnitGroupData.Rows.Count > 0)
                        return dtUnitGroupData.Rows[0][TF_ORCS_ClassGroup_ClassGroupID].ToString();
                    break;
                case clsGroupNode.GroupClassification_TempGroup:
                    dtUnitGroupData = ORCS_TempGroup_SELECT_By_cTempGroupName(strUnitName);
                    if (dtUnitGroupData.Rows.Count > 0)
                        return dtUnitGroupData.Rows[0][TF_ORCS_TempGroup_TempGroupID].ToString();
                    break;
            }
            return null;
        }

        /// <summary>
        /// 根據單位名稱和上屬單位ID取得單位編號
        /// </summary>
        /// <param name="strGroupName"></param>
        /// <param name="strGroupClassification"></param>
        /// <returns>If have UnitGroupData,return UnitGroupID ,else return null</returns>
        public static string GetGroupIDByGroupName_ParentGroupID_GroupClassification(string strUnitName, string strParentUnitID, string strGroupClassification)
        {
            DataTable dtUnitGroupData;
            switch (strGroupClassification)
            {
                case clsGroupNode.GroupClassification_SchoolGroup:
                    dtUnitGroupData = ORCS_SchoolGroup_SELECT_By_cSchoolGroupName(strUnitName);
                    if (dtUnitGroupData.Rows.Count > 0)
                        return dtUnitGroupData.Rows[0][TF_ORCS_SchoolGroup_SchoolGroupID].ToString();
                    break;
                case clsGroupNode.GroupClassification_ClassGroup:
                    dtUnitGroupData = ORCS_ClassGroup_SELECT_By_cClassGroupName_cSchoolGroupID(strUnitName, strParentUnitID);
                    if (dtUnitGroupData.Rows.Count > 0)
                        return dtUnitGroupData.Rows[0][TF_ORCS_ClassGroup_ClassGroupID].ToString();
                    break;
                case clsGroupNode.GroupClassification_TempGroup:
                    dtUnitGroupData = ORCS_TempGroup_SELECT_By_cTempGroupName_cClassGroupID(strUnitName, strParentUnitID);
                    if (dtUnitGroupData.Rows.Count > 0)
                        return dtUnitGroupData.Rows[0][TF_ORCS_TempGroup_TempGroupID].ToString();
                    break;
            }
            return null;
        }

        /// <summary>
        /// 取得所有部門
        /// </summary>
        /// <returns></returns>
        public static List<clsGroupNode> GetAllDepartment()
        {
            DataTable dtSchoolGroups = ORCS_SchoolGroup_SELECT_All();
            List<clsGroupNode> groupNodes = new List<clsGroupNode>();
            foreach (DataRow drSchoolGroup in dtSchoolGroups.Rows)
            {
                clsGroupNode groupNode = new clsGroupNode(drSchoolGroup[TF_ORCS_SchoolGroup_SchoolGroupID].ToString(),
                                                           drSchoolGroup[TF_ORCS_SchoolGroup_SchoolGroupName].ToString(),
                                                           drSchoolGroup[TF_ORCS_SchoolGroup_ParentNodeID].ToString(),
                                                           "false", clsGroupNode.GroupClassification_SchoolGroup);
                groupNodes.Add(groupNode);
            }
            return groupNodes;
        }

        /// <summary>
        /// 根據部門ID取得所有課程
        /// </summary>
        /// <returns></returns>
        public static List<clsGroupNode> GetAllCourseByDepartment(string strDepartmentID)
        {
            DataTable dtCourseGroups = ORCS_ClassGroup_SELECT_By_cSchoolGroupID(strDepartmentID);
            List<clsGroupNode> groupNodes = new List<clsGroupNode>();
            foreach (DataRow drCourseGroup in dtCourseGroups.Rows)
            {
                clsGroupNode groupNode = new clsGroupNode(drCourseGroup[TF_ORCS_ClassGroup_ClassGroupID].ToString(),
                                                           drCourseGroup[TF_ORCS_ClassGroup_ClassGroupName].ToString(),
                                                           drCourseGroup[TF_ORCS_ClassGroup_SchoolGroupID].ToString(),
                                                           "false", clsGroupNode.GroupClassification_ClassGroup);
                groupNodes.Add(groupNode);
            }
            return groupNodes;
        }

        #endregion

        #region SET

        /// <summary>
        /// 設定群組資訊
        /// </summary>
        public static void SetGroups()
        {
            Groups = new List<clsGroupNode>();
            DataTable GroupDatas = ORCS_Group_SELECT_All();
            foreach (DataRow GroupData in GroupDatas.Rows)
            {
                clsGroupNode groupNode = new clsGroupNode(GroupData[TF_Group_NodeID].ToString(),
                                                          GroupData[TF_Group_NodeName].ToString(),
                                                          GroupData[TF_Group_ParentNodeID].ToString(),
                                                          GroupData[TF_Group_IsNextNodeDepartment].ToString(),
                                                          GroupData[TF_Group_NodeClassificationID].ToString());
                Groups.Add(groupNode);
            }
        }

        /// <summary>
        /// 設定部門資訊
        /// </summary>
        public static void SetGroupDepartments()
        {
            GroupDepartments = new List<clsGroupNode>();
            DataTable DepartmentDatas = ORCS_SchoolGroup_SELECT_All();
            foreach (DataRow DepartmentData in DepartmentDatas.Rows)
            {
                clsGroupNode groupNode = new clsGroupNode(DepartmentData[TF_ORCS_SchoolGroup_SchoolGroupID].ToString(),
                                          DepartmentData[TF_ORCS_SchoolGroup_SchoolGroupName].ToString(),
                                          DepartmentData[TF_ORCS_SchoolGroup_ParentNodeID].ToString(),
                                          "false",
                                          clsGroupNode.GroupClassification_SchoolGroup);
                GroupDepartments.Add(groupNode);
            }
        }

        /// <summary>
        /// 設定課程資訊
        /// </summary>
        public static void SetGroupCourses()
        {
            GroupCourses = new List<clsGroupNode>();
            DataTable CourseDatas = ORCS_classGroup_SELECT_All();
            foreach (DataRow CourseData in CourseDatas.Rows)
            {
                clsGroupNode groupNode = new clsGroupNode(CourseData[TF_ORCS_ClassGroup_ClassGroupID].ToString(),
                                          CourseData[TF_ORCS_ClassGroup_ClassGroupName].ToString(),
                                          CourseData[TF_ORCS_ClassGroup_SchoolGroupID].ToString(),
                                          "false",
                                          clsGroupNode.GroupClassification_ClassGroup);
                GroupCourses.Add(groupNode);
            }
        }

        /// <summary>
        /// 設定小組資訊
        /// </summary>
        public static void SetGroupTeams()
        {
            GroupTeams = new List<clsGroupNode>();
            DataTable TeamDatas = ORCS_TempGroup_SELECT_All();
            foreach (DataRow TeamData in TeamDatas.Rows)
            {
                clsGroupNode groupNode = new clsGroupNode(TeamData[TF_ORCS_TempGroup_TempGroupID].ToString(),
                                          TeamData[TF_ORCS_TempGroup_TempGroupName].ToString(),
                                          TeamData[TF_ORCS_TempGroup_ClassGroupID].ToString(),
                                          "false",
                                          clsGroupNode.GroupClassification_TempGroup);
                GroupDepartments.Add(groupNode);
            }
        }


        /// <summary>
        /// SetGroupClassificationDatas
        /// </summary>
        private static void SetGroupClassificationDatas()
        {
            GroupClassifications = new Dictionary<string, string>();
            DataTable groupClassificationDatas = ORCS_GroupClassification_SELECT_All();
            foreach (DataRow groupClassificationData in groupClassificationDatas.Rows)
            {
                GroupClassifications.Add(groupClassificationData[TF_GroupClassification_iGroupNodeClassificationID].ToString()
                    , groupClassificationData[TF_GroupClassification_cGroupNodeClassificationName].ToString());
            }
        }

        /// <summary>
        /// 根據子節點IDs設定節點的父節點ID
        /// </summary>
        /// <param name="childNodeIDs">子節點IDs</param>
        /// <param name="parentNodeID">父節點ID</param>
        public static void SetChildNodeParentID(List<string> childNodeIDs, string parentNodeID)
        {
            ORCS_Group_UPDATE_ParentNodeID_BY_NodeIDs(childNodeIDs, parentNodeID);
        }

        /// <summary>
        /// 設定所選節點 下一個節點為系別為True
        /// </summary>
        /// <param name="cLevelNodeName"></param>
        /// <param name="iPreLevelNodeID"></param>
        /// <param name="bIsNextNodeDepartment"></param>
        public static void SetNodeIsNextNodeDepartment(string nodeID, string isNextNodeDepartment)
        {
            ORCS_Group_UPDATE_IsNextNodeDepartment_BY_NodeID(nodeID, isNextNodeDepartment);
        }

        #endregion
       
        /// <summary>
        /// 如果資料表沒有節點資料加入樹根資料.
        /// </summary>
        public static void HasNoNodeAddRootNode()
        {
            //GroupClassification加入一筆Other類別
            ORCS_GroupClassification_INSERT(clsGroupNode.GroupClassification_Other);
            //GroupClassification加入一筆ROOT類別
            ORCS_GroupClassification_INSERT(clsGroupNode.GroupClassification_Root);
            //取得ROOT類別ID
            string rootClassificationID = GetTreeNodeRootID();
            //LevelGroup加入一筆樹根資料
            AddNodeData("未命名", "0", rootClassificationID, "0");
        }

        /// <summary>
        /// 是否下一個節點類別為系別
        /// </summary>
        public static bool IsNextNodeClassificationDepartment(string NodeID)
        {
            DataTable nodeData = ORCS_Group_SELECT_By_LevelNodeID(NodeID);
            if (nodeData.Rows[0][TF_Group_IsNextNodeDepartment].ToString().Equals("True"))
                return true;
            return false;
        }



        /// <summary>
        /// 新增節點至ORCS_Group表
        /// </summary>
        /// <param name="cNodeName"></param>
        /// <param name="iParentNodeID"></param>
        /// <param name="iNodeClassificationID"></param>
        /// <param name="bIsNextNodeDepartment"></param>
        public static void AddNodeData(string cNodeName, string iParentNodeID, string iNodeClassificationID, string bIsNextNodeDepartment)
        {
            if (iNodeClassificationID == null)
            {
                iNodeClassificationID = GetTreeNodeOtherID();
            }
            ORCS_Group_INSERT(cNodeName, iParentNodeID, iNodeClassificationID, bIsNextNodeDepartment);
        }


        /// <summary>
        /// 刪除節點
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static int DeleteNodeByNodeID(string NodeID)
        {
            return ORCS_Group_Delete_By_NodeID(NodeID);
        }

        

        #region SELECT

        /// <summary>
        /// 取出所有系級資料
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_SchoolGroup_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_SchoolGroup;
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable ORCS_SchoolGroup_SELECT_By_cSchoolGroupName(string strGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_SchoolGroup + " WHERE " + TF_ORCS_SchoolGroup_SchoolGroupName + " ='" + strGroupName + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 取出所有課程資料
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_classGroup_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_ClassGroup;
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable ORCS_ClassGroup_SELECT_By_cClassGroupName(string strGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_ClassGroup + " WHERE " + TF_ORCS_ClassGroup_ClassGroupName + " ='" + strGroupName + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable ORCS_ClassGroup_SELECT_By_cClassGroupName_cSchoolGroupID(string strCourseGroupName,string strSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_ClassGroup + " WHERE " + TF_ORCS_ClassGroup_ClassGroupName + " ='" + strCourseGroupName + 
                "' AND " + TF_ORCS_ClassGroup_SchoolGroupID + " ='" + strSchoolGroupID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        public static DataTable ORCS_ClassGroup_SELECT_By_cSchoolGroupID(string strSchoolGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_ClassGroup + " WHERE " + TF_ORCS_ClassGroup_SchoolGroupID + " ='" + strSchoolGroupID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 取出所有課程資料
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_TempGroup;
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 根據小組ID取出小組資料
        /// </summary>
        /// <param name="strGroupName"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_By_cTempGroupID(string strTempGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_TempGroup + " WHERE " + TF_ORCS_TempGroup_TempGroupID + " ='" + strTempGroupID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 根據小組名稱取出小組資料
        /// </summary>
        /// <param name="strGroupName"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_By_cTempGroupName(string strGroupName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_TempGroup + " WHERE " + TF_ORCS_TempGroup_TempGroupName + " ='" + strGroupName + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 根據小組名稱和所屬課程ID取出小組資料
        /// </summary>
        /// <param name="strGroupName"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_By_cTempGroupName_cClassGroupID(string strGroupName,string strClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_TempGroup + " WHERE " + TF_ORCS_TempGroup_TempGroupName + " ='" + strGroupName +
                "' AND " + TF_ORCS_TempGroup_ClassGroupID + " ='" + strClassGroupID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }


        /// <summary>
        /// 根據課程ID取出小組資料
        /// </summary>
        /// <param name="strClassGroupID"></param>
        /// <returns></returns>
        public static DataTable ORCS_TempGroup_SELECT_By_cClassGroupID(string strClassGroupID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_TempGroup + " WHERE " + TF_ORCS_TempGroup_ClassGroupID + " ='" + strClassGroupID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 取出所有ORCS_Group資料
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_Group_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_Group;
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 根據iLevelNodeID取出ORCS_Group資料
        /// </summary>
        /// <param name="iLevelNodeID">節點ID</param>
        /// <returns></returns>
        public static DataTable ORCS_Group_SELECT_By_LevelNodeID(string iLevelNodeID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_Group + " WHERE " + TF_Group_NodeID + " ='" + iLevelNodeID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 根據ParentNodeID取出ORCS_Group資料表中的ChildNode
        /// </summary>
        /// <param name="iParentNodeID">父節點ID</param>
        /// <returns></returns>
        public static DataTable ORCS_Group_SELECT_All_By_ParentNodeID(string iParentNodeID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_Group + " WHERE " + TF_Group_ParentNodeID + " ='" + iParentNodeID + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        /// <summary>
        /// 取出所有ORCS_GroupClassification資料
        /// </summary>
        /// <returns></returns>
        public static DataTable ORCS_GroupClassification_SELECT_All()
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_GroupClassification;
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }
        /// <summary>
        /// 根據iGroupNodeClassificationID取出ORCS_GroupClassification資料
        /// </summary>
        /// <param name="iGroupNodeClassificationName">節點類別名稱</param>
        /// <returns></returns>
        public static DataTable ORCS_GroupClassification_SELECT_By_GroupNodeClassificationName(string iGroupNodeClassificationName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "SELECT * FROM " + TB_ORCS_GroupClassification + " WHERE " + TF_GroupClassification_cGroupNodeClassificationName + " ='" + iGroupNodeClassificationName + "'";
            return ORCSDB.GetDataSet(strSQL).Tables[0];
        }

        #endregion

        #region INSERT

        /// <summary>
        /// 在ORCS_GroupClassification資料表存入節點類別
        /// </summary>
        /// <param name="cNodeClassificationName">節點類別名稱</param>
        /// <returns></returns>
        public static int ORCS_GroupClassification_INSERT(object cNodeClassificationName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO " + TB_ORCS_GroupClassification + "(" + TF_GroupClassification_cGroupNodeClassificationName +
                ") VALUES('" + cNodeClassificationName + "')";
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
        /// 在ORCS_Group資料表存入NodeData
        /// </summary>
        /// <param name="NodeName">節點名稱</param>
        /// <param name="iPreLevelNodeID">父節點ID</param>
        /// <param name="iGroupNodeClassificationID">類別ID</param>
        /// <param name="bIsNextNodeDepartment">下一個節點是否為系別</param>
        /// <returns></returns>
        public static int ORCS_Group_INSERT(object cNodeName, object iParentNodeID, object iNodeClassificationID, object bIsNextNodeDepartment)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "INSERT INTO " + TB_ORCS_Group + "(" + TF_Group_NodeName + "," +
                TF_Group_ParentNodeID + "," + TF_Group_NodeClassificationID + "," + TF_Group_IsNextNodeDepartment +
                ") VALUES('" + cNodeName + "','" + iParentNodeID + "','" + iNodeClassificationID + "'," + bIsNextNodeDepartment + ")";
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
        /// 根據NodeID更新NodeName
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        public static int ORCS_Group_UPDATE_NodeName_BY_NodeID(object NodeID, object NodeName)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE " + TB_ORCS_Group + " SET " + TF_Group_NodeName + " = '" + NodeName +
                "' WHERE " + TF_Group_NodeID + " LIKE '" + NodeID + "'";
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
        /// 根據NodeID更新節點的parentNodeID
        /// </summary>
        /// <param name="NodeIDs"></param>
        /// <param name="parentNodeID"></param>
        /// <returns></returns>
        public static int ORCS_Group_UPDATE_ParentNodeID_BY_NodeIDs(List<string> NodeIDs, object parentNodeID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            foreach (string NodeID in NodeIDs)
            {
                string strSQL = "UPDATE " + TB_ORCS_Group + " SET " + TF_Group_ParentNodeID + " = '" + parentNodeID +
                    "' WHERE " + TF_Group_NodeID + " LIKE '" + NodeID + "'";
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

        /// <summary>
        /// 根據NodeID更新節點的IsNextNodeDepartment
        /// </summary>
        /// <param name="isNextNodeDepartment"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public static int ORCS_Group_UPDATE_IsNextNodeDepartment_BY_NodeID(object nodeID, object isNextNodeDepartment)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "UPDATE " + TB_ORCS_Group + " SET " + TF_Group_IsNextNodeDepartment + " = " + isNextNodeDepartment +
                " WHERE " + TF_Group_NodeID + " LIKE '" + nodeID + "'";
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

        #region Delete

        /// <summary>
        /// 根據NodeID刪除節點
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public static int ORCS_Group_Delete_By_NodeID(string NodeID)
        {
            clsORCSDB ORCSDB = new clsORCSDB();
            string strSQL = "DELETE " + TB_ORCS_Group + " WHERE " + TF_Group_NodeID + " LIKE '" + NodeID + "'";
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