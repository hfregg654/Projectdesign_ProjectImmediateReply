using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace ProjectImmediateReply.Utility
{
    public class LoginHelper
    {   //定義三個常數分別儲存"登入狀態","登入者名稱"及"登入者權限"的Session名稱
        private const string _sessionislogin = "IsLogined";
        private const string _sessionUserName = "UserName";
        private const string _sessionUserPri = "UserPri";
        /// <summary>
        /// 確認登入狀態,回傳值為true or false
        /// </summary>
        /// <returns></returns>
        public static bool HasLogIned()
        {
            //定義布林值取得登入狀態的Session
            bool? val = HttpContext.Current.Session[_sessionislogin] as bool?;
            //檢查變數內有值且為true回傳true,否則回傳false
            if (val.HasValue && val.Value)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 登入的帳號密碼驗證,回傳值為true or false
        /// </summary>
        /// <param name="Account">使用者帳號</param>
        /// <param name="Password">使用者密碼</param>
        /// <returns></returns>
        public static bool TryLogIn(string Account, string Password)
        {
            //若已是登入狀態則回傳true
            if (LoginHelper.HasLogIned())
                return true;
            //從資料庫裡撈出符合帳號的資料,若沒有則回傳FALSE
            string[] readcol = { "Account", "PassWord", "Name", "Privilege" };
            DataTable dtuserAccount = DBTool.readTableWhere("Users", readcol, "Account=@Account", Account);
            if (dtuserAccount == null || dtuserAccount.Rows.Count == 0)
                return false;
            //將撈到的資料放進變數中存放
            string dbPwd = dtuserAccount.Rows[0].Field<string>("PassWord");
            string dbName = dtuserAccount.Rows[0].Field<string>("Name");
            string dbPri = dtuserAccount.Rows[0].Field<string>("Privilege");
            //檢查密碼是否正確
            bool isPasswordRight = string.Compare(dbPwd, Password, true) == 0;
            //若密碼正確將資料放進Session並回傳true,否則回傳false
            if (isPasswordRight)
            {
                HttpContext.Current.Session[_sessionislogin] = true;
                HttpContext.Current.Session[_sessionUserName] = dbName;
                HttpContext.Current.Session[_sessionUserPri] = dbPri;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 將使用者登出並刪除其Session
        /// </summary>
        public static void Logout()
        {
            //若為非登入狀態直接回傳
            if (!LoginHelper.HasLogIned())
                return;
            HttpContext.Current.Session.Remove(_sessionislogin);
            HttpContext.Current.Session.Remove(_sessionUserName);
            HttpContext.Current.Session.Remove(_sessionUserPri);
        }

        /// <summary>
        /// 取得已登入者的資訊，若沒登入傳空字串
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserInfo()
        {
            if (!LoginHelper.HasLogIned())
                return string.Empty;
            return HttpContext.Current.Session[_sessionUserName] as string;

        }



    }
}