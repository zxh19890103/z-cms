<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Member.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>会员
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">会员组：</td>
                        <td class="right">
                            <%=HtmlHelper.DropdownList(AvailableMemberRoles, new {_class="category-drop-down",name="MemberRole_Id" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">会员名：</td>
                        <td class="right">
                            <input type="hidden" name="oldLoginName" value="<%=Model.LoginName %>" />
                            <input type="text" class="input-text" name="LoginName" value="<%=Model.LoginName %>" />
                        </td>
                    </tr>

                    <%if (!EnsureEdit)
                      { %>
                    <tr>
                        <td class="left">密码：</td>
                        <td class="right">
                            <input type="password" class="input-text" name="Password" maxlength="255" value="<%=Model.Password %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">重输密码：</td>
                        <td class="right">
                            <input type="password" class="input-text" name="Password.Again" maxlength="255" value="<%=Model.Password %>" />
                        </td>
                    </tr>

                    <%} %>
                    <tr>
                        <td class="left">真实姓名：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="RealName" maxlength="255" value="<%=Model.RealName %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">性别：</td>
                        <td class="right">
                            <%=HtmlHelper.RadioBoxList("男","女",Model.Sex,"Sex") %>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">公司：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Company" maxlength="512" value="<%=Model.Company %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">地址：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Address" maxlength="512" value="<%=Model.Address %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">邮编：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="ZipCode" maxlength="255" value="<%=Model.ZipCode %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">手机号码：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="MobilePhone" maxlength="255" value="<%=Model.MobilePhone %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">电话号码：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Phone" maxlength="255" value="<%=Model.Phone %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">传真：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Fax" maxlength="255" value="<%=Model.Fax %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">邮箱地址：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Email" maxlength="255" value="<%=Model.Email %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否启用：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.Active,"Active",new{})%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">备注：</td>
                        <td class="right">
                            <textarea cols="4" rows="5" name="Note"><%=Model.Note %></textarea>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">添加时间：</td>
                        <td class="right">
                            <input type="text" name="AddDate" readonly="readonly" class="time input-datetime" value="<%=Model.AddDate.ToString("yyyy-MM-dd") %>" />
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td class="td-end" colspan="2">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="EditDate" value="<%=DateTime.Now %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

