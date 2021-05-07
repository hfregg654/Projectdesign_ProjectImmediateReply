using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// GetFileHandler 的摘要描述
    /// </summary>
    public class GetFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = string.Empty;
            string error = string.Empty;
            string result = string.Empty;
            string filePath = string.Empty;
            string fileNewName = string.Empty;
            //這裡只能用<input type="file" />才能有效果,因為伺服器控制元件是HttpInputFile型別
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                //設定檔名
                fileNewName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + System.IO.Path.GetFileName(files[0].FileName);
                //儲存檔案
                files[0].SaveAs(context.Server.MapPath("~/FileUpload/" + fileNewName));
                msg = "檔案上傳成功！";
                result = "{msg:'" + msg + "',filenewname:'" + fileNewName + "'}";
            }
            else
            {
                error = "檔案上傳失敗！";
                result = "{ error:'" + error + "'}";
            }
            context.Response.Write(result);
            context.Response.End();
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