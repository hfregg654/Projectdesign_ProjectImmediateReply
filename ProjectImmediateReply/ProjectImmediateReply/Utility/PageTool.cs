﻿using ProjectImmediateReply.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Utility
{
    public class PageTool
    {
        public List<string> GetClassNumber()
        {
            DBTool dbtool = new DBTool();
            string[] colname = { "ClassNumber" };
            DataTable classnumber = dbtool.readTable("Users", colname, "GROUP BY ClassNumber", null, null);
            List<string> classnum = new List<string>();
            foreach (DataRow item in classnumber.Rows) //Rows表一列
            {
                if (item != null && item[0].ToString() != "")
                {
                    classnum.Add(item[0].ToString());
                }

            }
            return classnum;
        }
        public string GetClassNumberJS(List<string> classnumstring)
        {
            List<string> getchooseitem = new List<string>();
            foreach (string item in classnumstring) //每個item分別是班別100-1,100-2...
            {
                string newitem = $"'{item}'"; // item => '100-1','100-2',...
                getchooseitem.Add(newitem);
            }
            string chooseitem = string.Join(",", getchooseitem); // chooseitem =>  「'100-1','100-2',...」

			return chooseitem;
        }

        public string PageLeft(string PageType)
        {
            if (PageType == "Manager")
            {
                return @"<v-list two-line>
								<v-list-item @click="""" href =""/Index.aspx?PageInnerType=UpdateInfo"" >
									<v-list-item-icon >
										<v-icon color = ""primary"" > account_circle </v-icon >
   
									   </v-list-item-icon >
   

									   <v-list-item-content >
   
										   <v-list-item-title class=""chinese h4 primary--text"">個人資料維護</v-list-item-title>
										<v-list-item-subtitle>UpdateInformation</v-list-item-subtitle>
									</v-list-item-content>

								</v-list-item>


								
								<v-list-item @click = """" href =""/Index.aspx?PageInnerType=CreateClass"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">建立班級及修改</v-list-item-title>
										<v-list-item-subtitle>CreateClass</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""/Index.aspx?PageInnerType=CreateProject"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">建立專案及修改</v-list-item-title>
										<v-list-item-subtitle>CreateProject</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""/Index.aspx?PageInnerType="" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon>
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">小組分配及修改</v-list-item-title>
										<v-list-item-subtitle>TeamAssign</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>
								
								<v-list-item @click = """" href =""/Index.aspx?PageInnerType="" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > preview </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">成績</v-list-item-title>
										<v-list-item-subtitle>Grade</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>


							</v-list>";
            }
            else if (PageType == "Grades")
            {
                return @"<v-list two-line>
								<v-list-item @click="""" href=""/Index.aspx?PageInnerType=UpdateInfo"" >
									<v-list-item-icon >
										<v-icon color=""primary"" > account_circle </v-icon >
   
									   </v-list-item-icon >
   

									   <v-list-item-content >
   
										   <v-list-item-title class=""chinese h4 primary--text"" > 個人資料維護</v-list-item-title>
										<v-list-item-subtitle>UpdateInformation</v-list-item-subtitle>
									</v-list-item-content>

								</v-list-item>



								<v-list-item @click = """" href =""/Index.aspx?PageInnerType=GradesCrud"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > check_circle_outline </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" > 專案評分</v-list-item-title>
										<v-list-item-subtitle>Grades</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""/Index.aspx?PageInnerType="" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > preview </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" > 成績</v-list-item-title>
										<v-list-item-subtitle>Grade</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>


							</v-list>";
            }
            else if (PageType == "User" || PageType == "Leader")
            {
                return @"<v-list two-line>
								<v-list-item @click="""" href=""#1"" >
									<v-list-item-icon >
										<v-icon color=""primary"" > face </v-icon >
   
									   </v-list-item-icon >
   

									   <v-list-item-content >
   
										   <v-list-item-title class=""chinese h4 primary--text"" > 個人資料維護</v-list-item-title>
										<v-list-item-subtitle>Mobile</v-list-item-subtitle>
									</v-list-item-content>

								</v-list-item>



								<v-list-item @click = """" href =""#2"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > thumbs_up_down </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" > 專案評分</v-list-item-title>
										<v-list-item-subtitle>Personal</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""#3"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > receipt </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" > 成績</v-list-item-title>
										<v-list-item-subtitle>Work</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>


							</v-list>";
            }
            else
                return string.Empty;
        }
        public string PageRight(string PageInner)
        {
            if (PageInner == "GradesCrud")
            {
				string headeritem = " {text: '專案名',align: 'start',sortable: true,value: 'ProjectName'},{text: '組長',value: 'LeaderName'},{text: '組員',value: 'MemberName'},{text: '組名',value: 'TeamName'},{text: '',value: 'btn',sortable: false}";
                string chooseitem = GetClassNumberJS(GetClassNumber());
				string otherdata = " classchoice: \"\",";
                return $@"
                        <script>
                             var vm = new Vue({{
                                    el: '#app',
                                    vuetify: new Vuetify(),
                                    data: () => ({{
										drawer: null,
									    chooseitem: [{chooseitem}],
										headers: [{headeritem}],
										inneritem: [],
										{otherdata}
									 }}),
									 methods: {{
										changeRoute() {{
											axios
												.post('API/GetCrudHandler.ashx',{{innertype:'GradesCrud',classchoice:vm.classchoice}})
												.then(response => (this.inneritem = response.data))
												.catch(function(error) {{ 
												alert(error);
												}});
										}}
									 }},
							 }})
						 </script>";
            }
            else if (PageInner == "CreateClass")
            {
                return @"
						<script>
                              new Vue({
                                     el: '#app',
                                     vuetify: new Vuetify(),
                                     data: () => ({
                                     drawer: null,
                                     ClassNumber: """",
								     valid: true,
                                     License: """",
                                     numberrules: [
                                           value => !!value || '此輸入框需輸入2位數以內數字且不可為空白',
                                           value => (value || '').length <= 2 || '請輸入2位數以內'
                                     ],
                                     classrules: [
                                           value => !!value || '此輸入框不可為空白',
                                           value => (value || '').indexOf(' ') < 0 || '此輸入框不可輸入空白',
										   value => (value || '').length <= 10 || '請輸入10個字以內'
                                     ],
                                           
                                     }),
                               })
                         </script>";
            }
            else if (PageInner == "CreateProject")
            {
                string getclass =GetClassNumberJS(GetClassNumber());
                return $@"
						<script>
                            var vm = new Vue({{
                                     el: '#app',
                                     vuetify: new Vuetify(),
                                     data: () => ({{
										drawer: null,
										chooseclass: [{getclass}],
										rules1: [
											value => !!value || '此輸入框不可為空白',
											value => (value || '').length <= 20 || '請輸入20個字元以內',
										],
										classrules: [
											value => !!value || '此輸入框不可為空白',
										],
										date: new Date().toISOString().substr(0, 10),
										menu2: false,
										classchoice: """",
										C3projectname: """",
										date: """",
										valid: true,
                                     }}),
									methods: {{
										submit() {{
											if (this.$refs.form.validate()) {{
												axios.post('/user', {{
													classchoice: this.classchoice,
													C3projectname: this.C3projectname,
													date: this.date,
												}})
												.then(response => {{
													console.log(response);
													alert(""發送成功"");
												}})
												.catch (error => {{
													console.log(error);
													alert(""發送失敗可能是ＰＯＳＴ路徑問題"");
												}});
											}}
										}},
									}}
                              }})
						</script >";
            }
            else if (PageInner == "UpdateInfo")
            {
                DBTool Dbtool = new DBTool();
                string[] colcheckname = { "Name", "Phone", "Mail", "LineID", "License" };
                string[] colchecknamep = { "@UserID" };
                LogInfo info = (LogInfo)HttpContext.Current.Session["IsLogined"];
                string[] checkp = { info.UserID.ToString() };
                List<UserInfo> checkdata = Dbtool.ChangeTypeUserInfo(Dbtool.readTable("Users", colcheckname, "WHERE UserID=@UserID", colchecknamep, checkp));
                UserInfo userdata = new UserInfo();
                if (checkdata.Count != 0)
                {
                    userdata = checkdata[0];
                }
                return $@"
						<script>
                            new Vue({{
                                     el: '#app',
                                     vuetify: new Vuetify(),
                                     data: () => ({{
										drawer: null,
										valid: true, 
										C1name: ""{userdata.Name}"",
										C1phone: ""{userdata.Phone}"",
										C1email: ""{userdata.Mail}"",
										C1lineid: ""{userdata.LineID}"",
										C1password: """",
										C1newpassword: """",
										C1newpasswordconfirm: """",
										show1:false,
										show2:false,
										show3:false,				
                                     }}),
                                     methods: {{
										validate () {{
											if (this.$refs.form.validate()) {{
												axios.post('/123', {{
													classchoice:this.classchoice,
													people:this.people,
												}})
												.then(response => {{
													console.log(response);
													alert(""發送成功"");
												}})
												.catch (error => {{
													console.log(error);
													alert(""發送失敗可能是ＰＯＳＴ路徑問題"");
												}});
											}}
										}},
                                      
                                     }}
                            }})
						</script > ";
            }
            else
            {
                return @"
                       <script>
                            new Vue({
				                     el: '#app',
				                     vuetify: new Vuetify(),
				                     data: () => ({
					                  drawer: null,
				                      }),
			                       })
                        </script>";
            }

        }
    }
}