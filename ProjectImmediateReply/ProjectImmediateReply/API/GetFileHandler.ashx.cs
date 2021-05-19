using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.IO;
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
            DBTool dbtool = new DBTool();
            context.Response.ContentType = "text/plain";
            string msg = string.Empty;
            string error = string.Empty;
            string result = string.Empty;
            string filePath = string.Empty;
            string fileNewName = string.Empty;
            string json = string.Empty;
            //這裡只能用<input type="file" />才能有效果,因為伺服器控制元件是HttpInputFile型別
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                //設定檔名
                fileNewName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + System.IO.Path.GetFileName(files[0].FileName);
                //儲存檔案
                files[0].SaveAs(context.Server.MapPath("~/FileUpload/" + fileNewName));
                string[] updatecol_Logic = { "FilePath=@FilePath" }; /*  要更新的欄位*/
                string Where_Logic = "WorkID=@WorkID";
                string[] updatecolname_P = { "@FilePath", "@WorkID" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add("~/FileUpload/" + fileNewName);
                update_P.Add("1");
                dbtool.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);

                msg = "檔案上傳成功！";
                result = "{msg:'" + msg + "',filenewname:'" + fileNewName + "'}";
            }
            else
            {
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    json = reader.ReadToEnd();
                }
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