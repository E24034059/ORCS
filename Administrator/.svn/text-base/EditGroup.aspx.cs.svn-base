using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ORCS.Util;
using ORCS.DB;
using ORCS.DB.Administrator;
using System.Collections.Generic;

public partial class Administrator_EditGroup : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //頁面初始化
        base.ORCS_Init();
        //載入群組TreeView
        LoadGroupTree();
        if(!IsPostBack)
        {
            //初使化控制項
            btnEditName.Enabled = false;
            btnAddChildUnit.Enabled = false;
            btnDeleteNode.Enabled = false;

            //由Hints說明文件開啟此頁面時只能顯示完成按鈕
            string strTransferFrom = "";
            if (Request.QueryString["TransferFrom"] != null)
                strTransferFrom = Request.QueryString["TransferFrom"].ToString();
            else if(Session["TransferFrom"] != null)
                strTransferFrom = Session["TransferFrom"].ToString();
            if (strTransferFrom != "")
            {
                switch (strTransferFrom)
                {
                    case "Hints_HelpMenu":
                        btnClose.Attributes.Add("style", "font-size:20pt;height:40px;display:block;");
                        Session["TransferFrom"] = strTransferFrom;
                        btnBack.Visible = false;
                        break;
                }
            }

        }
        //加入刪除節點警告視窗
        btnDeleteNode.Attributes.Add("onclick", "return confirm('確定要刪除此單位')");
    }
    //載入群組TreeView
    protected void LoadGroupTree()
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
        List<clsGroupNode> schoolNodeDatas = clsGroup.GetNodeDatasBy_TB_SchoolOrClassOrTempGroup(dtSchoolGroup,clsGroupNode.GroupClassification_SchoolGroup);
        //Get ClassGroup node data list
        DataTable dtClassGroup = clsEditGroup.ORCS_ClassGroup_SELECT_All();
        List<clsGroupNode> classNodeDatas = clsGroup.GetNodeDatasBy_TB_SchoolOrClassOrTempGroup(dtClassGroup,clsGroupNode.GroupClassification_ClassGroup);
        //Get TempGroup node data
        DataTable dtTempGroup = clsEditGroup.ORCS_TempGroup_SELECT_All();
        List<clsGroupNode> tempNodeDatas = clsGroup.GetNodeDatasBy_TB_SchoolOrClassOrTempGroup(dtTempGroup, clsGroupNode.GroupClassification_TempGroup);
        //設定TreeNode ROOT 
        TreeNode tnRoot = new TreeNode();
        foreach (clsGroupNode nodeData in nodeDatas)
        {
            if (nodeData.nodeClassificationName.Equals(clsGroupNode.GroupClassification_Root))
            {
                tnRoot = nodeData.GetTreeNode();
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
                            GetTreeBySchoolClassTempGroupDatas(childTreeNode, clsGroupNode.GroupClassification_TempGroup, schoolNodeDatas, classNodeDatas, tempNodeDatas);
                            beginTreeNod.ChildNodes.Add(childTreeNode);
                            classNodeDatas.Remove(classNodeData);
                            isNotRepeat = true;
                            break;
                        }
                    }
                }
                break;
            case clsGroupNode.GroupClassification_TempGroup:
                while (tempNodeDatas.Count > 0 && isNotRepeat)
                {
                    isNotRepeat = false;
                    foreach (clsGroupNode tempNodeData in tempNodeDatas)
                    {
                        if (tempNodeData.preNodeID.Equals(beginTreeNod.Value.Split('_')[0]))
                        {
                            TreeNode childTreeNode = tempNodeData.GetTreeNode();
                            beginTreeNod.ChildNodes.Add(childTreeNode);
                            tempNodeDatas.Remove(tempNodeData);
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
        btnEditName.Enabled = true;
        btnAddChildUnit.Enabled = true;
        btnDeleteNode.Enabled = true;
        //將所選擇到的Node存入hfNodeValue，供其他按鈕事件使用
        hfNodeValue.Value = tvGroup.SelectedNode.Value;
        //定義所選擇的NodeID
        string strNodeID = tvGroup.SelectedNode.Value.Split('_')[0];
        //定義所選擇的群組
        string strGroup = tvGroup.SelectedNode.Value.Split('_')[1];
        //定義所選擇到的Node名稱
        string strNodeName = tvGroup.SelectedNode.Text;
        //定義所選擇不同的群組所發生之事件
        switch (strGroup)
        {
            case "Root": // 選擇RootNode
                trDeleteUnit.Attributes.Add("style", "display:none;");
                trChildUnit.Attributes.Add("style", "display:block;");
                trChildUnitIsDepartment.Attributes.Add("style", "display:block;");
                lbUnitNodeData.Text = "單位資料";
                //單位名稱
                lbUnitName.Text = "單位名稱";
                tbUnitName.Text = strNodeName;
                //新增子單位名稱
                tbChildUnitName.Visible = true;
                lbChildUnitName.Text = "新增隸屬單位名稱";
                lbChildUnitName.Visible = true;
                btnAddChildUnit.Visible = true;
                btnAddChildUnit.Text = "新增單位"; 
                //刪除單位
                btnDeleteNode.Visible = false;
                btnDeleteNode.Text = "刪除單位";
                //子單位是否為系級
                lbIsNodeDepartment.Visible = true;
                rblIsNodeDepartment.Visible = true;
                if (clsGroup.IsNextNodeClassificationDepartment(strNodeID))
                {
                    rblIsNodeDepartment.Enabled = false;
                    rblIsNodeDepartment.SelectedValue = "Y";
                }
                else
                {
                    rblIsNodeDepartment.Enabled = true;
                    rblIsNodeDepartment.SelectedValue = "N";
                }
                break;
            case "SchoolGroup": // Selected Department
                trDeleteUnit.Attributes.Add("style", "display:block;");
                trChildUnit.Attributes.Add("style", "display:block;");
                trChildUnitIsDepartment.Attributes.Add("style", "display:none;");
                lbUnitNodeData.Text = "部門資料";
                //部門名稱
                lbUnitName.Text = "部門名稱";
                tbUnitName.Text = strNodeName;
                //新增子單位名稱
                lbChildUnitName.Text = "新增隸屬課程名稱";
                lbChildUnitName.Visible = true;
                tbChildUnitName.Visible = false;
                btnAddChildUnit.Visible = true;
                btnAddChildUnit.Text = "新增課程"; 
                //刪除部門
                btnDeleteNode.Visible = true;
                btnDeleteNode.Text = "刪除部門";
                //子單位是否為系級
                lbIsNodeDepartment.Visible = false;
                rblIsNodeDepartment.Visible = false;
                break;
            case "ClassGroup": // Selected Course Node
                trDeleteUnit.Attributes.Add("style", "display:block;");
                trChildUnit.Attributes.Add("style", "display:block;");
                trChildUnitIsDepartment.Attributes.Add("style", "display:none;");
                lbUnitNodeData.Text = "課程資料";
                //課程名稱
                lbUnitName.Text = "課程名稱";
                tbUnitName.Text = strNodeName;
                //新增子單位名稱
                lbChildUnitName.Text = "新增隸屬小組名稱";
                lbChildUnitName.Visible = true;
                tbChildUnitName.Visible = true;
                btnAddChildUnit.Visible = true;
                btnAddChildUnit.Text = "新增小組"; 
                //刪除節點
                btnDeleteNode.Visible = true;
                btnDeleteNode.Text = "刪除課程";
                //子單位是否為系級
                lbIsNodeDepartment.Visible = false;
                rblIsNodeDepartment.Visible = false;
                break;
            case "TempGroup": // 選擇第三層Node
                trDeleteUnit.Attributes.Add("style", "display:block;");
                trChildUnit.Attributes.Add("style", "display:none;");
                trChildUnitIsDepartment.Attributes.Add("style", "display:none;");
                lbUnitNodeData.Text = "小組資料";
                lbUnitName.Text = "小組名稱";
                tbUnitName.Text = strNodeName;
                //新增子單位名稱
                tbChildUnitName.Visible = false;
                lbChildUnitName.Visible = false;
                btnAddChildUnit.Visible = false;
                //刪除小組
                btnDeleteNode.Visible = true;
                btnDeleteNode.Text = "刪除小組";
                //子單位是否為系級
                lbIsNodeDepartment.Visible = false;
                rblIsNodeDepartment.Visible = false;
                break;
            default: //其他節點
                trDeleteUnit.Attributes.Add("style", "display:block;");
                trChildUnit.Attributes.Add("style", "display:block;");
                trChildUnitIsDepartment.Attributes.Add("style", "display:block;");
                lbUnitNodeData.Text = "單位資料";
                //單位名稱
                lbUnitName.Text = "單位名稱";
                tbUnitName.Text = strNodeName;
                //新增子單位名稱
                lbChildUnitName.Text = "新增隸屬單位名稱";
                lbChildUnitName.Visible = true;
                tbChildUnitName.Visible = true;
                btnAddChildUnit.Visible = true;
                btnAddChildUnit.Text = "新增單位"; 
                //刪除單位
                btnDeleteNode.Visible = true;
                btnDeleteNode.Text = "刪除單位";
                //子單位是否為系級
                lbIsNodeDepartment.Visible = true;
                rblIsNodeDepartment.Visible = true;
                if(clsGroup.IsNextNodeClassificationDepartment(strNodeID))
                {
                    rblIsNodeDepartment.Enabled = false;
                    rblIsNodeDepartment.SelectedValue = "Y";
                }
                else
                {
                    rblIsNodeDepartment.Enabled = true;
                    rblIsNodeDepartment.SelectedValue = "N";
                }
                break;
        }
    }
    //「修改名稱」事件
    protected void btnEditName_Click(object sender, EventArgs e)
    {
        //定義所選擇的NodeID
        string strNodeID = hfNodeValue.Value.Split('_')[0];
        //定義所選擇的群組
        string strGroup = hfNodeValue.Value.Split('_')[1];
        //定義所選擇到的Node名稱
        string strNodeName = tbUnitName.Text;
        //若欄位為空則不能增加
        if (strNodeName == "")
        {
            Response.Write("<script>alert('名稱不能為空')</script>");
        }
        else
        {
            //定義所選擇不同的群組所發生之事件
            switch (strGroup)
            {
                case "SchoolGroup": // 選擇第一層Node
                    clsEditGroup.ORCS_SchoolGroup_UPDATE(strNodeID, strNodeName);
                    break;
                case "ClassGroup": // 選擇第二層Node
                    clsEditGroup.ORCS_ClassGroup_UPDATE(strNodeID, strNodeName);
                    break;
                case "TempGroup": // 選擇第三層Node
                    clsEditGroup.ORCS_TempGroup_UPDATE(strNodeID, strNodeName);
                    break;
                default:
                    clsGroup.ORCS_Group_UPDATE_NodeName_BY_NodeID(strNodeID, strNodeName);
                    break;
            }
        }
        //清空名稱欄位
        tbUnitName.Text = "";
        //載入群組TreeView
        LoadGroupTree();
    }
    //「增加」事件
    protected void btnAddChildUnit_Click(object sender, EventArgs e)
    {
        //定義所選擇的NodeID
        string strNodeID = hfNodeValue.Value.Split('_')[0];
        //定義所選擇的群組
        string strGroup = hfNodeValue.Value.Split('_')[1];
        //父節點ID
        string strParentNodeID = hfNodeValue.Value.Split('_')[2];
        //定義所選擇到的Node名稱
        string strChildNodeName = tbChildUnitName.Text;
        //若欄位為空則不能增加
        if (strChildNodeName == "" && strGroup != clsGroupNode.GroupClassification_SchoolGroup)
        {
            Response.Write("<script>alert('新增隸屬單位名稱不能為空')</script>");
        }
        else
        {
            //定義所選擇不同的群組所發生之事件
            switch (strGroup)
            {
                case clsGroupNode.GroupClassification_SchoolGroup:// 選擇單位節點為部門
                    Response.Redirect("AddNewCourse.aspx?DepartmentID=" + strNodeID, true);
                    return;
                case clsGroupNode.GroupClassification_ClassGroup: // 選擇單位節點為課程
                    clsEditGroup.ORCS_TempGroup_INSERT(strChildNodeName, strNodeID);
                    break;
                default:
                    if (rblIsNodeDepartment.SelectedValue.Equals("N"))//新增節點不是系級
                    {
                        clsGroup.AddNodeData(strChildNodeName, strNodeID, null , "0");
                    }else//新增子節點 子節點類別名稱為系級
                    {
                        //設定所選節點 下一個節點類別名稱為系別為True
                        clsGroup.SetNodeIsNextNodeDepartment(strNodeID, "1");
                        //新增子節點 並設子節點的父節點ID為所選節點ID
                        clsEditGroup.ORCS_SchoolGroup_INSERT(strChildNodeName, strNodeID);
                    }
                    break;
            }
        }
        //清空子節點名稱欄位
        tbChildUnitName.Text = "";
        //載入群組TreeView
        LoadGroupTree();
    }
    //「刪除節點」事件
    protected void btnDeleteNode_Click(object sender, EventArgs e)
    {
        //定義所選擇的NodeID
        string strNodeID = hfNodeValue.Value.Split('_')[0];
        //定義所選擇的群組
        string strGroup = hfNodeValue.Value.Split('_')[1];
        //定義所選擇的ParentNodeID
        string strParentNodeID = hfNodeValue.Value.Split('_')[2];
        //刪除不同群組類別所發生之事件
        DeleteNode(strNodeID, strGroup, strParentNodeID);
        //清空TextBox欄位
        tbUnitName.Text = "";
        tbChildUnitName.Text = "";
        //載入群組TreeView
        LoadGroupTree();
    }

    ///刪除不同群組類別所發生之事件
    private void DeleteNode(string strNodeID,string strGroup,string strParentNodeID)
    {
        //定義所選擇不同的群組所發生之事件
        switch (strGroup)
        {
            case "SchoolGroup": // 選擇第一層Node
                //找出子群組並刪除
                DataTable dtClassGroup = clsEditGroup.ORCS_ClassGroup_SELECT_by_ParentGroupID(strNodeID);
                if (dtClassGroup.Rows.Count > 0)
                {
                    //刪除群組人員
                    foreach (DataRow drClassGroup in dtClassGroup.Rows)
                    {
                        DataTable dtTempGroup = clsEditGroup.ORCS_TempGroup_SELECT_by_ParentGroupID(drClassGroup["iClassGroupID"].ToString());
                        if (dtTempGroup.Rows.Count > 0)
                        {
                            foreach (DataRow drTempGroup in dtTempGroup.Rows)
                            {
                                //刪除第三層人員
                                clsEditGroupMember.ORCS_GroupMember_DELETE_by_GroupID_Classify(drTempGroup["iTempGroupID"].ToString(), clsGroupNode.GroupClassification_TempGroup);
                            }
                        }
                        //刪除第二層人員
                        clsEditGroupMember.ORCS_GroupMember_DELETE_by_GroupID_Classify(drClassGroup["iClassGroupID"].ToString(), clsGroupNode.GroupClassification_ClassGroup);
                    }
                    //刪除第一層人員
                    clsEditGroupMember.ORCS_GroupMember_DELETE_by_GroupID_Classify(strNodeID, clsGroupNode.GroupClassification_SchoolGroup);
                    //刪除第三層Node
                    clsEditGroup.ORCS_TempGroup_DELETE_by_ParentGroupID(dtClassGroup.Rows[0]["iClassGroupID"].ToString());
                }
                //刪除第二層Node
                clsEditGroup.ORCS_ClassGroup_DELETE_by_ParentGroupID(strNodeID);
                //刪除第一層Node
                clsEditGroup.ORCS_SchoolGroup_DELETE_by_SchoolGroupID(strNodeID);
                break;
            case "ClassGroup": // 選擇第二層Node
                //刪除群組人員
                DataTable dtTempGroup2 = clsEditGroup.ORCS_TempGroup_SELECT_by_ParentGroupID(strNodeID);
                if (dtTempGroup2.Rows.Count > 0)
                {
                    foreach (DataRow drTempGroup2 in dtTempGroup2.Rows)
                    {
                        //刪除第三層人員
                        clsEditGroupMember.ORCS_GroupMember_DELETE_by_GroupID_Classify(drTempGroup2["iTempGroupID"].ToString(), clsGroupNode.GroupClassification_TempGroup);
                    }
                }
                //刪除第二層人員
                clsEditGroupMember.ORCS_GroupMember_DELETE_by_GroupID_Classify(strNodeID, clsGroupNode.GroupClassification_ClassGroup);
                //刪除第三層Node
                clsEditGroup.ORCS_TempGroup_DELETE_by_ParentGroupID(strNodeID);
                //刪除第二層Node
                clsEditGroup.ORCS_ClassGroup_DELETE_by_ClassGroupID(strNodeID);
                break;
            case "TempGroup": // 選擇第三層Node
                //刪除第三層人員
                clsEditGroupMember.ORCS_GroupMember_DELETE_by_GroupID_Classify(strNodeID, clsGroupNode.GroupClassification_TempGroup);
                //刪除第三層Node
                clsEditGroup.ORCS_TempGroup_DELETE_by_TempGroupID(strNodeID);
                break;
            default:
                if (clsGroup.IsNextNodeClassificationDepartment(strNodeID))//如果子節點類別名稱為系別
                {
                    List<clsGroupNode> schoolNodeDatas = new List<clsGroupNode>();
                    DataTable dtSchoolGroups = clsEditGroup.ORCS_SchoolGroup_SELECT__ChildeNode_by_ParentGroupID(strNodeID);
                    foreach (DataRow drSchoolGroup in dtSchoolGroups.Rows)
                    {
                        clsGroupNode nodeData = new clsGroupNode(drSchoolGroup["iSchoolGroupID"].ToString(), drSchoolGroup["cSchoolGroupName"].ToString()
                        , drSchoolGroup["iParentNodeID"].ToString(), "0", clsGroupNode.GroupClassification_SchoolGroup);
                        schoolNodeDatas.Add(nodeData);
                    }
                    foreach(clsGroupNode schoolNode in schoolNodeDatas)
                        DeleteNode(schoolNode.nodeID, schoolNode.nodeClassificationName, schoolNode.preNodeID);
                    //當子節點都刪除後再刪除所選取節點
                    clsGroup.DeleteNodeByNodeID(strNodeID);
                }
                else
                {
                    //取得所選節點的子節點
                    List<clsGroupNode> ChildNodes = clsGroup.GetChildNodes(strNodeID);
                    if(ChildNodes != null)
                    {
                        foreach(clsGroupNode childNode in ChildNodes)
                        {
                            DeleteNode(childNode.nodeID, childNode.nodeClassificationName, childNode.preNodeID);
                        }
                    }
                    //當子節點都刪除後再刪除所選取節點
                    clsGroup.DeleteNodeByNodeID(strNodeID);
                }
                break;
        }
    }
}
