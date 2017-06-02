<%@ Page Language="C#" Inherits="Nt.Web.BasePage" %>


<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>


<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ChannelNo = 6;
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=PageTitle %></title>
    <meta name="keywords" content="<%=Keywords %>" />
    <meta name="description" content="<%=Description %>" />
    <!--#include file="../html.inc/head.htm"-->
    <!--#include file="../html.inc/head.news.htm"-->
</head>

<body>

    <uc1:top runat="server" ID="top" />

    <div class="content">
        <div class="content_in">

            <uc1:left runat="server" ID="left" />

            <div class="content_right">
                <div class="position">
                    <a href="/">首 页</a>  &gt;士奇研究中心&gt;研究报告
                </div>
                
                <div class="content_right_content">

                    <%
                        //输出新闻类别的列表
                        MyUtility.C.CacheNamePattern = "sub-index-{0}";
                        Response.Write("<ul class=\"new_list_page\">");
                        MyUtility.C.OutNewsClasses(16,
                            "<li><a class=\"new_list_page_img\" href=\"news.aspx?sortid={id}\"><img src=\"{pictureurl}\"/></a><div class=\"new_list_page_text\"><h3>{title}</h3>{description}</div></li>");
                        Response.Write("</ul>");
                        
                    %>
                </div>


            </div>
        </div>
    </div>
    <uc1:foot runat="server" ID="foot" />
</body>
</html>
