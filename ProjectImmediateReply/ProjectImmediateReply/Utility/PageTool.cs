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
			//要找的DataTable欄位名稱
			string[] colname = { "ClassNumber" };
			//讀取Users表，第一個null為DB內參數欄位名稱，第二個為傳入比對之變數
			//GROUP BY會將相同的資料整合成一筆，防止清單資料重複
			DataTable classnumber = dbtool.readTable("Users", colname, "WHERE DeleteDate IS NULL AND WhoDelete IS NULL GROUP BY ClassNumber", null, null);
			//建立班級數量清單
            List<string> classnum = new List<string>();
			//Rows表一列 foreach跑一次
			foreach (DataRow item in classnumber.Rows) 
            {
				//資料表不為空的狀況進入判斷
                if (item != null && item[0].ToString() != "")
                {
					//將班級資料表依序加入classnum
					classnum.Add(item[0].ToString());
                }

            }
			//回傳加好的班級資料
            return classnum;
        }
		//js版本的獲得班級選單
        public string GetClassNumberJS(List<string> classnumstring)
        {
			//定義獲取班級的集合清單(分開的)
            List<string> getchooseitem = new List<string>();
            foreach (string item in classnumstring) //每個item分別是班別100-1,100-2...
            {
				//將讀表GROUP BY後的班級一個一個放入getchooseitem
				string newitem = $"'{item}'"; // item => '100-1','100-2',...
                getchooseitem.Add(newitem);
            }
			//將所有班級串成字串後回傳
            string chooseitem = string.Join(",", getchooseitem); // chooseitem =>  「'100-1','100-2',...」

            return chooseitem;
        }

        public string PageLeft(string PageType)
        {
			//ascx控制項才要寫到這裡插入
			//href =""./Index.aspx?PageInnerType=UpdateInfo"" +. 從根目錄開始抓
			//轉跳的網頁網址 => href = "" / Index.aspx ? PageInnerType = UpdateInfo"" >
			// 依照權限分別顯示左邊選單
			//如果PageType 是管理員則顯示以下左邊選單
			if (PageType == "Manager")
            {
				//<v-list-item> 整個包成一個A標籤
				//<v-list-item @click="""" href =""./Index.aspx?PageInnerType=UpdateInfo"" >   個人資料更新的超連結
				//<v-list-item-icon >  左選單的小圖 primary為圖案顏色 account_circle為抓取之圖片名稱
				//<v-list-item-content> 內容區塊 等同於div
				//<v-list-item-title> 為標籤連結顯示文字及字大小
				//<v-list-item-subtitle> 文字下方的小Title
				return @"<v-list two-line>
							<v-list-item @click="""" href =""./Index.aspx?PageInnerType=UpdateInfo"" >

								<v-list-item-icon >
									<v-icon color = ""primary"" >account_circle </v-icon >
								</v-list-item-icon >
   
								<v-list-item-content>
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
			//如果PageType 是評分者則顯示以下左邊選單
			else if (PageType == "Grades")
            {
				//<v-list-item> 整個包成一個A標籤
				//<v-list-item @click="""" href =""./Index.aspx?PageInnerType=UpdateInfo"" >   個人資料更新的超連結
				//<v-list-item-icon >  左選單的小圖 primary為圖案顏色 account_circle為抓取之圖片名稱
				//<v-list-item-content> 內容區塊 等同於div
				//<v-list-item-title> 為標籤連結顯示文字及字大小
				//<v-list-item-subtitle> 文字下方的小Title
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
			//如果PageType 是組長則顯示以下左邊選單
			else if (PageType == "Leader")
            {
				//<v-list-item> 整個包成一個A標籤
				//<v-list-item @click="""" href =""./Index.aspx?PageInnerType=UpdateInfo"" >   個人資料更新的超連結
				//<v-list-item-icon >  左選單的小圖 primary為圖案顏色 account_circle為抓取之圖片名稱
				//<v-list-item-content> 內容區塊 等同於div
				//<v-list-item-title> 為標籤連結顯示文字及字大小
				//<v-list-item-subtitle> 文字下方的小Title
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

							<v-list-item @click = """" href =""./CreateWorks.aspx"" >

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
			//如果PageType 是使用者則顯示以下左邊選單
			else if (PageType == "User")
            {
				//<v-list-item> 整個包成一個A標籤
				//<v-list-item @click="""" href =""./Index.aspx?PageInnerType=UpdateInfo"" >   個人資料更新的超連結
				//<v-list-item-icon >  左選單的小圖 primary為圖案顏色 account_circle為抓取之圖片名稱
				//<v-list-item-content> 內容區塊 等同於div
				//<v-list-item-title> 為標籤連結顯示文字及字大小
				//<v-list-item-subtitle> 文字下方的小Title
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
			//如果都不是則回傳空字串
            else
                return string.Empty;
        }
        //then 等同於ajax的done
        //catch 等同於ajax的fail
		//網頁的右邊頁面JS部分
        public string PageRight(string PageInner)
        {
			//評分者用 觀看所有專案
            if (PageInner == "GradesCrud")
            {
				//chooseitem 為帶入班級下拉式選單的值
				//classchoice為班級下拉式選單選擇的那一項值
				//找id 為app的dom 標籤 插入<script></script>的程式碼
				//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
				//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
				//drawer 左邊選單 null為預設開啟
				//page 預設第一頁 pageCount 數量預設為0  itemsPerPage 10筆為一頁
				//inneritem為泛型處理常式傳回來的資料
				//headers 內value分別帶入inneritem 對應的各項值
				//sortable為false表示關閉預設排序
				//created() 類似PageLoad事件 呼叫this.initialize() 在每次重新載入時清空全部值
				//methods 為程式區域 寫function的地方
				//changeRoute() 方法內 axios為vue.js內建的get post語法
				//post 將innertype及classchoice的值傳送到API/GetCrudHandler.ashx
				//then 將API/GetCrudHandler.ashx所傳回的值顯示在此頁面 類似ajax的done
				//catch 直接將錯誤訊息彈跳出來 類似ajax的fail
				//onButtonClick(item) 為專案的查看按鈕
				string chooseitem = GetClassNumberJS(GetClassNumber());
                return $@"
                        <script>
                             var vm = new Vue({{
                                    el: '#app',
                                    vuetify: new Vuetify(),
                                    data: () => ({{
										drawer: null,
									    chooseitem: [{chooseitem}],
										classchoice: """",
										page: 1,
										pageCount: 0,
										itemsPerPage: 10,
										inneritem: [],										
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
			//管理者用 建立班級
            else if (PageInner == "CreateClass")
            {
				//chooseitem 為帶入班級下拉式選單的值
				//classchoice為班級下拉式選單選擇的那一項值
				//找id 為app的dom 標籤 插入<script></script>的程式碼
				//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
				//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
				//drawer 左邊選單 null為預設開啟
				//numberrules 為輸入規則
				//classrules 為輸入規則
				//下方script純粹插入頁面效果
				//直接抓頁面的值與post值的程式碼在ImmediateReplyAJAX的"#CreatClassbtn"部分
				return @"
						<script>
                              new Vue({
                                     el: '#app',
                                     vuetify: new Vuetify(),
                                     data: () => ({
                                     drawer: null,
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
			// 管理者用 亂數分配專案小組
            else if (PageInner == "AssignTeam")
            {
				//已刪除 dialog: false  editedIndex: -1

				//chooseitem 為帶入班級下拉式選單的值
				//classchoice為班級下拉式選單選擇的那一項值
				//找id 為app的dom 標籤 插入<script></script>的程式碼
				//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
				//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
				//drawer 左邊選單 null為預設開啟
				//page 預設第一頁 pageCount 數量預設為0  itemsPerPage 4筆為一頁
				//inneritem為泛型處理常式傳回來的資料
				//headers 內value分別帶入inneritem 對應的各項值
				//sortable為false表示關閉預設排序
				//created() 類似PageLoad事件 呼叫this.initialize() 在每次重新載入時清空全部值
				//mounted() 類似Pageinit事件 在頁面載入前先隱藏按鈕savebtn&randombtn及訊息leaderp
				//methods 為程式區域 寫function的地方
				//changeRoute() 方法內 axios為vue.js內建的get post語法 選班級之後會顯示的資料
				//vm.page=1; 重設一次第一頁 讓按鈕觸發後回到第一頁
				//if(vm.inneritem[0].TeamID==0) 為如果傳回來的資料沒有TeamID則顯示亂數分配按鈕
				//else為如果已分好組則隱藏亂數分組按鈕 顯示組長為星號訊息
				//post 將innertype及classchoice的值傳送到API/GetCrudHandler.ashx
				//then 將API/GetCrudHandler.ashx所傳回的值顯示在此頁面 類似ajax的done
				//catch 直接將錯誤訊息彈跳出來 類似ajax的fail
				//random()為按下亂數分配按鈕後 將GetCrudHandler.ashx所傳回的值 再post到AssignTeamHandler.ashx做分組處理 axios為傳送的值
				//random()的then this.changeRoute()為將處理完的儲存的後端資料再次取得並顯示在頁面
				//如果資料處理失敗 則傳送錯誤訊息 反之顯示小組亂數分配成功
				//store()為按下儲存按鈕後 將GetCrudHandler.ashx所傳回的值 再post到AssignTeamHandler.ashx做分組處理 axios為傳送的值
				//store()的then this.changeRoute()為將處理完的儲存的後端資料再次取得並顯示在頁面
				//如果資料處理失敗 則傳送錯誤訊息 反之顯示儲存成功
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
										inneritem: [],										
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
											random(){{
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
									}},
								}})
						</script>";
            }
			// 管理者用建立專案
            else if (PageInner == "CreateProject")
            {
				//不必要的程式部分 date: """", 預設空白 valid: true

				//chooseclass 為帶入班級下拉式選單的值
				//classchoice 為班級下拉式選單選擇的那一項值
				//找id 為app的dom 標籤 插入<script></script>的程式碼
				//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
				//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
				//drawer 左邊選單 null為預設開啟
				//rules1 為輸入規則
				//classrules 為輸入規則
				//date為時程期限的預設值
				//menu2 為時程期限彈跳的日曆
				//C3projectname 為專案名稱輸入框
				//下方script純粹插入頁面效果
				//直接抓頁面的值與post值的程式碼在ImmediateReplyAJAX的"#CreateProjectbtn"部分
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
                                     }}),
                              }})
						</script >";
            }

            //沒動到後端資料庫的話，可將方法寫在PageTool.cs
			// 全部權限的個人資訊更新
            else if (PageInner == "UpdateInfo")
            {
				//以Session擷取到的UserID找到對應的個人資料
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
				//一格一格的值""{userdata.Name}"" 一列一列[{chooseitem}]
				//console.log(response); 顯示傳回來的值 確認到哪一個判斷狀態 於網頁上 F12

				////管理者的資料更新(沒有隱藏特別帳號建立按鈕)
				//找id 為app的dom 標籤 插入<script></script>的程式碼
				//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
				//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
				//drawer 左邊選單 null為預設開啟
				//valid 判斷輸入框輸入不正確的值時 儲存按鈕功能無法使用
				//C1name帶入找到的個人資料名 C1phone為手機號碼 C1email為Mail C1lineid為LineID C1password密碼部分皆預設為空
				//license 顯示授權碼於左下方
				//show1~3 為錯誤訊息顯示
				//validate () vue.js驗證方法
				//this.$refs.form.validate() 如果傳回來的值驗證通過後才進入傳後端值
				//axios.post 將下列值傳送到API/UpdateInfoHandler.ashx
				//then為資料在UpdateInfoHandler.ashx處理完後所顯示的各種訊息
				////管理者以外的資料更新與上述相同 差別在於有隱藏特別帳號建立按鈕
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
													userid:""{info.UserID}""
												}})
												.then(response => {{
													if(response.data[0].success== ""success""){{
														alert(""更新完成"");
														location.reload();
													}}
													else if(response.data[0].success== ""Empty""){{
														alert(""原資料不可輸入空白"");
													}}
													else if(response.data[0].success== ""pwdwrong""){{
														alert(""原密碼輸入錯誤"");
													}}
													else if(response.data[0].success== ""pwdmiss""){{
														alert(""請將密碼欄位填寫完整"");
													}}
													else{{
														alert(""更新失敗,請檢查新密碼輸入欄位"");
													}}
												}})
												.catch (error => {{
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
													userid:""{info.UserID}""
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
			// 全部權限的觀看成績
            else if (PageInner == "SeeGrade")
				//$(""#chagegrade"").hide(); 移除
			{   // changeRoute方法名稱 修改
				//獲得下拉式選單班級值
				string chooseitem = GetClassNumberJS(GetClassNumber());
				//獲得登入者的Session
                LogInfo info = (LogInfo)HttpContext.Current.Session["IsLogined"];
				//如果登入者的權限為Grades的話插入下列Script程式碼
				if (info.Privilege == "Grades")
                {
					//#Showgradesandmanager 顯示班級小組及個人的選擇框讓評分者選擇
					//#Showuserandleader 將固定顯示的選擇框隱藏
					//找id 為app的dom 標籤 插入<script></script>的程式碼
					//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
					//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
					//drawer 左邊選單 null為預設開啟
					//chooseclass 為帶入的班級選單值
					//choosegroup 可選擇的小組清單
					//choosename 可選擇的小組人員清單
					//rules1 及 classrules 為輸入規則
					//classchoice 為選到的班級值
					//group為選到的小組值
					//name為選到的小組成員值
					//email為選到的人的Email資訊
					//score為經評分者與PM分數打完計算後顯示的分數
					//boss 與 pm 分別顯示評語部分
					//panel為 boss與pm的版面設計部分
					//methods 為程式區域 寫function的地方
					//changeRoutechooseclass() 方法內 axios為vue.js內建的get post語法 選班級之後傳送到SeeGradeHandler處理 並回傳對應的班級小組資料供小組選單選擇
					//changeRoutechoosegroup() 方法內 axios為vue.js內建的get post語法 選小組之後傳送到SeeGradeHandler處理 並回傳對應的小組成員資料供成員選單選擇
					//changeRoutechoosename() 方法內 axios為vue.js內建的get post語法 選小組之後傳送到SeeGradeHandler處理 並回傳成員對應的成績郵件評語等資料供評分者閱覽
					//style type=""text/css"" scoped 為排版部分
					return $@"
								<script>
								$(""#Showgradesandmanager"").show();
								$(""#Showuserandleader"").hide();
		                        var vm = new Vue({{
		                                  el: '#app',
		                                  vuetify: new Vuetify(),
		                                  data: () => ({{
												drawer: null,
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
																vm.boss=""未評分完成"";
																vm.pm=""未評分完成"";
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
				//如果登入者的權限為Manager的話插入下列Script程式碼
				else if (info.Privilege == "Manager")
                {
					//#Showgradesandmanager 顯示班級小組及個人的選擇框讓管理者選擇
					//#Showuserandleader 將固定顯示的選擇框隱藏
					//找id 為app的dom 標籤 插入<script></script>的程式碼
					//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
					//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
					//drawer 左邊選單 null為預設開啟
					//chooseclass 為帶入的班級選單值
					//choosegroup 可選擇的小組清單
					//choosename 可選擇的小組人員清單
					//rules1 及 classrules 為輸入規則
					//classchoice 為選到的班級值
					//group為選到的小組值
					//name為選到的小組成員值
					//email為選到的人的Email資訊
					//score為經評分者與PM分數打完計算後顯示的分數
					//boss 與 pm 分別顯示評語部分
					//panel為 boss與pm的版面設計部分
					//methods 為程式區域 寫function的地方
					//changeRoutechooseclass() 方法內 axios為vue.js內建的get post語法 選班級之後傳送到SeeGradeHandler處理 並回傳對應的班級小組資料供小組選單選擇
					//changeRoutechoosegroup() 方法內 axios為vue.js內建的get post語法 選小組之後傳送到SeeGradeHandler處理 並回傳對應的小組成員資料供成員選單選擇
					//changeRoutechoosename() 方法內 axios為vue.js內建的get post語法 選小組之後傳送到SeeGradeHandler處理 並回傳成員對應的成績郵件評語等資料供管理者閱覽
					//style type=""text/css"" scoped 為排版部分
					return $@"
								<script>
								$(""#Showgradesandmanager"").show();
								$(""#Showuserandleader"").hide();
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
				//如果登入者的權限為Leader或User的話插入下列Script程式碼
				else if (info.Privilege == "Leader" || info.Privilege == "User")
                {
					//如果沒有找到對應UserID的使用者則顯示空白頁面
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
					//#Showgradesandmanager 隱藏班級小組及個人的選擇框
					//#Showuserandleader 將固定顯示的選擇框顯示
					//找id 為app的dom 標籤 插入<script></script>的程式碼
					//vuetify: new Vuetify() 使用vue.js當中的Vuetify框架方法
					//data內的左邊變數 分別對應ImmediateReplyInSide.Master及uc控制項內的相對名稱
					//drawer 左邊選單 null為預設開啟
					//chooseclass 為顯示使用者的班級值
					//choosegroup 為顯示使用者的小組值
					//choosename 為顯示使用者的名稱
					//rules1 及 classrules 為輸入規則
					//classchoice 為傳送去SeeGradeHandler的班級值
					//group為傳送去SeeGradeHandler的小組值
					//name為傳送去SeeGradeHandler的小組成員值
					//this.initialize()為將傳送過去的資料在SeeGradeHandler處理後 顯示在頁面上
					//email為選到的人的Email資訊
					//score為經評分者與PM分數打完計算後顯示的分數
					//boss 與 pm 分別顯示評語部分
					//panel為 boss與pm的版面設計部分
					//style type=""text/css"" scoped 為排版部分
					return $@"
								<script>
								$(""#Showgradesandmanager"").hide();
								$(""#Showuserandleader"").show();
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
				//如果沒抓到PageType則顯示空白頁面
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
			//如果沒抓到PageType則顯示空白頁面
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