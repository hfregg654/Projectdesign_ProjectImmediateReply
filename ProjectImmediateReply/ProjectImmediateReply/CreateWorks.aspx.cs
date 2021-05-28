﻿using System;
using ProjectImmediateReply.Utility;
using ProjectImmediateReply.Models;

namespace ProjectImmediateReply
{
    public partial class CreateWorks : System.Web.UI.Page
    {
        //建立工作頁面載入
        protected void Page_Load(object sender, EventArgs e)
        {
            //引用LoginHelper方法
            LoginHelper logtool = new LoginHelper();
            //判斷是否有登入資訊 如果沒有則返回至登入頁面
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");
            //擷取使用者的登入資訊
            LogInfo Info = (LogInfo)Session["IsLogined"];
            //判斷如果使用者登入資訊不是Leader 則跳轉至登入頁面
            if (Info.Privilege != "Leader")
            {
                logtool.Logout();
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
}