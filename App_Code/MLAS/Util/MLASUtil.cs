using MLAS.DB;
using MLAS.DB.Activity;
using MLAS.DB.Course;
using MLAS.DB.Role;
using MLAS.DB.SeriesActivity;
using MLAS.DB.User;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Security;

namespace MLAS.Util
{
    /// <summary>
    /// MLASUtil 的摘要描述
    /// </summary>
    public class MLASUtil
    {
        public MLASUtil()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// Get new ID by time
        /// </summary>
        /// <param name="strPrefix">prefix before time</param>
        /// <returns>New ID as string</returns>
        public static string GetNewID(string strPrefix)
        {
            return strPrefix + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        #region datetime conversion
        /// <summary>
        /// Convert string to DateTime, the input string format should be 'yyyyMMddHHmmss'.
        /// </summary>
        /// <param name="strTime">yyyyMMddHHmmss</param>
        /// <returns>DateTime format</returns>
        public static DateTime ConvertHintsTimeToDateTime(string strTime)
        {
            // yyyyMMddHHmmss
            // 01234567890123
            int year = System.Convert.ToInt32(strTime.Substring(0, 4));
            int month = System.Convert.ToInt32(strTime.Substring(4, 2));
            int day = System.Convert.ToInt32(strTime.Substring(6, 2));
            int hour = System.Convert.ToInt32(strTime.Substring(8, 2));
            int minute = System.Convert.ToInt32(strTime.Substring(10, 2));
            int second = System.Convert.ToInt32(strTime.Substring(12, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Convert string to display format, the input string format should be 'yyyyMMddHHmmss'.
        /// </summary>
        /// <param name="hintsTime">yyyyMMddHHmmss</param>
        /// <returns>display format as string</returns>
        public static string ConvertHintsTimeToDisplay(string hintsTime)
        {
            DateTime dtHintsTime = MLASUtil.ConvertHintsTimeToDateTime(hintsTime);
            return dtHintsTime.ToString(clsMLASConfig.TimeDisplayFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string ConvertHintsTimeToDisplay_yymmddhhmm(string strTime)
        {
            // yyyyMMddHHmmss
            int year = System.Convert.ToInt32(strTime.Substring(0, 4));
            int month = System.Convert.ToInt32(strTime.Substring(4, 2));
            int day = System.Convert.ToInt32(strTime.Substring(6, 2));
            string hour = strTime.Substring(8, 2);
            string minute = strTime.Substring(10, 2);

            string cTime = year + "/" + month + "/" + day + " " + hour + ":" + minute;
            return cTime;
        }
        #endregion

        /// <summary>
        /// Shorten the string if necessary
        /// </summary>
        /// <param name="word"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FixLengthString(string word, int length)
        {
            string ret = (word.Length > length) ? word.Substring(0, length - 2) + ".." : word;
            return ret;
        }
        /// <summary>
        /// string encryption
        /// </summary>
        /// <param name="objstr"></param>
        /// <returns></returns>
        public static string HashEncryption(object objstr)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(objstr.ToString(), clsMLASConfig.Encryption);
        }

        // if target time is due, return true
        public static bool IsCurrentTimeDue(string strTime)
        {
            Int64 iCurrentTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss").ToString());
            Int64 iTargetTime = Convert.ToInt64(strTime);
            return (iCurrentTime > iTargetTime) ? true : false;
        }


        public static void SendHtmlMail(string p_strToUser, string p_strSubject, string p_strBody)
        {
            //設定寄件者的電子郵件地址 //設定以分號區隔的接收者電子郵件地址清單
            MailMessage myMail = new MailMessage(clsMLASConfig.ServerMail, p_strToUser);
            myMail.Subject = p_strSubject;                       //設定電子郵件訊息的主旨
            myMail.Body = p_strBody;                             //設定電子郵件訊息的主體

            myMail.IsBodyHtml = true;

            //取得或設定 SMTP 轉接郵件伺服器的名稱，用來傳送電子郵件訊息。
            SmtpClient client = new SmtpClient(clsMLASConfig.ServerSMTP);
            try
            {
                //多載。 傳送電子郵件訊息。
                client.Send(myMail);
            }
            catch { }
        }
        /// <summary>
        /// send server mail with attachment
        /// </summary>
        /// <param name="p_strToUser"></param>
        /// <param name="p_strSubject"></param>
        /// <param name="p_strBody"></param>
        /// <param name="attachments">attached file as an arry list</param>
        public static void SendHtmlMailWithAttachments(string p_strToUser, string p_strSubject, string p_strBody, ArrayList attachments)
        {
            //設定寄件者的電子郵件地址 //設定以分號區隔的接收者電子郵件地址清單
            MailMessage myMail = new MailMessage(clsMLASConfig.ServerMail, p_strToUser);
            myMail.Subject = p_strSubject;                       //設定電子郵件訊息的主旨
            myMail.Body = p_strBody;                             //設定電子郵件訊息的主體

            foreach (object attach in attachments)
            {
                Attachment myAttachment = new Attachment(attach.ToString());
                myMail.Attachments.Add(myAttachment);
            }

            myMail.IsBodyHtml = true;

            //取得或設定 SMTP 轉接郵件伺服器的名稱，用來傳送電子郵件訊息。
            SmtpClient client = new SmtpClient(clsMLASConfig.ServerSMTP);
            try
            {
                //多載。 傳送電子郵件訊息。
                client.Send(myMail);
            }
            catch { }
        }

        // 計算距離活動截止時間
        public static string ActivityDueTimeSecond(string cActivityID)
        {
            string DueTime = "";

            DataTable dtActivityEndTime = clsMLASActivityBasicInfo.MLAS_ActivityBasicInfo_Select(cActivityID);
            if (dtActivityEndTime.Rows[0]["cActivityEndTime"] != DBNull.Value)
            {
                if (dtActivityEndTime.Rows[0]["cActivityEndTime"].ToString() != "")
                {
                    DateTime datCurrentTime = ConvertHintsTimeToDateTime(DateTime.Now.ToString("yyyyMMddHHmmss"));

                    DateTime datActivityEndTime = ConvertHintsTimeToDateTime(dtActivityEndTime.Rows[0]["cActivityEndTime"].ToString());

                    TimeSpan datFidd = new TimeSpan(datActivityEndTime.Ticks - datCurrentTime.Ticks);

                    DueTime = "0";

                    int iTotalSeconds = 0;
                    if (datFidd.Days > 0)
                        iTotalSeconds += datFidd.Days * 24 * 60 * 60;
                    if (datFidd.Hours > 0)
                        iTotalSeconds += datFidd.Hours * 60 * 60;
                    if (datFidd.Minutes > 0)
                        iTotalSeconds += datFidd.Minutes * 60;
                    if (datFidd.Seconds > 0)
                        iTotalSeconds += datFidd.Seconds;

                    if (iTotalSeconds > 0)
                        DueTime = iTotalSeconds.ToString();

                }
            }

            return DueTime;
        }

        public static string DueTimeSecond(DateTime datActivityEndTime)
        {
            string DueTime = "0";

            DateTime datCurrentTime = ConvertHintsTimeToDateTime(DateTime.Now.ToString("yyyyMMddHHmmss"));

            TimeSpan datFidd = new TimeSpan(datActivityEndTime.Ticks - datCurrentTime.Ticks);

            DueTime = "0";

            int iTotalSeconds = 0;
            if (datFidd.Days > 0)
                iTotalSeconds += datFidd.Days * 24 * 60 * 60;
            if (datFidd.Hours > 0)
                iTotalSeconds += datFidd.Hours * 60 * 60;
            if (datFidd.Minutes > 0)
                iTotalSeconds += datFidd.Minutes * 60;
            if (datFidd.Seconds > 0)
                iTotalSeconds += datFidd.Seconds;

            if (iTotalSeconds > 0)
                DueTime = iTotalSeconds.ToString();

            return DueTime;
        }

        /// <summary>
        /// 新增活動通知信件
        /// </summary>
        /// <param name="Mail"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="Assigner"></param>
        /// <param name="Deadline"></param>
        public static void InsertActivityNotifyMail(string Mail, string Subject, string Body, string Assigner, string Deadline)
        {

            SqlConnection conn = new SqlConnection("Data Source=140.116.177.79;Initial Catalog=NewVersionHintsDB;User ID=hints;Password=mirac");
            conn.Open();
            string command = "INSERT INTO AssignCaseMailRemindList(cAssignID,cAssigner,cMailSubject,cMailBody,cMailTo,dDeadline,cSystem,nStatus) VALUES(";
            command += "'" + MLASUtil.GetNewID("") + "','" + Assigner + "','" + Subject + "','" + Body + "','" + Mail + "','" + Deadline + "','MLAS', 0)";
            SqlCommand cmd = new SqlCommand(command, conn);
            int iExcute = cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// 更新活動通知信件
        /// </summary>
        /// <param name="Mail"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="Deadline"></param>
        /// <param name="Status"></param>
        /// <param name="AssignID"></param>
        public static void UpdateActivityNotifyMail(string Mail, string Subject, string Body, string Deadline, string Status, string AssignID)
        {

            SqlConnection conn = new SqlConnection("Data Source=140.116.177.79;Initial Catalog=NewVersionHintsDB;User ID=hints;Password=mirac");
            conn.Open();
            string command = "UPDATE AssignCaseMailRemindList SET ";
            command += "cMailTo = '" + Mail + "', cMailSubject = '" + Subject + "', cMailBody = '" + Body + "', dDeadline = '" + Deadline + "', nStatus = '" + Status + "'";
            command += "where (cAssignID = '" + AssignID + "')";
            SqlCommand cmd = new SqlCommand(command, conn);
            int iExcute = cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// 刪除活動通知信件
        /// </summary>
        /// <param name="AssignID"></param>
        public static void DeleteActivityNotifyMail(string AssignID)
        {
            SqlConnection conn = new SqlConnection("Data Source=140.116.177.79;Initial Catalog=NewVersionHintsDB;User ID=hints;Password=mirac");
            conn.Open();
            string command = "DELETE From AssignCaseMailRemindList Where cAssignID ='" + AssignID + "'";
            SqlCommand cmd = new SqlCommand(command, conn);
            int iExcute = cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 新增活動通知手機簡訊 PhoneMessage為備份與測試的資料表 PhoneMessageX為醫學院資訊室取的資料的資料表
        /// </summary>
        /// <param name="Employee_no"></param>
        /// <param name="Msg"></param>
        public static void InsertActivityNotifyPhone(string Msg, string BBCall, string Sender)
        {
            string strCreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            //存到資訊室(140.116.60.111)做簡訊傳送 若下載到本機端請註解下面程式碼 避免測試時傳送簡訊
            clsMLASDB NCKMDB = new clsMLASDB(System.Configuration.ConfigurationManager.ConnectionStrings["connNCKMDB"].ToString());
            string strSQL_PhoneMessageX = "INSERT INTO PhoneMessageX(employee_no,sender,msg,source,bbcall,create_time) VALUES(";
            strSQL_PhoneMessageX += "'MR0950','" + Sender + "','" + Msg + "','R','" + BBCall + "','" + strCreateTime + "')";
            NCKMDB.ExecuteNonQuery(strSQL_PhoneMessageX);
        }
        /// <summary>
        /// 更新活動通知手機簡訊
        /// </summary>
        /// <param name="Employee_no"></param>
        /// <param name="Msg"></param>
        /// <param name="Key"></param>
        public static void UpdateActivityNotifyPhone(string Employee_no, string Msg, string Key)
        {
            clsMLASDB MLASDB = new clsMLASDB(System.Configuration.ConfigurationManager.ConnectionStrings["connHintsDB"].ToString());
            string strCreateTime = DateTime.Now.ToString("yyyy/MM/dd");
            string strSQL_PhoneMessage = "UPDATE PhoneMessage SET ";
            strSQL_PhoneMessage += "employee_no = '" + Employee_no + "', msg = '" + Msg + "', bbcall = '8888', create_time = '" + strCreateTime + "', proc_time = ''";
            strSQL_PhoneMessage += "where (pkey = '" + Key + "')";
            MLASDB.ExecuteNonQuery(strSQL_PhoneMessage);
        }
        /// <summary>
        /// 刪除活動通知手機簡訊
        /// </summary>
        /// <param name="Key"></param>
        public static void DeleteActivityNotifyPhone(string Key)
        {
            clsMLASDB MLASDB = new clsMLASDB(System.Configuration.ConfigurationManager.ConnectionStrings["connHintsDB"].ToString());
            string strSQL_PhoneMessage = "DELETE From PhoneMessage Where pkey ='" + Key + "'";
            MLASDB.ExecuteNonQuery(strSQL_PhoneMessage);
        }

        /// <summary>
        /// 活動開始前 寄送通知的信件給予角色的使用者
        /// </summary>
        /// <param name="strActivityID"></param>
        /// <param name="strActivityStep"></param>
        public static void sendNotStartedActivityMessage(string strActivityID, string strActivityStep)
        {
            //根據活動ID與活動進行狀態取得資料
            DataTable dtMLAS_ActivityMessage = clsMLASActivityMessage.MLAS_ActivityMessage_Select(strActivityID, "Before");
            if (dtMLAS_ActivityMessage.Rows.Count > 0)
            {
                foreach (DataRow drMLAS_ActivityMessage in dtMLAS_ActivityMessage.Rows)
                {
                    //收件者的角色
                    String strArrayRecipients = drMLAS_ActivityMessage["cRoleID"].ToString();
                    //信件的內容
                    String strMessageContent = drMLAS_ActivityMessage["cMessageContent"].ToString();

                    //收件者的mail  
                    String strRecipientsMail = "";
                    //課程的ID
                    String strCourseID = clsMLASVIEWCourseToActivity.VIEWCourseToActivity_SELECT_Activity(strActivityID).Rows[0]["cCourseID"].ToString();

                    #region loop 取得收件者的MAIL 並以"," 隔開
                    if (strArrayRecipients == "Teacher")
                    {
                        strRecipientsMail = getTeacherMail(strCourseID);
                    }
                    else if (strArrayRecipients == "All")
                    {
                        strRecipientsMail = getCourseAllUserMail(strCourseID);
                    }
                    else if (strArrayRecipients == "Others")
                    {
                        strRecipientsMail = getOthersMail(strCourseID, strActivityID);
                    }
                    else
                    {
                        strRecipientsMail = getRoleUserMail(strActivityID, strArrayRecipients);
                    }
                    #endregion

                    //寄送MAIL
                    if (strRecipientsMail != "")
                        sendMail("MLAS系統 教學活動通知", "您好: <br/>" + strMessageContent + "<br /> MLAS系統管理員 敬上", strRecipientsMail, "");
                }
            }
        }

        /// <summary>
        /// 活動開始後 寄送通知的信件給予角色的使用者
        /// </summary>
        /// <param name="strActivityID"></param>
        /// <param name="strActivityStep"></param>
        /// <param name="strUserID"></param>
        public static void sendStartActivityMessage(string strActivityID, string strActivityStep, string strUserID, string strSeriesActivityID)
        {
            //課程的ID
            String strCourseID = clsMLASVIEWCourseToActivity.VIEWCourseToActivity_SELECT_Activity(strActivityID).Rows[0]["cCourseID"].ToString();

            //取得使用者是否具有角色ID
            DataTable dtMLAS_ActivityUserRoleJoinMLAS_Role = new DataTable();
            dtMLAS_ActivityUserRoleJoinMLAS_Role = getUserRoleID(strActivityID, strUserID);

            if (dtMLAS_ActivityUserRoleJoinMLAS_Role.Rows.Count > 0)
            {
                #region 具有角色ID
                //取得角色ID
                String strRoleID = dtMLAS_ActivityUserRoleJoinMLAS_Role.Rows[0]["cRoleID"].ToString();
                #region 取得此角色ID是否有寄送活動訊息條件
                DataTable dtMLAS_ActivityMessage = clsMLASActivityMessage.MLAS_ActivityMessage_Select(strActivityID, strActivityStep, strRoleID);
                #endregion
                if (dtMLAS_ActivityMessage.Rows.Count > 0)
                {
                    //所有收件者的角色
                    String[] strArrayRecipients = dtMLAS_ActivityMessage.Rows[0]["cRecipients"].ToString().Split(';');
                    //所有副本的角色
                    String[] strArrayCarbonCopy = dtMLAS_ActivityMessage.Rows[0]["cCarbonCopy"].ToString().Split(';');
                    //信件的內容
                    String strMessageContent = dtMLAS_ActivityMessage.Rows[0]["cMessageContent"].ToString();

                    //收件者的mail  
                    String strRecipientsMail = "";
                    //副本的mail
                    String strCarbonCopyMail = "";
                    //是否分組
                    String strGroup = "";
                    strGroup = clsMLASSeriesActivityMetadata.MLAS_SeriesActivityMetadata_Select_Parameter(strSeriesActivityID, "cGroupType");
                    //有分組
                    if (strGroup != "No" && strGroup != "")
                    {
                        #region 有分組
                        //取得組別ID
                        DataTable dtGroupID = clsMLASSeriesActivityUser.MLAS_SeriesActivityUser_Select_user(strSeriesActivityID, strUserID);
                        String strGroupID = dtGroupID.Rows[0]["cSeriesActivityUserGroupID"].ToString();
                        DataTable dtMLAS_SeriesActivityUser = clsMLASSeriesActivityUser.MLAS_SeriesActivityUser_Select_group_specific(strSeriesActivityID, strGroupID);
                        if (dtMLAS_SeriesActivityUser.Rows.Count > 0)
                        {
                            foreach (DataRow drMLAS_SeriesActivityUser in dtMLAS_SeriesActivityUser.Rows)
                            {
                                if (drMLAS_SeriesActivityUser["cUserID"].ToString() != strUserID)
                                {
                                    #region loop 取得收件者的MAIL 並以"," 隔開
                                    for (int iArrayRecipients = 0; iArrayRecipients < strArrayRecipients.Length; iArrayRecipients++)
                                    {
                                        if (strArrayRecipients[iArrayRecipients] != "")
                                        {
                                            if (strArrayRecipients[iArrayRecipients] == "Teacher")
                                            {
                                                strRecipientsMail += getTeacherMail(strCourseID);
                                            }
                                            else if (strArrayRecipients[iArrayRecipients] == "All")
                                            {
                                                strRecipientsMail += getCourseAllUserMail(strCourseID);
                                            }
                                            else if (strArrayRecipients[iArrayRecipients] == "Others")
                                            {
                                                strRecipientsMail += getOthersMail(strCourseID, strActivityID);
                                            }
                                            else
                                            {
                                                strRecipientsMail += getRoleUserMail_ByGroup(strActivityID, strArrayRecipients[iArrayRecipients].ToString(), drMLAS_SeriesActivityUser["cUserID"].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region loop 取得副本的MAIL 並以"," 隔開
                                    for (int iArrayCarbonCopy = 0; iArrayCarbonCopy < strArrayCarbonCopy.Length; iArrayCarbonCopy++)
                                    {
                                        if (strArrayCarbonCopy[iArrayCarbonCopy] != "")
                                        {
                                            if (strArrayCarbonCopy[iArrayCarbonCopy] == "Teacher")
                                            {
                                                strCarbonCopyMail += getTeacherMail(strCourseID);
                                            }
                                            else if (strArrayCarbonCopy[iArrayCarbonCopy] == "All")
                                            {
                                                strCarbonCopyMail += getCourseAllUserMail(strCourseID);
                                            }
                                            else if (strArrayCarbonCopy[iArrayCarbonCopy] == "Others")
                                            {
                                                strCarbonCopyMail += getOthersMail(strCourseID, strActivityID);
                                            }
                                            else
                                            {
                                                strCarbonCopyMail += getRoleUserMail_ByGroup(strActivityID, strArrayCarbonCopy[iArrayCarbonCopy].ToString(), drMLAS_SeriesActivityUser["cUserID"].ToString());
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 沒分組

                        #region loop 取得收件者的MAIL 並以"," 隔開
                        for (int iArrayRecipients = 0; iArrayRecipients < strArrayRecipients.Length; iArrayRecipients++)
                        {
                            if (strArrayRecipients[iArrayRecipients] != "")
                            {
                                if (strArrayRecipients[iArrayRecipients] == "Teacher")
                                {
                                    strRecipientsMail += getTeacherMail(strCourseID);
                                }
                                else if (strArrayRecipients[iArrayRecipients] == "All")
                                {
                                    strRecipientsMail += getCourseAllUserMail(strCourseID);
                                }
                                else if (strArrayRecipients[iArrayRecipients] == "Others")
                                {
                                    strRecipientsMail += getOthersMail(strCourseID, strActivityID);
                                }
                                else
                                {
                                    strRecipientsMail += getRoleUserMail(strActivityID, strArrayRecipients[iArrayRecipients].ToString());
                                }
                            }
                        }
                        #endregion

                        #region loop 取得副本的MAIL 並以"," 隔開
                        for (int iArrayCarbonCopy = 0; iArrayCarbonCopy < strArrayCarbonCopy.Length; iArrayCarbonCopy++)
                        {
                            if (strArrayCarbonCopy[iArrayCarbonCopy] != "")
                            {
                                if (strArrayCarbonCopy[iArrayCarbonCopy] == "Teacher")
                                {
                                    strCarbonCopyMail += getTeacherMail(strCourseID);
                                }
                                else if (strArrayCarbonCopy[iArrayCarbonCopy] == "All")
                                {
                                    strCarbonCopyMail += getCourseAllUserMail(strCourseID);
                                }
                                else if (strArrayCarbonCopy[iArrayCarbonCopy] == "Others")
                                {
                                    strCarbonCopyMail += getOthersMail(strCourseID, strActivityID);
                                }
                                else
                                {
                                    strCarbonCopyMail += getRoleUserMail(strActivityID, strArrayCarbonCopy[iArrayCarbonCopy].ToString());
                                }

                            }
                        }
                        #endregion

                        #endregion
                    }

                    //寄送MAIL
                    if (strRecipientsMail != "")
                        sendMail("MLAS系統 教學活動通知", strMessageContent, strRecipientsMail, strCarbonCopyMail);

                }
                #endregion
            }
            else
            {
                #region 不具有角色ID

                ArrayList alNotRoleID = new ArrayList();
                alNotRoleID.Add("Teacher");
                alNotRoleID.Add("All");
                alNotRoleID.Add("Others");

                #region 取得此角色ID是否有寄送活動訊息條件
                for (int i = 0; i < alNotRoleID.Count; i++)
                {
                    DataTable dtMLAS_ActivityMessage = clsMLASActivityMessage.MLAS_ActivityMessage_Select(strActivityID, strActivityStep, alNotRoleID[i]);
                    if (dtMLAS_ActivityMessage.Rows.Count > 0)
                    {
                        //所有收件者的角色
                        String[] strArrayRecipients = dtMLAS_ActivityMessage.Rows[0]["cRecipients"].ToString().Split(';');
                        //所有副本的角色
                        String[] strArrayCarbonCopy = dtMLAS_ActivityMessage.Rows[0]["cCarbonCopy"].ToString().Split(';');
                        //信件的內容
                        String strMessageContent = dtMLAS_ActivityMessage.Rows[0]["cMessageContent"].ToString();

                        //收件者的mail  
                        String strRecipientsMail = "";
                        //副本的mail
                        String strCarbonCopyMail = "";

                        #region loop 取得收件者的MAIL 並以"," 隔開
                        for (int iArrayRecipients = 0; iArrayRecipients < strArrayRecipients.Length; iArrayRecipients++)
                        {
                            if (strArrayRecipients[iArrayRecipients] != "")
                            {
                                if (strArrayRecipients[iArrayRecipients] == "Teacher")
                                {
                                    strRecipientsMail += getTeacherMail(strCourseID);
                                }
                                else if (strArrayRecipients[iArrayRecipients] == "All")
                                {
                                    strRecipientsMail += getCourseAllUserMail(strCourseID);
                                }
                                else if (strArrayRecipients[iArrayRecipients] == "Others")
                                {
                                    strRecipientsMail += getOthersMail(strCourseID, strActivityID);
                                }
                                else
                                {
                                    strRecipientsMail += getRoleUserMail(strActivityID, strArrayRecipients[iArrayRecipients].ToString());
                                }
                            }
                        }
                        #endregion

                        #region loop 取得副本的MAIL 並以"," 隔開
                        for (int iArrayCarbonCopy = 0; iArrayCarbonCopy < strArrayCarbonCopy.Length; iArrayCarbonCopy++)
                        {
                            if (strArrayCarbonCopy[iArrayCarbonCopy] != "")
                            {
                                if (strArrayCarbonCopy[iArrayCarbonCopy] == "Teacher")
                                {
                                    strCarbonCopyMail += getTeacherMail(strCourseID);
                                }
                                else if (strArrayCarbonCopy[iArrayCarbonCopy] == "All")
                                {
                                    strCarbonCopyMail += getCourseAllUserMail(strCourseID);
                                }
                                else if (strArrayCarbonCopy[iArrayCarbonCopy] == "Others")
                                {
                                    strCarbonCopyMail += getOthersMail(strCourseID, strActivityID);
                                }
                                else
                                {
                                    strCarbonCopyMail += getRoleUserMail(strActivityID, strArrayCarbonCopy[iArrayCarbonCopy].ToString());
                                }

                            }
                        }
                        #endregion


                        //寄送MAIL
                        if (strRecipientsMail != "")
                            sendMail("MLAS系統 教學活動通知", "您好: <br/>" + strMessageContent + "<br /> MLAS系統管理員 敬上", strRecipientsMail, strCarbonCopyMail);
                    }
                }
                #endregion


                #endregion

            }
        }

        /// <summary>
        /// 寄信 僅需傳入 信件主旨(strSubject) 信件內容(strMessageContent) 收件者(strRecipientsMail) 副本(strCarbonCopyMail)
        /// 若收件者與副本有多位 請以 "," 隔開
        /// </summary>
        /// <param name="strRecipientsMail"></param>
        /// <param name="strMessageContent"></param>
        /// <param name="strCarbonCopyMail"></param>
        public static void sendMail(String strSubject, String strMessageContent, String strRecipientsMail, String strCarbonCopyMail)
        {
            #region 寄送MAIL
            //設定寄件者的電子郵件地址 //設定以分號區隔的接收者電子郵件地址清單
            MailMessage myMail = new MailMessage(clsMLASConfig.ServerMail, strRecipientsMail);

            //設定電子郵件訊息的主旨
            myMail.Subject = strSubject;

            //設定電子郵件訊息的主體
            myMail.Body = strMessageContent;

            //設定副本
            if (strCarbonCopyMail != "")
            {
                String[] strArrayCarbonCopyMail = strCarbonCopyMail.Split(',');
                for (int iArrayCarbonCopyMail = 0; iArrayCarbonCopyMail < strArrayCarbonCopyMail.Length; iArrayCarbonCopyMail++)
                {
                    if (strArrayCarbonCopyMail[iArrayCarbonCopyMail] != "")
                    {
                        myMail.CC.Add(new MailAddress(strArrayCarbonCopyMail[iArrayCarbonCopyMail]));
                    }
                }
            }

            myMail.IsBodyHtml = true;

            //取得或設定 SMTP 轉接郵件伺服器的名稱，用來傳送電子郵件訊息。
            SmtpClient client = new SmtpClient(clsMLASConfig.ServerSMTP);
            try
            {
                //多載。 傳送電子郵件訊息。
                client.Send(myMail);
            }
            catch { }
            #endregion
        }

        /// <summary>
        /// 取得教師的Mail
        /// </summary>
        /// <param name="strCourseID"></param>
        /// <returns></returns>
        public static String getTeacherMail(String strCourseID)
        {
            String strMail = "";
            String strTeacherID = clsMLASVIEWCourseUser.VIEWCourseUser_SELECT(strCourseID, AllSystemUser.Authority_Teacher).Rows[0]["cUserID"].ToString();
            strMail = clsMLASUser.MLASUser_SELECT(strTeacherID).Rows[0]["cEmail"].ToString() + ",";
            return strMail;
        }

        /// <summary>
        /// 取得課程所有使用者的Mail
        /// </summary>
        /// <param name="strCourseID"></param>
        /// <returns></returns>
        public static String getCourseAllUserMail(String strCourseID)
        {
            String strMail = "";
            DataTable dtCourseUser = clsMLASCourseUser.MLAS_CourseUser_SELECT(strCourseID);
            if (dtCourseUser.Rows.Count > 0)
            {
                foreach (DataRow drCourseUser in dtCourseUser.Rows)
                {
                    if (clsMLASUser.MLASUser_SELECT(drCourseUser["cUserID"]).Rows[0]["cEmail"].ToString() != "")
                        strMail += clsMLASUser.MLASUser_SELECT(drCourseUser["cUserID"]).Rows[0]["cEmail"].ToString() + ",";
                }
            }
            return strMail;
        }

        /// <summary>
        /// 取得課程中其他使用者的Mail(即不為任何角色)
        /// </summary>
        /// <param name="strCourseID"></param>
        /// <param name="strActivityID"></param>
        /// <returns></returns>
        public static String getOthersMail(String strCourseID, String strActivityID)
        {
            String strMail = "";
            //先取得課程的所有使用者
            DataTable dtCourseUser = clsMLASCourseUser.MLAS_CourseUser_SELECT(strCourseID);
            if (dtCourseUser.Rows.Count > 0)
            {
                foreach (DataRow drCourseUser in dtCourseUser.Rows)
                {
                    //判斷使用者是否具有角色 不具角色才寄信
                    DataTable dtMLAS_ActivityUserRole = clsMLASActivityUserRole.MLAS_ActivityUserRole_SELECT_cActivityID_UserID(strActivityID, drCourseUser["cUserID"]);
                    if (dtMLAS_ActivityUserRole.Rows.Count == 0)
                    {
                        //判斷使用者權限為學生才寄
                        if (clsMLASUser.MLASUser_SELECT_Authority(drCourseUser["cUserID"]) == "s")
                        {
                            if (clsMLASUser.MLASUser_SELECT(drCourseUser["cUserID"]).Rows[0]["cEmail"].ToString() != "")
                                strMail += clsMLASUser.MLASUser_SELECT(drCourseUser["cUserID"]).Rows[0]["cEmail"].ToString() + ",";
                        }
                    }
                }
            }
            return strMail;
        }

        /// <summary>
        /// 取得課程中角色的Mail
        /// </summary>
        /// <param name="strActivityID"></param>
        /// <param name="strArrayRecipients"></param>
        /// <returns></returns>
        public static String getRoleUserMail(String strActivityID, String strArrayRecipients)
        {
            String strMail = "";
            clsMLASDB MLASDB = new clsMLASDB();
            //根據活動ID與角色ID取得使用者ID的字串物件
            String strSQL_MLAS_ActivityUserRoleJoinMLAS_Role = "";
            strSQL_MLAS_ActivityUserRoleJoinMLAS_Role = "SELECT dbo.MLAS_ActivityUserRole.cRoleID, dbo.MLAS_ActivityUserRole.cActivityID, dbo.MLAS_ActivityUserRole.cUserID, dbo.MLAS_Role.cRoleName " +
                                                     " FROM dbo.MLAS_ActivityUserRole INNER JOIN dbo.MLAS_Role ON dbo.MLAS_ActivityUserRole.cRoleID = dbo.MLAS_Role.cRoleID " +
                                                     " WHERE dbo.MLAS_ActivityUserRole.cActivityID LIKE '" + strActivityID + "' AND dbo.MLAS_ActivityUserRole.cRoleID LIKE '" + strArrayRecipients + "'";

            DataTable dtMLAS_ActivityUserRoleJoinMLAS_Role = new DataTable();
            dtMLAS_ActivityUserRoleJoinMLAS_Role = MLASDB.GetDataSet(strSQL_MLAS_ActivityUserRoleJoinMLAS_Role).Tables[0];
            if (dtMLAS_ActivityUserRoleJoinMLAS_Role.Rows.Count > 0)
            {
                foreach (DataRow drMLAS_ActivityUserRoleJoinMLAS_Role in dtMLAS_ActivityUserRoleJoinMLAS_Role.Rows)
                {
                    strMail += clsMLASUser.MLASUser_SELECT(drMLAS_ActivityUserRoleJoinMLAS_Role["cUserID"]).Rows[0]["cEmail"].ToString() + ",";
                }
            }
            return strMail;
        }

        /// <summary>
        /// 取得課程中角色的Mail
        /// </summary>
        /// <param name="strActivityID"></param>
        /// <param name="strArrayRecipients"></param>
        /// <returns></returns>
        public static String getRoleUserMail_ByGroup(String strActivityID, String strArrayRecipients, String strUserID)
        {
            String strMail = "";
            clsMLASDB MLASDB = new clsMLASDB();
            //根據活動ID與角色ID取得使用者ID的字串物件
            String strSQL_MLAS_ActivityUserRoleJoinMLAS_Role = "";
            strSQL_MLAS_ActivityUserRoleJoinMLAS_Role = "SELECT dbo.MLAS_ActivityUserRole.cRoleID, dbo.MLAS_ActivityUserRole.cActivityID, dbo.MLAS_ActivityUserRole.cUserID, dbo.MLAS_Role.cRoleName " +
                                                         " FROM dbo.MLAS_ActivityUserRole INNER JOIN dbo.MLAS_Role ON dbo.MLAS_ActivityUserRole.cRoleID = dbo.MLAS_Role.cRoleID " +
                                                         " WHERE dbo.MLAS_ActivityUserRole.cActivityID LIKE '" + strActivityID + "' AND dbo.MLAS_ActivityUserRole.cRoleID LIKE '" + strArrayRecipients + "' " +
                                                         " AND dbo.MLAS_ActivityUserRole.cUserID LIKE '" + strUserID + "'";

            DataTable dtMLAS_ActivityUserRoleJoinMLAS_Role = new DataTable();
            dtMLAS_ActivityUserRoleJoinMLAS_Role = MLASDB.GetDataSet(strSQL_MLAS_ActivityUserRoleJoinMLAS_Role).Tables[0];
            if (dtMLAS_ActivityUserRoleJoinMLAS_Role.Rows.Count > 0)
            {
                foreach (DataRow drMLAS_ActivityUserRoleJoinMLAS_Role in dtMLAS_ActivityUserRoleJoinMLAS_Role.Rows)
                {
                    strMail += clsMLASUser.MLASUser_SELECT(drMLAS_ActivityUserRoleJoinMLAS_Role["cUserID"]).Rows[0]["cEmail"].ToString() + ",";
                }
            }
            return strMail;
        }

        /// <summary>
        /// 取得使用者是否具有角色ID
        /// </summary>
        /// <param name="strActivityID"></param>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public static DataTable getUserRoleID(String strActivityID, String strUserID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            String strSQL_MLAS_ActivityUserRoleJoinMLAS_Role = "SELECT dbo.MLAS_ActivityUserRole.cRoleID, dbo.MLAS_ActivityUserRole.cActivityID, dbo.MLAS_ActivityUserRole.cUserID, dbo.MLAS_Role.cRoleName " +
                                    " FROM dbo.MLAS_ActivityUserRole INNER JOIN dbo.MLAS_Role ON dbo.MLAS_ActivityUserRole.cRoleID = dbo.MLAS_Role.cRoleID " +
                                    " WHERE dbo.MLAS_ActivityUserRole.cActivityID LIKE '" + strActivityID + "' AND dbo.MLAS_ActivityUserRole.cUserID LIKE '" + strUserID + "'";
            DataTable dtMLAS_ActivityUserRoleJoinMLAS_Role = MLASDB.GetDataSet(strSQL_MLAS_ActivityUserRoleJoinMLAS_Role).Tables[0];
            return dtMLAS_ActivityUserRoleJoinMLAS_Role;
        }
    }

}