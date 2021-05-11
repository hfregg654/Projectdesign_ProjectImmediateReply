<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="ProjectDetail_Grades.aspx.cs" Inherits="ProjectImmediateReply.ProjectDetail_Grades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <v-main style="background: -webkit-linear-gradient(right, #FFAF7B, #FFABAB); background: linear-gradient(to right, #FFAF7B, #FFABAB);">
        <div v-show="false">
            <input type="hidden" id="hiddenClass" value=""/>
            <input type="hidden" id="hiddenTeam" value=""/>
        </div>
						<v-row>
							<v-spacer></v-spacer>
							<p class="h1 ml-10 pl-16 font-weight-bold">{{專案}}專案</p>
							<v-spacer></v-spacer>
							<!-- <v-btn color="primary" dark class="my-2 mr-10"　@click="GiveGrade()">
									評分</v-btn> -->
									<!-- 評分彈跳視窗開始 -->
							<v-dialog v-model="dialog" persistent max-width="600px">
								<template v-slot:activator="{ on, attrs }">
									<v-btn color="primary" dark class="mr-5" 　@click="GetData()" 　v-bind="attrs"
										v-on="on">
										評分
									</v-btn>
								</template>
								<v-card>
									<!-- <v-card-title>
								      <span class="headline">User Profile</span>
								    </v-card-title> -->
									<v-card-text>
										<v-container>
											<v-row>
												<v-col cols="12" >
													<v-select v-model="details.姓名" :items="details.姓名" label="姓名" required></v-select>
												</v-col>
												<v-col cols="12" sm="12" md="6">
													<v-text-field v-model="details.專案成績" label="專案成績" hint="專案成績:每位成員分數皆不一樣"></v-text-field>
												</v-col>

												<v-col cols="12" sm="12" md="6">
													<v-text-field v-model="details.面談成績" label="面談成績" hint="面談成績:成員和社長或PM面談所獲得的分數">
													</v-text-field>
												</v-col>

												<v-col cols="12" sm="12">
													<v-textarea background-color="amber lighten-4"
														color="orange orange-darken-4" label="評語" no-resize class="mb-0"
														v-model="details.評語"></v-textarea>
												</v-col>

											</v-row>
										</v-container>
										<!-- <small>*indicates required field</small> -->
									</v-card-text>
									<v-card-actions>
										<v-spacer></v-spacer>
										<v-btn color="blue darken-1" text @click="send()">
											評分
										</v-btn>
										<v-btn color="blue darken-1" text @click="dialog = false">
											取消
										</v-btn>

									</v-card-actions>
								</v-card>
							</v-dialog>
							<!-- 評分彈跳視窗end -->
						</v-row>
						<!-- -------- -->
						<v-row>
							<v-spacer></v-spacer>
							<p class="pa-5 subtitle-2 font-weight-bold">組名{{組名}} 組長{{組長}} 組員{{組員}}</p>
							<v-spacer></v-spacer>
						</v-row>
						
						<template>
							<v-data-table @page-count="pageCount = $event" :page.sync="page"
								:items-per-page="itemsPerPage" 
								hide-default-footer 
								:headers="headers" 
								:items="inneritem"
								item-key="id"
								class="elevation-1"
								style="background: -webkit-linear-gradient(right, #FFAF7B, #FFABAB);background: linear-gradient(to right, #FFAF7B, #FFABAB);   ">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat color="white"
										style="background: -webkit-linear-gradient(right, #FFAF7B, #FFABAB);background: linear-gradient(to right, #FFAF7B, #FFABAB);   ">
										<v-spacer></v-spacer>
										工作項目
										<v-spacer></v-spacer>
									</v-toolbar>
								</template>
								<!-- ----->
									  <template #item.花費時間="{ item }">
									        {{ item.花費時間 }}天
									    </template>
										<template #item.url="{ item }">
										             <v-btn  text dark small color="green" :href="item.url">
														 檔案連結
										             </v-btn>
										         </template>
								<!-- ---
								 -->
								
							</v-data-table>

							<v-pagination v-model="page" :length="pageCount"></v-pagination>
							<v-row>
								<v-spacer></v-spacer>
								<v-btn color="primary" dark class="mb-2 mr-10" href="./Index.aspx?PageInnerType=GradesCrud" >
									返回</v-btn>
							</v-row>
						</template>

					</v-main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        // 下面ＴＡＢＬＥ的作法是,頁面加載時獲取一次,按下小組亂數分配刷新獲取一次,儲存時ＰＯＳＴ回去小組亂數結果
        // 組別和專案名一開始先給空值 不會顯示 （inneritem.project  inneritem.teamname）
        var vm = new Vue({
            el: '#app',
            vuetify: new Vuetify(),
            data: () => ({
                drawer: false,
                //
                dialog: false,
                // -----
                // table外面資料
                專案: "XXX",
                組名: "1234",
                組長: "5678",
                組員: "XXXXX,XXXX,XXXX,XXXXX,XXXXXXXX,XXXXXX",
                // 評分按鈕彈跳視窗邏輯
                details: {
                    班別集合: ['第一班', '第二班', '第三班', '第四班'],
                    班別: '第三班',
                    姓名: ['A', 'B', 'C', 'D'],
                    專案成績: "",
                    面談成績: "",
                    評語: "aaaaa <br> bbbbbb   cccc       ddddddd    eeeee      ffffffff",
                },
                // ----------------------
                // 變數開始
                // 管理分頁變數開始
                page: 1,
                pageCount: 0,
                itemsPerPage: 10,
                // 管理分頁變數結束
                // 彈跳視窗開始
                dialog: false,
                // 彈跳視窗結束					
                inneritem: [],
                // json資料結束

                // 變數結束
                headers: [{
                    text: '工作',
                    align: 'start',
                    value: '工作',
                },
                {
                    text: '工作內容',
                    value: '工作內容'
                },
                {
                    text: '時程期限',
                    value: '時程期限'
                },
                {
                    text: '負責人員',
                    value: '負責人員',
                },
                {
                    text: '完成日期',
                    value: '完成日期',
                },
                {
                    text: '花費時間',
                    value: '花費時間',
                },
                {
                    text: '',
                    value: 'url',
                },
                ],

            }),

            created() {
                this.initialize()
            },

            methods: {
                // 評分案件邏輯
                GetData() {
                    // axios
                    // 	.get('API/GetCrudHandler.ashx')
                    // 	.then(response => (this.details = response.data))
                    // 	.catch(function(error) {
                    // 		{
                    // 			alert(error);
                    // 		}
                    // 	});	
                },
                // 彈跳視窗評分按鍵	按下後把整個inneritem傳過去	
                send() {
                    // axios
                    // 	.post('API/GetCrudHandler.ashx', {
                    // 		{
                    // 			innertype: 'GradesCrud',
                    // 			classchoice: vm.班別,
                    // 			classchoice: vm.姓名,
                    // 			classchoice: vm.專案成績,
                    // 			classchoice: vm.面談成績,
                    // 			classchoice: vm.評語,
                    // 	or
                    // details:	 vm.details
                    // 		}
                    // 	})
                    // 	.then(response => (this.inneritem = response.data);this.dialog = false)
                    // 	.catch(function(error) {
                    // 		{
                    // 			alert(error);
                    // 		}
                    // 	});	

                },

                // 初始化資料
                // 二擇一 initialize上面靜態 下面動態get取得資料
                initialize() {
                    // 組別專案名一開始給空的 之後去接值
                    this.inneritem = [{
                        id: "1",
                        工作: '專案管理',
                        工作內容: '工作內容1',
                        時程期限: '2020/10/21',
                        負責人員: '學武 學武 學武 學武',
                        完成日期: '2020/10/21',
                        花費時間: '15',
                        url: 'https://www.google.com/webhp?hl=zh-TW&sa=X&ved=0ahUKEwiG9Yn3_cDwAhVKBKYKHYddDlAQPAgI'
                    },
                    {
                        id: "2",
                        工作: '專案管理',
                        工作內容: '工作內容2',
                        時程期限: '2020/10/21',
                        負責人員: '學武 學武 學武 學武 ',
                        完成日期: '2020/10/21',
                        花費時間: '6',
                        url: 'https://tw.help.yahoo.com/kb/account'
                    },

                    ];
                },
                // initialize() {
                // 	axios
                // 		.get('API/GetCrudHandler.ashx')
                // 		.then(response => {
                // 			console.table(response.data)
                // 			this.inneritem = response.data;
                // 		})
                // 		.catch(function(error) {
                // 			{
                // 				alert(error);
                // 			}
                // 		});
                // 	this.inneritem = []
                // },
            },

        })
    </script>
</asp:Content>
