﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImmediateReplay.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProjectImmediateReply.Index" %>

<%@ Register Src="~/ucRegistered.ascx" TagPrefix="uc1" TagName="ucRegistered" %>
<%@ Register Src="~/ucForgetpassword.ascx" TagPrefix="uc1" TagName="ucForgetpassword" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- -wrapper包容器 不可刪---- -->
    <div class="wrapper">
        <!-- ------Login Form---------
				 -->
        <div class="row login">
            <div class="col s12 l4 offset-l4">
                <div class="card">

                    <div class="card-action red white-text">
                        <div class="center-align">
                            <p class="flow-text">專案即時回覆系統</p>
                        </div>
                    </div>
                    <div class="card-content">
                        <div class="form-field">
                            <label for="username">帳號</label>
                            <input type="text" id="username" runat="server" style="color: white" onkeyup="value=value.replace(/[\W]/g,'') " />
                        </div>
                        <br>
                        <div class="form-field">
                            <label for="password">密碼</label>
                            <div class="row">
                                <div class="col s12">
                                    <input type="password" id="passwordIndex" name="password" style="color: white" onkeyup="value=value.replace(/[\W]/g,'') "
                                        onbeforepaste=
                                        "clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))" />
                                </div>
                                <div>
                                    <a href="javascript:void(0)" id="showbtnIndex" 
                                        style="margin-left: -3.25rem!important; margin-top: 2rem!important;">
                                        <i class="material-icons">remove_red_eye</i></a>
                                </div>
                            </div>
                        </div>
                        <br>
                        <div class="form-field center-align">
                            <button class="btn-large red">登入<i class="material-icons right">send</i></button>
                        </div>
                        <div class="card-content center-align">
                            <a class="modal-trigger" href="#terms">註冊帳號</a><br>
                            <br>
                            <a class="modal-trigger" href="#terms1">忘記密碼</a>
                        </div>
                        <!--modal 外層用container包著 ------
							 -->
                        <div class="container">
                            <uc1:ucRegistered runat="server" ID="ucRegistered" />
                            <uc1:ucForgetpassword runat="server" ID="ucForgetpassword" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
