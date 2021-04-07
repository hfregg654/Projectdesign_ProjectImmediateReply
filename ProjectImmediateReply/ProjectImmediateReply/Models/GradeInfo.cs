using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Models
{
    public class GradeInfo
    {
        public int GradeID { get; set; }
        public int UserID { get; set; }
        public byte PresidentProjectGrade { get; set; }
        public byte PresidentInterviewGrade { get; set; }
        public string PresidentComments { get; set; }
        public byte PMProjectGrade { get; set; }
        public byte PMInterviewGrade { get; set; }
        public string PMComments { get; set; }
        public DateTime CreateDate { get; set; }
        public string WhoCreate { get; set; }
        public DateTime DeleteDate { get; set; }
        public string WhoDelete { get; set; }
    }
}