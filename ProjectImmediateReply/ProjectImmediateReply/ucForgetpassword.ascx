<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucForgetpassword.ascx.cs" Inherits="ProjectImmediateReply.ucForgetpassword" %>
<!-- 忘記密碼彈跳視窗開始 -->
<div class="container center-align">
    <div id="terms1" class="modal">
        <div class="modal-content">
            <h4>忘記密碼</h4>

            <div class="row">
                <div class="col s12">
                     <div class="input-field inline">
                        <select class="icons" runat="server" id="forgetpwd_class">
                            <option disabled selected>班別</option>
                        </select>
                        <label>請選擇班別</label>
                    </div>
                    <div class="row">
                        <div class="col s12">
                            請輸入授權碼
							<div class="input-field inline">
                                <input id="rescue_key" type="text" class="validate">
                                <label for="rescue_key">授權碼</label>
                                <%--利用F12網頁找授權碼標籤 會是ID--%>
                            </div>
                            <br />
                            <span style="color: red">
                                <literal id="GetPasswordMessage"></literal>
                                <%--使用asp的話必須參照上面label用法抓網頁ID--%>
                            </span>
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <div class="modal-footer">
            <button type="button" class="btn orange btn-large" id="Fgbtn">搜尋</button>
        </div>
    </div>
</div>

<!-- 忘記密碼彈跳視窗結束 -->

