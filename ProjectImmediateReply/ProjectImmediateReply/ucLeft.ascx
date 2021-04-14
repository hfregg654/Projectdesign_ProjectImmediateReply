<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLeft.ascx.cs" Inherits="ProjectImmediateReply.WebUserControl1" %>
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
