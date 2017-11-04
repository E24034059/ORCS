using ORCS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORCS.Util
{
    /// <summary>
    /// ORCSSessionManagerExtend 的摘要描述
    /// </summary>
    public class ORCSSessionManagerExtend : ORCSSessionManager
    {
        public ORCSSessionManagerExtend(System.Web.UI.Control currentControl) : base(currentControl)
        {
            m_currentControl = currentControl;
            this.GetSessionData();
        }


        protected override void GetSessionData()
        {
            if (((System.Web.UI.UserControl)m_currentControl).Session["IsLogin"] != null)   // login flag
                base.m_bLogin = System.Convert.ToBoolean(((System.Web.UI.UserControl)m_currentControl).Session["IsLogin"].ToString());
            else
                base.m_bLogin = false;  // default value

            if (((System.Web.UI.UserControl)m_currentControl).Session["UserID"] != null)    // user id
                base.m_strUserID = ((System.Web.UI.UserControl)m_currentControl).Session["UserID"].ToString();

            if (((System.Web.UI.UserControl)m_currentControl).Session["Authority"] != null)
                base.m_strPrivilege = ((System.Web.UI.UserControl)m_currentControl).Session["Authority"].ToString();

            if (((System.Web.UI.UserControl)m_currentControl).Session["Language"] != null)
                base.m_strLanguage = ((System.Web.UI.UserControl)m_currentControl).Session["Language"].ToString();
            else
                base.m_strLanguage = System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultLanguage"];   // default language

            if (((System.Web.UI.UserControl)m_currentControl).Session["ProcessPage"] != null)
                base.m_strProcessPage = ((System.Web.UI.UserControl)m_currentControl).Session["ProcessPage"].ToString();
        }

        /// <summary>
        /// Save data to session
        /// </summary>
        public override void SaveToSession(string keyName, object keyValue)
        {
            ((System.Web.UI.UserControl)m_currentControl).Session[keyName] = keyValue;
        }

        /// <summary>
        /// Clear all session variable
        /// </summary>
        public override void ClearSession()
        {
            ((System.Web.UI.UserControl)m_currentControl).Session.Clear();
        }
    }
}