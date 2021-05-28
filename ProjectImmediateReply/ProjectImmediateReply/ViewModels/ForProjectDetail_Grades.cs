using System.Collections.Generic;

namespace ProjectImmediateReply.ViewModels
{

    public class InnerItem_Work
    {
        //小組專案評分頁面所使用到的變數模型
        // 在網頁顯示表用
        //public int UserID { get; set; }
        public string Name { get; set; }
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string DeadLine { get; set; }
        public string UpdateTime { get; set; }
        public string SpendTime { get; set; }
        public string FilePath { get; set; }
    }
    public class ForProjectDetail_Grades
    {
        //除List之外只會有一筆
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string LeaderName { get; set; }
        public string MemberName { get; set; }
        public string TeamName { get; set; }
        public List<InnerItem_Work> inneritem { get;  set;}
        // 傳過去ProjectDetail_Grades 的變數名稱 inneritem
        public List<UserNameGroupforPD_G> NameGroup { get; set; }
    }
    public class UserNameGroupforPD_G
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}