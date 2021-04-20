using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string PageType = string.Empty;
            string UserName = string.Empty;
            if (Session["IsLogined"] != null)
            {
                LogInfo Info = (LogInfo)Session["IsLogined"];
                PageType = Info.Privilege.ToString();
                UserName = Info.Name.ToString();
            }
            LabelUserName.Text = UserName;
            PageTool ptool = new PageTool();
            divLeftTitle.InnerHtml = ptool.PageLeft(PageType);

        }

        protected void Logoutbtn_ServerClick(object sender, EventArgs e)
        {
            LoginHelper helper = new LoginHelper();
            if (helper.HasLogIned())
            {
                helper.Logout();
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
}