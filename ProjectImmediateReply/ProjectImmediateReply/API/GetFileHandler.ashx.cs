using Newtonsoft.Json;
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
    /// GetFileHandler 的摘要描述
    /// </summary>
    public class GetFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DBTool dbtool = new DBTool();
            context.Response.ContentType = "text/plain";
            string filePath;
            string fileNewName;
            string json = string.Empty;

            //這裡只能用<input type="file" />才能有效果,因為伺服器控制元件是HttpInputFile型別
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                //設定檔名
                fileNewName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + System.IO.Path.GetFileName(files[0].FileName);
                //設定路徑
                filePath = "/FileUpload/" + fileNewName;

                string ActuallyPath = context.Server.MapPath(filePath);
                //儲存檔案
                files[0].SaveAs(ActuallyPath);


                context.Response.ContentType = "text/json";
                context.Response.Write($"{{\"FileName\":\"{filePath}\"}}");
            }
            else
            {
                string filename;
                string id;
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    json = reader.ReadToEnd();
                }
                var Uploadfile = JsonConvert.DeserializeObject<ForUploadFile>(json);
                filename = Uploadfile.FileName;
                id = Uploadfile.WorkID.ToString();

                string[] workscolname = { "FilePath", "Complete" };
                string[] workscolnamep = { "@WorkID" };
                string[] worksp = { id };
                string workslogic = @"
                                WHERE WorkID=@WorkID AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
                DataTable worksdata = dbtool.readTable("Works", workscolname, workslogic, workscolnamep, worksp);//查此工作的檔案路徑

                string workfilepath = worksdata.Rows[0]["FilePath"].ToString();


                if (workfilepath.Contains("FileUpload"))
                {
                    string paths = context.Server.MapPath(workfilepath);
                    File.Delete(paths);
                }

                if (worksdata.Rows[0]["Complete"].ToString().ToUpper() != "TRUE")
                {
                    string[] updatecol_Logic = { "FilePath=@FilePath", "UpdateTime=@UpdateTime" }; /*  要更新的欄位*/
                    string Where_Logic = "WorkID=@WorkID";
                    string[] updatecolname_P = { "@FilePath", "@UpdateTime", "@WorkID" }; /*要帶入的參數格子*/
                    List<string> update_P = new List<string>();
                    update_P.Add(filename);
                    update_P.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                    update_P.Add(id);
                    dbtool.UpdateTable("Works", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                }
                else
                {
                    string[] updatecol_Logic = { "FilePath=@FilePath" }; /*  要更新的欄位*/
                    string Where_Logic = "WorkID=@WorkID";
                    string[] updatecolname_P = { "@FilePath", "@WorkID" }; /*要帶入的參數格子*/
                    List<string> update_P = new List<string>();
                    update_P.Add(filename);
                    update_P.Add(id);
                    dbtool.UpdateTable("Works", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                }


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