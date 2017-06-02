<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Seo.FriendLinkEdit" %>

<%@ Register Src="~/Netin/Shared/UcPicture.ascx" TagPrefix="uc1" TagName="UcPicture" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%
        UcPicture.Picture_Id = Model.Picture_Id;
    %>
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title"><%=EditTitlePrefix %>友情链接</div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <%
                        if (WithImage)
                        {
                    %>
                    <tr>
                        <td class="left">图片:</td>
                        <td class="right">
                            <uc1:UcPicture runat="server" ID="UcPicture" />
                        </td>
                    </tr>
                    <%} %>

                    <tr>
                        <td class="left">链接Url:</td>
                        <td class="right">
                            <input type="text" name="Url" value="<%=Model.Url %>" class="input-text" />
                            <span class="admin-tips">请填写完整的网址，格式如：
                                <a href="javascript:;" onclick="document.EditForm.Url.value='http://www.naite.com.cn'">http[s]://www.naite.com.cn</a></span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">文本:</td>
                        <td class="right">
                            <textarea cols="1" rows="2" name="Text"><%=Model.Text %></textarea>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">备注:</td>
                        <td class="right">
                            <textarea cols="1" rows="2" name="Note"><%=Model.Note %></textarea></td>
                    </tr>

                    <tr>
                        <td class="left">生效:</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true, "Display", new { })%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序:</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5" name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">添加日期:</td>
                        <td class="right">
                            <input type="text" class="time input-datetime" name="AddDate" value="<%=Model.AddDate.ToString("yyyy-MM-dd") %>" />
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>

</asp:Content>

