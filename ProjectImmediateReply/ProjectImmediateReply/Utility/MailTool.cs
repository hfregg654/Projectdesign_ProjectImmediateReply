using ProjectImmediateReply.Log;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectImmediateReply.Utility
{
    public class MailTool
    {
        public void SendMail(string mailfrom, string mailto, string mailername, string mailtitle, string mailbody, string mailpassword)
        {
            try
            {
                if (!IsValidEMailAddress(mailfrom)|| !IsValidEMailAddress(mailto))
                {
                    return;
                }
                MailMessage msg = new MailMessage();
                msg.To.Add(mailto);
                //msg.To.Add("b@b.com");可以發送給多人
                //msg.CC.Add("c@c.com");
                //msg.CC.Add("c@c.com");可以抄送副本給多人 
                msg.From = new MailAddress(mailfrom, mailername, Encoding.UTF8);
                /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                msg.Subject = mailtitle;//郵件標題
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼
                msg.Body = mailbody; //郵件內容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                //msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
                msg.IsBodyHtml = true;//是否是HTML郵件 
                                      //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(mailfrom, mailpassword); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證
                client.Send(msg); //寄出信件
                client.Dispose(); //關閉伺服器端連接
                msg.Dispose();  //關閉MaillMessage使用的資源

            }
            catch (Exception ex)
            {
                txtLog logtool = new txtLog();
                logtool.WriteLog(ex.ToString());
                throw;
            }
        }
        public static bool IsValidEMailAddress(string email)
        {
            return Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$");
        }
    }
}