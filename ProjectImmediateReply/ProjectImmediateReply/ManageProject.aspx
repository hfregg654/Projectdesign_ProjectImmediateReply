<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="ManageProject.aspx.cs" Inherits="ProjectImmediateReply.ManageProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <v-main>
						<v-row>
							<v-spacer></v-spacer>
							<p class="h1 ml-0 mr-6 mt-3 pl-0 font-weight-bold">{{專案}}專案  組名{{組名}}</p>
							</v-row>
							<v-row>
								<v-spacer></v-spacer>
								<p class="pa-2 mx-0 h1 font-weight-bold" style="font-size: 1.5rem;">
									專案管理</p>
									<v-spacer></v-spacer>
							</v-row>
							<v-row>
								<p class="pa-5 ml-3 subtitle-2 font-weight-bold"> 專案進度 {{專案進度趴數}}% </p>
								<v-btn v-if="" color="primary" dark class="mt-3 mx-0">結案</v-btn>
								<v-spacer></v-spacer>
								<!-- <p class="pa-2 pr-10 mx-0 h1 font-weight-bold" style="font-size: 1.5rem;">
									專案管理</p> -->
								<p class="pa-5  mr-3 subtitle-2 font-weight-bold"> 姓名 {{姓名}}</p>
							</v-row>
						<!-- <v-row>
							<v-spacer></v-spacer>
							<p class="h1 font-weight-bold" style="font-size: 2rem;">
								專案管理</p>
							<v-spacer></v-spacer>
						</v-row> -->
						<!-- 下面是table標題和table -->
						<template>
							<v-data-table @page-count="pageCount = $event" :page.sync="page"
								:items-per-page="itemsPerPage" hide-default-footer :headers="headers" :items="inneritem" 
								sort-by="calories" class="elevation-1">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat color="white">
										<v-spacer></v-spacer>
										工作項目
										<v-spacer></v-spacer>
									</v-toolbar>
								</template>
								<!-- ----->
								<!-- ROW是TABLE裡面一橫排內容的意思 封裝好的 -->
								<template #item.上傳連結或檔案="{ item }">
								             <!-- 切割出來開始 -->
								             								<v-dialog v-model="dialoga" persistent max-width="600px" :retain-focus="false">
								             									<template v-slot:activator="{ on, attrs }">
								             										<v-btn color="blue" dark text v-bind="attrs" v-on="on" @click="editItem(item)">
								             											上傳連結或檔案
								             										</v-btn>
								             									</template>
								             									
								             									
								             									
								             									
								             									<v-card>
								             									        <v-tabs
								             									            v-model="tab"
								             												dark
								             									            centered
								             									            icons-and-text
								             									        >
								             									            <v-tabs-slider></v-tabs-slider>
								             									
								             									            <v-tab href="#上傳檔案" @click="clear()">
								             									                上傳檔案
								             									                <v-icon>mdi-cloud-upload</v-icon>
								             									            </v-tab>
								             									
								             									            <v-tab href="#上傳網址" @click="clear()">
								             									                上傳網址
								             									                <v-icon>mdi-code-json</v-icon>
								             									            </v-tab>
								             									        </v-tabs>
								             									<!-- ------------ -->
								             									        <v-tabs-items v-model="tab">
								             												<!-- - -->
								             												<v-form method="post" novalidate="true" ref="form1">
								             									            <v-tab-item :key="1" value="上傳檔案">
								             									                <v-card flat>
								             														<v-img src="https://wallpaperaccess.com/full/1381091.jpg" height="250">
								             															<v-card-title>
								             																<v-spacer></v-spacer>
								             																<span class="headline">{{專案}}專案</span>
								             																<span class="headline ml-2">組名{{組名}}</span>
								             																<v-spacer></v-spacer>
								             															</v-card-title>
								             															<v-card-text>
								             																<v-row>
								             																<v-spacer></v-spacer>
								             																<span class="h6">工作項目</span>
								             																<span class="h6 ml-6">{{editedItem.工作項目}}</span>
								             																<v-spacer></v-spacer>
								             																</v-row>
								             									                    <v-col cols="12" sm="12">
								             									                    	<v-file-input
								             									                    		v-model="editedItem.files"
								             									                    		:rules="rules1"
								             									                    		hint="檔案大小須小於10MB"
								             									                    		placeholder="請選擇檔案"
								             									                    		label="上傳檔案"
								             									                    		accept=".docx,.doc,.pdf,.xlsx,.xls,.jpg,.png,"
								             									                    		show-size 
								             									                    		counter 
								             									                    		chips 
								             									                    		multiple
								             									                    		prepend-icon="mdi-paperclip"
								             									                    		required
								             									                    	  >
								             									                    		<template v-slot:selection="{ text }">
								             									                    		  <v-chip
								             									                    			small
								             									                    			label
								             									                    			color="primary"
								             									                    		  >
								             									                    			{{ text }}
								             									                    		  </v-chip>
								             									                    		</template>
								             									                    	  </v-file-input>
								             									                    </v-col>
								             														<v-card-actions>
								             															<v-spacer></v-spacer>
								             															<v-btn color="blue darken-1" text :disabled="!valid" @click="上傳1()">上傳</v-btn>
								             															<v-btn color="blue darken-1" text @click="取消()">取消</v-btn>
								             														</v-card-actions>
								             														</v-card-text>
								             														</v-img>
								             									                </v-card>
								             									            </v-tab-item>
								             												</v-form>
								             												<!-- ------ -->
								             												<v-form method="post" novalidate="true" ref="form2">
								             									            <v-tab-item :key="2" value="上傳網址">
								             									                <v-card flat>
								             														<v-img src="https://wallpaperaccess.com/full/1381091.jpg" height="250">
								             															<v-card-title>
								             																<v-spacer></v-spacer>
								             																<span class="headline">{{專案}}專案</span>
								             																<span class="headline ml-2">組名{{組名}}</span>
								             																<v-spacer></v-spacer>
								             															</v-card-title>
								             															<v-card-text>
								             																<v-row>
								             																<v-spacer></v-spacer>
								             																<span class="h6">工作項目</span>
								             																<span class="h6 ml-6">{{editedItem.工作項目}}</span>
								             																<v-spacer></v-spacer>
								             																</v-row>
								             									                    <v-col cols="12" sm="12">
								             									                    	<v-text-field label="上傳網址"
								             									                    	v-model="url"
								             															:rules="rules2"
								             									                    		 persistent-hint
								             									                    		required></v-text-field>
								             									                    </v-col>
								             														<v-card-actions>
								             															<v-spacer></v-spacer>
								             															<v-btn color="blue darken-1" text :disabled="!valid" @click="上傳2()">上傳</v-btn>
								             															<v-btn color="blue darken-1" text @click="取消()">取消</v-btn>
								             														</v-card-actions>
								             														</v-card-text>
								             														</v-img>
								             									                </v-card>
								             									            </v-tab-item>
								             												</v-form>
								             									        </v-tabs-items>
								             											
								             									    </v-card>
								             									
								             									
								             									
								             									
								             								</v-dialog>
								             <!-- 切割出來結束 -->
								         </template>
										 
								<template #item.組長確認="{ item }">
								             <v-btn  color="red" dark text :href="item.組長確認">
												 組長確認
								             </v-btn>
								         </template>
										 
								<template #item.已完成="{ item }">
								             <v-btn  color="cyan" dark text :href="item.已完成">
												 已完成
								             </v-btn>
								         </template>
								<!-- v-slot 結束							 -->
								<!-- 小組名插槽1 -->
								<!-- <template v-slot:item.查看="{ item }">
									<p>1</p>
								</template> -->
								
								
							</v-data-table>

							<v-pagination v-model="page" :length="pageCount"></v-pagination>
								<v-row>
									<v-spacer></v-spacer>
							<v-btn color="primary" dark class="mb-2 mr-10"　@click="store">
								儲存</v-btn>
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
					//彈跳視窗
