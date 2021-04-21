using Newtonsoft.Json;
using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string License = context.Request.QueryString["License"];
            Utility.DBTool Forgot = new Utility.DBTool();
            string[] readcolname = { "License", "PassWord" };
            string[] Pname = { "@License" };
            string[] P = { License };
            List<UserInfo> Check = new List<UserInfo>();
            if (P[0] != null && P[0] != string.Empty && P[0] != "null")
            {
                Check = Forgot.ChangeTypeUserInfo(Forgot.readTable("Users", readcolname, "WHERE License=@License", Pname, P)); //單筆時候的取值用法
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