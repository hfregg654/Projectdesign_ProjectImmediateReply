<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCreateProject.ascx.cs" Inherits="ProjectImmediateReply.ucCreateProject" %>
					<v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<%--v-model=語法  "classchoice"=屬性--%>
						<v-form>
							<v-card class="mx-auto mt-6 pa-8" max-width="1100" height="500">
								<v-card-text>
									<v-select 
										:items="chooseclass" 
										v-model="classchoice" 
										label="班別" 
										:rules="classrules"
										solo required
										id ="ClassNumber">
									</v-select>
									<v-text-field 
										:rules="rules1" 
										class="mt-6" 
										label="專案名"
										v-model="C3projectname"
										id ="ProjectNameTbox">
									</v-text-field>
<%--									<v-text-field 
										:rules="rules2" 
										class="mt-6" 
										label="小組名稱" 
										v-model="C3teamname"
										id ="TeamNameTbox">
									</v-text-field>--%>
									<!-- data picker start -->
									<v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40"
										transition="scale-transition" offset-y min-width="auto" class="mt-6">
										<template v-slot:activator="{ on, attrs }">
											<v-text-field 
												v-model="date" 
												label="時程期限" 
												prepend-icon="mdi-calendar"
												readonly v-bind="attrs" 
												v-on="on"
												id ="DeadLine">
											</v-text-field>
										</template>
										<v-date-picker v-model="date" @input="menu2 = false" locale="zh-cn">
										</v-date-picker>
									</v-menu>
									<!-- data picker over -->
								</v-card-text>
								<v-card-actions class="justify-center">
									<v-btn 
										depressed color="primary" 
										type="button" 
										class="justify-center h1 font-weight-black"
										id = "CreateProjectbtn">
										建立
									</v-btn>
									<br />
									<span style="color: red">
										<literal id="CreateProjectMessage"></literal>
									</span>
								</v-card-actions>
								<v-card-actions class="justify-end">
									<v-btn color="blue lighten-2" text href="./ProjectDetail.aspx" >
										專案修改
									</v-btn>
								</v-card-actions>

							</v-card>
						</v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->