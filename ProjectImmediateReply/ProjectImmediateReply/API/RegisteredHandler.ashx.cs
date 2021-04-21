using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// RegisteredHandler 的摘要描述
    /// </summary>
    public class RegisteredHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Name = context.Request.Form["Name"];
            string Phone = context.Request.Form["Phone"];
            string Mail = context.Request.Form["Mail"];
            string LineID = context.Request.Form["LineID"];
            string ClassNumber = context.Request.Form["ClassNumber"];
            string Account = context.Request.Form["Account"];
            string PassWord = context.Request.Form["PassWord"];
            string PassWordCheck = context.Request.Form["PassWordCheck"];
            string License = context.Request.Form["License"];

            if (Name != string.Empty && Phone != string.Empty && Mail != string.Empty && LineID != string.Empty && ClassNumber != string.Empty
                && Account != string.Empty && PassWord != string.Empty && PassWordCheck != string.Empty && License != string.Empty)
            {
                if (PassWord == PassWordCheck)
                {
                    Utility.DBTool Create = new Utility.DBTool();
                    string[] readcolname = { "Account" };  //輸入幾個為條件就找幾格。
                    string[] Pname = { "@Account" };
                    string[] P = { Account };
                    //DataTable Check = Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P) //如上面寫一個字串，只會找一格，已經在核對帳號找出有的那一格，P表網頁輸出欄位，以帳號為條件搜尋，如為空則傳空。
                    List<UserInfo> Check_Acc = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P));
                    string[] readcolname2 = { "Account", "License" };
                    string[] Pname2 = { "@Account", "@License" };
                    string[] P2 = { Account , License };
                    List<UserInfo> Check_Acc_Lic = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname2, "WHERE License=@License AND Account=@Account", Pname2, P2));
                    if (Check_Acc_Lic.Count != 0) //資料表型式的變數都是存在，非空值。
                    {
                        return "授權碼已使用";
                    }
                    else if (Check_Acc.Count != 0)
                    {
                        return "帳號已存在，請使用其他帳號";
                    }
                    else
                    {
                        return "註冊成功，請至註冊信箱收取驗證信";
                    }
                }
                else
                {
                    return "密碼確認輸入錯誤";
                }
            }
            else
            {
                return "欄位不可為空";
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