<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplyInSide.Master" AutoEventWireup="true" CodeBehind="CreateSpecialAccount.aspx.cs" Inherits="ProjectImmediateReply.CreateSpecialAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<v-form method="post" novalidate="true" ref="form">
							<v-card class="mx-auto mt-4 pa-8 mb-15" max-width="1100" height="770">
								<v-card-text>
									<v-text-field :rules="[v => !!v || '此輸入框不可為空白']" label="姓名" type="text"
										v-model="C1name" required></v-text-field>

									<v-text-field :rules="[v => !!v || '此輸入框不可為空白',v => /^\d+$/.test(v)||'請輸入純數字',
									v => (v && v.length === 10) || '請輸入十位數字',]" class="mt-1" label="電話號碼"
										type="text" v-model="C1phone" required></v-text-field>

									<v-text-field :rules="[v => !!v || '此輸入框不可為空白',
        　　　　　　　　　　　　　　　　　v => /.+@.+\..+/.test(v) || '不符合email格式',]" class="mt-1" label="電子郵件" type="email" v-model="C1email"
										required></v-text-field>

									<v-text-field :rules="[v => !!v || '此輸入框不可為空白']" class="mt-1" label="LineID"
										type="text" v-model="C1lineid" required></v-text-field>
										
										<v-text-field :rules="[v => !!v || '此輸入框不可為空白']" class="mt-1" label="帳號"
											type="text" v-model="C1account" required></v-text-field>

									<v-text-field :rules="[v => !!v || '此輸入框不可為空白']" class="mt-1" label="密碼"
										type="password" v-model="C1newpassword" :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'" :type="show2 ? 'text' : 'password'" @click:append="show2 = !show2" required></v-text-field>

									<v-text-field :rules="[(this.C1newpasswordconfirm === this.C1newpassword || '密碼與密碼確認不相符'),[v => !!v || '此輸入框不可為空白']]" class="mt-1" label="密碼確認"
										type="password" v-model="C1newpasswordconfirm" :append-icon="show3 ? 'mdi-eye' : 'mdi-eye-off'" :type="show3 ? 'text' : 'password'" @click:append="show3 = !show3" required></v-text-field>
										
										<v-select 
										:items="chooseauthority" v-model="authority" label="權限" :rules="[v => !!v || '此輸入框不可為空白']" class="mt-4"
											solo required></v-select>
								</v-card-text>
				<!-- 儲存按鈕	 -->
								<v-card-actions class="justify-center">
									<v-btn depressed color="primary" @click="validate()"
										:disabled="!valid" class="justify-center h1 font-weight-black" large>
										建立
									</v-btn>
								</v-card-actions>
								<v-card-actions class="justify-start">
									<!-- <label for="" class="blue--text">授權碼 XXXXXX</label> -->
									<v-spacer></v-spacer>
									<v-btn color="primary" href="./Index.aspx?PageInnerType=UpdateInfo">
										返回
									</v-btn>
								</v-card-actions>

							</v-card>
						</v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->
					</v-main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
	<script>
			new Vue({
				el: '#app',
				vuetify: new Vuetify(),
				data: () => ({
					drawer: false,
					// 資料宣告 街輸入者資料框資料
					valid: true,  //v-form驗證
					License: "",
					C1name: "",
					C1phone: "",
					C1email: "",
					C1lineid: "",
					C1account:"",
					C1newpassword: "",
					C1newpasswordconfirm: "",
					authority:"",
					// 權限選擇框
					chooseauthority: ['權限1', '權限2', '權限3'],
					// 控制密碼眼睛
					show1:false,
					show2:false,
					show3:false,				
				}),
				methods: {
					validate () {
						// 如果驗證成功
						// vuetify驗證寫法 refs
					        if (this.$refs.form.validate()) {
								// 表單提交
					               axios.post('/123', {
					                   classchoice:this.classchoice,
									   people:this.people,
					                 })
					                 .then(response => {
					                   console.log(response);
									   alert("發送成功");
					                 })
					                 .catch(error => {
					                   console.log(error);
									   alert("發送失敗可能是ＰＯＳＴ路徑問題");
					                 });
					             }
					      },
				}
			})
    </script>
</asp:Content>
