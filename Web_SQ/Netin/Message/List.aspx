<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Message.List" %>

<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function viewMemberInfo(id){
            openWindow('/Netin/Shared/MemberInfo.aspx?Member_Id='+id,'');
        }

        function doReply(id){
            openWindow('MessageReply.aspx?Message_Id='+id,'');
        }

        function openMessageStatusSelection(sender,current,id)
        {
            if($(sender).data('current'))
                current=$(sender).data('current');
            var data=<%=NtUtility.GetJsObjectArrayFromList(Service.MessageStatusProvider)%>;
            openSelectionWindow('留言状态',
                data
                ,current,function(v,t){
                    if(v!=current){
                        ntAjax({
                            method:'SetEnumValue',
                            data:'{"tab":"Nt_Message","field":"Status","id":"'+id+'","value":"'+v+'"}',
                            success:function(msg){
                                var json=$.parseJSON(msg.d);
                                ntAlert(json.message);
                                $(sender).text(t);
                                $(sender).data('current',v);
                            }
                        })
                    }
                })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                用户留言
            </div>

            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-select">选择</th>
                    <th class="td-order">排序</th>
                    <th class="td-order">留言状态</th>
                    <th>标题</th>
                    <th>留言类型</th>
                    <th>联系人</th>
                    <th class="td-published-date">留言日期</th>
                    <th class="td-attributes">显示\置顶</th>
                    <th class="th-end" style="width: 180px;">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td><a href="javascript:;" onclick="openMessageStatusSelection(this,<%#Eval("Status") %>,<%#Eval("ID") %>);"><%#Service.GetStatusName(Eval("Status")) %></a></td>
                            <td><%#Eval("Title") %></td>
                            <td><%#GetCatalogName(Eval("Type"))%></td>
                            <td><%#Eval("LinkMan") %></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                                <%#HtmlHelper.BoolLabel("置顶", "未置顶", Eval("SetTop"), new {itemid = Eval("Id"), onclick="setTop(this)",_class="lbl-ajax"})%>
                            </td>

                            <td class="th-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-view-detail" title="查看详细"></a>
                                <a href="javascript:;" onclick="doReply(<%#Eval("Id") %>);" class="admin-reply" title="留言回复"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td><a href="javascript:;" onclick="openMessageStatusSelection(this,<%#Eval("Status") %>,<%#Eval("ID") %>);"><%#Service.GetStatusName(Eval("Status")) %></a></td>
                            <td><%#Eval("Title") %></td>
                            <td><%#GetCatalogName(Eval("Type"))%></td>
                            <td><%#Eval("LinkMan") %></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                                <%#HtmlHelper.BoolLabel("置顶", "未置顶", Eval("SetTop"), new {itemid = Eval("Id"), onclick="setTop(this)",_class="lbl-ajax"})%>
                            </td>

                            <td class="th-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-view-detail" title="查看详细"></a>
                                <a href="javascript:;" onclick="viewMemberInfo(<%#Eval("Member_Id") %>);" class="admin-view-user-info" title="留言者信息"></a>
                                <a href="javascript:;" onclick="doReply(<%#Eval("Id") %>);" class="admin-reply" title="留言回复"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
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
        </div>
    </div>
</asp:Content>

