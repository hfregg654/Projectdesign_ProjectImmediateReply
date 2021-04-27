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
    /// GetCrudHandler 的摘要描述
    /// </summary>
    public class GetCrudHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            DBTool Dbtool = new DBTool();
            string innertype = string.Empty;
            string ClassNumber = string.Empty;
            string json = string.Empty;
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(json))
            {
                ClassNumber = json.Split('"')[3];
                innertype = json.Split('"')[7];
            }


            if (string.IsNullOrWhiteSpace(ClassNumber) || string.IsNullOrWhiteSpace(innertype))
                return;


            string ShowTable = string.Empty;
            if (innertype == "GradesCrud")
            {
                List<ForGradesShow> ProjectAll = new List<ForGradesShow>();
                for (int i = 1; i < 5; i++)
                {
                    string[] colname = { "Projects.ProjectName", "Users.[Name] as LeaderName", "Users.TeamName" };
                    string[] colnamep = { "@ClassNumber", "@Privilege", "@TeamID" };
                    string[] pl = { ClassNumber, "Leader", i.ToString() };
                    string[] pm = { ClassNumber, "User", i.ToString() };
                    string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Users.Privilege=@Privilege AND Users.TeamID=@TeamID
                                ";
                    List<ForGradesShow> datal = Dbtool.ChangeTypeForGradesShow(Dbtool.readTable("Projects", colname, logic, colnamep, pl));
                    List<ForGradesShow> datam = Dbtool.ChangeTypeForGradesShow(Dbtool.readTable("Projects", colname, logic, colnamep, pm));
                    List<string> member = new List<string>();
                    foreach (var item in datam)
                    {
                        member.Add(item.LeaderName);
                    }

                    if (datal.Count != 0 || datam.Count != 0)
                    {
                        ProjectAll.Add(datal[0]);
                        int count = ProjectAll.Count;
                        ProjectAll[count-1].MemberName = string.Join(",", member);
                    }
                }

                ShowTable = JsonConvert.SerializeObject(ProjectAll);
            }
            else
            {
                string[] colname = { "ClassNumber", "License", "Name" };
                string[] colnamep = { "@ClassNumber" };
                string[] p = { ClassNumber };
                List<UserInfo> data = Dbtool.ChangeTypeUserInfo(Dbtool.readTable("Users", colname, "WHERE ClassNumber=@ClassNumber", colnamep, p));

                ShowTable = JsonConvert.SerializeObject(data);
            }



            context.Response.ContentType = "text/json";
            context.Response.Write(ShowTable);
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