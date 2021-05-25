<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="ManageProject.aspx.cs" Inherits="ProjectImmediateReply.ManageProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <v-main style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1); background: linear-gradient(to right, #FFAF7B, #FFB6C1);">
						<div class="text-center">
						  <!--             <v-btn dark color="red darken-2" @click="snackbar = true">
						    Open Snackbar
						  </v-btn> -->
						
						  <v-snackbar v-model="snackbar" top right>
						    <v-icon color="red">mdi-close-circle-outline</v-icon>
						    {{ showmessage }}
						
						    <template v-slot:action="{ attrs }">
						      <v-btn color="red" text v-bind="attrs" @click="snackbar = false">
						        Close
						      </v-btn>
						    </template>
						  </v-snackbar>
						
						  <v-snackbar v-model="snackbar1" top right>
						    <v-icon color="green">
						      mdi-checkbox-marked-circle
						    </v-icon>
						    {{ showmessagesuccess }}
						
						    <template v-slot:action="{ attrs }">
						      <v-btn color="red" text v-bind="attrs" @click="snackbar1 = false">
						        Close
						      </v-btn>
						    </template>
						  </v-snackbar>
						</div>
						<v-row>
							<v-spacer></v-spacer>
							<p class="h1 ml-0 mr-6 mt-3 pl-0 font-weight-bold">專案名：{{ProjectName}}  組名：{{TeamName}}</p>
							</v-row>
							<v-row>
								<v-spacer></v-spacer>
								<p class="pa-2 mx-0 h1 font-weight-bold" style="font-size: 1.5rem;">
									專案管理</p>
									<v-spacer></v-spacer>
							</v-row>
							<v-row>
								<p class="pa-5 ml-3 subtitle-2 font-weight-bold"> 專案進度 {{ProjectSchedule}}% </p>
								<v-btn v-if="" color="primary" dark class="mt-3 mx-0" id="ProjectCompletebtn" @click="ProjectCompleted">結案</v-btn>
								<v-spacer></v-spacer>
								<!-- <p class="pa-2 pr-10 mx-0 h1 font-weight-bold" style="font-size: 1.5rem;">
									專案管理</p> -->
								<p class="pa-5  mr-3 subtitle-2 font-weight-bold"> 姓名 {{UserName}}</p>
							</v-row>
						<!-- <v-row>
							<v-spacer></v-spacer>
							<p class="h1 font-weight-bold" style="font-size: 2rem;">
								專案管理</p>
							<v-spacer></v-spacer>
						</v-row> -->
						<!-- 下面是table標題和table -->
						<template>
							<v-data-table 
							style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);"
							@page-count="pageCount = $event" 
							:page.sync="page"
							:items-per-page="itemsPerPage" hide-default-footer :headers="headers" :items="inneritem" 
								sort-by="calories" class="elevation-1">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat color="white" style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);">
										<v-spacer></v-spacer>
										<p class="pa-2 mx-0 h1 font-weight-bold" style="font-size: 1rem;">
									工作項目</p>
										<v-spacer></v-spacer>
									</v-toolbar>
								</template>
								<!-- ----->
								<!-- ROW是TABLE裡面一橫排內容的意思 封裝好的 -->
								<template #item.uploadfile="{ item }">
								             <!-- 切割出來開始 -->
						<v-dialog v-model="dialoga"  persistent max-width="600px" :retain-focus="false" >
						 <template v-slot:activator="{ on, attrs }">
					<v-btn color="blue" dark text v-bind="attrs" v-on="on" @click="editItem(item)" v-show="Userid==item.Work_UserID">
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
									
									            <v-tab href="#上傳檔案" @click="">
									                上傳檔案
									                <v-icon>mdi-cloud-upload</v-icon>
									            </v-tab>
									
									            <v-tab href="#上傳網址" @click="">
									                上傳網址
									                <v-icon>mdi-code-json</v-icon>
									            </v-tab>
									        </v-tabs>
									<!-- ------------ -->
									        <v-tabs-items v-model="tab">
												<!-- - -->
												<v-form method="post" novalidate="true" ref="form" v-model="valid1">
								                 <v-tab-item :key="1" value="上傳檔案">
								                     <v-card flat>
								             	<v-img src="https://wallpaperaccess.com/full/1381091.jpg" height="250">
								             		<v-card-title>
								             			<v-spacer></v-spacer>
								             			<span class="headline">{{ProjectName}}專案</span>
														<span class="headline ml-2">組名{{TeamName}}</span>
														<v-spacer></v-spacer>
													</v-card-title>
													<v-card-text>
														<v-row>
														<v-spacer></v-spacer>
														<span class="h6">工作項目</span>
														<span class="h6 ml-6">{{editedItem.WorkName}}</span>
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
		        									<v-btn color="blue darken-1" text :disabled="!valid1" @click="上傳1()">上傳</v-btn>
								  <v-btn color="blue darken-1" text @click="取消()">取消</v-btn>
								     					</v-card-actions>
								     					</v-card-text>
								     					</v-img>
								                    </v-card>
								                </v-tab-item>
								     			</v-form>
								     			<!-- ------ -->
								     			<v-form method="post" novalidate="true" ref="form" v-model="valid2">
								                <v-tab-item :key="2" value="上傳網址">
								              <v-card flat>
								     <v-img src="https://wallpaperaccess.com/full/1381091.jpg" height="250">
								             			<v-card-title>
								             				<v-spacer></v-spacer>
								             				<span class="headline">{{ProjectName}}專案</span>
								    		<span class="headline ml-2">組名{{TeamName}}</span>
				                				<v-spacer></v-spacer>
				                			</v-card-title>
				                			<v-card-text>
				                				<v-row>
				                				<v-spacer></v-spacer>
				                				<span class="h6">工作項目</span>
				                				<span class="h6 ml-6">{{editedItem.WorkName}}</span>
								       				<v-spacer></v-spacer>
								       				</v-row>
								               <v-col cols="12" sm="12">
								               	<v-text-field label="上傳網址"
								               	v-model="editedItem.url"
								       			:rules="rules2"
								               		 persistent-hint
								               		required></v-text-field>
								               </v-col>
								       		<v-card-actions>
								       			<v-spacer></v-spacer>
								       			<v-btn color="blue darken-1" text :disabled="!valid2" @click="上傳2()">上傳</v-btn>
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
										 
								<template #item.leadercheck="{ item }">
								             <v-dialog v-model="dialogb" persistent max-width="600px" :retain-focus="false">
								             	<template v-slot:activator="{ on, attrs }">
								    <v-btn color="red" dark text v-bind="attrs" v-on="on" @click="editItem(item)" 
                                        v-show="item.FilePath && Privilege=='Leader'&&!item.Complete">
								             			組長確認
								             		</v-btn>
								             	</template>
								             	
								             	<v-form
                                                     method="post"
                                                     novalidate="true"
								             	    ref="form"
								             	    v-model="valid3"
								             	  >
								             	<v-card>
								             		<v-img src="https://wallpaperaccess.com/full/1381091.jpg" height="470">
								             		<v-card-title>
								             			<v-spacer></v-spacer>
								             			<span class="headline">{{ProjectName}}專案</span>
								             			<span class="headline ml-2">組名{{TeamName}}</span>
								             			<v-spacer></v-spacer>
								             		</v-card-title>
								             		<v-card-text>
								             			<v-container>
								             				
								             					<v-row>
								             					<v-spacer></v-spacer>
								             						<span class="h6">工作項目:</span>
								             						<span class="h6">{{editedItem.WorkName}}</span>
								             					<v-spacer></v-spacer>
								             					</v-row>
								             					
								             					<v-row>
								             						<v-spacer></v-spacer>
								             						<!-- :href="item.url" -->
								           <v-btn color="blue darken-1" text :href="editedItem.FilePath" class="mt-3" >檔案連結</v-btn>
								             						<v-spacer></v-spacer>
								             					</v-row>
								             					
								             					<v-row v-if="">
								             						<v-textarea
								             							:rules="[v => !!v || '您必須填寫此項才可以駁回']"
																		hint="您必須填寫此項才可以駁回"
								             						      background-color="amber lighten-4"
								             						      color="orange orange-darken-4"
								             						      label="意見"
								             							  no-resize
								             							  class="mb-0"
								             							  v-model="editedItem.opinion"
								             							  required
								             						    ></v-textarea>
								             					</v-row>
								             					
								             					<v-row v-if="">
								             					<v-checkbox
								             					      v-model="editedItem.checkbox"
								             					      :rules="[v => !!v || '您必須勾選此項確認或駁回']"
								             					      label="您確定要確認或駁回此項目嗎?"
								             					      required
								             					    ></v-checkbox>
								             						</v-row>
								             					
								             			</v-container>
								             		</v-card-text>
								             		<v-card-actions class="mx-2 mt-0">
								  <v-btn :disabled="!editedItem.checkbox" color="blue darken-1" text @click="leadercheck的確認()">確認</v-btn>
								            <v-btn :disabled="!valid3" color="blue darken-1" text @click="leadercheck的駁回()">駁回</v-btn>
								             			<v-spacer></v-spacer>
								             			<v-btn color="blue darken-1" text @click="取消()">取消</v-btn>
								             		</v-card-actions>
								             		</v-img>
								             	</v-card>	
								             	</v-form>
								             </v-dialog>
								         </template>
										 
								<template #item.complete="{ item }">
								             <v-btn  color="cyan" dark text :href="item.FilePath" v-show="item.Complete">
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
						</template>

					</v-main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        //import { error } from "jquery"

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
                ProjectComplete: false,
                ProjectSchedule: "",
                Userid: 0,
                UserName: "",
                ClassNumber: "",
                Privilege: "",
                page: 1,
                pageCount: 0,
                itemsPerPage: 10,
                inneritem: [],
                snackbar: false,
                snackbar1: false,
                showmessagesuccess: '我是正確訊息',
                showmessage: '我是錯誤訊息',
                // 主頁變數結束	
                // -----------------// B3-1變數開始
                // 控制彈跳視窗
                dialoga: false,
                // ----------
                // 進入這頁面前需要帶的資料
                editedIndex: -1,
                tab: "subscribe",
                ProjectName: "",
                TeamName: "",
                ProjectID: "",
                FileName: "",
                // 工作項目:"789",
                // 驗證valid
                valid1: true,
                valid2: true,
                // 上傳檔案後顯示的小卡值
                //files:[],
                // 上傳網址
                url: null,
                rules1: [
                    files => !files || !files.some(file => file.size > 10485760) || '檔案不可超過 10 MB!',
                    files => !!files || '請上傳檔案'
                ],
                rules2: [
                    url => !!url || '請填入上傳網址'
                ],
                // 下面是上傳檔案視窗中間變數
                editedItem: {
                    id: '',
                    ProjectName: '',
                    TeamName: '',
                    WorkName: '',
                    files: [],
                    url: '',
                    opinion: '',
                },
                // B3-1變數結束
                // -----------------// B3-2變數開始
                dialogb: false,
                valid3: true,
                opinion: "",

                // 下面是組長確認視窗中間變數
                defaultItem: {
                    id: '',
                    ProjectName: '',
                    TeamName: '',
                    WorkName: '',
                    files: null,
                    url: null,
                },
                // B3-2變數結束				

                // 下面headers若非需要請勿修改
                headers: [{
                    text: '工作項目',
                    align: 'start',
                    value: 'WorkName',
                },
                {
                    text: '工作內容',
                    value: 'WorkDescription'
                },
                {
                    text: '時程期限',
                    value: 'DeadLine'
                },
                {
                    text: '完成日期',
                    value: 'UpdateTime',
                },
                {
                    text: '',
                    value: 'uploadfile',
                    sortable: false
                },
                {
                    text: '',
                    value: 'leadercheck',
                    sortable: false
                },
                {
                    text: '',
                    value: 'complete',
                    sortable: false
                },
                ],

            }),

            watch: {
                dialoga(val) {
                    val || this.close()
                },
                dialogb(val) {
                    val || this.close()
                },
            },

            created() {
                this.initialize()
            },

            methods: {
                // 主頁方法開始
                // 初始化資料
                // 二擇一 initialize上面靜態 下面動態get取得資料
                //initialize() {
                //    // 組別專案名一開始給空的 之後去接值
                //    this.inneritem = [

                //    ];
                //},
                initialize() {
                    axios.post('API/GetCrudHandler.ashx', {
                        innertype: 'ManageProject',
                        classchoice: 'None'
                    })
                        .then(response => {
                            vm.showmessagesuccess = '完成';
                            vm.snackbar1 = true;
                            vm.Privilege = response.data.Privilege;
                            vm.Userid = response.data.Userid;
                            vm.UserName = response.data.UserName;
                            vm.ClassNumber = response.data.ClassNumber;
                            vm.ProjectName = response.data.ProjectName;
                            vm.TeamName = response.data.TeamName;
                            vm.ProjectID = response.data.ProjectID;
                            vm.inneritem = response.data.inneritem;
                            vm.ProjectSchedule = response.data.ProjectSchedule;
                            vm.ProjectComplete = response.data.ProjectComplete;
                            if (vm.ProjectSchedule == '100' && vm.Privilege == 'Leader') {
                                $("#ProjectCompletebtn").show();
                            } else {
                                $("#ProjectCompletebtn").hide();
                            }
                            if (vm.ProjectComplete == true) {
                                $("#ProjectCompletebtn").hide();
                            }
                        }
                        )
                        .catch(error => {
                            vm.showmessage = '加載失敗' + error;
                            vm.snackbar = true;
                        })
                },
                // 主頁方法結束
                //B3-1方法開始
                上傳1() {

                    let formData = new FormData();

                    // files
                    for (let file of this.editedItem.files) {
                        formData.append("files", file, file.name);
                    }
                    axios
                        .post("API/GetFileHandler.ashx", formData)
                        .then(response => {
                            vm.FileName = response.data.FileName;
                            this.UpdateFileDB();
                            this.取消();
                        })
                        .catch(error => {
                            vm.showmessage = '發送失敗' + error;
                            vm.snackbar = true;
                            this.initialize();
                            this.取消();
                        });

                    // else {
                    // 	alert('不可預期錯誤');
                    // }
                },
                上傳2() {


                    axios
                        .post("API/GetFileHandler.ashx", { FileName: vm.editedItem.url, id: vm.editedItem.id })
                        .then(response => {
                            vm.showmessagesuccess = '發送成功';
                            vm.snackbar1 = true;
                            this.initialize();
                            this.取消();
                        })
                        .catch(error => {
                            vm.showmessage = '發送失敗' + error;
                            vm.snackbar = true;
                            this.initialize();
                            this.取消();
                        });

                    // else {
                    // 	alert('不可預期錯誤');
                    // }
                },
                close() {
                    this.initialize();
                    this.dialoga = false,
                        this.dialogb = false,
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
                    // this.dialogb = true
                },

                取消() {
                    this.$refs.form.reset();
                    this.$refs.form.resetValidation();
                    this.close();
                },

                //B3-1方法結束		
                // B3-2方法開始
                leadercheck的確認() {
                    if (confirm('確定認可此工作?')) {
                        axios.post('API/ManageProject_LeaderCheckHandler.ashx', {
                            CheckType: "WorkComplete",
                            id: this.editedItem.id,
                            opinion: ""
                        })
                            .then(response => {
                                vm.showmessagesuccess = '發送成功';
                                vm.snackbar1 = true;
                                this.initialize();
                                this.取消();
                            })
                            .catch(error => {
                                vm.showmessage = '發送失敗' + error;
                                vm.snackbar = true;
                                this.initialize();
                                this.取消();
                            });
                    }
                },
                leadercheck的駁回() {
                    if (confirm('確定駁回此工作?')) {
                        axios.post('API/ManageProject_LeaderCheckHandler.ashx', {
                            CheckType: "WorkFailed",
                            id: this.editedItem.id,
                            opinion: this.editedItem.opinion,
                        })
                            .then(response => {
                                vm.showmessagesuccess = '發送成功';
                                vm.snackbar1 = true;
                                this.initialize();
                                this.取消();
                            })
                            .catch(error => {
                                vm.showmessage = '發送失敗' + error;
                                vm.snackbar = true;
                                this.initialize();
                                this.取消();
                            });
                    }
                },
                ProjectCompleted() {
                    if (confirm('確定完成此專案?')) {
                        vm.ProjectComplete = true;
                        axios.post('API/ManageProject_LeaderCheckHandler.ashx', {
                            CheckType: "ProjectComplete",
                            id: vm.ProjectID,
                            opinion: "",
                        })
                            .then(response => {
                                vm.showmessagesuccess = '發送成功';
                                vm.snackbar1 = true;
                                this.initialize();
                                this.取消();
                            })
                            .catch(error => {
                                vm.showmessage = '發送失敗' + error;
                                vm.snackbar = true;
                                this.initialize();
                                this.取消();
                            });
                    }
                },
                UpdateFileDB() {
                    axios.post("API/GetFileHandler.ashx", { FileName: vm.FileName, id: vm.editedItem.id })
                        .then(response => {
                            vm.showmessagesuccess = '發送成功';
                            vm.snackbar1 = true;
                            this.initialize();
                            this.取消();
                        })
                        .catch(error => {
                            vm.showmessage = '發送失敗' + error;
                            vm.snackbar = true;
                            this.initialize();
                            this.取消();
                        });
                },

                // B3-2方法結束
                // B3-3方法開始

                // B3-3方法結束


            },   //methods結尾標籤

        })
    </script>
</asp:Content>
