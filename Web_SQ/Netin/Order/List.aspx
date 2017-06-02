<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Order.List" %>

<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function viewMemberInfo(id){
            openWindow('/Netin/Shared/MemberInfo.aspx?Member_Id='+id,'');
        }

        function openOrderStatusSelection(sender,current,id)
        {
            if($(sender).data('current'))
                current=$(sender).data('current');
            var data=<%=NtUtility.GetJsObjectArrayFromList(Service.OrderStatusProvider)%>;
            openSelectionWindow('订单状态设置',
                data
                ,current,function(v,t){
                    if(v!=current){
                        ntAjax({
                            method:'SetEnumValue',
                            data:'{"tab":"Nt_Order","field":"Status","id":"'+id+'","value":"'+v+'"}',
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
                用户订单
            </div>

            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-select">选择</th>
                    <th class="td-order">订单状态</th>
                    <th>标题</th>
                    <th>联系人</th>
                    <th class="td-published-date">下单日期</th>
                    <th class="th-end" style="width: 180px;">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td>
                                <a href="javascript:;" onclick="openOrderStatusSelection(this,<%#Eval("Status") %>,<%#Eval("ID") %>);"><%#Service.GetStatusName(Eval("Status")) %></a>
                            </td>
                            <td><%#Eval("Title") %></td>
                            <td><%#Eval("LinkMan") %></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                            <td class="th-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-view-detail" title="查看"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <a href="javascript:;" onclick="viewMemberInfo(<%#Eval("Member_Id") %>)" class="admin-view-user-info" title="下单者信息"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td>
                                <a href="javascript:;" onclick="openOrderStatusSelection(this,<%#Eval("Status") %>,<%#Eval("ID") %>);"><%#Service.GetStatusName(Eval("Status")) %></a>
                            </td>
                            <td><%#Eval("Title") %></td>
                            <td><%#Eval("LinkMan") %></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                            <td class="th-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-view-detail" title="查看"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <a href="javascript:;" onclick="viewMemberInfo(<%#Eval("Member_Id") %>)" class="admin-view-user-info" title="下单者信息"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td class="td-select">
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td colspan="4">
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

