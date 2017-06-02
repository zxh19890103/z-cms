<%@ Page Language="C#" Inherits="Nt.Framework.NtPage" %>

<%@ Import Namespace="Nt.Framework" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">

    const string MENU_DATA_PATH = "/App_Data/Menu/menu-{0}.txt";

    StringBuilder html;

    private void GenerateMenuByPermissions()
    {
        string phy_menu_path = MapPath(string.Format(MENU_DATA_PATH, WorkingUser.UserLevel_Id));
        if (File.Exists(phy_menu_path))
        {
            Response.Write(File.ReadAllText(phy_menu_path));
        }
        else
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Server.MapPath("/Netin/sitemap.config"));
            XmlNode root = xDoc.SelectSingleNode("/siteMap");
            html = new StringBuilder();
            RenderSubMenu(root);
            Response.Write(html.ToString());
            File.WriteAllText(phy_menu_path, html.ToString());
        }
    }

    private void RenderSubMenu(XmlNode root)
    {
        html.Append("<ul>");
        for (int i = 0; i < root.ChildNodes.Count; i++)
        {
            var item = root.ChildNodes.Item(i);
            var permissionNames = item.Attributes["permissionNames"];
            //当当前登录的管理员是超级管理员时，所有均通过，无需检查权限
            if (IsAdministrator || permissionNames == null)
            { }
            else
            {
                if (string.IsNullOrEmpty(permissionNames.Value))
                    continue;
                else
                {
                    if (!permissionNames.Value
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(x => this.PermissionService.Authorize(x.Trim())))
                        continue;
                }
            }
            var url = item.Attributes["url"].Value;
            var title = item.Attributes["title"].Value;
            var _class = item.Attributes["class"].Value;
            html.AppendFormat("<li class=\"{0}\"><a href=\"{1}\">", _class, url);
            html.Append(title);
            html.Append("</a>");
            if (item.HasChildNodes)
            {
                RenderSubMenu(item);
            }
            html.Append("</li>");
        }
        html.Append("</ul>");
    }
</script>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Admin-Menu</title>
    <link href="/Netin/Content/Css/admin.menu.css" rel="stylesheet" />
    <script src="/Netin/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Netin/Scripts/admin.menu.js" type="text/javascript"></script>
    <base target="mainFrame" />
</head>
<body>
    <div class="menu">
        <a href="/Netin/System/SystemDescription.aspx" class="menu-top">系统首页</a>
        <div class="menu-nav">
            <%
                GenerateMenuByPermissions();
            %>
        </div>
        <div class="CopyRight">
            版权所有：奈特商务网
        </div>
    </div>
</body>
</html>
