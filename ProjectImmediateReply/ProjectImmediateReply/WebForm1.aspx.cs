using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            foreach (string item in Utility.RandomTool.CreateLicence(10, "Manager").ToArray())
            {
                Response.Write($"{item}<br/>");
            }
            DateTime end = DateTime.Now;
            Response.Write((end-start).ToString());
        }
    }
}