using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Log
{
    public class txtLog
    {
        private static String logPath = "\\WriteLog"; //Log目錄


        public void WriteLog(String logMsg)
        {
            //檔案名稱 使用現在日期
            String logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".txt";

            //Log檔內的時間 使用現在時間
            String nowTime = int.Parse(DateTime.Now.Hour.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Minute.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Second.ToString()).ToString("00");

            if (!Directory.Exists(logPath))
            {
                //建立資料夾
                Directory.CreateDirectory(logPath);
            }

            if (!File.Exists(logPath + "\\" + logFileName))
            {
                //建立檔案
                File.Create(logPath + "\\" + logFileName).Close();
            }

            using (StreamWriter sw = File.AppendText(logPath + "\\" + logFileName))
            {
                //WriteLine為換行 
                sw.Write(nowTime + "---->");
                sw.WriteLine(logMsg);
                sw.WriteLine("");
            }
        }
    }
}