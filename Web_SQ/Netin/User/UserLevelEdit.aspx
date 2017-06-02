<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
  Inherits="Nt.Pages.User.UserLevelEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <label id="EditPannelTitle">
                    <%=EditTitlePrefix %>管理员角色
                </label>
            </div>
            <form id="EditForm" name="EditForm"
                action="<%=LocalUrl%>"
                method="post" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">名称:</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Name" value="<%=Model.Name %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">系统名称:</td>
                        <td class="right">
                            <input type="text" class="input-text" name="SystemName" value="<%=Model.SystemName %>" />
                            <span>*请勿输入非英文字符，空格，标点符号，数字，特殊字符</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">是否启用:</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.Active,"Active",new{}) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">描述:</td>
                        <td class="right">
                            <textarea rows="5" cols="10" name="Description"><%=Model.Description %></textarea>
                        </td>
                    </tr>
                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

