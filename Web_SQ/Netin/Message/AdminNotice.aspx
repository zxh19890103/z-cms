<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Message.AdminNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                管理员公告
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Title" value="<%=Model.Title%>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">公告内容：</td>
                        <td class="right">
                            <textarea cols="2" rows="3" name="Content"><%=Model.Content%></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">添加日期：</td>
                        <td class="right">
                            <input type="text" class="time input-datetime" name="AddDate" value="<%=Model.AddDate.ToString("yyyy-MM-dd")%>" />
                        </td>
                    </tr>
                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="reset" class="admin-button" value="重置" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

