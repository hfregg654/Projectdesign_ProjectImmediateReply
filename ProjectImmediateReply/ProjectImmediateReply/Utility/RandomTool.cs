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
        public List<string> CreateLicence(int num, string Pri)
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
        /// 將隨機人員加入小組集合
        /// </summary>
        /// <param name="people">當前剩餘人員之集合</param>
        /// <param name="team">當前未滿人之小組集合</param>
        private void AssignMember(List<string> assignpeople, List<string> team)
        {
            //定義亂數種子並定義數字儲存被抽到的人員在集合中的號碼
            Random random = new Random();
            byte Team_cell = Convert.ToByte(random.Next(0, assignpeople.Count));
            //將該人員放入小組集合中並將其移除於人員集合
            team.Add(assignpeople.ToArray()[Team_cell]);
            assignpeople.RemoveAt(Team_cell);
        }
        /// <summary>
        /// 將隨機專案加入小組集合並將其加入班級集合
        /// </summary>
        /// <param name="assignporject">當前剩餘專案之集合</param>
        /// <param name="team">當前已滿人之小組集合</param>
        /// <param name="_class">當前未滿組之班級集合</param>
        private void AssignProject_AssignTeam(List<string> assignporject, List<string> team, List<string> _class)
        {
            //定義亂數種子並定義數字儲存被抽到的專案在集合中的號碼
            Random random = new Random();
            byte projectnum = Convert.ToByte(random.Next(assignporject.Count));
            //將該專案放入及小組集合一同放入班級集合中並將被抽出專案移除於專案集合
            _class.Add($"{assignporject.ToArray()[projectnum]},{string.Join(",", team)}");
            assignporject.RemoveAt(projectnum);
            //清空小組集合
            team.Clear();
        }
        /// <summary>
        /// 亂數將專案及小組分配為4組,回傳值為第一項資料為專案,後續為組員的集合
        /// </summary>
        /// <param name="assignpeople">當前班級所有人的集合</param>
        /// <param name="assignporject">當前所有專案的集合</param>
        /// <param name="Pri">傳入Session中的權限值,認證用</param>
        /// <returns></returns>
        public List<string> RandomAssign(List<string> assignpeople, List<string> assignporject, string Pri)
        {
            //判斷身分是否為管理者,不是則回傳null
            if (Pri != "Manager")
                return null;
            //定義亂數種子並定義"小組集合","班級集合"
            Random random = new Random();
            List<string> team = new List<string>();
            List<string> _class = new List<string>();
            //嘗試抓取錯誤,有錯誤即回傳null及錯誤訊息
            try
            {
                //若班級人數為4的倍數
                if (assignpeople.Count % 4 == 0)
                {
                    //定義數字計算每組人數
                    ushort peoplenum = Convert.ToUInt16(assignpeople.Count / 4);
                    //重複執行分配直到4組完成
                    do
                    {
                        //分配人員
                        AssignMember(assignpeople, team);
                        //若小組內人數到達每組所需人數則分配專案並加入班級
                        if (team.Count % peoplenum == 0)
                            AssignProject_AssignTeam(assignporject, team, _class);
                    } while (_class.Count != 4);
                    //回傳班級集合
                    return _class;
                }
                //若班級人數不是4的倍數
                else
                {
                    //定義數字計算多出的人數及每組人數
                    ushort peoplelonly = Convert.ToUInt16(assignpeople.Count % 4);
                    ushort peoplenum = Convert.ToUInt16((assignpeople.Count - (peoplelonly)) / 4);
                    //重複執行分配直到4組完成
                    do
                    {
                        //分配人員
                        AssignMember(assignpeople, team);
                        //若小組內人數到達每組所需人數
                        if (team.Count % peoplenum == 0)
                        {
                            //若多出的人數不為0
                            if (peoplelonly != 0)
                            {
                                //先亂數決定是否在這組多加一人
                                if (random.Next(2) == 0)
                                {
                                    //多出的人數-1,再抽一人加入該組並分配專案及加入班級
                                    peoplelonly--;
                                    AssignMember(assignpeople, team);
                                    AssignProject_AssignTeam(assignporject, team, _class);
                                }
                                //若多出的人數已經等於剩餘的組數了
                                else if (peoplelonly == 4 - _class.Count)
                                {
                                    //多出的人數-1,再抽一人加入該組並分配專案及加入班級
                                    peoplelonly--;
                                    AssignMember(assignpeople, team);
                                    AssignProject_AssignTeam(assignporject, team, _class);
                                }
                                else
                                    //其餘則直接分配專案並加入班級
                                    AssignProject_AssignTeam(assignporject, team, _class);
                            }
                            else
                                //其餘則直接分配專案並加入班級
                                AssignProject_AssignTeam(assignporject, team, _class);
                        }
                    } while (_class.Count != 4);
                    //回傳班級集合
                    return _class;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"請聯繫開發人員。錯誤訊息：{ex}");
                return null;
            }
        }





    }
}