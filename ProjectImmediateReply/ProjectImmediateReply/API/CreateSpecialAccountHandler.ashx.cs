using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// CreateSpecialAccountHandler 的摘要描述
    /// </summary>
    public class CreateSpecialAccountHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            RandomTool RTool = new RandomTool();

            string Name = context.Request.Form["Name"]; //POST取值
            string Phone = context.Request.Form["Phone"];
            string Mail = context.Request.Form["Mail"];
            string LineID = context.Request.Form["LineID"];
            string ClassNumber = context.Request.Form["ClassNumber"];
            string Account = context.Request.Form["Account"];
            string PassWord = context.Request.Form["PassWord"];
            string PassWordCheck = context.Request.Form["PassWordCheck"];
            string License = string.Empty;

            List<UserInfo> Check_Acc = new List<UserInfo>();
            List<UserInfo> Check_Acc_Lic = new List<UserInfo>();
            string ShowMessage = string.Empty;
            string ShowMessage2 = string.Empty;
            if (Name != string.Empty && Phone != string.Empty && Mail != string.Empty && LineID != string.Empty && ClassNumber != string.Empty
                && Account != string.Empty && PassWord != string.Empty && PassWordCheck != string.Empty)
            {
                if (PassWord == PassWordCheck)
                {
                    Utility.DBTool Create = new Utility.DBTool();
                    string[] readcolname = { "Account", "Privilege" };  //輸入幾個為條件就找幾格。
                    string[] Pname = { "@Account" };
                    string[] P = { Account };
                    //DataTable Check = Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P) //如上面寫一個字串，只會找一格，已經在核對帳號找出有的那一格，P表網頁輸出欄位，以帳號為條件搜尋，如為空則傳空。
                    Check_Acc = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE Account=@Account AND Privilege!='Visitor' AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname, P));
                    do
                    {
                        foreach (var item in RTool.CreateLicence(1, "Manager"))
                        {
                            License = item;
                        }
                        string[] readcolname3 = { "Account", "License", "Privilege" };
                        string[] Pname3 = { "@Account", "@License" };
                        string[] P3 = { Account, License };
                        Check_Acc_Lic = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname3, "WHERE License=@License AND Account=@Account AND Privilege!='Visitor' AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname3, P3));
                    } while (Check_Acc_Lic.Count != 0);
                
                    if (Check_Acc.Count != 0)
                    {
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"account\"}]");
                    }
                    else
                    {
                        string[] insertcolname = { "Name=@Name", "Phone=@Phone", "Mail=@Mail", "LineID=@LineID", "ClassNumber=@ClassNumber", "Account=@Account", "PassWord=@PassWord", }; 
                        string[] inserttablenamep = { "@Name", "@Phone", "@Mail", "@LineID", "@ClassNumber", "@Account", "@PassWord", "@License" };
                        List<string> insert_P = new List<string>();
                        insert_P.Add(Name);
                        insert_P.Add(Phone);
                        insert_P.Add(Mail);
                        insert_P.Add(LineID);
                        insert_P.Add(ClassNumber);
                        insert_P.Add(Account);
                        insert_P.Add(PassWord);
                        insert_P.Add(License);
                        Create.InsertTable("Users", insertcolname, inserttablenamep, insert_P);
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