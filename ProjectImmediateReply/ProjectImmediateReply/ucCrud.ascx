<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCrud.ascx.cs" Inherits="ProjectImmediateReply.ucCRUD" %>
<v-main>
    <v-row>
	    <v-col>
	    </v-col>
	    <v-col sm="6" xs="12">
<%--			對應PageTool裡的data chooseitem--%>
	    	<v-select :items="chooseitem"  @change="changeRoute"  v-model="classchoice"  label="選擇班級" solo outlined></v-select>
	    </v-col>
	    <v-col>
	    </v-col>
    </v-row>
   <template>
	   <%--v-data-table 類似Repeater或GridView--%>
							<v-data-table @page-count="pageCount = $event" :page.sync="page"
								:items-per-page="itemsPerPage" hide-default-footer :headers="headers" :items="inneritem" 
								sort-by="calories" class="elevation-1">
								<!-- v-slot 開始							 -->
								<template v-slot:top>
									<v-toolbar flat color="white">
										<v-spacer></v-spacer>
										專案
										<v-spacer></v-spacer>
									</v-toolbar>
								</template>
								<!-- ----->
								<!-- ROW是TABLE裡面一橫排內容的意思 封裝好的 -->
								<template v-slot:item="row">
								    <tr>
								      <td>{{row.item.ProjectName}}</td>
								      <td>{{row.item.LeaderName}}</td>
									  <td>{{row.item.MemberName}}</td>
									  <td>{{row.item.TeamName}}</td>
								      <td>
										  <!-- click後面給他唯一值去抓 --> <!-- 每個都是唯一的 -->
								          <v-btn class="mx-2" text dark small color="green" @click="onButtonClick(row.item)">
											  查看
								          </v-btn>
								      </td>
								    </tr>
								</template>
							
								
								
							</v-data-table>

							<v-pagination v-model="page" :length="pageCount"></v-pagination>
								
						</template>
</v-main>
