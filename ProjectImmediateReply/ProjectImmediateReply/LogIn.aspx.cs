using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectImmediateReply.Utility;  //引用專案下的某樣工具方法

namespace ProjectImmediateReply
{
    public partial class Index : System.Web.UI.Page
    {
        string _goToUrl = "~/Index.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            //_goToUrl = Request.RawUrl; //轉回本頁
            Utility.LoginHelper helper = new Utility.LoginHelper(); //如不用static 需用new建立此實體物件
            if (helper.HasLogIned())
            {
                Response.Redirect(this._goToUrl);
                //自動轉到下一個頁面
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string acc = this.username.Value;
            string pwd = this.passwordIndex.Value;
            Utility.LoginHelper helper = new Utility.LoginHelper();
            bool isSuccess = helper.TryLogIn(acc, pwd);

            if (isSuccess)
            {
                //跳轉到新網頁
                Response.Redirect(this._goToUrl);
            }
            else
            {
                this.ltMessage.Text = "帳號或密碼輸入錯誤，請重新輸入";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Utility.LoginHelper helper = new Utility.LoginHelper();
            if (helper.HasLogIned())
            {
                helper.Logout();

                Response.Redirect(this._goToUrl);
            }
        }
    }
}