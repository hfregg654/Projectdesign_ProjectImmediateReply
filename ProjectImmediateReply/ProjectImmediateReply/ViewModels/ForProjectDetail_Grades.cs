using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.ViewModels
{
    public class InnerItem_Work
    {
        // 在網頁顯示表用
        //public int UserID { get; set; }
        public string Name { get; set; }
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime UpdateTime { get; set; }
        public string SpendTime { get { return (UpdateTime - DeadLine).ToString(); } }
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
    }
}