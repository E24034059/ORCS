using ORCS.DB.Administrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrator_SelectGroup : System.Web.UI.Page
{
    //從何種模式進來
    string modle = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        modle = Request["modle"].ToString();

        if (!IsPostBack)
        {

            //先清空TreeView(避免重複出現)
            tvGroup.Nodes.Clear();
            //Get node data 
            DataTable dtLevelNodeDatas = clsGroup.ORCS_Group_SELECT_All();
            //如果沒資料 新增節點
            if (dtLevelNodeDatas.Rows.Count == 0)
            {
                clsGroup.HasNoNodeAddRootNode();
                dtLevelNodeDatas = clsGroup.ORCS_Group_SELECT_All();
            }
            //Get Group node data list
            List<clsGroupNode> nodeDatas = clsGroup.GetNodeDatasBy_TB_Group(dtLevelNodeDatas);
            //Get SchoolGroup node data list
            DataTable dtSchoolGroup = clsEditGroup.ORCS_SchoolGroup_SELECT_by_SchoolGroupID("%");
            List<clsGroupNode> schoolNodeDatas = clsGroup.GetNodeDatasBy_TB_SchoolOrClassOrTempGroup(dtSchoolGroup, clsGroupNode.GroupClassification_SchoolGroup);
            //Get ClassGroup node data list
            DataTable dtClassGroup = clsEditGroup.ORCS_ClassGroup_SELECT_All();
            List<clsGroupNode> classNodeDatas = clsGroup.GetNodeDatasBy_TB_SchoolOrClassOrTempGroup(dtClassGroup, clsGroupNode.GroupClassification_ClassGroup);
            //Get TempGroup node data 無需用到小組資訊
            List<clsGroupNode> tempNodeDatas = null;

            //設定TreeNode ROOT 
            TreeNode tnRoot = new TreeNode();
            foreach (clsGroupNode nodeData in nodeDatas)
            {
                if (nodeData.nodeClassificationName.Equals(clsGroupNode.GroupClassification_Root))
                {
                    tnRoot = nodeData.GetTreeNode();
                    tnRoot.SelectAction = TreeNodeSelectAction.None;
                    nodeDatas.Remove(nodeData);
                    break;
                }
            }
            //begin orginize tree node
            SetTreeNodeByGroupNodeDatas(tnRoot, nodeDatas, schoolNodeDatas, classNodeDatas, tempNodeDatas);
            //將TreeNode加入TreeView裡
            tvGroup.Nodes.Add(tnRoot);
            //展開所有Node
            tvGroup.ExpandAll();
        }
    }


    /// <summary>
    /// 根據levelGroupData組成樹節點
    /// </summary>
    /// <param name="beginTreeNod"></param>
    /// <param name="levelNodeDatas"></param>
    /// <param name="schoolNodeDatas"></param>
    /// <param name="classNodeDatas"></param>
    /// <param name="tempNodeDatas"></param>
    /// <returns></returns>
    private TreeNode SetTreeNodeByGroupNodeDatas(TreeNode beginTreeNod, List<clsGroupNode> levelNodeDatas, List<clsGroupNode> schoolNodeDatas, List<clsGroupNode> classNodeDatas, List<clsGroupNode> tempNodeDatas)
    {
        bool isNotRepeat = true;
        while (levelNodeDatas.Count > 0 && isNotRepeat)
        {
            isNotRepeat = false;
            foreach (clsGroupNode levelNodeData in levelNodeDatas)
            {
                if (levelNodeData.preNodeID.Equals(beginTreeNod.Value.Split('_')[0]))
                {
                    TreeNode childTreeNode = levelNodeData.GetTreeNode();
                    if (levelNodeData.isNextNodeDepartment)
                    {
                        GetTreeBySchoolClassTempGroupDatas(childTreeNode, clsGroupNode.GroupClassification_SchoolGroup, schoolNodeDatas, classNodeDatas, tempNodeDatas);
                    }
                    SetTreeNodeByGroupNodeDatas(childTreeNode, levelNodeDatas, schoolNodeDatas, classNodeDatas, tempNodeDatas);
                    childTreeNode.SelectAction = TreeNodeSelectAction.None;
                    beginTreeNod.ChildNodes.Add(childTreeNode);
                    levelNodeDatas.Remove(levelNodeData);
                    isNotRepeat = true;
                    break;
                }
            }
        }
        return beginTreeNod;
    }

    /// <summary>
    /// 根據schoolGroupDatas,classGroupDatas,tempGroupDatas組成樹節點
    /// </summary>
    /// <param name="beginTreeNod"></param>
    /// <param name="chileNodeClassification"></param>
    /// <param name="schoolNodeDatas"></param>
    /// <param name="classNodeDatas"></param>
    /// <param name="tempNodeDatas"></param>
    /// <returns></returns>
    private TreeNode GetTreeBySchoolClassTempGroupDatas(TreeNode beginTreeNod, string chileNodeClassification, List<clsGroupNode> schoolNodeDatas, List<clsGroupNode> classNodeDatas, List<clsGroupNode> tempNodeDatas)
    {
        bool isNotRepeat = true;
        switch (chileNodeClassification)
        {
            case clsGroupNode.GroupClassification_SchoolGroup:
                while (schoolNodeDatas.Count > 0 && isNotRepeat)
                {
                    isNotRepeat = false;
                    foreach (clsGroupNode schoolNodeData in schoolNodeDatas)
                    {
                        if (schoolNodeData.preNodeID.Equals(beginTreeNod.Value.Split('_')[0]))
                        {
                            TreeNode childTreeNode = schoolNodeData.GetTreeNode();
                            GetTreeBySchoolClassTempGroupDatas(childTreeNode, clsGroupNode.GroupClassification_ClassGroup, schoolNodeDatas, classNodeDatas, tempNodeDatas);
                            childTreeNode.SelectAction = TreeNodeSelectAction.None;
                            beginTreeNod.ChildNodes.Add(childTreeNode);
                            schoolNodeDatas.Remove(schoolNodeData);
                            isNotRepeat = true;
                            break;
                        }
                    }
                }
                break;
            case clsGroupNode.GroupClassification_ClassGroup:
                while (classNodeDatas.Count > 0 && isNotRepeat)
                {
                    isNotRepeat = false;
                    foreach (clsGroupNode classNodeData in classNodeDatas)
                    {
                        if (classNodeData.preNodeID.Equals(beginTreeNod.Value.Split('_')[0]))
                        {
                            TreeNode childTreeNode = classNodeData.GetTreeNode();
                            beginTreeNod.ChildNodes.Add(childTreeNode);
                            classNodeDatas.Remove(classNodeData);
                            isNotRepeat = true;
                            break;
                        }
                    }
                }
                break;
            default:
                break;
        }
        return beginTreeNod;
    }

    //選擇不同TreeView的Node發生之事件
    protected void tvGroup_SelectedNodeChanged(object sender, EventArgs e)
    {
        string[] paths = tvGroup.SelectedNode.ValuePath.Split('/');
        clsGroup GroupUtil = new clsGroup();
        int departmentIndex = paths.Length;
        string NamePaths = "";
        string ValuePaths = "";
        //組合單位路徑
        int num = 1;
        //判斷是什麼頁面使用本頁面
        switch (modle)
        {
            //編輯模式頁面進來
            case "EditMLASCourse":
                foreach (string path in paths)
                {
                    string GroupID = "";
                    string GroupName = "";
                    if (num < paths.Length - 1)
                    {
                        GroupID = path.Split('_')[0];
                        GroupName = clsGroup.GetGroupNameByGroupIDAndGroupClassifyName(GroupID, clsGroupNode.GroupClassification_Other);
                    }
                    else if (num < paths.Length)
                    {
                        GroupID = path.Split('_')[0];
                        GroupName = clsGroup.GetGroupNameByGroupIDAndGroupClassifyName(GroupID, clsGroupNode.GroupClassification_SchoolGroup);
                    }
                    else
                    {
                        GroupID = path.Split('_')[0];
                        GroupName = clsGroup.GetGroupNameByGroupIDAndGroupClassifyName(GroupID, clsGroupNode.GroupClassification_ClassGroup);
                    }
                    NamePaths += GroupName + "/";
                    ValuePaths += GroupID + "/";
                    num++;
                }
                NamePaths = NamePaths.Remove(NamePaths.Length - 1);
                ValuePaths = ValuePaths.Remove(ValuePaths.Length - 1);
                //將所選擇到的Node存入hfNodeValue
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClickScript", "opener.document.getElementById('ctl00_cphContentArea_tbSelectGroupPath').value = '"
                    + NamePaths + "';opener.document.getElementById('ctl00_cphContentArea_hfBelongUnitID').value = '" + ValuePaths +
                    "';window.close();", true);
                break;
            case "MLASSelectUser":
                foreach (string path in paths)
                {
                    string GroupID = "";
                    string GroupName = "";
                    if (num < paths.Length - 1)
                    {
                        GroupID = path.Split('_')[0];
                        GroupName = clsGroup.GetGroupNameByGroupIDAndGroupClassifyName(GroupID, clsGroupNode.GroupClassification_Other);
                    }
                    else if (num < paths.Length)
                    {
                        GroupID = path.Split('_')[0];
                        GroupName = clsGroup.GetGroupNameByGroupIDAndGroupClassifyName(GroupID, clsGroupNode.GroupClassification_SchoolGroup);
                    }
                    else
                    {
                        GroupID = path.Split('_')[0];
                        GroupName = clsGroup.GetGroupNameByGroupIDAndGroupClassifyName(GroupID, clsGroupNode.GroupClassification_ClassGroup);
                    }
                    NamePaths += GroupName + "/";
                    ValuePaths += GroupID + "/";
                    num++;
                }
                NamePaths = NamePaths.Remove(NamePaths.Length - 1);
                ValuePaths = ValuePaths.Remove(ValuePaths.Length - 1);
                //將所選擇到的Node存入hfNodeValue
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClickScript", "opener.document.getElementById('ctl00_cphContentArea_tbSelectGroupPath').value = '"
                    + NamePaths + "';opener.document.getElementById('ctl00_cphContentArea_hfBelongUnitID').value = '" + ValuePaths +
                    "';window.close();", true);
                break;
        }
    }
}