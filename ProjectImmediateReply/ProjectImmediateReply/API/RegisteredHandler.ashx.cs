using System.Collections.Generic;
using System.Web;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;

//System.Collections.Generic 包含定義泛型集合的介面和類別，其允許使用者建立強型別集合，這些集合提供比起非泛型強型別集合更佳的型別安全和效能。
//System.Web 包含啟用瀏覽器和伺服器間通訊的類別和介面
//ProjectImmediateReply.Models; 引用專案內的模型
//ProjectImmediateReply.Utility; 引用專案內的方法工具
// 命名空間名稱 此專案的API
namespace ProjectImmediateReply.API
{
    /// <summary>
    /// RegisteredHandler 的摘要描述
    /// </summary>
    //IHttpHandler 為父類別 委派給RegisteredHandler 使用IHttpHandler內容 執行伺服器連接
    public class RegisteredHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //ImmediateReplyAJAX內用data傳後  此處接值的用法
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
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone) &&  !string.IsNullOrWhiteSpace(Mail) &&  !string.IsNullOrWhiteSpace(LineID) &&  !string.IsNullOrWhiteSpace(ClassNumber)
                &&  !string.IsNullOrWhiteSpace(Account) &&  !string.IsNullOrWhiteSpace(PassWord) &&  !string.IsNullOrWhiteSpace(PassWordCheck) &&  !string.IsNullOrWhiteSpace(License))
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
                    string[] readcolname3 = { "License", "Privilege" };
                    string[] Pname3 = { "@License" };
                    string[] P3 = { License };
                    Check_Acc_Lic = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname3, "WHERE License=@License AND Account IS NOT NULL AND Privilege != 'Visitor' AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname3, P3));
                    if (Check_Lic.Count == 0)
                    {
                        context.Response.ContentType = "text/json"; //傳送的是文字 文字是json型態
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
                        mtool.SendMail("shiyuance989898@gmail.com", Mail, "Manager", "驗證信", $"請點擊網址以完成註冊<br/>http://WIN-A1VN7POGE3B/LogIn.aspx?license={License}&classnumber={ClassNumber}", "1qazxcvfr432wsde");
                        string[] updatecol_Logic = { "Name=@Name", "Phone=@Phone", "Mail=@Mail", "LineID=@LineID", "ClassNumber=@ClassNumber", "Account=@Account", "PassWord=@PassWord", }; //欲更新的欄位 Name是開頭欄位名稱 @Name是欄位名稱底下格子
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
                        Create.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P); //updatecolname_P 傳入的@格 update_P傳入的值
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