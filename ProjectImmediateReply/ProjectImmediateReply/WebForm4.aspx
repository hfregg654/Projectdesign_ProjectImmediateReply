<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="ProjectImmediateReply.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div id="app">
			<v-app id="inspire">
				<v-app id="keep">
				
					<v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<v-form method="post" novalidate="true" ref="form">
							<v-card class="mx-5 my-3 px-4 pb-0 pt-4" max-width="1100">
								<v-card-title class="justify-center">
								      成績
								    </v-card-title>
								<v-card-text>
									<v-select height="30" :items="chooseclass" v-model="classchoice" label="班別" :rules="classrules"
										solo required></v-select>
								<!-- 隱藏第一區，需等班別有值再顯示 -->
								<div id="showarea1" v-if="classchoice.length">
									<v-select height="30" :items="choosegroup" v-model="group" label="小組" :rules="classrules" solo
										required></v-select>
									<v-select height="30" :items="choosename" v-model="name" label="姓名" :rules="classrules" solo
										required></v-select>
								</div>
								<!-- 隱藏第一區結束 -->
								<!-- 隱藏第二區開始 -->
								<div id="showarea2" v-if="group.length && name.length">
								<v-row>
										<v-spacer></v-spacer>
										<span class="h3">電子郵件</span>
										<v-spacer></v-spacer>
								</v-row>
								<v-row>
									<v-spacer></v-spacer>
										<p class="subtitle-2" v-model="email">{{email}}</p>
									<v-spacer></v-spacer>
								</v-row>
								<v-row>
									<v-spacer></v-spacer>
										<span>成績</span>
										<span class="subtitle-2 mx-2" style="font-size: 2rem!important;" v-model="score">{{score}}</span>
										<span>分</span>
									<v-spacer></v-spacer>
								</v-row>
									<v-flex xs12 md12>
										<span　class="pa=0">評語</span>
										<v-expansion-panels v-model="panel" multiple>
											<v-expansion-panel class="mb-2">
												<v-expansion-panel-header>社長評語</v-expansion-panel-header>
												<v-expansion-panel-content>
													<p class="font-weight-medium text-justify">
												           {{boss}}
													</p>	
												</v-expansion-panel-content>
											</v-expansion-panel>

											<v-expansion-panel>
												<v-expansion-panel-header>PM評語</v-expansion-panel-header>
												<v-expansion-panel-content>
													<p class="font-weight-medium text-justify">
													       {{pm}}
													</p>
												</v-expansion-panel-content>
											</v-expansion-panel>

										</v-expansion-panels>
									</v-flex>
								</div>
								<!-- 隱藏第二區結束 -->

								</v-card-text>
								
								<v-card-actions class="justify-end">
									<v-btn color="blue lighten-2" text　href="/abcde" target="_blank">
										分數修改
									</v-btn>
								</v-card-actions>

							</v-card>
						</v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->
					</v-main>
					<!-- footer -->
					
				</v-app>
			</v-app>
		</div>
		<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/babel-polyfill/dist/polyfill.min.js"></script>
		<script src="https://cdn.jsdelivr.net/npm/vue@2.x/dist/vue.js"></script>
		<script src="https://cdn.jsdelivr.net/npm/vuetify@2.3.10/dist/vuetify.min.js"></script>
		<!-- 導入AXIOS -->
		<script src="https://cdnjs.cloudflare.com/ajax/libs/axios/0.21.1/axios.min.js"></script>
		<script>
			new Vue({
				el: '#app',
				vuetify: new Vuetify(),
				data: () => ({
					drawer: null,
					valid: true,
					chooseclass: ['班級A', '班級B', '班級C', '班級D'],
					choosegroup: ['小組A', '小組B', '小組C', '小組D'],
					choosename: ['A', 'B', 'C', 'D'],
					rules1: [
						value => !!value || '此輸入框不可為空白',
					],
					classrules: [
						value => !!value || '此輸入框不可為空白',
					],
					classchoice: "",
					group: "",
					name: "",
					email:"yes123yes123yes123@yahoo.com.tw",
					score:"82",
					boss:"社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語社長評語",
					pm:"PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語PM評語",
					panel:[],
				}),
				methods: {
					
				}
			})
        </script>
		
		
    </form>
</body>
</html>
