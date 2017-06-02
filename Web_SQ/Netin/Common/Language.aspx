<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.Language" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                语言版本管理
                &nbsp;<a href="LanguageEdit.aspx" title="">添加语言版本</a>
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-order">排序</th>
                            <th>国旗</th>
                            <th>名称</th>
                            <th class="td-display">是否启用</th>
                            <th class="td-published-date">资源文件名</th>
                            <th class="th-end td-edit-del">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <img src="/Netin/Content/Flags/<%#Eval("LanguageCode") %>.png" alt="<%#Eval("LanguageCode") %>" />
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("已发布", "未发布", Eval("Published"), new { itemid = Eval("Id"), onclick="publish(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("ResxPath") %>
                            </td>
                            <td class="td-end">
                                <a href="LanguageEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>

                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <img src="/Netin/Content/Flags/<%#Eval("LanguageCode") %>.png" alt="<%#Eval("LanguageCode") %>" />
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("已发布", "未发布", Eval("Published"), new { itemid = Eval("Id"), onclick="publish(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <%#Eval("ResxPath") %>
                            </td>
                            <td class="td-end">
                                <a href="LanguageEdit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>

                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td class="td-order">
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                            </td>
                            <td class="td-end" colspan="5"></td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

</asp:Content>
