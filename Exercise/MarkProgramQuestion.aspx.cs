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

public partial class Exercise_MarkProgramQuestion : System.Web.UI.Page
{
    private ITaskCorrection TaskCorrectionService;
    protected void Page_Load(object sender, EventArgs e)
    { 
        string ProgramPath=Request.Form["ProgramPath"];
        string cQID = Request.Form["cQID"];
        string StuCouHWDe_ID = Request.Form["StuCouHWDe_ID"];
        string questionNum = Request.Form["questionNum"];
        string result= MarkProgram(ProgramPath,cQID,StuCouHWDe_ID,questionNum);
        Response.Write(result);
        Response.End();
    }


    //if StuCouHWDe_ID is null, just mark all the programs in the folder.
    [WebMethod]
    public string MarkProgram(string ProgramPath,string cQID,string StuCouHWDe_ID,string questionNum)
    {
        //call program marker
        this.TaskCorrectionService = new TaskCorrection();
        return TaskCorrectionService.CorrectTask( ProgramPath,cQID, StuCouHWDe_ID, questionNum);
        /*
        string strUpdateSQL = "UPDATE RecordMultiFormItem SET Required='true' WHERE MFID='" + FormID + "'";
        CsDBConnection.InsertDataTable(strUpdateSQL);
        return FormID + DateTime.Now.ToString();
        */
    }
}