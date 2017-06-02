<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
   Inherits="Nt.Pages.Download.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                下载类别管理
                <%if(IsAdministrator){ %>
                &nbsp;<a href="CategoryEdit.aspx"  title="添加">添加</a>
                &nbsp;<a href="CopyCategory.aspx" title="将下载目录拷贝至其他语言版本">将下载目录拷贝至其他语言版本</a>
                <%} %>
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-order">排序</th>
                            <th>类别名称</th>
                            <th class="td-display">生效</th>
                            <th class="td-edit-del th-end">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("Crumbs") %>
                                <%if(IsAdministrator){ %>
                                <a href="CategoryEdit.aspx?PId=<%#Eval("Id") %>" class="add-child">添加子类别</a>
                                <%} %>
                                <a href="Edit.aspx?CategoryId=<%#Eval("Id") %>" class="add-new-record">添加下载</a>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td class="td-end">
                                <a href="CategoryEdit.aspx?id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <%if(IsAdministrator){ %>
                                
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td class="border_right" align="center">
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32  input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("Crumbs") %>
                                <%if(IsAdministrator){ %>
                                <a href="CategoryEdit.aspx?PId=<%#Eval("Id") %>" class="add-child">添加子类别</a>
                                <%} %>
                                <a href="Edit.aspx?CategoryId=<%#Eval("Id") %>" class="add-new-record">添加下载</a>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td class="td-end">
                                <a href="CategoryEdit.aspx?id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <%if(IsAdministrator){ %>
                                
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <%} %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td align="center">
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                            </td>
                            <td class="td-end" colspan="4"></td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

