<%@ Page Language="C#" Inherits="Nt.Web.News" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>



<script runat="server">

    protected Nt_NewsCategory Model;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ChannelNo = 4;
        SetDefaultSortIDIfRequestFailed(6);
        Model = CommonFactory2.GetById<Nt_NewsCategory>(SortID);
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
                    <a href="/">首 页</a>   > 
                    <%
                        WriteCrumbs();
                    %>
                </div>
                <a class="gotoback" href="javascript:history.go(-1);">返回</a>


                <div class="clear"></div>
                <div class="content_in_list_news">
                    <div class="news_list_title">
                        <%
                            Response.Write(" <h2>");
                            Response.Write(Model.Name);
                            Response.Write("</h2>");
                            Response.Write("<span>");
                            Response.Write(Model.EnTitle);
                            Response.Write("</span>");
                            Response.Write("<img src=\"");
                            Response.Write(Model.PictureUrl);
                            Response.Write("\" width=\"224\" height=\"88\" />");
                            Response.Write("<div class=\"news_list_description\">");
                            Response.Write(Model.Description);
                            Response.Write("</div>");
                        %>
                    </div>

                    <ul id="news_list_in_two">
                        <%
                            foreach (DataRow item in DataList.Rows)
                            {
                                Response.Write("<li>");
                                Response.Write("	<a href=\"" + GetNewsUrl(item["id"], "newsdetail.aspx") + "\">");
                                Response.Write("<img src=\"" + item["FirstPicture"] + "\" width=\"225\" height=\"144\" />");
                                Response.Write("	<p>" + item["title"].ToString().GetSubStr(250) + "</p>");
                                Response.Write("</a>");
                                Response.Write("</li>");
                            }
                        %>
                    </ul>
                    <script>
                        $('#news_list_in_two li:eq(2)').css('padding-right', '0px');
                        $('#news_list_in_two li:eq(5)').css('padding-right', '0px');
                        $('#news_list_in_two li:eq(8)').css('padding-right', '0px');

                    </script>




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

