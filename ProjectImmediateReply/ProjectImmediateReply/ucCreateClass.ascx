<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCreateClass.ascx.cs" Inherits="ProjectImmediateReply.ucCreateClass" %>
<v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<v-form v-model="valid" ref="form" lazy-validation>
							<v-card class="mx-auto mt-6 pa-8" max-width="1100">
								<v-card-text>
									<v-text-field 
									:rules="classrules" 
									class="mt-6" 
									label="班別"
									type="text"
									v-model="ClassNumber"
									required id="ClassNumberTbox"></v-text-field>
									
									<v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="人數"
									type="number"
									v-model="License" 
									required id="PeopleNumTbox"></v-text-field>
								</v-card-text>
								<v-card-actions class="justify-center">
									<v-btn id="CreatClassbtn" depressed color="primary" type="button"  :disabled="!valid"
										class="justify-center h1 font-weight-black">
										建立
									</v-btn>
									<span style="color: red">
										<literal id="CreateClassMessage"></literal>
									</span>
								</v-card-actions>
								<v-card-actions class="justify-end">
									<v-btn color="blue lighten-2" text href="./ClassDetail.aspx">
										班級修改
									</v-btn>
								</v-card-actions>

							</v-card>
						</v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->
</v-main>


