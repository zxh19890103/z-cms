<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
Inherits="Nt.Pages.User.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
      <script type="text/javascript">
          function resetPassword(id) {
              ntAjax({
                  method: 'ResetPassword',
                  data: '{"id":"' + id + '"}',
                  success: function (msg) {
                      var json = $.parseJSON(msg.d);
                      ntAlert(json.message);
                  }
              })
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                管理员管理
                &nbsp;<a href="Edit.aspx" class="admin-add-user"  title="添加管理员"></a>
            </div>

            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th>登录名</th>
                            <th>管理员角色</th>
                            <th>是否启用</th>
                            <th>添加日期</th>
                            <th class="th-end td-edit-del">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("UserName") %></td>
                            <td><%#Eval("UserLevelName") %></td>
                            <td><%#HtmlHelper.BoolLabel("启用", "未启用", Eval("Active"), new {  itemid = Eval("Id"), onclick="active(this)",_class="lbl-ajax"})%></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}")%></td>
                            <td class="td-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit-user" title="修改用户"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete-user" title="删除用户"></a>
                                <a href="ChangePassword.aspx?Id=<%#Eval("Id") %>" class="admin-edit-pwd" title="修改密码"></a>
                                 <a href="javascript:;" onclick="resetPassword(<%#Eval("Id") %>);" title="重置密码" class="admin-reset-pwd"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("UserName") %></td>
                            <td><%#Eval("UserLevelName") %></td>
                            <td><%#HtmlHelper.BoolLabel("启用", "未启用", Eval("Active"), new {  itemid = Eval("Id"), onclick="active(this)",_class="lbl-ajax"})%></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}")%></td>
                            <td class="td-end">
                               <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit-user" title="修改用户"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete-user" title="删除用户"></a>
                                <a href="ChangePassword.aspx?Id=<%#Eval("Id") %>" class="admin-edit-pwd" title="修改密码"></a>
                                 <a href="javascript:;" onclick="resetPassword(<%#Eval("Id") %>);" title="重置密码" class="admin-reset-pwd"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

