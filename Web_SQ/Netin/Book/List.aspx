<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
    Inherits="Nt.Pages.Book.List" %>

<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function doReply(id){
            openWindow('BookReply.aspx?Book_Id='+id,'');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                预订列表
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-select">选择</th>
                            <th class="td-order">排序</th>
                            <th class="td-display">是否显示</th>
                            <th>类别</th>
                            <th>标题</th>
                            <th>姓名</th>
                            <th>邮箱</th>
                            <th class="td-published-date">添加日期</th>
                            <th class="td-edit-del th-end">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#GetCatalogName(Eval("Type")) %>
                            </td>
                            <td>
                                <a href="Detail.aspx?Id=<%#Eval("Id") %>"><%#Eval("Title") %></a>
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#Eval("Email") %>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="Detail.aspx?Id=<%#Eval("Id") %>" class="admin-view-detail" title="查看"></a>
                                
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                
                                <a href="javascript:;" onclick="doReply(<%#Eval("Id") %>);" class="admin-reply" title="留言回复"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#GetCatalogName(Eval("Type")) %>
                            </td>
                            <td>
                                <a href="Detail.aspx?Id=<%#Eval("Id") %>"><%#Eval("Title") %></a>
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#Eval("Email") %>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                               <a href="Detail.aspx?Id=<%#Eval("Id") %>" class="admin-view-detail" title="查看"></a>
                                
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                
                                <a href="javascript:;" onclick="doReply(<%#Eval("Id") %>);" class="admin-reply" title="留言回复"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td class="td-select">
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td class="td-order">
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                            </td>
                            <td colspan="6">
                                <uc1:UcNtPager runat="server" ID="UcNtPager" />
                            </td>
                            <td class="td-edit-del td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <!--Pager-->

        </div>
    </div>
</asp:Content>
