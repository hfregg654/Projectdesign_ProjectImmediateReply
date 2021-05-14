using Newtonsoft.Json;
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
    /// AssignTeamHandler 的摘要描述
    /// </summary>
    public class AssignTeamHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //準備DB工具以及宣告頁面檢查,班級檢查,接到的JSON字串
            DBTool Dbtool = new DBTool();
            string json = string.Empty;
            string ClassNumber = string.Empty;

            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //SerializeObject()將物件與Dataset序列化(Serialize)為JSON
            //DeserializeObject()將Json反序列化(Deserialize)為物件
            var allpeople = JsonConvert.DeserializeObject<InnerItem_AssignTeam>(json);
            ClassNumber = allpeople.ClassNumber;


            //宣告最後回傳的字串
            string ShowTable = string.Empty;



            string[] pidcolname = { "ProjectID" };
            string[] pidcolnamep = { "@ClassNumber" };
            string[] pidp = { ClassNumber };
            string pidlogic = @"
                            WHERE ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL
                            ";
            DataTable piddata = Dbtool.readTable("Projects", pidcolname, pidlogic, pidcolnamep, pidp);//查ProjectID
            if (piddata.Rows.Count == 4)
            {
                RandomTool rtool = new RandomTool();
                List<string> assignedteam = new List<string>();
                List<string> assignpeople = new List<string>();
                List<string> assignproject = new List<string>();
                foreach (var item in allpeople.InnerItems)
                {
                    assignpeople.Add(item.UserID.ToString());
                }
                foreach (DataRow item in piddata.Rows)
                {
                    assignproject.Add(item["ProjectID"].ToString());
                }
                assignedteam = rtool.RandomAssign(assignpeople, assignproject, "Manager");
                for (int i = 0; i < assignedteam.Count; i++)
                {
                    string[] project_team = assignedteam[i].Split(',');
                    foreach (var inneritem in allpeople.InnerItems)
                    {
                        for (int j = 0; j < project_team.Length; j++)
                        {
                            if (inneritem.UserID == Convert.ToInt32(project_team[j]))
                            {

                                string[] pncolname = { "ProjectName" };
                                string[] pncolnamep = { "@ProjectID" };
                                string[] pnp = { project_team[0] };
                                string pnlogic = @"
                                                    WHERE ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL
                                                  ";
                                DataTable pndata = Dbtool.readTable("Projects", pncolname, pnlogic, pncolnamep, pnp);//查ProjectName

                                inneritem.ProjectName = pndata.Rows[0]["ProjectName"].ToString();
                                inneritem.TeamID = i + 1;
                                inneritem.TeamName = $"第{i + 1}組";

                                string[] updatecol_Logic = { "ProjectID=@ProjectID", "TeamID=@TeamID", "TeamName=@TeamName", "Privilege=@Privilege" }; /*  要更新的欄位*/
                                string Where_Logic = "UserID=@UserID";
                                string[] updatecolname_P = { "@ProjectID", "@TeamID", "@TeamName", "@Privilege", "@UserID" }; /*要帶入的參數格子*/
                                List<string> update_P = new List<string>();
                                update_P.Add(project_team[0]);
                                update_P.Add((i + 1).ToString());
                                update_P.Add($"第{i + 1}組");
                                if (j == 1)
                                    update_P.Add("Leader");
                                else
                                    update_P.Add("User");
                                update_P.Add(project_team[j]);
                                Dbtool.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                            }
                        }
                    }
                }
            }
            else
            {
                context.Response.ContentType = "text/json";
                context.Response.Write("{\"success\":\"Wrong\"}");
                return;
            }

            //以JSON形式回傳資料
            ShowTable = JsonConvert.SerializeObject(allpeople);
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