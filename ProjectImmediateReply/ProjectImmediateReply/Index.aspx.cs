using ProjectImmediateReply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectImmediateReply
{
    public partial class Index1 : System.Web.UI.Page
    {
        private const string JSstringDefault = @"
<script>
    new Vue({
				el: '#app',
				vuetify: new Vuetify(),
				data: () => ({
					drawer: null,
				}),
			})
</script>";

        private const string JSstringCreateClass = @"<script>
                                    new Vue({
                                            el: '#app',
                                            vuetify: new Vuetify(),
                                            data: () => ({
                                            drawer: null,
                                            ClassNumber: """",
                                            valid: true,
                                            License: """",
                                            numberrules: [
                                                   value => !!value || '此輸入框需輸入數字且不可為空白',
                                                   value => (value || '').length <= 5 || '請輸入5個字元以內',

                                            ],
                                            classrules: [
                                                   value => !!value || '此輸入框不可為空白',
                                            ],
                                            }),
                                     
                                           
                                     })
                              </script>";
        private const string JSstringCrud = @"
<script>
            new Vue({
                el: '#app',
                vuetify: new Vuetify(),
                data: () => ({
                    drawer: null,
                    
                    chooseclass: ['班級A', '班級B', '班級C', '班級D'],
                    
                    headers: [{
                        text: '專案名稱',
                        align: 'start',
                        sortable: true,
                        value: 'projectname',
                    },
                    {
                        text: '組長',
                        value: 'teamleader'
                    },
                    {
                        text: '組員',
                        value: 'teammember'
                    },
                    {
                        text: '組名',
                        value: 'teamname'
                    },
                    {
                        text: '',
                        value: 'btn',
                        sortable: false
                    },
                    ],
                    inneritem: [{
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                    }, {
                        projectname: 'B計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                    }, {
                        projectname: 'C計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'D計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    },],
                    
                }),
            })
</script>";
        private const string _sessionKey = "PageInnerType";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLogined"] != null)
            {
                Models.LogInfo logInfo = new Models.LogInfo();
                logInfo=(Models.LogInfo)Session["IsLogined"];
                Label1.Text = logInfo.Privilege.ToString();
            }

            string PageInnerType = null;
            if (Request.QueryString[_sessionKey] != null)
            {
                PageInnerType = Request.QueryString[_sessionKey].ToString();
            }
            if (PageInnerType == "Crud")
            {
                ucCrud.Visible = true;
                divJS.InnerHtml = JSstringCrud;
            }
            else if (PageInnerType == "CreateClass")
            {
                ucCreateClass.Visible = true;
                divJS.InnerHtml = JSstringCreateClass;
            }
            else if (PageInnerType == "UpdateInfo")
            {
                ucUpdateInfo.Visible = true;
                divJS.InnerHtml = JSstringDefault;
            }
            else
            {
                divinnerplace.InnerHtml = "<v-main></v-main>";
                divJS.InnerHtml = JSstringDefault;
            }

            Utility.LoginHelper logtool = new Utility.LoginHelper();
            if (!logtool.HasLogIned())
                Response.Redirect("~/LogIn.aspx");


        }
    }
}