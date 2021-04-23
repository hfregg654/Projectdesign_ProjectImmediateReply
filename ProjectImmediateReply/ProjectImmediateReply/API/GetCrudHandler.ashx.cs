using Newtonsoft.Json;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
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
            DBTool Dbtool = new DBTool();
            string ClassNumber = "";
            string[] colname = { "ClassNumber","License","Name" };
            string[] colnamep = { "@ClassNumber" };
            string[] p = { "100-1" };
            List<UserInfo> data = Dbtool.ChangeTypeUserInfo(Dbtool.readTable("Users", colname, "WHERE ClassNumber=@ClassNumber", colnamep, p));

            string ShowTable = JsonConvert.SerializeObject(data);


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