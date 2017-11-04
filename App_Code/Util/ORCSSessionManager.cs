using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ORCS.Util
{
    /// <summary>
    /// Summary description for ORCSSessionManager
    /// </summary>
    public class ORCSSessionManager
    {
        protected System.Web.UI.Control m_currentControl = null;

        protected bool m_bLogin;                    // 是否登入
        protected string m_strUserID;               // 使用者名稱
        protected string m_strGroupID;              // 目前上課的GroupID
        protected string m_strTempID;               // 目前上課的小組ID
        protected string m_strPrivilege;            // 使用者身份
        protected string m_strProcessPage;          // 接續頁
        protected string m_strPrePage;              // 上一頁
        protected string m_strLoginFrom;            // 從何處登入
        protected string m_strLanguage;             // 語言

        public ORCSSessionManager(System.Web.UI.Control currentControl)
        {
            m_currentControl = currentControl;
            this.GetSessionData();
        }

        protected virtual void GetSessionData()
        {
            if (((System.Web.UI.Page)m_currentControl).Session["IsLogin"] != null)   // login flag
                this.m_bLogin = System.Convert.ToBoolean(((System.Web.UI.Page)m_currentControl).Session["IsLogin"].ToString());
            else
                this.m_bLogin = false;  // default value

            if (((System.Web.UI.Page)m_currentControl).Session["UserID"] != null)    // user id
                this.m_strUserID = ((System.Web.UI.Page)m_currentControl).Session["UserID"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["GroupID"] != null)    // user id
                this.m_strGroupID = ((System.Web.UI.Page)m_currentControl).Session["GroupID"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["TempID"] != null)    // TempID
                this.m_strTempID = ((System.Web.UI.Page)m_currentControl).Session["TempID"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["Authority"] != null)
                this.m_strPrivilege = ((System.Web.UI.Page)m_currentControl).Session["Authority"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["ProcessPage"] != null)
                this.m_strProcessPage = ((System.Web.UI.Page)m_currentControl).Session["ProcessPage"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["PrePage"] != null)
                this.m_strPrePage = ((System.Web.UI.Page)m_currentControl).Session["PrePage"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["LoginFrom"] != null)
                this.m_strLoginFrom = ((System.Web.UI.Page)m_currentControl).Session["LoginFrom"].ToString();

            if (((System.Web.UI.Page)m_currentControl).Session["Language"] != null)
                this.m_strLanguage = ((System.Web.UI.Page)m_currentControl).Session["Language"].ToString();
            else
                this.m_strLanguage = System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultLanguage"];// default language
        }

        /// <summary>
        /// Save data to session
        /// </summary>
        public virtual void SaveToSession(string keyName, object keyValue)
        {
            ((System.Web.UI.Page)m_currentControl).Session[keyName] = keyValue;
        }

        /// <summary>
        /// Clear all session variable
        /// </summary>
        public virtual void ClearSession()
        {
            ((System.Web.UI.Page)m_currentControl).Session.Clear();
        }

        /// <summary>
        /// Reset session variable to initial value
        /// </summary>
        public void ResetSession()
        {
            this.ClearSession();
            this.m_bLogin = false;
            this.m_strUserID = "";
            this.m_strGroupID = "";
            this.m_strTempID = "";
            this.m_strPrivilege = "";
            this.m_strProcessPage = "ORCS_Homepage";
            this.m_strPrePage = "";
            this.m_strLoginFrom = "ORCS";
        }

        /// <summary>
        /// check if user login
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return m_bLogin;
            }
            set
            {
                m_bLogin = value;
                SaveToSession("IsLogin", value);
            }
        }

        /// <summary>
        /// User Login ID
        /// </summary>
        public string UserID
        {
            get
            {
                return m_strUserID;
            }
            set
            {
                m_strUserID = value;
                SaveToSession("UserID", value);
            }
        }

        /// <summary>
        /// 目前上課的GroupID
        /// </summary>
        public string GroupID
        {
            get
            {
                return m_strGroupID;
            }
            set
            {
                m_strGroupID = value;
                SaveToSession("GroupID", value);
            }
        }

        /// <summary>
        /// 目前上課的小組ID
        /// </summary>
        public string TempID
        {
            get
            {
                return m_strTempID;
            }
            set
            {
                m_strTempID = value;
                SaveToSession("TempID", value);
            }
        }

        /// <summary>
        /// 使用者的身份，為學生，老師或系統管理員
        /// </summary>
        public string Authority
        {
            get
            {
                return m_strPrivilege;
            }
            set
            {
                m_strPrivilege = value;
                SaveToSession("Authority", value);
            }
        }

        /// <summary>
        /// 接續頁
        /// </summary>
        public string ProcessPage
        {
            get
            {
                return this.m_strProcessPage;
            }
            set
            {
                m_strProcessPage = value;
                SaveToSession("ProcessPage", value);
            }
        }

        /// <summary>
        /// 上一頁
        /// </summary>
        public string PrePage
        {
            get
            {
                return this.m_strPrePage;
            }
            set
            {
                m_strPrePage = value;
                SaveToSession("PrePage", value);
            }
        }

        /// <summary>
        /// User Login From
        /// </summary>
        public string LoginFrom
        {
            get
            {
                return m_strLoginFrom;
            }
            set
            {
                m_strLoginFrom = value;
                SaveToSession("LoginFrom", value);
            }
        }

        /// <summary>
        /// Language
        /// </summary>
        public string Language
        {
            get
            {
                return m_strLanguage;
            }
            set
            {
                m_strLanguage = value;
                SaveToSession("Language", value);
            }
        }
    }
}