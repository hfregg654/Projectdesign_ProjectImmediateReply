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
                ClassNumber = json.Split('"')[11];
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
                if (!string.IsNullOrWhiteSpace(ClassNumber))
                {
                    string[] Teamcolname = { "TeamName" };
                    string[] Teamcolnamep = { "@ClassNumber" };
                    string[] Teamp = { ClassNumber };
                    string Teamlogic = @"
                                WHERE ClassNumber=@ClassNumber
                                AND TeamName!='NULL'
                                GROUP BY TeamName
                                ";
                    DataTable TeamData = DbTool.readTable("Users", Teamcolname, Teamlogic, Teamcolnamep, Teamp);//查班級的所有組別
                    //ShowTable = JsonConvert.SerializeObject(ProjectAll);
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
                    {
                        string newItem = $"'{item}'";
                        GetTeamItem.Add(newItem);
                    }
                    string TeamChooseItem = string.Join(",", GetTeamItem);
                    context.Response.ContentType = "text/json";
                    context.Response.Write($"[{{\"choosegroup\":\"{TeamChooseItem}\"}}]");
                }

                string[] Namecolname = { "Name" };
                string[] Namecolnamep = { "@ClassNumber" };
                string[] Namep = { ClassNumber };
                string Namelogic = @"
                                WHERE ClassNumber=@ClassNumber
                                ";
                DataTable NameData = DbTool.readTable("Users", Namecolname, Namelogic, Namecolnamep, Namep);//查小組的所有人

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