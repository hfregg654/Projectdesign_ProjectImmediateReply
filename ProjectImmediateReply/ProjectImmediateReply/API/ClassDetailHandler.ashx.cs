using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ProjectImmediateReply.Utility;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// ClassDetailHandler 的摘要描述
    /// </summary>
    public class ClassDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //準備DB工具以及宣告頁面檢查,班級檢查,接到的JSON字串
            DBTool Dbtool = new DBTool();
            string Type = string.Empty;
            string UserID = string.Empty;
            string json = string.Empty;

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
                UserID = splititem[7];  //有時候會是ClassNumber
            }
            //若沒有接到頁面檢查的參數則直接回傳
            if (string.IsNullOrWhiteSpace(Type))
                return;

            if (Type == "Delete")
            {
                string[] updatecol_Logic = { "DeleteDate=@DeleteDate", "WhoDelete=@WhoDelete" }; /*  要更新的欄位*/
                string Where_Logic = "UserID=@UserID AND DeleteDate IS NULL AND WhoDelete IS NULL";
                string[] updatecolname_P = { "@DeleteDate", "@WhoDelete", "@UserID" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                update_P.Add("Manager");
                update_P.Add(UserID);

                Dbtool.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                context.Response.Write("{\"success\":\"success\"}");
            }
            else if (Type == "ALLDelete")
            {
                string[] updatecol_Logic = { "DeleteDate=@DeleteDate", "WhoDelete=@WhoDelete" }; /*  要更新的欄位*/
                string Where_Logic = "ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL";
                string[] updatecolname_P = { "@DeleteDate", "@WhoDelete", "@ClassNumber" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                update_P.Add("Manager");
                update_P.Add(UserID); //其實是ClassNumber

                Dbtool.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
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