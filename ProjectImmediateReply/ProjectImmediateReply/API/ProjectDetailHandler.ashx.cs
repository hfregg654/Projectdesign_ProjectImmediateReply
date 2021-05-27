using Newtonsoft.Json;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// ProjectDetailHandler 的摘要描述
    /// </summary>
    public class ProjectDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //準備DB工具以及宣告頁面檢查,班級檢查,接到的JSON字串
            DBTool Dbtool = new DBTool();
            string Type = string.Empty;
            string ProjectID = string.Empty;
            string json = string.Empty;

            string ProjectName = string.Empty;
            string TeamName = string.Empty;
            string DeadLine = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //若有接到則提取裡面的資料
            if (!string.IsNullOrWhiteSpace(json))
            {
                string[] splititem = json.Split('"');
                Type = splititem[3];//用"切開則參數會在3,7,11,15...的位置
                ProjectID = splititem[7];
            }
            //若沒有接到頁面檢查的參數則直接回傳
            if (string.IsNullOrWhiteSpace(Type))
                return;

            if (Type == "Edit")
            {
                ProjectName = json.Split('"')[11];
                TeamName = json.Split('"')[15];
                DeadLine = json.Split('"')[19];


                if (Convert.ToDateTime(DeadLine) >= DateTime.Now)
                {

                    string[] updatecol_Logic = { "ProjectName=@ProjectName", "DeadLine=@DeadLine" }; /*  要更新的欄位*/
                    string Where_Logic = "ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL ; UPDATE Users SET TeamName=@TeamName WHERE ProjectID=@ProjctIDUsers";
                    string[] updatecolname_P = { "@ProjectName", "@DeadLine", "@ProjectID", "@TeamName", "@ProjctIDUsers" }; /*要帶入的參數格子*/
                    List<string> update_P = new List<string>();
                    update_P.Add(ProjectName.Trim());
                    update_P.Add(DeadLine.Trim());
                    update_P.Add(ProjectID);
                    update_P.Add(TeamName.Trim());
                    update_P.Add(ProjectID);


                    Dbtool.UpdateTable("Projects", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                }
                else
                {
                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\":\"日期不可小於當前日期\"}");
                    return;
                }
            }
            else if (Type == "Delete")
            {
                string[] updatecol_Logic = { "DeleteDate=@DeleteDate", "WhoDelete=@WhoDelete" }; /*  要更新的欄位*/
                string Where_Logic = @"ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL 
                                       ";
                string[] updatecolname_P = { "@DeleteDate", "@WhoDelete", "@ProjectID" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                update_P.Add("Manager");
                update_P.Add(ProjectID);

                Dbtool.UpdateTable("Projects", updatecol_Logic, Where_Logic, updatecolname_P, update_P);


                context.Response.Write("{\"success\":\"success\"}");
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