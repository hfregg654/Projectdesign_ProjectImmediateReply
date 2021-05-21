using System;
using System.Collections.Generic;
//System.Collections.Generic 包含定義泛型集合的介面和類別，其允許使用者建立強型別集合，這些集合提供比起非泛型強型別集合更佳的型別安全和效能。
namespace ProjectImmediateReply
{
    //繼承父類別 System.Web.UI.UserControl 讓使用者控制項可以使用
    public partial class ucRegistered : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)  //protected表此方法只能於該頁面(檔案)使用 可繼承，後面()內語法為使用後代表為該物件，拿掉後會出錯 EventArgs e表事件資訊。
        {
            Utility.PageTool ptool = new Utility.PageTool();
            List<string> classnum = ptool.GetClassNumber();
            register_class.DataSource = classnum; //<select class="icons" runat="server" id="register_class">
            register_class.DataBind(); 
        }
    }
}