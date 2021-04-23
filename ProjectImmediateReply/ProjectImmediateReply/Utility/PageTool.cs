using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectImmediateReply.Utility
{
    public class PageTool
    {
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



								<v-list-item @click = """" href =""/Index.aspx?PageInnerType=Crud"" >
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
            if (PageInner == "Crud")
            {
                return @"
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
										   projectname: 'A計畫',
									       teamleader: '毛豆',
										   teammember: '一二三四五六七',
									       teamname: '第一組',
										   btn: '1%',
									 },],
                    
									 }),
							    })
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
                                           value => (value || '').length <= 2 || '請輸入2位數以內',
                                     ],
                                     classrules: [
                                           value => !!value || '此輸入框不可為空白',
                                           value => (value || '').indexOf(' ') < 0 || '此輸入框不可輸入空白',
                                     ],
                                           
                                     }),
                               })
                         </script>";
            }
            else if (PageInner == "CreateProject")
            {
                return @"
						 <script>
                              new Vue({
                                     el: '#app',
                                     vuetify: new Vuetify(),
                                     data: () => ({
                                     drawer: null,
                                     rules1: [
                                     value => !!value || '此輸入框不可為空白',
                                     value => (value || '').length <= 20 || '請輸入20個字元以內',
                                     ],
                                     rules2: [
                                     value => !!value || '此輸入框不可為空白',
                                     value => (value || '').length <= 20 || '請輸入20個字元以內',
                                     ],
                                     date: new Date().toISOString().substr(0, 10),
                                     }),
                               })
                         </script>";
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