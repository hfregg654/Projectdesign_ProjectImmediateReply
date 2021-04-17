using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            Utility.LoginHelper tool = new Utility.LoginHelper();
            if (tool.TryLogIn(username.Value, passwordIndex.Value))
            {
                Response.Redirect($"~/Index.aspx");
            }
        }
    }
}