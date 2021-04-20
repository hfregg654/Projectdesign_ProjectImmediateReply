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
                                <label for="ContentPlaceHolder1_ucForgetpassword_rescue_key">授權碼</label>  <%--利用F12網頁找授權碼標籤 會是ID--%>
                            </div>
                            <br />
                            <span style="color:red">
                            <literal ID="Message"></literal> <%--使用asp的話必須參照上面label用法抓網頁ID--%>
                            </span>
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <div class="modal-footer">
            <%--<a href="#!" class="modal-close btn orange btn-large" onserverclick="Btn_Forgot" runat="server">搜尋</a>--%>
            <button type="button" class="btn orange btn-large" id="Fgbtn">搜尋</button>
        </div>
    </div>
</div>

<!-- 忘記密碼彈跳視窗結束 -->

<script>
    $(document).ready(function () { //畫面準備好的語法，下方方法是完成後即可開始觸發
        $("#Fgbtn").click(function () {
            $("#Message").empty(); //先清空一次字串 以免造成疊加
            $("#Message").append("<%=Btn_Forgot()%>") //觸發按鈕內的方法
        })
    })
</script>
