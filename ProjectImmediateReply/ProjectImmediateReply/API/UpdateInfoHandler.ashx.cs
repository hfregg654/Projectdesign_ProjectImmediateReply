﻿using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// UpdateInfoHandler 的摘要描述
    /// </summary>
    public class UpdateInfoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DBTool Dbtool = new DBTool();
            string C1name = string.Empty;
            string C1phone = string.Empty;
            string C1email = string.Empty;
            string C1lineid = string.Empty;
            string C1password = string.Empty;
            string C1newpassword = string.Empty;
            string C1newpasswordconfirm = string.Empty;
            string license = string.Empty;
            string json = string.Empty;
            //先處理接到的JSON放進字串中
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(json))
            {
                string[] splitjson = json.Split('"');
                C1name = splitjson[3];//用"切開則參數會在3,7,11,15...的位置
                C1phone = splitjson[7];
                C1email = splitjson[11];
                C1lineid = splitjson[15];
                C1password = splitjson[19];
                C1newpassword = splitjson[23];
                C1newpasswordconfirm = splitjson[27];
                license = splitjson[31];
            }
            if (!string.IsNullOrWhiteSpace(C1password) && !string.IsNullOrWhiteSpace(C1newpassword) && !string.IsNullOrWhiteSpace(C1newpasswordconfirm))
            {
                string[] colname = { "Name" };
                string[] colnamep = { "@License", "@PassWord" };
                string[] p = { license, C1password };
                DataTable check_pwd = Dbtool.readTable("users", colname, "WHERE License=@License AND PassWord=@PassWord", colnamep, p);
                if (check_pwd.Rows.Count != 0)
                {
                    if (C1newpassword== C1newpasswordconfirm)
                    {
                        string[] updatecol_Logic = { "Name=@Name", "Phone=@Phone", "Mail=@Mail", "LineID=@LineID", "PassWord=@NEWPassWord", };
                        string Where_Logic = "License=@License AND PassWord=@PassWord";
                        string[] updatecolname_P = { "@Name", "@Phone", "@Mail", "@LineID", "@NEWPassWord", "@License", "@PassWord" };
                        List<string> update_P = new List<string>();
                        update_P.Add(C1name);
                        update_P.Add(C1phone);
                        update_P.Add(C1email);
                        update_P.Add(C1lineid);
                        update_P.Add(C1newpassword);
                        update_P.Add(license);
                        update_P.Add(C1password);
                        Dbtool.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                        context.Response.Write("[{\"success\":\"success\"}]");
                    }
                    else
                    {
                        context.Response.ContentType = "text/json";
                        context.Response.Write("[{\"success\":\"wrong\"}]");
                    }
                }
                else
                {
                    context.Response.ContentType = "text/json";
                    context.Response.Write("[{\"success\":\"wrong\"}]");
                }
            }
            else
            {
                string[] colname = { "PassWord" };
                string[] colnamep = { "@License", "@Name" };
                string[] p = { license, C1name };
                DataTable check_acc = Dbtool.readTable("users", colname, "WHERE License=@License AND Name=@Name", colnamep, p);
                if (check_acc.Rows.Count!=0)
                {
                    string[] updatecol_Logic = { "Name=@Name", "Phone=@Phone", "Mail=@Mail", "LineID=@LineID" };
                    string Where_Logic = "License=@License AND PassWord=@PassWord";
                    string[] updatecolname_P = { "@Name", "@Phone", "@Mail", "@LineID", "@License", "@PassWord" };
                    List<string> update_P = new List<string>();
                    update_P.Add(C1name);
                    update_P.Add(C1phone);
                    update_P.Add(C1email);
                    update_P.Add(C1lineid);
                    update_P.Add(license);
                    update_P.Add(check_acc.Rows[0]["PassWord"].ToString());
                    Dbtool.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                    context.Response.ContentType = "text/json";
                    context.Response.Write("[{\"success\":\"success\"}]");
                }
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