<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Order.Detail" %>

<%@ Register Src="~/Netin/Shared/MemberInfo.ascx" TagPrefix="uc1" TagName="MemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        /*保存订单备注*/
        function saveOrderNote(id) {
            saveNote(id,
               document.getElementById('OrderNote').value
               , 'Nt_Order');
        }

        /*保存订单回复*/
        function saveOrderReply(id) {
            ntAjax(
        {
            method: "SaveOrderReply",
            data: '{"id":"' + id + '","reply":"' + document.getElementById('OrderReply').value + '"}',
            success: function (msg) {
                var json = $.parseJSON(msg.d);
                alert(json.message)
            }
        }
        )
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%MemberInfo.MemberID = Model.Member_Id; %>
    <div class="admin-main-wrap">
        <div class="admin-main">
            <!--留言查看并回复-->
            <div class="admin-main-title">
                订单详情
            </div>
            <table class="admin-table">
                <tr>
                    <td class="left">订单编号：</td>
                    <td class="right"><%=Model.OrderCode %></td>
                </tr>
                <tr>
                    <td class="left">标题：</td>
                    <td class="right"><%=Model.Title %></td>
                </tr>
                <tr>
                    <td class="left">联系人：</td>
                    <td class="right"><%=Model.LinkMan %></td>
                </tr>
                <tr>
                    <td class="left">订单状态：</td>
                    <td class="right"><%=Service.GetStatusName(Model.Status) %></td>
                </tr>
                <tr>
                    <td class="left">下单日期：</td>
                    <td class="right"><%=Model.AddDate.ToString("yyyy-MM-dd") %></td>
                </tr>
                <tr>
                    <td class="left">备注：</td>
                    <td class="right">
                        <textarea cols="1" rows="2" name="OrderNote" id="OrderNote"><%=Model.Note%></textarea>
                        <br />
                        <input type="button" class="admin-button" value="保存" onclick="saveOrderNote(<%=Model.Id%>);" />
                    </td>
                </tr>
                <tr>
                    <td class="left">订单回复：</td>
                    <td class="right">
                        <textarea cols="1" rows="2" id="OrderReply" name="OrderReply"><%=Model.ReplyContent%></textarea>
                        <br />
                        <input type="button" class="admin-button" value="保存" onclick="saveOrderReply(<%=Model.Id%>);" />
                    </td>
                </tr>
            </table>
        </div>
        <uc1:MemberInfo runat="server" ID="MemberInfo" />
    </div>
</asp:Content>

