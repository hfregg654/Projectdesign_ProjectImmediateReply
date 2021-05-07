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
                string[] colname = { "Projects.ProjectID", "Projects.ProjectName", "Users.[Name]", "Users.TeamName", "Users.Privilege", "Users.TeamID" };
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
            else if (innertype == "AssignTeam")
            {
                //若沒有班級參數則直接回傳
                if (string.IsNullOrWhiteSpace(ClassNumber))
                    return;
                //宣告評分頁面顯示型別的變數
                List<ForAssignTeam> ProjectAll = new List<ForAssignTeam>();
                //準備查詢語法 INNER JOIN聯集
                string[] colname = { "Projects.ProjectName", "Users.[Name]", "Users.UserID", "Users.TeamName", "Users.TeamID" };
                string[] colnamep = { "@ClassNumber" };
                string[] p = { ClassNumber };
                string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Projects.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ORDER BY Users.TeamID
                                ";
                DataTable data = Dbtool.readTable("Projects", colname, logic, colnamep, p);//查班級的所有人

                //抓小組名
                string[] groupcolname = { "TeamName" };
                string[] groupcolnamep = { "@ClassNumber" };
                string[] groupp = { ClassNumber };
                string grouplogic = @"
                                WHERE ClassNumber=@ClassNumber AND TeamName IS NOT NULL AND DeleteDate IS NULL AND WhoDelete IS NULL
                                GROUP BY TeamName
                                ";
                DataTable groupdata = Dbtool.readTable("Users", groupcolname, grouplogic, groupcolnamep, groupp);//查班級的所有人
                List<string> grouplist = new List<string>();
                if (groupdata.Rows.Count != 0)
                {
                    foreach (DataRow item in groupdata.Rows)
                    {
                        grouplist.Add(item["TeamName"].ToString());
                    }
                }
                if (data.Rows.Count != 0)
                {

                    foreach (DataRow item in data.Rows)
                    {
                        ForAssignTeam ClassMember = new ForAssignTeam();
                        if (data.Columns["UserID"] != null && !Convert.IsDBNull(item["UserID"]))
                            ClassMember.UserID = Convert.ToInt32(item["UserID"]);
                        if (data.Columns["Name"] != null && !Convert.IsDBNull(item["Name"]))
                            ClassMember.Name = item["Name"].ToString();
                        if (data.Columns["TeamID"] != null && !Convert.IsDBNull(item["TeamID"]))
                            ClassMember.TeamID = Convert.ToInt32(item["TeamID"]);
                        if (data.Columns["ProjectName"] != null && !Convert.IsDBNull(item["ProjectName"]))
                            ClassMember.ProjectName = item["ProjectName"].ToString();
                        if (data.Columns["TeamName"] != null && !Convert.IsDBNull(item["TeamName"]))
                            ClassMember.TeamName = item["TeamName"].ToString();
                        if (groupdata.Columns["TeamName"] != null)
                            ClassMember.TeamNameGroup = grouplist.ToArray();

                        Array.Sort(ClassMember.Name.ToArray(), ClassMember.TeamNameGroup);

                        ProjectAll.Add(ClassMember);
                    }

                }
                //將最後結果以JSON形式放進回傳字串
                ShowTable = JsonConvert.SerializeObject(ProjectAll);
            }
            else if (innertype == "ProjectDetail")
            {
                if (string.IsNullOrWhiteSpace(ClassNumber))
                {
                    PageTool ptool = new PageTool();
                    List<string> classnumber = ptool.GetClassNumber();
                    getClassnumber classnumberjson = new getClassnumber();
                    classnumberjson.chooseclass = classnumber.ToArray();
                    ShowTable = JsonConvert.SerializeObject(classnumberjson);
                    context.Response.ContentType = "text/json";
                    context.Response.Write(ShowTable);
                    return;
                }
                else
                {
                    string[] colname = { "ProjectID", "ProjectName", "TeamName", "DeadLine" };
                    string[] colnamep = { "@ClassNumber" };
                    string[] p = { ClassNumber };
                    string logic = @"
                                WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                    DataTable data = Dbtool.readTable("Projects", colname, logic, colnamep, p);//查班級所有專案

                    List<ForProjectDetail> detaildatalist = new List<ForProjectDetail>();

                    foreach (DataRow item in data.Rows)
                    {
                        ForProjectDetail detail = new ForProjectDetail();
                        detail.ProjectID = item["ProjectID"].ToString();
                        detail.ProjectName = item["ProjectName"].ToString();
                        detail.TeamName = item["TeamName"].ToString();
                        detail.DeadLine = Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd");
                        detaildatalist.Add(detail);
                    }
                    ShowTable = JsonConvert.SerializeObject(detaildatalist);
                }
            }
            else
            {

            }


            //以JSON形式回傳資料
            context.Response.ContentType = "text/json";
            context.Response.Write(ShowTable);
        }
        private class getClassnumber
        {
            public string[] chooseclass { get; set; }
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