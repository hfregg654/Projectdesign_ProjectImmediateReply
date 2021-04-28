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
            //準備DB工具以及宣告頁面檢查,班級檢查,接到的JSON字串
            DBTool Dbtool = new DBTool();
            string innertype = string.Empty;
            string ClassNumber = string.Empty;
            string json = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //若有接到則提取裡面的資料
            if (!string.IsNullOrWhiteSpace(json))
            {
                innertype = json.Split('"')[3];//用"切開則參數會在3,7,11,15...的位置
                ClassNumber = json.Split('"')[7];
            }
            //若沒有接到頁面檢查的參數則直接回傳
            if (string.IsNullOrWhiteSpace(innertype))
                return;

            //宣告最後回傳的字串
            string ShowTable = string.Empty;

            //判斷頁面檢查參數的值執行相應的動作
            if (innertype == "GradesCrud")//專案評分頁面
            {
                //若沒有班級參數則直接回傳
                if (string.IsNullOrWhiteSpace(ClassNumber))
                    return;
                //宣告評分頁面顯示型別的變數
                List<ForGradesShow> ProjectAll = new List<ForGradesShow>();
                //將1~4組的資料丟進變數中
                for (int i = 1; i < 5; i++)
                {
                    string[] colname = { "Projects.ProjectName", "Users.[Name]", "Users.TeamName", "Users.Privilege" };
                    string[] colnamep = { "@ClassNumber", "@TeamID" };
                    string[] p = { ClassNumber, i.ToString() };
                    string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Users.TeamID=@TeamID
                                ";
                    DataTable data = Dbtool.readTable("Projects", colname, logic, colnamep, p);//查小組的所有人
                    //整理小組資料
                    List<string> member = new List<string>();
                    ForGradesShow ProjectTeam = new ForGradesShow();
                    if (data.Rows.Count != 0)
                    {
                        foreach (DataRow item in data.Rows)
                        {
                            if (item["Privilege"].ToString() == "Leader")
                            {
                                ProjectTeam.ProjectName = item["ProjectName"].ToString();
                                ProjectTeam.LeaderName = item["Name"].ToString();
                                ProjectTeam.TeamName = item["TeamName"].ToString();
                            }
                            else
                            {
                                member.Add(item["Name"].ToString());
                            }
                        }
                        ProjectTeam.MemberName=string.Join(",", member);
                        ProjectAll.Add(ProjectTeam);
                    }
                }
                //將最後結果以JSON形式放進回傳字串
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


            //以JSON形式回傳資料
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