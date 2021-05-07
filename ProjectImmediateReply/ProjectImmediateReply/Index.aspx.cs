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
    public partial class Index1 : System.Web.UI.Page
    {
        // 網頁?後面接的字串
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
            LogInfo Info = (LogInfo)Session["IsLogined"];

            PageTool ptool = new PageTool();
            if (PageInner == "GradesCrud")//查看專案及評分
            {
                if (Info.Privilege != "Grades")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                ucCrud.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else if (PageInner == "CreateClass")//班級建立
            {
                if (Info.Privilege != "Manager")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                ucCreateClass.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            else if (PageInner == "UpdateInfo")//個人資料更新
            {
                ucUpdateInfo.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            else if (PageInner == "CreateProject")//建立專案
            {
                if (Info.Privilege != "Manager")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                ucCreateProject.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else if (PageInner == "SeeGrade")//查看成績
            {
                ucSeeGrade.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else if (PageInner == "AssignTeam")//分組
            {
                if (Info.Privilege != "Manager")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                ucAssignTeam.Visible = true;
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else//普通狀態
            {
                divinnerplace.InnerHtml = "<v-main></v-main>";
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }



        }
    }
}