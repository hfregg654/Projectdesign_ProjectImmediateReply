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
            List<string> people = new List<string>()
            {
                "陳彥瑋","鍾學武","饒文傑","林可偉","歐紀安","翁秉寬","郭家榮","胡冠傑","徐丞邑",
                "葉韋豪","張瑋芝","吳遠哲","連秋堂","AAA","BBB","CCC","DDD","EEE","FFF"
            };
            List<string> project = new List<string>()
            {
                "財務系統","無人機管理系統","專案及時回覆系統","選課系統"
            };
            DateTime start = DateTime.Now;
            Utility.RandomTool tool = new Utility.RandomTool();
            
            foreach (string item in tool.RandomAssign(people, project ,"Manager").ToArray())
            {
                Response.Write($"{item}<br/>");
            }
            DateTime end = DateTime.Now;
            Response.Write((end - start).ToString());
        }
    }
}