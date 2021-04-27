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
                    string[] colname = { "Projects.ProjectName", "Users.[Name] as LeaderName", "Users.TeamName" };
                    string[] colnamep = { "@ClassNumber", "@Privilege", "@TeamID" };
                    string[] pl = { ClassNumber, "Leader", i.ToString() };
                    string[] pm = { ClassNumber, "User", i.ToString() };
                    string logic = @"
                                INNER JOIN Users ON Projects.ProjectID=Users.ProjectID
                                WHERE Users.ClassNumber=@ClassNumber AND Users.Privilege=@Privilege AND Users.TeamID=@TeamID
                                ";
                    List<ForGradesShow> datal = Dbtool.ChangeTypeForGradesShow(Dbtool.readTable("Projects", colname, logic, colnamep, pl));//查組長
                    List<ForGradesShow> datam = Dbtool.ChangeTypeForGradesShow(Dbtool.readTable("Projects", colname, logic, colnamep, pm));//查組員
                    //一開始查回來的資料大家都會在LeaderName上,將組員資料放入MemberName
                    List<string> member = new List<string>();
                    foreach (var item in datam)
                    {
                        member.Add(item.LeaderName);
                    }
                    if (datal.Count != 0 || datam.Count != 0)
                    {
                        ProjectAll.Add(datal[0]);
                        int count = ProjectAll.Count;
                        ProjectAll[count - 1].MemberName = string.Join(",", member);
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