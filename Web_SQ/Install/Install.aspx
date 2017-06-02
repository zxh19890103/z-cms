<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Install.aspx.cs" Inherits="Install_Install" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Netin7.0系统安装</title>
    <style type="text/css">
        body { background-color: chocolate; font-size: 12px; font-family: "微软雅黑"; }
        .td-left { text-align: right; }
        .td-right { text-align: left; }
        .nt-mask { position: absolute; left: 0; top: 0; width: 100%; height: 100%; z-index: 20000; background-color: gray; filter: alpha(opacity=50); /*IE滤镜，透明度50%*/ -moz-opacity: 0.5; /*Firefox私有，透明度50%*/ opacity: 0.5; /*其他，透明度50%*/ }
        .nt-loading { position: fixed; z-index: 20001; top: 200px; left: 48%; width: 100px; height: 100px; }
        input[type=text], input[type=password] { width: 200px; height: 22px; line-height: 22px; border: 1px solid #bababa; background: #fff; text-align: left; font-size: 12px; color: #545454; }
        input[type=submit] { width: 64px; height: 26px; font-size: 12px; text-align: center; line-height: 26px; display: inline-block; color: #555555; }
            input[type=submit]:hover { cursor: pointer; }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            //UseWindowsAuthentication
            var ck = document.getElementById("UseWindowsAuthentication");
            ck.onclick = function () {
                if (ck.checked) {
                    document.getElementById("trUser").style.display = 'none';
                    document.getElementById("trPwd").style.display = 'none';
                } else {
                    document.getElementById("trUser").style.display = 'inline-block';
                    document.getElementById("trPwd").style.display = 'inline-block';
                }
            }
        }
    </script>
</head>
<body>
    <form id="InstallForm" runat="server">
        <div style="text-align: center; width: 400px; height: 300px; margin: 20px auto;">

            <asp:Label ID="Message" runat="server" Text="" ForeColor="White"></asp:Label>
            <h3 style="text-align:center;">Netin7.0系统安装</h3>
            <table cellpadding="2" cellspacing="1" border="0" width="400">
                <tr>
                    <td class="td-left">服务器:</td>
                    <td class="td-right">
                        <asp:TextBox ID="DataSource" runat="server" Text=""></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="td-left">数据库是否存在:</td>
                    <td class="td-right">
                        <asp:CheckBox ID="DbExisting" runat="server" Checked="false" />
                    </td>
                </tr>

                <tr>
                    <td class="td-left">数据库名称:</td>
                    <td class="td-right">
                        <asp:TextBox ID="DbName" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td class="td-left">是否使用Windows身份验证:</td>
                    <td class="td-right">
                        <asp:CheckBox ID="UseWindowsAuthentication" Checked="false" runat="server" Text="" />
                    </td>
                </tr>

                <tr id="trUser">
                    <td class="td-left">用户名:</td>
                    <td class="td-right">
                        <asp:TextBox ID="UserID" runat="server" Text="sa"></asp:TextBox>
                    </td>
                </tr>

                <tr id="trPwd">
                    <td class="td-left">密码:</td>
                    <td class="td-right">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="td-left"></td>
                    <td class="td-right">
                        <asp:Button ID="Install" runat="server" Text="安装" OnClick="Install_Click" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
