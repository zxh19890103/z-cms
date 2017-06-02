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
	<script>
	var nod = "1";
	
	function rating(sender,id){
	
	if(nod=="1" ){
	$.post('/handlers/rating.ashx',
	{id:id},
	function(json){
		sender.innerHTML=parseInt(sender.innerHTML)+1;
	},
	'text');
	nod = "2";
	
	}else{
		alert("你已经赞过了~~~");
	}
	}
	</script>
</head>

<body>

    <uc1:top runat="server" ID="top" />

    <div class="content">
        <div class="content_in">

            <uc1:left runat="server" ID="left" />

            <div class="content_right">
                <div class="position">
				
                    <a href="/">首 页</a>   > <a href="/about/">关于士奇咨询</a>   >   <%=CurrentCategoryName %>
                </div>
                <a class="gotoback" href="javascript:window.history.go(-1)">返回</a>
                <div class="Research_short">
            	<h1>我们的团队</h1>
                <h2>Our team</h2>
                <span></span>
                <p>我们的团队是独一无二的，共同的信念使我们走到一起</p>
                <strong>Our team is the one and only, the common belief that we come together</strong>
                
            </div>
               

                <div class="team_list">
                    <ul>

                        <%
                            foreach (DataRow r in DataList.Rows)
                            {
                                Response.Write("<li>");
								
								Response.Write("	<div class=\"team_list_in\">");
								Response.Write("	  <img src=\"" + r["thumbnailurl"] + "\" width=\"144\" height=\"158\" />");
								Response.Write("	<a href=\"" + GetProductUrl(r["id"], "productdetail.aspx") + "\">");
								Response.Write("	<h1>"+r["ManNameTeam"].ToString().GetSubStr(24)+"</h1>");
								Response.Write("	<h2>"+r["Poster"].ToString().GetSubStr(34)+"</h2>");
								Response.Write("	<p>"+r["short"].ToString().GetSubStr(250)+"</p>");
								Response.Write("	</a>");
								Response.Write("	<span onclick=\"rating(this,"+r["id"]+");\">"+r["clickrate"]+"</span>");
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

                <div class="page">
							<span>		
                    <%                      
                          
                        foreach (var item in Pager)
                        {
                            Response.Write("<a href=\"" + GetPagerUrl(item.Value) + "\" class=\"num");
                            if (item.Selected)
                                Response.Write(" numed");
                            Response.Write("\">" + item.Text + "</a>	");
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