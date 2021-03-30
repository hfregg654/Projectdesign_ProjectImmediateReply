using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Utility
{
    public class RandomTool
    {
        /// <summary>
        /// 創建授權碼,回傳值為16位數字為一組的集合
        /// </summary>
        /// <param name="num">授權碼組數</param>
        /// <param name="Pri">傳入Session中的權限值,認證用</param>
        /// <returns></returns>
        public static List<string> CreateLicence(int num, string Pri)
        {
            //判斷身分是否為管理者,不是則回傳null
            if (Pri != "Manager")
                return null;
            //定義亂數種子    
            Random random = new Random();
            //定義數字每16位為一組授權碼
            byte licence_cell;
            List<ushort> licencelist = new List<ushort>();
            List<string> licence = new List<string>();
            //另外定義計數器及重複檢查器
            byte counter = 0;
            bool isrepeat;
            //嘗試抓取錯誤,有錯誤即回傳null及錯誤訊息
            try
            {
                //重複創建不重複的授權碼,直到到達預定的數量
                do
                {
                    //重複檢查器初始化
                    isrepeat = false;
                    //將亂數取得的0~9之數字放進集合中
                    licence_cell = Convert.ToByte(random.Next(0, 10));
                    licencelist.Add(licence_cell);
                    counter++;//計數器+1
                    //每16位就將其合併成為一組授權碼
                    if (counter % 16 == 0)
                    {
                        counter = 0;//計數器歸零
                        //將16位數字連接成一組授權碼
                        licence.Add(string.Join("", licencelist));
                        //檢查授權碼集合內是否有重複的授權碼,若有則將當前授權碼移除重新創建一組
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
                        //小授權碼集合清空
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
        /// <summary>
        /// 亂數將專案及小組分配為4組,回傳值為集合第一項為專案,後續為組員的集合
        /// </summary>
        /// <param name="assignpeople">當前班級所有人的集合</param>
        /// <param name="assignporject">當前所有專案的集合</param>
        /// <param name="Pri">傳入Session中的權限值,認證用</param>
        /// <returns></returns>
        public static List<string> RandomAssign(List<string> assignpeople,List<string> assignporject, string Pri)
        {
            if (Pri != "Manager")
                return null;
            Random random = new Random();
            byte Team_cell;
            byte projectnum;
            List<string> people = new List<string>();
            List<string> team = new List<string>();
            List<string> _class = new List<string>();
            if (assignpeople.Count % 4 == 0)
            {
                ushort peoplenum = Convert.ToUInt16(assignpeople.Count / 4);
                foreach (string item in assignpeople)
                {
                    people.Add(item);
                }
                do
                {
                    Team_cell = Convert.ToByte(random.Next(0, people.Count));
                    team.Add(people.ToArray()[Team_cell]);
                    people.RemoveAt(Team_cell);
                    if (team.Count % peoplenum == 0)
                    {
                        _class.Add(string.Join(",", team));
                        team.Clear();
                    }
                } while (_class.Count != 4);
                return _class;
            }
            else
            {
                ushort peoplelonly = Convert.ToUInt16(assignpeople.Count % 4);
                ushort peoplenum = Convert.ToUInt16((assignpeople.Count - (peoplelonly)) / 4);
                foreach (string item in assignpeople)
                {
                    people.Add(item);
                }
                do
                {
                    Team_cell = Convert.ToByte(random.Next(0, people.Count));
                    team.Add(people.ToArray()[Team_cell]);
                    people.RemoveAt(Team_cell);

                    if (team.Count % peoplenum == 0)
                    {
                        if (peoplelonly != 0)
                        {
                            if (random.Next(2) == 0)
                            {
                                peoplelonly--;
                                projectnum = Convert.ToByte(random.Next(assignporject.Count));
                                Team_cell = Convert.ToByte(random.Next(0, people.Count));
                                team.Add(people.ToArray()[Team_cell]);
                                people.RemoveAt(Team_cell);
                                _class.Add($"{assignporject.ToArray()[projectnum]},{string.Join(",", team)}");
                                assignporject.RemoveAt(projectnum);
                                team.Clear();
                            }
                            else if (peoplelonly < _class.Count)
                            {
                                peoplelonly--;
                                projectnum = Convert.ToByte(random.Next(assignporject.Count));
                                Team_cell = Convert.ToByte(random.Next(0, people.Count));
                                team.Add(people.ToArray()[Team_cell]);
                                people.RemoveAt(Team_cell);
                                _class.Add($"{assignporject.ToArray()[projectnum]},{string.Join(",", team)}");
                                assignporject.RemoveAt(projectnum);
                                team.Clear();
                            }
                            else
                            {
                                projectnum = Convert.ToByte(random.Next(assignporject.Count));
                                _class.Add($"{assignporject.ToArray()[projectnum]},{string.Join(",", team)}");
                                assignporject.RemoveAt(projectnum);
                                team.Clear();
                            }
                        }
                        else
                        {
                            projectnum = Convert.ToByte(random.Next(assignporject.Count));
                            _class.Add($"{assignporject.ToArray()[projectnum]},{string.Join(",", team)}");
                            assignporject.RemoveAt(projectnum);
                            team.Clear();
                        }
                    }
                } while (_class.Count != 4);
                return _class;
            }
        }
    }
}