
$(document).ready(function () {
    // Modal trigger
    $('.modal').modal();
    //增加班別選單
    $('select').formSelect();
    // 登入頁密碼框顯示
    $('#showbtnIndex').click(function () { ShowPassWord('ContentPlaceHolder1_passwordIndex','showbtnIndex'); });
    //左側選單
    $('.sidenav').sidenav();
    // 舊密碼框顯示
    $('#showbtnold').click(function () { ShowPassWord('passwordold', 'showbtnold'); });
    // 新密碼框顯示
    $('#showbtnnew').click(function () { ShowPassWord('passwordnew', 'showbtnnew'); });
    // 確認密碼框顯示
    $('#showbtncheck').click(function () { ShowPassWord('passwordcheck', 'showbtncheck'); });
    // 註冊密碼框顯示
    $('#showbtnregister').click(function () { ShowPassWord('ContentPlaceHolder1_ucRegistered_passwordregister', 'showbtnregister'); });
    // 註冊確認密碼框顯示
    $('#showbtnregistercheck').click(function () { ShowPassWord('ContentPlaceHolder1_ucRegistered_passwordregistercheck','showbtnregistercheck'); });
    $("#Fgbtn").click(function () {
        $("#Message").empty(); //先清空一次字串 以免造成疊加
        $("#Message").("Btn_Forgot()") //觸發按鈕內的方法
    })
});
// ----
// 增加密碼框顯示功能
function ShowPassWord(txtboxname,btnname) {
    var password = document.getElementById(txtboxname);
    var btn = document.getElementById(btnname);
    if (password.type == 'password') {
        btn.innerHTML = '<i class="material-icons">visibility_off</i>';
        password.type = 'text';
    }
    else {
        btn.innerHTML = '<i class="material-icons">visibility</i>';
        password.type = 'password';
    }
};




