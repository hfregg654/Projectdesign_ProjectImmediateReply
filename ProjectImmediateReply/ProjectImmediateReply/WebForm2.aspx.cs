using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.DBTool dbtool = new Utility.DBTool();
            string[] colname = { "ClassNumber" };
            DataTable classnumber = dbtool.readTable("Users", colname, "GROUP BY ClassNumber", null, null);
            List<string> classnum = new List<string>();
            foreach (DataRow item in classnumber.Rows)
            {
                if (item != null && item[0].ToString() != "")
                {
                    Response.Write(item[0].ToString() +"<br />");
                }

            }
            //foreach (DataRow item in classnumber.Rows)
            //{
            //    Response.Write(item.ToString()+"<br/>");
            //}

        }
    }
}