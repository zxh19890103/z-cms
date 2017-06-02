<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
 Inherits="Nt.Pages.Member.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" Runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                重设密码
            </div>
            <form action="<%=LocalUrl %>" method="post">
                <table class="admin-table">
                    <tr>
                        <td class="left">
                            请输入密码：
                        </td>
                        <td class="right">
                            <input type="password" class="input-text" maxlength="255" name="NewPassword" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            请再次输入密码：
                        </td>
                        <td class="right">
                            <input type="password" class="input-text" maxlength="255" name="NewPassword.Again" />
                        </td>
                    </tr>
                    <tr class="admin-table-footer">
                        <td class="left"></td>
                        <td class="right">
                            <input type="submit" class="admin-button" value="更改" />
                            <input type="button" class="admin-button" value="返回" onclick="window.location.href = 'List.aspx'" />
                            <input name="UserID" type="hidden" value="<%=UserID %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

