using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class ucLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.DBTool dbtool = new Utility.DBTool();
            string[] colname = { "ClassNumber" };
            DataTable classnumber = dbtool.readTable("Users", colname,"GROUP BY ClassNumber",null,null);
            List<string> classnum = new List<string>();
            foreach (DataRow item in classnumber.Rows)
            {
                if (item != null && item[0].ToString() != "")
                {
                    classnum.Add(item[0].ToString());
                }

            }
            register_class.DataSource = classnum;
            register_class.DataBind();
        }
    }
}