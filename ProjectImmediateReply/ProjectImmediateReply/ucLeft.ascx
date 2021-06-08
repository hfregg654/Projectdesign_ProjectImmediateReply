<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLeft.ascx.cs" Inherits="ProjectImmediateReply.WebUserControl1" %>
<!-- 導航欄開始 -->
<%--drawer 左選單的DOM ID部分--%>
<v-navigation-drawer v-model="drawer" app clipped color="grey lighten-4">
						<v-card max-width="375" class="mx-auto" height="100%">
							<v-img
								src="https://www.morganstanley.com/pub/content/dam/msdotcom/ideas/rise-of-the-tech-super-platforms/tw-rise-of-tech.jpg"
								height="150px" dark>
								<v-row class="fill-height">
									<v-card-title>
										<v-spacer></v-spacer>
									</v-card-title>
									<v-spacer></v-spacer>
									<v-card-content class="white--text mb-3 pr-8 mt-3 text-truncate">

										<v-list-item class="mr-7 mt-1">
											<div class="h1 navfont pr-4">歡迎您</div><br>
											<div class="h1 navfont pr-4"><asp:label text="" runat="server" ID="LabelUserName" /></div>
										</v-list-item>
										<v-list-item class="justify-center">
										<button runat="server" id="Logoutbtn" onserverclick="Logoutbtn_ServerClick" type="button" class="ml-10 v-btn v-btn--depressed theme--dark v-size--default primary">
											<span class="v-btn__content">登出</span>
										</button>
											</v-list-item>
									</v-card-content>

								</v-row>
							</v-img>
						


							<div runat="server" id="divLeftTitle">

							

							</div>


						</v-card>
					</v-navigation-drawer>
<!-- 導航欄結束 -->
