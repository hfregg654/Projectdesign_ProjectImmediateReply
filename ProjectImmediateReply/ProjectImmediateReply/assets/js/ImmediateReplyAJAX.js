$(document).ready(function () {
        //$.ajax({
        //    url: "API/GetClassNumberHandler.ashx",
        //    data: { "ClassNumber": ClassNumber },
        //    type: 'GET',
        //    dataType: 'json',
        //})
        //    .done(function (item) {
        //        for (var i = 0; i < item.length; i++) {
        //            var html = "<p><span>" +
        //                item[i].Account + "</span><span>" +
        //                item[i].Name + "</span><span>" +
        //                item[i].License + "</span></p>";
        //            $("#divContainer").append(html);
        //        }
        //    })
        //    .fail(function (xhr, status, errorThrown) {
        //        console.log("傳輸失敗");
        //    })
        //    .always(function (xhr, status) {

        //    });


    //當按下班級建立按鈕時觸發事件
    $("#CreatClassbtn").click(function () {
        $("#CreatClassbtn").hide(100);//將建立按鈕隱藏
        //取得輸入框上的班級名稱,班級人數,當前登入者的權限以及信箱
        var ClassNumber = $("#ClassNumberTbox").val();
        var PeopleNum = $("#PeopleNumTbox").val();
        var Privilege = $("#HiddenFieldSessionPri").val();
        var Mail = $("#HiddenFieldSessionMail").val();
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
                $("#CreateClassMessage").empty();
                if (responseData[0].success == "true") {
                    $("#CreateClassMessage").append("已建立" + PeopleNum + "組授權碼並發送至您的信箱");
                } else if (responseData[0].success == "NumWrong") {
                    $("#CreateClassMessage").append("建立途中發生錯誤,請將此班級刪除以重新建立");
                } else {
                    $("#CreateClassMessage").append("建立失敗,請確認輸入資訊或是資料庫狀況並重新建立");
                }

            })
            //當請求失敗提醒使用者失敗並顯示建立按鈕
            .fail(function (xhr, status, errorThrown) {
                alert("建立失敗,請確認輸入資訊或是資料庫狀況並重新建立");
            })
            .always(function () {
                $("#CreatClassbtn").show(100);
            });
    })


    //當按下建立按鈕時觸發事件
    $("#regisbtn").click(function () {
        $("#regisbtn").hide(100);//將建立按鈕隱藏
        //取得輸入框上的名字,電話,Mail,LineID,班級名稱,帳號,密碼,密碼再確認,授權碼
        var Name = $("#register_name").val();
        var Phone = $("#register_number").val();
        var Mail = $("#register_email").val();
        var LineID = $("#register_lineid").val();
        var ClassNumber = $("#ContentPlaceHolder1_ucRegistered_register_class").val();
        var Account = $("#register_account").val();
        var PassWord = $("#register_password").val();
        var PassWordCheck = $("#register_passwordcheck").val();
        var License = $("#register_key").val();
        //發送ajax請求,呼叫註冊的API並將參數送進去 右邊變數值 左邊是傳過去的變數名稱
        $.ajax({
            url: "API/RegisteredHandler.ashx",
            data: {
                "Name": Name,
                "Phone": Phone,
                "Mail": Mail,
                "LineID": LineID,
                "ClassNumber": ClassNumber,
                "Account": Account,
                "PassWord": PassWord,
                "PassWordCheck": PassWordCheck,
                "License": License
            },
            type: 'POST',
            dataType: 'json',
        })
            //當請求完成提醒使用者完成並顯示建立按鈕
            .done(function (responseData) {
                $("#messagelabel").empty();
                if (responseData[0].success == "Empty") {  //傳回第一筆(第一列)資料
                    $("#messagelabel").append("欄位不可為空");
                }
                else if (responseData[0].success == "PassWordWrong") {
                    $("#messagelabel").append("密碼確認輸入錯誤");
                }
                else if (responseData[0].success == "licensewrong") {
                    $("#messagelabel").append("查無此授權碼")
                }
                else if (responseData[0].success == "license") {
                    $("#messagelabel").append("授權碼已使用");
                }
                else if (responseData[0].success == "account") {
                    $("#messagelabel").append("帳號已存在，請使用其他帳號")
                }
                else {
                    $("#messagelabel").append("註冊成功，請至註冊信箱收取驗證信")
                }
            })
            //當請求失敗提醒使用者失敗並顯示建立按鈕
            .fail(function (xhr, status, errorThrown) { //  Status是錯誤類型 errorThrown是錯誤訊息
                alert("註冊失敗，請確認輸入資訊或是資料庫狀況");
            })
            .always(function () { //無論成功與否一定會做
                $("#regisbtn").show(100);
            });
    })


    //當按下建立按鈕時觸發事件
    $("#CreateProjectbtn").click(function () {
        $("#CreateProjectbtn").hide(100);//將建立按鈕隱藏
        //取得輸入框上的班別、專案名、時程期限
        //vm變數裡的classchoice屬性 =>在PageTool
        var ClassNumber = vm.classchoice;
        var ProjectName = $("#ProjectNameTbox").val();
        var DeadLine = $("#DeadLine").val();
        var Privilege = $("#HiddenFieldSessionPri").val();
        //發送ajax請求,呼叫班級建立的API並將參數送進去
        $.ajax({
            url: "API/CreateProjectHandler.ashx",
            data: {
                "ClassNumber": ClassNumber,
                "ProjectName": ProjectName,
                "DeadLine": DeadLine,
                "Privilege": Privilege
            },
            type:"POST",
            dataType:"json",
        })

         //當請求完成提醒使用者完成並顯示建立專案按鈕
            .done(function (responsedata)
            {
                $("#CreateProjectMessage").empty();
                if (responsedata[0].success == "true") {
                    $("#CreateProjectMessage").append("專案建立成功");
                }
                else if (responsedata[0].success == "DateWrong")
                {
                    $("#CreateProjectMessage").append("日期錯誤，請重新選擇日期");
                }
                else if (responsedata[0].success == "ProjectCountWrong") {
                    $("#CreateProjectMessage").append("此班的專案已存在4組");
                }
                else { 
                    $("#CreateProjectMessage").append("專案建立失敗，請重新建立");
                }
            })
            //當請求失敗提醒使用者失敗並顯示建立按鈕
            .fail(function (xhr, status, errThrown) {
                alert("專案建立失敗，請重新建立");
            })
            .always(function () {
                $("#CreateProjectbtn").show(100);
            })
    })


    //當按下查詢按鈕時觸發事件
    $("#Fgbtn").click(function () {
        $("#Fgbtn").hide(100);//將按鈕隱藏
        var License = $("#rescue_key").val();
        var ClassNum = $("#ContentPlaceHolder1_ucForgetpassword_forgetpwd_class").val();
        //發送ajax請求,呼叫班級建立的API並將參數送進去
        $.ajax({
            url: "API/ForgetPasswordHandeler.ashx",
            data: {
                "License": License,
                "ClassNum": ClassNum
            },
            type: 'GET',
            dataType: 'json',
        })
            //當請求完成提醒使用者完成並顯示建立按鈕
            .done(function (Data) {

                if (Data.length != 0) {
                    $("#GetPasswordMessage").empty();
                    $("#GetPasswordMessage").append("您的密碼是:" + Data[0].PassWord.toString());
                }
                else {
                    $("#GetPasswordMessage").empty();
                    $("#GetPasswordMessage").append("查無資料");
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
})