using ProjectImmediateReply.Log;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// CreateProjectHandler 的摘要描述
    /// </summary>
    public class CreateProjectHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        { 
            DBTool DbTool = new DBTool();
            //取得傳過來的資料
            string ClassNumber = context.Request.Form["ClassNumber"];
            string ProjectName = context.Request.Form["ProjectName"];
            string DeadLine = context.Request.Form["DeadLine"];
            string Privilege = context.Request.Form["Privilege"];
            //先檢查傳過來的值有沒有問題並先定義回傳的訊息
            string success = "";
            if (string.IsNullOrWhiteSpace(ClassNumber) || string.IsNullOrWhiteSpace(ProjectName) || string.IsNullOrWhiteSpace(DeadLine) || Privilege != "Manager")
            {
                success = "[{\"success\":\"Wrong\"}]";
                context.Response.ContentType = "text/json";
                context.Response.Write(success);  //先傳到網頁上 AJAX裡面寫好的再去抓
                return;
            }
            try
            {
                string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string WhoCreate = Privilege;

                string[] projectcolname = { "ProjectID" };
                string[] projectcolnamep = { "@ClassNumber" };
                string[] projectp = { ClassNumber };
                string projectlogic = @"
                                WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                DataTable projectdata = DbTool.readTable("Projects", projectcolname, projectlogic, projectcolnamep, projectp);//查班級的所有專案
                if (projectdata.Rows.Count>=4)
                {
                    success = "[{\"success\":\"ProjectCountWrong\"}]";
                    context.Response.ContentType = "text/json";
                    context.Response.Write(success);
                    return;
                }


                if (Convert.ToDateTime(DeadLine) > Convert.ToDateTime(CreateDate))
                {
                    string[] colname = { "ClassNumber", "ProjectName","Complete", "DeadLine", "CreateDate", "WhoCreate" };
                    string[] colnamep = { "@ClassNumber", "@ProjectName", "@Complete", "@DeadLine", "@CreateDate", "@WhoCreate" };
                    List<string> p = new List<string>();
                    p.Add(ClassNumber);
                    p.Add(ProjectName);
                    p.Add("false");
                    p.Add(DeadLine);
                    p.Add(CreateDate);
                    p.Add(WhoCreate);
                   
                    DbTool.InsertTable("Projects", colname, colnamep, p);

                    success = "[{\"success\":\"true\"}]";
                    context.Response.ContentType = "text/json";
                    context.Response.Write(success);
                }
                else
                {
                    success = "[{\"success\":\"DateWrong\"}]";
                    context.Response.ContentType = "text/json";
                    context.Response.Write(success);
                    return;
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