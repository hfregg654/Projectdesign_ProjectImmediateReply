using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectImmediateReply.Utility;
using ProjectImmediateReply.Models;
using System.Data;

namespace ProjectImmediateReply
{
    public partial class ucForgetpassword : System.Web.UI.UserControl
    {
        protected void Btn_Forgot(object sender, EventArgs e)
        {
            Utility.DBTool Forgot = new Utility.DBTool();
            string[] readcolname = { "License", "PassWord" };
            string[] Pname = { "@License" };
            string[] P = { this.rescue_key.Value };
            UserInfo Check = Forgot.ChangeTypeUserInfo(Forgot.readTable("Users", readcolname, "WHERE License=@License", Pname, P))[0]; //單筆時候的取值用法
            if (Check != null)
            {
                Message.Text = "您的密碼為：" + Check.PassWord;
            }
            else
            {
                Message.Text = "授權碼輸入錯誤";
            }
        }
    }
}