using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// CreateProjectHandler 的摘要描述
    /// </summary>
    public class CreateProjectHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ClassNumber = context.Request.Form["ClassNumber"];
            string ProjectName = context.Request.Form["ProjectName"];
            string DeadLine = context.Request.Form["DeadLine"];
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