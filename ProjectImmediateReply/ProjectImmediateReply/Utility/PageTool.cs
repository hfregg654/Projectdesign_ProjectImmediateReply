using ProjectImmediateReply.Models;
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
            //ascx才要寫到這裡插入
            //href =""./Index.aspx?PageInnerType=UpdateInfo"" +. 從根目錄開始抓
            //轉跳的網頁網址 => href = "" / Index.aspx ? PageInnerType = UpdateInfo"" >
            if (PageType == "Manager")
            {
                return @"<v-list two-line>
								<v-list-item @click="""" href =""./Index.aspx?PageInnerType=UpdateInfo"" >
									<v-list-item-icon >
										<v-icon color = ""primary"" > account_circle </v-icon >
   
									   </v-list-item-icon >
   

									   <v-list-item-content >
   
										   <v-list-item-title class=""chinese h4 primary--text"">個人資料維護</v-list-item-title>
										<v-list-item-subtitle>UpdateInformation</v-list-item-subtitle>
									</v-list-item-content>

								</v-list-item>


								
								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=CreateClass"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">建立班級及修改</v-list-item-title>
										<v-list-item-subtitle>CreateClass</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=CreateProject"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">建立專案及修改</v-list-item-title>
										<v-list-item-subtitle>CreateProject</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=AssignTeam"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon>
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"">小組分配及修改</v-list-item-title>
										<v-list-item-subtitle>TeamAssign</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>
								
								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=SeeGrade"" >
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
								<v-list-item @click="""" href=""./Index.aspx?PageInnerType=UpdateInfo"" >
									<v-list-item-icon >
										<v-icon color=""primary"" > account_circle </v-icon >
   
									   </v-list-item-icon >
   

									   <v-list-item-content >
   
										   <v-list-item-title class=""chinese h4 primary--text"" >個人資料維護</v-list-item-title>
										<v-list-item-subtitle>UpdateInformation</v-list-item-subtitle>
									</v-list-item-content>

								</v-list-item>



								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=GradesCrud"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > check_circle_outline </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" >專案評分</v-list-item-title>
										<v-list-item-subtitle>Grades</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=SeeGrade"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > preview </v-icon >
									</v-list-item-icon >

									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" >成績</v-list-item-title>
										<v-list-item-subtitle>Grade</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>


							</v-list>";
            }
            else if (PageType == "User" || PageType == "Leader")
            {
                return @"<v-list two-line>
								<v-list-item @click="""" href=""./Index.aspx?PageInnerType=UpdateInfo"" >
									<v-list-item-icon >
										<v-icon color=""primary"" > account_circle </v-icon >
									</v-list-item-icon >
									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" > 個人資料維護</v-list-item-title>
										<v-list-item-subtitle>UpdateInformation</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>



								<v-list-item @click = """" href =""#2"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon >
									</v-list-item-icon >
									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" >專案建置</v-list-item-title>
										<v-list-item-subtitle>Project</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>



								<v-list-item @click = """" href =""./ManageProject.aspx"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > build_circle </v-icon >
									</v-list-item-icon >
									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" >專案管理</v-list-item-title>
										<v-list-item-subtitle>Project</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>



								<v-list-item @click = """" href =""./Index.aspx?PageInnerType=SeeGrade"" >
									<v-list-item-icon>
										<v-icon color = ""primary"" > preview </v-icon >
									</v-list-item-icon >
									<v-list-item-content >
										<v-list-item-title class=""chinese h4 primary--text"" >成績</v-list-item-title>
										<v-list-item-subtitle>Grade</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>


							</v-list>";
            }
            else
                return string.Empty;
        }
        //then 等同於ajax的done
        //catch 等同於ajax的fail
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
										page: 1,
										pageCount: 0,
										itemsPerPage: 10,
										inneritem: [],
										{otherdata}
										headers: [{{
											text: '專案',
											align: 'start',
											value: 'ProjectName',
										}},
										{{
											text: '組長',
											value: 'LeaderName'
										}},
										{{
											text: '組員',
											value: 'MemberName'
										}},
										{{
											text: '組名',
											value: 'TeamName',
										}},
										{{
											text: '',
											value: 'ProjectID',
											sortable: false
										}},
									 ],
									 }}),
									 created() {{
									 	this.initialize()
									 }},
									 methods: {{
										changeRoute() {{
											axios
												.post('API/GetCrudHandler.ashx',{{innertype:'GradesCrud',classchoice:vm.classchoice}})
												.then(response => (this.inneritem = response.data))
												.catch(function(error) {{ 
												alert(error);
												}});
										}},
										onButtonClick(item) {{
											window.location.href='./ProjectDetail_Grades.aspx?ProjectID='+item.ProjectID;
										}},
										initialize() {{
											this.inneritem = [];
										}},
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
            else if (PageInner == "AssignTeam")
            {
				//刪除data:() 裡的 TeamName 防止跟ForAssignTeam的變數名稱相撞
                string chooseclass = GetClassNumberJS(GetClassNumber());
                return $@"
						<script>
                              var vm = new Vue({{
                                     el: '#app',
                                     vuetify: new Vuetify(),
                                     data: () => ({{
										drawer: null,
										chooseclass: [{chooseclass}],
										classchoice:"""",
										choosegroup:[],
										page: 1,
										pageCount: 0,
										itemsPerPage: 4,
										dialog: false,
										inneritem: [],
										editedIndex: -1,
										headers: [{{
												text: '姓名',
												align: 'start',
												value: 'Name',
										}},
										{{
												text: '組別',
												value: 'TeamID'
										}},
										{{
												text: '專案名',
												value: 'ProjectName'
										}},
										{{
												text: '小組名',
												value: 'choosegroup',
												sortable: false
										}},
										],

									}}),
									watch: {{
										dialog(val) {{
											val || this.close()
										}},
									}},
									created() {{
										this.changeRoute();
									}},
									mounted() {{
										$(""#savebtn"").hide();
										$(""#randombtn"").hide();
										$(""#leaderp"").hide();
									}},
									methods: {{
											changeRoute() {{
												axios
													.post('API/GetCrudHandler.ashx',{{innertype:'AssignTeam',classchoice:vm.classchoice}})
													.then(response => {{
															vm.page=1;
															vm.inneritem = response.data;
															$(""#savebtn"").show();
															if(vm.inneritem[0].TeamID==0){{
																$(""#randombtn"").show();
																$(""#leaderp"").hide();
															}}else{{
																$(""#randombtn"").hide();
																$(""#leaderp"").show();
															}}
													}})
													.catch(function(error) {{ 
													alert(error);
													}});
											}},
											randam(){{
												if(confirm('請確認資料正確性,分組完成後無法再次分組,確定開始進行亂數分組?')){{
													axios.post('API/AssignTeamHandler.ashx',{{
														Type:""RandomAssign"",
														classchoice:vm.classchoice,
														inneritem:vm.inneritem,
													}})
													  .then(response => {{
															this.changeRoute();
															if(response.data.success){{
																alert('小組亂數分配失敗,請檢查資料正確性(是否有4個專案等等)');
															}}else{{
																alert('小組亂數分配完成');
															}}
													  }})
													  .catch(error => {{
													    alert('小組亂數分配失敗');
													  }});
												}}
											}},
											store(){{
												if(confirm('確定儲存更改的小組結果?(注意：組長無法更改組別)')){{
													axios.post('API/AssignTeamHandler.ashx', {{
														Type:""SaveTeamChange"",
														classchoice:vm.classchoice,
														inneritem:vm.inneritem
													  }})
													  .then(response =>{{
														  this.changeRoute();
														  if(response.data.Type==""Wrong""){{
															  alert('儲存成功,但部分無法更改組別,請確認是否更改到組長');
														  }}else{{
															  alert('儲存成功');
														  }}
													  }})
													  .catch(error => {{
													    alert('儲存發送失敗');
													  }});
												}}
											}},
										initialize() {{
											
										}},
									}},
								}})
						</script>";
            }
            else if (PageInner == "CreateProject")
            {
                string getclass = GetClassNumberJS(GetClassNumber());
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
                              }})
						</script >";
            }
            //沒動到後端資料庫的話，可將方法寫在PageTool.cs
            else if (PageInner == "UpdateInfo")
            {
                DBTool Dbtool = new DBTool();
                string[] colcheckname = { "Name", "Phone", "Mail", "LineID" };
                string[] colchecknamep = { "@UserID" };
                LogInfo info = (LogInfo)HttpContext.Current.Session["IsLogined"];
                string[] checkp = { info.UserID.ToString() };
                List<UserInfo> checkdata = Dbtool.ChangeTypeUserInfo(Dbtool.readTable("Users", colcheckname, "WHERE UserID=@UserID", colchecknamep, checkp));
                UserInfo userdata = new UserInfo();
                if (checkdata.Count != 0)
                {
                    userdata = checkdata[0];
                }
                //一格一格的值""{userdata.Name}"" 一列一列[{ chooseitem}]
                //console.log(response); 顯示傳回來的值 確認到哪一個判斷狀態 於網頁上 F12  
                if (info.Privilege == "Manager")
                {
                    return $@"
						<script>
                            var vm = new Vue({{
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
										license: ""{info.License}"",
										show1:false,
										show2:false,
										show3:false,				
                                     }}),
                                     methods: {{
										validate () {{
											if (this.$refs.form.validate()) {{
												axios.post('API/UpdateInfoHandler.ashx', {{
													C1name:vm.C1name,
													C1phone:vm.C1phone,
													C1email:vm.C1email,
													C1lineid:vm.C1lineid,
													C1password:vm.C1password,
													C1newpassword:vm.C1newpassword,
													C1newpasswordconfirm:vm.C1newpasswordconfirm,
													license:vm.license,
													oldname:""{info.Name}""
												}})
												.then(response => {{
													if(response.data[0].success== ""success""){{
														console.log(response); 
														alert(""更新完成"");
													}}
													else if(response.data[0].success== ""Empty""){{
														console.log(response); 
														alert(""原資料不可輸入空白"");
													}}
													else if(response.data[0].success== ""pwdwrong""){{
														console.log(response); 
														alert(""原密碼輸入錯誤"");
													}}
													else if(response.data[0].success== ""pwdmiss""){{
														console.log(response); 
														alert(""請將密碼欄位填寫完整"");
													}}
													else{{
														console.log(response); 
														alert(""更新失敗,請檢查新密碼輸入欄位"");
													}}
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
                    return $@"
						<script>
							$(""#specialaccountbtn"").hide();
                            var vm = new Vue({{
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
										license: ""{info.License}"",
										show1:false,
										show2:false,
										show3:false,				
                                     }}),
                                     methods: {{
										validate () {{
											if (this.$refs.form.validate()) {{
												axios.post('API/UpdateInfoHandler.ashx', {{
													C1name:vm.C1name,
													C1phone:vm.C1phone,
													C1email:vm.C1email,
													C1lineid:vm.C1lineid,
													C1password:vm.C1password,
													C1newpassword:vm.C1newpassword,
													C1newpasswordconfirm:vm.C1newpasswordconfirm,
													license:vm.license,
													oldname:""{info.Name}""
												}})
												.then(response => {{
													if(response.data[0].success== ""success""){{
														console.log(response); 
														alert(""更新完成"");
													}}
													else if(response.data[0].success== ""Empty""){{
														console.log(response); 
														alert(""原資料不可輸入空白"");
													}}
													else if(response.data[0].success== ""pwdwrong""){{
														console.log(response); 
														alert(""原密碼輸入錯誤"");
													}}
													else if(response.data[0].success== ""pwdmiss""){{
														console.log(response); 
														alert(""請將密碼欄位填寫完整"");
													}}
													else{{
														console.log(response); 
														alert(""更新失敗,請檢查新密碼輸入欄位"");
													}}
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

            }
            else if (PageInner == "SeeGrade")
            {// changeRoute方法名稱 修改
                string chooseitem = GetClassNumberJS(GetClassNumber());
                LogInfo info = (LogInfo)HttpContext.Current.Session["IsLogined"];
                if (info.Privilege == "Grades")
                {
                    return $@"
								<script>
								$(""#Showgradesandmanager"").show();
								$(""#Showuserandleader"").hide();
								$(""#chagegrade"").hide();
		                        var vm = new Vue({{
		                                  el: '#app',
		                                  vuetify: new Vuetify(),
		                                  data: () => ({{
												drawer: null,
												valid: true,
												chooseclass: [{chooseitem}],
												choosegroup: [],
												choosename: [],
												rules1: [
													value => !!value || '此輸入框不可為空白',
												],
												classrules: [
													value => !!value || '此輸入框不可為空白',
												],
												classchoice: """",
												group: """",
												name: """",
												email:"""",
												score: """",
												boss: """",
												pm: """",
												panel:[],
		                                  }}),
		                                  methods: {{
												changeRoutechooseclass() {{
													$(""#chagegrade"").hide();
													vm.group="""";
													vm.name="""";
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:vm.classchoice, TeamName:vm.group, Name:vm.name}})
														.then(response => {{
															if(response.data[0].choosegroup){{
																vm.choosegroup=response.data[0].choosegroup.split(',');
															}}
															else{{
																vm.choosegroup=[];
															}}

														}})
														.catch(function(error) {{ 
															alert(error);
														}})
												}},
												changeRoutechoosegroup() {{
													$(""#chagegrade"").hide();
													vm.name="""";
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:vm.classchoice, TeamName:vm.group, Name:vm.name}})
														.then(response => {{
															if(response.data){{
																vm.choosename=response.data;
															}}
															else{{
																vm.choosename=[];
															}}
														}})
														.catch(function(error) {{ 
															alert(error);
														}})
												}},
												changeRoutechoosename() {{
													$(""#chagegrade"").show();
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:vm.classchoice, TeamName:vm.group, Name:vm.name}})
														.then(response => {{
															if(response.data.Grade!=0){{
																vm.email=response.data.Mail;
																vm.score=response.data.Grade+""分"";
																vm.boss=response.data.PresidentComments;
																vm.pm=response.data.PMComments;
															}}
															else{{
																vm.email=response.data.Mail;
																vm.score=""無"";
																vm.boss=""未評分"";
																vm.pm=""未評分"";
															}}
														}})
														.catch(function(error) {{ 
															alert(error);
														}});
												}}
											 }},
		                         }})
								</script>
								<style type=""text/css"" scoped>
									.v-text-field.v-text-field--enclosed.v-text-field__details, 
									.v-text-field.v-text-field--enclosed > .v-input__control > .v-input__slot {{
										margin: 0;
										max-height: 50px;
										min-height: auto!important;
										display: flex!important;
										align-items: center!important
									}}
								</style > ";
                }
                else if (info.Privilege == "Manager")
                {
                    return $@"
								<script>
								$(""#Showgradesandmanager"").show();
								$(""#Showuserandleader"").hide();
								$(""#chagegrade"").hide();
		                        var vm = new Vue({{
		                                  el: '#app',
		                                  vuetify: new Vuetify(),
		                                  data: () => ({{
												drawer: null,
												valid: true,
												chooseclass: [{chooseitem}],
												choosegroup: [],
												choosename: [],
												rules1: [
													value => !!value || '此輸入框不可為空白',
												],
												classrules: [
													value => !!value || '此輸入框不可為空白',
												],
												classchoice: """",
												group: """",
												name: """",
												email:"""",
												score: """",
												boss: """",
												pm: """",
												panel:[],
		                                  }}),
		                                  methods: {{
												changeRoutechooseclass() {{
													vm.group="""";
													vm.name="""";
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:vm.classchoice, TeamName:vm.group, Name:vm.name}})
														.then(response => {{
															if(response.data[0].choosegroup){{
																vm.choosegroup=response.data[0].choosegroup.split(',');
															}}
															else{{
																vm.choosegroup=[];
															}}

														}})
														.catch(function(error) {{ 
															alert(error);
														}})
												}},
												changeRoutechoosegroup() {{
													vm.name="""";
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:vm.classchoice, TeamName:vm.group, Name:vm.name}})
														.then(response => {{
															if(response.data){{
																vm.choosename=response.data;
															}}
															else{{
																vm.choosename=[];
															}}
														}})
														.catch(function(error) {{ 
															alert(error);
														}})
												}},
												changeRoutechoosename() {{
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:vm.classchoice, TeamName:vm.group, Name:vm.name}})
														.then(response => {{
															if(response.data.Grade!=0){{

																vm.email=response.data.Mail;
																vm.score=response.data.Grade+""分"";
																vm.boss=response.data.PresidentComments;
																vm.pm=response.data.PMComments;
															}}
															else{{
																vm.email=response.data.Mail;
																vm.score=""無"";
																vm.boss=""未評分"";
																vm.pm=""未評分"";
															}}
														}})
														.catch(function(error) {{ 
															alert(error);
														}});
												}}
											 }},
		                         }})
								</script>
								<style type=""text/css"" scoped>
									.v-text-field.v-text-field--enclosed.v-text-field__details, 
									.v-text-field.v-text-field--enclosed > .v-input__control > .v-input__slot {{
										margin: 0;
										max-height: 50px;
										min-height: auto!important;
										display: flex!important;
										align-items: center!important
									}}
								</style > ";
                }
                else if (info.Privilege == "Leader" || info.Privilege == "User")
                {
                    DBTool dBTool = new DBTool();
                    string[] col = { "ClassNumber", "TeamName", "Account", "Name" };
                    DataTable dt = dBTool.readTable("Users", col, $"WHERE UserID={info.UserID} AND DeleteDate IS NULL AND WhoDelete IS NULL", null, null);
                    if (dt.Rows.Count == 0)
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
                    return $@"
								<script>
								$(""#Showgradesandmanager"").hide();
								$(""#Showuserandleader"").show();
								$(""#chagegrade"").hide();
		                        var vm = new Vue({{
		                                  el: '#app',
		                                  vuetify: new Vuetify(),
		                                  data: () => ({{
												drawer: null,
												valid: true,
												chooseclass: ""{dt.Rows[0]["ClassNumber"]}"",
												choosegroup: ""{dt.Rows[0]["TeamName"]}"",
												choosename: ""{dt.Rows[0]["Name"]}"",
												rules1: [
													value => !!value || '此輸入框不可為空白',
												],
												classrules: [
													value => !!value || '此輸入框不可為空白',
												],
												classchoice: ""{dt.Rows[0]["ClassNumber"]}"",
												group: ""{dt.Rows[0]["TeamName"]}"",
												name: ""{dt.Rows[0]["Name"]}"",
												email:"""",
												score: """",
												boss: """",
												pm: """",
												panel:[],
		                                  }}),
										  created() {{
												this.initialize()
										  }},
										  methods: {{
												initialize() {{
													axios
														.post('API/SeeGradeHandler.ashx',{{innerType:'SeeGrade', Privilege:'{info.Privilege}', ClassNumber:'{dt.Rows[0]["ClassNumber"]}', TeamName:'{dt.Rows[0]["TeamName"]}', Name:'{dt.Rows[0]["Account"]}'}})
														.then(response => {{
															if(response.data.Grade!=0){{
																vm.email=response.data.Mail;
																vm.score=response.data.Grade+""分"";
																vm.boss=response.data.PresidentComments;
																vm.pm=response.data.PMComments;
															}}
															else{{
																vm.email=response.data.Mail;
																vm.score=""無"";
																vm.boss=""未評分"";
																vm.pm=""未評分"";
															}}
														}})
														.catch(function(error) {{ 
															alert(error);
														}});
												}},
										  }},
		                         }})
								</script>
								<style type=""text/css"" scoped>
									.v-text-field.v-text-field--enclosed.v-text-field__details, 
									.v-text-field.v-text-field--enclosed > .v-input__control > .v-input__slot {{
										margin: 0;
										max-height: 50px;
										min-height: auto!important;
										display: flex!important;
										align-items: center!important
									}}
								</style > ";

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