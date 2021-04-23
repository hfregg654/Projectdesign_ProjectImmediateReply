﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="ProjectImmediateReply.WebForm3" %>

<!DOCTYPE html>
<html>
<%--	<head>
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
	</head>--%>
<%--	<body>
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
					</v-navigation-drawer>--%>
					<!-- 導航欄結束 -->
<%--					<v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<v-form>
							<v-card class="mx-auto mt-6 pa-8" max-width="1100" height="500">
								<v-card-text>
									<v-text-field :rules="rules1" class="mt-6" label="專案名" v-model="C3projectname">
									</v-text-field>
									<v-text-field :rules="rules2" class="mt-6" label="小組名稱" v-model="C3teamname">
									</v-text-field>
									<!-- data picker start -->
									<v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40"
										transition="scale-transition" offset-y min-width="auto" class="mt-6">
										<template v-slot:activator="{ on, attrs }">
											<v-text-field v-model="date" label="時程期限" prepend-icon="mdi-calendar"
												readonly v-bind="attrs" v-on="on">
											</v-text-field>
										</template>
										<v-date-picker v-model="date" @input="menu2 = false" locale="zh-cn">
										</v-date-picker>
									</v-menu>
									<!-- data picker over -->
								</v-card-text>
								<v-card-actions class="justify-center">
									<v-btn depressed color="primary" type="submit" @click=""
										class="justify-center h1 font-weight-black">
										建立
									</v-btn>
								</v-card-actions>
								<v-card-actions class="justify-end">
									<v-btn color="blue lighten-2" text>
										專案修改
									</v-btn>
								</v-card-actions>

							</v-card>
						</v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->
					</v-main>--%>
					<!-- footer -->
<%--					<v-footer dark padless>
						<v-card class="flex" flat tile>

							<v-card-text class="py-2 white--text text-center">
								<p class="flow-text grey-text center footer-ubay">Copyright&copy;2021,
									<font color="#0069CE">u
									</font>
									<font color="#00A65A">B</font>
									<font color="#EEC803">A</font>
									<font color="#EC475A">Y</font> All Rights Reserved
								</p>
							</v-card-text>
						</v-card>
					</v-footer>--%>
<%--				</v-app>
			</v-app>
		</div>--%>
		<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/babel-polyfill/dist/polyfill.min.js"></script>
		<script src="https://cdn.jsdelivr.net/npm/vue@2.x/dist/vue.js"></script>
		<script src="https://cdn.jsdelivr.net/npm/vuetify@2.3.10/dist/vuetify.min.js"></script>
		<script>
            new Vue({
                el: '#app',
                vuetify: new Vuetify(),
                data: () => ({
                    drawer: null,
                    // 班級選擇框
                    // -------
                    // -------
                    //------input規則
                    rules1: [
                        value => !!value || '此輸入框不可為空白',
                        value => (value || '').length <= 20 || '請輸入20個字元以內',
                    ],
                    rules2: [
                        value => !!value || '此輸入框不可為空白',
                        value => (value || '').length <= 20 || '請輸入20個字元以內',
                    ],
                    // 加入data picker
                    date: new Date().toISOString().substr(0, 10),

                }),
            })
        </script>
<%--		<style type="text/css">
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

			/* card start */
			.v-card--reveal {
				bottom: 0;
				opacity: 1 !important;
				position: absolute;
				width: 100%;
			}

			/* card over */

			@media (max-width:600px),
			print {
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
		</style>--%>
	</body>
</html>

