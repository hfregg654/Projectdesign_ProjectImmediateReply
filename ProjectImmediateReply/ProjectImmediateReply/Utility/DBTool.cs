using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ProjectImmediateReply.Models;

namespace ProjectImmediateReply.Utility
{
    public class DBTool
    {
        //資料庫連結字串
        private const string connectionString =
               "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectImmediateReply; Integrated Security=true";
        /// <summary>
        /// 判斷接過來的readtablename來決定那個表的order方式
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <returns></returns>
        private string OrderName(string readtablename)
        {
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
            return ordername;
        }
        /// <summary>
        /// 讀取資料庫目標資料表的目標欄位資料
        /// SELECT 欄位名稱 FROM 資料表名稱 ORDER BY 排序目標
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="readcolname">目標欄位名稱的陣列</param>
        /// <returns></returns>
        public DataTable readTable(string readtablename, string[] readcolname)
        {
            //將接過來的目標欄位名稱陣列用「,」連接成一個字串
            string readcoladd = string.Join(",", readcolname);
            //判斷接過來的readtablename來決定那個表的order方式
            string ordername = OrderName(readtablename);
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
        /// 不需要的參數請傳入NULL
        /// SELECT 欄位名稱 FROM 資料表名稱 "SELECT 欄位名稱 FROM 資料表名稱 條件"
        /// 條件的帶@參數名及參數值順序必須相同
        /// </summary>
        /// <param name="readtablename">目標資料表名稱</param>
        /// <param name="readcolname">目標欄位名稱的陣列</param>
        /// <param name="Logic">條件</param>
        /// <param name="Pname">條件的帶@參數名陣列</param>
        /// <param name="P">條件的參數值陣列</param>
        /// <returns></returns>
        public DataTable readTable(string readtablename, string[] readcolname,
            string Logic, string[] Pname, string[] P)
        {
            //將接過來的目標欄位名稱陣列用「,」連接成一個字串
            string readcoladd = string.Join(",", readcolname);
            //判斷接過來的readtablename來決定那個表的排序方式
            string ordername = OrderName(readtablename);
            //SQL語法參數化"SELECT 欄位名稱 FROM 資料表名稱 條件"
            string queryString =
                $@" SELECT {readcoladd} FROM {readtablename}
                    {Logic};";

            //資料庫開啟並執行SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //傳入參數至@目標欄位
                if (Pname != null && P != null
                    && Pname.Length != 0 && P.Length != 0)
                {
                    for (int i = 0; i < Pname.Length; i++)
                        command.Parameters.AddWithValue(Pname[i], P[i]);
                }
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
        /// 將DataTable轉換至UserInfo型別
        /// </summary>
        /// <param name="dataTable">DataTable型別的資料</param>
        /// <returns></returns>
        public List<UserInfo> ChangeTypeUserInfo(DataTable dataTable)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    var uInfo = new UserInfo();
                    if (dataTable.Columns["UserID"] != null && item["UserID"] != null)
                        uInfo.UserID = Convert.ToInt32(item["UserID"]);
                    if (dataTable.Columns["Account"] != null && item["Account"] != null)
                        uInfo.Account = item["Account"].ToString();
                    if (dataTable.Columns["PassWord"] != null && item["PassWord"] != null)
                        uInfo.PassWord = item["PassWord"].ToString();
                    if (dataTable.Columns["Name"] != null && item["Name"] != null)
                        uInfo.Name = item["Name"].ToString();
                    if (dataTable.Columns["Phone"] != null && item["Phone"] != null)
                        uInfo.Phone = item["Phone"].ToString();
                    if (dataTable.Columns["Mail"] != null && item["Mail"] != null)
                        uInfo.Mail = item["Mail"].ToString();
                    if (dataTable.Columns["LineID"] != null && item["LineID"] != null)
                        uInfo.LineID = item["LineID"].ToString();
                    if (dataTable.Columns["ClassNumber"] != null && item["ClassNumber"] != null)
                        uInfo.ClassNumber = item["ClassNumber"].ToString();
                    if (dataTable.Columns["License"] != null && item["License"] != null)
                        uInfo.License = item["License"].ToString();
                    if (dataTable.Columns["TeamID"] != null && item["TeamID"] != null)
                        uInfo.TeamID = Convert.ToInt32(item["TeamID"]);
                    if (dataTable.Columns["TeamName"] != null && item["TeamName"] != null)
                        uInfo.TeamName = item["TeamName"].ToString();
                    if (dataTable.Columns["ProjectID"] != null && item["ProjectID"] != null)
                        uInfo.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    if (dataTable.Columns["WorkID"] != null && item["WorkID"] != null)
                        uInfo.WorkID = item["WorkID"].ToString();
                    if (dataTable.Columns["Privilege"] != null && item["Privilege"] != null)
                        uInfo.Privilege = item["Privilege"].ToString();
                    if (dataTable.Columns["CreateDate"] != null && item["CreateDate"] != null)
                        uInfo.CreateDate = Convert.ToDateTime(item["CreateDate"]);
                    if (dataTable.Columns["WhoCreate"] != null && item["WhoCreate"] != null)
                        uInfo.WhoCreate = item["WhoCreate"].ToString();
                    if (dataTable.Columns["DeleteDate"] != null && item["DeleteDate"] != null)
                        uInfo.DeleteDate = Convert.ToDateTime(item["DeleteDate"]);
                    if (dataTable.Columns["WhoDelete"] != null && item["WhoDelete"] != null)
                        uInfo.WhoDelete = item["WhoDelete"].ToString();
                    list.Add(uInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                return null;
            }
        }
        /// <summary>
        /// 將DataTable轉換至ProjectInfo型別
        /// </summary>
        /// <param name="dataTable">DataTable型別的資料</param>
        /// <returns></returns>
        public List<ProjectInfo> ChangeTypeProjectInfo(DataTable dataTable)
        {
            List<ProjectInfo> list = new List<ProjectInfo>();
            try
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    var uInfo = new ProjectInfo();
                    if (dataTable.Columns["ProjectID"] != null && item["ProjectID"] != null)
                        uInfo.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    if (dataTable.Columns["ProjectName"] != null && item["ProjectName"] != null)
                        uInfo.ProjectName = item["ProjectName"].ToString();
                    if (dataTable.Columns["TeamID"] != null && item["TeamID"] != null)
                        uInfo.TeamID = Convert.ToInt32(item["TeamID"]);
                    if (dataTable.Columns["TeamName"] != null && item["TeamName"] != null)
                        uInfo.TeamName = item["TeamName"].ToString();
                    if (dataTable.Columns["WorkID"] != null && item["WorkID"] != null)
                        uInfo.WorkID = item["WorkID"].ToString();
                    if (dataTable.Columns["Schedule"] != null && item["Schedule"] != null)
                        uInfo.Schedule = Convert.ToSingle(item["Schedule"]);
                    if (dataTable.Columns["DeadLine"] != null && item["DeadLine"] != null)
                        uInfo.DeadLine = Convert.ToDateTime(item["DeadLine"]);
                    if (dataTable.Columns["CreateDate"] != null && item["CreateDate"] != null)
                        uInfo.CreateDate = Convert.ToDateTime(item["CreateDate"]);
                    if (dataTable.Columns["WhoCreate"] != null && item["WhoCreate"] != null)
                        uInfo.WhoCreate = item["WhoCreate"].ToString();
                    if (dataTable.Columns["DeleteDate"] != null && item["DeleteDate"] != null)
                        uInfo.DeleteDate = Convert.ToDateTime(item["DeleteDate"]);
                    if (dataTable.Columns["WhoDelete"] != null && item["WhoDelete"] != null)
                        uInfo.WhoDelete = item["WhoDelete"].ToString();
                    list.Add(uInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                return null;
            }
        }
        /// <summary>
        /// 將DataTable轉換至WorkInfo型別
        /// </summary>
        /// <param name="dataTable">DataTable型別的資料</param>
        /// <returns></returns>
        public List<WorkInfo> ChangeTypeWorkInfo(DataTable dataTable)
        {
            List<WorkInfo> list = new List<WorkInfo>();
            try
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    var uInfo = new WorkInfo();
                    if (dataTable.Columns["ProjectID"] != null && item["ProjectID"] != null)
                        uInfo.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    if (dataTable.Columns["TeamID"] != null && item["TeamID"] != null)
                        uInfo.TeamID = Convert.ToInt32(item["TeamID"]);
                    if (dataTable.Columns["WorkID"] != null && item["WorkID"] != null)
                        uInfo.WorkID = Convert.ToInt32(item["WorkID"]);
                    if (dataTable.Columns["WorkDescription"] != null && item["WorkDescription"] != null)
                        uInfo.WorkDescription = item["WorkDescription"].ToString();
                    if (dataTable.Columns["DeadLine"] != null && item["DeadLine"] != null)
                        uInfo.DeadLine = Convert.ToDateTime(item["DeadLine"]);
                    if (dataTable.Columns["FilePath"] != null && item["FilePath"] != null)
                        uInfo.FilePath = item["FilePath"].ToString();
                    if (dataTable.Columns["UpdateTime"] != null && item["UpdateTime"] != null)
                        uInfo.UpdateTime = Convert.ToDateTime(item["UpdateTime"]);
                    if (dataTable.Columns["SpendTime"] != null && item["SpendTime"] != null)
                        uInfo.SpendTime = Convert.ToDateTime(item["SpendTime"]);
                    if (dataTable.Columns["CreateDate"] != null && item["CreateDate"] != null)
                        uInfo.CreateDate = Convert.ToDateTime(item["CreateDate"]);
                    if (dataTable.Columns["WhoCreate"] != null && item["WhoCreate"] != null)
                        uInfo.WhoCreate = item["WhoCreate"].ToString();
                    if (dataTable.Columns["DeleteDate"] != null && item["DeleteDate"] != null)
                        uInfo.DeleteDate = Convert.ToDateTime(item["DeleteDate"]);
                    if (dataTable.Columns["WhoDelete"] != null && item["WhoDelete"] != null)
                        uInfo.WhoDelete = item["WhoDelete"].ToString();
                    list.Add(uInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                return null;
            }
        }
        /// <summary>
        /// 將DataTable轉換至GradeInfo型別
        /// </summary>
        /// <param name="dataTable">DataTable型別的資料</param>
        /// <returns></returns>
        public List<GradeInfo> ChangeTypeGradeInfo(DataTable dataTable)
        {
            List<GradeInfo> list = new List<GradeInfo>();
            try
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    var uInfo = new GradeInfo();
                    if (dataTable.Columns["GradeID"] != null && item["GradeID"] != null)
                        uInfo.GradeID = Convert.ToInt32(item["GradeID"]);
                    if (dataTable.Columns["UserID"] != null && item["UserID"] != null)
                        uInfo.UserID = Convert.ToInt32(item["UserID"]);
                    if (dataTable.Columns["PresidentProjectGrade"] != null && item["PresidentProjectGrade"] != null)
                        uInfo.PresidentProjectGrade = Convert.ToByte(item["PresidentProjectGrade"]);
                    if (dataTable.Columns["PresidentInterviewGrade"] != null && item["TeaPresidentInterviewGrademID"] != null)
                        uInfo.PresidentInterviewGrade = Convert.ToByte(item["PresidentInterviewGrade"]);
                    if (dataTable.Columns["PresidentComments"] != null && item["PresidentComments"] != null)
                        uInfo.PresidentComments = item["PresidentComments"].ToString();
                    if (dataTable.Columns["PMProjectGrade"] != null && item["PMProjectGrade"] != null)
                        uInfo.PMProjectGrade = Convert.ToByte(item["PMProjectGrade"]);
                    if (dataTable.Columns["PMInterviewGrade"] != null && item["PMInterviewGrade"] != null)
                        uInfo.PMInterviewGrade = Convert.ToByte(item["PMInterviewGrade"]);
                    if (dataTable.Columns["PMComments"] != null && item["PMComments"] != null)
                        uInfo.PMComments = item["PMComments"].ToString();
                    if (dataTable.Columns["CreateDate"] != null && item["CreateDate"] != null)
                        uInfo.CreateDate = Convert.ToDateTime(item["CreateDate"]);
                    if (dataTable.Columns["WhoCreate"] != null && item["WhoCreate"] != null)
                        uInfo.WhoCreate = item["WhoCreate"].ToString();
                    if (dataTable.Columns["DeleteDate"] != null && item["DeleteDate"] != null)
                        uInfo.DeleteDate = Convert.ToDateTime(item["DeleteDate"]);
                    if (dataTable.Columns["WhoDelete"] != null && item["WhoDelete"] != null)
                        uInfo.WhoDelete = item["WhoDelete"].ToString();
                    list.Add(uInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                return null;
            }
        }


        /// <summary>
        /// 往資料庫中插入新資料列
        /// INSERT INTO 資料表名稱 (欄位名稱) VALUES (@欄位名稱)
        /// 傳入的@欄位和參數的順序必須相同
        /// </summary>
        /// <param name="inserttablename">目標資料表名稱</param>
        /// <param name="insertcolname">目標欄位名稱的陣列</param>
        /// <param name="insertcolname_P">目標欄位名稱帶有@的陣列</param>
        /// <param name="insert_P">需給予@欄位之參數值的集合</param>
        public void InsertTable(string inserttablename, string[] insertcolname,
            string[] insertcolname_P, List<string> insert_P)
        {
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
        public void UpdateTable(string updatetablename, string[] updatecol_Logic, string Where_Logic, string[] updatecolname_P, List<string> update_P)
        {
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
        public void DeleteTable(string deletetablename, string deletecolname, string deletecolname_P, string delete_P)
        {
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