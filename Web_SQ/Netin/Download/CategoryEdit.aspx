﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Download.CategoryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>下载类别
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">父类别：</td>
                        <td class="right">
                            <label><%=ParentName %></label>
                            <input name="Parent" type="hidden" class="input-text" value="<%=ParentID %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">类别名称：</td>
                        <td class="right">
                            <input type="text" class="input-no-comma input-text" name="Name" maxlength="255" value="<%=Model.Name %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true, "Display", new{ _class="admin-checkbox-list" })%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=Model.Id %>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                            <input type="hidden" name="IsDownloadable" value="True" />
                            <input type="hidden" name="Language_Id" value="<%=LanguageID %>" />
                            <input type="hidden" name="Crumbs" value="0" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>
