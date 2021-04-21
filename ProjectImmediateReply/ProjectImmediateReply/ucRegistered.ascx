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
                        <label for="ContentPlaceHolder1_ucRegistered_register_name">姓名</label>
                    </div>
                    <div class="input-field col s6">
                        <input id="register_number" type="tel" class="validate" runat="server">
                        <label for="ContentPlaceHolder1_ucRegistered_register_number">電話號碼</label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s6">
                        <input id="register_email" type="email" class="validate" runat="server">
                        <label for="ContentPlaceHolder1_ucRegistered_register_email">電子郵件</label>
                    </div>
                    <div class="input-field col s6">
                        <input id="register_lineid" type="text" class="validate" runat="server">
                        <label for="ContentPlaceHolder1_ucRegistered_register_lineid">LineID</label>
                    </div>
                </div>
                <!-- 增加班別選單開始 -->
                <div class="row">
                    <div class="input-field col s6">
                        <select class="icons" runat="server" id="register_class">
                            <option disabled selected>班別</option>
                            <%--<option data-icon="https://visualpharm.com/assets/160/Class-595b40b65ba036ed117d2ab4.svg">1</option>
                            <option data-icon="https://visualpharm.com/assets/160/Class-595b40b65ba036ed117d2ab4.svg">2</option>--%>
                        </select>
                        <label>請選擇班別</label>
                    </div>
                    <!-- 增加班別選單結束 -->
                    <div class="input-field col s6">
                        <input id="register_account" type="text" class="validate" runat="server">
                        <label for="ContentPlaceHolder1_ucRegistered_register_account">帳號</label>
                    </div>
                </div>
                <div class="row">
                    <div class="form-field">
                        <div class="input-field col s6">
                            <input type="password" id="passwordregister"
                                class="validate" onkeyup="value=value.replace(/[\W]/g,'') " runat="server"
                                onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))" />
                            <label for="ContentPlaceHolder1_ucRegistered_passwordregister">密碼</label>

                        </div>
                        <div class="input-field col" style="margin-left: -3.25rem!important; margin-top: 2rem!important;">
                            <a href="javascript:void(0)" id="showbtnregister">
                                <i class="material-icons">remove_red_eye</i></a>
                        </div>
                    </div>
                    <div class="form-field">
                        <div class="input-field col s6">
                            <input type="password" id="passwordregistercheck"
                                class="validate" onkeyup="value=value.replace(/[\W]/g,'') " runat="server"
                                onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))" />
                            <label for="ContentPlaceHolder1_ucRegistered_passwordregistercheck">密碼確認</label>
                        </div>
                        <div class="input-field col" style="margin-left: -3.25rem!important; margin-top: 2rem!important;">
                            <a href="javascript:void(0)" id="showbtnregistercheck">
                                <i class="material-icons">remove_red_eye</i></a>
                        </div>
                    </div>
                </div>
               
                <div class="row">
                    <div class="input-field col s12">
                        <input id="register_key" type="text" class="validate" runat="server">
                        <label for="ContentPlaceHolder1_ucRegistered_register_key">授權碼</label>
                    </div>
                </div>
                <span style="color: red">
                    <literal id="messagelabel"></literal>
                </span>

            </div>
        </div>

    </div>
    <div class="modal-footer">
        <button type="button" class="btn orange btn-large" id="regisbtn" >建立</button>
    </div>
</div>
<!-- 註冊彈跳視窗內容結束 -->


<%--<script>
    $(document).ready(function () {
        $("#regisbtn").click(function () {

            $("#messagelabel").empty();
            $("#messagelabel").append(<%=CheckCanUpdate()%>);
        })
    })
</script>--%>
