using ProjectImmediateReply.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjectImmediateReply.Utility
{
    public class SortTool
    {
        /// <summary>
        /// 將組長以組別排進Dictionary中
        /// </summary>
        /// <param name="item">查回來的單筆資料 是組長的資料</param>
        /// <param name="ProjectTeam">一開始空的組別Dictionary 是整個小組的Dictionary</param>
        /// <param name="key">Dictionary的Key Key是小組ID</param>
        /// <returns></returns>
        public Dictionary<int, ForGradesShow> SortTeamLeader(DataRow item, Dictionary<int, ForGradesShow> ProjectTeam, int key)
        {
            //檢查Key值存不存在,不存在則加入新的key
            if (!ProjectTeam.ContainsKey(key))  
                ProjectTeam.Add(key, new ForGradesShow());
            // 將查到的組長專案ID放入 Dictionary的ProjectID
            ProjectTeam[key].ProjectID = Convert.ToInt32(item["ProjectID"]);
            // 將查到的組長專案名稱放入 Dictionary的ProjectName
            ProjectTeam[key].ProjectName = item["ProjectName"].ToString();
            // 將查到的組長名稱放入 Dictionary的LeaderName
            ProjectTeam[key].LeaderName = item["Name"].ToString();
            // 將查到的組長隊伍名稱放入 Dictionary的TeamName
            ProjectTeam[key].TeamName = item["TeamName"].ToString();

            return ProjectTeam;//將整理好的Dictionary回傳
        }
        /// <summary>
        /// 將組員以組別排進Dictionary中
        /// </summary>
        /// <param name="item">查回來的單筆資料 是成員的資料</param>
        /// <param name="member">一開始空的組別Dictionary 是整個小組的Dictionary</param>
        /// <param name="key">Dictionary的Key Key是小組ID</param>
        /// <returns></returns>
        public Dictionary<int, List<string>> SortTeamMember(DataRow item, Dictionary<int, List<string>> member, int key)
        {
            //檢查Key值存不存在,不存在則加入新的key
            if (!member.ContainsKey(key)) 
                member.Add(key, new List<string>());
            // 將查到的成員名稱放入 Dictionary的Name
            member[key].Add(item["Name"].ToString());

            return member;//將整理好的Dictionary回傳
        }
    }
}