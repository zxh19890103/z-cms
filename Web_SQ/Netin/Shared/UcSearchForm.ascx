<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Pages.Shared.UcSearchForm" %>
<div class="admin-main">
    <div class="admin-main-title">
        搜索
    </div>
    <form action="<% =LocalUrl%>" method="get" id="SearchForm" name="SearchForm" enctype="application/x-www-form-urlencoded">
        <table class="admin-table">
            <tr>
                <td class="left">类别：</td>
                <td class="right">
                    <%=HtmlHelper.DropdownList(Categories, new { _class = "category-drop-down", 
    name =ConstStrings.SEARCH_CATEGORY }) %>
                </td>
            </tr>

            <tr>
                <td class="left">标题：</td>
                <td class="right">
                    <input type="text" name="<%=ConstStrings.SEARCH_TITLE %>" class="input-text" value="<%=SearchTitle %>" />
                </td>
            </tr>

            <tr style="height: 60px;">
                <td></td>
                <td class="right td-end">
                    <input type="submit" class="admin-button" value="搜索" />
                    <input type="button"  value="清除搜索" class="admin-button" onclick="clearSearch(); return false;" />
                    <input type="reset" class="admin-button" value="重置" />
                    <input type="hidden" name="IsSearch" value="Yes" />
                </td>
            </tr>
        </table>
    </form>
</div>
