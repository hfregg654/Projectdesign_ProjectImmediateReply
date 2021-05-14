using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class ProjectDetail_Grades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
            {
                LogInfo Info = (LogInfo)Session["IsLogined"];
            }
        }
    }
}