<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
Inherits="Nt.Pages.Member.RegisterDeclare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                注册声明
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">注册声明：</td>
                        <td class="right">
                            <textarea cols="2" rows="3" name="Content"><%=Model.Content %></textarea>
                        </td>
                    </tr>
                    <tr style="height: 60px;">
                        <td></td>
                        <td class="td-end" style="text-align:left">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="reset" class="admin-button" value="重置" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

