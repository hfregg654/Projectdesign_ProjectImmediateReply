using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.ViewModels
{
    public class ForSeeGrade
    {
        public string ClassNumber { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public byte Grade { get; set; }
        public string PresidentComments { get; set; }
        public string PMComments { get; set; }
    }
}