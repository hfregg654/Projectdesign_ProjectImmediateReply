using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;

namespace ProjectImmediateReply
{
    public partial class ucCreateProject : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.DBTool dbTool = new Utility.DBTool();
            string[] readcolname = { "ClassNumber" };
            DataTable classnumber = dbTool.readTable("Users", readcolname, "GROUP BY ClassNumber", null, null);
            List<string> classnum = new List<string>();
            foreach (DataRow item in classnumber.Rows)
            {
                if (string.IsNullOrWhiteSpace(item[0].ToString()))
                {
                    classnum.Add(item[0].ToString());
                }
                
            }
        }
    }
}