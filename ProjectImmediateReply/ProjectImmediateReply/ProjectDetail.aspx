<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="ProjectDetail.aspx.cs" Inherits="ProjectImmediateReply.ProjectDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <v-main
        style="background: -webkit-linear-gradient(right, #FFAF7B,#EE82EE, #FFB6C1); background: linear-gradient(to right, #FFAF7B, #FFB6C1);">
        
        
        <div class="text-center">
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





						<v-container>
							<v-row style="height:2vh">
								<v-spacer></v-spacer>
								<p class="font-weight-bold">
									專案修改</p>
								<v-spacer></v-spacer>
							</v-row>
							<v-row style="height:57px">
								<v-col>
								</v-col>
								<v-col sm="6" xs="12">
									<v-select @change="changeRoute()" :items="chooseclass" v-model="classchoice"
										label="選擇班級" solo outlined></v-select>
								</v-col>
								<v-col>
								</v-col>
							</v-row>
						</v-container>
						<!-- 下面是table標題和table -->
						<template>
							<v-data-table
								style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);"
								height="45vh" @page-count="pageCount = $event" :page.sync="page"
								:items-per-page="itemsPerPage" hide-default-footer :headers="headers" :items="inneritem"
								sort-by="ProjectID" class="elevation　table-striped　
								">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat
										style="background: -webkit-linear-gradient(right, #FFAF7B, #FFB6C1);background: linear-gradient(to right, #FFAF7B, #FFB6C1);">
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
																<v-text-field v-model="editedItem.ProjectName" label="專案名稱">
																</v-text-field>
															</v-col>
															<v-col cols="12" sm="6" md="6">
																<v-text-field v-model="editedItem.TeamName" label="小組名稱">
																</v-text-field>
															</v-col>
															<v-col cols="12" sm="12" md="12" class="mx-0 px-0">
																<!-- <v-text-field v-model="editedItem.DeadLine" label="時程期限">
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
									<v-btn outlined tile color="success" @click="editItem(item)" v-show="item.TeamName">
										<v-icon color="green lighten-1" small class="mr-1">
											mdi-pencil
										</v-icon>
										修改
									</v-btn>
								</template>
								<!-- ------- -->
								<template v-slot:item.刪除="{ item }">
									<v-btn outlined tile color="danger" @click="deleteItem(item)" v-show="!item.TeamName">

										<v-icon color="red darken-2" small class="mr-1">
											mdi-delete
										</v-icon>
										刪除
									</v-btn>
								</template>
							</v-data-table>
							<v-row>
							<v-spacer></v-spacer>
							<v-btn color="primary" href="./Index.aspx?PageInnerType=CreateProject"　class="mr-5" >
								返回
							</v-btn>
							</v-row>
							<v-pagination v-model="page" :length="pageCount"></v-pagination>
						</template>

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
                // -----
                // 上方班級選擇開始
                chooseclass: ['班級A', '班級B', '班級C', '班級D'],
                classchoice: '',
                // 變數開始
                // 管理分頁變數開始
                page: 1,
                pageCount: 0,
                itemsPerPage: 5,
                // 管理分頁變數結束
                // 彈跳視窗開始
                dialog: false,
                // datepicker
                menu2: false,
                // 彈跳視窗結束					
                // 
                inneritem: [],
                // json資料結束
                // 判斷是否是修改頁面 會自動改標題開始
                editedIndex: -1,
                // 判斷是否是修改頁面 會自動改標題結束

                editedItem: {
                    ProjectID: '',
                    ProjectName: '',
                    TeamName: '',
                    DeadLine: '',
                },
                defaultItem: {
                    ProjectID: '',
                    ProjectName: '',
                    TeamName: '',
                    DeadLine: '',
                },
                // 變數結束
                headers: [{
                    text: '專案名稱',
                    align: 'start',
                    value: 'ProjectName',
                },
                {
                    text: '小組名稱',
                    value: 'TeamName'
                },
                {
                    text: '時程期限',
                    value: 'DeadLine'
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
                this.initialize()
            },

            methods: {
                // 監聽select值若發生變化
                changeRoute() {
                    axios.post('API/GetCrudHandler.ashx', {
                        innertype: 'ProjectDetail',
                        classchoice: vm.classchoice
                    })
                        .then(response => {
                            this.inneritem = response.data;
                            if (vm.inneritem.length != 0) {
                                vm.showmessagesuccess = '完成';
                                vm.snackbar1 = true;
                            } else {
                                vm.showmessagesuccess = '無資料';
                                vm.snackbar1 = true;
                            }

                        })
                        .catch(error => {
                            vm.showmessage = '失敗' + error;
                            vm.snackbar = true;
                        })
                },
                // 
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
                    this.dialog = true
                },

                deleteItem(item) {
                    // 宣告此index數字為此筆  之後可能會有若中間資料庫有人增刪的bug
                    const index = this.inneritem.indexOf(item)
                    if
                        (confirm('確定要刪除此資料嗎?')) {
                        axios.post('API/ProjectDetailHandler.ashx', {
                            Type: 'Delete',
                            id: item.ProjectID
                        }).then(response => {
                            vm.showmessagesuccess = item.ProjectID + '發送刪除成功';
                            vm.snackbar1 = true;
                            this.changeRoute();
                            // this.inneritem.splice(index, 1);
                        })
                            .catch(error =>
                            // alert('id:' + item.ProjectID + '發送刪除失敗')
                            {
                                vm.showmessage = '刪除失敗：' + item.ProjectID + '發送刪除失敗';
                                vm.snackbar = true;
                            })
                    } else {

                    }


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

                save() {
                    // 若是為編輯狀態 editedIndex會有該項的indexof 
                    if (this.editedIndex > -1) {
                        // 把資料灌回DOM
                        // Object.assign(this.inneritem[this.editedIndex], this.editedItem);
                        axios.post('API/ProjectDetailHandler.ashx', {
                            Type: 'Edit',
                            id: this.editedItem.ProjectID,
                            ProjectName: this.editedItem.ProjectName,
                            TeamName: this.editedItem.TeamName,
                            DeadLine: this.editedItem.DeadLine
                        })
                            .then(response => {
                                if (!response.data.success) {
                                    vm.showmessagesuccess = '發送成功';
                                    this.changeRoute();
                                    this.close();
                                }
                                else {
                                    alert(response.data.success);
                                }
                            })
                            .catch(error => {
                                vm.showmessage = '發送失敗' + error;
                                vm.snackbar = true;
                                this.close();
                            })
                        // alert(this.editedItem)
                    }
                    // 若是為新增狀態 預設editedIndex -1 不處理 直接推新的editedIndex和editedItem
                    else {
                        // this.inneritem.push(this.editedItem)
                    }
                    this.close()
                },
                // 初始化資料
                initialize() {
                    axios
                        .post('API/GetCrudHandler.ashx', { innertype: 'Detail', classchoice: '' })
                        .then(response => {
                            console.table(response.data.chooseclass)
                            this.chooseclass = response.data.chooseclass;
                        })
                        .catch(function (error) {
                            {
                                vm.showmessage = '加載失敗' + error;
                                vm.snackbar = true;
                            }
                        });

                    this.inneritem = []
                },

            },

        })
    </script>
</asp:Content>
