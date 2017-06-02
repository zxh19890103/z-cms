<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" Inherits="Nt.Pages.Seo.ColumnsSeo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%
        XRepeater.DataSource = DataSource;
        XRepeater.DataBind();
    %>
    <div class="admin-main-wrap">
        <div class="admin-tips">
            栏目优化有助于您网站的推广
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                栏目优化
            </div>
            <form action="<%=LocalUrl %>" method="post" name="EditForm" id="EditForm">
                <table class="admin-table">
                    <tr class="admin-table-header">
                        <th>导航名称</th>
                        <th>标题</th>
                        <th>关键词</th>
                        <th class="th-end">描述</th>
                    </tr>
                    <asp:Repeater ID="XRepeater" runat="server">
                        <ItemTemplate>
                            <tr class="tr-even">
                                <td>
                                    <%#Eval("Name") %>
                                </td>
                                <td>
                                    <input type="hidden" name="NavigationID" value="<%#Eval("ID") %>" />
                                    <input type="text" class="input-text" name="MetaTitle<%#Eval("ID") %>" value="<%#Eval("MetaTitle") %>" /></td>
                                <td>
                                    <input type="text" class="input-text" name="MetaKeywords<%#Eval("ID") %>" value="<%#Eval("MetaKeywords") %>" /></td>
                                <td class="td-end">
                                    <input type="text" class="input-text" name="MetaDescription<%#Eval("ID") %>" value="<%#Eval("MetaDescription") %>" /></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="tr-odd">
                                <td>
                                    <%#Eval("Name") %>
                                </td>
                                <td>
                                    <input type="hidden" name="NavigationID" value="<%#Eval("ID") %>" />
                                    <input type="text" class="input-text" name="MetaTitle<%#Eval("ID") %>" value="<%#Eval("MetaTitle") %>" /></td>
                                <td>
                                    <input type="text" class="input-text" name="MetaKeywords<%#Eval("ID") %>" value="<%#Eval("MetaKeywords") %>" /></td>
                                <td class="td-end">
                                    <input type="text" class="input-text" name="MetaDescription<%#Eval("ID") %>" value="<%#Eval("MetaDescription") %>" /></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="4" class="td-end" style="text-align: center">
                            <input type="button" value="保存修改"
                                class="admin-button" onclick="document.forms['EditForm'].submit();" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