// 主頁變數開始
					// 專案:"A",
					// 組名:"123",
					專案進度趴數:"99",
					姓名:"阿哲",
					page: 1,
					pageCount: 0,
					itemsPerPage: 10,
					inneritem: [],
// 主頁變數結束	
// -----------------// B3-1變數開始
							// 控制彈跳視窗
							dialoga: false,
							// ----------
							// 進入這頁面前需要帶的資料
							editedIndex: -1,
							tab: "subscribe",
							專案:"123",
							組名:"456",
							// 工作項目:"789",
							// 驗證valid
							valid: true,
							// 上傳檔案後顯示的小卡值
							files:null,
							// 上傳網址
							url:null,
							rules1: [
								  files => !files || !files.some(file => file.size > 10485760) || '檔案不可超過 10 MB!',
								  files => !!files || '請上傳檔案'
								],
								rules2: [
									  url => !!url || '請填入上傳網址'
									],	
							// 下面是上傳檔案視窗中間變數
					editedItem: {
						id:'',
						專案:'',
						組名:'',
						工作項目: '',
						files:'',
						url:'',
					},
					// B3-1變數結束
// -----------------// B3-2變數開始
					
					
					
					// 下面是組長確認視窗中間變數
					defaultItem: {
						id:'',
						專案:'',
						組名:'',
						工作項目: '',
						files:null,
						url:null,
					},
					// B3-2變數結束
// -----------------// B3-3變數開始
					
					
					
					
					
					// B3-3變數結束
					
					
					
					
					
