using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ORCS.Util;

namespace ORCS.DB
{
    /// <summary>
    /// Summary description for clsSystemControl
    /// </summary>
    public class clsSystemControl
    {
        public clsSystemControl()
        {
        }

    #region SELECT

    /// <summary>
    /// 從SystemControl資料表取得資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <returns></returns>
    public static DataTable ORCS_SystemControl_SELECT(object cSysControlName)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "SELECT * FROM ORCS_SystemControl WHERE cSysControlName LIKE '" + cSysControlName + "' ORDER BY iSysControlID ASC ";
        return ORCSDB.GetDataSet(strSQL).Tables[0];
    }

    /// <summary>
    /// 從SystemControl資料表取得資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="iClassGroupID"></param>
    /// <returns></returns>
    public static DataTable ORCS_SystemControl_SELECT_SysName_GroupID(object cSysControlName, object iClassGroupID)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "SELECT * FROM ORCS_SystemControl WHERE cSysControlName LIKE '" + cSysControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "' ORDER BY iSysControlID ASC ";
        return ORCSDB.GetDataSet(strSQL).Tables[0];
    }

    #endregion

    #region INSERT

    /// <summary>
    /// 在SystemControl資料表存入資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_INSERT(object cSysControlName)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "INSERT INTO ORCS_SystemControl(cSysControlName) VALUES('" + cSysControlName + "')";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// 在SystemControl資料表存入資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="iClassGroupID"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_INSERT_SysName_GroupID(object cSysControlName, object iClassGroupID)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "INSERT INTO ORCS_SystemControl(cSysControlName, iClassGroupID ,iAnswerMode) VALUES('" + cSysControlName + "','" + iClassGroupID + "','0')";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    #endregion

    #region UPDATE

    /// <summary>
    /// 在SystemControl資料表更新資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="iSysControlParam"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_UPDATE(object cSysControlName, object iSysControlParam)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "UPDATE ORCS_SystemControl SET iSysControlParam = '" + iSysControlParam + "' WHERE cSysControlName LIKE '" + cSysControlName + "'";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// 在SystemControl資料表更新資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="iSysControlParam"></param>
    /// <param name="iClassGroupID"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_UPDATE_SysName_Param_GroupID(object cSysControlName, object iSysControlParam, object iClassGroupID)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "UPDATE ORCS_SystemControl SET iSysControlParam = '" + iSysControlParam + "' WHERE cSysControlName LIKE '" + cSysControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// 在SystemControl資料表更新資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="cEndTime"></param>
    /// <param name="iClassGroupID"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_UPDATE_SysName_EndTime_GroupID(object cSysControlName, object cEndTime, object iClassGroupID)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "UPDATE ORCS_SystemControl SET cEndTime = '" + cEndTime + "' WHERE cSysControlName LIKE '" + cSysControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// 在SystemControl資料表更新資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="iSysControlParam"></param>
    /// <param name="cStartTime"></param>
    /// <param name="cEndTime"></param>
    /// <param name="iClassGroupID"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID(object cSysControlName, object iSysControlParam, object cStartTime, object cEndTime, object iClassGroupID)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "UPDATE ORCS_SystemControl SET iSysControlParam = '" + iSysControlParam + "' ,cStartTime = '" + cStartTime + "' ,cEndTime = '" + cEndTime + "' WHERE cSysControlName LIKE '" + cSysControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// 在SystemControl資料表更新資料
    /// </summary>
    /// <param name="cSysControlName"></param>
    /// <param name="iSysControlParam"></param>
    /// <param name="cStartTime"></param>
    /// <param name="cEndTime"></param>
    /// <param name="iClassGroupID"></param>
    /// <param name="iAnswerMode"></param>
    /// <returns></returns>
    public static int ORCS_SystemControl_UPDATE_SysName_Param_StartTime_EndTime_GroupID_AnswerMode(object cSysControlName, object iSysControlParam, object cStartTime, object cEndTime, object iClassGroupID, object iAnswerMode)
    {
        clsORCSDB ORCSDB = new clsORCSDB();
        string strSQL = "UPDATE ORCS_SystemControl SET iSysControlParam = '" + iSysControlParam + "' ,cStartTime = '" + cStartTime + "' ,cEndTime = '" + cEndTime + "' ,iAnswerMode = '" + iAnswerMode + "' WHERE cSysControlName LIKE '" + cSysControlName + "' AND iClassGroupID LIKE '" + iClassGroupID + "'";
        try
        {
            ORCSDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    #endregion

    #region DELETE
    #endregion

    }
}