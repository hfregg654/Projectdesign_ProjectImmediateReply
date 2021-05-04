<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAssignTeam.ascx.cs" Inherits="ProjectImmediateReply.ucAssignTeam" %>
<v-main>
						<v-row>
							<v-col>
							</v-col>
							<v-col sm="6" xs="12">
								<v-select :items="chooseclass"　@change="changeRoute" label="選擇班級" solo outlined></v-select>
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
										<!-- <v-toolbar-title>專案進度X%</v-toolbar-title>
										<v-btn>結案</v-btn> -->
										<!-- <v-divider class="mx-4" inset vertical></v-divider> -->
										<v-spacer></v-spacer>
										<!-- 彈跳視窗功能 (包含按鍵觸發)開始 -->
										<v-btn color="primary" dark class="mb-2" @click="randam()">小組亂數分配</v-btn>
										
									</v-toolbar>
								</template>
								<!-- v-slot 結束							 -->
								<!-- 小組名插槽1 -->
								<template v-slot:item.choosegroup="{ item }">
									<v-select
									          
									          :items="choosegroup"
									          prepend-icon="mdi-dialpad"
									          menu-props="auto"
									          label="小組名"
									          single-line
									        ></v-select>
								</template>
								
								
							</v-data-table>

							<v-pagination v-model="page" :length="pageCount"></v-pagination>
								<v-row>
									<v-spacer></v-spacer>
							<v-btn color="primary" dark class="mb-2 mr-10"　@click="store">
								儲存</v-btn>
								</v-row>
						</template>

					</v-main>