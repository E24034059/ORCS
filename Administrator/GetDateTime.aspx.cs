using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Administrator_GetDateTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //預設取得時間為現在
            calDateTime.SelectedDate = DateTime.Now;
        }
        //取得日期時間
        GetDateTime();
    }
    //取得日期時間
    protected void GetDateTime()
    {
        //定義時間，將時間存入HiddenFild
        hfDateTime.Value = calDateTime.SelectedDate.ToString("yyyy/MM/dd");
    }
    //「行事曆」選擇不同日期事件
    protected void calDateTime_SelectionChanged(object sender, EventArgs e)
    {
        //取得日期時間
        GetDateTime();
    }
}
