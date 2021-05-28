<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUpdateInfo.ascx.cs" Inherits="ProjectImmediateReply.ucUpdateInfo" %>
<v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<v-form method="post" novalidate="true" ref="form">
							<v-card class="mx-auto mt-6 pa-8 mb-15" max-width="1100">
								<v-card-text>
									<v-text-field :rules="[v => !!v || '此輸入框不可為空白',v => !/[ ?<>{}@!]/.test(v)||'請勿輸入特殊字元']" label="姓名" type="text"
										v-model="C1name" required></v-text-field>
<%--				vue的判斷式 前端	V=輸入框當下值 v=> object(型態) (!!v 等於 ifv = null) ||就輸出後面的值 --%> 
									<v-text-field :rules="[v => !!v || '此輸入框不可為空白',v => /^\d+$/.test(v)||'請輸入純數字',
									v => (v && v.length === 10) || '請輸入十位數字',]" class="mt-6" label="電話號碼"
										type="text" v-model="C1phone" required></v-text-field>

									<v-text-field :rules="[v => !!v || '此輸入框不可為空白',
        　　　　　　　　　　　　　　　　　v => /.+@.+\..+/.test(v) || '不符合email格式',v => !/[ ?<>{}!]/.test(v)||'請勿輸入特殊字元']" class="mt-6" label="電子郵件" type="email" v-model="C1email"
										required></v-text-field>

									<v-text-field :rules="[v => !!v || '此輸入框不可為空白',v => !/[ ?<>{}!]/.test(v)||'請勿輸入特殊字元']" class="mt-6" label="LineID"
										type="text" v-model="C1lineid" required></v-text-field>

									<v-text-field :rules="[v => !/[ ?<>{}!]/.test(v)||'請勿輸入特殊字元']" class="mt-6" label="原密碼"
										type="password" v-model="C1password" :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" :type="show1 ? 'text' : 'password'" @click:append="show1 = !show1" required></v-text-field>

									<v-text-field :rules="[v => !/[ ?<>{}!]/.test(v)||'請勿輸入特殊字元']" class="mt-6" label="新密碼"
										type="password" v-model="C1newpassword" :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'" :type="show2 ? 'text' : 'password'" @click:append="show2 = !show2" required></v-text-field>

									<v-text-field :rules="[(this.C1newpasswordconfirm === this.C1newpassword || '新密碼與新密碼確認不相符')]" class="mt-6" label="新密碼確認"
										type="password" v-model="C1newpasswordconfirm" :append-icon="show3 ? 'mdi-eye' : 'mdi-eye-off'" :type="show3 ? 'text' : 'password'" @click:append="show3 = !show3" required></v-text-field>
								</v-card-text>
				<!-- 儲存按鈕	 -->
								<v-card-actions class="justify-center">
									<v-btn depressed color="primary" @click="validate()"
										:disabled="!valid" class="justify-center h1 font-weight-black" large>
										儲存
									</v-btn>
								</v-card-actions>
								<v-card-actions class="justify-start">
									<p class="blue--text" v-model="license">授權碼：{{license}}</p>
									<v-spacer></v-spacer>
									<div id="specialaccountbtn">
										<v-btn color="blue lighten-2" text href="./CreateSpecialAccount.aspx" >
											特別帳號及建立
										</v-btn>
									</div>
								</v-card-actions>

							</v-card>
						</v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->
					</v-main>
