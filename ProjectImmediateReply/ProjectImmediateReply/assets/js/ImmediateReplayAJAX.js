$(document).ready(function () {
    //$("#btn1").click(function () {
    //    var Account = $("#inpName").val();
    //    $.ajax({
    //        url: "API/QueryUser.ashx",
    //        data: { "Account": Account },
    //        type: 'GET',
    //        dataType: 'json',
    //    })
    //        .done(function (item) {
    //            $("#divContainer").empty();
    //            for (var i = 0; i < item.length; i++) {
    //                var html = "<p><span>" +
    //                    item[i].Account + "</span><span>" +
    //                    item[i].Name + "</span><span>" +
    //                    item[i].License + "</span></p>";
    //                $("#divContainer").append(html);
    //            }
    //        })
    //        .fail(function (xhr, status, errorThrown) {
    //            console.log("傳輸失敗");
    //        })
    //        .always(function (xhr, status) {

    //        });
    //})
    //當按下班級建立按鈕時觸發事件
    $("#CreatClassbtn").click(function () {
        $("#CreatClassbtn").hide(100);//將建立按鈕隱藏
        //取得輸入框上的班級名稱,班級人數,當前登入者的權限以及信箱
        var ClassNumber = $("#ClassNumberTbox").val();
        var PeopleNum = $("#PeopleNumTbox").val();
        var Privilege = $("#ContentPlaceHolder1_ucCreateClass_HiddenFieldSessionPri").val();
        var Mail = $("#ContentPlaceHolder1_ucCreateClass_HiddenFieldSessionMail").val();
        //發送ajax請求,呼叫班級建立的API並將參數送進去
        $.ajax({
            url: "API/CreateClassHandler.ashx",
            data: {
                "ClassNumber": ClassNumber,
                "PeopleNum": PeopleNum,
                "Privilege": Privilege,
                "Mail": Mail
            },
            type: 'POST',
            dataType: 'json',
        })
            //當請求完成提醒使用者完成並顯示建立按鈕
            .done(function (responseData) {
                alert("已建立" + PeopleNum + "組授權碼並發送至您的信箱");
                $("#CreatClassbtn").show(100);
            })
            //當請求失敗提醒使用者失敗並顯示建立按鈕
            .fail(function (xhr, status, errorThrown) {
                alert("建立失敗,請確認輸入資訊或是資料庫狀況");
                $("#CreatClassbtn").show(100);
            });
    })



})