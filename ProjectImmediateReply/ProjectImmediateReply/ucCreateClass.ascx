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
									required></v-text-field>
									
									<v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="人數"
									type="number"
									v-model="License" 
									required></v-text-field>
								</v-card-text>
								<v-card-actions class="justify-center">
									<v-btn depressed color="primary" type="submit" @click="submit" :disabled="!valid"
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
</v-main>


