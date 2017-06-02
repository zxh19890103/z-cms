<%@ Page Language="C#" Inherits="Nt.Web.News" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%@ Register Src="~/shared/hotnews.ascx" TagPrefix="uc1" TagName="hotnews" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ChannelNo = 3;
        SetDefaultSortIDIfRequestFailed(1);
        TryGetList();
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
                    <a href="#">首 页</a>   > 
                    <%
                        WriteCrumbs();
                    %>
                </div>
                <a class="gotoback" href="#">返回</a>


                <uc1:hotnews runat="server" ID="hotnews" />

                <div class="clear"></div>
                <div class="content_in_list_news">
                    <div class="news_list_title">
                        <h2>士奇动态</h2>
                        <span>Shiqi news</span>
                    </div>
                    <ul id="news_list_in">


                        <%
                            foreach (DataRow item in DataList.Rows)
                            {
                                Response.Write("<li>");
                                Response.Write("	<a href=\"" + GetNewsUrl(item["id"], "newsdetail.aspx") + "\">");
                                Response.Write("	<h2>" + item["Title"] + "<span>" + CommonUtility.DateTimeFormat(item["adddate"], "yyyy-MM-dd") + "</span></h2>");
                                Response.Write("	<p>" + item["short"] + "</p>");
                                Response.Write("</a>");
                                Response.Write("</li>");
                            }
                        %>
                    </ul>
                </div>
                <div class="clear" style="height: 60px; width: 100%;"></div>

                <div class="page">
                    <span>

                        <%
                            
                            foreach (var item in Pager)
                            {
                                Response.Write("<a href=\"" + GetPagerUrl(item.Value) + "\" class=\"num");
                                if (item.Selected)
                                    Response.Write(" numed");
                                Response.Write("\">" + item.Text + "</a>");
                            }
                            
                        %>
                    </span>
                </div>

            </div>
        </div>
    </div>


    <uc1:foot runat="server" ID="foot" />

</body>
</html>

