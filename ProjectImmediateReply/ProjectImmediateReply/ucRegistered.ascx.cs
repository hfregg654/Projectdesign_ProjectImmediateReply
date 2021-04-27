using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectImmediateReply.Models;
using ProjectImmediateReply.Utility;

namespace ProjectImmediateReply
{
    public partial class ucLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)  //protected表此方法只能於該頁面(檔案)使用，後面()內語法為使用後代表為該物件，拿掉後會出錯。
        {
            Utility.PageTool ptool = new Utility.PageTool();
            List<string> classnum = ptool.GetClassNumber();
            register_class.DataSource = classnum;
            register_class.DataBind();
        }
    }
}