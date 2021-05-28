using System;
using System.Collections.Generic;
//System.Collections.Generic 包含定義泛型集合的介面和類別，其允許使用者建立強型別集合，這些集合提供比起非泛型強型別集合更佳的型別安全和效能。
namespace ProjectImmediateReply
{
    //繼承父類別 System.Web.UI.UserControl 讓使用者控制項可以使用
    public partial class ucRegistered : System.Web.UI.UserControl
    {
        //protected表此方法只能於該頁面(檔案)使用 可繼承，後面()內語法為使用後代表為該物件，拿掉後會出錯 EventArgs e表事件資訊。
        protected void Page_Load(object sender, EventArgs e)  
        {
            //引用PageTool方法內的GetClassNumber()取得資料庫內ClassNumber的變數放入到下拉式清單內，並設定給伺服器控制項forgetpwd_class.DataSource
            //之後作資料繫結至控制項顯示出當前資料庫內之ClassNumber
            Utility.PageTool ptool = new Utility.PageTool();
            List<string> classnum = ptool.GetClassNumber();
            register_class.DataSource = classnum; //<select class="icons" runat="server" id="register_class">
            register_class.DataBind(); 
        }
    }
}