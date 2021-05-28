using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class ProjectDetail : System.Web.UI.Page
    {
        //管理者用 看成績頁面
        protected void Page_Load(object sender, EventArgs e)
        {
            //引用LoginHelper方法
            LoginHelper logtool = new LoginHelper();
            //判斷是否有登入資訊 如果沒有則返回至登入頁面
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");
            //擷取使用者的登入資訊
            LogInfo Info = (LogInfo)Session["IsLogined"];
            //判斷如果使用者登入資訊不是Manager 則跳轉至登入頁面
            if (Info.Privilege != "Manager")
            {
                logtool.Logout();
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
}