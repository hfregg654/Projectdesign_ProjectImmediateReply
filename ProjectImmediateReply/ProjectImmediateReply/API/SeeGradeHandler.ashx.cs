using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectImmediateReply.Utility;
using ProjectImmediateReply.ViewModels;
using Newtonsoft.Json;
using System.IO;
using System.Data;


namespace ProjectImmediateReply.API
{
    /// <summary>
    /// SeeGradeHandler 的摘要描述
    /// </summary>
    public class SeeGradeHandler : IHttpHandler
    {
        
        private class Nameselect
        {
            public string Account { get; set; }
            public string UserName { get; set; }
        }
        public void ProcessRequest(HttpContext context)
        {
            //準備DB工具以及宣告頁面檢查,班級檢查,接到的JSON字串
            //不同的表一起顯示要用到ViewModel
            DBTool DbTool = new DBTool();
            string innerType = string.Empty;
            string Privilege = string.Empty;
            string ClassNumber = string.Empty;
            string TeamName = string.Empty;
            string Name = string.Empty;
            string json = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //若有接到則提取裡面的資料
            if (!string.IsNullOrWhiteSpace(json))
            {
                innerType = json.Split('"')[3];//用"切開則參數會在3,7,11,15...的位置
                Privilege = json.Split('"')[7];
                ClassNumber = json.Split('"')[11];
                TeamName = json.Split('"')[15];
                Name = json.Split('"')[19];
            }
            //若沒有接到頁面檢查的參數則直接回傳
            if (string.IsNullOrWhiteSpace(innerType))
            {
                return;
            }

            //判斷頁面檢查參數的值執行相應的動作
            if (innerType == "SeeGrade")//專案評分頁面
            {
                //若沒有班級參數則直接回傳(預設空欄位)
                if (string.IsNullOrWhiteSpace(ClassNumber))
                {
                    return;
                }
                //準備查詢語法
                if (!string.IsNullOrWhiteSpace(ClassNumber) && !string.IsNullOrWhiteSpace(TeamName) && !string.IsNullOrWhiteSpace(Name))
                {
                    string[] colname = { "Users.Mail", "Grades.PresidentProjectGrade", "Grades.PresidentInterviewGrade", "Grades.PresidentComments", "Grades.PMProjectGrade", "Grades.PMInterviewGrade", "Grades.PMComments" };
                    string[] colnamep = { "@ClassNumber", "@TeamName", "@Account" };
                    string[] p = { ClassNumber, TeamName, Name };
                    string logic = @"
                                        LEFT JOIN Grades ON Users.UserID=Grades.UserID
                                        WHERE Users.ClassNumber=@ClassNumber AND Users.TeamName=@TeamName AND Users.Account=@Account AND Grades.DeleteDate IS NULL AND Grades.WhoDelete IS NULL
                                        ";
                    DataTable data = DbTool.readTable("Users", colname, logic, colnamep, p);//查人

                    ForSeeGrade seeGrade = new ForSeeGrade();
                    if (data.Rows.Count != 0)
                    {
                        seeGrade = DbTool.GetForSeeGrade(data);
                    }

                    if (Convert.IsDBNull(data.Rows[0]["PresidentProjectGrade"]) || Convert.IsDBNull(data.Rows[0]["PresidentInterviewGrade"])
                        || Convert.IsDBNull(data.Rows[0]["PMProjectGrade"]) || Convert.IsDBNull(data.Rows[0]["PMInterviewGrade"])) 
                    {
                        seeGrade.Grade = 0;
                    }
                   
                    string ShowTable = JsonConvert.SerializeObject(seeGrade);

                    context.Response.ContentType = "text/json";
                    context.Response.Write(ShowTable);
                    return;

                }
                else if (!string.IsNullOrWhiteSpace(ClassNumber) && !string.IsNullOrWhiteSpace(TeamName))
                {
                    string[] Namecolname = { "Account", "Name" };
                    string[] Namecolnamep = { "@ClassNumber", "@TeamName" };
                    string[] Namep = { ClassNumber, TeamName };
                    string Namelogic = @"
                                WHERE ClassNumber=@ClassNumber AND TeamName=@TeamName
                                AND Name IS NOT NULL AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                    DataTable NameData = DbTool.readTable("Users", Namecolname, Namelogic, Namecolnamep, Namep);//查小組的所有人
                    Dictionary<string, string> NameNum = new Dictionary<string, string>();
                    foreach (DataRow item in NameData.Rows)
                    {
                        if (item != null && !string.IsNullOrWhiteSpace(item["Account"].ToString()) && !string.IsNullOrWhiteSpace(item["Name"].ToString()))
                        {
                            NameNum.Add(item["Account"].ToString(), item["Name"].ToString());
                        }
                    }

                    List<Nameselect> GetNameItem = new List<Nameselect>();

                    foreach (var item in NameNum)
                    {
                        Nameselect additem = new Nameselect();
                        additem.Account = item.Key;
                        additem.UserName = item.Value;
                        GetNameItem.Add(additem);
                    }
                    string ShowTable = JsonConvert.SerializeObject(GetNameItem);

                    context.Response.ContentType = "text/json";
                    context.Response.Write(ShowTable);
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(ClassNumber))
                {
                    string[] Teamcolname = { "TeamName" };
                    string[] Teamcolnamep = { "@ClassNumber" };
                    string[] Teamp = { ClassNumber };
                    string Teamlogic = @"
                                WHERE ClassNumber=@ClassNumber
                                AND TeamName IS NOT NULL AND DeleteDate IS NULL AND WhoDelete IS NULL
                                GROUP BY TeamName
                                ";
                    DataTable TeamData = DbTool.readTable("Users", Teamcolname, Teamlogic, Teamcolnamep, Teamp);//查班級的所有組別
                    List<string> TeamNum = new List<string>();
                    foreach (DataRow item in TeamData.Rows)
                    {
                        if (item != null && item[0].ToString() != "")
                        {
                            TeamNum.Add(item[0].ToString());
                        }
                    }
                    List<string> GetTeamItem = new List<string>();
                    foreach (string item in TeamNum)
                        GetTeamItem.Add(item);
                    string TeamChooseItem = string.Join(",", GetTeamItem);
                    context.Response.ContentType = "text/json";
                    context.Response.Write($"[{{\"choosegroup\":\"{TeamChooseItem}\"}}]");
                    return;
                }
            }
            else
                return;
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