using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using ProjectImmediateReply;

namespace WeekFour.Utility
{
    public class LoginHelper
    {
        private const string _sessionkey = "IsLogined";
        private const string _sessionUserName = "UserName";
        private const string _sessionUserPri = "UserPri";
        public static bool HasLogIned()
        {
            bool? val = HttpContext.Current.Session[_sessionkey] as bool?;
            if (val.HasValue && val.Value)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool TryLogIn(string Account, string Password)
        {
            if (LoginHelper.HasLogIned())
                return true;

            string[] readcol = { "Account", "PassWord", "Name", "Privilege" };
            DataTable dtuserAccount = DBTool.readTableWhere("Users", readcol, "Account=@Account", Account);
            if (dtuserAccount == null || dtuserAccount.Rows.Count == 0)
                return false;

            string dbPwd = dtuserAccount.Rows[0].Field<string>("PassWord");
            string dbName = dtuserAccount.Rows[0].Field<string>("Name");
            string dbPri = dtuserAccount.Rows[0].Field<string>("Privilege");
            bool isPasswordRight = string.Compare(dbPwd, Password, true) == 0;
            if (isPasswordRight)
            {
                HttpContext.Current.Session[_sessionUserName] = dbName;
                HttpContext.Current.Session[_sessionkey] = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Logout()
        {
            if (!LoginHelper.HasLogIned())
                return;
            HttpContext.Current.Session.Remove(_sessionkey);
            HttpContext.Current.Session.Remove(_sessionUserName);
        }

        /// <summary>
        /// 取得已登入者的資訊，若沒登入傳空字串
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserInfo()
        {
            if (!LoginHelper.HasLogIned())
            {
                return string.Empty;
            }
            return HttpContext.Current.Session[_sessionUserName] as string;

        }



    }
}