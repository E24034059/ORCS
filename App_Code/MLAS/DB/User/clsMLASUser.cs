using MLAS.Util;
using System.Data;

namespace MLAS.DB.User
{
    /// <summary>
    /// clsMLASUser 的摘要描述
    /// </summary>
    public class clsMLASUser
    {
        protected clsMLASDB MLASDB = new clsMLASDB();
        protected string strSQL = "";

        public clsMLASUser()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        #region SELECT
        public static DataTable MLASUser_SELECT(object cUserID)
        /*cUserID  已經改成編號，不是過去存放使用者ID的欄位了*/
        {
            DataTable dtResult = new DataTable();
            string strSQL = "SELECT * FROM MLAS_User WHERE cUserID LIKE '" + cUserID + "'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                dtResult = MLASDB.GetDataSet(strSQL).Tables[0];
            }
            catch { }

            return dtResult;
        }

        public static DataTable MLASUser_SELECT_by_UserAccount(object cUserID)
        {
            DataTable dtResult = new DataTable();
            string strSQL = "SELECT * FROM MLAS_User WHERE cUserAccount LIKE '" + cUserID + "'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                dtResult = MLASDB.GetDataSet(strSQL).Tables[0];
            }
            catch { }

            return dtResult;
        }
        public static DataTable MLASUser_SELECT_Group(object cGroup, object cUserID)
        {
            DataTable dtResult = new DataTable();
            string strSQL = "SELECT * FROM MLAS_User WHERE cUserID LIKE '" + cUserID + "' AND cGroup LIKE '" + cGroup + "'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                dtResult = MLASDB.GetDataSet(strSQL).Tables[0];
            }
            catch { }

            return dtResult;
        }
        public static DataTable MLASUser_SELECT_DISTINCT_Group()
        {
            DataTable dtResult = new DataTable();
            string strSQL = "SELECT DISTINCT cGroup FROM MLAS_User ";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                dtResult = MLASDB.GetDataSet(strSQL).Tables[0];
            }
            catch { }

            return dtResult;
        }
        public static DataTable MLASUser_SELECT_DISTINCT_Group_byAuthority(object cAuthority)
        {
            DataTable dtResult = new DataTable();
            string strSQL = "SELECT DISTINCT cGroup FROM MLAS_User WHERE cAuthority LIKE '%" + cAuthority + "%'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                dtResult = MLASDB.GetDataSet(strSQL).Tables[0];
            }
            catch { }

            return dtResult;
        }
        public static string MLASUser_SELECT_UserName(object cUserID)
        {
            string name = "";
            DataTable dtUser = clsMLASUser.MLASUser_SELECT(cUserID);
            if (dtUser.Rows.Count > 0)
            {
                name = dtUser.Rows[0]["cUserName"].ToString();
            }

            return name;
        }
        public static string MLASUser_SELECT_Authority(object cUserID)
        {
            string authority = "";
            DataTable dtUser = clsMLASUser.MLASUser_SELECT(cUserID);
            if (dtUser.Rows.Count > 0)
            {
                authority = dtUser.Rows[0]["cAuthority"].ToString();
            }

            return authority;
        }
        public static string MLASUser_SELECT_Group(object cUserID)
        {
            string authority = "";
            DataTable dtUser = clsMLASUser.MLASUser_SELECT(cUserID);
            if (dtUser.Rows.Count > 0)
            {
                authority = dtUser.Rows[0]["cGroup"].ToString();
            }

            return authority;
        }

        public static DataTable MLAS_User_SELECT_ID_Name_by_UserID_Authority(object cUserAccount, object cAuthority)
        {
            clsMLASDB MLASDB = new clsMLASDB();

            DataTable dtUserInfo = new DataTable();
            string strSQL = "SELECT cUserAccount, cUserName FROM MLAS_User WHERE cUserAccount LIKE '" + cUserAccount + "' AND cAuthority LIKE '" + cAuthority + "'";
            dtUserInfo = MLASDB.GetDataSet(strSQL).Tables[0];

            return dtUserInfo;
        }
        #endregion


        #region UPDATE
        public static int MLASUser_UPDATE_Email(object cUserID, object cEmail)
        {
            string strSQL = "UPDATE MLAS_User SET cEmail=@cEmail WHERE cUserID=@cUserID ";
            object[] pList = { cEmail, cUserID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLASUser_UPDATE_Login(object cUserID, object cLastLogin)
        {
            string strSQL = "UPDATE MLAS_User SET cLastLogin=@cLastLogin WHERE cUserID=@cUserID ";
            object[] pList = { cLastLogin, cUserID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLASUser_UPDATE_Passwd(object cUserID, object cPasswdNew)
        {
            string strSQL = "UPDATE MLAS_User SET cPasswd=@cPasswdNew WHERE cUserID LIKE @cUserID ";
            object[] pList = { MLASUtil.HashEncryption(cPasswdNew), cUserID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLASUser_UPDATE_Group(object cUserID, object group)
        {
            string strSQL = "UPDATE MLAS_User SET cGroup=@cGroup WHERE cUserID LIKE @cUserID ";
            object[] pList = { group, cUserID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLASUser_UPDATE_Photo(object cUserID, object cPhoto)
        {
            string strSQL = "UPDATE MLAS_User SET cPhoto=@cPhoto WHERE cUserID LIKE @cUserID ";
            object[] pList = { cPhoto, cUserID };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }

        //add by tctseng 2012 / 02 / 06
        public static int MLASUser_UPDATE_User_Information(object cUserAccount, object cPasswd, object cUserName, object cAuthority, object cEmail, object cYearly, object cHospital, object cOrganization, object cJopGrade, object cBirthday, object cGroup)
        {
            string strSQL = "UPDATE MLAS_User SET  cPasswd=@cPasswd , cUserName=@cUserName , cAuthority=@cAuthority , cEmail=@cEmail , cYearly=@cYearly , cHospital=@cHospital , cOrganization=@cOrganization , cJopGrade=@cJopGrade , cBirthday=@cBirthday , cGroup=@cGroup WHERE cUserAccount LIKE @cUserAccount ";
            object[] pList = { MLASUtil.HashEncryption(cPasswd), cUserName, cAuthority, cEmail, cYearly, cHospital, cOrganization, cJopGrade, cBirthday, cGroup, cUserAccount };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        #endregion


        #region INSERT
        public static int MLASUser_INSERT(object cUserAccount, object cPasswd, object cUserName, object cAuthority, object cEmail, object cYearly, object cHospital, object cOrganization, object cJopGrade, object cBirthday, object cGroup)
        {
            string strSQL = "INSERT MLAS_User(cUserAccount,cPasswd,cUserName,cAuthority,cEmail,cYearly,cHospital,cOrganization,cJopGrade,cBirthday,cGroup) " +
                " VALUES(@cUserAccount,@cPasswd,@cUserName,@cAuthority,@cEmail,@cYearly,@cHospital,@cOrganization,@cJopGrade,@cBirthday,@cGroup) ";
            object[] pList = { cUserAccount, MLASUtil.HashEncryption(cPasswd), cUserName, cAuthority, cEmail, cYearly, cHospital, cOrganization, cJopGrade, cBirthday, cGroup };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }

        #endregion


        #region CHECK
        public static bool MLASUser_CHECK_User(object cUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_User WHERE cUserID='" + cUserID + "'";
            try
            {
                if (MLASDB.ExecuteDataTable(strSQL).Rows.Count > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        public static bool MLASUser_CHECK_Login(object cUserAccount, object cPasswd)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_User WHERE cUserAccount=@cUserAccount AND cPasswd=@cPasswd ";
            object[] pList = { cUserAccount, MLASUtil.HashEncryption(cPasswd) };//密碼解密
            try
            {
                if (MLASDB.ExecuteDataTable(strSQL, pList).Rows.Count > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        #endregion


    }
}