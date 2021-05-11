using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.IO;
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
            string json = string.Empty;
            string Name = string.Empty; //POST取值
            string Phone = string.Empty;
            string Mail = string.Empty;
            string LineID = string.Empty;
            string Account = string.Empty;
            string PassWord = string.Empty;
            string PassWordCheck = string.Empty;
            string Privilege = string.Empty;
            string License = string.Empty;

            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //若有接到則提取裡面的資料
            if (!string.IsNullOrWhiteSpace(json))
            {
                string[] splititem = json.Split('"');
                Name = splititem[3];//用"切開則參數會在3,7,11,15...的位置
                Phone = splititem[7];
                Mail = splititem[11];
                LineID = splititem[15];
                Account = splititem[19];
                PassWord = splititem[23];
                PassWordCheck = splititem[27];
                Privilege = splititem[31];
            }




            List<UserInfo> Check_Acc = new List<UserInfo>();
            List<UserInfo> Check_Acc_Lic = new List<UserInfo>();
            string ShowMessage = string.Empty;
            string ShowMessage2 = string.Empty;
            if (Name != string.Empty && Phone != string.Empty && Mail != string.Empty && LineID != string.Empty
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
                        context.Response.Write("{\"success\":\"account\"}");
                    }
                    else
                    {
                        string[] insertcolname = { "Name", "Phone", "Mail", "LineID", "Account", "PassWord","License", "Privilege","WhoCreate","CreateDate" }; 
                        string[] inserttablenamep = { "@Name", "@Phone", "@Mail", "@LineID", "@Account", "@PassWord", "@License", "@Privilege", "@WhoCreate", "@CreateDate" };
                        List<string> insert_P = new List<string>();
                        insert_P.Add(Name);
                        insert_P.Add(Phone);
                        insert_P.Add(Mail);
                        insert_P.Add(LineID);
                        insert_P.Add(Account);
                        insert_P.Add(PassWord);
                        insert_P.Add(License);
                        insert_P.Add(Privilege);
                        insert_P.Add("Manager");
                        insert_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        Create.InsertTable("Users", insertcolname, inserttablenamep, insert_P);
                        context.Response.ContentType = "text/json";
                        context.Response.Write("{\"success\":\"true\"}");
                    }
                }
                else
                {
                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\":\"PassWordWrong\"}");
                }   // "{\"success\":\"PassWordWrong\"}" 單一值JSON字串寫法 "[{\"success\":\"PassWordWrong\"}","{\"success\":\"PassWordWrong\"}]" JSON陣列字串寫法
            }
            else
            {
                context.Response.ContentType = "text/json";
                context.Response.Write("{\"success\":\"Empty\"}");
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