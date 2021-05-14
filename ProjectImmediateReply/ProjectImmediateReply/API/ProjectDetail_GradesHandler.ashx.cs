using Newtonsoft.Json;
using ProjectImmediateReply.Models;
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
    /// ProjectDetail_GradesHandler 的摘要描述
    /// </summary>
    public class ProjectDetail_GradesHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DBTool DbTool = new DBTool();
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