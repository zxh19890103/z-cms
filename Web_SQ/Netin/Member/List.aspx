<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Member.List" %>

<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function resetPassword(id){
            ntAjax({
                method:'ResetPassword',
                data:'{"id":"'+id+'"}',
                success:function(msg){
                    var json=$.parseJSON(msg.d);
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
                用户列表
                &nbsp;<a href="Edit.aspx"  title="添加">添加</a>
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-select">选择</th>
                            <th>会员登录名</th>
                            <th class="td-display">是否禁用</th>
                            <th>会员角色</th>
                            <th>电话\手机</th>
                            <th class="td-dislpay">性别</th>
                            <th class="td-published-date">注册日期</th>
                            <th class="th-end" style="width:220px;">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>"><%#Eval("LoginName") %></a>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("启用", "禁用", Eval("Active"), new { itemid = Eval("Id"), onclick="active(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("MemberRoleName") %>
                            </td>
                            <td>
                                <%#Eval("Phone") %>\<%#Eval("MobilePhone") %>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("男", "女", Eval("Sex"), new {_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                 <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <a href="ChangePassword.aspx?Id=<%#Eval("Id") %>" class="admin-edit-pwd" title="修改密码"></a>
                                 <a href="javascript:;" onclick="resetPassword(<%#Eval("Id") %>);" class="admin-reset-pwd" title="密码重置"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>"><%#Eval("LoginName") %></a>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("启用", "禁用", Eval("Active"), new { itemid = Eval("Id"), onclick="active(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("MemberRoleName") %>
                            </td>
                            <td>
                                <%#Eval("Phone") %>\<%#Eval("MobilePhone") %>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("男", "女", Eval("Sex"), new {_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <a href="ChangePassword.aspx?Id=<%#Eval("Id") %>" class="admin-edit-pwd" title="修改密码"></a>
                                 <a href="javascript:;" onclick="resetPassword(<%#Eval("Id") %>);" class="admin-reset-pwd" title="密码重置"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td>
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td colspan="6">
                                <uc1:UcNtPager runat="server" ID="UcNtPager" />
                            </td>
                            <td class="td-end td-edit-del">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

