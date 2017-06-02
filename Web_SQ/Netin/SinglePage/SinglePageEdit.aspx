<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.SinglePage.SinglePageEdit" %>

<%@ Register Src="~/Netin/Shared/UcEditor.ascx" TagPrefix="uc1" TagName="UcEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <uc1:UcEditor runat="server" ID="UcEditor" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
                <div class="admin-main-title">
                    <%=EditTitlePrefix %>二级页面
                  <input type="submit" class="admin-button-head-save" value="保存" />
                    <input type="button" class="admin-button-head-back" onclick="<%=GoBackScript()%>" value="返回" />
                </div>

                <table class="admin-table">
                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Title" name="Title" maxlength="512" value="<%=Model.Title %>" />
                            <a href="javascript:;" onclick="copyTitle();">复制标题</a>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">推广关键词：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" id="MetaKeywords" name="MetaKeywords"><%=Model.MetaKeyWords %></textarea>
                            <span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">推广描述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" id="MetaDescription" name="MetaDescription"><%=Model.MetaDescription %></textarea>
                            <span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">内容简述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Short"><%=Model.Short%></textarea>
                            <span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">内容：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Body" style="width: 800px; height: 400px; visibility: hidden;"><%=Server.HtmlEncode(Model.Body) %></textarea>
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Language_Id" value="<%=LanguageID%>" />
                            <input type="hidden" name="Display" value="True" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

