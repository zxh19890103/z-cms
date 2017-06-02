<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.JavaScript" %>

<%@ Register Src="~/Netin/Dialog/JavaScriptEdit.ascx" TagPrefix="uc1" TagName="JavaScriptEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            1.通用Js脚本用于在网站中执行指定的Js脚本程序<br />
            2.请保证Js脚本的正确(包含&lt;script&gt;&lt;/script&gt;)，否则页面将无法正确运行
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                Js脚本管理
                &nbsp;
            <a href="javascript:;" onclick="ajaxCommonAdd();"  title="添加">添加</a>
            </div>

            <table class="admin-table">
                <tr class="admin-table-header">
                    <th>编号</th>
                    <th>显示</th>
                    <th>顺序</th>
                    <th>备注</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td><%#Eval("Id") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td title="<%#Eval("Note") %>">
                                <%#NtUtility.GetSubString(Eval("Note").ToString(),30)%>
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="ajaxCommonEdit(<%#Eval("Id") %>)" class="admin-edit" title="修改"></a>

                                <a href="javascript:;" onclick="ajaxCommonDel(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td><%#Eval("Id") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td title="<%#Eval("Note") %>">
                                <%#NtUtility.GetSubString(Eval("Note").ToString(),30)%>
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="ajaxCommonEdit(<%#Eval("Id") %>)" class="admin-edit" title="修改"></a>

                                <a href="javascript:;" onclick="ajaxCommonDel(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
                <tr class="admin-table-footer">
                    <td></td>
                    <td></td>
                    <td>
                        <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" /></td>
                    <td colspan="2" class="td-end"></td>
                </tr>
            </table>
        </div>
    </div>
    <uc1:JavaScriptEdit runat="server" ID="JavaScriptEdit" />
</asp:Content>

