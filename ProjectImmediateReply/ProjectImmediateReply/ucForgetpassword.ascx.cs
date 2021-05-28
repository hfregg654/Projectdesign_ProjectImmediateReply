using System;
using System.Collections.Generic;
using ProjectImmediateReply.Utility;

namespace ProjectImmediateReply
{
    public partial class ucForgetpassword : System.Web.UI.UserControl
    {
        //頁面載入事件
        protected void Page_Load(object sender, EventArgs e)
        {
            //引用PageTool方法內的GetClassNumber()取得資料庫內ClassNumber的變數放入到下拉式清單內，並設定給伺服器控制項forgetpwd_class.DataSource
            //之後作資料繫結至控制項顯示出當前資料庫內之ClassNumber
            PageTool ptool = new PageTool();
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