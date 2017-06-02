<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Member.MemberRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                会员角色管理
                &nbsp;<a href="MemberRoleEdit.aspx"  title="添加">添加</a>
            </div>

            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th>名称</th>
                            <th>系统名</th>
                            <th>是否启用</th>
                            <th class="th-end td-edit-del">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("SystemName") %></td>
                            <td><%#HtmlHelper.BoolLabel("启用", "未启用", Eval("Active"), new {  itemid = Eval("Id"), onclick="active(this)",_class="lbl-ajax"})%></td>
                            <td class="td-end">
                                <a href="MemberRoleEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("SystemName") %></td>
                            <td><%#HtmlHelper.BoolLabel("启用", "未启用", Eval("Active"), new {  itemid = Eval("Id"), onclick="active(this)",_class="lbl-ajax"})%></td>
                            <td class="td-end">
                                <a href="MemberRoleEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

