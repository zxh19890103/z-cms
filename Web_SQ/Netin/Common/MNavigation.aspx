<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
    Inherits="Nt.Pages.Common.MNavigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                微导航管理
                &nbsp;<a href="MNavigaionEdit.aspx"title="添加一级导航">添加一级导航</a>
                &nbsp;<a href="CopyNavigation.aspx" title="将导航拷贝至其他语言版本">将导航拷贝至其他语言版本</a>
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th>编号</th>
                    <th>导航名称</th>
                    <th>生效</th>
                    <th>排序</th>
                    <th>链接</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("Id") %></td>
                            <td style="text-align: left">
                                <%#Eval("Crumbs") %>
                                <%if (IsAdministrator)
                                  { %>
                                <a href="NavigaionEdit.aspx?PId=<%#Eval("Id") %>" class="add-child">添加子导航</a>
                                <%} %>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("生效", "未生效", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#Eval("Path") %>
                            </td>
                            <td class="td-end">
                                <a href="NavigaionEdit.aspx?id=<%#Eval("Id") %>" title="编辑" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  { %>
                                
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a><%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("Id") %></td>
                            <td style="text-align: left">
                                <%#Eval("Crumbs") %>
                                <%if (IsAdministrator)
                                  { %>
                                <a href="NavigaionEdit.aspx?PId=<%#Eval("Id") %>" class="add-child">添加子导航</a>
                                <%} %>
                            </td>                            <td>
                                <%#HtmlHelper.BoolLabel("生效", "未生效", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#Eval("Path") %>
                            </td>
                            <td class="td-end">
                                <a href="NavigaionEdit.aspx?id=<%#Eval("Id") %>" title="编辑" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  { %>
                                
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a><%} %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td colspan="3"></td>
                            <td class="td-order">
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                            </td>
                            <td colspan="2" class="td-end"></td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

