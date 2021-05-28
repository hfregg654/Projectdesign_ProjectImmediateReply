<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSeeGrade.ascx.cs" Inherits="ProjectImmediateReply.ucSeeGrade" %>
<v-main>
						<!-- 主要內容開始 -->
						<!-- -----card start -->
						<v-form method="post" novalidate="true" ref="form">
       <v-card class="mx-auto mb-15 px-4 pb-0 pt-4" max-width="1100">
        <v-card-title class="justify-center">
              成績
            </v-card-title>
        <v-card-text>
  <div id="Showuserandleader">
           	<v-select readonly height="30" :items="chooseclass" v-model="chooseclass" label="班別" :rules="classrules"
									solo required></v-select>
							<!-- 隱藏第一區，需等班別有值再顯示 -->
			<v-select readonly height="30" :items="choosegroup" v-model="choosegroup" label="小組" :rules="classrules" solo
									required></v-select>
							<!-- 隱藏第一區結束 -->
							<!-- 隱藏第二區開始 -->
			<v-select readonly height="30" :items="choosename" v-model="choosename" label="姓名" :rules="classrules" solo
									required></v-select>
  </div>





  <div id="Showgradesandmanager">

      <%--@change="changeRoute"引用--%>
         <v-select height="30" :items="chooseclass"  @change="changeRoutechooseclass" v-model="classchoice" label="班別" :rules="classrules"
          solo required></v-select>
        <!-- 隱藏第一區，需等班別有值再顯示 -->
        <div id="showarea1" v-if="classchoice.length">
         <v-select height="30" :items="choosegroup" @change="changeRoutechoosegroup" v-model="group" label="小組" :rules="classrules" solo
          required></v-select>
        </div>
        <!-- 隱藏第一區結束 -->
        <!-- 隱藏第二區開始 -->
        <div id="showarea2" v-if="group.length">
         <v-select height="30" :items="choosename" item-text="UserName" item-value="Account" @change="changeRoutechoosename" v-model="name" label="姓名" :rules="classrules" solo
          required></v-select>
        </div>

  </div>
        <!-- 隱藏第二區結束 -->
        <!-- 隱藏第三區開始 -->
        <div id="showarea3" v-if="classchoice.length && group.length && name.length">
        <v-row>
          <v-spacer></v-spacer>
          <span class="h3">電子郵件</span>
          <v-spacer></v-spacer>
        </v-row>
        <v-row>
         <v-spacer></v-spacer>                  <%--{{email}} 等同於 <%Eval.("變數名稱")%>用法--%>
          <p class="subtitle-2" v-model="email">{{email}}</p>
         <v-spacer></v-spacer>
        </v-row>
        <v-row>
         <v-spacer></v-spacer>
          <span>成績</span>
          <span class="subtitle-2 mx-2" style="font-size: 2rem!important;" v-model="score">{{score}}</span>
         <v-spacer></v-spacer>
        </v-row>
         <v-flex xs12 md12>
          <span　class="pa=0">評語</span>
          <v-expansion-panels v-model="panel" multiple>
           <v-expansion-panel class="mb-2">
            <v-expansion-panel-header>社長評語</v-expansion-panel-header>
            <v-expansion-panel-content>
             <p class="font-weight-medium text-justify">
                       {{boss}}
             </p> 
            </v-expansion-panel-content>
           </v-expansion-panel>

           <v-expansion-panel>
            <v-expansion-panel-header>PM評語</v-expansion-panel-header>
            <v-expansion-panel-content>
             <p class="font-weight-medium text-justify">
                    {{pm}}
             </p>
            </v-expansion-panel-content>
           </v-expansion-panel>

          </v-expansion-panels>
         </v-flex>
        </div>
        <!-- 隱藏第三區結束 -->

        </v-card-text>
   <%--<div id="chagegrade">
        <v-card-actions class="justify-end mb-10">
         <v-btn color="blue lighten-2" text　href="/abcde" target="_blank">
          分數修改
         </v-btn>
        </v-card-actions>
  </div>--%>
       </v-card>
      </v-form>

						<!-- -----card over-->
						<!-- 主要內容結束 -->
					</v-main>
