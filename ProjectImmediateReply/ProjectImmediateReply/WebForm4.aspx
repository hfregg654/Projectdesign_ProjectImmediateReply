<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="ProjectImmediateReply.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css"
        href="https://cdn.jsdelivr.net/npm/@mdi/font@5.x/css/materialdesignicons.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Material+Icons" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify@2.3.10/dist/vuetify.min.css" rel="stylesheet">
    <!-- 導入中文字體 -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+TC:wght@900&display=swap" rel="stylesheet">
    <meta name="viewport"
        content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui">
</head>
<body>
    <form id="form1" runat="server">
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

								<v-list-item @click="" href="#2">
									<v-list-item-icon>
										<v-icon color="primary">thumbs_up_down</v-icon>
									</v-list-item-icon>

									<v-list-item-content>
										<v-list-item-title class="chinese h4 primary--text">專案評分</v-list-item-title>
										<v-list-item-subtitle>Personal</v-list-item-subtitle>
									</v-list-item-content>
								</v-list-item>

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
					<v-main class="grey lighten-4" id="main">
						<%--<v-row>
							<v-col>
							</v-col>
							<v-col sm="6" xs="12">
								<v-select :items="chooseclass" label="選擇班級" solo outlined></v-select>
							</v-col>
							<v-col>
							</v-col>
						</v-row>
						<v-row>
							<v-spacer></v-spacer>
							<div class="chinese mb-5 mr-15">專案</div>
							<v-spacer></v-spacer>
						</v-row>
						<v-flex md12 class="grey lighten-4 fill-height justify-center ">
							<v-data-table :headers="headers" :items="desserts" :sort-by="['projectname']"
								:sort-desc="[false, true]" multi-sort class="elevation-1">
								<!-- 增加查看事件開始-->
								<template v-slot:item.btn="{item}">
									<v-icon small @click="">person_search</v-icon>
								</template>
								<!-- 增加查看事件結束 -->
								</v-data-table>
								
						</v-flex>--%>
					</v-main>
					<!-- footer -->
					<v-footer dark padless>
						<v-card class="flex" flat tile>

							<v-card-text class="py-2 white--text text-center">
								<p class="flow-text grey-text center footer-ubay">Copyright&copy;2021,
									<font color="#0069CE">u</font>
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
        <script>
            new Vue({
                el: '#app',
                vuetify: new Vuetify(),
                data: () => ({
                    drawer: null,
                    // items: [{
                    // 		icon: 'face',
                    // 		text: 'Notes'
                    // 	},
                    // 	{
                    // 		divider: true
                    // 	},
                    // 	{
                    // 		icon: 'thumbs_up_down',
                    // 		text: 'Create new label'
                    // 	},
                    // 	{
                    // 		divider: true
                    // 	},
                    // 	{
                    // 		icon: 'receipt',
                    // 		text: 'Archive'
                    // 	},
                    // ],
                    // -------datatable
                    // 班級選擇開始
                    chooseclass: ['班級A', '班級B', '班級C', '班級D'],
                    // 班級選擇結束
                    // datatable開始
                    headers: [{
                        text: '專案名稱',
                        align: 'start',
                        sortable: true,
                        value: 'projectname',
                    },
                    {
                        text: '組長',
                        value: 'teamleader'
                    },
                    {
                        text: '組員',
                        value: 'teammember'
                    },
                    {
                        text: '組名',
                        value: 'teamname'
                    },
                    {
                        text: '',
                        value: 'btn',
                        sortable: false
                    },
                    ],
                    desserts: [{
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                    }, {
                        projectname: 'B計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                    }, {
                        projectname: 'C計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'D計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    }, {
                        projectname: 'A計畫',
                        teamleader: '毛豆',
                        teammember: '一二三四五六七',
                        teamname: '第一組',
                        btn: '1%',
                    },],
                    // datatable結束
                }),
            })
        </script>
        <style type="text/css">
            #keep .v-navigation-drawer__border {
                display: none
            }

            .ubay {
                font-family: 'Anton', sans-serif;
                font-family: 'Shadows Into Light', cursive;
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
                font-family: 'Anton', sans-serif;
                font-family: 'Shadows Into Light', cursive;
                font-size: 1.875rem;
            }

            /* .top {
				background: rgba(0, 0, 0, .6) !important;
			} */

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
    </form>
</body>
</html>
