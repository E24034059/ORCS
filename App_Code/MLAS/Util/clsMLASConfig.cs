using MLAS.DB;
using System.Data;

namespace MLAS.Util
{
    /// <summary>
    /// clsMLASConfig 的摘要描述
    /// </summary>
    public class clsMLASConfig
    {
        public clsMLASConfig()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        #region from MLASDB
        #region Time Format
        /// <summary>
        /// 時間的顯示格式。(y：年、M：月、d：日、H：時、m：分、s：秒)
        /// </summary>
        public static string TimeDisplayFormat
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='TimeDisplayFormat'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return dtData.Rows[0]["cValue"].ToString();
                }

                return "";
            }
        }
        #endregion

        #region Mail Setting
        /// <summary>
        /// 伺服器所在網域的寄件伺服器
        /// </summary>
        public static string ServerSMTP
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string sql = "SELECT cValue FROM MLAS_Config WHERE cName='ServerSMTP'";

                string ret = "mail.ncku.edu.tw";    // default value
                DataTable dtValue = MLASDB.GetDataSet(sql).Tables[0];
                if (dtValue.Rows.Count > 0)
                {
                    ret = dtValue.Rows[0][0].ToString();
                }

                return ret;
            }
        }

        /// <summary>
        /// 伺服器所使用的Email Address
        /// </summary>
        public static string ServerMail
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string sql = "SELECT cValue FROM MLAS_Config WHERE cName='ServerMail'";

                string ret = "MLAS System <q3697124@mail.ncku.edu.tw>";    // default value
                DataTable dtValue = MLASDB.GetDataSet(sql).Tables[0];
                if (dtValue.Rows.Count > 0)
                {
                    ret = dtValue.Rows[0][0].ToString();
                }

                return ret;
            }
        }
        #endregion


        #region Login check when coding
        /// <summary>
        /// 是否進行登入檢查
        /// </summary>
        public static bool LoginCheck
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string sql = "SELECT cValue FROM MLAS_Config WHERE cName='LoginCheck' AND cValue LIKE '0'";
                DataTable dtValue = MLASDB.GetDataSet(sql).Tables[0];
                if (dtValue.Rows.Count > 0)
                {
                    return false;
                }

                return true;
            }
        }
        #endregion


        #region enable the multi-language setting
        /// <summary>
        /// 是否開放多國語言設定
        /// </summary>
        public static bool EnableMultiLanguage
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string sql = "SELECT cValue FROM MLAS_Config WHERE cName='MultiLanguageEnabled' AND cValue LIKE '1'";
                DataTable dtValue = MLASDB.GetDataSet(sql).Tables[0];
                if (dtValue.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
        #endregion


        #region Course setting
        /// <summary>
        /// PBL 課程中，每組最多人數上限
        /// </summary>
        public static int PBLMaximumPerGroup
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='PBLMaximumPerGroup'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 1;
            }
        }


        /// <summary>
        /// PBL 課程中，課程最多階段數上限
        /// </summary>
        public static int PBLMaxStagePerCourse
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='PBLMaxStagePerCourse'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 1;
            }
        }
        /// <summary>
        /// PBL 課程中，stage 預設天數
        /// </summary>
        public static int PBLDefaultDayAStage
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='PBLDefaultDayAStage'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 7;
            }
        }
        #endregion


        #region Peer Assessment
        public static int PeerAssessmentMateNumber
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='PeerAssessmentMateNumber'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 1;
            }
        }
        #endregion
        #region Display spliter
        /// <summary>
        /// 螢幕顯示分段符號
        /// </summary>
        public static string DisplaySpliter
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='DisplaySpliter'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return dtData.Rows[0]["cValue"].ToString() + " ";
                }

                return "";
            }
        }
        #endregion
        #region Display Empty
        /// <summary>
        /// 螢幕上代表空的值
        /// </summary>
        public static string DisplayEmpty
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='DisplayEmpty'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return dtData.Rows[0]["cValue"].ToString();
                }

                return "";
            }
        }
        #endregion
        #region Bulletin board
        /// <summary>
        /// 主選單的公告數量
        /// </summary>
        public static int BulletinBoardMessage
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='BulletinBoardMessage'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 3;   // default value
            }
        }
        /// <summary>
        /// 公告數量顯示最新的為幾天內
        /// </summary>
        public static int BulletinBoardLastestDate
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='BulletinBoardLastestDate'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 3;   // default value
            }
        }
        /// <summary>
        /// 公告標題最長顯示字數
        /// </summary>
        public static int BulletinBoardMessageTopicWords
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='BulletinBoardMessageTopicWords'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 30;   // default value
            }
        }
        /// <summary>
        /// 是否在主選單顯示最新公告
        /// </summary>
        public static bool BulletinBoardMenuEnabled
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string sql = "SELECT cValue FROM MLAS_Config WHERE cName='BulletinBoardMenuEnabled' AND cValue LIKE '0'";
                DataTable dtValue = MLASDB.GetDataSet(sql).Tables[0];
                if (dtValue.Rows.Count > 0)
                {
                    return false;
                }

                return true;
            }
        }
        #endregion
        #endregion


        #region 自動Email訊息通知
        /// <summary>
        /// series activity 自動寄信通知 提前天數
        /// </summary>
        public static int iSADayInterval
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='1SeriesActivityInfoDay'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 1;   // default value
            }
        }
        /// <summary>
        /// Activity 自動寄信通知 提前天數
        /// </summary>
        public static int iActivityDayInterval
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='1ActivityInfoDay'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 1;   // default value
            }
        }
        /// <summary>
        /// Activity 截止自動寄信通知 提前天數
        /// </summary>
        public static int iActivityInfoDueDay
        {
            get
            {
                clsMLASDB MLASDB = new clsMLASDB();
                string strSQL = "SELECT cValue FROM MLAS_Config WHERE cName='1ActivityInfoDueDay'";

                DataTable dtData = MLASDB.GetDataSet(strSQL).Tables[0];
                if (dtData.Rows.Count >= 0)
                {
                    return System.Convert.ToInt32(dtData.Rows[0]["cValue"].ToString());
                }

                return 1;   // default value
            }
        }
        #endregion


        #region from Web.config
        #region Language
        /// <summary>
        /// 預設語系
        /// </summary>
        public static string Language
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["language"];
            }
        }
        #endregion
        #region Encryption method
        /// <summary>
        /// 加密方法
        /// </summary>
        public static string Encryption
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["encrypt"];
            }
        }
        #endregion
        #region MasterPage file
        /// <summary>
        /// 主體頁面
        /// </summary>
        public static string MasterFile
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["masterFile"];
            }
        }
        #endregion
        #region RatingItem template
        /// <summary>
        /// RatingItem 編輯樣板
        /// </summary>
        public static string RatingItemEditTemplate
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RatingItemEditTemplate"];
            }
        }
        /// <summary>
        /// RatingItem 編輯樣板
        /// </summary>
        public static string RatingItemViewTemplate
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RatingItemViewTemplate"];
            }
        }
        #endregion

        #region default names
        #region Default group name
        /// <summary>
        /// 預設群組名稱
        /// </summary>
        public static string DefaultGroup
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["defaultGroup"];
            }
        }
        #endregion
        #region Default course name
        /// <summary>
        /// 預設課程名稱
        /// </summary>
        public static string DefaultCourse
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["defaultCourseName"];
            }
        }
        #endregion
        #region Default homework name
        /// <summary>
        /// 預設作業名稱
        /// </summary>
        public static string DefaultHomework
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["defaultHomeworkName"];
            }
        }
        #endregion
        #endregion
        #region Time Format
        /// <summary>
        /// 時間在程式中的格式。(y：年、M：月、d：日、H：時、m：分、s：秒)
        /// </summary>
        public static string TimeProgramFormat
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TimeFormat"];
            }
        }
        #endregion
        #region File directory
        /// <summary>
        /// 檔案在server上的資料夾(回傳虛擬路徑)
        /// </summary>
        public static string FileVirtualDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FileVirtualDirectory"];
            }
        }
        /// <summary>
        /// 檔案在server上的資料夾(回傳實際路徑)
        /// </summary>
        public static string FileServerDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FileServerDirectory"];
            }
        }
        /// <summary>
        /// 說明檔案的資料夾(回傳虛擬路徑)
        /// </summary>
        public static string HelpFileDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HelpFileDirectory"];
            }
        }
        /// <summary>
        /// 說明Flash的資料夾(回傳虛擬路徑)
        /// </summary>
        public static string HelpFlashDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HelpFlashDirectory"];
            }
        }
        #endregion

        #region work with other system
        public static bool Markit
        {
            get
            {
                bool bRet = (System.Configuration.ConfigurationManager.AppSettings["WithMarkit"] == "0") ? false : true;
                return bRet;
            }
        }
        public static bool Moodle
        {
            get
            {
                bool bRet = (System.Configuration.ConfigurationManager.AppSettings["WithMoodle"] == "0") ? false : true;
                return bRet;
            }
        }
        #endregion

        /// <summary>
        /// 取得指定key值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetWebConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        #endregion

        #region web access to MLAS_Config
        public static DataTable MLASConfig_SELECT(object cName)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_Config WHERE cName LIKE '" + cName + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static int MLASConfig_UPDATE(object cName, object cValue)
        {
            string strSQL = "UPDATE MLAS_Config SET cValue=@cValue WHERE cName LIKE @cName";
            object[] pList = { cValue, cName };
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
    }
}