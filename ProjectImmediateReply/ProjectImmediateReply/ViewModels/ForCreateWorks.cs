using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectImmediateReply.ViewModels
{
    //Leader的建立工作頁面變數模型
    public class ForCreateWorks
    {
        [JsonProperty("ProjectID")]
        public int ProjectID { get; set; }
        [JsonProperty("ProjectName")]
        public string ProjectName { get; set; }
        [JsonProperty("TeamName")]
        public string TeamName { get; set; }
        [JsonProperty("SelectList")]
        public List<TeamMember> TeamMember { get; set; }
        [JsonProperty("newWorkProject")]
        public List<WorkItem> WorkItems { get; set; }


    }
    public class TeamMember
    {
        [JsonProperty("UserID")]
        public int UserID { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }
    public class WorkItem
    {
        [JsonProperty("WorkID")]
        public int WorkID { get; set; }
        [JsonProperty("WorkName")]
        public string WorkName { get; set; }
        [JsonProperty("WorkDescription")]
        public string WorkDescription { get; set; }
        [JsonProperty("DeadLine")]
        public string DeadLine { get; set; }
        [JsonProperty("OrderMember")]
        public string OrderName { get; set; }
    }
}