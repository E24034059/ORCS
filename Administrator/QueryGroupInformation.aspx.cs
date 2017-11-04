using ORCS.DB.Administrator;
using ORCS.DB.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Administrator_QueryGroupInformation : System.Web.UI.Page
{
    string strCourseID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GetParameters();

        loadQueryGroupInformation();
    }

    //取得參數
    private void GetParameters()
    {
        //取得課程ID
        if (Request.QueryString["cCourseID"] != null)
        {
            strCourseID = Request.QueryString["cCourseID"].ToString();
        }
    }

    //取得分組資訊
    private void loadQueryGroupInformation()
    {

        #region 標題列
        //定義標題列
        HtmlTableRow tbrTitle = new HtmlTableRow();
        HtmlTableCell tbcTeamNameTitle = new HtmlTableCell();
        tbcTeamNameTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcTeamNameTitle.Width = "50%";
        tbcTeamNameTitle.Align = "center";
        HtmlTableCell tbcTeamMembersNameTitle = new HtmlTableCell();
        tbcTeamMembersNameTitle.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        tbcTeamMembersNameTitle.Width = "50%";
        tbcTeamMembersNameTitle.Align = "center";
        //HtmlTableCell tbcTitleClassStatistics = new HtmlTableCell();
        //tbcTitleClassStatistics.Attributes.Add("Style", "border: 2px solid black; background-color: #CCCCCC; font-weight: bold;");
        //tbcTitleClassStatistics.Align = "center";
        //定義標題列物件 
        Label lbTeamNameTitle = new Label();
        lbTeamNameTitle.Text = "小組名稱";
        Label lbTeamMembersNameTitle = new Label();
        lbTeamMembersNameTitle.Text = "小組成員名稱";
        //Label lbTitleClassStatistics = new Label();
        //lbTitleClassStatistics.Text = "全班統計";
        //將物件加入標題列
        tbcTeamNameTitle.Controls.Add(lbTeamNameTitle);
        tbcTeamMembersNameTitle.Controls.Add(lbTeamMembersNameTitle);
        //tbcTitleClassStatistics.Controls.Add(lbTitleClassStatistics);
        tbrTitle.Cells.Add(tbcTeamNameTitle);
        tbrTitle.Cells.Add(tbcTeamMembersNameTitle);
        tbQueryGroupInformation.Rows.Add(tbrTitle);
        #endregion

        #region 內容
        DataTable dtTempGroup = clsGroup.ORCS_TempGroup_SELECT_By_cClassGroupID(strCourseID);
        //第幾組學生
        int iTimes = 0;
        foreach (DataRow drTempGroup in dtTempGroup.Rows)
        {
            iTimes++;
            HtmlTableRow tbrContent = new HtmlTableRow();
            //小組名稱
            HtmlTableCell tbcTeamNameContent = new HtmlTableCell();
            tbcTeamNameContent.Attributes.Add("Style", "border: 2px solid black; font-weight: bold;");
            tbcTeamNameContent.Align = "center";
            //取得小組成員
            DataTable dtTempMemberGroup = clsEditGroupMember.ORCS_GroupMember_SELECT_by_GroupID_Classify_Authority(drTempGroup["iTempGroupID"], clsGroupNode.GroupClassification_TempGroup, AllSystemUser.Authority_Teacher);
            //取得小組組長ID
            string strGroupLeaderID = clsEditGroupMember.GetGroupLeaderID(drTempGroup["iTempGroupID"].ToString());
            //小組名稱
            Label lbTeamNameContent = new Label();
            //取得小組名單 且要求求顯示小組成員名稱
            lbTeamNameContent.Text = clsGroup.ORCS_TempGroup_SELECT_By_cTempGroupID(drTempGroup["iTempGroupID"].ToString()).Rows[0]["cTempGroupName"].ToString();
            tbcTeamNameContent.Controls.Add(lbTeamNameContent);
            tbcTeamNameContent.RowSpan = dtTempMemberGroup.Rows.Count;
            tbrContent.Cells.Add(tbcTeamNameContent);
            //第幾位學生
            int iStudentTimes = 0;
            foreach (DataRow drTempMemberGroup in dtTempMemberGroup.Rows)
            {
                iStudentTimes++;

                if (iStudentTimes != 1)
                {
                    tbrContent = new HtmlTableRow();
                }
                HtmlTableCell tbcTeamMembersNameContent = new HtmlTableCell();
                tbcTeamMembersNameContent.Attributes.Add("Style", "border: 2px solid black;");
                tbcTeamMembersNameContent.Align = "center";

                if ((iStudentTimes % 2) == 1)
                {
                    tbcTeamMembersNameContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFDFAC;");
                }
                else
                {
                    tbcTeamMembersNameContent.Attributes.Add("Style", "border: 2px solid black; background-color:#FFCAB6;");
                }
                //小組成員名稱
                Label lbTeamMemberNameContent = new Label();
                string strShowText =  clsORCSUser.ORCS_User_SELECT_by_UserID(drTempMemberGroup["cUserID"].ToString()).Rows[0]["cUserName"].ToString();
                if (strGroupLeaderID.Equals(drTempMemberGroup["cUserID"].ToString()))
                {
                    strShowText += "(組長)";
                }
                else
                {
                    strShowText += "(組員)";
                }
                lbTeamMemberNameContent.Text = strShowText;
                tbcTeamMembersNameContent.Controls.Add(lbTeamMemberNameContent);
                tbrContent.Cells.Add(tbcTeamMembersNameContent);
                tbQueryGroupInformation.Rows.Add(tbrContent);
            }
        }
        #endregion
    }

}