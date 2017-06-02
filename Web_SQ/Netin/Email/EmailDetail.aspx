<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout2.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Email.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                邮件详细
            </div>
            <table class="admin-table">
                <tr>
                    <td class="left">发送自:</td>
                    <td class="right"><%=Model.From %></td>
                </tr>
                <tr>
                    <td class="left">发送至:</td>
                    <td class="right"><%=Model.To %></td>
                </tr>
                <tr>
                    <td class="left">主题:</td>
                    <td class="right"><%=Model.Subject %></td>
                </tr>
                <tr>
                    <td class="left">内容:</td>
                    <td class="right"><%=Model.Body %></td>
                </tr>
                <tr>
                    <td class="left">添加日期:</td>
                    <td class="right"><%=Model.AddDate.ToString("yyyy-MM-dd") %></td>
                </tr>
                <tr>
                    <td class="left">发送日期:</td>
                    <td class="right"><%=Model.SentDate.ToString("yyyy-MM-dd") %></td>
                </tr>

                <tr>
                    <td class="td-end" style="text-align: center;" colspan="2">
                        <input type="button" class="admin-button" value="关闭窗口" onclick="window.close();" />
                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>

