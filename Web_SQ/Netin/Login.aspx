<%@ Page Language="C#" AutoEventWireup="true" Inherits="Nt.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/Css/login.css" rel="stylesheet" />
    <title>奈特网后台登录</title>
    <script type="text/javascript">

        function refreshCode() {
            var rand = Math.random();
            var checkCode = document.getElementById('checkCode');
            checkCode.src = 'Handlers/CheckCode/CheckCodeHandler.ashx?' + rand;
        }

        /*
        sender--InputHtmlElement
        type--1:focus;0:blur
        */
        function smartInput(sender, type) {
            if (type) {
                if (sender.value == '用户名')
                    sender.value = '';
            } else {
                if (sender.value == '')
                    sender.value = '用户名'
            }
        }

        /*登录验证*/
        function loginSubmit() {
            if (LoginForm.UserName.value == '') {
                alert('用户名不能为空!');
                return false;
            }
            if (LoginForm.Password.value == '') {
                alert('密码不能为空!');
                return false;
            }
            if (LoginForm.CheckCode.value == '') {
                alert('验证码不能为空!');
                return false;
            }
            LoginForm.submit();
        }

        function OnEnterPress(evt) {
            var iKeyCode = window.event ? event.keyCode : evt.which;
            if (iKeyCode == 13) {
                loginSubmit();
            }
        }

        window.onload = function () {
            LoginForm.UserName.focus();
        }

    </script>
</head>
<body>
    <div class="content">
        <form id="LoginForm" runat="server">
            <asp:Label ID="ErrorMessage" runat="server" Text="" Font-Size="Medium" ForeColor="Red"></asp:Label>
            <input type="text" maxlength="20" name="UserName" value="用户名" class="input-username"
                onfocus="smartInput(this,1)" onblur="smartInput(this,0);" />
            <input type="password" maxlength="20" onkeypress="OnEnterPress(event)" name="Password" class="input-password" value="" />
            <input type="text" name="CheckCode" onkeypress="OnEnterPress(event)" class="input-checkcode" /><span class="span-checkcode">
                <a href="javascript:;" onclick="refreshCode();">
                    <img src="Handlers/CheckCode/CheckCodeHandler.ashx" alt="" id="checkCode" /></a></span>
            <a href="javascript:;" onkeypress="OnEnterPress(event)" id="btn2submit" class="submit" onclick="loginSubmit();"></a>
        </form>
        <div class="corpyRight">
            CORPYRIGHT© 2013-<%=DateTime.Now.Year %>
            <a href="http://naite.com.cn">NAITE.COM.CN</a> ALL RIGHT RESERVED
        </div>
    </div>
</body>
</html>
