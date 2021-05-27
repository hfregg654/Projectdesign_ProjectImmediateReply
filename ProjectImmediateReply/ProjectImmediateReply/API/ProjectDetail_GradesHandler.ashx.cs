using Newtonsoft.Json;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using ProjectImmediateReply.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;


namespace ProjectImmediateReply.API
{
    /// <summary>
    /// ProjectDetail_GradesHandler 的摘要描述
    /// </summary>
    /// System.Web.SessionState.IRequiresSessionState 泛型抓session的方法
    public class ProjectDetail_GradesHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            DBTool DbTool = new DBTool();
            //接值的處理方法 Start 前端有寫到API/GetCrudHandler.ashx會接到此處
            string json = string.Empty;
            string UserID = string.Empty;
            string ProjectGrade = string.Empty;
            string InterviewGrade = string.Empty;
            string Comments = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(json))
            {
                //string[] jsonsplit= json.Split(':', ',','"','{','}');
                UserID = json.Split(':', ',')[1];
                ProjectGrade = json.Split('"')[5];
                InterviewGrade = json.Split('"')[9];
                Comments = json.Split('"')[13];
            }
            string newComments = string.Join("/", Comments.Split('\n'));
            //接值的處理方法 End
            LogInfo Info = new LogInfo();
            if (context.Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
            {
                Info = (LogInfo)context.Session["IsLogined"];
            }
            int PGNum = 0; int IGNum = 0; int id = 0;
            //嘗試轉換 轉換失敗就傳False 轉換成功就傳True
            if (!(int.TryParse(ProjectGrade, out PGNum)) || !(int.TryParse(InterviewGrade, out IGNum)) || !(int.TryParse(UserID, out id)))
            {
                context.Response.ContentType = "text/json";
                context.Response.Write("[{\"success\":\"wrong\"}]");
                return;
            }

            if (PGNum >= 0 && PGNum <= 100 && IGNum >= 0 && IGNum <= 100)
            {
                if (Info.UserID == 1)
                {
                    string[] readcolname = { "UserID" };
                    string[] Pname = { "@UserID" };
                    string[] P = { UserID };
                    string Logic = @"WHERE UserID=@UserID";
                    DataTable data = DbTool.readTable("Grades", readcolname, Logic, Pname, P);
                    if (data.Rows.Count == 0)
                    {
                        string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        string WhoCreate = "Grades";
                        string[] insertcolname = { "UserID", "PresidentProjectGrade", "PresidentInterviewGrade", "PresidentComments", "CreateDate", "WhoCreate" };
                        string[] insertcolname_P = { "@UserID", "@PresidentProjectGrade", "@PresidentInterviewGrade", "@PresidentComments", "@CreateDate", "@WhoCreate" };
                        List<string> insert_P = new List<string>();
                        insert_P.Add(UserID);
                        insert_P.Add(ProjectGrade);
                        insert_P.Add(InterviewGrade);
                        insert_P.Add(newComments);
                        insert_P.Add(CreateDate);
                        insert_P.Add(WhoCreate);
                        DbTool.InsertTable("Grades", insertcolname, insertcolname_P, insert_P);
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"true\"}]");
                    }
                    else
                    {
                        string[] updatecol_Logic = { "PresidentProjectGrade=@PresidentProjectGrade", "PresidentInterviewGrade=@PresidentInterviewGrade", "PresidentComments=@PresidentComments" };
                        string Where_Logic = "UserID=@UserID";
                        string[] updateColname_P = { "@PresidentProjectGrade", "@PresidentInterviewGrade", "@PresidentComments", "@UserID" };
                        List<string> update_P = new List<string>();
                        update_P.Add(ProjectGrade);
                        update_P.Add(InterviewGrade);
                        update_P.Add(newComments);
                        update_P.Add(UserID);
                        DbTool.UpdateTable("Grades", updatecol_Logic, Where_Logic, updateColname_P, update_P);
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"true\"}]");
                    }
                }
                else
                {
                    string[] readcolname = { "UserID" };
                    string[] Pname = { "@UserID" };
                    string[] P = { UserID };
                    string Logic = @"WHERE UserID=@UserID";
                    DataTable data = DbTool.readTable("Grades", readcolname, Logic, Pname, P);
                    if (data.Rows.Count == 0)
                    {
                        string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        string WhoCreate = "Grades";
                        string[] insertcolname = { "UserID", "PMProjectGrade", "PMInterviewGrade", "PMComments", "CreateDate", "WhoCreate" };
                        string[] insertcolname_P = { "@UserID", "@PMProjectGrade", "@PMInterviewGrade", "@PMComments", "@CreateDate", "@WhoCreate" };
                        List<string> insert_P = new List<string>();
                        insert_P.Add(UserID);
                        insert_P.Add(ProjectGrade);
                        insert_P.Add(InterviewGrade);
                        insert_P.Add(newComments);
                        insert_P.Add(CreateDate);
                        insert_P.Add(WhoCreate);
                        DbTool.InsertTable("Grades", insertcolname, insertcolname_P, insert_P);
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"true\"}]");
                    }
                    else
                    {
                        string[] updatecol_Logic = { "PMProjectGrade=@PMProjectGrade", "PMInterviewGrade=@PMInterviewGrade", "PMComments=@PMComments" };
                        string Where_Logic = "UserID=@UserID";
                        string[] updateColname_P = { "@PMProjectGrade", "@PMInterviewGrade", "@PMComments", "@UserID" };
                        List<string> update_P = new List<string>();
                        update_P.Add(ProjectGrade);
                        update_P.Add(InterviewGrade);
                        update_P.Add(newComments);
                        update_P.Add(UserID);
                        DbTool.UpdateTable("Grades", updatecol_Logic, Where_Logic, updateColname_P, update_P);
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"true\"}]");
                    }
                }
            }
            else
            {
                context.Response.ContentType = "text/json";
                context.Response.Write("[{\"success\":\"ScoreWrong\"}]");
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