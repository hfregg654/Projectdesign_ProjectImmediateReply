using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Utility
{
    public class RandomTool
    {
        /// <summary>
        /// 創建授權碼,回傳值為"0000,0000,0000,0000"為一組的集合
        /// </summary>
        /// <param name="num">授權碼組數</param>
        /// <param name="Pri">傳入Session中的權限值,認證用</param>
        /// <returns></returns>
        public static List<string> CreateLicence(int num, string Pri)
        {
            //判斷身分是否為管理者,不是救回傳null
            if (Pri != "Manager")
                return null;
            //定義亂數種子  定義數字每4位為一個小組,每4組為一個授權碼,共16碼  另外定義計數器及重複檢查器
            Random random = new Random();
            ushort licence_cell;
            List<ushort> licencelist = new List<ushort>();
            List<string> licence = new List<string>();
            uint counter = 0;
            bool isrepeat;
            //嘗試抓取錯誤,有錯誤即回傳null及錯誤訊息
            try
            {
                do
                {
                    //重複檢查器初始化
                    isrepeat = false;
                    //將亂數取得的4位數
                    licence_cell = Convert.ToUInt16(random.Next(1000, 9999));
                    licencelist.Add(licence_cell);
                    counter++;
                    if (counter % 4 == 0)
                    {
                        counter = 0;
                        licence.Add(string.Join(",", licencelist));
                        for (int i = 0; i < licence.Count; i++)
                        {
                            for (int j = i + 1; j < licence.Count; j++)
                            {
                                if (licence[i] == licence[j])
                                {
                                    isrepeat = true;
                                    licence.RemoveAt(j);
                                    break;
                                }
                            }
                            if (isrepeat)
                                break;
                        }
                        licencelist.Clear();
                    }
                } while (licence.Count != num);
                return licence;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                return null;
            }

        }

    }
}