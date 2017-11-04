using ORCS.DB.Administrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ORCS.DB.Administrator
{
    /// <summary>
    /// clsGroupNode 的摘要描述
    /// </summary>
    public class clsGroupNode
    {
        /// <summary>
        /// 節點類別_樹根
        /// </summary>
        public const string GroupClassification_Root = "Root";
        /// <summary>
        /// 節點類別_其他
        /// </summary>
        public const string GroupClassification_Other = "Other";
        /// <summary>
        /// 系級
        /// </summary>
        public const string GroupClassification_SchoolGroup = "SchoolGroup";
        /// <summary>
        /// 課程
        /// </summary>
        public const string GroupClassification_ClassGroup = "ClassGroup";
        /// <summary>
        /// 小組
        /// </summary>
        public const string GroupClassification_TempGroup = "TempGroup";

        /// <summary>
        /// record GroupClassificationDatas
        /// </summary>
        public static Dictionary<string, string> GroupClassificationDatas;

        /// <summary>
        /// 節點ID
        /// </summary>
        public string nodeID { get; set; }
        /// <summary>
        /// 節點名稱
        /// </summary>
        public string nodeName { get; set; }
        /// <summary>
        /// 父節點ID
        /// </summary>
        public string preNodeID { get; set; }
        /// <summary>
        /// 下一層是否為SchoolGroup
        /// </summary>
        public bool isNextNodeDepartment { get; set; }
        /// <summary>
        /// 節點類別名稱
        /// </summary>
        public string nodeClassificationName { get; set; }



        public clsGroupNode()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
        }

        /// <summary>
        /// 初始化節點
        /// </summary>
        /// <param name="strNodeID">節點ID</param>
        /// <param name="strNodeName">節點名稱</param>
        /// <param name="strPreNodeID">父節點ID</param>
        /// <param name="bIsNextNodeDepartment">下一格節點是否為部門</param>
        /// <param name="strClassificationID">節點類別</param>
        public clsGroupNode(string strNodeID, string strNodeName, string strPreNodeID, string bIsNextNodeDepartment, string strNodeClassificationID)
        {
            this.nodeID = strNodeID;
            this.nodeName = strNodeName;
            this.preNodeID = strPreNodeID;
            if (bIsNextNodeDepartment.Equals("True"))
                this.isNextNodeDepartment = true;
            else
                this.isNextNodeDepartment = false;
            if (strNodeClassificationID.Equals(GroupClassification_SchoolGroup)
                || strNodeClassificationID.Equals(GroupClassification_ClassGroup)
                || strNodeClassificationID.Equals(GroupClassification_TempGroup)
                )
            {
                this.nodeClassificationName = strNodeClassificationID;
            }
            else
            {
                this.nodeClassificationName = clsGroup.GetGroupClassificationNameByID(strNodeClassificationID);
            }
        }

        /// <summary>
        /// Produce TreeView nodeID
        /// </summary>
        /// <returns>TreeView NodeID</returns>
        public string GetTreeNodeID()
        {
            return this.nodeID + "_" + this.nodeClassificationName + "_" + this.preNodeID;
        }

        /// <summary>
        /// 將NodeData組成TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetTreeNode()
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Value = GetTreeNodeID();
            treeNode.Text = this.nodeName;
            return treeNode;
        }

    }
}