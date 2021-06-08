using ProjectImmediateReply.Log;
using System;
using System.Collections.Generic;
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
            //定義數字每16位為一組授權碼 一個byte可放16位數字
            byte licence_cell;
            //licencelist
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
                    //將亂數取得的0~9之數字給licence_cell再放入licencelist集合中
                    licence_cell = Convert.ToByte(random.Next(0, 10));
                    licencelist.Add(licence_cell);
                    //計數器+1
                    counter++;
                    //counter記數到達16就將licencelist合併成為一組授權碼
                    if (counter % 16 == 0)
                    {
                        counter = 0;//計數器歸零
                        //將16位數字集合連接成一組授權碼放入licence字串集合
                        licence.Add(string.Join("", licencelist));
                        //檢查授權碼集合內是否有重複的授權碼,若有則將當前授權碼移除重新創建一組
                        //如果i小於當前授權碼數量則進入此迴圈
                        for (int i = 0; i < licence.Count; i++)
                        {
                            //j=i+1為授權碼在只有一組的狀況下不會進入此迴圈
                            for (int j = i + 1; j < licence.Count; j++)
                            {
                                //如果當前授權碼與先前產生之授權碼重複則進入此迴圈
                                if (licence[i] == licence[j])
                                {
                                    isrepeat = true;
                                    licence.RemoveAt(j);
                                    break;
                                }
                            }
                            //偵測到重覆則跳出迴圈
                            if (isrepeat)
                                break;
                        }
                        //16位分開之數字授權碼集合清空
                        licencelist.Clear();
                    }
                  //當授權碼數量等於使用者輸入之需要數量後就停止並回傳授權碼集合
                } while (licence.Count != num);
                return licence;
            }
            catch (Exception ex)
            {
                txtLog logtool = new txtLog();
                logtool.WriteLog(ex.ToString());
                throw;
            }

        }


        /// <summary>
        /// 將隨機人員加入小組集合
        /// </summary>
        /// <param name="assignpeople">當前剩餘人員之集合</param>
        /// <param name="team">當前未滿人之小組集合</param>
        private void AssignMember(List<string> assignpeople, List<string> team)
        {
            //定義亂數種子並定義數字儲存被抽到的人員在集合中的號碼
            Random random = new Random();
            //設定每個人的亂數數字 數字為0~人數-1
            byte Team_cell = Convert.ToByte(random.Next(0, assignpeople.Count));
            //將該人員放入小組集合team中並將其移除於人員集合
            team.Add(assignpeople.ToArray()[Team_cell]);
            //將該數字剔除
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
            //隨機設定一個數字給專案
            byte projectnum = Convert.ToByte(random.Next(assignporject.Count));
            //將該專案對應數字及小組集合一同放入班級集合中
            _class.Add($"{assignporject.ToArray()[projectnum]},{string.Join(",", team)}");
            //將被抽出專案移除於專案集合
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
                //若班級人數為4的倍數進入此判斷
                if (assignpeople.Count % 4 == 0)
                {
                    //定義數字計算每組人數為人數/4
                    ushort peoplenum = Convert.ToUInt16(assignpeople.Count / 4);
                    //重複執行分配直到4組完成
                    do
                    {
                        //分配人員
                        AssignMember(assignpeople, team);
                        //若小組內人數到達每組所需人數則分配專案並加入班級
                        if (team.Count % peoplenum == 0)
                            AssignProject_AssignTeam(assignporject, team, _class);
                      //直到滿4組才跳出迴圈
                    } while (_class.Count != 4);
                    //回傳班級集合
                    return _class;
                }
                //若班級人數不是4的倍數
                else
                {
                    //定義數字計算多出的人數
                    ushort peoplelonly = Convert.ToUInt16(assignpeople.Count % 4);
                    //定義每組人數至少幾人
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
                                //先亂數決定是否在這組多加一人 如果亂數為0則進入此迴圈
                                if (random.Next(2) == 0)
                                {
                                    //多出的人數-1,再抽一人加入該組,分配專案及加入班級
                                    peoplelonly--;
                                    AssignMember(assignpeople, team);
                                    AssignProject_AssignTeam(assignporject, team, _class);
                                }
                                //若多出的人數已經等於剩餘的組數
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