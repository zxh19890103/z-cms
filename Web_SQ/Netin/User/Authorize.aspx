<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.User.Authorize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                角色授权
            </div>
            <center>
                <form
                    action="<%=LocalUrl %>"
                    method="post"
                    name="AuthorizeForm"
                    id="AuthorizeForm">
                    <div>
                        <%=HtmlHelper.DropdownList(UserLevels, 
            new {name="UserLevel_Id",_class="category-drop-down",onchange="window.location.href='"+LocalPath+"?UserLevel_Id='+this.value;"})%>
                    </div>
                    <div id="PermissionRecordPanel">
                        <%RenderPermissionRecords(!IsAdministrator); %>
                    </div>
                    <div>
                        <input type="submit" class="admin-button" value="保存" />
                        <input type="reset" class="admin-button" value="重置" />
                        <input type="button" class="admin-button" value="返回" onclick="window.location.href = 'UserLevel.aspx'; return false;" />
                    </div>
                </form>
            </center>
        </div>
    </div>
</asp:Content>

