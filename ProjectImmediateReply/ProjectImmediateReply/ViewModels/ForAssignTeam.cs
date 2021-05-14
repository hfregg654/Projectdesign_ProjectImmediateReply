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