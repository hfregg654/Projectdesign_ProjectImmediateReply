using Newtonsoft.Json;
using ProjectImmediateReply.Utility;
using ProjectImmediateReply.ViewModels;
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


                context.Response.ContentType = "text/json";
                context.Response.Write($"{{\"FileName\":\"{"~/FileUpload/" + fileNewName}\"}}");
            }
            else
            {
                string filename = string.Empty;
                string id = string.Empty;
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    json = reader.ReadToEnd();
                }
                var Uploadfile = JsonConvert.DeserializeObject<ForUploadFile>(json);
                filename = Uploadfile.FileName;
                id = Uploadfile.WorkID.ToString();
                string[] updatecol_Logic = { "FilePath=@FilePath" }; /*  要更新的欄位*/
                string Where_Logic = "WorkID=@WorkID";
                string[] updatecolname_P = { "@FilePath", "@WorkID" }; /*要帶入的參數格子*/
                List<string> update_P = new List<string>();
                update_P.Add(filename);
                update_P.Add(id);
                dbtool.UpdateTable("Works", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
            }

            context.Response.End();
        }


        private class ForUploadFile
        {
            [JsonProperty("id")]
            public int WorkID { get; set; }
            [JsonProperty("FileName")]
            public string FileName { get; set; }
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