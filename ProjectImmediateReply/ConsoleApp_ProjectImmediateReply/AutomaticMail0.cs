using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ProjectImmediateReply
{
    class AutomaticMail0
    {
        //private const string sql = "select Name,Email from table where birthday=@birthday"; //尋找今天有生日的人
        //private const string dt = DateTime.Now.ToShortDateString();
        //static void Main(string[] args)
        //{
        //    StringBuilder sb_mail = new StringBuilder();
        //    StringBuilder sb_name = new StringBuilder();
        //    using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sql, con))
        //        {
        //            con.Open();
        //            cmd.Parameters.Add("@birthday", SqlDbType.NVarChar);
        //            cmd.Parameters["@birthday"].Value = dt;
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                dr.Read();

        //                sb_mail.Append(string.Format("{0},", dr["Email"]));
        //                sb_name.Append(string.Format("{0}@", dr["Name"]));
        //            }
        //            else
        //            {
        //                return; //今天沒有人生日
        //            }
        //            con.Close();
        //        }
        //    }

        //    if (sb_mail.ToString() != string.Empty || sb_name.ToString() != string.Empty)
        //    {
        //        string LineName;
        //        string your_gmail_Acc = "123@gmail.com";
        //        string your_gmail_Pass = "123";
        //        LineName = sb_name.ToString().Replace("@", Environment.NewLine); //將指定字元'@'轉成空白鍵等同HTML的<br>標籤
        //        using (System.IO.StringReader sr = new System.IO.StringReader(sb_name.ToString()))
        //        {
        //            while ((LineName = sr.ReadLine()) != null)
        //            {
        //                try
        //                {
        //                    MailMessage msg = new MailMessage();
        //                    string MailList = sb_mail.ToString();
        //                    msg.To.Add(new MailAddress(MailList));
        //                    msg.From = new MailAddress(your_gmail_Acc, "生日快樂", System.Text.Encoding.UTF8);

        //                    msg.Subject = "生日快樂";
        //                    msg.SubjectEncoding = System.Text.Encoding.UTF8;

        //                    msg.Body = @"
        //                    <p>親愛的" + LineName + "</p>" +
        //                    "<img src="美工設計的生日明信片位置">" +
        //                    "";
        //                    msg.IsBodyHtml = true;
        //                    msg.BodyEncoding = System.Text.Encoding.UTF8;
        //                    msg.Priority = MailPriority.Normal;//郵件優先級 
        //                    SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
        //                    MySmtp.Credentials = new System.Net.NetworkCredential(your_gmail_Acc, your_gmail_Pass);
        //                    //MySmtp.UseDefaultCredentials = true;
        //                    MySmtp.EnableSsl = true;
        //                    MySmtp.Send(msg);
        //                }
        //                catch
        //                {
        //                    return; //發送時錯誤,停止
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
