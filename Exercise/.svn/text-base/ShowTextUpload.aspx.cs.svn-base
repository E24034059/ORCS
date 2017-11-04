using ORCS.DB.Administrator;
using ORCS.DB.Exercise;
using ORCS.DB.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exercise_ShowTextUpload : System.Web.UI.Page
{
    //cExerciseCondID
    string strExerciseCondID = "";
    //cExerciseID
    string strExerciseID = "";
    //課程ID
    string strCourseID = "";
    //作答者ID
    string strAnswerID = "";
    //作答模式
    string strAnswerMode = "";
    //使用者權限
    string strAuthority = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Getparameter();

        if (!IsPostBack)
        {
            DataTable dtTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, strAnswerID);
            lbUploadTextContent.Text = "上傳文字內容(第" + strExerciseID.Split('_')[1] + "份)";
            //代表已經存過檔
            if (dtTextUploadData.Rows.Count > 0)
                dContent.InnerHtml = dtTextUploadData.Rows[0]["txtTextContent"].ToString();

        }

        //設定是否匿名
        if (strAuthority.Equals(AllSystemUser.Authority_Teacher))
        {
            dIsAnonymous.Attributes.Add("style", "display:block;text-align:left;");
            if (rblIsAnonymous.SelectedValue.Equals("1"))//代表匿名
            {
                lbUploadTextContent.Text = "上傳文字內容";
            }
            else if (rblIsAnonymous.SelectedValue.Equals("0"))//代表不匿名
            {
                string strAnswerName = "";
                if (strAnswerMode == "1")//取得個人名稱
                    strAnswerName = clsORCSUser.ORCS_User_SELECT_by_UserID(strAnswerID).Rows[0]["cUserName"].ToString();
                else if (strAnswerMode == "2")//取得小組名稱
                    strAnswerName = clsGroup.ORCS_TempGroup_SELECT_By_cTempGroupID(strAnswerID).Rows[0]["cTempGroupName"].ToString();
                lbUploadTextContent.Text = "上傳文字內容(作者:" + strAnswerName + ")";
            }
        }
    }

    private void Getparameter()
    {
        strExerciseCondID = Request.QueryString["cExerciseCondID"].ToString();
        strExerciseID = Request.QueryString["cExerciseID"].ToString();
        strCourseID = Request.QueryString["cGroupID"].ToString();
        strAnswerID = Request.QueryString["cUserID"].ToString();
        strAnswerMode = Request.QueryString["cAnswerMode"].ToString();
        strAuthority = Request.QueryString["cAuthority"].ToString();
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

    //是否匿名
    protected void rblIsAnonymous_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}