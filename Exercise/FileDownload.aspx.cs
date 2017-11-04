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
using System.Text;

public partial class Exercise_FileDownload : ORCS.ORCS_Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //兩種做法
        //第一種傳所有參數在這裡組路徑(為了讓Javascript傳值)
        //參數組成:buttonID-exerciseCondID-exerciseID-studentID-fileName
        if (Request.QueryString["exerciseParams"] != null)
        {
            string exerciseParams = HttpUtility.UrlDecode(Request.QueryString["exerciseParams"].ToString(), System.Text.Encoding.UTF8);
            //取得上傳的題目日期
            string strExerciseCondID = exerciseParams.Split('-')[1];
            //取得上傳的題目ID
            string strExerciseID = exerciseParams.Split('-')[2];
            //取得上傳的學生ID
            string strStudentID = exerciseParams.Split('-')[3];
            //取得檔案名稱
            string strFileName = exerciseParams.Split('-')[4];
            //檔案路徑
            string strFilePath = Server.MapPath("~/Exercise/ExFileUpload/" + strExerciseCondID + "/" + strExerciseID + "/" + strStudentID + "/" + strFileName);
            Response.Clear();
            //定義下載的檔案名稱
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName));
            Response.ContentType = @"application/octet-stream";
            System.IO.FileStream downloadFile = new System.IO.FileStream(strFilePath, System.IO.FileMode.Open);
            downloadFile.Close();
            //開始下載
            Response.WriteFile(strFilePath);
            //清除快取
            Response.Flush();
            Response.End();
        }
        else//另一種組好路徑在傳過來fileName和filePath
        {
            string strFileName = Request.QueryString["fileName"].ToString();
            string strFilePath = Request.QueryString["filePath"].ToString();
            Response.Clear();
            //定義下載的檔案名稱
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName));
            Response.ContentType = @"application/octet-stream";
            System.IO.FileStream downloadFile = new System.IO.FileStream(strFilePath, System.IO.FileMode.Open);
            downloadFile.Close();
            //開始下載
            Response.WriteFile(strFilePath);
            //清除快取
            Response.Flush();
            Response.End();
        }
    }
}
