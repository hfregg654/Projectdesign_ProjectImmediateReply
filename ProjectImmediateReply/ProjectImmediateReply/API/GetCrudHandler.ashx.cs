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
using System.Web.SessionState;

namespace ProjectImmediateReply.API
{
    /// 查詢建表專用的泛型處理常式
    /// <summary>
    /// GetCrudHandler 的摘要描述
    /// </summary>
    public class GetCrudHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //準備DB工具以及宣告頁面檢查,班級檢查,接到的JSON字串
            DBTool Dbtool = new DBTool();
            string innertype = string.Empty;
            string ClassNumber = string.Empty;
            //接值的處理方法 Start 前端有寫到API/GetCrudHandler.ashx會接到此處
            string json = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //接值的處理方法 End
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
                                WHERE Users.ClassNumber=@ClassNumber AND Projects.DeleteDate IS NULL AND Users.WhoDelete IS NULL AND Projects.Complete='TRUE'
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
                string[] colname = { "Projects.ProjectName", "Users.[Name]", "Users.UserID", "Users.TeamName", "Users.TeamID", "Users.Privilege" };
                string[] colnamep = { "@ClassNumber" };
                string[] p = { ClassNumber };
                string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Users.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ORDER BY Users.TeamID
                                ";
                DataTable data = Dbtool.readTable("Projects", colname, logic, colnamep, p);//查班級的所有人
                if (data.Rows.Count != 0)
                {
                    //抓小組名
                    string[] groupcolname = { "TeamName" };
                    string[] groupcolnamep = { "@ClassNumber" };
                    string[] groupp = { ClassNumber };
                    string grouplogic = @"
                                WHERE ClassNumber=@ClassNumber AND TeamName IS NOT NULL AND DeleteDate IS NULL AND WhoDelete IS NULL
                                GROUP BY TeamName
                                ";
                    DataTable groupdata = Dbtool.readTable("Users", groupcolname, grouplogic, groupcolnamep, groupp);//查班級的所有小組
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
                                if (item["Privilege"].ToString() == "Leader")
                                    ClassMember.Name = $"★{item["Name"]}";
                                else
                                    ClassMember.Name = item["Name"].ToString();
                            if (data.Columns["TeamID"] != null && !Convert.IsDBNull(item["TeamID"]))
                                ClassMember.TeamID = Convert.ToInt32(item["TeamID"]);
                            if (data.Columns["ProjectName"] != null && !Convert.IsDBNull(item["ProjectName"]))
                                ClassMember.ProjectName = item["ProjectName"].ToString();
                            if (data.Columns["TeamName"] != null && !Convert.IsDBNull(item["TeamName"]))
                                ClassMember.TeamName = item["TeamName"].ToString();
                            if (groupdata.Columns["TeamName"] != null)
                                ClassMember.TeamNameGroup = grouplist.ToArray();


                            ProjectAll.Add(ClassMember);
                        }
                    }
                }
                else
                {
                    string[] colnamenotassign = { "Name", "UserID" };
                    string[] colnamepnotassign = { "@ClassNumber" };
                    string[] pnotassign = { ClassNumber };
                    string logicnotassign = @"
                                WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                    DataTable datanotassign = Dbtool.readTable("Users", colnamenotassign, logicnotassign, colnamepnotassign, pnotassign);//查班級的所有人
                    if (datanotassign.Rows.Count != 0)
                    {

                        foreach (DataRow item in datanotassign.Rows)
                        {
                            ForAssignTeam ClassMember = new ForAssignTeam();
                            if (data.Columns["UserID"] != null && !Convert.IsDBNull(item["UserID"]))
                                ClassMember.UserID = Convert.ToInt32(item["UserID"]);
                            if (data.Columns["Name"] != null && !Convert.IsDBNull(item["Name"]))
                                ClassMember.Name = item["Name"].ToString();

                            ProjectAll.Add(ClassMember);
                        }
                    }

                }
                //將最後結果以JSON形式放進回傳字串
                ShowTable = JsonConvert.SerializeObject(ProjectAll);
            }
            else if (innertype == "Detail")
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

            }
            else if (innertype == "ClassDetail")
            {
                string[] colname = { "UserID", "Name", "Phone", "Mail" };
                string[] colnamep = { "@ClassNumber" };
                string[] p = { ClassNumber };
                string logic = @"
                                WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                DataTable data = Dbtool.readTable("Users", colname, logic, colnamep, p);//查班級所有專案

                List<ForClassDetail> detaildatalist = new List<ForClassDetail>();

                foreach (DataRow item in data.Rows)
                {
                    ForClassDetail detail = new ForClassDetail();
                    detail.UserID = item["UserID"].ToString();
                    detail.Name = item["Name"].ToString();
                    detail.Phone = item["Phone"].ToString();
                    detail.Mail = item["Mail"].ToString();
                    detaildatalist.Add(detail);
                }
                ShowTable = JsonConvert.SerializeObject(detaildatalist);
            }
            else if (innertype == "ProjectDetail")
            {

                string[] colname = { "Projects.ProjectID", "Projects.ProjectName", "Users.TeamName", "Projects.DeadLine" };
                string[] colnamep = { "@ClassNumber" };
                string[] p = { ClassNumber };
                string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Projects.DeleteDate IS NULL AND Projects.WhoDelete IS NULL
                                GROUP BY Projects.ProjectID, Projects.ProjectName, Users.TeamName, Projects.DeadLine
                                ";
                DataTable data = Dbtool.readTable("Projects", colname, logic, colnamep, p);//查班級已分組的所有專案
                List<ForProjectDetail> detaildatalist = new List<ForProjectDetail>();
                if (data.Rows.Count > 0)
                {
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
                else
                {
                    string[] projectcolname = { "ProjectID", "ProjectName", "DeadLine" };
                    string[] projectcolnamep = { "@ClassNumber" };
                    string[] projectp = { ClassNumber };
                    string projectlogic = @"
                                WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                    DataTable projectdata = Dbtool.readTable("Projects", projectcolname, projectlogic, projectcolnamep, projectp);//查班級的所有專案

                    foreach (DataRow item in projectdata.Rows)
                    {
                        ForProjectDetail detail = new ForProjectDetail();
                        detail.ProjectID = item["ProjectID"].ToString();
                        detail.ProjectName = item["ProjectName"].ToString();
                        detail.DeadLine = Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd");
                        detaildatalist.Add(detail);
                    }
                    ShowTable = JsonConvert.SerializeObject(detaildatalist);
                }


            }
            else if (innertype == "ProjectDetail_Grades")
            {
                //若沒有專案ID參數則直接回傳
                if (string.IsNullOrWhiteSpace(ClassNumber))
                    return;
                string[] colnameclass = { "Users.Classnumber", "Users.TeamID" };
                string[] colnamepclass = { "@ProjectID" };
                string[] pclass = { ClassNumber };
                // ClassNumber 在ProjectDetail_Grades 為 ProjectID
                string logicclass = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Projects.ProjectID=@ProjectID AND Projects.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                GROUP BY Users.Classnumber,Users.TeamID
                                ";
                DataTable dataclass = Dbtool.readTable("Projects", colnameclass, logicclass, colnamepclass, pclass);//查班級




                //宣告評分頁面顯示型別的變數
                ForProjectDetail_Grades ProjectAll = new ForProjectDetail_Grades();
                //準備查詢語法
                string[] colnameteam = { "Users.UserID", "Projects.ProjectID", "Projects.ProjectName", "Users.[Name]", "Users.TeamName", "Users.Privilege" };
                string[] colnamepteam = { "@ClassNumber", "@TeamID" };
                string[] pteam = { dataclass.Rows[0]["Classnumber"].ToString(), dataclass.Rows[0]["TeamID"].ToString() };
                string logicteam = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Users.TeamID=@TeamID AND Projects.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ";

                DataTable data = Dbtool.readTable("Projects", colnameteam, logicteam, colnamepteam, pteam);//查班級的所有人
                                                                                                           //整理小組資料,宣告小組以及組員的Dictionary,以組別分別做排序，字典裡面放表。
                string[] colnamework = { "Works.WorkID", "Works.WorkName", "Works.WorkDescription", "Works.DeadLine", "Users.[Name]", "Works.UpdateTime", "Works.FilePath", "Works.CreateDate" }; //欲查詢的欄位
                string[] colnamepwork = { "@ProjectID" }; //以什麼欄位作查詢 @為資料庫內欄位
                string[] pwork = { ClassNumber }; //實際抓到的變數欄位 對照colnamepwork
                string logicwork = @"
                                INNER JOIN Users ON Users.UserID=Works.UserID
                                WHERE Works.ProjectID=@ProjectID AND Works.DeleteDate IS NULL AND Works.WhoDelete IS NULL
                                ";
                //前主後副 此方法一次只能作一次查詢或一次insert
                DataTable workdata = Dbtool.readTable("Works", colnamework, logicwork, colnamepwork, pwork);//查專案工作的所有工作項目
                List<string> member = new List<string>();
                List<UserNameGroupforPD_G> AllMember = new List<UserNameGroupforPD_G>();
                if (data.Rows.Count != 0)//如果根本沒有查回資料則將空資料回傳,有的話才開始整理
                {
                    foreach (DataRow item in data.Rows)
                    {
                        AllMember.Add(new UserNameGroupforPD_G()
                        {
                            UserID = Convert.ToInt32(item["UserID"]),
                            UserName = item["Name"].ToString()
                        });
                        //以小組組別分別整理,判斷是否為組長來分別做排序處理
                        if (item["Privilege"].ToString() == "Leader")
                        {
                            ProjectAll.LeaderName = item["Name"].ToString();
                            ProjectAll.ProjectID = Convert.ToInt32(item["ProjectID"]);
                            ProjectAll.ProjectName = item["ProjectName"].ToString();
                            ProjectAll.TeamName = item["TeamName"].ToString();
                        }
                        else
                            member.Add(item["Name"].ToString());
                    }
                    ProjectAll.MemberName = string.Join("、", member);
                    ProjectAll.NameGroup = AllMember;
                }
                if (workdata.Rows.Count != 0) //如果根本沒有查回資料則將空資料回傳,有的話才開始整理
                {
                    ProjectAll.inneritem = new List<InnerItem_Work>();
                    foreach (DataRow item in workdata.Rows)
                    {
                        TimeSpan ts = new TimeSpan(Convert.ToDateTime(item["UpdateTime"]).Ticks - Convert.ToDateTime(item["CreateDate"]).Ticks);
                        int SpendDate = (int)ts.TotalDays + 1;
                        ProjectAll.inneritem.Add(
                            new InnerItem_Work()
                            {
                                Name = item["Name"].ToString(),
                                WorkID = Convert.ToInt32(item["WorkID"]),
                                WorkName = item["WorkName"].ToString(),
                                WorkDescription = item["WorkDescription"].ToString(),
                                DeadLine = $"{Convert.ToDateTime(item["CreateDate"]).ToString("yyyy-MM-dd")} ~ {Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd")}",
                                UpdateTime = Convert.ToDateTime(item["UpdateTime"]).ToString("yyyy-MM-dd"),
                                SpendTime = $"{SpendDate}天",
                                FilePath = item["FilePath"].ToString(),
                            });
                    }
                }

                //將最後結果以JSON形式放進回傳字串
                ShowTable = JsonConvert.SerializeObject(ProjectAll);

            }
            else if (innertype == "ManageProject")
            {
                LogInfo Info = new LogInfo();
                if (context.Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
                {
                    Info = (LogInfo)context.Session["IsLogined"];
                }
                if (Info.Privilege != "Leader" && Info.Privilege != "User")
                    return;

                //宣告最後回傳的物件
                ForManageProject result = new ForManageProject();  //回傳的畫面全部資料
                DataTable lastdata = new DataTable(); //資料表部分 依照權限判斷顯示不同
                //準備查詢語法查詢此人的未完成工作
                string[] workscolname = { "ProjectID", "UserID", "WorkID", "WorkName", "WorkDescription", "DeadLine", "FilePath", "UpdateTime", "Complete" };
                string[] workscolnamep = { "@UserID" };
                string[] worksp = { Info.UserID.ToString() };
                string workslogic = @"
                                WHERE UserID=@UserID AND DeleteDate IS NULL AND WhoDelete IS NULL AND Complete=0
                                ";
                DataTable worksdata = Dbtool.readTable("Works", workscolname, workslogic, workscolnamep, worksp);//查此人的未完成工作
                //若此人目前無工作則查此人的專案ID(查專案所有工作用)
                DataTable projectdata = new DataTable();
                if (worksdata.Rows.Count <= 0)
                {
                    string[] projectcolname = { "ProjectID" };
                    string[] projectcolnamep = { "@UserID" };
                    string[] projectp = { Info.UserID.ToString() };
                    string projectlogic = @"
                                WHERE UserID=@UserID AND DeleteDate IS NULL AND WhoDelete IS NULL AND ProjectID IS NOT NULL
                                ";
                    projectdata = Dbtool.readTable("Users", projectcolname, projectlogic, projectcolnamep, projectp);//查此人的專案ID
                }

                //準備查詢語法查此專案的所有工作
                string[] allworksLcolname = { "ProjectID", "UserID", "WorkID", "WorkName", "WorkDescription", "DeadLine", "FilePath", "UpdateTime", "Complete" };
                string[] allworksLcolnamep = { "@ProjectID" };
                string[] allworksLp = { "-1" };
                if (projectdata.Rows.Count > 0)
                    allworksLp[0] = projectdata.Rows[0]["ProjectID"].ToString();
                else if (worksdata.Rows.Count > 0)
                    allworksLp[0] = worksdata.Rows[0]["ProjectID"].ToString();

                string allworksLlogic = @"
                                WHERE ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                DataTable allworksdata = Dbtool.readTable("Works", allworksLcolname, allworksLlogic, allworksLcolnamep, allworksLp);//查此專案的所有工作

                //準備查詢語法查此專案的所有已完成工作
                string[] ComworksLcolname = { "ProjectID", "UserID", "WorkID", "WorkName", "WorkDescription", "DeadLine", "FilePath", "UpdateTime", "Complete" };
                string[] ComworksLcolnamep = { "@ProjectID" };
                string[] ComworksLp = { "-1" };
                if (projectdata.Rows.Count > 0)
                    ComworksLp[0] = projectdata.Rows[0]["ProjectID"].ToString();
                else if (worksdata.Rows.Count > 0)
                    ComworksLp[0] = worksdata.Rows[0]["ProjectID"].ToString();

                string ComworksLlogic = @"
                                WHERE ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL AND Complete=1
                                ";
                DataTable Comworksdata = Dbtool.readTable("Works", ComworksLcolname, ComworksLlogic, ComworksLcolnamep, ComworksLp);//查此專案的所有已完成工作

                //若有查到資料並且此人為組長,更改顯示此專案的所有工作
                if (allworksdata.Rows.Count > 0 && Info.Privilege == "Leader")
                {
                    lastdata = allworksdata;
                }
                //為成員則顯示該成員的工作
                else if (allworksdata.Rows.Count > 0 && Info.Privilege == "User")
                {
                    lastdata = worksdata;
                }
                else
                {

                }

                string[] ProAndUscolname = { "Projects.ProjectName", "Users.TeamName", "Users.ClassNumber", "Projects.Complete" };
                string[] ProAndUscolnamep = { "@ProjectID" };
                string[] ProAndUsp = { "-1" };
                if (projectdata.Rows.Count > 0)
                    ProAndUsp[0] = projectdata.Rows[0]["ProjectID"].ToString();
                else if (worksdata.Rows.Count > 0)
                    ProAndUsp[0] = worksdata.Rows[0]["ProjectID"].ToString();
                string ProAndUslogic = @"
                                JOIN Projects ON Users.ProjectID=Projects.ProjectID
                                WHERE Projects.ProjectID=@ProjectID AND Projects.DeleteDate IS NULL AND Projects.WhoDelete IS NULL
                                ";
                DataTable ProAndUsdata = Dbtool.readTable("Users", ProAndUscolname, ProAndUslogic, ProAndUscolnamep, ProAndUsp);//查此專案的專案名,小組名,班級
                List<ViewWorks> TempVW = new List<ViewWorks>();
                List<ViewWorks> Allworks = new List<ViewWorks>();
                foreach (DataRow item in allworksdata.Rows)
                {
                    ViewWorks viewWorks = new ViewWorks();
                    if (!Convert.IsDBNull(item["UserID"]))
                        viewWorks.Work_UserID = Convert.ToInt32(item["UserID"]);
                    if (!Convert.IsDBNull(item["WorkID"]))
                        viewWorks.WorkID = Convert.ToInt32(item["WorkID"]);
                    if (!Convert.IsDBNull(item["WorkName"]))
                        viewWorks.WorkName = item["WorkName"].ToString();
                    if (!Convert.IsDBNull(item["WorkDescription"]))
                        viewWorks.WorkDescription = item["WorkDescription"].ToString();
                    if (!Convert.IsDBNull(item["DeadLine"]))
                        viewWorks.DeadLine = Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd");
                    if (!Convert.IsDBNull(item["UpdateTime"]))
                        viewWorks.UpdateTime = Convert.ToDateTime(item["UpdateTime"]).ToString("yyyy-MM-dd");
                    if (!Convert.IsDBNull(item["FilePath"]))
                        viewWorks.FilePath = item["FilePath"].ToString();
                    if (!Convert.IsDBNull(item["Complete"]))
                        viewWorks.Complete = Convert.ToBoolean(item["Complete"]);
                    Allworks.Add(viewWorks);
                }
                foreach (DataRow item in lastdata.Rows)
                {
                    ViewWorks viewWorks = new ViewWorks();
                    if (!Convert.IsDBNull(item["UserID"]))
                        viewWorks.Work_UserID = Convert.ToInt32(item["UserID"]);
                    if (!Convert.IsDBNull(item["WorkID"]))
                        viewWorks.WorkID = Convert.ToInt32(item["WorkID"]);
                    if (!Convert.IsDBNull(item["WorkName"]))
                        viewWorks.WorkName = item["WorkName"].ToString();
                    if (!Convert.IsDBNull(item["WorkDescription"]))
                        viewWorks.WorkDescription = item["WorkDescription"].ToString();
                    if (!Convert.IsDBNull(item["DeadLine"]))
                        viewWorks.DeadLine = Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd");
                    if (!Convert.IsDBNull(item["UpdateTime"]))
                        viewWorks.UpdateTime = Convert.ToDateTime(item["UpdateTime"]).ToString("yyyy-MM-dd");
                    if (!Convert.IsDBNull(item["FilePath"]))
                        viewWorks.FilePath = item["FilePath"].ToString();
                    if (!Convert.IsDBNull(item["Complete"]))
                        viewWorks.Complete = Convert.ToBoolean(item["Complete"]);
                    TempVW.Add(viewWorks);
                }
                if (Comworksdata.Rows.Count > 0 && Info.Privilege == "User")
                {
                    foreach (DataRow item in Comworksdata.Rows)
                    {
                        ViewWorks viewWorks = new ViewWorks();
                        if (!Convert.IsDBNull(item["UserID"]))
                            viewWorks.Work_UserID = Convert.ToInt32(item["UserID"]);
                        if (!Convert.IsDBNull(item["WorkID"]))
                            viewWorks.WorkID = Convert.ToInt32(item["WorkID"]);
                        if (!Convert.IsDBNull(item["WorkName"]))
                            viewWorks.WorkName = item["WorkName"].ToString();
                        if (!Convert.IsDBNull(item["WorkDescription"]))
                            viewWorks.WorkDescription = item["WorkDescription"].ToString();
                        if (!Convert.IsDBNull(item["DeadLine"]))
                            viewWorks.DeadLine = Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd");
                        if (!Convert.IsDBNull(item["UpdateTime"]))
                            viewWorks.UpdateTime = Convert.ToDateTime(item["UpdateTime"]).ToString("yyyy-MM-dd");
                        if (!Convert.IsDBNull(item["FilePath"]))
                            viewWorks.FilePath = item["FilePath"].ToString();
                        if (!Convert.IsDBNull(item["Complete"]))
                            viewWorks.Complete = Convert.ToBoolean(item["Complete"]);
                        TempVW.Add(viewWorks);
                    }
                }
                result.viewWorks = TempVW;
                result.UserID = Info.UserID;
                result.Name = Info.Name;
                result.Privilege = Info.Privilege;
                if (projectdata.Rows.Count > 0 || worksdata.Rows.Count > 0)
                {
                    if (projectdata.Rows.Count > 0)
                        result.ProjectID = Convert.ToInt32(projectdata.Rows[0]["ProjectID"]);
                    else if (worksdata.Rows.Count > 0)
                        result.ProjectID = Convert.ToInt32(worksdata.Rows[0]["ProjectID"]);
                    result.ProjectName = ProAndUsdata.Rows[0]["ProjectName"].ToString();
                    result.TeamName = ProAndUsdata.Rows[0]["TeamName"].ToString();
                    result.ClassNumber = ProAndUsdata.Rows[0]["ClassNumber"].ToString();
                    result.ProjectComplete = Convert.ToBoolean(ProAndUsdata.Rows[0]["Complete"]);
                }
                result.Schedule = SchedualCacu(Allworks);



                //將最後結果以JSON形式放進回傳字串
                ShowTable = JsonConvert.SerializeObject(result);
            }
            else if (innertype == "CreateWorks")
            {
                LogInfo Info = new LogInfo();
                if (context.Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
                {
                    Info = (LogInfo)context.Session["IsLogined"];
                }
                if (Info.Privilege != "Leader")
                    return;
                //準備查詢語法查詢此隊伍的所有資料
                string[] userscolname = { "ProjectID" };
                string[] userscolnamep = { "@UserID" };
                string[] usersp = { Info.UserID.ToString() };
                string userslogic = @"
                                WHERE UserID=@UserID AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                DataTable usersdata = Dbtool.readTable("Users", userscolname, userslogic, userscolnamep, usersp); //查User表的Leader UserID
                                                                                                                  //準備查詢語法查詢此隊伍的所有資料
                string[] teamcolname = { "UserID", "Name" };
                string[] teamcolnamep = { "@ProjectID" };
                string[] teamp = { "-1" };
                if (usersdata.Rows.Count > 0)
                    teamp[0] = usersdata.Rows[0]["ProjectID"].ToString();

                string teamlogic = @"
                                WHERE ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                DataTable teamdata = Dbtool.readTable("Users", teamcolname, teamlogic, teamcolnamep, teamp); //查User表的Leader UserID
                                                                                                             //準備查詢語法查詢此隊伍的所有資料
                string[] workscolname = { "Works.ProjectID", "Projects.ProjectName", "Users.TeamName", "Works.UserID", "Users.[Name]", "Works.WorkID", "Works.WorkName", "Works.WorkDescription", "Works.DeadLine" };
                string[] workscolnamep = { "@ProjectID" };
                string[] worksp = { "-1" };
                if (usersdata.Rows.Count > 0)
                    worksp[0] = usersdata.Rows[0]["ProjectID"].ToString();
                string workslogic = @"
                                join Projects on Works.ProjectID = Projects.ProjectID
                                join Users on Works.UserID = Users.UserID
                                WHERE Works.ProjectID=@ProjectID AND Works.DeleteDate IS NULL AND Works.WhoDelete IS NULL
                                ";
                DataTable worksdata = Dbtool.readTable("Works", workscolname, workslogic, workscolnamep, worksp);//查此人的所有工作


                string[] projectcolname = { "Users.ProjectID", "Projects.ProjectName", "Users.TeamName" };
                string[] projectcolnamep = { "@ProjectID" };
                string[] projectp = { "-1" };
                if (usersdata.Rows.Count > 0)
                    projectp[0] = usersdata.Rows[0]["ProjectID"].ToString();
                string projectlogic = @"
                                JOIN Projects ON Users.ProjectID = Projects.ProjectID
                                WHERE Users.ProjectID=@ProjectID AND Users.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ";
                DataTable projectdata = Dbtool.readTable("Users", projectcolname, projectlogic, projectcolnamep, projectp);//查此人的所有工作





                ForCreateWorks Alldata = new ForCreateWorks();

                if (projectdata.Rows.Count > 0)
                {
                    Alldata.ProjectID = Convert.ToInt32(projectdata.Rows[0]["ProjectID"]); //中括弧內字串是資料庫的欄位名稱
                    Alldata.ProjectName = projectdata.Rows[0]["ProjectName"].ToString();
                    Alldata.TeamName = projectdata.Rows[0]["TeamName"].ToString();
                }
                List<TeamMember> teamMember = new List<TeamMember>();
                List<WorkItem> workItem = new List<WorkItem>();
                foreach (DataRow item in teamdata.Rows)
                {
                    TeamMember cell = new TeamMember();
                    if (!Convert.IsDBNull(item["UserID"]))
                        cell.UserID = Convert.ToInt32(item["UserID"]);
                    if (!Convert.IsDBNull(item["Name"]))
                        cell.UserName = item["Name"].ToString();
                    teamMember.Add(cell);
                }
                Alldata.TeamMember = teamMember;
                foreach (DataRow item in worksdata.Rows)
                {
                    WorkItem cell = new WorkItem();
                    if (!Convert.IsDBNull(item["UserID"]))
                        cell.WorkID = Convert.ToInt32(item["WorkID"]);
                    if (!Convert.IsDBNull(item["WorkName"]))
                        cell.WorkName = item["WorkName"].ToString();
                    if (!Convert.IsDBNull(item["WorkDescription"]))
                        cell.WorkDescription = item["WorkDescription"].ToString();
                    if (!Convert.IsDBNull(item["DeadLine"]))
                        cell.DeadLine = Convert.ToDateTime(item["DeadLine"]).ToString("yyyy-MM-dd");
                    if (!Convert.IsDBNull(item["Name"]))
                        cell.OrderName = item["Name"].ToString();
                    workItem.Add(cell);
                }
                Alldata.WorkItems = workItem;
                //將最後結果以JSON形式放進回傳字串
                ShowTable = JsonConvert.SerializeObject(Alldata);
            }
            //以JSON形式回傳資料
            context.Response.ContentType = "text/json";
            context.Response.Write(ShowTable);
        }
        private string SchedualCacu(List<ViewWorks> viewWorks)
        {
            float sch = 0.0f;
            if (viewWorks.Count > 0)
            {
                float completework = 0.0f;
                foreach (ViewWorks item in viewWorks)
                {
                    if (item.Complete == true)
                    {
                        completework++;
                    }
                }
                sch = completework / viewWorks.Count;
            }
            return (Math.Floor(sch * 100)).ToString();
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