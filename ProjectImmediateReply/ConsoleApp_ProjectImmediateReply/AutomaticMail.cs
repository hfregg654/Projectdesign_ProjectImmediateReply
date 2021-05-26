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
            DateTime today = DateTime.Now;
            DBTool Dbtool = new DBTool();
            MailTool mailTool = new MailTool();


            //檢查專案期限
            string[] projectscolname = { "ProjectID", "ProjectName", "DeadLine" };
            string[] projectscolnamep = { "" };
            string[] projectsp = { "" };
            string projectslogic = @"
                                WHERE Complete=0 AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
            DataTable projectsdata = Dbtool.readTable("Projects", projectscolname, projectslogic, projectscolnamep, projectsp);//查所有未完成專案
            foreach (DataRow item in projectsdata.Rows)
            {
                TimeSpan ts = new TimeSpan(Convert.ToDateTime(item["DeadLine"]).Ticks - today.Ticks);
                int DeadDate = (int)ts.TotalDays;

                if (DeadDate < 7 && DeadDate > 0)
                {
                    string[] colname = { "Mail" };
                    string[] colnamep = { "@ProjectID" };
                    string[] p = { item["ProjectID"].ToString() };
                    string logic = @"
                                WHERE ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL AND Mail IS NOT NULL
                                ";
                    DataTable Maildata_Project = Dbtool.readTable("Users", colname, logic, colnamep, p);//查需寄信人的Mail
                    //每個須寄信都都寄
                    foreach (DataRow mitem in Maildata_Project.Rows)
                    {
                        mailTool.SendMail("shiyuance989898@gmail.com", mitem["Mail"].ToString(), "系統", "專案接近期限", $"您的專案「{item["ProjectName"]}」已接近完成期限<br/>剩餘{DeadDate}天", "1qazxcvfr432wsde");
                    }
                    mailTool.SendMail("shiyuance989898@gmail.com", "shiyuance989898@gmail.com", "系統", "專案接近期限", $"專案「{item["ProjectName"]}」已接近完成期限<br/>剩餘{DeadDate}天", "1qazxcvfr432wsde");
                }
                else if (DeadDate < 0)
                {
                    string[] colname = { "Mail" };
                    string[] colnamep = { "@ProjectID" };
                    string[] p = { item["ProjectID"].ToString() };
                    string logic = @"
                                WHERE ProjectID=@ProjectID AND DeleteDate IS NULL AND WhoDelete IS NULL AND Mail IS NOT NULL
                                ";
                    DataTable Maildata_Project = Dbtool.readTable("Users", colname, logic, colnamep, p);//查需寄信人的Mail
                    //每個須寄信都都寄
                    foreach (DataRow mitem in Maildata_Project.Rows)
                    {
                        mailTool.SendMail("shiyuance989898@gmail.com", mitem["Mail"].ToString(), "系統", "專案已過期", $"您的專案「{item["ProjectName"]}」已過期<br/>超過{Math.Abs(DeadDate)}天", "1qazxcvfr432wsde");
                    }
                    mailTool.SendMail("shiyuance989898@gmail.com", "shiyuance989898@gmail.com", "系統", "專案已過期", $"專案「{item["ProjectName"]}」已過期<br/>超過{Math.Abs(DeadDate)}天", "1qazxcvfr432wsde");
                }
            }


            //檢查工作期限
            string[] workscolname = { "WorkID", "WorkName", "DeadLine" };
            string[] workscolnamep = { "" };
            string[] worksp = { "" };
            string workslogic = @"
                                WHERE Complete=0 AND DeleteDate IS NULL AND WhoDelete IS NULL
                                ";
            DataTable worksdata = Dbtool.readTable("Works", workscolname, workslogic, workscolnamep, worksp);//查所有未完成工作

            foreach (DataRow item in worksdata.Rows)
            {
                TimeSpan ts = new TimeSpan(Convert.ToDateTime(item["DeadLine"]).Ticks - today.Ticks);
                int DeadDate = (int)ts.TotalDays;

                if (DeadDate < 7 && DeadDate > 0)
                {
                    string[] colname = { "Works.WorkName", "Users.Mail" };
                    string[] colnamep = { "@WorkID" };
                    string[] p = { item["WorkID"].ToString() };
                    string logic = @"
                                INNER JOIN Works ON Users.UserID=Works.UserID
                                WHERE Works.WorkID=@WorkID AND Users.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ";
                    DataTable Maildata_Work = Dbtool.readTable("Users", colname, logic, colnamep, p);//查需寄信人的Mail

                    foreach (DataRow mitem in Maildata_Work.Rows)
                    {
                        mailTool.SendMail("shiyuance989898@gmail.com", mitem["Mail"].ToString(), "系統", $"工作接近期限", $"您的工作「{item["WorkName"]}」已接近完成期限<br/>剩餘{DeadDate}天", "1qazxcvfr432wsde");
                    }
                }
                else if (DeadDate < 0)
                {
                    string[] colname = { "Works.WorkName", "Users.Mail" };
                    string[] colnamep = { "@WorkID" };
                    string[] p = { item["WorkID"].ToString() };
                    string logic = @"
                                INNER JOIN Works ON Users.UserID=Works.UserID
                                WHERE Works.WorkID=@WorkID AND Users.DeleteDate IS NULL AND Users.WhoDelete IS NULL
                                ";
                    DataTable Maildata_Work = Dbtool.readTable("Users", colname, logic, colnamep, p);//查需寄信人的Mail

                    foreach (DataRow mitem in Maildata_Work.Rows)
                    {
                        mailTool.SendMail("shiyuance989898@gmail.com", mitem["Mail"].ToString(), "系統", $"工作已過期", $"您的工作「{item["WorkName"]}」已過期<br/>超過{Math.Abs(DeadDate)}天", "1qazxcvfr432wsde");
                    }
                }
            }



        }
    }
}
