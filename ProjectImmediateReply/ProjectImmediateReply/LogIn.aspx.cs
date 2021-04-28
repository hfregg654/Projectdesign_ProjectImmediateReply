using ProjectImmediateReply.Models;
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
            //找到license 後 判斷 = 後面是否有值
            if (Request.QueryString["license"] != null && Request.QueryString["classnumber"] != null)
            {
                DBTool Create = new DBTool();
                string[] readcolname = { "ClassNumber", "License" };
                string[] Pname = { "@ClassNumber", "@License" };
                string[] P = { Request.QueryString["classnumber"], Request.QueryString["license"] };
                List<UserInfo> Check = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE License=@License AND ClassNumber=@ClassNumber AND Privilege='Visitor'", Pname, P));
                if (Check.Count != 0)
                {
                    string[] updatecol_Logic = { "Privilege=@Privilege" };
                    string Where_Logic = "License=@License";
                    string[] updatecolname_P = { "@Privilege", "@License" };
                    List<string> update_P = new List<string>();
                    update_P.Add("User");
                    update_P.Add(Request.QueryString["license"]);
                    Create.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                    this.ltMessage.Text = $"授權碼{Check[0].License}之帳號註冊完成，請輸入帳號密碼以登入";
                }
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

        


    }
}