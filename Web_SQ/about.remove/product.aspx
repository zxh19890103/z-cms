<%@ Page Language="C#" Inherits="Nt.Web.Product" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>



<%
    ChannelNo = 2;
    SortID = 1;
    TryGetList();
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
                    <a href="#">首 页</a>   > <a href="#">研究中心</a>   >   <%=CurrentCategoryName %>
                </div>
                <a class="gotoback" href="#">返回</a>
                
                <%
                    Response.Write("<div class=\"Research_short\">");
                    Response.Write("	<h1>" + CurrentCategoryName + "</h1>");
                    switch (SortID)
                    {
                        case 1:
                            Response.Write("	<h2>SSS</h2>");
                            Response.Write("	<span></span>");
                            Response.Write("	<p>奉行着严谨认真，每一个结论都经过不断的验证</p>");
                            Response.Write("	<strong>A rigorous, each conclusion through continuous validation</strong>");

                            break;
                        case 2:
                            Response.Write("	<h2>SSS</h2>");
                            Response.Write("	<span></span>");
                            Response.Write("	<p>奉行着严谨认真，每一个结论都经过不断的验证</p>");
                            Response.Write("	<strong>A rigorous, each conclusion through continuous validation</strong>");

                            break;
                        default: break;
                    }
                    Response.Write("</div>");
                %>

                <div class="Research_list">
                    <ul id="Research_list">

                        <%
                            foreach (DataRow r in DataList.Rows)
                            {
                                Response.Write("<li>");
                                Response.Write("	<a href=\"" + GetProductUrl(r["id"], "productdetail.aspx") + "\">");
                                Response.Write("		<img src=\"" + r["thumbnail"] + "\" width=\"327\" height=\"238\" />");
                                Response.Write("		<p>" + r["title"] + "</p>");
                                Response.Write("		<span>" + r["short"] + "</span>");
                                Response.Write("	</a>");
                                Response.Write("</li>");
                            }                            
                        %>
                    </ul>
                    <script>
                        $('#Research_list li:odd').css('padding-right', '0px');
                    </script>
                </div>
                <div class="clear" style="height: 26px; width: 100%;"></div>

                <div class="page">

                    <%                      
                          
                        foreach (var item in Pager)
                        {
                            Response.Write("<a href=\"" + GetPagerUrl(item.Value) + "\" class=\"num");
                            if (item.Selected)
                                Response.Write(" numed");
                            Response.Write("\">" + item.Text + "</a>");
                        }
                    %>
                </div>

            </div>
        </div>
    </div>
    <uc1:foot runat="server" ID="foot" />
</body>
</html>