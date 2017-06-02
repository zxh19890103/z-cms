<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout2.master" AutoEventWireup="true" 
    Inherits="Nt.Pages.Book.BookReply" %>

<%@ Register Src="~/Netin/Dialog/BookReplyEdit.ascx" TagPrefix="uc1" TagName="BookReplyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" Runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                预订/留言回复
                &nbsp;
                <a href="javascript:;" onclick="ajaxCommonAdd();">添加回复</a>
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-select">选择</th>
                    <th>回复人</th>
                    <th class="td-display">显示</th>
                    <th class="td-order">排序</th>
                    <th class="td-published-date">回复日期</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr  class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td><%#Eval("ReplyMan") %></td>
                            <td><%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new {itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax" })%></td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" /></td>
                            <td><%#Eval("ReplyDate","{0:yyyy-MM-dd}") %></td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="ajaxCommonEdit(<%#Eval("Id") %>)" class="admin-edit" title="修改"></a>
                                
                                 <a href="javascript:;" onclick="ajaxCommonDel(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr  class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td><%#Eval("ReplyMan") %></td>
                            <td><%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new {itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax" })%></td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" /></td>
                            <td><%#Eval("ReplyDate","{0:yyyy-MM-dd}") %></td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="ajaxCommonEdit(<%#Eval("Id") %>)" class="admin-edit" title="修改"></a>
                                
                                 <a href="javascript:;" onclick="ajaxCommonDel(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td>
                                <input type="checkbox" onclick="selectall(this)" /></td>
                            <td></td>
                            <td></td>
                            <td>
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" /></td>
                            <td></td>
                            <td class="td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" /></td>
                        </tr>
                        <tr class="admin-table-footer">
                            <td colspan="6" style="text-align:center;" class="td-end">
                                <input type="button" class="admin-button" value="关闭窗口" onclick="window.close();" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <uc1:BookReplyEdit runat="server" ID="BookReplyEdit" />
</asp:Content>

