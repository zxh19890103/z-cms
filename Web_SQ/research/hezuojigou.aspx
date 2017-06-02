<%@ Page Language="C#" Inherits="Nt.Web.BasePage" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>


<%
    ChannelNo = 6;
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

                    <a href="/">首 页</a>  &gt; <a href="/research/">关于士奇咨询</a>   &gt;   合作机构
                </div>
                <a class="gotoback" href="javascript:window.history.go(-1)">返回</a>

                <%
                    Response.Write("<div class=\"Research_short\">");
                    Response.Write("<h1>合作机构</h1>");
                    Response.Write("<h2>Cooperation Agency</h2>");
                    Response.Write("<span></span>");
                    Response.Write("<p>奉行着严谨认真，每一个结论都经过不断的验证</p>");
                    Response.Write(" <strong>A rigorous, each conclusion through continuous validation</strong>");
                %>
            </div>
            <div class="content_right_content">
                <ul  class="new_list_page new_list_page2">

                    <%
                        
                        DataTable data = new DataTable();
                        
                        using (var conn = SqlHelper.GetConnection())
                        {
                            conn.Open();
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "select * from nt_hezuojigou where display=1 order by displayorder"; 
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(data);
                            conn.Close();
                        }

                        foreach (DataRow r in data.Rows)
                        {
                            Response.Write("<li>");
                            Response.Write("	<a class=\"new_list_page_img\" href=\"");
                            Response.Write(r["url"]);
                            Response.Write("\">");
                            Response.Write("	  <img src=\"");
                            Response.Write(r["img"]);
                            Response.Write("\" width=\"144\" height=\"158\" />");
                            Response.Write("	</a>");
                            Response.Write("<div  class=\"new_list_page_text\">");
                            Response.Write("	<h3>");
                            Response.Write(r["title"]);
                            Response.Write("</h3>");
                            Response.Write("	</div>");
                            Response.Write("</li>");
                        } 
                    %>
                </ul>
                <script>
                    $('#Research_list li:odd').css('padding-right', '0px');
                </script>
            </div>
            <div class="clear" style="height: 26px; width: 100%;"></div>
        </div>
    </div>
    <uc1:foot runat="server" ID="foot" />
</body>
</html>
