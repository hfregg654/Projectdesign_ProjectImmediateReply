using Newtonsoft.Json;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// ManageProject_LeaderCheckHandler 的摘要描述
    /// </summary>
    public class ManageProject_LeaderCheckHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string checktype;
            string id;
            string opinion;
            string json;
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            var LeaderCheck = JsonConvert.DeserializeObject<ForLeaderCheck>(json);
            checktype = LeaderCheck.CheckType;
            id = LeaderCheck.ID.ToString();
            opinion = LeaderCheck.Opinion;

            DBTool Dbtool = new DBTool();
            if (checktype == "WorkComplete")
            {
                string[] updatecol_Logic = { "Complete=@Complete" }; /*  要更新的欄位*/
                string Where_Logic = "WorkID=@WorkID";
                string[] updatecolname_P = { "@Complete", "@WorkID" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add("TRUE");
                update_P.Add(id);
                Dbtool.UpdateTable("Works", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
            }
            else if (checktype == "WorkFailed")
            {
                string[] colname = { "Works.WorkName", "Users.Mail" };
                string[] colnamep = { "@WorkID" };
                string[] p = { id };
                string logic = @"
                                INNER JOIN Works ON Users.UserID=Works.UserID
                                WHERE Works.WorkID=@WorkID AND Users.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ";
                DataTable data = Dbtool.readTable("Users", colname, logic, colnamep, p);//查被駁回人的Mail
                string Mailaddress;
                if (data.Rows.Count > 0)
                    Mailaddress = data.Rows[0]["Mail"].ToString();
                else
                    Mailaddress = "";
                LogInfo Info = new LogInfo();
                if (context.Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
                {
                    Info = (LogInfo)context.Session["IsLogined"];
                }

                string mailopinion = string.Join("<br/>",opinion.Split('\n'));
                
                MailTool mtool = new MailTool();
                mtool.SendMail("shiyuance989898@gmail.com", Mailaddress, Info.Name, $"工作{data.Rows[0]["WorkName"]}駁回", $"您的工作{data.Rows[0]["WorkName"]}已被組長駁回<br/>駁回原因：<br/>{mailopinion}", "1qazxcvfr432wsde");
            }
            else if (checktype == "ProjectComplete")
            {
                string[] updatecol_Logic = { "Complete=@Complete" }; /*  要更新的欄位*/
                string Where_Logic = "ProjectID=@ProjectID";
                string[] updatecolname_P = { "@Complete", "@ProjectID" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add("TRUE");
                update_P.Add(id);
                Dbtool.UpdateTable("Projects", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
            }
        }
        private class ForLeaderCheck
        { 
            [JsonProperty("CheckType")]
            public string CheckType { get; set; }
            [JsonProperty("id")]
            public int ID { get; set; }
            [JsonProperty("opinion")]
            public string Opinion { get; set; }

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