using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectImmediateReply.ViewModels
{
    //Leader專案管理的變數模型
    public class ForManageProject
    {
        [JsonProperty("Userid")]
        public int UserID { get; set; }
        [JsonProperty("UserName")]
        public string Name { get; set; }
        [JsonProperty("Privilege")]
        public string Privilege { get; set; }
        [JsonProperty("ClassNumber")]
        public string ClassNumber { get; set; }
        [JsonProperty("TeamName")]
        public string TeamName { get; set; }
        [JsonProperty("ProjectID")]
        public int ProjectID { get; set; }
        [JsonProperty("ProjectName")]
        public string ProjectName { get; set; }
        [JsonProperty("inneritem")]
        public List<ViewWorks> viewWorks { get; set; }
        [JsonProperty("ProjectSchedule")]
        public string Schedule { get; set; }
        [JsonProperty("ProjectComplete")]
        public bool ProjectComplete { get; set; }

    }
    public class ViewWorks
    {
        [JsonProperty("Work_UserID")]
        public int Work_UserID { get; set; }
        [JsonProperty("id")]
        public int WorkID { get; set; }
        [JsonProperty("WorkName")]
        public string WorkName { get; set; }
        [JsonProperty("WorkDescription")]
        public string WorkDescription { get; set; }
        [JsonProperty("DeadLine")]
        public string DeadLine { get; set; }
        [JsonProperty("FilePath")]
        public string FilePath { get; set; }
        [JsonProperty("UpdateTime")]
        public string UpdateTime { get; set; }
        [JsonProperty("Complete")]
        public bool Complete { get; set; }
    }
}