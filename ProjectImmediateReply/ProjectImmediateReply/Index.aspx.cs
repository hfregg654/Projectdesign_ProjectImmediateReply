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
            LoginHelper logtool = new LoginHelper();
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");

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
            else if (PageInner == "CreateClass")
            {
                ucCreateClass.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            else if (PageInner == "UpdateInfo")
            {
                ucUpdateInfo.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            else
            {
                divinnerplace.InnerHtml = "<v-main></v-main>";
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }



        }
    }
}