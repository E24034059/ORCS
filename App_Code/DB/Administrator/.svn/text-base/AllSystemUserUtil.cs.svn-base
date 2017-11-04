using AllSystemDB;
using ORCS.DB.Administrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// AllSystemUserUtil 的摘要描述
/// </summary>
public class AllSystemUserUtil
{

    //Data table(TB)
    public const string TB_UserData = "UserData";
    //Data table field(TF)
    public const string TF_UserData_cUserID = "cUserID";
    public const string TF_UserData_cName = "cName";
    public const string TF_UserData_cPassword = "cPassword";
    public const string TF_UserData_cMailAddress = "cMailAddress";
    public const string TF_UserData_cUserSerialNumber = "cUserSerialNumber";
    //Data table(TB)
    public const string TB_UserHintsExtraData = "UserHintsExtraData";
    //Data table field(TF)
    public const string TF_UserHintsExtraData_cUserSerialNumber = "cUserSerialNumber";
    public const string TF_UserHintsExtraData_cHints_Authority = "cHints_Authority";
    public const string TF_UserHintsExtraData_cHints_Class = "cHints_Class";
    public const string TF_UserHintsExtraData_cHints_EngName = "cHints_EngName";
    public const string TF_UserHintsExtraData_cHints_LoginTime = "cHints_LoginTime";
    public const string TF_UserHintsExtraData_cHints_SystemUser = "cHints_SystemUser";
    public const string TF_UserHintsExtraData_cHints_IdNumber = "cHints_IdNumber";
    public const string TF_UserHintsExtraData_cHints_IsFirstTime = "cHints_IsFirstTime";
    //Data table(TB)
    public const string TB_UserMLASExtraData = "UserMLASExtraData";
    //Data table field(TF)
    public const string TF_UserMLASExtraData_cUserSerialNumber = "cUserSerialNumber";
    public const string TF_UserMLASExtraData_cMLAS_Authority = "cMLAS_Authority";
    public const string TF_UserMLASExtraData_cMLAS_Password_MD5 = "cMLAS_Password_MD5";
    public const string TF_UserMLASExtraData_cMLAS_LastLogin = "cMLAS_LastLogin";
    public const string TF_UserMLASExtraData_cMLAS_Group = "cMLAS_Group";
    public const string TF_UserMLASExtraData_cMLAS_Yearly = "cMLAS_Yearly";
    public const string TF_UserMLASExtraData_cMLAS_Hospital = "cMLAS_Hospital";
    public const string TF_UserMLASExtraData_cMLAS_Organization = "cMLAS_Organization";
    public const string TF_UserMLASExtraData_cMLAS_JobGrade = "cMLAS_JobGrade";
    public const string TF_UserMLASExtraData_cMLAS_Birthday = "cMLAS_Birthday";
    //Data table(TB)
    public const string TB_UserORCSExtraData = "UserORCSExtraData";
    //Data table field(TF)
    public const string TF_UserORCSExtraData_cUserSerialNumber = "cUserSerialNumber";
    public const string TF_UserORCSExtraData_cORCS_Authority = "cORCS_Authority";
    /// <summary>
    /// 系統名稱_Hints
    /// </summary>
    public const string SystemName_Hints = "Hints";
    /// <summary>
    /// 系統名稱_MLAS
    /// </summary>
    public const string SystemName_MLAS = "MLAS";
    /// <summary>
    /// 系統名稱_ORCS
    /// </summary>
    public const string SystemName_ORCS = "ORCS";
    /// <summary>
    /// 系統名稱_Markit
    /// </summary>
    public const string SystemName_Markit = "Markit";

    //連結資料庫類別
    private static clsAllSystemDB sqlAllSystemDB = new clsAllSystemDB();

    public AllSystemUserUtil()
    {
    }


