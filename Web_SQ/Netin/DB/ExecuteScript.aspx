<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.DB.ExecuteScript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                远程执行Sql脚本
            </div>
            <form id="form1" action="<%=LocalUrl %>" method="post">
                <table class="admin-table">
                    <tr>
                        <th class="td-end">可以在这里远程执行Sql脚本</th>
                    </tr>
                    <tr>
                        <td class="td-end">
                            <textarea cols="1" rows="3" name="Script"></textarea></td>
                    </tr>
                    <tr>
                        <td class="td-end">
                            <input type="button" value="执行脚本" class="admin-button"
                                onclick="document.getElementById('form1').submit();" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

