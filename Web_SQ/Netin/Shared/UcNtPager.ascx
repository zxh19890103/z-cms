<%@ Control Language="C#" ClassName="UcNtPager" Inherits="Nt.Framework.NtUserControl" %>

<%
    INtPageForList page = NtPage as INtPageForList;
    NtPager pager = null;
    if (page != null)
        pager = page.Pager;
%>
<%
    if (page.NeedPagerize && pager != null)
    {
%>
<form action="<%=Request.Url.PathAndQuery %>" id="PagerForm" name="PagerForm" method="get" enctype="application/x-www-form-urlencoded">
    <span class="page-text">找到<span style="color: #ff6400"><%=pager.TotalRecords %></span>条记录,
       共<span style="color: #ff6400"><%=pager.PageCount %></span>页,
        当前第<span style="color: #ff6400"><%=pager.PageIndex %></span>页
    </span>
    <span class="admin-pager">
        <input type="button" value="首页" onclick="goto(<%=pager.HomePage%>);" />
        <input type="button" value="上一页" onclick="goto(<%=pager.PrePage%>);" />
        <%=HtmlHelper.DropdownList(pager.Pager,new{onchange="goto(this.value);",_class="page-select"}) %>
        <input type="button" value="下一页" onclick="goto(<%=pager.NextPage%>)" />
        <input type="button" value="末页" onclick="goto(<%=pager.EndPage%>)" />
        <input type="hidden" name="Page" value="<%=pager.PageIndex %>" />
        <input type="hidden" name="<%=ConstStrings.SEARCH_TITLE %>" value="<%=Request.QueryString[ConstStrings.SEARCH_TITLE] %>" />
        <input type="hidden" name="<%=ConstStrings.SEARCH_CATEGORY %>" value="<%=Request.QueryString[ConstStrings.SEARCH_CATEGORY] %>" />
    </span>
</form>
<%}
    else
    {
        Response.Write("此列表被设置为不使用分页...");
    } %>

