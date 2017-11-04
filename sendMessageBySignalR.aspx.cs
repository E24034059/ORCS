using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //userID
        if (Request.QueryString["UserID"] != null)
            hUserID.Value = Request.QueryString["UserID"];
        //groupID
        if (Request.QueryString["groupID"] != null)
            hTempGroupID.Value = (Request.QueryString["GroupID"].ToString()!="")?Request.QueryString["GroupID"].ToString():"NoGroup";
        //message
        if (Request.QueryString["message"] != null)
            txtMessage.Value = Request.QueryString["message"];
    }
}