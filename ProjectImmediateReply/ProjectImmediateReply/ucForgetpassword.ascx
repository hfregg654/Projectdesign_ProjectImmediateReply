<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucForgetpassword.ascx.cs" Inherits="ProjectImmediateReply.ucForgetpassword" %>
<!-- 忘記密碼彈跳視窗開始 -->
<div class="container center-align">
    <div id="terms1" class="modal">
        <div class="modal-content">
            <h4>忘記密碼</h4>

            <div class="row">
                <div class="col s12">
                    <div class="row">
                        <div class="col s12">
                            請輸入授權碼
							<div class="input-field inline">
                                <input id="rescue_key" type="text" class="validate" runat="server">
                                <label for="rescue_key">授權碼</label>
                            </div>
                            <br />
                            <span style="color:red">
                            <asp:Literal ID="Message" runat="server"></asp:Literal>
                            </span>
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close btn orange btn-large" onserverclick="Btn_Forgot" runat="server">搜尋</a>
        </div>
    </div>
</div>

<!-- 忘記密碼彈跳視窗結束 -->
