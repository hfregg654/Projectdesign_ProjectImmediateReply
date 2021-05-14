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
            //接值的處理方法 Start 前端有寫到API/GetCrudHandler.ashx會接到此處
            string json = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            //接值的處理方法 End
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