// 下面headers若非需要請勿修改
					headers: [{
							text: '工作項目',
							align: 'start',
							value: '工作項目',
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
							text: '完成日期',
							value: '完成日期',
						},
						{
							text: '',
							value: '上傳連結或檔案',
							sortable: false
						},
						{
							text: '',
							value: '組長確認',
							sortable: false
						},
						{
							text: '',
							value: '已完成',
							sortable: false
						},
						
					],

				}),

				watch: {
					dialoga(val) {
						val || this.close()
					},
				},

				created() {
					this.initialize()
				},

				methods: {
// 主頁方法開始
						// 儲存按鍵	按下後把整個inneritem傳過去					
						store(){
							axios.post('', {
								inneritem:this.inneritem
							  })
							  .then(response =>{
								  alert('儲存發送成功');
							  })
							  .catch(error => {
							    alert('儲存發送失敗');
							  });
						},
					// 初始化資料
					// 二擇一 initialize上面靜態 下面動態get取得資料
					initialize() {
						// 組別專案名一開始給空的 之後去接值
						this.inneritem = [
							{
								id:'1',
								專案:'projectA',
								組名:'teamA',
								工作項目: '專案管理',
								工作內容:'掃地',
								時程期限: '2013-07-29',
								完成日期:'2013-07-29',
								上傳連結或檔案:'https://www.google.com/webhp?hl=zh-TW&sa=X&ved=0ahUKEwiG9Yn3_cDwAhVKBKYKHYddDlAQPAgI',
								files:null,
								url:null,
								組長確認:'1',
								已完成:'1',
							},
							{
								id:'2',
								專案:'projectB',
								組名:'teamB',
								工作項目: '無人機',
								工作內容:'拖地',
								時程期限: '2019-01-29',
								完成日期:'2013-07-29',
								上傳連結或檔案:'https://tw.help.yahoo.com/kb/account',
								files:null,
								url:null,
								組長確認:'2',
								已完成:'2',
							},
							{
								id:'3',
								專案:'projectC',
								組名:'teamC',
								工作項目: '進出貨',
								工作內容:'擦桌子',
								時程期限: '1999-01-29',
								完成日期:'2013-07-29',
								上傳連結或檔案:'https://www.google.com/webhp?hl=zh-TW&sa=X&ved=0ahUKEwiG9Yn3_cDwAhVKBKYKHYddDlAQPAgI',
								files:null,
								url:null,
								組長確認:'3',
								已完成:'3',
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
// 主頁方法結束
//B3-1方法開始
					close() {
						this.dialoga = false
						this.$nextTick(() => {
							// 把彈跳視窗輸入框清空成defaultItem
							this.editedItem = Object.assign({}, this.defaultItem)
							// 恢復成新增模式
							this.editedIndex = -1
						})
					},
					
					editItem(item) {
				  // FOR循環找到item(此for循環物件)的index
				  this.editedIndex = this.inneritem.indexOf(item)
				  // 抓到此item後 
				  // Object.assign() 方法用于将所有可枚举属性的值从一个或多个源对象source复制到目标对象。它将返回目标对象target。
				  // 例子
				  // const v1 = 'abc' const v2 = true const v3 = 10 
				  // const obj = Object.assign({}, v1, v2, v3)
				  // obj // { "0": "a", "1": "b", "2": "c" }
				  this.editedItem = Object.assign({}, item)
				  this.dialoga = true
					},
				上傳1() {
					if (this.$refs.form1.validate()) {
						let formData = new FormData();
								
						// files
						for (let file of this.editedItem.files) {
							formData.append("files", file, file.name);
							axios
								.post("API/GetFileHandler.ashx", this.editedItem.formData)
								.then(response => {
									console.log("Success!");
									
									alert('success');
									this.取消();
								})
								.catch(error => {
									
									alert('fail');
									this.取消();
								});
						}
					} 
					// else {
					// 	alert('不可預期錯誤');
					// }
				},
				上傳2() {
					if (this.$refs.form2.validate()) {
						
							axios
								.post("API/GetFileHandler.ashx", vm.editedItem.url)
								.then(response => {
									alert('success');
									this.取消();
								})
								.catch(error => {
									alert('fail');
									this.取消();
								});
					} 
					// else {
					// 	alert('不可預期錯誤');
					// }
				},
				clear(){
					this.$refs.form1.reset();
					this.$refs.form2.reset();
				},
				取消(){
					this.editedItem.files = null,
					this.editedItem.url = null,
					this.editedItem.formData = null,
					this.editedItem.FormData = null,
					this.dialoga = false,
					this.$refs.form1.reset();
					this.$refs.form2.reset();
					this.$refs.form1.resetValidation();
					this.$refs.form2.resetValidation();
					this.close();
				},
				
				
					
//B3-1方法結束		
// B3-2方法開始
					
// B3-2方法結束
// B3-3方法開始

// B3-3方法結束


				 },   //methods結尾標籤

			})
    </script>
</asp:Content>
