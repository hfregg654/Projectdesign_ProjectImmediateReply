﻿using System;
using System.Collections.Generic;
using ProjectImmediateReply.Utility;
using System.Data;

namespace ProjectImmediateReply
{
    public partial class ucForgetpassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.PageTool ptool = new Utility.PageTool();
            List<string> classnum = ptool.GetClassNumber();
            forgetpwd_class.DataSource = classnum;
            forgetpwd_class.DataBind();

        }

        //protected void Btn_Forgot(object sender, EventArgs e)
        //{
        //    Utility.DBTool Forgot = new Utility.DBTool();
        //    string[] readcolname = { "License", "PassWord" };
        //    string[] Pname = { "@License" };
        //    string[] P = { this.rescue_key.Value };
        //    List<UserInfo> Check = Forgot.ChangeTypeUserInfo(Forgot.readTable("Users", readcolname, "WHERE License=@License", Pname, P)); //單筆時候的取值用法
        //    if (Check.Count != 0)
        //    {
        //        Message.Text = "您的密碼為：" + Check[0].PassWord;
        //    }
        //    else
        //    {
        //        Message.Text = "授權碼輸入錯誤";
        //    }
        //}
    }
}