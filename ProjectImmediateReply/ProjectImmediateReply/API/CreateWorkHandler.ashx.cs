using Newtonsoft.Json;
using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// CreateWorkHandler 的摘要描述
    /// </summary>
    public class CreateWorkHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            Utility.DBTool Create = new Utility.DBTool();
            string innertype = "";
            string json;
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //若有接到則提取裡面的資料
            if (!string.IsNullOrWhiteSpace(json))
            {
                innertype = json.Split('"')[3];//用"切開則參數會在3,7,11,15...的位置
            }
            LogInfo Info = new LogInfo();
            if (context.Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
            {
                Info = (LogInfo)context.Session["IsLogined"];
            }
            if (innertype == "ChangeTeamName")
            {
                string[] temp = json.Split('"');
                string NewTeamName = temp[7];
                string[] temptemp = temp[10].Split('}', ':');
                string ProjectID = temptemp[1];
                if (NewTeamName.Length > 50)
                    return;

                string[] updatecol_Logic = { "TeamName=@TeamName" }; //欲更新的欄位 Name是開頭欄位名稱 @Name是欄位名稱底下格子
                string Where_Logic = "ProjectID=@ProjectID";
                string[] updatecolname_P = { "@TeamName", "@ProjectID" };
                List<string> update_P = new List<string>();
                update_P.Add(NewTeamName);
                update_P.Add(ProjectID);
                Create.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P); //updatecolname_P 傳入的@格 update_P傳入的值
            }
            else if (innertype == "CreateNewWork")
            {

                var newWorkItem = JsonConvert.DeserializeObject<NewWorkItem>(json);
                string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string WhoCreate = Info.Name;
                if (newWorkItem.DeadLine >= DateTime.Now)
                {
                    string[] colname = { "ProjectID", "UserID", "WorkName", "WorkDescription", "DeadLine", "Complete", "CreateDate", "WhoCreate" };
                    string[] colnamep = { "@ProjectID", "@UserID", "@WorkName", "@WorkDescription", "@DeadLine", "@Complete", "@CreateDate", "@WhoCreate" };
                    List<string> p = new List<string>();
                    p.Add(newWorkItem.ProjectID.ToString());
                    p.Add(newWorkItem.OrderMember.ToString());
                    p.Add(newWorkItem.WorkName);
                    p.Add(newWorkItem.WorkDescription);
                    p.Add(Convert.ToDateTime(newWorkItem.DeadLine).ToString("yyyy/MM/dd HH:mm:ss"));
                    p.Add("False");
                    p.Add(CreateDate);
                    p.Add(WhoCreate);
                    Create.InsertTable("Works", colname, colnamep, p);
                }
                else
                {
                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\":\"日期不可小於當前日期\"}");
                    return;
                }
            }
            else if (innertype == "UpdateWork")
            {
                var updateWorkItem = JsonConvert.DeserializeObject<NewWorkItem>(json);
                if (updateWorkItem.DeadLine >= DateTime.Now)
                {
                    // 有@就是參數
                    string[] updatecol_Logic = { "WorkName=@WorkName", "WorkDescription=@WorkDescription", "DeadLine=@DeadLine", "UserID=@UserID" }; //欲更新的欄位 Name是開頭欄位名稱 @Name是欄位名稱底下格子
                    string Where_Logic = "WorkID=@WorkID";
                    string[] updatecolname_P = { "@WorkName", "@WorkDescription", "@DeadLine", "@UserID", "@WorkID" };
                    List<string> update_P = new List<string>();
                    update_P.Add(updateWorkItem.WorkName);
                    update_P.Add(updateWorkItem.WorkDescription);
                    update_P.Add(updateWorkItem.DeadLine.ToString("yyyy-MM-dd"));
                    update_P.Add(updateWorkItem.OrderMember.ToString()); //這裡是UserID
                    update_P.Add(updateWorkItem.ProjectID.ToString()); //其實這裡是workID
                    Create.UpdateTable("Works", updatecol_Logic, Where_Logic, updatecolname_P, update_P); //updatecolname_P 傳入的@格 update_P傳入的值
                }
                else
                {
                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\":\"日期不可小於當前日期\"}");
                    return;
                }
            }
            else if (innertype == "DeleteWork")
            {
                string[] temp = json.Split('"', '}', ':');
                string WorkID = temp[8];
                // 有@就是參數
                string[] updatecol_Logic = { "DeleteDate=@DeleteDate", "WhoDelete=@WhoDelete" }; //欲更新的欄位 Name是開頭欄位名稱 @Name是欄位名稱底下格子
                string Where_Logic = "WorkID=@WorkID";
                string[] updatecolname_P = { "@DeleteDate", "@WhoDelete", "@WorkID" };
                List<string> update_P = new List<string>();
                update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                update_P.Add(Info.Name);
                update_P.Add(WorkID);
                Create.UpdateTable("Works", updatecol_Logic, Where_Logic, updatecolname_P, update_P); //updatecolname_P 傳入的@格 update_P傳入的值
            }
        }
        private class NewWorkItem
        {
            [JsonProperty("ProjectID")]
            public int ProjectID { get; set; }
            [JsonProperty("OrderMember")]
            public int OrderMember { get; set; }
            [JsonProperty("WorkName")]
            public string WorkName { get; set; }
            [JsonProperty("WorkDescription")]
            public string WorkDescription { get; set; }
            [JsonProperty("DeadLine")]
            public DateTime DeadLine { get; set; }

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