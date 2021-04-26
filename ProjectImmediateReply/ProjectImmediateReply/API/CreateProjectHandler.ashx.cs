using ProjectImmediateReply.Log;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
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
            //取得傳過來的資料
            string ClassNumber = context.Request.Form["ClassNumber"];
            string ProjectName = context.Request.Form["ProjectName"];
            string DeadLine = context.Request.Form["DeadLine"];
            //先檢查傳過來的值有沒有問題並先定義回傳的訊息
            string success = "";
            if (string.IsNullOrWhiteSpace(ClassNumber) || string.IsNullOrWhiteSpace(ProjectName) || string.IsNullOrWhiteSpace(DeadLine || Privilege != "Manager")
            {

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