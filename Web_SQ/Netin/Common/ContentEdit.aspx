<%@ Page Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.ContentEdit" %>

<%@ Register Src="~/Netin/Shared/UcPicture.ascx" TagPrefix="uc1" TagName="UcPicture" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%
        UcPicture.Picture_Id = Model.Picture_Id;
    %>
    <div class="admin-main-wrap">
        <div class="admin-tips">
            1.通用内容用于在网站中展示活动内容<br />
            2.通用内容的图片视需要上传
        </div>
        <div class="admin-main">
            <div class="admin-main-title"><%=EditTitlePrefix %>通用内容</div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">图片:</td>
                        <td class="right">
                            <uc1:UcPicture runat="server" ID="UcPicture" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">标题:</td>
                        <td class="right">
                            <input type="text" class="input-text" maxlength="255" name="Title" value="<%=Model.Title %>" />
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

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Display" value="True" />
                            <input type="hidden" name="Language_Id" value="<%=LanguageID %>" />
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
</asp:Content>
