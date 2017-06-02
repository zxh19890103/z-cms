<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
   Inherits="Nt.Pages.Common.Log" %>

<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                系统日志
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-select">选择</th>
                            <th class="td-order">用户ID</th>
                            <th class="td-display">登录IP</th>
                            <th>描述</th>
                            <th class="td-published-date">日期</th>
                            <th class="th-end td-edit-del">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <a href="javascript:;" onclick="detail('user',<%#Eval("UserID") %>);"><%#Eval("UserID") %></a>
                            </td>
                            <td>
                                <%#Eval("LoginIp") %>
                            </td>
                            <td>
                                <a href="javascript:;" title="<%#Eval("Description") %>"><%#NtUtility.GetSubString(Eval("Description").ToString(),30) %></a>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
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
                                <a href="javascript:;" onclick="detail('user',<%#Eval("UserID") %>);"><%#Eval("UserID") %></a>
                            </td>
                            <td>
                                <%#Eval("LoginIp") %>
                            </td>
                            <td>
                                <a href="javascript:;" title="<%#Eval("Description") %>"><%#NtUtility.GetSubString(Eval("Description").ToString(),30) %></a>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>

                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td class="td-select">
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td colspan="4">
                                <uc1:UcNtPager runat="server" ID="UcNtPager" />
                            </td>
                            <td align="center" class="td-edit-del td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                            </td>
                        </tr>
                    </FooterTemplate>

                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

