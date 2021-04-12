<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="ProjectImmediateReply.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="form-field">
            <label for="oldpassword">原密碼</label>
            <div class="row">
                <div class="col s12">
                    <input type="password" id="passwordold"
                        onkeyup="value=value.replace(/[\W]/g,'') "
                        onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))" />
                </div>
                <div>
                    <a href="javascript:void(0)" id="showbtnold"
                        style="margin-left: -3.25rem!important; margin-top: 2rem!important;">
                        <i class="material-icons">remove_red_eye</i></a>

                </div>
            </div>
        </div>


        <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/js/materialize.min.js"></script>
        <script>
            function ShowPassWord(name) {
                var password = document.getElementById('password' + name);
                var btn = document.getElementById('showbtn' + name);
                if (password.type == 'password') {
                    btn.innerHTML = '<i class="material-icons">visibility_off</i>';
                    password.type = 'text';
                }
                else {
                    btn.innerHTML = '<i class="material-icons">visibility</i>';
                    password.type = 'password';
                }
            };
            $(document).ready(function () {

                $('#showbtnold').click(function () { ShowPassWord('old'); });

            });


        </script>
    </form>
</body>
</html>
