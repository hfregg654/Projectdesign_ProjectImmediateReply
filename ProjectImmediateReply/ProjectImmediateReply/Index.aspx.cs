using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Web.UI;

namespace ProjectImmediateReply
{
    public partial class Index1 : Page
    {
        // 網頁?後面接的字串類別 於右方顯示不同的頁面
        private const string _sessionKey = "PageInnerType";
        protected void Page_Load(object sender, EventArgs e)
        {
            //確認登入狀態 如果沒資訊則跳回登入頁面
            LoginHelper logtool = new LoginHelper();
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");
            //如果取得的頁面型態資料不為空 進入此判斷
            string PageInner = string.Empty;
            //確認當前頁面,取得頁面QueryString判斷
            if (Request.QueryString[_sessionKey] != null)
            {
                //將回傳的QueryString轉成字串放入到PageInner
                PageInner = Request.QueryString[_sessionKey].ToString();
            }
            //取得登入者資訊
            LogInfo Info = (LogInfo)Session["IsLogined"];
            //引用PageTool方法
            PageTool ptool = new PageTool();
            //如果PageInner字串為GradesCrud進入此判斷
            //查看專案及評分頁面
            if (PageInner == "GradesCrud")
            {
                //權限不是Grades則登出並跳轉至登入頁面
                if (Info.Privilege != "Grades")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                //套用ImmediateReplyInSide.Master的Index ucCrud預設為false 
                //點擊左選單判斷後變更為true並插入PageTool的PageInner == "GradesCrud" script部分
                //將查看專案及評分頁面顯示的DOM變更為True 將網頁框架顯示出來
                ucCrud.Visible = true;
                //將查看專案及評分頁面顯示的JS效果插入到DOM裡面
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            //如果PageInner字串為CreateClass進入此判斷
            else if (PageInner == "CreateClass")//班級建立
            {
                //權限不是Manager則登出並跳轉至登入頁面
                if (Info.Privilege != "Manager")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                //套用ImmediateReplyInSide.Master的Index ucCreateClass預設為false 
                //點擊左選單判斷後變更為true並插入PageTool的PageInner == "CreateClass" script部分
                //將建立班級頁面顯示的DOM變更為True 將網頁框架顯示出來
                ucCreateClass.Visible = true;
                //將建立班級頁面顯示的JS效果插入到DOM裡面
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            //如果PageInner字串為UpdateInfo進入此判斷
            else if (PageInner == "UpdateInfo")//個人資料更新
            {
                //套用ImmediateReplyInSide.Master的Index ucUpdateInfo預設為false 
                //點擊左選單判斷後變更為true並插入PageTool的PageInner == "UpdateInfo" script部分
                //將個人資訊更新頁面顯示的DOM變更為True 將網頁框架顯示出來
                ucUpdateInfo.Visible = true;
                //將個人資訊更新顯示的JS效果插入到DOM裡面
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }
            //如果PageInner字串為CreateProject進入此判斷
            else if (PageInner == "CreateProject")//建立專案
            {
                //權限不是Manager則登出並跳轉至登入頁面
                if (Info.Privilege != "Manager")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                //套用ImmediateReplyInSide.Master的Index ucCreateProject預設為false 
                //點擊左選單判斷後變更為true並插入PageTool的PageInner == "CreateProject" script部分
                //將建立專案頁面顯示的DOM變更為True 將網頁框架顯示出來
                ucCreateProject.Visible = true;
                //將建立專案頁面顯示的JS效果插入到DOM裡面
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else if (PageInner == "SeeGrade")//查看成績
            {
                //套用ImmediateReplyInSide.Master的Index ucSeeGrade預設為false 
                //點擊左選單判斷後變更為true並插入PageTool的PageInner == "SeeGrade" script部分
                //將觀看成績頁面顯示的DOM變更為True 將網頁框架顯示出來
                ucSeeGrade.Visible = true;
                //將觀看成績頁面顯示的JS效果插入到DOM裡面
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else if (PageInner == "AssignTeam")//亂數分組
            {
                //權限不是Manager則登出並跳轉至登入頁面
                if (Info.Privilege != "Manager")
                {
                    logtool.Logout();
                    Response.Redirect("~/LogIn.aspx");
                    return;
                }
                //套用ImmediateReplyInSide.Master的Index ucAssignTeam預設為false 
                //點擊左選單判斷後變更為true並插入PageTool的PageInner == "AssignTeam" script部分
                //將亂數分組頁面顯示的DOM變更為True 將網頁框架顯示出來
                ucAssignTeam.Visible = true;
                //將亂數分組頁面顯示的JS效果插入到DOM裡面
                divJS.InnerHtml = ptool.PageRight(PageInner);
            }
            else//普通狀態
            {
                //顯示空白頁面
                divinnerplace.InnerHtml = "<v-main></v-main>";
                //插入空白頁面的JS效果
                divJS.InnerHtml = ptool.PageRight(PageInner); ;
            }



        }
    }
}