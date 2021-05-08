using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Models
{
    public class UserInfo　//很多屬性的類別
    {
        public int UserID { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string LineID { get; set; }
        public string ClassNumber { get; set; }
        public string License { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int ProjectID { get; set; }
        public string Privilege { get; set; }
        public DateTime CreateDate { get; set; }
        public string WhoCreate { get; set; }
        public DateTime DeleteDate { get; set; }
        public string WhoDelete { get; set; }

    }
}