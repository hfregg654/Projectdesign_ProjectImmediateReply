using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class Index1 : System.Web.UI.Page
    {
        private const string _sessionKey = "PageInnerType";
        protected void Page_Load(object sender, EventArgs e)
        {
            //確認登入狀態
            LoginHelper logtool = new LoginHelper();
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");
            //確認當前頁面,取得頁面QueryString判斷當前頁面應顯示的畫面以及使用的JS並插入DOM中
            string PageInner = string.Empty;
            if (Request.QueryString[_sessionKey] != null)
            {
                PageInner = Request.QueryString[_sessionKey].ToString();
            }
            PageTool ptool = new PageTool();
            if (PageInner == "Crud")
            {
                ucCrud.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else if (PageInner == "CreateClass")//班級建立
            {
                ucCreateClass.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            else if (PageInner == "UpdateInfo")//個人資料更新
            {
                ucUpdateInfo.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            else//普通狀態
            {
                divinnerplace.InnerHtml = "<v-main></v-main>";
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }



        }
    }
}