using System;
using ProjectImmediateReply.Utility;
using ProjectImmediateReply.Models;

namespace ProjectImmediateReply
{
    public partial class CreateWorks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginHelper logtool = new LoginHelper();
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");
            LogInfo Info = (LogInfo)Session["IsLogined"];
            if (Info.Privilege != "Leader")
            {
                logtool.Logout();
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
}