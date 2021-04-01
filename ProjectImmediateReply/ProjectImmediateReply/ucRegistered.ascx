<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRegistered.ascx.cs" Inherits="ProjectImmediateReply.ucLogin" %>
<!-- 註冊帳號彈跳視窗內容 -->
<div id="terms" class="modal">
    <div class="modal-content center-align">
        <h4>帳號註冊</h4>

        <div class="row">
            <div class="col s12">
                <div class="row">
                    <div class="input-field col s6">
                        <input id="register_name" type="text" class="validate" runat="server">
                        <label for="register_name">姓名</label>
                    </div>
                    <div class="input-field col s6">
                        <input id="register_number" type="tel" class="validate" runat="server">
                        <label for="register_number">電話號碼</label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s6">
                        <input id="register_email" type="email" class="validate" runat="server">
                        <label for="register_email">電子郵件</label>
                    </div>
                    <div class="input-field col s6">
                        <input id="register_lineid" type="text" class="validate" runat="server">
                        <label for="register_lineid">LineID</label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s6">
                        <select id="register_class" class="validate" runat="server">
                            <option value="value">text</option>
                            <option value="value">text</option>
                        </select>
                        <label for="register_class">班別</label>
                    </div>
                    <div class="input-field col s6">
                        <input id="register_account" type="text" class="validate" runat="server">
                        <label for="register_account">帳號</label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s6">
                        <input id="register_password" type="password" class="validate" runat="server">
                        <label for="register_password">密碼</label>
                    </div>
                    <div class="input-field col s6">
                        <input id="register_confirm_password" type="password"
                            class="validate" runat="server">
                        <label for="register_confirm_password">密碼確認</label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s12">
                        <input id="register_key" type="text" class="validate" runat="server">
                        <label for="register_key">授權碼</label>
                    </div>
                </div>

            </div>
        </div>

    </div>
    <div class="modal-footer">
        <a href="#!" id="registeredbutton" class="modal-close btn orange btn-large" runat="server">建立</a>
    </div>
</div>
<!-- 註冊彈跳視窗內容結束 -->
k
