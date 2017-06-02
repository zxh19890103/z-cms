<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Seo.FriendLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            如果您网站中的友情链接设计为不带图片，可不上传图片
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                友情链接管理
                &nbsp;<a href="FriendLinkEdit.aspx" title="添加">添加</a>
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th>选择</th>
                    <%if (WithImage) Response.Write("<th>图片</th>");%>
                    <th>链接</th>
                    <th>显示</th>
                    <th>排序</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <%if (WithImage)
                              { %>
                            <td>
                                <img width="100" height="<%=ThumbnailSize %>" src="<%#Eval("PictureUrl") %>" alt="" /></td>
                            <%} %>
                            <td><%#Eval("Url") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td class="td-end">
                                <a href="FriendLinkEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <%if (WithImage)
                              { %>
                            <td>
                                <img width="100" height="<%=ThumbnailSize %>" src="<%#Eval("PictureUrl") %>" alt="" /></td>
                            <%} %>
                            <td><%#Eval("Url") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td class="td-end">
                                <a href="FriendLinkEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <td>
                            <input type="checkbox" onclick="selectall(this)" />
                        </td>
                        <%
                            if (WithImage)
                                Response.Write("<td></td>");
                        %>
                        <td></td>
                        <td></td>
                        <td>
                            <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" /></td>
                        <td class="td-end">
                            <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                        </td>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

