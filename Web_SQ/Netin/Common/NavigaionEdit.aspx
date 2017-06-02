<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.NavigationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>导航
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">父级导航</td>
                        <td class="right">
                            <label><%=ParentName %></label>
                            <input name="Parent" type="hidden" class="input-text" value="<%=ParentID %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">导航名称</td>
                        <td class="right">
                            <input type="text" class="input-text no-comma" name="Name" value="<%=Model.Name %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">链接</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Path" value="<%=Model.Path %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">静态链接</td>
                        <td class="right">
                            <input type="text" class="input-text" name="HtmlPath" value="<%=Model.HtmlPath %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">链接目标</td>
                        <td class="right">
                            <select name="AnchorTarget" class="category-drop-down">
                                <%
                                    foreach (string item in AnchorTargetProvider)
                                    {
                                        if (Model.AnchorTarget == item)
                                            Response.Write(string.Format(
                                            "<option selected=\"selected\" value=\"{0}\">{0}</option>", item));
                                        else
                                            Response.Write(string.Format(
                                            "<option value=\"{0}\">{0}</option>", item));
                                    }
                                %>
                            </select>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true, "Display", new{})%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">推广标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="MetaTitle" value="<%=Model.MetaTitle %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">推广关键词：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="MetaKeyWords"><%=Model.MetaKeywords %></textarea><span>*请勿超过255个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">推广描述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="MetaDescription"><%=Model.MetaDescription %></textarea><span>*请勿超过255个字符。</span>
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Crumbs" value="<%=Model.Crumbs %>" />
                            <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
</asp:Content>

