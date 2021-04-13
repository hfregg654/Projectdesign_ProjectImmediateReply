using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class ucCRUD : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }
        protected void Page_PreRander(object sender, EventArgs e)
        {



        }



        public int GetDBLength()
        {
            Utility.DBTool tool = new Utility.DBTool();
            string[] colname = { "UserID" };
            DataTable dt = tool.readTable("Users", colname);
            return dt.Rows.Count * 5;
        }

        public string GetSomething()
        {
            Utility.DBTool tool = new Utility.DBTool();
            string[] colname = { "Account", "Privilege", "ClassNumber", "Name", "License" };
            DataTable dt = tool.readTable("Users", colname);

            List<string> ar = new List<string>();

            foreach (DataRow item in dt.Rows)
            {
                ar.Add(item["Account"].ToString());
                ar.Add(item["Privilege"].ToString());
                ar.Add(item["ClassNumber"].ToString());
                ar.Add(item["Name"].ToString());
                ar.Add(item["License"].ToString());
            }

            string[] lar = ar.ToArray();
            string slar = string.Join(",", lar);

            return slar;
        }


    }
}