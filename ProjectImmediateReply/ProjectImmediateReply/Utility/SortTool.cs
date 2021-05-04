using ProjectImmediateReply.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Utility
{
    public class SortTool
    {
        /// <summary>
        /// 將組長以組別排進Dictionary中
        /// </summary>
        /// <param name="item">查回來的單筆資料</param>
        /// <param name="ProjectTeam">組別Dictionary</param>
        /// <param name="key">Dictionary的Key</param>
        /// <returns></returns>
        public Dictionary<int, ForGradesShow> SortTeamLeader(DataRow item, Dictionary<int, ForGradesShow> ProjectTeam, int key)
        {
            if (!ProjectTeam.ContainsKey(key))  //檢查Key值存不存在,不存在則創建新的
                ProjectTeam.Add(key, new ForGradesShow());
            ProjectTeam[key].ProjectID = Convert.ToInt32(item["ProjectID"]); //存在則直接將資料放進該Key的值中
            ProjectTeam[key].ProjectName = item["ProjectName"].ToString(); 
            ProjectTeam[key].LeaderName = item["Name"].ToString();
            ProjectTeam[key].TeamName = item["TeamName"].ToString();

            return ProjectTeam;//將整理好的Dictionary回傳
        }
        /// <summary>
        /// 將組員以組別排進Dictionary中
        /// </summary>
        /// <param name="item">查回來的單筆資料</param>
        /// <param name="member">組員Dictionary</param>
        /// <param name="key">Dictionary的Key</param>
        /// <returns></returns>
        public Dictionary<int, List<string>> SortTeamMember(DataRow item, Dictionary<int, List<string>> member, int key)
        {
            if (!member.ContainsKey(key)) //檢查Key值存不存在,不存在則創建新的
                member.Add(key, new List<string>());
            member[key].Add(item["Name"].ToString()); //存在則直接將資料放進該Key的值中

            return member;//將整理好的Dictionary回傳
        }
    }
}