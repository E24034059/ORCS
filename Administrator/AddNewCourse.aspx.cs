using MLAS.DB.Course;
using ORCS.DB.Administrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util.Course;

public partial class Administrator_AddNewCourse : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        base.ORCS_Init();

        if (!this.IsPostBack)
        {
            lbEditCourse.Text = "編輯課程";
            lbCourseName.Text = "課程名稱";
            lbCourseDescription.Text = "課程大綱";
            lbCourseView.Text = "課程觀看";
            btBack.Text = "回上頁";
            btFinish.Text = "完成";
            rbViewCourse.Text = "一般 (此課程學生才能觀看及操作)";
            rbHindCourse.Text = "隱藏 (所有學生皆無法觀看及操作)";
            rbPublicCourse.Text = "公開 (所有學生都可觀看，但僅此課程學生能操作)";
            lblCourseType.Text = "系列教學活動模組";

            string strDepartmentID = Request["DepartmentID"].ToString();
            //Set departmentID
            hfDepartmentID.Value = strDepartmentID;
            //設定使用者預設所屬單位路徑
            InitBeleUnit(strDepartmentID);
        }

        //設定ServerIP
        IPAddress ServerIP = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
        hfServerIP.Value = ServerIP.ToString();
    }

    /// <summary>
    /// 設定使用者預設所屬單位路徑
    /// </summary>
    private void InitBeleUnit(string strDepartmentID)
    {
        //取得單位所屬路徑
        //hfBelongUnitID.Value = clsGroup.GetUnitPathIDsByCourseID(strBelongCourseID, "Value");
        //tbSelectGroupPath.Value = clsGroup.GetUnitPathIDsByCourseID(strBelongCourseID, "Name");
        hfBelongUnitID.Value = clsGroup.GetUnitPathIDsByDepartmentID(strDepartmentID, "Value");
        tbSelectGroupPath.Value = clsGroup.GetUnitPathIDsByDepartmentID(strDepartmentID, "Name");
    }


    protected void btFinish_Click(object sender, EventArgs e)
    {
        SaveCurrentSetting();
    }

    protected void btBack_Click(object sender ,EventArgs e)
    {
        Response.Redirect("EditGroup.aspx", true);
    }

    protected void SaveCurrentSetting()
    {
        //Get parameters
        string strCourseName = tbCourseName.Text;
        string strDepartmentID = hfDepartmentID.Value;

        //Check for duplicate units
        //If have data return "Have the same course name.Please re-edit course name"
        if (clsGroup.GetGroupIDByGroupName_ParentGroupID_GroupClassification(strCourseName, strDepartmentID, clsGroupNode.GroupClassification_ClassGroup) != null)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "haveSameCourse", "<script>alert('部門內已有相同課程名稱,請重新命名!')</script>", false);
            tbCourseName.Text = "";
            return;
        }
        //Add new course in ORCS Unit
        clsEditGroup.ORCS_ClassGroup_INSERT(strCourseName, strDepartmentID);
        //Get new courseID 
        string strCourseID = clsGroup.GetGroupIDByGroupName_ParentGroupID_GroupClassification(strCourseName, strDepartmentID, clsGroupNode.GroupClassification_ClassGroup);

        //Record MLAS course Data
        string strCourseDescription = tbCourseDescription.Text;
        string strCourseDivision = "";
        string strCourseSubDivision = "";
        string strCourseBelongUnitName = tbSelectGroupPath.Value;
        string strCourseBelongUnitID = hfBelongUnitID.Value;
        // save to database

        // Course mode
        string cCourseViewMode = "";
        if (rbHindCourse.Checked == true)
            cCourseViewMode = MLASCourseViewState.HindCourse;
        else if (rbPublicCourse.Checked == true)
            cCourseViewMode = MLASCourseViewState.PublicCourse;
        else
            cCourseViewMode = MLASCourseViewState.ViewCourse;
        clsMLASCourseInfo.MLASCourse_INSERT(strCourseID, strCourseName, cCourseViewMode, strCourseDescription, strCourseDivision, ddlCourseType.SelectedValue, strCourseSubDivision, strCourseBelongUnitID, strCourseBelongUnitName);
        Response.Redirect("EditGroup.aspx", true);
    }

    protected void ddlCourseType_Init(object sender, EventArgs e)
    {
        ddlCourseType.Controls.Clear();
        ListItem liCourseOtherType = new ListItem();
        liCourseOtherType.Value = "Other";
        liCourseOtherType.Text = "其他";
        ddlCourseType.Items.Add(liCourseOtherType);
        DataTable dtCourseType = clsMLASCourseType.MLAS_CourseType_SELECT_All();
        foreach (DataRow drCourseType in dtCourseType.Rows)
        {
            ListItem liCourseType = new ListItem();
            liCourseType.Value = drCourseType["cCourseTypeName"].ToString();
            liCourseType.Text = drCourseType["cCourseTypeName"].ToString();
            //預設為團體課程
            if (drCourseType["cCourseTypeName"].ToString().Equals("團體課程"))
            {
                liCourseType.Selected = true;
            }
            ddlCourseType.Items.Add(liCourseType);
        }

    }
}