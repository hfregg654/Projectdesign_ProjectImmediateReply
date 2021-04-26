using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Models
{
    public class ProjectInfo
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ClassNumber { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string WorkID { get; set; }
        public float Schedule { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime CreateDate { get; set; }
        public string WhoCreate { get; set; }
        public DateTime DeleteDate { get; set; }
        public string WhoDelete { get; set; }
    }
}