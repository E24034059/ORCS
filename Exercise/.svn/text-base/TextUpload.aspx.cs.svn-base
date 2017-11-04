using ORCS.DB.Exercise;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exercise_TextUpload : System.Web.UI.Page
{
    //cExerciseCondID
    string strExerciseCondID = "";
    //cExerciseID
    string strExerciseID = "";
    //課程ID
    string strCourseID = "";
    //作答者ID
    string strAnswerID = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        Getparameter();

        if (!IsPostBack)
        {
            DataTable dtTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, strAnswerID);
            lbUploadTextContent.Text = "上傳文字內容(第" + strExerciseID.Split('_')[1] + "份)";
            //代表已經存過檔
            if (dtTextUploadData.Rows.Count > 0)
                tbContent.Text = dtTextUploadData.Rows[0]["txtTextContent"].ToString();
        }
    }

    private void Getparameter()
    {
        strExerciseCondID = Request.QueryString["cExerciseCondID"].ToString();
        strExerciseID = Request.QueryString["cExerciseID"].ToString();
        strCourseID = Request.QueryString["cGroupID"].ToString();
        strAnswerID = Request.QueryString["cUserID"].ToString();
    }
    protected void btFininsh_Click(object sender, EventArgs e)
    {
        string strTextContent = tbContent.Text;

        DataTable dtTextUploadData = clsExercise.ORCS_TextUploadData_SELECT_by_ExerciseCondID_ExerciseID_UserID(strExerciseCondID, strExerciseID, strAnswerID);
        //代表已經存過檔
        if(dtTextUploadData.Rows.Count > 0)
        {
            clsExercise.ORCS_TextUploadData_UPDATE(strExerciseCondID, strExerciseID, strAnswerID, strTextContent);
        }else
        {
            clsExercise.ORCS_TextUploadData_INSERT(strExerciseCondID, strExerciseID, strAnswerID, strTextContent,strCourseID);
        }

        //更新作答題目參數
        clsExercise.ORCS_ExerciseCondition_UPDATE(strExerciseID, strAnswerID, "1", strCourseID);

        Response.Write("<script>window.close();</script>");
    }


    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }
}