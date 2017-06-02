<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.BannerEdit" %>

<%@ Register Src="~/Netin/Shared/UcPicture.ascx" TagPrefix="uc1" TagName="UcPicture" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">

    <%
        UcPicture.Picture_Id = Model.Picture_Id;
    %>

    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>Banner
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Title" maxlength="255" value="<%=Model.Title%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">文本：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Text"><%=Model.Text%></textarea><span class="admin-tips">请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">链接：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Url" maxlength="512" value="<%=Model.Url%>" />
                            <span class="admin-tips">请填写完整的网站链接，如http://www.naite.com.cn
                            </span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true, "Display", new{ _class="admin-checkbox-list" })%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">备注：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Note"><%=Model.Note%></textarea>
                            <span class="admin-tips">请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">上传图片：</td>
                        <td class="right">
                            <uc1:UcPicture runat="server" ID="UcPicture" />
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Language_Id" value="<%=LanguageID %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>
