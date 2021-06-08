<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAssignTeam.ascx.cs" Inherits="ProjectImmediateReply.ucAssignTeam" %>
<v-main>
						<v-row>
							<v-col>
							</v-col>
							<v-col sm="6" xs="12">
								<v-select :items="chooseclass" v-model="classchoice"　@change="changeRoute" label="選擇班級" solo outlined></v-select>
							</v-col>
							<v-col>
							</v-col>
						</v-row>
						<!-- 下面是table標題和table -->
						<template>
							<v-data-table @page-count="pageCount = $event" :page.sync="page"
								:items-per-page="itemsPerPage" hide-default-footer :headers="headers" :items="inneritem"
								sort-by="calories" class="elevation-1">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat color="white">
										<v-spacer></v-spacer>
										<!-- 彈跳視窗功能 (包含按鍵觸發)開始 -->
											<v-btn color="primary" dark class="mb-2" @click="random()" id="randombtn">小組亂數分配</v-btn>
									</v-toolbar>
								</template>
								<!-- v-slot 結束							 -->
								<!-- 小組名插槽1 -->
<%--							不抓前台資料 「:items = "item.choosegroup"」 --> 抓後台資料  「:items="item.TeamNameGroup"」 --%>
<%--								vue.js內的 v-select套件 :items 傳一個是label 傳多個會變成下拉式選單  v-model為使用者對應值--%>
								<template #item.choosegroup="{ item }">
									<v-select
									          :items="item.TeamNameGroup"
											  v-model="item.TeamName"
									          prepend-icon="toc"
									          menu-props="auto"
									          label="小組名"
									          single-line
									        ></v-select>
								</template>
								
								
							</v-data-table>
							<p id="leaderp">★為組長</p>
							<v-pagination v-model="page" :length="pageCount"></v-pagination>
								<v-row>
									<v-spacer></v-spacer>
							<v-btn color="primary" dark class="mb-2 mr-10 justify-end mb-15"　@click="store" id="savebtn" >
								儲存</v-btn>
								</v-row>
						</template>

					</v-main>