$(document).ready(function () {
    $("#btn1").click(function () {
        var Account = $("#inpName").val();
        $.ajax({
            url: "API/QueryUser.ashx",
            data: { "Account": Account },
            type: 'GET',
            dataType: 'json',
        })
            .done(function (item) {
                for (var i = 0; i < item.length; i++) {
                    var html = "<p><span>" +
                        item[i].Account + "</span><span>" +
                        item[i].Name + "</span><span>" +
                        item[i].License + "</span></p>";
                    $("#divContainer").empty();
                    $("#divContainer").append(html);
                }
            })
            .fail(function (xhr, status, errorThrown) {
                console.log("傳輸失敗");
            })
            .always(function (xhr, status) {

            });
    })
    $("#CreatClassbtn").click(function () {
        var ClassNumber = $("#ClassNumberTbox").val();
        var PeopleNum = $("#PeopleNumTbox").val();
        var Privilege = $("#ContentPlaceHolder1_ucCreateClass_HiddenFieldSessionPri").val();


        $.ajax({
            url: "API/CreateClassHandler.ashx",
            data: {
                "ClassNumber": ClassNumber,
                "PeopleNum": PeopleNum,
                "Privilege": Privilege
            },
            type: 'POST',
            dataType: 'json',
        })
            .done(function (item) {
                alert(0);
            })
            .always(function (xhr, status) {
                
            });
    })



})