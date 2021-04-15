<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUpdateInfo.ascx.cs" Inherits="ProjectImmediateReply.ucUpdateInfo" %>
<v-main>
    <v-form v-model="valid" ref="form" lazy-validation>
							<v-card class="mx-auto mt-6 pa-8" max-width="1100">
								<v-card-text>
									<v-text-field 
									:rules="classrules" 
									class="mt-6" 
									label="姓名"
									type="text"
									v-model="ClassNumber" 
									required></v-text-field>
									
									<v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="電話號碼"
									type="text"
									v-model="License" 
									required></v-text-field>

                                    <v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="電子郵件"
									type="text"
									v-model="License" 
									required></v-text-field>

                                    <v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="LineID"
									type="text"
									v-model="License" 
									required></v-text-field>

                                    <v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="原密碼"
									type="text"
									v-model="License" 
									required></v-text-field>

                                    <v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="新密碼"
									type="text"
									v-model="License" 
									required></v-text-field>

                                     <v-text-field 
									:rules="numberrules" 
									class="mt-6" 
									label="新密碼確認"
									type="text"
									v-model="License" 
									required></v-text-field>

								</v-card-text>
								<v-card-actions class="justify-center">
									<v-btn depressed color="primary" type="submit" @click="submit" :disabled="!valid"
										class="justify-center h1 font-weight-black">
										儲存
									</v-btn>
								</v-card-actions>
								<v-card-actions class="justify-end">
									<v-label color="blue lighten-2" text>
										授權碼 XXXXX
									</v-label>
								</v-card-actions>

							</v-card>
						</v-form>




</v-main>
