using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// RegisteredHandler 的摘要描述
    /// </summary>
    public class RegisteredHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
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