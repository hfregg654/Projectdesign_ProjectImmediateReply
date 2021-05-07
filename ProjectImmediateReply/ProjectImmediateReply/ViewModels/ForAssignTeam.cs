using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.ViewModels
{
    public class ForAssignTeam
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int TeamID { get; set; }
        public string ProjectName { get; set; }
        public string TeamName { get; set; }
        public string[] TeamNameGroup { get; set; }
    }
}