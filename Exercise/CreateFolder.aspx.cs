using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using gradeprogram.Service;
using gradeprogram.Service.Interface;
using gradeprogram.Models;

public partial class Exercise_CreateFolder : System.Web.UI.Page
{
    string assignmentPath=@"C:\Users\chiu\Desktop\Integrate_Program_comparing_to_Hints\gradeprogram\gradeprogram\HWfile";
    protected void Page_Load(object sender, EventArgs e)
    { 
        string cCourseName = Request.Form["cCourseName"];
        string HW_Exam_Number = Request.Form["HW_Exam_Number"];
        string QuestionNumber = Request.Form["QuestionNumber"];
        //call the program marking function implemented in gradeprogram project
        string result = createfolder(cCourseName, HW_Exam_Number, QuestionNumber);
        Response.Write(result);
        Response.End();
    }


    //Create Folder to put programs
    [WebMethod]
    public string createfolder(string cCourseName, string HW_Exam_Number,string QuestionNumber)
    {
        //call program marker
        HWPathHelper s = new HWPathHelper(assignmentPath+@"\"+cCourseName+@"\"+HW_Exam_Number+@"\"+QuestionNumber);
        s.HWPathdir();
        return "Finish create";
        /*
        string strUpdateSQL = "UPDATE RecordMultiFormItem SET Required='true' WHERE MFID='" + FormID + "'";
        CsDBConnection.InsertDataTable(strUpdateSQL);
        return FormID + DateTime.Now.ToString();
        */
    }
}