using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class ucCreateClass : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLogined"] !=null)
            {
                LogInfo Info = (LogInfo)Session["IsLogined"];
                HiddenFieldSessionPri.Value = Info.Privilege.ToString();
            }
        }
    }
}