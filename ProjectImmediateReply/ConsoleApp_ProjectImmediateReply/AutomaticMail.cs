using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ProjectImmediateReply
{
    class AutomaticMail
    {
        static void Main(string[] args)
        {
            DateTime NowTime = DateTime.Now;
            DBTool Dbtool = new DBTool();

            string[] workscolname = { "ProjectName", "DeadLine" };
            string[] workscolnamep = { "" };
            string[] worksp = { "" };
            string workslogic = @"
                                WHERE Complete IS NULL AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
            DataTable worksdata = Dbtool.readTable("Works", workscolname, workslogic, workscolnamep, worksp);//查此人的所有工作

        }
    }
}
