using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using ProjectImmediateReply.Utility;  //引用專案下的某樣工具方法

namespace ProjectImmediateReply
{
    public partial class Index : System.Web.UI.Page
    {
        //命名變數 登入後首頁
        string _goToUrl = "~/Index.aspx"; 
        protected void Page_Load(object sender, EventArgs e)
        {
            //_goToUrl = Request.RawUrl; //轉回本頁
            //如不用static 需用new建立此實體物件 命名helper變數 使用LoginHelper方法儲存Session資料
            LoginHelper helper = new LoginHelper(); 
            // 檢查使用者資訊，如果已經有session 且符合資料庫內使用者資訊則直接轉至首頁
            if (helper.HasLogIned())
            {
                //導向至指定網址(首頁)
                Response.Redirect(this._goToUrl);
            }
            // 如果是第一次登入頁面則進入此判斷
            if (!IsPostBack)
            {
                // 判斷是否註冊成功  如果在找QueryString找到到license及班級 且皆不為空的狀況下 進入此判斷式
                if (Request.QueryString["license"] != null && Request.QueryString["classnumber"] != null)
                {
                    //建立DBTool變數容器 讀取Users表中的ClassNumber、License、Account欄位
                    //以Pname的ClassNumber及License為搜尋參數 並於P放入驗證信傳來的classnumber及license變數做為比對
                    //在條件有找到相同ClassNumber及License且權限尚為"Visitor" 帳號不為空的狀況下將資料給變數Check
                    DBTool Create = new DBTool();
                    string[] readcolname = { "ClassNumber", "License","Account" };
                    string[] Pname = { "@ClassNumber", "@License" };
                    string[] P = { Request.QueryString["classnumber"], Request.QueryString["license"] };
                    List<UserInfo> Check = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE License=@License AND ClassNumber=@ClassNumber AND Privilege='Visitor' AND Account!='NULL'", Pname, P));
                    // 更新權限  檢查後如果取得的資料數Check.Count不為0 則進入此條件 
                    if (Check.Count != 0)
                    {
                        //更新欄位為授權碼、條件是找到相同授權碼的資料列、因更新為List需建立List變數，將權限字串"User"及授權碼變數更新至資料庫
                        //Response.Write 如果成功更新則輸出 "授權碼XXX之帳號XXX註冊完成，請輸入帳號密碼以登入"訊息之彈跳視窗 並跳轉至登入頁面
                        string[] updatecol_Logic = { "Privilege=@Privilege" };
                        string Where_Logic = "License=@License";
                        string[] updatecolname_P = { "@Privilege", "@License" };
                        List<string> update_P = new List<string>();
                        update_P.Add("User");
                        update_P.Add(Request.QueryString["license"]);
                        Create.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
                        Response.Write($"<script>alert('授權碼{Check[0].License}之帳號{Check[0].Account}註冊完成，請輸入帳號密碼以登入');</script>");
                    }
                    //其他錯誤訊息
                    else
                    {
                        Response.Write($"<script>alert('授權碼{Request.QueryString["license"]}之帳號註冊失敗，有可能已經註冊或尚未註冊亦或是連結錯誤');</script>");
                    }
                }
            }
        }
        // 登入按鈕事件 點擊登入按鈕後觸發
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //擷取使用者的帳號this.username.Value與密碼this.passwordIndex.Value 放入acc及pwd變數
            string acc = this.username.Value;
            string pwd = this.passwordIndex.Value;
            //如不用static 需用new建立此實體物件 命名helper變數 使用LoginHelper方法儲存Session資料
            LoginHelper helper = new LoginHelper();
            //判斷登入資訊是否為真 比較後如果為真 真為True
            bool isSuccess = helper.TryLogIn(acc, pwd);
            //如果是True 則跳轉到登入首頁
            if (isSuccess)
            {
                //跳轉到首頁
                Response.Redirect(this._goToUrl);
            }
            else
            {   //如果為0 則輸出帳號或密碼輸入錯誤，請重新輸入訊息
                this.ltMessage.Text = "帳號或密碼輸入錯誤，請重新輸入";
            }
        }
    }
}