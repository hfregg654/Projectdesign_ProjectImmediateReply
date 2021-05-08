using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProjectImmediateReply.Log;


namespace ProjectImmediateReply.Utility
{
    public class DBModelTool
    {
        /// <summary>
        /// 判斷接過來的readtablename來決定那個表的order方式
        /// </summary>
        /// <param name="readtablename"></param>
        /// <returns></returns>
        private string DBOrderName(string readtablename)
        {
            string DBordername;
            switch (readtablename)
            {
                case "Users":
                    DBordername = "UserID";
                    break;
                case "Projects":
                    DBordername = "ProjectID";
                    break;
                case "Works":
                    DBordername = "WorkID";
                    break;
                case "Grades":
                    DBordername = "GradeID";
                    break;
                default:
                    {
                        txtLog logtool = new txtLog();
                        logtool.WriteLog("請輸入正確資料表名稱");
                        return null;
                    }
            }
            return DBordername;
        }

        //public DataTable DBreadTable(string readtablename, string[] readcolname, string Logic, string[] Pname, string[] P)
        //{
        //    using (var DBreadTableContext = new DBModels.ProjectImmediateReplyDataContext())
        //    {
        //        //將接過來的目標欄位名稱陣列用「,」連接成一個字串
        //        string readcoladd = string.Join(",", readcolname);
        //        //判斷接過來的readtablename來決定那個表的order方式
        //        string dbordername = DBOrderName(readtablename);
        //        //語法參數化"SELECT 欄位名稱 FROM 資料表名稱 條件"
                
        //        string queryString = from readcoladd in readtablename select 
        //        try
        //        {
        //            DataTable dt = new DataTable();
        //            dt.Load();
        //        }
        //        //拋錯誤訊息
        //        catch (Exception ex)
        //        {
        //            txtLog logtool = new txtLog();
        //            logtool.WriteLog(ex.ToString());
        //            throw;
        //        }
        //    }
        //}
    }
}