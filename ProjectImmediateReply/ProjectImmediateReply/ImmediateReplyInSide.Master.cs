using ProjectImmediateReply.Models;
using System;
using System.Web.UI;

namespace ProjectImmediateReply
{
    public partial class ImmediateReplayInSide : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLogined"] != null) /*使用Session內建方法取得LoginHelper TryLogin的值*/
            {
                LogInfo Info = (LogInfo)Session["IsLogined"];
                HiddenFieldSessionPri.Value = Info.Privilege.ToString(); //為提取當前使用者權限的值，以便後續方法確認身分
                HiddenFieldSessionMail.Value = Info.Mail.ToString(); //提取當前使用者的Mail，以便後續方法寄信使用，密碼一類的請勿使用。
            }

        }
    }
}