<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
  Inherits="Nt.Pages.SinglePage.SpecialPageEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>专题页
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">路径：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Path" maxlength="512" value="<%=Model.Path %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <textarea cols="2" rows="2" name="Note"><%=Model.Note %></textarea>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox("显示", Model.Display,"Display", new { })%>
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Language_Id" value="<%=LanguageID%>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

