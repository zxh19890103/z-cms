<%@ Page Language="C#" Inherits="Nt.Framework.NtPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>大连奈特网络科技有限公司.系统后台管理</title>
    <link href="Content/Css/admin.layout.css" rel="stylesheet" />
    <link href="Content/Css/fileuploader.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Scripts/admin.layout.js" type="text/javascript"></script>
</head>
<body>
    <div>
        <div class="layout-top">
            <iframe src="Shared/Header.aspx" name="topFrame" frameborder="0" scrolling="no" width="100%"></iframe>
        </div>
        <div class="layout-body">
            <div class="layout-menu" id="layout_menu">
                <iframe height="750" name="menuFrame" src="Shared/Menu.aspx" frameborder="0" scrolling="no" width="100%"></iframe>
            </div>
            <div class="layout-middle-bar" id="layout_middle_bar">
                <img id="hide-btn" src="/Netin/Content/Images/hide_btn.jpg" alt="bg" />
            </div>
            <div class="layout-content" id="layout_content">
                <iframe height="750" name="mainFrame" src="System/SystemDescription.aspx" frameborder="0" scrolling="auto" width="100%"></iframe>
            </div>
        </div>
        <div class="layout-foot">
            <iframe src="Shared/Footer.aspx" frameborder="0" height="40" scrolling="no" width="100%"></iframe>
        </div>
    </div>
    <img id="show-btn" src="/Netin/Content/Images/show_btn.jpg" alt="bg" />
</body>
</html>
