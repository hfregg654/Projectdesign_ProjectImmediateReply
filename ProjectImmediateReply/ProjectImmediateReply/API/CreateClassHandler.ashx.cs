using ProjectImmediateReply.Log;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// CreateClassHandler 的摘要描述
    /// </summary>
    public class CreateClassHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得傳過來的資料
            string ClassNumber = context.Request.Form["ClassNumber"];
            string PeopleNum = context.Request.Form["PeopleNum"];
            string Privilege = context.Request.Form["Privilege"];
            string Mailaddress = context.Request.Form["Mail"];
            //先檢查傳過來的值有沒有問題並先定義回傳的訊息
            string success;
            if (ClassNumber == string.Empty || ClassNumber == null || PeopleNum == string.Empty || PeopleNum == null || Privilege != "Manager" || Mailaddress == string.Empty || Mailaddress == null)
            {
                success = "[{\"success\":\"Wrong\"}]";
                context.Response.ContentType = "text/json";
                context.Response.Write(success);
                return;
            }
            DBTool Dbtool = new DBTool();
            string[] colcheckhasname = { "ClassNumber" };
            string[] colcheckhasnamep = { "@ClassNumber" };
            string[] checkhasp = { ClassNumber };
            DataTable checkhasdata = Dbtool.readTable("Users", colcheckhasname, "WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL", colcheckhasnamep, checkhasp);
            if (checkhasdata.Rows.Count > 0)
            {
                success = "[{\"success\":\"Wrong\"}]";
                context.Response.ContentType = "text/json";
                context.Response.Write(success);
                return;
            }

            //建立亂數工具並定義準備裝授權碼的集合
            RandomTool Rtool = new RandomTool();
            List<string> License = new List<string>();

            try
            {
                //確認人數為正確值後執行創建授權碼並放至集合中
                int temp;
                if (int.TryParse(PeopleNum, out temp))
                {
                    if (temp > 0)
                    {
                        License = Rtool.CreateLicence(temp, Privilege);
                    }
                    else
                    {
                        success = "[{\"success\":\"PeopleNumWrong\"}]";
                        context.Response.ContentType = "text/json";
                        context.Response.Write(success);
                        return;
                    }

                }

                //將創建的授權碼一個一個加進資料庫

                string newPrivilege = "Visitor";
                string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string WhoCreate = Privilege;

                string[] colname = { "ClassNumber", "License", "Privilege", "CreateDate", "WhoCreate" };
                string[] colnamep = { "@ClassNumber", "@License", "@Privilege", "@CreateDate", "@WhoCreate" };
                List<string> p = new List<string>();
                foreach (string item in License)
                {
                    p.Add(ClassNumber);
                    p.Add(item);
                    p.Add(newPrivilege);
                    p.Add(CreateDate);
                    p.Add(WhoCreate);
                }
                Dbtool.InsertTable("Users", colname, colnamep, p);
                //檢查欲寫入資料庫的數量與創建數量,相同則成功並發送信件,不同則提醒使用者
                string[] colcheckname = { "ClassNumber" };
                string[] colchecknamep = { "@ClassNumber" };
                string[] checkp = { ClassNumber };
                DataTable checkdata = Dbtool.readTable("Users", colcheckname, "WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL", colchecknamep, checkp);
                if (checkdata.Rows.Count == License.Count)
                {
                    MailTool mtool = new MailTool();
                    string licensemail = string.Join("<br/>", License);
                    mtool.SendMail("shiyuance989898@gmail.com", Mailaddress, Privilege, "授權碼發送", $"{ClassNumber}班的授權碼<br/>{licensemail}", "1qazxcvfr432wsde");
                    success = "[{\"success\":\"true\"}]";
                    context.Response.ContentType = "text/json";
                    context.Response.Write(success);
                }
                else
                {
                    success = "[{\"success\":\"NumWrong\"}]";
                    context.Response.ContentType = "text/json";
                    context.Response.Write(success);
                }

            }
            catch (Exception ex)
            {
                success = "[{\"success\":\"Wrong\"}]";
                context.Response.ContentType = "text/json";
                context.Response.Write(success);
                txtLog logtool = new txtLog();
                logtool.WriteLog(ex.ToString());
                throw;
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