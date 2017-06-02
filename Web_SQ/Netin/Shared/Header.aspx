<%@ Page Language="C#" Inherits="Nt.Framework.NtPage" %>

<%@ Register Src="~/Netin/Shared/LanguageSelector.ascx" TagPrefix="uc1" TagName="LanguageSelector" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Admin-Header</title>
    <link href="/Netin/Content/Css/admin.header.css" rel="stylesheet" />
    <link href="/Netin/Content/Css/admin.select.pretty.css" rel="stylesheet" />
    <script src="/Netin/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Netin/Scripts/admin.common.js" type="text/javascript"></script>
</head>
<body>
    <div class="head">
        <div class="logo fl">
            <img src="/netin/content/images/logo.png" alt="here is naite logo" />
        </div>
        <div class="visiter fr">
            <span>您好！<%=this.WorkingUser.UserName %></span>
            <span></span>
            <uc1:LanguageSelector runat="server" ID="LanguageSelector" />
            <span></span>
            <a href="/" target="_blank"><span style="background: url(/netin/content/images/home.jpg) no-repeat;">网站首页</span></a>
            <span></span>
            <a href="javascript:;" onclick="logout();"><span style="background: url(/netin/content/images/out.jpg) no-repeat;">[退出]</span></a>
            <span></span>
            <a href="javascript:;" onclick="refresh();"><span style="background: url(/netin/content/images/reload16.png) no-repeat;">刷新页面</span></a>
            <span></span>
        </div>
    </div>
</body>
</html>
