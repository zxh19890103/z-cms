<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.Content" %>

<%@ Register Src="~/Netin/Dialog/ContentEdit.ascx" TagPrefix="uc1" TagName="ContentEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            1.通用内容用于在网站中展示活动内容<br />
            2.通用内容的图片视需要上传
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                通用内容管理<%if (IsAdministrator)
                        { %>
                &nbsp;<a href="ContentEdit.aspx"  title="添加">添加</a>
                <%} %>
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-order">编号</th>
                    <th class="td-picture">图片</th>
                    <th>标题</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("Id") %></td>
                            <td>
                                <img width="100" height="<%=ThumbnailSize %>"  src="<%#Eval("PictureUrl") %>" alt="图片" />
                            </td>
                            <td><%#Eval("Title") %></td>
                            <td class="td-end">
                                <a href="ContentEdit.aspx?ID=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  {%>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("Id") %></td>
                            <td>
                                <img width="100" height="<%=ThumbnailSize %>"  src="<%#Eval("PictureUrl") %>" alt="图片" />
                            </td>
                            <td><%#Eval("Title") %></td>
                            <td class="td-end">
                                <a href="ContentEdit.aspx?ID=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <%if (IsAdministrator)
                                  {%>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <uc1:ContentEdit runat="server" ID="ContentEdit" />
</asp:Content>

