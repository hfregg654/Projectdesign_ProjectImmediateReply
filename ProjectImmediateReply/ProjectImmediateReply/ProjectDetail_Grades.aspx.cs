using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectImmediateReply.Utility;

namespace ProjectImmediateReply
{
    public partial class ProjectDetail_Grades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginHelper logtool = new LoginHelper();
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");
            LogInfo Info = (LogInfo)Session["IsLogined"];
            if (Info.Privilege != "Grades")
            {
                logtool.Logout();
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
}