    public static int UpdateUserData(
        string strUserID,
        string strAuthority,
        string strPassword,
        string strChName,
        string strEnName,
        string strMailAddress,
        string strYearly,
        string strStudentNum,
        string strJobGrade,
        string strBirthday
        )
    {
        //取得學生流水號
        string strSerialNum = GetUserSerialNumberByUserID(strUserID);

        string strUpdate_UserData = 
            "UPDATE [NewVersionHintsDB].[dbo].[UserData] SET cName=@cName, " +
            "cPassword=@cPassword, cMailAddress=@cMailAddress " +
            "WHERE cUserID=@cuserID ";

        object[] pList_UserData = { strChName, strPassword, strMailAddress, strUserID };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strUpdate_UserData, pList_UserData);
        }
        catch
        {
            return -1;
        }
        //HINTS
        string strHintsAuthority = GetTransAuthority(SystemName_Hints, strAuthority);
        string strUpdate_UserHintsExtraData =
            "UPDATE [NewVersionHintsDB].[dbo].[UserHintsExtraData] SET "+
            " cHints_Authority=@cHints_Authority, cHints_EngName=@cHints_EngName, cHints_IdNumber=@cHints_IdNumber" +
            " WHERE cUserSerialNumber = " + strSerialNum;
        object[] pList_UserHintsExtraData = { strHintsAuthority, strEnName, strStudentNum };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strUpdate_UserHintsExtraData, pList_UserHintsExtraData);
        }
        catch
        {
            return -1;
        }
        //MLAS
        //密碼(MD5)
        string strPassword_MD5 = FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "md5");
        string strMLASAuthority = GetTransAuthority(SystemName_MLAS, strAuthority);
        string strUpdate_UserMLASExtraData =
            "UPDATE [NewVersionHintsDB].[dbo].[UserMLASExtraData] SET " +
            " cMLAS_Authority=@cMLAS_Authority, cMLAS_Password_MD5=@cMLAS_Password_MD5, cMLAS_Yearly=@cMLAS_Yearly," +
            " cMLAS_JobGrade=@cMLAS_JobGrade,cMLAS_Birthday=@cMLAS_Birthday "+
            " WHERE cUserSerialNumber = " + strSerialNum;
        object[] pList_UserMLASExtraData = { strMLASAuthority, strPassword_MD5, strYearly, strJobGrade, strBirthday };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strUpdate_UserMLASExtraData, pList_UserMLASExtraData);
        }
        catch
        {
            return -1;
        }
        //ORCS
        string strORCSAuthority = GetTransAuthority(SystemName_ORCS, strAuthority);
        string strUpdate_UserORCSExtraData =
            "UPDATE [NewVersionHintsDB].[dbo].[UserORCSExtraData] SET " +
            " cORCS_Authority=@cORCS_Authority " +
            "WHERE cUserSerialNumber = " + strSerialNum;
        object[] pList_UserORCSExtraData = { strORCSAuthority };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strUpdate_UserORCSExtraData, pList_UserORCSExtraData);
        }
        catch
        {
            return -1;
        }

        return 1;
    }

    /// <summary>
    /// 新增使用者資訊
    /// </summary>
    /// <param name="strUserID"></param>
    /// <param name="strAuthority"></param>
    /// <param name="strPassword"></param>
    /// <param name="strChName"></param>
    /// <param name="strEnName"></param>
    /// <param name="strMailAddress"></param>
    /// <param name="strTelNum"></param>
    /// <param name="strYearly"></param>
    /// <param name="strStudentNum"></param>
    /// <param name="strJobGrade"></param>
    /// <param name="strBirthday"></param>
    /// <param name="strDepartmentID"></param>
    /// <param name="strCourseID"></param>
    /// <param name="strGroupID"></param>
    /// <returns></returns>
    public static int AddUserData(
        string strUserID, 
        string strAuthority, 
        string strPassword, 
        string strChName, 
        string strEnName,
        string strMailAddress,
        string strTelNum,
        string strYearly,
        string strStudentNum,
        string strJobGrade,
        string strBirthday
        )
    {

        string strInsert_UserData = 
            "INSERT "+ TB_UserData +
            " (" +
            TF_UserData_cUserID +", "+
            TF_UserData_cName +", "+
            TF_UserData_cPassword +", "+
            TF_UserData_cMailAddress +
            ") " +
            "VALUES (@UserID, @Name, @Password, @MailAddress)";
        object[] pList = { strUserID, strChName, strPassword, strMailAddress };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strInsert_UserData, pList);
        }
        catch
        {
            return -1;
        }
        //取得流水號
        string strSerialNumber = GetUserSerialNumberByUserID(strUserID);
        string strHintsAuthority = GetTransAuthority(SystemName_Hints,strAuthority);
        //新增資料至UserHintsExtraData
        string strInsert_UserHintsExtraData =
            "INSERT " + TB_UserHintsExtraData +
            " (" +
            TF_UserHintsExtraData_cUserSerialNumber + ", " +
            TF_UserHintsExtraData_cHints_Authority + ", " +
            TF_UserHintsExtraData_cHints_Class + ", " +
            TF_UserHintsExtraData_cHints_EngName + ", " +
            TF_UserHintsExtraData_cHints_SystemUser + ", " +
            TF_UserHintsExtraData_cHints_IdNumber + ", " +
            TF_UserHintsExtraData_cHints_IsFirstTime +
            ") VALUES (@UserSerialNumber, @Authority, @Class, @EngName, @SystemUser, @IdNumber, @IsFirstTime)";
        object[] pList_Hints = { strSerialNumber, strHintsAuthority, "MIRAC", strEnName, "nckumc", strStudentNum, "1" };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strInsert_UserHintsExtraData, pList_Hints);
        }
        catch
        {
            return -1;
        }

        //密碼(MD5)
        string strPassword_MD5 = FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "md5");
        string strMLASAuthority = GetTransAuthority(SystemName_MLAS,strAuthority);
        //新增資料至UserHintsExtraData
        string strInsert_UserMLASExtraData =
            "INSERT " + TB_UserMLASExtraData +
            " (" +
            TF_UserMLASExtraData_cUserSerialNumber + ", " +
            TF_UserMLASExtraData_cMLAS_Authority + ", " +
            TF_UserMLASExtraData_cMLAS_Password_MD5 + ", " +
            TF_UserMLASExtraData_cMLAS_Group + ", " +
            TF_UserMLASExtraData_cMLAS_Yearly + ", " +
            TF_UserMLASExtraData_cMLAS_JobGrade + ", " +
            TF_UserMLASExtraData_cMLAS_Birthday +
            ") VALUES (@UserSerialNumber, @Authority, @Password_MD5, @Group, @Yearly, @JobGrade, @Birthday)";
        object[] pList_MLAS = { strSerialNumber, strMLASAuthority, strPassword_MD5, "default", strYearly, strJobGrade, strBirthday };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strInsert_UserMLASExtraData, pList_MLAS);
        }
        catch
        {
            return -1;
        }

        
        string strORCSAuthority = GetTransAuthority(SystemName_ORCS,strAuthority);
        //新增資料至UserORCSExtraData
        string strInsert_UserORCSExtraData =
            "INSERT " + TB_UserORCSExtraData +
            " (" +
            TF_UserORCSExtraData_cUserSerialNumber + ", " +
            TF_UserORCSExtraData_cORCS_Authority +
            ") VALUES (@UserSerialNumber, @Authority)";
        object[] pList_ORCS = { strSerialNumber, strAuthority};
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strInsert_UserORCSExtraData, pList_ORCS);
        }
        catch
        {
            return -1;
        }


        string strMarkitAuthority = GetTransAuthority(SystemName_Markit,strAuthority);
        //新增Markit使用者
        string strInsert_IFUser = "INSERT [IFDB].[dbo].[IF_User] " +
        "(cUserID, iIdentity, cPassword,  cFullName, cMail, cTelephone) " +
        "VALUES (@UserID, @Identity, @Password, @FullName, @MailAddr, @Telephone)";
        object[] pList_Markit = { strUserID, strMarkitAuthority, strPassword, strChName, strMailAddress, strTelNum };
        try
        {
            sqlAllSystemDB.ExecuteNonQuery(strInsert_IFUser, pList_Markit);
        }
        catch
        {
            return -1;
        }

        return 1;
    }



    /// <summary>
    /// 檢查系統有無使用者資料
    /// </summary>
    /// <param name="strSystemName">系統名稱</param>
    /// <param name="strUserID">使用者ID</param>
    /// <returns></returns>
    public static bool HasUserData(string strUserID)
    {
        string strSQL = "SELECT * FROM " + TB_UserData + " WHERE " + TF_UserData_cUserID + "=@UserID";
        object[] pList = { strUserID };
        try
        {
            if (sqlAllSystemDB.ExecuteDataTable(strSQL,pList).Rows.Count > 0)
                return true;
        }
        catch
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// 根據取得UserID取得使用者流水號
    /// </summary>
    public static string GetUserSerialNumberByUserID(string strUserID)
    {
        string strSQL = "SELECT " + TF_UserData_cUserSerialNumber + " FROM " + TB_UserData + " WHERE " + TF_UserData_cUserID + "=@UserID";
        object[] pList = { strUserID };
        try
        {
            DataTable dtUserData = sqlAllSystemDB.ExecuteDataTable(strSQL,pList);
            if (dtUserData.Rows.Count > 0)
            {
                return dtUserData.Rows[0][TF_UserData_cUserSerialNumber].ToString();
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    /// <summary>
    /// 根據不同系統回傳權限值
    /// </summary>
    /// <param name="strSystemName"></param>
    /// <param name="strAuthority"></param>
    /// <returns></returns>
    private static string GetTransAuthority(string strSystemName, string strAuthority)
    {
        switch (strSystemName)
        {
            case SystemName_Hints:
                return strAuthority;
            case SystemName_ORCS:
                return strAuthority;
            case SystemName_MLAS:
                if (strAuthority.Equals(AllSystemUser.Authority_Administrator))
                {
                    return "X";
                }
                return strAuthority;
            case SystemName_Markit:
                if (strAuthority.Equals(AllSystemUser.Authority_Teacher))
                    return "3";
                else
                    return "1";
        }

        return strAuthority;
    }

    //取得Hints使用者身分(根據使用者ID)
    public static string GetAuthourityByUserID(string strUserID)
    {
        string strSQL = "SELECT cIdentity FROM HintsUser WHERE  cUserID=@UserID";
        object[] pList = { strUserID };
        try
        {
            DataTable dtUserData = sqlAllSystemDB.ExecuteDataTable(strSQL, pList);
            if (dtUserData.Rows.Count > 0)
            {
                return dtUserData.Rows[0]["cIdentity"].ToString();
            }
        }
        catch
        {
            return null;
        }
        return null;
    }
}