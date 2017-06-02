<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
 Inherits="Nt.Pages.SinglePage.SpecialPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                专题页管理
                <%if (IsAdministrator)
                  { %>
                &nbsp;<a href="SpecialPageEdit.aspx"  title="添加">添加</a>
                <%} %>
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th>编号</th>
                    <th>路径</th>
                    <th>显示</th>
                    <th>排序</th>
                    <th>备注</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("Id") %></td>
                            <td><%#Eval("Path") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td title="<%#Eval("Note") %>">
                                <%#NtUtility.GetSubString(Eval("Note").ToString(),30) %>
                            </td>
                            <td class="td-end">

                                <a href="SpecialPageEdit.aspx?Id=<%#Eval("Id")%>" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  { %>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("Id") %></td>
                            <td><%#Eval("Path") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td title="<%#Eval("Note") %>">
                                <%#NtUtility.GetSubString(Eval("Note").ToString(),30) %>
                            </td>
                            <td class="td-end">

                                <a href="SpecialPageEdit.aspx?Id=<%#Eval("Id")%>" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  { %>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

