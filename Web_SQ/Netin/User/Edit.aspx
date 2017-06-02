<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
   Inherits="Nt.Pages.User.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>管理员
            </div>
            <form id="EditForm" name="EditForm"
                action="<%=LocalUrl %>"
                method="post">
                <table class="admin-table">
                    <tr>
                        <td class="left">管理员角色:</td>
                        <td class="right">
                            <%=HtmlHelper.DropdownList(AvailableUserLevel,new {_class="category-drop-down",name="UserLevel_Id" }) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">名称:</td>
                        <td class="right">
                            <input type="hidden" name="oldUserName" value="<%=Model.UserName %>" />
                            <input type="text" class="input-text" name="UserName" value="<%=Model.UserName %>" />
                        </td>
                    </tr>
                    <%if (!EnsureEdit)
                      {%>
                    <tr>
                        <td class="left">密码:</td>
                        <td class="right">
                            <input type="password" class="input-text" name="Password" value="<%=Model.Password %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">重输密码:</td>
                        <td class="right">
                            <input type="password" class="input-text" name="Password.Again" value="<%=Model.Password %>" />
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td class="left">是否启用:</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.Active,"Active",new{}) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">描述:</td>
                        <td class="right">
                            <textarea rows="5" cols="10" name="Description"><%=Model.Description %></textarea>
                        </td>
                    </tr>
                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="AddDate" value="<%=Model.AddDate %>" />
                            <input type="hidden" name="AddUser" value="<%=UserID %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

