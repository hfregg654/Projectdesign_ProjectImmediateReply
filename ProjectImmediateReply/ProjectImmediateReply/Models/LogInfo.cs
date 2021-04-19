using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Models
{
    public enum UserLevel
    {
        /// <summary> 學員 </summary>
        User = 0,

        /// <summary>
        /// 組長
        /// </summary>
        Leader = 1,

        /// <summary>
        /// 管理者
        /// </summary>
        Manager = 2,

        /// <summary>
        /// 評分者
        /// </summary>
        Judges = 3

    }
    public class LogInfo
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Privilege { get; set; }
        public string Mail { get; set; }
    }
}