using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.ViewModels
{
    public class InnerItem_AssignTeam
    {
        [JsonProperty("classchoice")]
        public string ClassNumber { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("inneritem")]
        public List<ForAssignTeam> InnerItems { get; set; }
    }
    public class ForAssignTeam
    {
        //object為最底層型別 可轉換成其他型別
        //接過來為單一值 不會是JSON型態 將JSON轉換成object型別
        [JsonProperty("id")]
        public int UserID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("TeamID")]
        public int TeamID { get; set; }
        [JsonProperty("ProjectName")]
        public string ProjectName { get; set; }
        [JsonProperty("TeamName")]
        public string TeamName { get; set; }
        [JsonProperty("TeamNameGroup")]
        public string[] TeamNameGroup { get; set; }
    }
}