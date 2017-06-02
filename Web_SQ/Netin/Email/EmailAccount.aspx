<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
 Inherits="Nt.Pages.Email.EmailAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function setThisDefualt(id) {
            ntAjax({
                method: 'SetDefualtEmailAccount',
                data: '{"id":"' + id + '"}',
                success: function (msg) {
                    var json = $.parseJSON(msg.d);
                    if (json.error) { ntAlert(json.message); }
                    else {
                        window.location.reload();
                    }
                }
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                邮箱账号管理
                  &nbsp;<a href="EmailAccountEdit.aspx"  title="添加">添加</a>
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th>邮箱地址</th>
                    <th>邮箱显示名称</th>
                    <th>是否默认邮箱账号</th>
                    <th>设置为默认邮箱账号</th>
                    <th class="th-end">编辑</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("Email") %></td>
                            <td><%#Eval("DisplayName") %></td>
                            <td><%#Convert.ToBoolean(Eval("IsDefault"))?"是":"否" %></td>
                            <td><a href="javascript:;" onclick="setThisDefualt(<%#Eval("Id") %>)">设置为默认账户</a></td>
                            <td class="td-end">
                                <a href="EmailAccountEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("Email") %></td>
                            <td><%#Eval("DisplayName") %></td>
                            <td><%#Convert.ToBoolean(Eval("IsDefault"))?"是":"否" %></td>
                            <td><a href="javascript:;" onclick="setThisDefualt(<%#Eval("Id") %>)">设置为默认账户</a></td>
                            <td class="td-end">
                                <a href="EmailAccountEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
           
        </div>
    </div>
</asp:Content>

