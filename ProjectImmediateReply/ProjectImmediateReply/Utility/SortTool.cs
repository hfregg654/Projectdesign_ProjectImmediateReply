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

        public ForGradesShow GetForGradeShowSort(DataRow data, ForGradesShow Team, List<string> member)
        {
            if (data["Privilege"].ToString() == "Leader")
            {
                Team.ProjectName = data["ProjectName"].ToString();
                Team.LeaderName = data["Name"].ToString();
                Team.TeamName = data["TeamName"].ToString();
            }
            else
            {
                member.Add(data["Name"].ToString());
            }
            return Team;
        }

    }
}