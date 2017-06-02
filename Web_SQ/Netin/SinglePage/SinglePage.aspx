<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
   Inherits="Nt.Pages.SinglePage.SinglePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                二级页面管理
                <%if (IsAdministrator)
                  { %>
                &nbsp;<a href="SinglePageEdit.aspx"  title="添加">添加</a>
                <%} %>
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-select">选择</th>
                            <th>编号</th>
                            <th>标题</th>
                            <th class="td-edit-del th-end">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <%#Eval("ID") %>
                            </td>
                            <td>
                                <a href="SinglePageEdit.aspx?Id=<%#Eval("Id") %>"><%#Eval("Title") %></a>
                            </td>
                            <td class="td-end">
                                <a href="SinglePageEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  { %>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <%#Eval("ID") %>
                            </td>
                            <td>
                                <a href="SinglePageEdit.aspx?Id=<%#Eval("Id") %>"><%#Eval("Title") %></a>
                            </td>
                            <td class="td-end">
                                <a href="SinglePageEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  { %>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td class="td-select">
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td colspan="2"></td>
                            <td class="td-edit-del td-end">
                                <%if (IsAdministrator)
                                  { %>
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                                <%} %>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

