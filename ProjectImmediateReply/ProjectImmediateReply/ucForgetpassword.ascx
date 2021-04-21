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

<script>
    $("#Fgbtn").click(function () {
        $("#Fgbtn").hide(100);//將按鈕隱藏
        var License = $("#rescue_key").val();

        //發送ajax請求,呼叫班級建立的API並將參數送進去
        $.ajax({
            url: "API/ForgetPasswordHandeler.ashx",
            data: {
                "License": License
            },
            type: 'GET',
            dataType: 'json',
        })
            //當請求完成提醒使用者完成並顯示建立按鈕
            .done(function (Data) {
                $("#GetPasswordMessage").empty;
                if (Data.length != 0) {
                    $("#GetPasswordMessage").append("您的密碼是:"+Data[0].PassWord.toString());
                }
                else {
                    $("#GetPasswordMessage").append("授權碼輸入錯誤");
                }

            })
            //當請求失敗提醒使用者失敗並顯示建立按鈕
            .fail(function (xhr, status, errorThrown) {
                alert("錯誤發生失敗,請確認輸入資訊或是資料庫狀況");
            })
            .always(function (xhr, status) {
                $("#Fgbtn").show(100);
            });

    })
</script>
