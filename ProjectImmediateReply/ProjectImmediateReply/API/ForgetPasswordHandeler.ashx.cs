using Newtonsoft.Json;
using ProjectImmediateReply.Models;
using System.Collections.Generic;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// ForgetPasswordHandeler 的摘要描述
    /// </summary>
    public class ForgetPasswordHandeler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string License = context.Request.QueryString["License"]; //GET取值
            string ClassNum= context.Request.QueryString["ClassNum"];
            Utility.DBTool Forgot = new Utility.DBTool();
            string[] readcolname = { "License", "PassWord", "ClassNumber" };
            string[] Pname = { "@License", "@ClassNumber" };
            string[] P = { License,ClassNum };
            List<UserInfo> Check = new List<UserInfo>();
            if (!string.IsNullOrWhiteSpace(P[0]))
            {
                Check = Forgot.ChangeTypeUserInfo(Forgot.readTable("Users", readcolname, "WHERE License=@License AND ClassNumber=@ClassNumber AND DeleteDate IS NULL AND WhoDelete IS NULL", Pname, P)); //單筆時候的取值用法
            }

            string ShowPassword = JsonConvert.SerializeObject(Check);
            
                
            context.Response.ContentType = "text/json";
            context.Response.Write(ShowPassword);
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