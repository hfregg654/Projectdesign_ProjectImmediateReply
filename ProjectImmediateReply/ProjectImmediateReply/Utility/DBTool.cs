using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ProjectImmediateReply.Utility
{
    public class DBTool
    {
        /// <summary>
        /// 讀取資料庫目標資料表的目標欄位資料
        /// SELECT 欄位名稱 FROM 資料表名稱 ORDER BY 排序目標
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="readcolname">目標欄位名稱的陣列</param>
        /// <returns></returns>
        public static DataTable readTable(string readtablename, string[] readcolname)
        {
            //資料庫連結字串
            string connectionString =
               "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectImmediateReply; Integrated Security=true";
            //將接過來的目標欄位名稱陣列用「,」連接成一個字串
            string readcoladd = string.Join(",", readcolname);
            //判斷接過來的readtablename來決定那個表的order方式
            string ordername;
            switch (readtablename)
            {
                case "Users":
                    ordername = "UserID";
                    break;
                case "Projects":
                    ordername = "ProjectID";
                    break;
                case "Works":
                    ordername = "WorkID";
                    break;
                case "Grades":
                    ordername = "UserID";
                    break;
                default:
                    {
                        HttpContext.Current.Response.Write("請輸入正確資料表名稱");
                        return null;
                    }
            }
            //SQL語法參數化"SELECT 欄位名稱 FROM 資料表名稱 ORDER BY 排序目標"
            string queryString =
                $@" SELECT {readcoladd} FROM {readtablename}
                    ORDER BY {ordername} ;";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;

                }
                //拋錯誤訊息
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                    return null;
                }
            }
        }
        /// <summary>
        /// 讀取資料庫目標資料表的目標欄位中符合條件的資料
        /// SELECT 欄位名稱 FROM 資料表名稱 WHERE Where條件(欄位名稱=@欄位名稱) ORDER BY 排序目標
        /// readcolname第一個值須為Where的目標欄位名稱
        /// 只能下一種WHERE條件
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="readcolname">目標欄位名稱的陣列</param>
        /// <param name="Where_Logic">Where條件</param>
        /// <param name="Where_P">條件的參數值</param>
        /// <returns></returns>
        public static DataTable readTableWhere(string readtablename, string[] readcolname, string Where_Logic, string Where_P)
        {
            string connectionString =
               "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectImmediateReply; Integrated Security=true";
            //將接過來的目標欄位名稱陣列用「,」連接成一個字串
            string readcoladd = string.Join(",", readcolname);
            //判斷接過來的readtablename來決定那個表的排序方式
            string ordername;
            switch (readtablename)
            {
                case "Users":
                    ordername = "UserID";
                    break;
                case "Projects":
                    ordername = "ProjectID";
                    break;
                case "Works":
                    ordername = "WorkID";
                    break;
                case "Grades":
                    ordername = "UserID";
                    break;
                default:
                    {
                        HttpContext.Current.Response.Write("請輸入正確資料表名稱");
                        return null;
                    }
            }
            //SQL語法參數化"SELECT 欄位名稱 FROM 資料表名稱 WHERE Where條件(欄位名稱=@欄位名稱) ORDER BY 排序目標"
            string queryString =
                $@" SELECT {readcoladd} FROM {readtablename}
                    WHERE  {Where_Logic}
                    ORDER BY {ordername} ;";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //傳入參數至Where條件的@目標欄位
                command.Parameters.AddWithValue($"@{readcolname[0]}", Where_P);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                    return null;
                }
            }
        }
        /// <summary>
        /// 往資料庫中插入新資料列
        /// INSERT INTO 資料表名稱 (欄位名稱) VALUES (@欄位名稱)
        /// 傳入的@欄位和參數的順序必須相同
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="insertcolname">目標欄位名稱的陣列</param>
        /// <param name="insertcolname_P">目標欄位名稱帶有@的陣列</param>
        /// <param name="insert_P">需給予@欄位之參數值的集合</param>
        public static void InsertTable(string inserttablename, string[] insertcolname, string[] insertcolname_P, List<string> insert_P)
        {
            string connectionString =
                "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectImmediateReply; Integrated Security=true";
            //將接過來的目標欄位名稱及目標欄位名稱帶有@的陣列各自用「,」連接成一個字串
            string insertcolum = string.Join(",", insertcolname);
            string insertparameter = string.Join(",", insertcolname_P);
            //將user輸入的集合轉為陣列
            string[] puserinsert = insert_P.ToArray();
            //SQL語法參數化"INSERT INTO 資料表名稱 (欄位名稱) VALUES (@欄位名稱)"
            string queryString =
               $@" INSERT INTO {inserttablename}
                         ({insertcolum})
                   VALUES
                         ({insertparameter})";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    //利用迴圈將參數一個一個放進@欄位
                    for (int i = 0; i < insertcolname_P.Length; i++)
                    {
                        command.Parameters.AddWithValue($"{insertcolname_P[i]}", puserinsert[i]);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                }
            }
        }
        /// <summary>
        /// 更新資料庫中的資料列
        /// UPDATE 資料表名稱 SET 欄位名稱=@欄位名稱... WHERE Where條件(欄位名稱=@欄位名稱)
        /// 傳入的@欄位和參數的順序必須相同
        /// updatecolname_P最後一個值須為Where的目標@欄位名稱
        /// 只能下一種WHERE條件
        /// </summary>
        /// <param name="updatetablename">目標資料表名稱</param>
        /// <param name="updatecol_Logic">欲更新的"欄位名稱=@欄位名稱"之字串陣列</param>
        /// <param name="Where_Logic">Where條件</param>
        /// <param name="updatecolname_P">目標欄位名稱及Where條件欄位帶有@的陣列</param>
        /// <param name="update_P">需給予@欄位之參數值的集合</param>
        public static void UpdateTable(string updatetablename, string[] updatecol_Logic, string Where_Logic, string[] updatecolname_P, List<string> update_P)
        {
            string connectionString =
                "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectImmediateReply; Integrated Security=true";
            //將接過來的陣列用「,」連接成一個字串
            string updatecolum = string.Join(",", updatecol_Logic);
            //將user輸入的集合轉為陣列
            string[] puserupdate = update_P.ToArray();
            //SQL語法參數化"UPDATE 資料表名稱 SET 欄位名稱=@欄位名稱... WHERE Where條件(欄位名稱=@欄位名稱)"
            string queryString =
                $@"   UPDATE {updatetablename}
                        SET  {updatecolum}
                        WHERE {Where_Logic} ";

            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    //利用迴圈將參數一個一個放進@欄位
                    for (int i = 0; i < updatecolname_P.Length; i++)
                    {
                        command.Parameters.AddWithValue($"{updatecolname_P[i]}", puserupdate[i]);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                }
            }
        }
        /// <summary>
        /// 刪除資料庫中的資料列
        /// DELETE FROM 資料表名稱 WHERE 欄位名稱=@欄位名稱
        /// </summary>
        /// <param name="deletetablename">目標資料表名稱</param>
        /// <param name="deletecolname">目標欄位名稱</param>
        /// <param name="deletecolname_P">目標欄位名稱帶有@的字串</param>
        /// <param name="delete_P">需給予@欄位之參數值</param>
        public static void DeleteTable(string deletetablename, string deletecolname, string deletecolname_P, string delete_P)
        {
            string connectionString =
                "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectImmediateReply; Integrated Security=true";
            //SQL語法參數化"DELETE FROM 資料表名稱 WHERE 欄位名稱=@欄位名稱"
            string queryString = $"DELETE FROM {deletetablename} WHERE {deletecolname} = {deletecolname_P}";
            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //傳入參數至Where條件的@目標欄位
                command.Parameters.AddWithValue(deletecolname_P, delete_P);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                }
            }
        }
    }
}