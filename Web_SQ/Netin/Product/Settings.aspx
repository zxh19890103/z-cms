<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Product.Settings" %>

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
                上传图片设置
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">启用列表页缩略图：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableThumbnail, "EnableThumbnail", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">列表页缩略图大小：</td>
                        <td class="right">高:
                            <input type="text" class="input-int32" maxlength="5"  value="<%=Model.ThumbnailHeight %>" name="ThumbnailHeight" />
                            <br />
                            <br />
                            宽:
                            <input type="text" class="input-int32" maxlength="5"  value="<%=Model.ThumbnailWidth %>" name="ThumbnailWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">列表页缩略图生成模式：</td>
                        <td class="right">
                            <%
                                for (int i = 0; i < ThumnailModeProvider.Length; i++)
                                {
                                    Response.Write(
                                        string.Format("<input type=\"radio\" {2} value=\"{0}\" name=\"ThumbnailMode\" /><label>{1}</label><br/>"
                                        , ThumnailModeProvider[i],
                                        ThumnailModeDescriptionProvider[i],
                                        ThumnailModeProvider[i].Equals(Model.ThumbnailMode) ? "checked=\"checked\"" : ""
                                        ));
                                }
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">启用首页缩略图：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableThumbOnHomePage, "EnableThumbOnHomePage", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">首页缩略图大小：</td>
                        <td class="right">高:
                            <input type="text" class="input-int32" maxlength="5"  value="<%=Model.ThumbOnHomePageHeight %>" name="ThumbOnHomePageHeight" />
                            <br />
                            <br />
                            宽:
                            <input type="text" class="input-int32" maxlength="5"  value="<%=Model.ThumbOnHomePageWidth %>" name="ThumbOnHomePageWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">首页缩略图生成模式：</td>
                        <td class="right">
                            <%
                                for (int i = 0; i < ThumnailModeProvider.Length; i++)
                                {
                                    Response.Write(
                                        string.Format("<input type=\"radio\" {2} value=\"{0}\" name=\"ThumbOnHomePageMode\" /><label>{1}</label><br/>"
                                        , ThumnailModeProvider[i],
                                        ThumnailModeDescriptionProvider[i],
                                        ThumnailModeProvider[i].Equals(Model.ThumbOnHomePageMode) ? "checked=\"checked\"" : ""
                                        ));
                                }
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">启用文字水印：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableTextMark, "EnableTextMark", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">水印文字：</td>
                        <td class="right">
                            <input type="text" class="input-text" value="<%=Model.TextMark %>" name="TextMark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">水印位置：</td>
                        <td class="right">
                            <select class="category-drop-down" name="Position">
                                <%
                                    for (int i = 1; i <= PositionProvider.Length; i++)
                                    {
                                        if (Model.Position == i)
                                            Response.Write(string.Format("<option selected=\"selected\" value=\"{0}\">{1}</option>", i, PositionProvider[i - 1]));
                                        else
                                            Response.Write(string.Format("<option value=\"{0}\">{1}</option>", i, PositionProvider[i - 1]));
                                    }
                                %>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">水印字体：</td>
                        <td class="right">
                            <select class="category-drop-down" name="FontFamily">
                                <%
                                    for (int i = 0; i < FontFamilyProvider.Length; i++)
                                    {
                                        if (Model.FontFamily.Equals(FontFamilyProvider[i], StringComparison.OrdinalIgnoreCase))
                                            Response.Write(string.Format("<option selected=\"selected\" value=\"{0}\">{0}</option>", FontFamilyProvider[i]));
                                        else
                                            Response.Write(string.Format("<option value=\"{0}\">{0}</option>", FontFamilyProvider[i]));
                                    }
                                %>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">文本框宽度：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  value="<%=Model.WidthOfTextBox %>" name="WidthOfTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">文本框高度：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  value="<%=Model.HeightOfTextBox %>" name="HeightOfTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">启用图片水印：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableImgMark, "EnableImgMark", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">水印图片：</td>
                        <td class="right">
                            <uc1:UcPicture runat="server" ID="UcPicture" />
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="reset" class="admin-button" value="重置" />
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
</asp:Content>

