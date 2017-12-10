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

public partial class Exercise_CreateStuProgramFN : System.Web.UI.Page
{
    private ITaskCorrection TaskCorrectionService;
    protected void Page_Load(object sender, EventArgs e)
    { 
        string StuCouHWDe_ID = Request.Form["StuCouHWDe_ID"];
        string StuProgramFN = Request.Form["StuProgramFN"];
        //Initial StuCouHWDe
        string result = InitStuCouHWDe_prog(StuCouHWDe_ID, StuProgramFN);
        Response.Write(result);
        Response.End();
    }


    //Create Folder to put programs
    [WebMethod]
    public string InitStuCouHWDe_prog(string StuCouHWDe_ID, string StuProgramFN)
    {
        this.TaskCorrectionService = new TaskCorrection();
        TaskCorrectionService.InitStuCouHWDe_prog_record(StuCouHWDe_ID, StuProgramFN);
        
       
        return "Finish create";
        /*
        string strUpdateSQL = "UPDATE RecordMultiFormItem SET Required='true' WHERE MFID='" + FormID + "'";
        CsDBConnection.InsertDataTable(strUpdateSQL);
        return FormID + DateTime.Now.ToString();
        */
    }
}