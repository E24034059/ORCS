using ORCS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ORCS_MasterPage 的摘要描述
/// </summary>
public class ORCS_MasterPage : System.Web.UI.MasterPage
{
    protected ORCSSessionManagerExtend ORCSSession;

	public ORCS_MasterPage()
	{
		//
		// TODO: 在這裡新增建構函式邏輯
		//
	}

    /// <summary>
    /// initial function
    /// </summary>
    protected virtual void ORCS_Init()
    {
        ORCSSession = new ORCSSessionManagerExtend(this);
    }
}