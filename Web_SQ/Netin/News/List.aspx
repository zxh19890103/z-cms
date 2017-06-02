<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.News.List" %>

<%@ Register Src="~/Netin/Shared/UcSearchForm.ascx" TagPrefix="uc1" TagName="UcSearchForm" %>
<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%UcSearchForm.Categories = this.AvailableNewsCategories; %>
    <div class="admin-main-wrap">
        <uc1:UcSearchForm runat="server" ID="UcSearchForm" FormAction="List.aspx" />
        <div class="admin-main">
            <div class="admin-main-title">
                新闻列表
                &nbsp;<a href="Edit.aspx" title="添加">添加</a>
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
                            <th class="td-attributes">属性</th>
                            <th class="td-published-date">发布日期</th>
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
                                <%#Eval("CategoryName") %>
                            </td>
                            <td>
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>"><%#Eval("Title") %></a>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("置顶", "未置顶", Eval("SetTop"), new {itemid = Eval("Id"), onclick="setTop(this)",_class="lbl-ajax"})%>
                                 &nbsp;
                                 &nbsp;
                                 <%#HtmlHelper.BoolLabel("推荐", "未推荐", Eval("Recommended"), new { itemid = Eval("Id"), onclick="recommend(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
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
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new {itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax" })%>
                            </td>
                            <td>
                                <%#Eval("CategoryName") %>
                            </td>
                            <td>
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>"><%#Eval("Title") %></a>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("置顶", "未置顶", Eval("SetTop"), new {itemid = Eval("Id"), onclick="setTop(this)",_class="lbl-ajax"})%>
                                 &nbsp;
                                 &nbsp;
                                 <%#HtmlHelper.BoolLabel("推荐", "未推荐", Eval("Recommended"), new { itemid = Eval("Id"), onclick="recommend(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td colspan="8" class="td-end">
                                <%=HtmlHelper.DropdownList(AvailableNewsCategories, new { _class = "category-drop-down", name = "ToCategoryId" }) %>
                                &nbsp;&nbsp;<input type="button" class="admin-button" value="批量转移" onclick="batchMigrate()" />
                            </td>
                        </tr>
                        <tr class="admin-table-footer">
                            <td class="td-select">
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td class="td-order">
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                            </td>
                            <td colspan="5">
                                <uc1:UcNtPager runat="server" id="UcNtPager" />
                            </td>
                            <td class="td-edit-del td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

