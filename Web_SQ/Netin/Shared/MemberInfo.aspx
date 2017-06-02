<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout2.master" AutoEventWireup="true"
Inherits="Nt.Pages.Shared.MemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                用户信息
            </div>
            <table class="admin-table">
                <tr>
                    <td class="left">姓名：</td>
                    <td class="right"><%=Model.RealName %></td>
                </tr>
                <tr>
                    <td class="left">性别：</td>
                    <td class="right"><%=Model.Sex?"男":"女" %></td>
                </tr>
                <tr>
                    <td class="left">公司名：</td>
                    <td class="right"><%=Model.Company %></td>
                </tr>
                <tr>
                    <td class="left">地址：</td>
                    <td class="right"><%=Model.Address %></td>
                </tr>
                <tr>
                    <td class="left">联系方式：</td>
                    <td class="right">
                        电话:<%=Model.Phone %><br />
                        手机:<%=Model.MobilePhone %><br />
                        传真:<%=Model.Fax %><br />
                        邮箱:<%=Model.Email %>
                    </td>
                </tr>
                <tr>
                    <td class="left">备注信息：</td>
                    <td class="right">
                        <%=Model.Note %>
                    </td>
                </tr>
                <tr>
                    <td class="left">邮编：</td>
                    <td class="right"><%=Model.ZipCode %> </td>
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

