using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AllSystemUser 的摘要描述(整合HINTS,MLAS,ORCS系統使用者)
/// </summary>
public class AllSystemUser
{
    //系級
    List<string> liDepartment = new List<string>();
    //課程
    List<string> liCourses = new List<string>();
    //小組
    List<string> liTeams = new List<string>();

    // <部門<部門,課程<課程,小組>>>
    Dictionary<string, Dictionary<string, Dictionary<string, string>>> ss = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

    /// <summary>
    /// 權限(管理員)
    /// </summary>
    public const string Authority_Administrator = "x";
    /// <summary>
    /// 權限(老師)
    /// </summary>
    public const string Authority_Teacher = "t";
    /// <summary>
    /// 權限(學生)
    /// </summary>
    public const string Authority_Student = "s";
    /// <summary>
    /// 權限(助教)
    /// </summary>
    public const string Authority_Assistant = "a";

    /// <summary>
    /// 使用者ID
    /// </summary>
    public string userID { get; set; }
    /// <summary>
    /// 使用者姓名
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 權限
    /// </summary>
    public string authority { get; set; }
    /// <summary>
    /// 密碼
    /// </summary>
    public string password { get; set; }
    /// <summary>
    /// 郵件地址
    /// </summary>
    public string mailAddress { get; set; }
    /// <summary>
    /// 英文名子
    /// </summary>
    public string englishName { get; set; }
    /// <summary>
    /// Hints課程
    /// </summary>
    public string Hints_Class { get; set; }
    /// <summary>
    /// Hints登入時間
    /// </summary>
    public string Hints_LoginTime { get; set; }
    /// <summary>
    /// 所屬系統
    /// </summary>
    public string Hints_SystemUser { get; set; }
    /// <summary>
    /// 學號
    /// </summary>
    public string Hints_StudentNum { get; set; }
    /// <summary>
    /// Hints是否第一次登入
    /// </summary>
    public string Hints_IsFirstTime { get; set; }
    /// <summary>
    /// MLAS學員編號
    /// </summary>
    public string MLAS_SerialNum { get; set; }
    /// <summary>
    /// MLAS最後登入時間
    /// </summary>
    public string MLAS_LastLogin { get; set; }
    /// <summary>
    /// 所屬群組
    /// </summary>
    public string MLAS_Group { get; set; }
    /// <summary>
    /// 年度
    /// </summary>
    public string MLAS_Yearly { get; set; }
    /// <summary>
    /// 所屬醫院
    /// </summary>
    public string MLAS_Hospital { get; set; }
    /// <summary>
    /// 所屬部門
    /// </summary>
    public string MLAS_Organization { get; set; }
    /// <summary>
    /// 職位
    /// </summary>
    public string MLAS_JopGrade { get; set; }
    /// <summary>
    /// 生日
    /// </summary>
    public string MLAS_Birthday { get; set; }
    /// <summary>
    /// 電話
    /// </summary>
    public string Markit_TelNum { get; set; }



    public AllSystemUser()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }


    /// <summary>
    /// 新增使用者(不包含群組)
    /// </summary>
    public void AddUserData()
    {
        AllSystemUserUtil.AddUserData(userID, authority, password, name,
        englishName, mailAddress, Markit_TelNum, MLAS_Yearly, Hints_StudentNum, 
        MLAS_JopGrade,MLAS_Birthday);
    }
}