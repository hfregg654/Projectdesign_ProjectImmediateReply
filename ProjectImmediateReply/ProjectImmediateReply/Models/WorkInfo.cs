using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Models
{
    public class WorkInfo
    {
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public DateTime DeadLine { get; set; }
        public string FilePath { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Complete { get; set; }
        public DateTime CreateDate { get; set; }
        public string WhoCreate { get; set; }
        public DateTime DeleteDate { get; set; }
        public string WhoDelete { get; set; }
    }
}