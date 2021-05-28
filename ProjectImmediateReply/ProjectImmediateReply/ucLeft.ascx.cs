using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Web.UI;

namespace ProjectImmediateReply
{
    public partial class WebUserControl1 : UserControl
    {
        //頁面載入前判斷身分
        protected void Page_Init(object sender, EventArgs e)
        {
            //命名權限類別頁面及使用者名稱變數
            string PageType = string.Empty;
            string UserName = string.Empty;
            // 如果有抓到Session資訊 則進入此判斷
            if (Session["IsLogined"] != null)
            {
                //已建立的LogInfo 模型 並將Session放入Info
                LogInfo Info = (LogInfo)Session["IsLogined"];
                PageType = Info.Privilege.ToString();
                UserName = Info.Name.ToString();
            }
            //左上角名字顯示
            LabelUserName.Text = UserName;
            // 方法容器變數
            PageTool ptool = new PageTool();
            // 依照方法判別的網頁型態顯示左邊清單
            divLeftTitle.InnerHtml = ptool.PageLeft(PageType);
        }

        protected void Logoutbtn_ServerClick(object sender, EventArgs e)
        {
            // 引用登入登出方法
            LoginHelper helper = new LoginHelper();
            // 如果是已登入狀態
            if (helper.HasLogIned())
            {
                // 登出至登入頁面並清空Session
                helper.Logout();
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
}