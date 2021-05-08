using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using Newtonsoft.Json;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// RegisteredHandler 的摘要描述
    /// </summary>
    public class RegisteredHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Name = context.Request.Form["Name"]; //POST取值
            string Phone = context.Request.Form["Phone"];
            string Mail = context.Request.Form["Mail"];
            string LineID = context.Request.Form["LineID"];
            string ClassNumber = context.Request.Form["ClassNumber"];
            string Account = context.Request.Form["Account"];
            string PassWord = context.Request.Form["PassWord"];
            string PassWordCheck = context.Request.Form["PassWordCheck"];
            string License = context.Request.Form["License"];

            List<UserInfo> Check_Acc = new List<UserInfo>();
            List<UserInfo> Check_Lic = new List<UserInfo>();
            List<UserInfo> Check_Acc_Lic = new List<UserInfo>();
            string ShowMessage = string.Empty;
            string ShowMessage2 = string.Empty;
            if (Name != string.Empty && Phone != string.Empty && Mail != string.Empty && LineID != string.Empty && ClassNumber != string.Empty
                && Account != string.Empty && PassWord != string.Empty && PassWordCheck != string.Empty && License != string.Empty)
            {
                if (PassWord == PassWordCheck)
                {
                    Utility.DBTool Create = new Utility.DBTool();
                    string[] readcolname = { "Account","Privilege" };  //輸入幾個為條件就找幾格。
                    string[] Pname = { "@Account" };
                    string[] P = { Account };
                    //DataTable Check = Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P) //如上面寫一個字串，只會找一格，已經在核對帳號找出有的那一格，P表網頁輸出欄位，以帳號為條件搜尋，如為空則傳空。
                    Check_Acc = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE Account=@Account AND Privilege != 'Visitor' AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname, P));
                    string[] readcolname2 = { "ClassNumber", "License" };
                    string[] Pname2 = { "@ClassNumber", "@License" };
                    string[] P2 = { ClassNumber, License };
                    Check_Lic = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname2, "WHERE License=@License AND ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname2, P2));
                    string[] readcolname3 = { "Account", "License", "Privilege" };
                    string[] Pname3 = { "@Account", "@License" };
                    string[] P3 = { Account, License };
                    Check_Acc_Lic = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname3, "WHERE License=@License AND Account=@Account AND Privilege != 'Visitor' AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname3, P3));
                    if (Check_Lic.Count == 0)
                    {
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"licensewrong\"}]"); // \"當成字串的雙引號
                    }
                    else if (Check_Acc_Lic.Count != 0) //資料表型式的變數都是存在，非空值。
                    {
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"license\"}]");
                    }
                    else if (Check_Acc.Count != 0)
                    {
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"account\"}]");
                    }
                    else
                    {
                        MailTool mtool = new MailTool();
                        mtool.SendMail("shiyuance989898@gmail.com", Mail, "Manager", "驗證信", $"請點擊網址以完成註冊<br/>http://localhost:8085/LogIn.aspx?license={License}&classnumber={ClassNumber}", "1qazxcvfr432wsde");
                        string[] updatecol_Logic = { "Name=@Name", "Phone=@Phone", "Mail=@Mail", "LineID=@LineID", "ClassNumber=@ClassNumber", "Account=@Account", "PassWord=@PassWord", };
                        string Where_Logic = "License=@License";
                        string[] updatecolname_P = { "@Name", "@Phone", "@Mail", "@LineID", "@ClassNumber", "@Account", "@PassWord","@License" };
                        List<string> update_P = new List<string>();
                        update_P.Add(Name);
                        update_P.Add(Phone);
                        update_P.Add(Mail);
                        update_P.Add(LineID);
                        update_P.Add(ClassNumber);
                        update_P.Add(Account);
                        update_P.Add(PassWord);
                        update_P.Add(License);
                        Create.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"true\"}]");
                    }
                }
                else
                {
                    context.Response.ContentType = "text/json";
                    context.Response.Write("[{\"success\":\"PassWordWrong\"}]");
                }
            }
            else
            {
                context.Response.ContentType = "text/json";
                context.Response.Write("[{\"success\":\"Empty\"}]");
            }

        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}