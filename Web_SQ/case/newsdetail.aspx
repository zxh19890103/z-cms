<%@ Page Language="C#" Inherits="Nt.Web.NewsDetail" %>

<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>


<%
    
    ChannelNo = 7;
    TryGetModel();
    CalculateNextAndPrevious();
    
%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=PageTitle %></title>
    <meta name="keywords" content="<%=Keywords %>" />
    <meta name="description" content="<%=Description %>" />    
    <!--#include file="../html.inc/head.htm"-->
</head>


<body>

    <uc1:top runat="server" ID="top" />

    
    <div class="content">
        <div class="content_in">
            
            <uc1:left runat="server" ID="left" />

            <div class="content_right">
                <div class="position">
                    <a href="/">首 页</a>   > 
                    <%WriteCrumbs();%>
                </div>
                <a class="gotoback" href="javascript:window.history.go(-1);">返回</a>
                <div class="newsshow_short">
                    <h2><%=Model.Title %></h2>
                    <p><span>作者：<%=Model.Author %>   </span><span>时间：<%=Model.AddDate.ToString("yyyy-MM-dd") %>    </span><span>浏览次数：<%=Model.ClickRate %></span></p>
                </div>

                <div id="editor_content">
                    <div class="content_in_NewsShow">
                        
                        <%=Model.Body %>


                    </div>
                    <div class="clear" style="width: 100%; height: 50px;"></div>
                    <div class="news_show_page">
                        <span class="per_page_title">上一篇：<a href="?id=<%=PreID %>"><%=PreTitle %> </a></span>
                        <span class="next_page_title">下一篇：<a href="?id=<%=NextID %>"><%=NextTitle %></a></span>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <uc1:foot runat="server" ID="foot" />


</body>
</html>

