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
                //準備查詢語法
                string[] colname = { "Projects.ProjectID","Projects.ProjectName", "Users.[Name]", "Users.TeamName", "Users.Privilege", "Users.TeamID" };
                string[] colnamep = { "@ClassNumber" };
                string[] p = { ClassNumber };
                string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Projects.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ORDER BY Users.TeamID
                                ";
                DataTable data = Dbtool.readTable("Projects", colname, logic, colnamep, p);//查班級的所有人
                //整理小組資料,宣告小組以及組員的Dictionary,以組別分別做排序，字典裡面放表。
                Dictionary<int, ForGradesShow> ProjectTeam = new Dictionary<int, ForGradesShow>();
                Dictionary<int, List<string>> member = new Dictionary<int, List<string>>();
                SortTool stool = new SortTool();
                if (data.Rows.Count != 0)//如果根本沒有查回資料則將空資料回傳,有的話才開始整理
                {
                    foreach (DataRow item in data.Rows)
                    {
                        //以小組組別分別整理,判斷是否為組長來分別做排序處理
                        switch (item["TeamID"].ToString())
                        {
                            case "1":
                                if (item["Privilege"].ToString() == "Leader")
                                    ProjectTeam = stool.SortTeamLeader(item, ProjectTeam, 1);//組長進組長排序
                                else
                                    member = stool.SortTeamMember(item, member, 1);//組員進組員排序
                                break;
                            case "2":
                                if (item["Privilege"].ToString() == "Leader")
                                    ProjectTeam = stool.SortTeamLeader(item, ProjectTeam, 2);
                                else
                                    member = stool.SortTeamMember(item, member, 2);
                                break;
                            case "3":
                                if (item["Privilege"].ToString() == "Leader")
                                    ProjectTeam = stool.SortTeamLeader(item, ProjectTeam, 3);
                                else
                                    member = stool.SortTeamMember(item, member, 3);
                                break;
                            case "4":
                                if (item["Privilege"].ToString() == "Leader")
                                    ProjectTeam = stool.SortTeamLeader(item, ProjectTeam, 4);
                                else
                                    member = stool.SortTeamMember(item, member, 4);
                                break;
                            default:
                                break;
                        }
                    }
                    //將整理完成後的小組Dictionary加入整理後的組員,最後加入回傳字串中
                    foreach (var item in ProjectTeam)
                    {
                        ForGradesShow newitem = item.Value;
                        newitem.MemberName = string.Join(",", member[item.Key]);
                        ProjectAll.Add(newitem);
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
                List<UserInfo> data = Dbtool.ChangeTypeUserInfo(Dbtool.readTable("Users", colname, "WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL", colnamep, p));

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