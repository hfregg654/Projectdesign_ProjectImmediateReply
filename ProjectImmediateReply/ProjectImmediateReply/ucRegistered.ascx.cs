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

            Utility.DBTool dbtool = new Utility.DBTool();
            string[] colname = { "ClassNumber" };
            DataTable classnumber = dbtool.readTable("Users", colname, "GROUP BY ClassNumber", null, null);
            List<string> classnum = new List<string>();
            foreach (DataRow item in classnumber.Rows) //Rows表一列
            {
                if (item != null && item[0].ToString() != "")
                {
                    classnum.Add(item[0].ToString());
                }

            }
            register_class.DataSource = classnum;
            register_class.DataBind();
        }

        //public string CheckCanUpdate()
        //{


        //    string[] CheckItem = { this.register_name.Value, this.register_number.Value, this.register_email.Value,
        //        this.register_lineid.Value, this.register_class.Value, this.register_account.Value, this.passwordregister.Value,
        //        this.passwordregistercheck.Value, this.register_key.Value };
        //    if (CheckItem[0] != string.Empty && CheckItem[1] != string.Empty && CheckItem[2] != string.Empty && CheckItem[3] != string.Empty && CheckItem[4] != string.Empty
        //        && CheckItem[5] != string.Empty && CheckItem[6] != string.Empty && CheckItem[7] != string.Empty && CheckItem[8] != string.Empty)
        //    {
        //        if (CheckItem[6] == CheckItem[7])
        //        {
        //            Utility.DBTool Create = new Utility.DBTool();
        //            string[] readcolname = { "Account" };  //輸入幾個為條件就找幾格。
        //            string[] Pname = { "@Account" };
        //            string[] P = { this.register_account.Value };
        //            //DataTable Check = Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P) //如上面寫一個字串，只會找一格，已經在核對帳號找出有的那一格，P表網頁輸出欄位，以帳號為條件搜尋，如為空則傳空。
        //            List<UserInfo> Check_Acc = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P));
        //            string[] readcolname2 = { "Account", "License" };
        //            string[] Pname2 = { "@Account", "@License" };
        //            string[] P2 = { this.register_account.Value, this.register_key.Value };
        //            List<UserInfo> Check_Acc_Lic = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname2, "WHERE License=@License AND Account=@Account", Pname2, P2));
        //            if (Check_Acc_Lic.Count != 0) //資料表型式的變數都是存在，非空值。
        //            {
        //                return "授權碼已使用";
        //            }
        //            else if (Check_Acc.Count != 0)
        //            {
        //                return "帳號已存在，請使用其他帳號";
        //            }
        //            else
        //            {
        //                return "註冊成功，請至註冊信箱收取驗證信";
        //            }
        //        }
        //        else
        //        {
        //            return "密碼確認輸入錯誤";
        //        }
        //    }
        //    else
        //    {
        //        return "欄位不可為空";
        //    }
        //}


        //protected void Btn_Create(object sender, EventArgs e)
        //{
        //    string[] CheckItem = { this.register_name.Value, this.register_number.Value, this.register_email.Value,
        //        this.register_lineid.Value, this.register_class.Value, this.register_account.Value, this.passwordregister.Value,
        //        this.passwordregistercheck.Value, this.register_key.Value };
        //    if (CheckItem[0] != string.Empty && CheckItem[1] != string.Empty && CheckItem[2] != string.Empty && CheckItem[3] != string.Empty && CheckItem[4] != string.Empty
        //        && CheckItem[5] != string.Empty && CheckItem[6] != string.Empty && CheckItem[7] != string.Empty && CheckItem[8] != string.Empty)
        //    {
        //        if (CheckItem[6] == CheckItem[7])
        //        {
        //            Utility.DBTool Create = new Utility.DBTool();
        //            string[] readcolname = { "Account" };  //輸入幾個為條件就找幾格。
        //            string[] Pname = { "@Account" };
        //            string[] P = { this.register_account.Value };
        //            //DataTable Check = Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P) //如上面寫一個字串，只會找一格，已經在核對帳號找出有的那一格，P表網頁輸出欄位，以帳號為條件搜尋，如為空則傳空。
        //            List<UserInfo> Check = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname, "WHERE Account=@Account", Pname, P));
        //            string[] readcolname2 = { "Account", "License" };
        //            string[] Pname2 = { "@Account", "@License" };
        //            string[] P2 = { this.register_account.Value, this.register_key.Value };
        //            List<UserInfo> Check2 = Create.ChangeTypeUserInfo(Create.readTable("Users", readcolname2, "WHERE License=@License AND Account=@Account", Pname2, P2));
        //            if (Check2.Count != 0) //資料表型式的變數都是存在，非空值。
        //            {
        //                Message.Text = "授權碼已使用";
        //            }
        //            else if (Check.Count != 0)
        //            {
        //                Message.Text = "帳號已存在，請使用其他帳號";
        //            }
        //            else
        //            {
        //                string[] updatecol_Logic = { "Name=@Name", "Phone=@Phone", "Mail=@Mail", "LineID=@LineID", "ClassNumber=@ClassNumber", "Account=@Account", "PassWord=@PassWord", "License=@License" };
        //                string Where_Logic = "License=@License";
        //                string[] updatecolname_P = { "@Name", "@Phone", "@Mail", "@LineID", "@ClassNumber", "@Account", "@PassWord", "@License" };
        //                List<string> update_P = new List<string>();
        //                update_P.Add(register_name.Value);
        //                update_P.Add(register_number.Value);
        //                update_P.Add(register_email.Value);
        //                update_P.Add(register_lineid.Value);
        //                update_P.Add(register_class.Value);
        //                update_P.Add(register_account.Value);
        //                update_P.Add(passwordregister.Value);
        //                //update_P.Add(passwordregistercheck.Value);
        //                update_P.Add(register_key.Value);
        //                Create.UpdateTable("Users", updatecol_Logic, Where_Logic, updatecolname_P, update_P);
        //                Message.Text = "註冊成功，請至註冊信箱收取驗證信";
        //            }
        //        }
        //        else
        //        {
        //            Message.Text = "密碼確認輸入錯誤";
        //        }
        //    }
        //    else
        //    {
        //        Message.Text = "欄位不可為空";
        //    }
        //}
    }
}