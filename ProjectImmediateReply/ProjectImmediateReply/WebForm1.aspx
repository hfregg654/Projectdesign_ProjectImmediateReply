﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ProjectImmediateReply.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- meta引入 -->
    <meta name="viewport"
        content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui">
    <meta charset="UTF-8">
    <meta http-equiv="content-type" content="text/html; charset=utf-8">
    <meta http-equiv="content-language" content="zh-tw">
    <meta name="viewport"
        content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <!-- ---------- -->
    <link rel="stylesheet" type="text/css"
        href="https://cdn.jsdelivr.net/npm/@mdi/font@5.x/css/materialdesignicons.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Material+Icons" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify@2.3.10/dist/vuetify.min.css" rel="stylesheet">
    <!-- 導入中文字體 -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+TC:wght@900&display=swap" rel="stylesheet">
    <!-- 導入英文字體 -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Lemonada:wght@700&display=swap" rel="stylesheet">
</head>
<body>
    <div id="app">
        <v-app id="inspire">
				<v-app id="keep">
					<v-app-bar app clipped-left color="black">
						<v-app-bar-nav-icon @click="drawer = !drawer" color="white"></v-app-bar-nav-icon>
						<v-spacer></v-spacer>
						<span class="title mr-5">
							<font color="#0069CE" class="ubay">u</font>
							<font color="#00A65A" class="ubay">B</font>
							<font color="#EEC803" class="ubay">A</font>
							<font color="#EC475A" class="ubay">Y</font>
						</span>
						<v-spacer></v-spacer>
					</v-app-bar>
					<!-- 導航欄開始 -->
					<v-navigation-drawer v-model="drawer" app clipped color="grey lighten-4">
						<v-card max-width="375" class="mx-auto" height="100%">
							<v-img
								src="https://www.morganstanley.com/pub/content/dam/msdotcom/ideas/rise-of-the-tech-super-platforms/tw-rise-of-tech.jpg"
								height="250px" dark>
								<v-row class="fill-height">
									<v-card-title>
										<!-- <v-btn dark icon>
						            <v-icon>mdi-chevron-left</v-icon>
						          </v-btn> -->

										<v-spacer></v-spacer>

									</v-card-title>

									<v-spacer></v-spacer>

									<v-card-content class="white--text mb-3 pr-8 mt-7">
										<v-list-item-avatar size="90" class="ml-6">
											<img
												src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQV-9p7LkFhpIsdRTn3uij6E50Nn-4SQZO3sA&usqp=CAU">
										</v-list-item-avatar>
										<v-list-item class="mr-7 mt-5">
											<div class="h1 navfont pr-4">歡迎您</div><br>
											<div class="h1 navfont pr-4">XXXX</div>
										</v-list-item>
									</v-card-content>
								</v-row>
							</v-img>

							<v-list two-line>
								<v-list-item @click="" href="#1">
									<v-list-item-icon>
										<v-icon color="primary">face</v-icon>
									</v-list-item-icon>

									<v-list-item-content>
										<v-list-item-title class="chinese h4 primary--text">個人資料維護</v-list-item-title>
										<v-list-item-subtitle>Mobile</v-list-item-subtitle>
									</v-list-item-content>

								</v-list-item>


								<v-divider inset></v-divider>
								<!-- 專案選單開始 -->
								<v-list-item @click="" href="#2">
									<v-list-item-icon>
										<v-icon color="primary">assignment_ind</v-icon>
									</v-list-item-icon>

									<v-list-item-content>
										<v-list-item-title class="chinese h4 primary--text">建立班級及修改</v-list-item-title>
										<v-list-item-subtitle>Personal</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click="" href="#2">
									<v-list-item-icon>
										<v-icon color="primary">assignment_turned_in</v-icon>
									</v-list-item-icon>

									<v-list-item-content>
										<v-list-item-title class="chinese h4 primary--text">建立專案及修改</v-list-item-title>
										<v-list-item-subtitle>Personal</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-list-item @click="" href="#2">
									<v-list-item-icon>
										<v-icon color="primary">today</v-icon>
									</v-list-item-icon>

									<v-list-item-content>
										<v-list-item-title class="chinese h4 primary--text">小組分配及修改</v-list-item-title>
										<v-list-item-subtitle>Personal</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>
								<!-- 專案選單結束 -->
								<v-list-item @click="" href="#3">
									<v-list-item-icon>
										<v-icon color="primary">receipt</v-icon>
									</v-list-item-icon>

									<v-list-item-content>
										<v-list-item-title class="chinese h4 primary--text">成績</v-list-item-title>
										<v-list-item-subtitle>Work</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

								<v-divider inset></v-divider>

							</v-list>
						</v-card>
					</v-navigation-drawer>
					<!-- 導航欄結束 -->
					<v-main>




						<template>
							<v-row justify="center">
								<v-dialog v-model="dialog" persistent max-width="600px">
									<template v-slot:activator="{ on, attrs }">
										<v-btn color="blue" dark text v-bind="attrs" v-on="on">
											上傳連結或檔案
										</v-btn>
									</template>
									
									
									<v-card>
										<v-img src="https://wallpaperaccess.com/full/1381091.jpg" height="410">
										<v-card-title>
											<v-spacer></v-spacer>
											<span class="headline">XXX專案</span>
											<span class="headline ml-2">組名XXX</span>
											<v-spacer></v-spacer>
										</v-card-title>
										<v-card-text>
											<v-container>
												<v-row>
													<v-col cols="12" sm="6" md="6" class="mt-5">
														<v-spacer></v-spacer>
														<span class="h6">工作項目</span>
														<span class="h6 ml-6">XXXX</span>
														<v-spacer></v-spacer>
													</v-col>
													
													<v-col cols="12" sm="6" md="6">
														<v-file-input
															v-model="files"
															placeholder="請選擇檔案"
															label="上傳檔案"
															accept=".jpg"
															show-size 
															counter 
															chips 
															multiple
															prepend-icon="mdi-paperclip"
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
													
													<v-col cols="12" sm="6" md="6">
														<v-text-field label="上傳網址"
														v-model="urls"
															hint="檔案大小須小於?MB" persistent-hint
															required></v-text-field>
													</v-col>
													
													<!-- <v-col cols="12" sm="6">
														<v-select :items="['0-17', '18-29', '30-54', '54+']"
															label="Age*" required></v-select>
													</v-col> -->
													
												</v-row>
											</v-container>
											<!-- <small>*indicates required field</small> -->
										</v-card-text>
										<v-card-actions>
											<v-spacer></v-spacer>
											<v-btn color="blue darken-1" text @click="上傳()">上傳</v-btn>
											<v-btn color="blue darken-1" text @click="dialog = false">取消</v-btn>
										</v-card-actions>
										</v-img>
									</v-card>
									
									
								</v-dialog>
							</v-row>
						</template>






					</v-main>
					<!-- footer -->
					<v-footer dark padless>
						<v-card class="flex" flat tile>

							<v-card-text class="py-2 white--text text-center">
								<p class="flow-text grey-text center footer-ubay h6">Copyright&copy;2021,
									<font color="#0069CE">u
									</font>
									<font color="#00A65A">B</font>
									<font color="#EEC803">A</font>
									<font color="#EC475A">Y</font> All Rights Reserved
								</p>
							</v-card-text>
						</v-card>
					</v-footer>
				</v-app>
			</v-app>
    </div>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/babel-polyfill/dist/polyfill.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/vue@2.x/dist/vue.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/vuetify@2.3.10/dist/vuetify.min.js"></script>
    <!-- 導入AXIOS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/axios/0.21.1/axios.min.js"></script>
    <script>
        var vm = new Vue({
            el: '#app',
            vuetify: new Vuetify(),
            data: () => ({
                drawer: null,
                // 控制彈跳視窗
                dialog: false,
                // ----------
                // 上傳檔案後顯示的小卡值
                files: [],
                // 上傳網址
                urls: "",
            }),
            methods: {
                上傳() {
                    if (this.files) {
                        let formData = new FormData();

                        // files
                        for (let file of this.files) {
                            formData.append("files", file, file.name);
                        }
                        axios
                            .post("API/GetFileHandler.ashx", formData)
                            .then(response => {
                                console.log("Success!");
                                alert({
                                    response
                                });
                            })
                            .catch(error => {
                                alert({
                                    error
                                });
                            });
                    } else {
                        console.log("there are no files.");
                    }
                }
            },
        })
    </script>
    <style type="text/css">
        #keep .v-navigation-drawer__border {
            display: none
        }

        .ubay {
            font-family: 'Lemonada', cursive;
            font-weight: 900;
            font-size: 3.125rem;
            padding-left: 0.9375rem;
            margin-left: -0.3125rem;
        }

        .navfont {
            font-family: 'ZCOOL KuaiLe', cursive;
            font-weight: bold;
        }

        .footer-ubay {
            font-family: 'Lemonada', cursive !important;
        }

        .top {
            background: rgba(0, 0, 0, .6) !important;
        }

        .chinese {
            font-family: 'Noto Sans TC', sans-serif;
            padding-right: 2.375rem;
        }


        @media (max-width:600px), print {
            .ubay {
                font-size: 2.125rem;
                padding: 0.125rem
            }

            .footer-ubay {
                font-family: 'Anton', sans-serif;
                font-family: 'Shadows Into Light', cursive;
                font-size: 1rem;
            }
        }
    </style>
</body>

</html>
