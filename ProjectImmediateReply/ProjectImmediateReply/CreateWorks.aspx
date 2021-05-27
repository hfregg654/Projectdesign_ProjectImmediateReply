<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="CreateWorks.aspx.cs" Inherits="ProjectImmediateReply.CreateWorks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <v-main
        style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1); background: linear-gradient(to right, #FFAF7B, #FFB6C1);">
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
							<p class="h1 ml-0 mr-6  mb-0 pl-0 font-weight-bold">專案名：{{ProjectName}} 組名：{{TeamName}}</p>
                            <v-btn id="ChangeTeamName_btn" color="primary" @click="" class="mr-6" v-show="ProjectID">
									更改組名
							</v-btn>
						</v-row>
						<v-row style="height:3vh" class="mb-2">
							<v-spacer></v-spacer>
							<p class="font-weight-bold">
								專案建置</p>
							<v-spacer></v-spacer>
						</v-row>
						<v-form method="post" novalidate="true" ref="form" v-model="valid">
							<v-stepper 
							class="ma-0" 
							v-model="e1"
								style="height:35vh!important;background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);">
								<v-stepper-header class="elevation-0">
									<v-stepper-step :complete="e1 > 1" step="1" @click="e1 = 1" color="blue">
										步驟1:指派人員
									</v-stepper-step>

									<v-divider></v-divider>

									<v-stepper-step :complete="e1 > 2" step="2" @click="e1 = 2" color="green">
										步驟2:工作項目
									</v-stepper-step>

									<v-divider></v-divider>

									<v-stepper-step :complete="e1 > 3" 　step="3" @click="e1 = 3"
										color="yellow darken-1">
										步驟3:工作內容
									</v-stepper-step>

									<v-divider></v-divider>

									<v-stepper-step :complete="e1 > 4" step="4" @click="e1 = 4" color="red">
										步驟4:時程期限
									</v-stepper-step>
								</v-stepper-header>

								<v-stepper-items>
									<v-container fill-height>
										<v-stepper-content step="1">
											<v-select :items="SelectList" v-model="SelectMember" item-text="UserName" item-value="UserID"
												@change="enterevent()" label="選擇指派人員" solo outlined required>
											</v-select>
											<v-spacer></v-spacer>

											<v-btn color="primary" @click="e1 = 2" class="mb-2">
												下一步
											</v-btn>

										</v-stepper-content>

										<v-stepper-content step="2">
											<v-text-field @keydown.enter="enterevent" :rules="[v => !!v || '此輸入框不可為空白']"
												class="mb-5" label="工作項目" type="text" v-model="newWorkProject.WorkName"
												required></v-text-field>
											<v-spacer></v-spacer>

											<v-btn color="primary" @click="e1 = 3" class="mb-2">
												下一步
											</v-btn>

											<v-btn text @click="e1 = 1" class="mb-2">
												上一步
											</v-btn>
										</v-stepper-content>

										<v-stepper-content step="3">
											<v-text-field @keydown.enter="enterevent" 
												class="mb-5" label="工作內容" type="text" v-model="newWorkProject.WorkDescription"
												></v-text-field>
											<v-spacer></v-spacer>
											<v-spacer></v-spacer>

											<v-btn color="primary" @click="e1 = 4" class="mb-2">
												下一步
											</v-btn>

											<v-btn text @click="e1 = 2" class="mb-2">
												上一步
											</v-btn>
										</v-stepper-content>

										<v-stepper-content step="4" class="mb-5">
											<!-- <p>時程期限</p> -->
											<v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40"
												transition="scale-transition" offset-y min-width="290px">
												<template v-slot:activator="{ on, attrs }">
													<v-text-field @keydown.enter="enterevent" v-model="newWorkProject.DeadLine"
														label="時程期限" prepend-icon="event"
														:rules="[v => !!v || '此輸入框不可為空白']" readonly v-bind="attrs"
														v-on="on"></v-text-field>
												</template>
												<v-date-picker v-model="newWorkProject.DeadLine" @input="menu2 = false" locale="zh-cn">
												</v-date-picker>
											</v-menu>
											<v-spacer></v-spacer>

											<v-btn color="primary" @click="建立工作項目()" :disabled="!valid">
												建立工作項目
											</v-btn>

											<v-btn text @click="e1 = 3">
												上一步
											</v-btn>
										</v-stepper-content>

									</v-container>
								</v-stepper-items>
							</v-stepper>
						</v-form>
						<!-- -----------						 -->

						<template>
							<v-data-table
								style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);"
								height="35vh" @page-count="pageCount = $event" :page.sync="page"
								:items-per-page="itemsPerPage" hide-default-footer :headers="headers" :items="newWorkProject"
								sort-by="WorkID" class="elevation　table-striped mt-0　
									">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);
										">
										<!-- <v-toolbar-title>專案進度X%</v-toolbar-title>
											<v-btn>結案</v-btn> -->
										<!-- <v-divider class="mx-4" inset vertical></v-divider> -->
										<v-spacer></v-spacer>
										<!-- 彈跳視窗功能 (包含按鍵觸發)開始 -->
										<v-dialog v-model="dialog" max-width="500px">
											<!-- <template v-slot:activator="{ on, attrs }">
													<v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">New
														Item</v-btn>
												</template> -->
											<v-card>
												<v-card-title>
													<span class="headline">{{ formTitle }}</span>
												</v-card-title>

												<v-card-text>
													<!-- 編輯彈跳視窗內容項目開始 -->
													<v-container>
														<v-row>
															<v-col cols="12" sm="6" md="6">
																<v-text-field v-model="editedItem.WorkName" label="工作項目">
																</v-text-field>
															</v-col>
															<v-col cols="12" sm="6" md="6">
																<v-text-field v-model="editedItem.WorkDescription" label="工作內容">
																</v-text-field>
															</v-col>
															<v-col cols="12" sm="12" md="12"> 
                                                                 
                                        <v-select :items="SelectList" v-model="editedItem.OrderMember" item-text="UserName" item-value="UserID"
												label="更換負責人員" solo outlined>
										</v-select>
									
																<%--<v-text-field v-model="editedItem.OrderMember" label="負責人員">
																</v-text-field>--%>
															</v-col>
															<v-col cols="12" sm="12" md="12" class="mx-0 px-0">
																<!-- <v-text-field v-model="editedItem.時程期限" label="時程期限">
																	</v-text-field> <-->
																<span>時程期限</span>
																<v-date-picker v-model="editedItem.DeadLine" label="時程期限"
																	@input="menu2 = false" locale="zh-cn">
																</v-date-picker>
															</v-col>
															<v-row>
																<v-spacer></v-spacer>
																<v-btn color="blue darken-1" text @click="save">儲存
																</v-btn>
																<v-btn color="blue darken-1" text @click="close">取消
																</v-btn>
															</v-row>
														</v-row>
													</v-container>
													<!-- 編輯彈跳視窗內容項目（除了按鈕）結束 -->
												</v-card-text>

												<!-- 編輯彈跳視窗按鈕結束 -->
											</v-card>
										</v-dialog>
										<!-- 彈跳視窗功能 (包含按鍵觸發)結束 -->
									</v-toolbar>
								</template>
								<!-- v-slot 結束							 -->
								<template v-slot:item.修改="{ item }">
									<v-btn outlined tile color="success" @click="editItem(item)">
										<v-icon color="green lighten-1" small class="mr-1">
											mdi-pencil
										</v-icon>
										修改
									</v-btn>
								</template>
								<!-- ------- -->
								<template v-slot:item.刪除="{ item }">
									<v-btn outlined tile color="danger" @click="deleteItem(item)">

										<v-icon color="red darken-2" small class="mr-1">
											mdi-delete
										</v-icon>
										刪除
									</v-btn>
								</template>
							</v-data-table>
							<%--<v-row>
								<v-spacer></v-spacer>
								<v-btn color="primary" 　class="mr-5">
									返回
								</v-btn>
							</v-row>--%>
							<v-pagination v-model="page" :length="pageCount" class="mb-15"></v-pagination>
						</template>
						<!-- ----------------- -->
					</v-main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>

        var vm = new Vue({
            el: '#app',
            vuetify: new Vuetify(),
            data: () => ({
                drawer: false,
                snackbar: false,
                snackbar1: false,
                showmessagesuccess: '我是正確訊息',
                showmessage: '我是錯誤訊息',
                // 
                valid: true, //v-form驗證
                ProjectID: "",
                ProjectName: "",  //要連同 同樣名稱的變數一起更改
                TeamName: "",
                SelectList: [],
                SelectMember: '',
                e1: 1,
                newWorkProject: [{
                    WorkID: "",
                    OrderMember: "",
                    WorkName: "",
                    WorkDescription: "",
                    DeadLine: new Date().toISOString().substr(0, 10),
                },],
                //
                // date: new Date().toISOString().substr(0, 10),
                menu2: false,
                // 
                // 管理分頁變數開始
                page: 1,
                pageCount: 0,
                itemsPerPage: 4,
                // 管理分頁變數結束
                // 彈跳視窗開始
                dialog: false,
                // datepicker
                menu2: false,
                // 彈跳視窗結束					
                // 
                //資料容器: [],
                // json資料結束
                // 判斷是否是修改頁面 會自動改標題開始
                editedIndex: -1,
                // 判斷是否是修改頁面 會自動改標題結束

                editedItem: {
                    WorkID: '',
                    WorkName: '',
                    WorkDescription: '',
                    DeadLine: '',
                    UserID: '',
                },
                defaultItem: {
                    WorkID: '',
                    WorkName: '',
                    WorkDescription: '',
                    DeadLine: '',
                    UserID: '',
                },
                // 變數結束
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
                    text: '負責人員',
                    value: 'OrderMember'
                },
                {
                    text: '',
                    value: '修改',
                    sortable: false
                },
                {
                    text: '',
                    value: '刪除',
                    sortable: false
                },
                ],
            }),
            computed: {
                formTitle() {
                    // computed計算後傳出的值可以直接當data 用鬍子語法被DOM拿到
                    // 抓到的index 若是-1則將標題改為New Item ; 非-1(正常indexof取到大於等於0)時為 Edit Item
                    return this.editedIndex === -1 ? '新增' : '修改'
                },
            },

            watch: {
                dialog(val) {
                    val || this.close()
                },
            },

            created() {
                this.頁面載入()
            },
            methods: {
                enterevent() {
                    this.e1 += 1;
                },
                建立工作項目() {
                    if (this.$refs.form.validate()) {
                        axios.post('API/CreateWorkHandler.ashx', {
                            innertype: 'CreateNewWork',
                            ProjectID: vm.ProjectID,
                            OrderMember: vm.SelectMember,
                            WorkName: vm.newWorkProject.WorkName,
                            WorkDescription: vm.newWorkProject.WorkDescription,
                            DeadLine: vm.newWorkProject.DeadLine
                        })
                            .then(response => {
                                if (!response.data.success) {
                                    vm.showmessagesuccess = '發送成功';
                                    vm.snackbar1 = true;
                                    vm.頁面載入();
                                    vm.刷新stepper();
                                }
                                else {
                                    alert(response.data.success);
                                }

                            })
                            .catch(error => {
                                vm.showmessage = '發送失敗' + error;
                                vm.snackbar = true;
                                // vm.刷新stepper();
                            })
                    }
                },
                // 獲取資訊() {
                // 	axios.post('API/GetCrudHandler.ashx', {
                // 			innertype: '這一頁',
                // 			專案: vm.專案,
                // 			組名: vm.組名,
                // 		})
                // 		.then(response => {
                // 			this.資料容器 = response.data;
                // 		})
                // 		.catch(error => alert(error))
                // },
                save() {
                    // 若是為編輯狀態 editedIndex會有該項的indexof 
                    if (this.editedIndex > -1) {
                        // 把資料灌回DOM
                        // Object.assign(this.資料容器[this.editedIndex], this.editedItem);
                        axios.post('API/CreateWorkHandler.ashx', {
                            Type: 'UpdateWork',
                            ProjectID: vm.editedItem.WorkID,
                            WorkName: vm.editedItem.WorkName,
                            WorkDescription: vm.editedItem.WorkDescription,
                            DeadLine: vm.editedItem.DeadLine,
                            OrderMember: vm.editedItem.OrderMember,
                        })
                            .then(response => {
                                if (!response.data.success) {
                                    vm.showmessagesuccess = '發送成功';
                                    vm.snackbar1 = true;
                                    this.頁面載入();
                                    this.close();
                                }
                                else {
                                    alert(response.data.success);
                                }
                            })
                            .catch(error => {
                                vm.showmessage = '發送失敗' + error;
                                vm.snackbar = true;
                                this.close()
                                    ;
                            })
                        // alert(this.editedItem)
                    }
                    // 若是為新增狀態 預設editedIndex -1 不處理 直接推新的editedIndex和editedItem
                    else {
                        // this.資料容器.push(this.editedItem)
                    }
                    this.close()
                },
                刷新stepper() {
                    this.$refs.form.reset();
                    this.e1 = 1
                },
                editItem(item) {
                    // FOR循環找到item(此for循環物件)的index
                    this.editedIndex = this.newWorkProject.indexOf(item)
                    // 抓到此item後 
                    // Object.assign() 方法用于将所有可枚举属性的值从一个或多个源对象source复制到目标对象。它将返回目标对象target。
                    // 例子
                    // const v1 = 'abc' const v2 = true const v3 = 10 
                    // const obj = Object.assign({}, v1, v2, v3)
                    // obj // { "0": "a", "1": "b", "2": "c" }

                    this.editedItem = Object.assign({}, item)
                    this.dialog = true
                },

                deleteItem(item) {
                    // 宣告此index數字為此筆  之後可能會有若中間資料庫有人增刪的bug
                    //const index = this.資料容器.indexOf(item)
                    if (confirm('確定要刪除此資料嗎?')) {
                        axios.post('API/CreateWorkHandler.ashx', {
                            Type: 'DeleteWork',
                            WorkID: item.WorkID
                        }).then(response => {
                            // alert(item.ProjectID + '發送刪除成功');
                            vm.showmessagesuccess = '發送刪除成功';
                            vm.snackbar1 = true;
                            this.頁面載入();
                            // this.inneritem.splice(index, 1);
                        })
                            .catch(error =>
                            // alert('id:' + item.ProjectID + '發送刪除失敗')
                            {
                                vm.showmessage = '發送刪除失敗';
                                vm.snackbar = true;
                            }
                            )
                    } else { }
                },

                close() {
                    this.dialog = false
                    this.$nextTick(() => {
                        // 把彈跳視窗輸入框清空成defaultItem
                        this.editedItem = Object.assign({}, this.defaultItem)
                        // 恢復成新增模式
                        this.editedIndex = -1
                    })
                },

                ChangeTName() {
                    var TName = prompt("請輸入欲變更組名");
                    if (TName) {
                        axios
                            .post('API/CreateWorkHandler.ashx', {
                                innertype: 'ChangeTeamName',
                                TeamName: TName,
                                ProjectID: vm.ProjectID
                            })
                            .then(response => {
                                vm.頁面載入();
                            })
                            .catch(function (error) {
                                {
                                    vm.showmessage = '加載失敗' + error;
                                    vm.snackbar = true;
                                }
                            });
                    };
                },

                // 初始化資料
                頁面載入() {
                    axios
                        .post('API/GetCrudHandler.ashx', {
                            innertype: 'CreateWorks',
                            ClassNumber: 'none'
                        })
                        .then(response => {
                            vm.ProjectID = response.data.ProjectID;
                            vm.ProjectName = response.data.ProjectName;
                            vm.TeamName = response.data.TeamName;
                            vm.SelectList = response.data.SelectList;
                            vm.newWorkProject = response.data.newWorkProject;
                        })
                        .catch(function (error) {
                            {
                                vm.showmessage = '加載失敗' + error;
                                vm.snackbar = true;
                            }
                        });
                },
            }
        })
        $("#ChangeTeamName_btn").click(function () {
            vm.ChangeTName();
        });
    </script>
</asp:Content>
