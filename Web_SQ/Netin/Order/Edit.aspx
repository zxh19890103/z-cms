<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
 Inherits="Nt.Pages.Order.Edit" %>

<%@ Register Src="~/Netin/Shared/MemberInfo.ascx" TagPrefix="uc1" TagName="MemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>订单信息
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">选择用户：</td>
                        <td class="right">
                            <%=HtmlHelper.DropdownList(MemberCollection, new { _class="category-drop-down",name="Member_Id"})%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Title" maxlength="512" value="<%=Model.Title %>" /></td>
                    </tr>

                    <tr>
                        <td class="left">联系人：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="LinkMan" value="<%=Model.LinkMan %>" /></td>
                    </tr>

                    <tr>
                        <td class="left">订单状态：</td>
                        <td class="right">
                            <%=HtmlHelper.DropdownList(Service.OrderStatusProvider
                            , new { name="Status",_class="category-drop-down"})%>
                            </td>
                    </tr>

                    <tr>
                        <td class="left">备注：</td>
                        <td class="right">
                            <textarea cols="1" rows="2" name="Note"><%=Model.Note%></textarea>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">订单回复：</td>
                        <td class="right">
                            <textarea cols="1" rows="2" name="ReplyContent"><%=Model.ReplyContent%></textarea>
                            <input type="hidden" name="ReplyDate" value="<%=DateTime.Now %>" />
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="AddDate" value="<%=DateTime.Now %>" />
                            <input type="hidden" name="EditDate" value="<%=DateTime.Now %>" />
                        </td>
                    </tr>

                </table>
            </form>
            <hr />
        </div>
    </div>
</asp:Content>

