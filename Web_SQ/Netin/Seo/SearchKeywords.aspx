<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Seo.SearchKeyword" %>

<%@ Register Src="~/Netin/Dialog/SearchKeyWordEdit.ascx" TagPrefix="uc1" TagName="SearchKeyWordEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
         <div class="admin-tips">
            1.您添加的关键词将会绑定到前端页面的搜索框(如果有)
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                搜索关键词管理
                &nbsp;
                <a href="javascript:;" onclick="ajaxCommonAdd();"  title="添加">添加</a>
            </div>

            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-select">选择</th>
                    <th class="td-order">排序</th>
                    <th class="td-display">显示</th>
                    <th>关键词</th>
                    <th class="td-edit-del th-end">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" /></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%></td>
                            <td>
                                <%#Eval("KeyWord") %>
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="ajaxCommonEdit(<%#Eval("Id") %>);" class="admin-edit" title="修改"></a>
                                <a href="javascript:;" onclick="ajaxCommonDel(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="admin-table-footer">
                    <td class="td-select">
                        <input type="checkbox" onclick="selectall(this)" />
                    </td>
                    <td class="td-order">
                        <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                    </td>
                    <td></td>
                    <td></td>
                    <td class="td-end">
                        <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" /></td>
                </tr>
            </table>
        </div>
    </div>
    <uc1:SearchKeyWordEdit runat="server" ID="SearchKeyWordEdit" />
</asp:Content>

