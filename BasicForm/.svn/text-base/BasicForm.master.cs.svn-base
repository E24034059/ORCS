using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hints.DB;
using ORCS.Base;

public partial class BasicForm_BasicForm : ORCS_MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //初始化Session值
        ORCS_Init();
        //初始化參數
        InitParams();
    }

    /// <summary>
    /// 初始化參數
    /// </summary>
    private void InitParams()
    {
        hfUserID.Value = ORCSSession.UserID;
    }
}
