<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Email.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function viewEmailDetail(id) {
            openWindow("EmailDetail.aspx?Id=" + id, "");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">邮件发送历史</div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-select">选择</th>
                    <th>发送自</th>
                    <th>发送至</th>
                    <th>主题</th>
                    <th>添加日期</th>
                    <th>发送日期</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <%#Eval("From") %>
                            </td>
                            <td>
                                <%#Eval("To") %>
                            </td>
                            <td>
                                <%#Eval("Subject") %>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td>
                                <%#Eval("SentDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="viewEmailDetail(<%#Eval("Id") %>);" class="admin-view-detail" title="查看邮件"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>

                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <%#Eval("From") %>
                            </td>
                            <td>
                                <%#Eval("To") %>
                            </td>
                            <td>
                                <%#Eval("Subject") %>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td>
                                <%#Eval("SentDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="viewEmailDetail(<%#Eval("Id") %>);" class="admin-view-detail" title="查看邮件"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td>
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td colspan="5"></td>
                            <td class="td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

