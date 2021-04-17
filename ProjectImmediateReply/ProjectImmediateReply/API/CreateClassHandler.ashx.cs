using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.API
{
    /// <summary>
    /// CreateClassHandler 的摘要描述
    /// </summary>
    public class CreateClassHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string ClassNumber = context.Request.Form["ClassNumber"];
            string PeopleNum = context.Request.Form["PeopleNum"];
            string Privilege = context.Request.Form["Privilege"];
            RandomTool Rtool = new RandomTool();
            List<string> License = new List<string>();
            int temp;
            if (int.TryParse(PeopleNum, out temp))
            {
                License = Rtool.CreateLicence(temp, Privilege);
            }


            foreach (string item in License)
            {
                string newPrivilege = "User";
                string CreateDate = DateTime.Now.ToString("yyyy/MM/dd");
                string WhoCreate = "Manager";

                DBTool Dbtool = new DBTool();
                string[] colname = { "ClassNumber","License", "Privilege", "CreateDate", "WhoCreate" };
                string[] colnamep = { "@ClassNumber", "@License", "@Privilege", "@CreateDate", "@WhoCreate" };
                List<string> p = new List<string>();
                p.Add(ClassNumber);
                p.Add(item);
                p.Add(newPrivilege);
                p.Add(CreateDate);
                p.Add(WhoCreate);
                Dbtool.InsertTable("Users",colname,colnamep,p);
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